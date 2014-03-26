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
                foreach (var callType in (LatticeEnum.Call[])System.Enum.GetValues(typeof(LatticeEnum.Call)))
                {
                    if (callType.ToString().Equals("None"))
                    {
                        continue;
                    }

                    var call = new Call
                    {
                        Signature = callType.ToString()
                    };

                    if (callType.ToString().Equals(LatticeEnum.Call.Sslpublishoverride.ToString()))
                    {
                        BuildSslpublishoverride(ref call);

                        call.CallType = LatticeEnum.CallType.Publish.ToString();
                    }
                     else if (callType.ToString().Equals(LatticeEnum.Call.Sslrecordpublish.ToString()))
                    {
                        call.CallType = LatticeEnum.CallType.Publish.ToString();
                    }
                     else if (callType.ToString().Equals(LatticeEnum.Call.Sslrequestpublish.ToString()))
                    {
                        call.CallType = LatticeEnum.CallType.Publish.ToString();
                    }
                     else if (callType.ToString().Equals(LatticeEnum.Call.Sslrecordrequest.ToString()))
                     {
                         BuildSslrecordrequest(ref call);

                        call.CallType = LatticeEnum.CallType.Subscribe.ToString();
                    }
                     else if (callType.ToString().Equals(LatticeEnum.Call.Sslcheckbox.ToString()))
                    {
                        call.CallType = LatticeEnum.CallType.Publish.ToString();
                    }
                     else if (callType.ToString().Equals(LatticeEnum.Call.Sslcombobox.ToString()))
                    {
                        call.CallType = LatticeEnum.CallType.Publish.ToString();
                    }
                    else if (callType.ToString().Equals(LatticeEnum.Call.Sslrecordinsert.ToString()))
                    {
                        call.CallType = LatticeEnum.CallType.Publish.ToString();
                    }
                    else if (callType.ToString().Equals(LatticeEnum.Call.Sslrecordinserttrigger.ToString()))
                    {
                        call.CallType = LatticeEnum.CallType.Publish.ToString();
                    }

                    context.Calls.Add(call);
                    context.SaveChanges();
                }
            }
        }

        private static void BuildSslrecordrequest(ref Call call)
        {
            var parameters = new List<Parameter>();

            var param1 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Source.ToString(),
                Position = 1
            };

            parameters.Add(param1);

            var param2 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Instrument.ToString(),
                Position = 2
            };

            parameters.Add(param2);

            var param3 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Fid.ToString(),
                Position = 3
            };

            parameters.Add(param3);

            call.Parameters = parameters;
        }

        private static void BuildSslpublishoverride(ref Call call)
        {
            var parameters = new List<Parameter>();

            var param1 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Source.ToString(),
                Position = 1
            };

            parameters.Add(param1);

            var param2 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Call.ToString(),
                Position = 2
            };

            parameters.Add(param2);

            var param3 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Fid.ToString(),
                Position = 3
            };

            parameters.Add(param3);

            var param4 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Call.ToString(),
                Position = 4
            };

            parameters.Add(param4);

            var param5 = new Parameter
            {
                ParameterType = LatticeEnum.ParamterType.Boolean.ToString(),
                Position = 5
            };

            parameters.Add(param5);

            call.Parameters = parameters;
        }
    }
}
