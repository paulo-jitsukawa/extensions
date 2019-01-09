using System;

namespace DotNetWindows
{
    public static class Mathematics
    {
        /// <summary>
        /// Arredonda um número por truncamento.
        /// </summary>
        /// <param name="mantissa">Quantidade de casas decimais.</param>
        public static decimal Truncate(this decimal number, uint mantissa)
        {
            var p = (decimal)Math.Pow(10, mantissa);
            return Math.Truncate(number * p) / p;
        }

        /// <summary>
        /// Arredonda um número por truncamento.
        /// </summary>
        /// <param name="mantissa">Quantidade de casas decimais.</param>
        public static double Truncate(this double number, uint mantissa)
        {
            var p = Math.Pow(10, mantissa);
            return Math.Truncate(number * p) / p;
        }

        /// <summary>
        /// Arredonda um número por arredondamento científico.
        /// </summary>
        /// <param name="mantissa">Quantidade de casas decimais.</param>
        public static decimal Round(this decimal number, uint mantissa)
        {
            var p = (decimal)Math.Pow(10, mantissa);
            return Math.Round(number * p) / p;
        }

        /// <summary>
        /// Arredonda um número por arredondamento científico.
        /// </summary>
        /// <param name="mantissa">Quantidade de casas decimais.</param>
        public static double Round(this double number, uint mantissa)
        {
            var p = Math.Pow(10, mantissa);
            return Math.Round(number * p) / p;
        }
    }
}
