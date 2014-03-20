using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Core.Transformer
{
    public interface ITransform
    {
        Tuple<int, int> Tranform(string cellRef);
    }
}
