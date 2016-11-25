using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This call will determine if today is the day we make a Stat and send it out.
    /// </summary>
    public class ScheduleStatsGen 
    {
        //Private members
        private StatsGen statGenerator;
        private DateTime today;

        /// <summary>
        /// This is the constructor for the Schedule Stats generator. 
        /// 
        /// This class determines if today is the last working day of the week, month, 
        /// or first working day of the year and send out a report via email automatically.
        /// </summary>
        /// <param name="MainForm"></param>
        /// <param name="detailFrom"></param>
        public ScheduleStatsGen(LogCreator MainForm, DetailForm detailFrom)
        {
            //Check if today is either the end of the week, end of the month, or end of the year.
            //End of the week == Monday(of next week) 
            //End of the month == Last business day of the month
            //End of the year == One week into the new year. (Send out on the second Monday of the year)

            today = DateTime.Today;

            //If today is the end of the week we generate end of week statistics
            if (isEndOfWeek())
            {
                //Get the start date
                DateTime startDate = this.getFirstDayOfWeek();
                DateTime endDate = today;

                while (endDate.DayOfWeek != DayOfWeek.Friday)
                {
                    endDate = endDate.AddDays(-1);
                }
                //generate the stats
                detailFrom.updateDetail("Generating weekly statistics.");
                statGenerator = new StatsGen(MainForm, startDate, endDate, "Weekly");
                //Get the file path
                string filePath = MainForm.STATS_LOCATION + statGenerator.getfileName();
                //Send the email
                detailFrom.updateDetail("Sending Email.");
                EmailSender ES = new EmailSender(filePath, "Weekly CSCO PT Stats");
            }
            
            //If we are at the end of the month we auto generate statistics
            if(isEndOfMonth())
            {
                //Get the start date
                DateTime starteDate = this.getFirstDayOfMonth();
                //generate the stats
                detailFrom.updateDetail("Generating monthly statistics.");
                statGenerator = new StatsGen(MainForm, starteDate, today, "Monthly");
                //Get the file path
                string filePath = MainForm.STATS_LOCATION + statGenerator.getfileName();
                //Send the email
                detailFrom.updateDetail("Sending Email.");
                EmailSender ES = new EmailSender(filePath, "Monthly CSCO PT Stats for " + today.Month);
            }

            //If we are at the end of the year we end 
            if(isEndOfYear())
            {
                //Get the start date
                DateTime starteDate = this.getFirstDayOfYear();
                //generate the stats
                detailFrom.updateDetail("Generating yearly statistics.");
                statGenerator = new StatsGen(MainForm, starteDate, today, "Yearly");
                //Get the file path
                string filePath = MainForm.STATS_LOCATION + statGenerator.getfileName();
                //Send the email
                detailFrom.updateDetail("Sending Email.");
                EmailSender ES = new EmailSender(filePath, "Yearly CSCO PT stats for " + (today.Year - 1));
            }
        }

        /// <summary>
        /// Returns true if today is the end of the week. False otherwise.
        /// </summary>
        /// <returns></returns>
        private bool isEndOfWeek()
        {
            //If today is Friday then we return true;
            if(today.DayOfWeek == DayOfWeek.Monday)
            {
                return true;
            }
            //Else return false
            return false;
        }

        /// <summary>
        /// Return true if today is the last day of the month
        /// </summary>
        /// <returns></returns>
        private bool isEndOfMonth()
        {
            //Fill this list up with holidays
            var holidays = new List<DateTime> {/* list of observed holidays */};
            DateTime lastBusinessDay = new DateTime();
            //Get the number of days in the month
            var i = DateTime.DaysInMonth(today.Year, today.Month);
            //Iterate through each day in the month
            while (i > 0)
            {
                //The date to check
                var dtCurrent = new DateTime(today.Year, today.Month, i);
                if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                 !holidays.Contains(dtCurrent))
                {
                    //Found the last business day of the month
                    lastBusinessDay = dtCurrent;
                    i = 0;
                }
                else
                {
                    //move to the precious month
                    i = i - 1;
                }
            }

            //If today is the last business day then return true
            if (today == lastBusinessDay)
            {
                return true;
            }
            //return false otherwise.
            return false;
        }

        /// <summary>
        /// Return true if we are at the end of the year.
        /// 
        /// This check looks if we are in January, and looks if we are in the 
        /// second Monday of the January.
        /// </summary>
        /// <returns></returns>
        private bool isEndOfYear()
        {
            if (today.Month == 1)
            {
                //Fill this list up with holidays
                var holidays = new List<DateTime> {/* list of observed holidays */};
                DateTime firstBusinessDay = new DateTime();
                //Get the number of days in the month
                var i = 7;
                //Iterate through each day in the month
                while (i > 0)
                {
                    //The date to check
                    var dtCurrent = new DateTime(today.Year, 1, i);
                    if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                     !holidays.Contains(dtCurrent))
                    {
                        //Found the last business day of the month
                        firstBusinessDay = dtCurrent;
                        i = 0;
                    }
                    else
                    {
                        //move to the precious month
                        i = i + 1;
                    }
                }
                if(today == firstBusinessDay)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return the first day of the week
        /// </summary>
        /// <returns></returns>
        private DateTime getFirstDayOfWeek()
        {
            //get the date and subtract a day until we reach Monday.
            DateTime date = today.AddDays(-1);
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                date = date.AddDays(-1);
            }

            //Start of selected week and end of the given week.
            return date;
        }

        /// <summary>
        /// Get the first day of the month
        /// </summary>
        /// <returns></returns>
        private DateTime getFirstDayOfMonth()
        {
            var holidays = new List<DateTime> {/* list of observed holidays */};
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            var j = 1;
            while (j < 7)
            {
                var dtCurrent = new DateTime(today.Year, today.Month, j);
                if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                 !holidays.Contains(dtCurrent))
                {
                    startDate = dtCurrent;
                    j = 8;
                }
                else
                {
                    j++;
                }
            }
            return startDate;
        }

        private DateTime getFirstDayOfYear()
        {
            //Fill this list up with holidays
            var holidays = new List<DateTime> {/* list of observed holidays */};
            DateTime startDate = new DateTime(today.Year - 1, 1, 1);
            //Get the number of days in the month
            var i = 7;
            //Iterate through each day in the month
            while (i > 0)
            {
                //The date to check
                var dtCurrent = new DateTime(today.Year - 1, 1, i);
                if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                 !holidays.Contains(dtCurrent))
                {
                    //Found the last business day of the month
                    startDate = dtCurrent;
                    i = 0;
                }
                else
                {
                    //move to the precious month
                    i = i + 1;
                }
            }
            return startDate;
        }
    }
}
