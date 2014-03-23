using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ropnoy.Lattice.Core.Enum;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;

namespace Ropnoy.Lattice.Core.BootStrapper
{
    public class CallTypeBuilder
    {
        public static void Build()
        {
            using (var context = new LatticeContext())
            {
                foreach (var callType in System.Enum.GetValues(typeof(LatticeEnum.CallType)).Cast<string>())
                {
                    var call = new Call
                    {
                        Signature = callType
                    };

                    if (callType.Equals("SSLPUBLISHOVERRIDE"))
                    {
                        BuildSslpublishoverride(ref call);
                    }
                }
            }
        }

        private static void BuildSslpublishoverride(ref Call call)
        {
            var parameters = new List<Parameter>();

            var param1 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Source.ToString(),
                Position = 1
            };

            var param2 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Call.ToString(),
                Position = 2
            };

            var param3 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Fid.ToString(),
                Position = 3
            };

            var param4 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Call.ToString(),
                Position = 4
            };

            var param5 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Boolean.ToString(),
                Position = 5
            };

            call.Parameters = parameters;
        }
    }
}
