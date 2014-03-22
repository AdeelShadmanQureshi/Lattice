using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ropnoy.Lattice.Core.Extractor;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;
using Transformer;

namespace Extractor
{
    public class SubscribeExtractor : IExtract
    {
        public SubscribeExtractor(Layout layout, LatticeContext context)
        {
            Layout = layout;
            Context = context;
        }

        public LatticeContext Context { get; private set; }

        public Layout Layout { get; private set; }

        public int Extract(Cell mainCell)
        {
            var command = mainCell.Content;

            var parameters = Regex.Match(command, @"@.*\((.*)\)").Groups[1].Value.Split(',');

            if (parameters.Count() != 3)
            {
                return 0;
            }

            var transformer = new CellReferenceTransformer();

            var sourceRef = transformer.Tranform(parameters[0]);

            var instrumentRef = transformer.Tranform(parameters[1]);

            var fidRef = transformer.Tranform(parameters[2]);

            var publish = new Publish
            {
                Layout = Layout,
                Cell = mainCell
            };

            foreach (var cell in Layout.Cells)
            {
                if (cell.Row == sourceRef.Item1 && cell.Column == sourceRef.Item2)
                {
                    var sourceExtractor = new SourceExtractor(Context);
                    var sourceId = sourceExtractor.Extract(cell);

                    Source source = null;
                    if (sourceId == 0)
                    {
                        source = new Source
                        {
                            Name = cell.Content,
                            IsValid = false
                        };
                        publish.IsValid = false;
                    }
                    else
                    {
                        source = (from s in Context.Sources
                                  where s.Id == sourceId
                                  select s).FirstOrDefault();
                    }


                    publish.Source = source;

                }

                if (cell.Row == instrumentRef.Item1 && cell.Column == instrumentRef.Item2)
                {
                    var instrumentExtractor = new InstrumentExtractor(Context);
                    var instrumentId = instrumentExtractor.Extract(cell);

                    Instrument instrument = null;

                    if (instrumentId == 0)
                    {
                        instrument = new Instrument
                        {
                            Name = cell.Content,
                            IsValid = false
                        };
                        publish.IsValid = false;
                    }
                    else
                    {
                        instrument = (from i in Context.Instruments
                                      where i.Id == instrumentId
                                      select i).FirstOrDefault();
                    }

                    publish.Instrument = instrument;
                }

                if (cell.Row == fidRef.Item1 && cell.Column == fidRef.Item2)
                {
                    var fidExtractor = new FidExtractor(Context);
                    var fidId = fidExtractor.Extract(cell);

                    Fid fid = null;

                    if (fidId == 0)
                    {
                        fid = new Fid
                        {
                            Name = cell.Content,
                            IsValid = false
                        };

                        publish.IsValid = false;
                    }
                    else
                    {
                        fid = (from f in Context.Fids
                               where f.Id == fidId
                               select f).FirstOrDefault();
                    }

                    publish.Fid = fid;

                }
            }

            Context.Publishs.Add(publish);
            Context.SaveChanges();

            return 1;
        }
    }
}
