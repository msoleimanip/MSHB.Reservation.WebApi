using System;

namespace MSHB.Reservation.Shared.Common.DateToolkit
{
    public class DateToolkit
    {
        public static string GetTime(TimeSpan? duration)
        {
            if (!duration.HasValue)
                return " --- ";
            return duration.Value.Hours.ToString().PadLeft(2, '0') + ":" +
                   duration.Value.Minutes.ToString().PadLeft(2, '0') + ":" +
                   duration.Value.Seconds.ToString().PadLeft(2, '0') ;
        }

        public static string GetTimeWithTotalSeconds(TimeSpan? duration)
        {
            if (!duration.HasValue)
                return " --- ";
            return duration.Value.Hours.ToString().PadLeft(2, '0') + ":" +
                   duration.Value.Minutes.ToString().PadLeft(2, '0') + ":" +
                   duration.Value.Seconds.ToString().PadLeft(2, '0') +
                   $" ({duration.Value.TotalSeconds} ثانیه)";
        }
    }
}
