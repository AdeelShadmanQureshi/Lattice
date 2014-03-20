using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ropnoy.Lattice.Core.Extensions
{
    public static class StringExtension
    {
        public static int ExcelColumnNameToNumber(this string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("columnName");

            columnName = columnName.ToUpperInvariant();

            var sum = 0;

            foreach (char t in columnName)
            {
                sum *= 26;
                sum += (t - 'A' + 1);
            }

            return sum;
        }

        public static string RemoveFirstCharacter(this string str)
        {
            return str.Substring(1);
        }
    }
}
