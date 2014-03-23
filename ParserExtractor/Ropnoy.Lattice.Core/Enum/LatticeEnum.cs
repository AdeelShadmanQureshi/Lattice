using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Core.Enum
{
    public class LatticeEnum
    {
        public enum CallType
        {
            Sslrequestpublish,
            Sslrecordrequest,
            Sslrecordpublish, 
            Sslrecordinsert, 
            Sslrecordinserttrigger,     
            Sslcheckbox, 
            Sslcombobox, 
            Sslpublishoverride 
        };

        public enum ParamterType
        {
            Source,
            Instrument,
            Fid,
            Call,
            String,
            Boolean
        };
    }
}
