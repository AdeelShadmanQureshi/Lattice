using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ropnoy.Lattice.Domain;

namespace Ropnoy.Lattice.Core.Extractor
{
    public interface IExtract
    {
        int Extract(Cell cell);
    }
}
