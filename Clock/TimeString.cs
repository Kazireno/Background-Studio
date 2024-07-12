using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    internal class TimeString
    {
        public static string GetTime() 
        {
            return DateTime.Now.ToString("HH") + " : " + System.DateTime.Now.ToString("mm") + " : " + System.DateTime.Now.ToString("ss") + " ";
        }
        public static string GetWeek() 
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "Sunday";
                case DayOfWeek.Monday:
                    return "Monday";
                case DayOfWeek.Tuesday:
                    return "Tuesday";
                case DayOfWeek.Wednesday:
                    return "Wednesday";
                case DayOfWeek.Thursday:
                    return "Thursday";
                case DayOfWeek.Friday:
                    return "Friday";
                case DayOfWeek.Saturday:
                    return "Saturday";
                default:
                    return "???";
            }
        }
        public static string GetYear() 
        {
            return DateTime.Now.ToString("yyyy") + " - " + DateTime.Now.ToString("MM") + " - " + DateTime.Now.ToString("dd");
        }
    }
}
