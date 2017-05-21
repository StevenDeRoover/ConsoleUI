using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.UI.Extensions
{
    public static class StringExt
    {
        public static string TruncWords(this string text, int maxCharacters, string ellipsis = "...")
        {
            if (text == null || (text = text.Trim()).Length <= maxCharacters)
                return text;
            int trailLength = ellipsis.StartsWith("&") ? 1 : ellipsis.Length;
            maxCharacters = maxCharacters - trailLength >= 0 ? maxCharacters - trailLength : 0;
            int pos = text.LastIndexOf(' ', maxCharacters);
            if (pos >= 0)
                return text.Substring(0, pos) + ellipsis;
            return string.Empty;
        }
    }
}
