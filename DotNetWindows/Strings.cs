using System.Text;
using System.Text.RegularExpressions;

namespace Jitsukawa.Extensions.Strings
{
    public static class Strings
    {
        /// <summary>
        /// Remove todos os acentos.
        /// </summary>
        public static string RemoveAcents(this string text)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(text));
        }

        /// <summary>
        /// Remove todos os caracteres que não forem dígitos.
        /// </summary>
        public static string ExtractDigits(this string text)
        {
            string digits = string.Empty;

            foreach (var d in Regex.Matches(text, "(\\d+)"))
                digits = string.Concat(digits, d);

            return digits;
        }
    }
}
