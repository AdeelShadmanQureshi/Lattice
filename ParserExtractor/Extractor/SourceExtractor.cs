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
    public class SourceExtractor : IExtract
    {
        public SourceExtractor(LatticeContext context)
        {
            Context = context;
        }

        public LatticeContext Context { get; private set; }

        public int Extract(Cell cell)
        {
            if (!Regex.IsMatch(cell.Content, @"[@$=\(\)]"))
            {
                if (!Context.Sources.Any(s => s.Name.Equals(cell.Content)))
                {
                    Context.Sources.Add(new Source() { Name = cell.Content });
                    Context.SaveChanges();
                }

                var id = (from s in Context.Sources
                          where s.Name == cell.Content
                          select s.Id).FirstOrDefault();

                return id;
            }
            else
            {
                return 0;
            }
        }
    }
}
