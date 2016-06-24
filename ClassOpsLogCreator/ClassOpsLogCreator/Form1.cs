using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
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

        private static Excel.Application logoutMaster = null;
        private static Excel.Workbook logoutMasterWorkBook = null;
        private static Excel.Worksheet logoutMasterWorkSheet = null;

        
        /** Constructor for the system. (Changes here should be confirmed with everyone first) */
        public LogCreator()
        {
            InitializeComponent();

            //fill the combo boxes
            for(int i = 1; i <= 12; i ++)
            {
                this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.startHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });

                this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //15 minute intervals
                for (int k = 15; k <= 45; k += 15)
                {
                    this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString()});
                    this.startHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });

                    this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    this.endHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                }
            }

            //add number of shifts
            for(int j = 1; j <= 6; j ++)
            {
                this.numberOfShiftsCombo1.Items.Add(j.ToString());
                this.numberOfShiftsCombo2.Items.Add(j.ToString());
            }

            //Fill the am/pm selector
            this.am_pmCombo1.Items.Add("AM");
            this.am_pmCombo1.Items.Add("PM");
            this.am_pmCombo2.Items.Add("AM");
            this.am_pmCombo2.Items.Add("PM");
            this.am_pmCombo3.Items.Add("AM");
            this.am_pmCombo3.Items.Add("PM");
            this.am_pmCombo4.Items.Add("AM");
            this.am_pmCombo4.Items.Add("PM");

            //set the default view for the combo
            this.startHour1.SelectedIndex = 0;
            this.startHour2.SelectedIndex = 0;
            this.endHour1.SelectedIndex = 0;
            this.endHour2.SelectedIndex = 0;
            this.numberOfShiftsCombo1.SelectedIndex = 0;
            this.numberOfShiftsCombo2.SelectedIndex = 0;
            this.am_pmCombo1.SelectedIndex = 0;
            this.am_pmCombo2.SelectedIndex = 0;
            this.am_pmCombo3.SelectedIndex = 0;
            this.am_pmCombo4.SelectedIndex = 0;

            //Make the combo box read only
            this.startHour1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.startHour2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.endHour1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.endHour2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo2.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        /** When the user clicks the "Create" Button this is what will happen
         */
        private void createBTN_Click(object sender, EventArgs e)
        {
            //Open the room excel file
            roomSched = new Excel.Application();
            roomSched.Visible = false;
            
            try
            {
                //This should look for the file one level up. (Temporary to keep everything local)
                //roomWorkBook = roomSched.Workbooks.Open(@"H:\CS\SHARE-PT\CLASSOPS\clo.xlsx");
                roomWorkBook = roomSched.Workbooks.Open(@"C:\Users\Jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\room schedule.xlsx");
                //Work in worksheet number 1
                roomSheet1 = roomWorkBook.Sheets[1];
            }
            catch (Exception ex)
            {
                //File not found...
                textBox1.Text = "error: FILE NOT FOUND" + ex.ToString();
                roomSched.Quit();
                return;
            }
            

            //Get the range we are working within. (A1, A.LastRow)
            Excel.Range last = roomSheet1.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range1 = roomSheet1.get_Range("A5", "A" + last.Row);
            Excel.Range range2 = roomSheet1.get_Range("C5", "C" + last.Row);

            //Lets export the whole range of raw data into an array. (Cell.Value2 is a fast and accurate operation to use)
            System.Array array = (System.Array)range1.Cells.Value2;
            System.Array array2 = (System.Array)range2.Cells.Value2;

            //Now we extract all the raw data to strings 
            string[] arrayClassRooms = this.ConvertToStringArray(array, 0);
            string[] arrayTimes = this.ConvertToStringArray(array2, 1);
            string[] arrayLastTimes = this.extract_last_time(arrayTimes);

            //Create the new Excel file where we will store all the new information
            logoutMaster = new Excel.Application();
            logoutMaster.Visible = false;
            logoutMasterWorkBook = logoutMaster.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            logoutMasterWorkSheet = (Excel.Worksheet)logoutMasterWorkBook.Worksheets[1];

            //write all the data to the excel file
            this.WriteLogOutArray(logoutMasterWorkSheet, arrayClassRooms, arrayLastTimes);

            //Saving and closing the new excel file
            logoutMasterWorkBook.SaveAs(Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory) + @"\Logout_Master.xlsx");

            //DEGUB CODE
            //textBox1.Text = DateTime.FromOADate(double.Parse(arrayTimes[0])).ToString("hh:mm:tt");
            textBox1.Text = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory) + @"\Logout_Master.xlsx";

            //close the excel application
            Quit();
        }


        /// <summary>
        ///  ALL HELPER METHODS GO HERE BELLOW HERE! 
        /// </summary>
       
        /**A Helper converter that will take our "values" and convert them into a string array. 
         * String parsing IS requires for now until we make it smart. 
         * 
         * A string array is returned by with white spaces.
         * flag = 0 means no null/white space, 1 means leave white space and null and we work with doubles
         **/
        private string[] ConvertToStringArray(System.Array values, int flag)
        {
            string[] newArray = new string[values.Length];
            int index = 0;

            for (int i = values.GetLowerBound(0);
                  i <= values.GetUpperBound(0); i++)
            {
                for (int j = values.GetLowerBound(1);
                          j <= values.GetUpperBound(1); j++)
                {
                    //This takes care of white space
                    if ((values.GetValue(i, j) == null) || (values.GetValue(i, j).ToString().Trim().Length == 0))
                    {
                        //Add an empty string so we can parse for last time later
                         if(flag == 1)
                        {
                            newArray[index] = "";
                            index++;
                        }
                    }
                    //this puts in the sting representaion of what is in cell i, j
                    // can be 1 of three types: a normal string, an integer, or a double. 
                    else
                    {
                        newArray[index] = (string)values.GetValue(i, j).ToString();
                        //Move to the next position in the new array.
                        index++;
                    }
                }
            }
            //Return an array with no null characters
            return newArray = newArray.Where(n => n != null).ToArray();
        }


        /* A  helper method to get the last time in our time array
         */
        private string[] extract_last_time(string[] array)
        {
            string[] newArray = new string[array.Length];
            int index = 0;
            //Iterate throught the list and find the ending time of the las class in said room.
            //Getlowerbound and GetUpperBound is safer then .Length
            for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0) - 2; i++)
            {
                //if the next cell is empty we found the last time, add it to the array
                if ((array[i].ToString().Length != 0) && (array[i + 1].ToString().Length == 0) || (array[i + 1] == null))
                {
                    //add the last time in a formatted wayS to the list
                    newArray[index] = DateTime.FromOADate(double.Parse(array[i])).ToString("hh:mm tt");
                    index++;
                }
            } 
            //Return an array with no null characters. 
            return newArray = newArray.Where(n => n != null).ToArray();
        }

        
        /*This method will write our arrays to the excel file.
         * 
         * This method generates the Excel output via the arrays
         */
        private void WriteLogOutArray(Excel.Worksheet worksheet, string[] arrayClass, string[] arrayTime)
        {
            string[,] values = new string[arrayClass.Length, 2];
            DateTime fourPM = DateTime.FromOADate(0.666);
            DateTime tenPM = DateTime.FromOADate(0.920);
            //Add all the elements of the array's into one array. 
            int index = 0;
            for (int i = 0; i < arrayClass.Length; i++)
            {
                //Add only the times between 4pm and 10pm
                DateTime check = Convert.ToDateTime(arrayTime[i]);
                if(check.TimeOfDay >= fourPM.TimeOfDay && check.TimeOfDay <= tenPM.TimeOfDay)
                {
                    values[index, 0] = arrayTime[i];
                    values[index, 1] = arrayClass[i];
                    index++;
                }  
            }

            //Setting up the cells to put the information into
            Excel.Range taskType_range = worksheet.get_Range("A2", "A" + (index + 1)); 
            Excel.Range value_range = worksheet.get_Range("B2", "C" + (index + 1));

            //Formatting for easy to read for "Crestron logout"
            taskType_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            taskType_range.ColumnWidth = 20;
            taskType_range.Value2 = "Crestron Logout";

            //Format for easy read Time and Building and room
            value_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            value_range.ColumnWidth = 17;
            value_range.Value2 = values;

            //Sorting it by column 2
            dynamic allDataRange = worksheet.UsedRange;
            allDataRange.Sort(allDataRange.Columns[2], Excel.XlSortOrder.xlAscending);
        }


        /* Close all open instances of Excel and Garbage collects. 
         * 
         */ 
        private void Quit()
        {
            roomWorkBook.Close(0);
            roomSched.Quit();


            logoutMasterWorkBook.Close(0);
            logoutMaster.Quit();


            roomSched = null;
            roomWorkBook = null;
            roomSheet1 = null;

            logoutMaster = null;
            logoutMasterWorkBook = null;
            logoutMasterWorkSheet = null;

            GC.Collect();
        }
    }
}
