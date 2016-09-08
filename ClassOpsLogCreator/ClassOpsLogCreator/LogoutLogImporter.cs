using System;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// 
/// Description of class: This class is a helper class that 
/// will assist with creating the Crestron Logouts of the 
/// input excel file. This class also assists with sorting and 
/// presenting a nice arrangement of all last times for the 
/// events held around School. 
///
/// Class Version: 0.1.0.0 - BETA - 7152016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>
namespace ClassOpsLogCreator
{
    class LogoutLogImporter
    {
        /// <summary>
        /// Private Attributes
        /// </summary>
        private LogCreator form1 = null;

        private static Excel.Application roomSched = null;
        private static Excel.Workbook roomWorkBook = null;
        private static Excel.Worksheet roomSheet1 = null;

        private string[] arrayClassRooms = null;
        private string[] arrayTimes = null;
        private string[] arrayLastTimes = null;
        private string[] arrayEvent = null;

        private string[,] masterArray = null;
        private int masterArrayCounter = 0;

        private string startTime = null;
        private string endTime = null;

        /// <summary>
        /// Constructor that will create the arrays for the main UI to use
        /// </summary>
        /// <param name="Form1"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
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
                roomSheet1 = (Excel.Worksheet)roomWorkBook.Sheets[1];

            }
            catch (Exception)
            {
                //File not found...

                Quit();
                throw new System.FieldAccessException("File not found!");
            }

            //Get the range we are working within. (A1, A.LastRow)
            Excel.Range last = roomSheet1.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range classRange = roomSheet1.get_Range("A5", "A" + last.Row);
            Excel.Range timeRange = roomSheet1.get_Range("C5", "C" + last.Row);
            Excel.Range eventRange = roomSheet1.get_Range("D5", "D" + last.Row);

            //Delete entire row if c.value2 is null but d.value2 is not null. 
            for(int i = 1; i <= last.Row; i++)
            {
                Excel.Range timeItem = (Excel.Range)timeRange.Item[i];
                Excel.Range eventItem = (Excel.Range)eventRange.Item[i];
                if(eventItem.Value2 != null && (timeItem.Value2 == null || timeItem.Value2.Equals("")))
                {
                    timeItem.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    
                }
            }
            //Save the changes
            roomWorkBook.Save();

            //Lets export the whole range of raw data into an array. (Cell.Value2 is a fast and accurate operation to use)
            System.Array classArray = (System.Array)classRange.Cells.Value2;
            System.Array timeArray = (System.Array)timeRange.Cells.Value2;
            System.Array eventArray = (System.Array)eventRange.Cells.Value2;

            //Now we extract all the raw data to strings 
            arrayClassRooms = this.ConvertToStringArray(classArray, 0);
            arrayTimes = this.ConvertToStringArray(timeArray, 1);
            arrayEvent = this.ConvertToStringArray(eventArray, 1);
            arrayLastTimes = this.extract_last_time(arrayTimes, arrayEvent);

            //Remove the sql entry in the array if they exist
            if(arrayClassRooms[arrayClassRooms.GetUpperBound(0)].Contains("SQL"))
            {
                arrayClassRooms = arrayClassRooms.Take(arrayClassRooms.Count() - 1).ToArray();
            }

            //Check if the arrayLastTimes and the class array are the same length, if so then we get correct results.
            if (arrayLastTimes.GetUpperBound(0) != arrayClassRooms.GetUpperBound(0))
            {
                Quit();
                throw new System.IO.FileLoadException("Error: While parsing clo.xlsx we ran into a problem:" + Environment.NewLine +
                                "The file is not formatted correctly, please ensure all rows have a corresponding classroom");
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

        /// <summary>
        /// /**A Helper converter that will take our "values" and convert them into a string array. 
        /// String parsing IS requires for now until we make it smart.
        /// A string array is returned by with white spaces.
        /// flag = 0 means no null/white space, 1 means leave white space and null and we work with doubles
        /// </summary>
        /// <param name="values"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
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
                    //this puts in the sting representation of what is in cell i, j
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

        /// <summary>
        /// This method will create a 2D array of all the classes and time to be logged out 
        /// Between the times of 4PM and 10PM
        /// </summary>
        /// <param name="classArray"></param>
        /// <param name="timeArray"></param>
        /// <returns></returns>
        private string[,] convertToString2DArray(string[] classArray, string[] timeArray)
        {
            //Check if the clo is empty or not
            //Throw an exception to cause the system to halt
            if (classArray.GetUpperBound(0) < 1)
            {
                Quit();
                throw new System.IO.FileLoadException("Error: It seems the CLO is empty!");
            }

            masterArray = new string[classArray.GetUpperBound(0), 4];
            ClassInfo classList = new ClassInfo();
            DateTime startingTime = Convert.ToDateTime(this.startTime.ToString());
            DateTime endingTime = Convert.ToDateTime(this.endTime.ToString());

            //Add all the elements of the array's into one array. 
            int index = 0;
            for (int i = 0; i < classArray.GetUpperBound(0); i++)
            {
                //and remove all classes with no crestron. 
                DateTime check = Convert.ToDateTime(timeArray[i]);
                if ((check.TimeOfDay >= startingTime.TimeOfDay) && (check.TimeOfDay < endingTime.TimeOfDay)
                    && (classList.hasCrestron(classArray[i])))
                {
                    //Set the time
                    masterArray[index, 0] = Convert.ToDateTime(timeArray[i]).ToString("HHmm");

                    //Split the building from the room 
                    string[] token = classArray[i].Split(' ');
                    //Remove any blanks in between
                    if (token.Length > 2)
                    {
                        token = token.Where(n => n != "").ToArray();
                    }

                    //Add it to the array
                    masterArray[index, 1] = token[0];

                    //Change IKB to OSG 
                    if (token[0].Equals("IKB"))
                    {
                        masterArray[index, 1] = "OSG";
                        masterArray[index, 2] = token[1];
                    }
                    //Add a logout comment for MC157A
                    else if (token[0].Equals("MC") && token[1].Equals("157A"))
                    {
                        masterArray[index, 2] = token[1];
                        masterArray[index, 3] = "Door code 11012*";
                    }
                    else
                    {
                        masterArray[index, 2] = token[1];
                    }
                    //Adding notes
                    //Does the class have a neck mic?
                    if (classList.hasLapelMic(classArray[i]) && masterArray[index, 3] == null)
                    {
                        masterArray[index, 3] = "Ensure neck mic goes back to equipment drawer.";
                    }
                    else if(masterArray[index, 3] == null)
                    {
                        masterArray[index, 3] = "";
                    }
                    index++;
                }
            }
            masterArrayCounter = index;
            return masterArray;
        }

        /// <summary>
        /// A  helper method to get the last time in our time array
        /// </summary>
        /// <param name="timearray"></param>
        /// <param name="eventarray"></param>
        /// <returns></returns>
        private string[] extract_last_time(string[] timearray, string[] eventarray)
        {
            string[] newArray = new string[eventarray.GetUpperBound(0) + 1];
            int index = 0;
            //Iterate through the list and find the ending time of the las class in said room.
            //Getlowerbound and GetUpperBound is safer then .Length
            for (int i = eventarray.GetLowerBound(0); i <= eventarray.GetUpperBound(0); i++)
            {
                //if the next cell is empty we found the last time, add it to the array
                if (eventarray[i].ToString().Length != 0)
                {
                    //Check if it is the last element in the array
                    if (i == eventarray.GetUpperBound(0))
                    {
                        //add the last time in a formatted way to the list
                        newArray[index] = DateTime.FromOADate(double.Parse(timearray[i])).ToString("HH:mm");
                        index++;
                    }
                    //else we check if the next element is empty or null
                    else if ((eventarray[i + 1].ToString().Length == 0) || (eventarray[i + 1] == null))
                    {
                        //check the time array for the corresponding times 
                        //If it is not null or empty we know this is the last time.
                        if ((timearray[i] != null) && (timearray[i].ToString().Length != 0))
                        {
                            newArray[index] = DateTime.FromOADate(double.Parse(timearray[i])).ToString("HH:mm");
                            index++;
                        }
                        //else we are going to check at most 2 elements up to see if its not null or not empty
                        else
                        {
                            for (int j = 1; j < 2; j++)
                            {
                                if ((timearray[i - j] != null) && (timearray[i - j].ToString().Length != 0))
                                {
                                    newArray[index] = DateTime.FromOADate(double.Parse(timearray[i - 1])).ToString("HH:mm");
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
            //Return an array with no null characters. 
            return newArray = newArray.Where(n => n != null).ToArray();
        }

        /// <summary>
        /// Close all open instances of Excel and Garbage collects. 
        /// </summary>
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
