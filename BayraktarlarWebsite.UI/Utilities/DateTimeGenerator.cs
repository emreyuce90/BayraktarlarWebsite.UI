using System;

namespace BayraktarlarWebsite.UI.Utilities
{
    public static class DateTimeGenerator
    {
        public static string GetDateTimeWithUnderScore(this DateTime dateTime)
        {
            return $"{dateTime.Millisecond}_{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";
        }
    }
}
