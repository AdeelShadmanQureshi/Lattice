using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Domain
{
    public class Argument
    {
        public Argument()
            : this(true)
        {

        }

        public Argument(bool isValid)
        {
            this.IsValid = isValid;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Parameter Parameter { get; set; }

        public bool IsValid { get; set; }

        public Command Command { get; set; }
    }
}
