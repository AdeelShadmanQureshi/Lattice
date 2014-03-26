using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ropnoy.Lattice.Core.Extractor;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;
using Transformer;

namespace Extractor
{
    public class CommandExtractor : IExtract
    {
        private const string CallRegex = @"@([a-zA-z]+)\(";
        public LatticeContext Context { get; private set; }
        public CommandExtractor(Layout layout, LatticeContext context)
        {
            Context = context;
            Layout = layout;
        }

        public Layout Layout { get; private set; }

        public int Extract(Cell mainCell)
        {
            var commandText = mainCell.Content;

            var signature = Regex.Match(commandText, CallRegex).Groups[1].Value;

            var call = (from c in Context.Calls
                        where c.Signature.Equals(signature, StringComparison.InvariantCultureIgnoreCase)
                        select c).FirstOrDefault();

            if (call == null)
                return 0;

            var command = new Command();

            command.Arguments = new List<Argument>();

            command.Call = call;

            var arguments = Regex.Match(commandText, @"@.*\((.*)\)").Groups[1].Value.Split(',');

            int count = 0;
            var transformer = new CellReferenceTransformer();

            var parameters = (from p in Context.Parameters
                              where p.Call.Id == command.Call.Id
                              orderby p.Position
                              select p).ToList();

            if (parameters.Count != arguments.Count())
            {
                int i = 0;
            }

            foreach (var parameter in parameters)
            {
                if (parameters.Count > 3)
                {
                    int i = 0;
                }
                var transformedArgument = transformer.Tranform(arguments[count]);

                foreach (var cell in Layout.Cells)
                {
                    if (cell.Row == transformedArgument.Item1 && cell.Column == transformedArgument.Item2)
                    {
                        var extractor = new ArgumentExtractor(Context);
                        var argument = extractor.Extract(cell, parameter, command);

                        if (argument == null)
                        {
                            return 0;
                        }

                        command.Arguments.Add(argument);

                        break;
                    }
                }
                count++;
            }

            command.Layout = Layout;
            command.Cell = mainCell;

            Context.Commands.Add(command);
            Context.SaveChanges();

            return 1;
        }
    }
}
