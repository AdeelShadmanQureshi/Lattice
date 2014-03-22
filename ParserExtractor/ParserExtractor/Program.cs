using Extractor;
using Parser;
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
            var parser = new RawDataParser();
            parser.Parse(@"C:\Adeel\Marc\Lattice\Brent Swaps\Naptha NWE Server-test.layout");

            Execute();

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
                            var parameters = Regex.Match(originalContent, @"@.*\((.*)\)").Groups[1].Value.Split(',');

                            if (parameters.Count() > 1)
                            {
                                if (parameters.Count() == 3)
                                {
                                    var extractor = new PublishExtractor(layout, context);
                                    extractor.Extract(cell);
                                }
                                else if (parameters.Count() == 5)
                                {
                                    var extractor = new SubscribeExtractor(layout, context);
                                    extractor.Extract(cell);
                                }
                                Console.WriteLine(parameters.Count());
                            }
                        }
                    }
                }
            }
        }
    }


}

