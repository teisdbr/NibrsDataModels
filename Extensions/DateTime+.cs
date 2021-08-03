using System;

namespace NibrsModels.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetQuarter(this DateTime dt)
        {
            return dt.Month % 3 == 0 ? dt.Month / 3 : dt.Month / 3 + 1;
        }
        public static string ShortMMDDYYYY(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                return string.Format("{0:MMddyyyy}", dt);
            }
            return "        ";
        }

        /// <summary>
        /// Strips the time from datetime variable.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateOnly(this DateTime? dt)
        {
            return dt.HasValue ? string.Format(@"{0:MM/dd/yyyy}", dt) : "        ";
        }

        /// <summary>
        /// Given that the server is in Eastern Time, we need to add one hour
        /// for when the datetime is assumed to be EST or EDT.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime? AddOneHour(this DateTime? dt)
        {
            return dt?.AddHours(1);
        }

        public static DateTime AddOneHour(this DateTime dt)
        {
            return dt.AddHours(1);
        }
    }
}
