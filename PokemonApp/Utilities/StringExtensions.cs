using System.Collections.Generic;
using System.Globalization;

namespace PokemonApp.Utilities
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        public static string JoinList(this IList<string> listValue)
        {
            return string.Join(", ", listValue);
        }
    }
}