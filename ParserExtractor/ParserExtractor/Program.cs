using System.Configuration;
using System.IO;
using Extractor;
using NLog;
using Parser;
using Ropnoy.Lattice.Core.BootStrapper;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Transformer;

namespace ParserExtractor
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
 
        private const string PublishSubscribeCell = @"@";

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += AllUnhandledExceptionCatcher;

            Logger.Info("Execution Started");
            Console.WriteLine("Processing Started");

            Console.WriteLine("Start Building the Call and Parameter lookup tables");
            CallTypeBuilder.Build();
            Console.WriteLine("Finish Building the Call and Parameter lookup tables");

            var folderToRead = ConfigurationManager.AppSettings["LayoutFolder"];

            Console.WriteLine("Layout Folder to used " + folderToRead);

            Console.WriteLine("Start reading and saving all the layout files in the given folder");

            foreach (var fileName in Directory.EnumerateFiles(folderToRead, "*.layout"))
            {
                Console.WriteLine(@"Parsing (Saving the layout to database) - " + fileName);

                var parser = new RawDataParser();
                parser.Parse(fileName);
            }

            Console.WriteLine("Finish reading and saving files");

            

            Console.WriteLine("Start Extracting Commands and Arguments");

            Execute();


            Console.WriteLine("Finish Extracting Commands and Arguments");

            Logger.Info("Execution Finished");

            Console.WriteLine("Processing Complete. Please press any key to quit.");

            Console.ReadKey();
        }

        private static void Execute()
        {
            using (var context = new LatticeContext())
            {
                var layouts = (from layout in context.Layouts
                               select layout).ToList();

                foreach (var layout in layouts)
                {
                    Logger.Info("Processing Layout # " + layout.Id);

                    var cells = (from cell in layout.Cells
                                 select cell).ToList();

                    foreach (var cell in cells)
                    {
                        var originalContent = cell.Content;

                        if (Regex.IsMatch(originalContent, PublishSubscribeCell))
                        {
                            var extractor = new CommandExtractor(layout, context);
                            extractor.Extract(cell);
                        }
                    }
                }
            }
        }

        public static void AllUnhandledExceptionCatcher(object sender, UnhandledExceptionEventArgs  e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Logger.ErrorException(e.ExceptionObject.ToString(), (Exception)e.ExceptionObject);
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}

