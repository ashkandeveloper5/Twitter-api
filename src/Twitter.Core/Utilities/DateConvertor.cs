using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Core.Utilities
{
    public class DateConvertor
    {
        public static string ToShamsi(DateTime dateTime)
        {
            PersianCalendar persianCalandar = new PersianCalendar();

            int year = persianCalandar.GetYear(dateTime);
            int month = persianCalandar.GetMonth(dateTime);
            int day = persianCalandar.GetDayOfMonth(dateTime);
            return $"{year}" + "/" + $"{month}" + "/" + $"{day}";
        }
    }
}
