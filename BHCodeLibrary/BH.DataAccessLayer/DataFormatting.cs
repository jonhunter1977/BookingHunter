using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BH.DataAccessLayer
{
    /// <summary>
    /// Provides functions for formatting data in a consistent manner to be stored in the data store.  
    /// </summary>
    internal static class DataFormatting
    {
        /// <summary>
        /// Formats a date time value to YYYYMMDD
        /// </summary>
        /// <param name="dateTimeToFormat">Date time to format</param>
        /// <returns>String formatted to YYYYMMDD</returns>
        internal static string FormatDateTime(DateTime dateTimeToFormat)
        {
            string yearPart = dateTimeToFormat.Year.ToString();
            string monthPart = dateTimeToFormat.Month.ToString().Length == 2 ? dateTimeToFormat.Month.ToString() : "0" + dateTimeToFormat.Month.ToString();
            string dayPart = dateTimeToFormat.Day.ToString().Length == 2 ? dateTimeToFormat.Day.ToString() : "0" + dateTimeToFormat.Day.ToString();

            return yearPart + monthPart + dayPart;
        }
    }
}
