using System;
using System.Globalization;

namespace LoonieTrader.Library.Constants
{
    public static class AppProperties
    {
        public static string ApplicationName { get; set; } = "LoonieTrader";

        // Used for non-visible things like settings folder and should not change
        public static string ApplicationNameInternal { get; set; } = "LoonieTrader";

        public static string FavouritesFolderName { get; set; } = "Favourites";

        public static IFormatProvider ServerCulture { get; set; } = new CultureInfo("en-US");
    }
}