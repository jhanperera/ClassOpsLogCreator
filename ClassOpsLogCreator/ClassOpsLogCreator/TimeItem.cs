using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassOpsLogCreator
{
    class TimeItem
    {
        /// <summary>
        /// Private attributes
        /// </summary>
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Am { get; set; }

        /// <summary>
        /// This is the toString method that will be invoked when the class
        /// is called on the combo box. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Hour + ":" + this.Minute;
        }
    }
}
