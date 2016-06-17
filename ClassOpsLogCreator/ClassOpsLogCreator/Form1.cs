using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpsLogCreator
{
    public partial class LogCreator : Form
    {
        // Values to read in the excel file, leave static so the excel file is 
        // accessable everywhere. 
        private static Excel.Application roomSched = null;
        private static Excel.Workbook roomWorkBook = null;
        private static Excel.Worksheet roomSheet1 = null;

        /** Constructor for the system. (Changes here should be confirmed with everyone first) */
        public LogCreator()
        {
            InitializeComponent();
        }

        /** When the user clicks the "Create" Button this is what will happen
         */
        private void createBTN_Click(object sender, EventArgs e)
        {
            //Open the room excel file
            roomSched = new Excel.Application();
            roomSched.Visible = true;
            try
            {
                //This should look for the file one level up. (Temporary to keep everything local)
                roomWorkBook = roomSched.Workbooks.Open(@"..\room schedule.xlsx");
                //Work in worksheet number 1
                roomSheet1 = roomWorkBook.Sheets[1];

            }
            catch(Exception ex)
            {
                //File not found...
                textBox1.Text = "error: FILE NOT FOUND" +  ex.ToString();
                roomSched.Quit();
                return;

            }

            //Excel.Range last = MySheetRoom.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            //Get the range we are working within. (A1, A.LastRow)
            //Excel.Range range1 = roomSheet1.get_Range("A5", "A33");

            Excel.Range range1 = roomSheet1.UsedRange;
            //Lets export the whole range of raw data into an array. (Cell.Value2 is a fast and accurate operation to use)
            System.Array array = (System.Array)range1.Cells.Value2;
            
            //Now we extract all the raw data to strings 
            string[] arrayS = this.ConvertToStringArray(array);
            //DO WORK HERE



            //DEGUB CODE
            textBox1.Text = array.Length.ToString();
        }

           
        /**A Helper converter that will take our "values" and convert them into a string array. 
         * String parsing IS requires for now until we make it smart. 
         * 
         * A string array is returned by with white spaces.
         **/
        private string[] ConvertToStringArray(System.Array values)
        {
            string[] newArray = new string[values.Length];
            int index = 0;
            //The fun of double nester for loops, this is O(x^2)
            //This is the slowest part of the program at the moment
            for (int i = values.GetLowerBound(0);
                  i <= values.GetUpperBound(0); i++)
            {
                for (int j = values.GetLowerBound(1);
                          j <= values.GetUpperBound(1); j++)
                {
                    //This takes care of white space
                    if (values.GetValue(i, j) == null)
                    {
                        newArray[index] = "";
                    }
                    //this puts in the sting representaion of what is in cell i, j
                    // can be 1 of three types: a normal string, an integer, or a double. 
                    else
                    {
                        newArray[index] = (string)values.GetValue(i, j).ToString();
                    }
                    //Move to the next position in the new array.
                    index++;
                }
            }
            return newArray;
        }
    }
}
