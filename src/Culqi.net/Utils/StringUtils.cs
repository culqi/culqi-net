using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Culqi.Utils
{
    internal static class StringUtils
    {
        private static Regex whitespaceRegex = new Regex(@"\s", RegexOptions.CultureInvariant);
        public static bool ContainsWhitespace(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return whitespaceRegex.IsMatch(str);
        }
    }
}
