using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Source
    {
        public Source()
            : this(true)
        {

        }

        public Source(bool isValid)
        {
            this.IsValid = isValid;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsValid { get; set; }
    }
}
