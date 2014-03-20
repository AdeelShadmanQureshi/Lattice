using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ropnoy.Lattice.Core.Extensions;
using Ropnoy.Lattice.Core.Transformer;
using Ropnoy.Lattice.Dal;
using Ropnoy.Lattice.Domain;

namespace Transformer
{
    public class CellReferenceTransformer : ITransform
    {
        public Tuple<int, int> Tranform(string cellRef)
        {

            if (cellRef.IndexOf("$", System.StringComparison.Ordinal)
                != cellRef.LastIndexOf("$", System.StringComparison.Ordinal))
            {
                if (cellRef.StartsWith("$"))
                {
                    var coordinates = cellRef.RemoveFirstCharacter().Split('$');

                    return GetRowAndColumn(coordinates[0], coordinates[1]);
                }
            }
            else
            {
                if (cellRef.StartsWith("$"))
                {
                    if (Regex.IsMatch(cellRef, @"(?<=\$)[a-zA-Z]+"))
                    {
                        var rowStr = Regex.Match(cellRef, @"(?<=\$)[a-zA-Z]+").Value;

                        var columnStr = Regex.Match(cellRef, @"\d+").Value;

                        return GetRowAndColumn(rowStr, columnStr);
                    }
                    else
                    {
                        throw new Exception(@"Beginning with $ cannot have both row and column as numbers.");
                    }
                    
                }
                else
                {
                    var coordinates = cellRef.Split('$');

                    return GetRowAndColumn(coordinates[0], coordinates[1]);
                }
            }

            return null;
        }

        private Tuple<int, int> GetRowAndColumn(string rowStr, string columnStr)
        {
            int row = 0;
            if (!Int32.TryParse(rowStr, out row)
                && !rowStr.Contains("$"))
            {
                row = rowStr.ExcelColumnNameToNumber();
            }

            int column = 0;
            if (!Int32.TryParse(columnStr, out column)
                && !columnStr.Contains("$"))
            {
                column = columnStr.ExcelColumnNameToNumber();
            }

            return Tuple.Create(row, column);
        }
    }
}
