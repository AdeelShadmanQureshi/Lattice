using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Call
    {
        public int Id { get; set; }
        public string Signature { get; set; }

        public List<Parameter> Parameters { get; set; }

        public string CallType { get; set; }

    }
}
