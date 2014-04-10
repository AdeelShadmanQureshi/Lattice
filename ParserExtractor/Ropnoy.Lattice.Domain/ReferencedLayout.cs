using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class ReferencedLayout
    {
        public int Id { get; set; }

        public Cell CellContainingRef { get; set; }

        public Layout LayoutContainingRef { get; set; }

        public string ReferencedToCell { get; set; }

        public Layout ReferenceToLayout { get; set; }
    }
}
