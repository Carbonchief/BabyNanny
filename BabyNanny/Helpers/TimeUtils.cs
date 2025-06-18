using System;
using System.Collections.Generic;

namespace BabyNanny.Helpers
{
    public static class TimeUtils
    {
        public static string TimeDifference(DateTime? start, DateTime? end = null)
        {
            if (start == null)
                return "Invalid start time";

            end ??= DateTime.Now;
            var timeSpan = end.Value - start.Value;

            var months = (end.Value.Year - start.Value.Year) * 12 + end.Value.Month - start.Value.Month;
            if (end.Value.Day < start.Value.Day)
            {
                months--;
            }

            var days = timeSpan.Days % 30; // Approximate 30 days per month
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            var seconds = timeSpan.Seconds;

            var result = new List<string>();

            if (months > 0)
                result.Add($"{months}M");

            if (days > 0)
                result.Add($"{days}d");

            if (months == 0)
            {
                if (hours > 0)
                    result.Add($"{hours}h");

                if (days == 0)
                {
                    if (minutes > 0)
                        result.Add($"{minutes}m");

                    if (timeSpan.TotalMinutes < 1 && seconds > 0)
                        result.Add($"{seconds}s");
                }
            }

            return string.Join(" ", result);
        }
    }
}
