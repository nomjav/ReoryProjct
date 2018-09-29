using System;
using System.Globalization;

namespace AcademyLockSmith.Data.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)  //"this DateTime dt" for extenstion method instead of "DateTime dt"
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Get the first day of the month for
        /// any full date submitted
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime CurrentMonthStart(this DateTime date)
        {
            // set return value to the first day of the month
            // for any date passed in to the method

            // create a datetime variable set to the passed in date
            DateTime dtFrom = date;

            // remove all of the days in the month
            // except the first day and set the
            // variable to hold that date
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            // return the first day of the month
            return dtFrom;
        }

        /// <summary>
        /// Get the last day of the month for any
        /// full date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            // set return value to the last day of the month
            // for any date passed in to the method

            // create a datetime variable set to the passed in date
            DateTime dtTo = date;

            // overshoot the date by a month
            dtTo = dtTo.AddMonths(1);

            // remove all of the days in the next month
            // to get bumped down to the last day of the
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));

            // return the last day of the month
            return dtTo;
        }

        /// <summary>
        /// Get the last monday of previous week
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime PreviousWeekStart(this DateTime date)
        {
            return date.AddDays(-(int)date.DayOfWeek - 6);
        }

        /// <summary>
        /// Get the last sunday of previous week
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime PreviousWeekEnd(this DateTime date)
        {
            return date.AddDays(-(int)date.DayOfWeek);
        }

        /// <summary>
        /// Get the 1st of previous month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime PreviousMonthStart(this DateTime date)
        {
            return DateTime.Now.CurrentMonthStart().AddMonths(-1);
        }

        /// <summary>
        /// Get the last date of previous month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime PreviousMonthEnd(this DateTime date)
        {
            return DateTime.Now.CurrentMonthStart().AddSeconds(-1);
        }

        /// <summary>
        /// Get the 1st of current year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime CurrentYearStart(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// Count number of days in a year
        /// 
        /// Integer representation of a year. i.e. DateTime.Now.Year
        /// 
        public static int GetDaysInYear(int year)
        {
            var thisYear = new DateTime(year, 1, 1);
            var nextYear = new DateTime(year + 1, 1, 1);

            return (nextYear - thisYear).Days;
        }

        public static DateTime EndOfWeek(DateTime dateTime) // for end of week
        {
            DateTime start = StartOfWeek(dateTime, DayOfWeek.Monday);

            return start.AddDays(7).AddTicks(-1);
        }

        /// <summary>
        /// this method will return parsed dateTime
        /// </summary>
        /// <param name="date">date</param>
        /// <returns></returns>
        public static DateTime ToParsedDateTime(this string date)
        {
            return DateTime.ParseExact(date.Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
