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
    class TaskRanks
    {
        private string[] value0 = null;
        private string[] value1 = null;
        private string[] value2 = null;
        private string[] value3 = null;

        /// <summary>
        /// The constructor that inistalizes all the arrays
        /// </summary>
        public TaskRanks()
        {
            //Value = 0 tasks
            value0 = new string[]
            {

            };

            // Value = 1 tasks
            value1 = new string[]
           {

           };

            //Value = 2 tasks
             value2 = new string[]
           {

           };

            // Value = 3 tasks
            value3 = new string[]
           {

           };

        }
    }
}
