using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Command
    {
        public int Id { get; set; }

        public Call Call { get; set; }

        public Cell Cell { get; set; }

        public Layout Layout { get; set; }

        public bool IsValid { get; set; }

        public List<Argument> Arguments { get; set; } 
    }
}
