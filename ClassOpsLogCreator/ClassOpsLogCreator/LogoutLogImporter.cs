using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ClassOpsLogCreator
{
    class LogoutLogImporter
    {
        // Private attributes
        private LogCreator form1 = null;
 
        private static Excel.Application roomSched = null;
        private static Excel.Workbook roomWorkBook = null;
        private static Excel.Worksheet roomSheet1 = null;

        private string[] arrayClassRooms = null;
        private string[] arrayTimes = null;
        private string[] arrayLastTimes = null;

        private string[,] masterArray = null;
        private int masterArrayCounter = 0;

        private string startTime = null;
        private string endTime = null;

        /** Constructor that will create the arrays for the main UI to use
         */
        public LogoutLogImporter(LogCreator Form1, string StartTime, string EndTime)
        {
            this.form1 = Form1;
            this.startTime = StartTime;
            this.endTime = EndTime;

            //Open the room excel file
            roomSched = new Excel.Application();
            roomSched.Visible = false;

            try
            {
                //This should look for the file
                roomWorkBook = roomSched.Workbooks.Open(form1.ROOM_SCHED);
                //Work in worksheet number 1
                roomSheet1 = roomWorkBook.Sheets[1];

            }
            catch (Exception)
            {
                //File not found...
                
                Quit();
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
            arrayClassRooms = this.ConvertToStringArray(array, 0);
            arrayTimes = this.ConvertToStringArray(array2, 1);
            arrayLastTimes = this.extract_last_time(arrayTimes);

            //Check if the arrayLastTimes and the classarray are the same length, if so then we get correct results.
            if(arrayLastTimes.GetUpperBound(0) > arrayClassRooms.GetUpperBound(0))
            {
                MessageBox.Show("Error: While parsing clo.xlsx we ran into a problem:" + Environment.NewLine +
                                "The file is not formatted correctly, please ensure all rows have a corresponding classroom",
                                 "File format error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button1);
            }
            else
            {
                masterArray = this.convertToString2DArray(arrayClassRooms, arrayLastTimes);
            }
            
            //Close all open processes
            Quit();
        }

        /// <summary>
        /// Public accessors of the 2 arrays
        /// </summary>
        /// <returns></returns>
        /// Getters
        public string[] getClassRooms()
        {
            return this.arrayClassRooms;
        }
        public string[] getLastTImes()
        {
            return this.arrayLastTimes;
        }
        public string[,] getLogOutArray()
        {
            return this.masterArray;
        }
        public int getLogOutArrayCount()
        {
            return this.masterArrayCounter;
        }
        
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
                        if (flag == 1)
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

        /** This method will create a 2D array of all the classes and time to be logged out 
         *  Between the times of 4PM and 10PM
         */
        private string[,] convertToString2DArray(string[] classArray, string[] timeArray)
        {
            masterArray = new string[classArray.GetUpperBound(0), 4];
            ClassInfo classList = new ClassInfo();
            DateTime startingTime = Convert.ToDateTime(this.startTime.ToString());  //4pm in DateTime
            DateTime endingTime = Convert.ToDateTime(this.endTime.ToString());   //10pm in DateTime

            //Add all the elements of the array's into one array. 
            int index = 0;
            for (int i = 0; i < classArray.GetUpperBound(0); i++)
            {
                //and remove all classes with no crestron. 
                DateTime check = Convert.ToDateTime(timeArray[i]);
                if ((check.TimeOfDay >= startingTime.TimeOfDay) && (check.TimeOfDay <= endingTime.TimeOfDay)
                    && (classList.hasCrestron(classArray[i])))
                {
                    //Set the time
                    masterArray[index, 0] = Convert.ToDateTime(timeArray[i]).ToString("HHmm");

                    //Split the building from the room 
                    string[] token = classArray[i].Split(' ');
                    //Add it to the array
                    masterArray[index, 1] = token[0];
                    //Acount for Ross
                    if (token[0].Equals("R"))
                    {
                        masterArray[index, 2] = token[2];
                    }
                    //Change IKB to OSG 
                    else if(token[0].Equals("IKB"))
                    {
                        masterArray[index, 1] = "OSG";
                        masterArray[index, 2] = token[1];
                    }
                    else
                    {
                        masterArray[index, 2] = token[1];
                    }
                    //Adding notes
                    //Does the class have a neck mic?
                    if(classList.hasLapelMic(classArray[i]))
                    {
                        masterArray[index, 3] = "Ensure neck mic goes back to equipment drawer.";
                    }
                    else
                    {
                        masterArray[index, 3] = "";
                    }
                    index++;
                }
            }
            masterArrayCounter = index;
            return masterArray;
        }

        /** A  helper method to get the last time in our time array
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
                    newArray[index] = DateTime.FromOADate(double.Parse(array[i])).ToString("HH:mm");
                    index++;
                }
            }
            //Return an array with no null characters. 
            return newArray = newArray.Where(n => n != null).ToArray();
        }

        /** Close all open instances of Excel and Garbage collects. 
         * 
         */
        public void Quit()
        {
            if (roomWorkBook != null)
            {
                roomWorkBook.Close(0);
                roomSched.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(roomSched);

                roomSched = null;
                roomWorkBook = null;
                roomSheet1 = null;
            }
            GC.Collect();
        }
    }
}
