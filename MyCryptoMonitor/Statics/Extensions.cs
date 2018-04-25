using System;

namespace MyCryptoMonitor.Statics
{
    public static class Extensions
    {
        public static bool ExtEquals(this string source, string toCheck) => source?.Equals(toCheck, StringComparison.OrdinalIgnoreCase) ?? false;

        public static decimal ConvertToDecimal(this string source) => string.IsNullOrWhiteSpace(source) ? 0 : source.Contains("E") ? Convert.ToInt32(source.Substring(0, source.IndexOf("E"))) * Convert.ToInt32(source.Substring(source.IndexOf("E") + 1)) : decimal.Parse(source);

        public static decimal SafeDivision(this decimal source, decimal numerator) => source == 0 ? 0 : numerator / source;

        public static string ConvertToString(this decimal source, int maxDecimalPlaces)
        {
            var index = source.ToString().IndexOf(".");
            int places = source.ToString().Substring(index + 1).Length;

            if(places > maxDecimalPlaces)
                return Decimal.Round(source, maxDecimalPlaces).ToString();

            if (places < 2 || index == -1)
                return Decimal.Round(source, 2).ToString("0.00");

            return Decimal.Round(source, places).ToString();
        }
    }
}