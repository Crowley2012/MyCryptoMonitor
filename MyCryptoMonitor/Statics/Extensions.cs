using System;

namespace MyCryptoMonitor.Statics
{
    public static class Extensions
    {
        public static bool ExtContains(this string source, string toCheck)
        {
            return source?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool ExtContains(this int source, string toCheck)
        {
            return source.ToString().IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool ExtContains(this Enum source, string toCheck)
        {
            return source.ToString().IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static decimal ConvertToDecimal(this string source) => string.IsNullOrWhiteSpace(source) ? 0 : decimal.Parse(source);

        public static decimal SafeDivision(this decimal source, decimal numerator) => source == 0 ? 0 : numerator / source;

        public static string ConvertToString(this decimal source, int maxDecimalPlaces)
        {
            var index = source.ToString().IndexOf(".");
            int places = source.ToString().Substring(index + 1).Length;

            if(places > maxDecimalPlaces)
                return Decimal.Round(source, maxDecimalPlaces).ToString();

            if (places < 2 || index == -1)
                return Decimal.Round(source, 2).ToString();

            return Decimal.Round(source, places).ToString();
        }
    }
}
