using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// This class will assist with assositating ranks with 
/// all the tasks in our logs
///
/// Class Version: 0.1.0.0 - BETA - 7282016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>
namespace ClassOpsLogCreator
{
    public class TaskRanks
    {
        private string[] value1 = null;
        private string[] value2 = null;
        private string[] value3 = null;
        private string[] value4 = null;

        /// <summary>
        /// The constructor that inistalizes all the arrays
        /// </summary>
        public TaskRanks()
        {
            //Value = 0 tasks
            value1 = new string[]
            {
                "AV Shutdown",  "Crestron Logout", "Proactive Classroom Check",
                "Other", "SCLD Student Logout"
            };

            // Value = 1 tasks
            value2 = new string[]
           {
               "Operator", "Replace Battery", "SCLD Student Event"
           };

            //Value = 2 tasks
             value3 = new string[]
           {
               "Inperson Technical Assistance", "Demo", "CLOSE ACE017", "Lockup",
               "Pickup Large PA","Pickup Mic", "Pickup PC","Pickup Projector",
               "Pickup Skype Kit","Pickup Small PA"
           };

            // Value = 3 tasks
            value4 = new string[]
           {
               "Setup Large PA","Setup Mic","Setup PC","Setup Projector",
               "Setup Skype Kit","Setup Small PA"
           };
        }

        /// <summary>
        /// This method evalutes the current task and determines the 
        /// weight of the task. 
        /// 
        /// return -1 if the task does not exist in our database                                                                                                                                                                                                                                                                  
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public int getTaskValue(string task)
        {
            int value = 0;
            if(value1.Contains(task))
            {
                value = 1;
            }
            else if (value2.Contains(task))
            {
                value = 2;
            }
            else if (value3.Contains(task))
            {
                value = 3;
            }
            else if (value4.Contains(task))
            {
                value = 4;
            }
            return value;
        }

        /// <summary>
        /// This method will return the total task value of 
        /// said zone
        /// </summary>
        /// <param name="taskArray"></param>
        /// <returns></returns>
        public int getTotalTaskValue(string[,] taskArray)
        {
            int value = 0;
            for(int i = 0; i <= taskArray.GetUpperBound(0); i++)
            {
                value += this.getTaskValue(taskArray[i, 1]);
            }
            return value;
        }

        public Boolean isSubsetSum(string[,] arr, int n, int sum)
        {
            //base case
            if(sum == 0)
            {
                return true;
            }
            if(n == 0 && sum != 0)
            {
                return false;
            }

            //if the last element is greate than the sum then ignore it
            if(this.getTaskValue(arr[n - 1, 1]) > sum )
            {
                return isSubsetSum(arr, n - 1, sum);
            }

            /* else, check if sum can be obtained by any of
               the following
               (a) including the last element
               (b) excluding the last element
            */
            return isSubsetSum(arr, n - 1, sum) || isSubsetSum(arr, n - 1, sum - this.getTaskValue(arr[n - 1, 1]));
        }
    }
}
