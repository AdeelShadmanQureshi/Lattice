using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ropnoy.Lattice.Core.Enum;
using Ropnoy.Lattice.Core.Extractor;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;

namespace Extractor
{
    public class ArgumentExtractor
    {
        public ArgumentExtractor(LatticeContext context)
        {
            Context = context;
        }

        public LatticeContext Context { get; private set; }

        public Argument Extract(Cell cell, Parameter parameter, Command command)
        {
            if (!Regex.IsMatch(cell.Content, @"[@$=\(\)]"))
            {
                if (!Context.Arguments.Any(s => s.Name.Equals(cell.Content)
                    && s.Parameter.ParameterType.Equals(parameter.ParameterType)
                    && s.Command.Id == command.Id))
                {
                    var arg = new Argument
                    {
                        Name = cell.Content,
                        Parameter = parameter,
                        Command = command
                    };
                    return arg;
                }
                return (from a in Context.Arguments
                        where a.Name.Equals(cell.Content)
                              && a.Parameter.ParameterType.Equals(parameter.ParameterType)
                        select a).FirstOrDefault();
            }

            var pType = LatticeEnum.ParamterType.Call.ToString();
            return new Argument
            {
                Name = cell.Content,
                Parameter = (from p in Context.Parameters
                             where p.ParameterType.Equals(pType)
                             select p).FirstOrDefault(),
                Command = command,
                IsValid = false
            };
        }
    }
}
