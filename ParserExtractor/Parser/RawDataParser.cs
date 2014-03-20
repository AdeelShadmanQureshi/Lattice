using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;

namespace Parser
{
    public class RawDataParser
    {
        const string StartCell = @"\[Cell:.+\]";
        const string EndCell = @"\[/Cell:.+\]";
        private const string FormulaCell1 = @"(?<=Formula==).*";
        private const string FormulaCell2 = @"(?<=Formula=).*";
        public void Parse(string filename)
        {
            using (var context = new LatticeContext())
            {
                var layout = new Layout
                {
                    Cells = new List<Cell>(),
                    Filename = "Naptha NWE Server-test",
                    Title = "Naptha NWE Server"
                };

                context.Layouts.Add(layout);
                context.SaveChanges();


                //read the file
                var lines =
                    System.IO.File.ReadAllLines(filename);

                for (int i = 0; i < lines.Count(); i++)
                {
                    //regex to check for a white space, a number, 2 or more white spaces then words after.
                    if (Regex.IsMatch(lines[i], StartCell))
                    {
                        var cell = new Cell
                        {
                            Row = Convert.ToInt32(Regex.Match(lines[i], @":(.*),").Groups[1].Value),
                            Column = Convert.ToInt32(Regex.Match(lines[i], @",(.*)\]").Groups[1].Value)
                        };

                        for (i++; i < lines.Count(); i++)
                        {
                            //ensure this line is not a title, else break out of it.
                            if (Regex.IsMatch(lines[i], EndCell))
                            {
                                break;
                            }

                            //if number only found, this is the start of a verse
                            if (Regex.IsMatch(lines[i], FormulaCell1))
                            {

                                cell.Content = Regex.Match(lines[i], FormulaCell1).Value;
                                layout.Cells.Add(cell);

                                break;
                            }
                            else if (Regex.IsMatch(lines[i], FormulaCell2))
                            {

                                cell.Content = Regex.Match(lines[i], FormulaCell2).Value;
                                layout.Cells.Add(cell);

                                break;
                            }

                        }

                    }
                }

                context.SaveChanges();
            }

            // Console.WriteLine("number of populated cells are {0}", layout.Cells.Count);
        }
    }
}
