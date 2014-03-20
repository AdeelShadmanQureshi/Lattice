using System.Linq;
using System.Text.RegularExpressions;
using Ropnoy.Lattice.Core.Extractor;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;
using Transformer;

namespace Extractor
{
    public class PublishExtractor : IExtract
    {
        public LatticeContext Context { get; private set; }
        public PublishExtractor(Layout layout, LatticeContext context)
        {
            Context = context;
            Layout = layout;
        }

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

                    if (sourceId == 0)
                    {
                        return 0;
                    }

                    var source = (from s in Context.Sources
                                  where s.Id == sourceId
                                  select s).FirstOrDefault();
                    publish.Source = source;

                }

                if (cell.Row == instrumentRef.Item1 && cell.Column == instrumentRef.Item2)
                {
                    var instrumentExtractor = new InstrumentExtractor(Context);
                    var instrumentId = instrumentExtractor.Extract(cell);

                    if (instrumentId == 0)
                    {
                        return 0;
                    }

                    var instrument = (from i in Context.Instruments
                                      where i.Id == instrumentId
                                      select i).FirstOrDefault();
                    publish.Instrument = instrument;

                }

                if (cell.Row == fidRef.Item1 && cell.Column == fidRef.Item2)
                {
                    var fidExtractor = new FidExtractor(Context);
                    var fidId = fidExtractor.Extract(cell);

                    if (fidId == 0)
                    {
                        return 0;
                    }

                    var fid = (from f in Context.Fids
                               where f.Id == fidId
                               select f).FirstOrDefault();
                    publish.Fid = fid;

                }
            }

            Context.Publishs.Add(publish);
            Context.SaveChanges();

            return 1;

        }
    }
}
