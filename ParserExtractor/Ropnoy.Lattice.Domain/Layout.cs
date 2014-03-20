using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Layout
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Filename { get; set; }

        public virtual IList<Cell> Cells { get; set; }
    }
}
