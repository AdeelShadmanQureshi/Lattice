using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ropnoy.Lattice.Core.Extractor;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;

namespace Extractor
{
    public class InstrumentExtractor : IExtract
    {
        public InstrumentExtractor(LatticeContext context)
        {
            Context = context;
        }

        public LatticeContext Context { get; private set; }

        public int Extract(Cell cell)
        {
            if (!Regex.IsMatch(cell.Content, @"[@$=\(\)]"))
            {
                if (!Context.Instruments.Any(i => i.Name.Equals(cell.Content)))
                {
                    Context.Instruments.Add(new Instrument() { Name = cell.Content });
                    Context.SaveChanges();
                }

                var id = (from i in Context.Instruments
                          where i.Name == cell.Content
                          select i.Id).FirstOrDefault();

                return id;
            }
            else
            {
                return 0;
            }
        }
    }
}
