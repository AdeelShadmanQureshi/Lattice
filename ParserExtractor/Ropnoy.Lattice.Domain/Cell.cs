using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Cell
    {
        public int Id { get; set; }
        public int Row { get; set; }

        public int Column { get; set; }

        public string Content { get; set; }

        public virtual Layout Layout { get; set; }
    }
}
