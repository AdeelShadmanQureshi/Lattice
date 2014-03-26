using Extractor;
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
        private const string PublishSubscribeCell = @"@";

        static void Main(string[] args)
        {
            CallTypeBuilder.Build();

            var parser = new RawDataParser();
            parser.Parse(@"C:\Adeel\Marc\Lattice\Brent Swaps\Naptha NWE Server-test.layout");

            Execute();

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
                    var cells = (from cell in context.Cells
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
    }


}

