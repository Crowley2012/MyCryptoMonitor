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

        public static decimal ConvertToDecimal(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return 0;

            return decimal.Parse(source);
        }
    }
}
