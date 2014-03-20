using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ropnoy.Lattice.Core.Extractor;
using Ropnoy.Lattice.Domain;

namespace Extractor
{
    public class SubscribeExtractor : IExtract
    {
        public SubscribeExtractor(Layout layout)
        {
            Layout = layout;
        }

        public Layout Layout { get; private set; }

        public int Extract(Cell mainCell)
        {
            var command = mainCell.Content;

            var parameters = Regex.Match(command, @"@.*\((.*)\)").Groups[1].Value.Split(',');

            return 1;
        }
    }
}
