using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ropnoy.Lattice.Core.Extractor;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;

namespace Extractor
{
    public class FidExtractor : IExtract
    {
        public FidExtractor(LatticeContext context)
        {
            Context = context;
        }

        public LatticeContext Context { get; private set; }

        public int Extract(Cell cell)
        {
            if (!Regex.IsMatch(cell.Content, @"[@$=\(\)]"))
            {
                if (!Context.Fids.Any(f => f.Name.Equals(cell.Content)))
                {
                    Context.Fids.Add(new Fid() { Name = cell.Content });
                    Context.SaveChanges();
                }

                var id = (from f in Context.Fids
                          where f.Name == cell.Content
                          select f.Id).FirstOrDefault();

                return id;
            }
            else
            {
                return 0;
            }
        }
    }
}
