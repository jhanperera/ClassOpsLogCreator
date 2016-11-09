using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This call will determine if today is the day we make a Stat and send it out.
    /// </summary>
    public class ScheduleStatsGen
    {
        private DateTime today;
        public ScheduleStatsGen()
        {
            //Check if today is either the end of the week, end of the month, or end of the year.
            //End of the week == Friday
            //End of the month == Last business day of the month
            //End of the year == One week into the new year. (Send out on the second monday of the year)

            today = DateTime.Now;
        }
    }
}
