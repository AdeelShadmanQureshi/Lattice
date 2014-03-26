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
            None = 0,
            Publish,
            Subscribe
        };
        public enum Call
        {
            None = 0,
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
            None = 0,
            Source,
            Instrument,
            Fid,
            Call,
            String,
            Boolean
        };
    }
}
