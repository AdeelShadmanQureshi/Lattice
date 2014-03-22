using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Instrument
    {
        public Instrument()
            : this(true)
        {

        }

        public Instrument(bool isValid)
        {
            this.IsValid = isValid;
        }

        public bool IsValid { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
