using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Publish
    {
        public Publish() : this(true)
        {
            
        }

        public Publish(bool isValid)
        {
            this.IsValid = isValid;
        }
        public int Id { get; set; }

        public Instrument Instrument { get; set; }

        public Source Source { get; set; }

        public Fid Fid { get; set; }

        public Cell Cell { get; set; }

        public Layout Layout { get; set; }

        public bool IsValid { get; set; }
    }
}
