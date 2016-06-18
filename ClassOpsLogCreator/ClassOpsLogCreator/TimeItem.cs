using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*This class is used to help fill in the time for the combo boxes.
 * 
 */
namespace ClassOpsLogCreator
{
    class TimeItem
    {
       
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Am { get; set; }

        //This is the toString method that will be invoked when the class
        //is called on the combo box. 
        public override string ToString()
        {
            return this.Hour + ":" + this.Minute;
        }
    }
}
