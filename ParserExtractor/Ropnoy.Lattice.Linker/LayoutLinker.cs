using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NLog;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;

namespace Ropnoy.Lattice.Linker
{
    public class LayoutLinker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public LayoutLinker(LatticeContext context)
        {
            Context = context;
        }

        public LatticeContext Context { get; private set; }
        public void Link()
        {
            var layoutNames = Context.Layouts.Select(l => l.Title).ToArray().Select(s => s.ToUpperInvariant()).ToArray();

            foreach (var layout in Context.Layouts)
            {
                foreach (var cell in layout.Cells)
                {
                    FindValueInArray(cell, layout, layoutNames);
                }
            }

            return;
        }

        private void FindValueInArray(Cell cell, Layout layout, string[] listOfLayoutNames)
        {
            string layoutName = ExtractLayoutNameFromCell(cell.Content);

            if (string.IsNullOrEmpty(layoutName))
            {
                return;
            }

            int pos = Array.IndexOf(listOfLayoutNames, layoutName.ToUpperInvariant());
            if (pos > -1)
            {
                BuildReferencedLayout(cell, layout, listOfLayoutNames[pos]);
            }
        }

        private string ExtractLayoutNameFromCell(string cellContent)
        {
            return Regex.Match(cellContent, @"\[(.*)\]").Groups[1].Value;
        }

        private void BuildReferencedLayout(Cell cell, Layout layout, string layoutName)
        {
            var refLayout = Context.Layouts.Select(l => l).FirstOrDefault(l => l.Title == layoutName);

            if (refLayout == null)
            {
                return;
            }

            var referencedLayout = new ReferencedLayout()
            {
                LayoutContainingRef = layout,
                ReferenceToLayout = refLayout,
                CellContainingRef = cell,
                ReferencedToCell = cell.Content
            };

            Context.ReferencedLayouts.Add(referencedLayout);
            //Context.SaveChanges();
        }
    }
}
