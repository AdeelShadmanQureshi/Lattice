using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ropnoy.Lattice.Domain;

namespace Ropnoy.Lattice.Dal
{
    public class LatticeContext : DbContext
    {
        public LatticeContext()
            : base("LatticeContext")
        {
        }

        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Cell> Cells { get; set; }

        //public DbSet<Publish> Publishs { get; set; }

        //public DbSet<Instrument> Instruments { get; set; }

        //public DbSet<Source> Sources { get; set; }

        //public DbSet<Fid> Fids { get; set; }


        public DbSet<Call> Calls { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<Command> Commands { get; set; }

        public DbSet<Argument> Arguments { get; set; } 

    }
}
