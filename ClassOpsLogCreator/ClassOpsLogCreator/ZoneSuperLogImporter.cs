using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// Description of class: This class will take in 3 input logs 
/// and sort the most important components. This will output only 
/// demos/setups/pickups and any other special tasks.
///
/// Class Version: 0.1.0.2 - BETA - 7202016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>
namespace ClassOpsLogCreator
{
    public class ZoneSuperLogImporter
    {
        /// <summary>
        /// Private attributes
        /// </summary>
        private LogCreator Form1 = null;

        private static Excel.Application JeannineLog = null;
        private static Excel.Workbook JeannineWorkBook = null;
        private static Excel.Worksheet JeannineSheet1 = null;

        private static Excel.Application RaulLog = null;
        private static Excel.Workbook RaulWorkBook = null;
        private static Excel.Worksheet RaulSheet1 = null;

        private static Excel.Application DerekLog = null;
        private static Excel.Workbook DerekWorkBook = null;
        private static Excel.Worksheet DerekSheet1 = null;

        private static Excel.Worksheet databaseSheet = null;

        private string lastDate = null;
        private string[,] JeannineLogArray = null;
        private string[,] RaulLogArray = null;
        private string[,] DerekLogArray = null;
        List<string> employeeNameList = null;

        private string startTime = null;
        private string endTime = null;

        /// <summary>
        /// This Class will import all the zone supervisor logs and assist with 
        /// modification and find operations. 
        /// </summary>
        public ZoneSuperLogImporter(LogCreator form1, string StartTime, string EndTime)
        {
            //Assigning the variables
            this.Form1 = form1;
            this.startTime = StartTime;
            this.endTime = EndTime;

            JeannineLog = new Excel.Application();
            RaulLog = new Excel.Application();
            DerekLog = new Excel.Application();
            JeannineLog.Visible = false;
            RaulLog.Visible = false;
            DerekLog.Visible = false;

            try
            {
                //This should look for the file
                JeannineWorkBook = JeannineLog.Workbooks.Open(Form1.JEANNINE_LOG);
                RaulWorkBook = RaulLog.Workbooks.Open(form1.RAUL_LOG);
                DerekWorkBook = DerekLog.Workbooks.Open(Form1.DEREK_LOG);

                //Work in worksheet number 1
                JeannineSheet1 = (Excel.Worksheet)JeannineWorkBook.Sheets[1];
                RaulSheet1 = (Excel.Worksheet)RaulWorkBook.Sheets[1];
                DerekSheet1 = (Excel.Worksheet)DerekWorkBook.Sheets[1];

                //Get the database for the employee names
                databaseSheet = (Excel.Worksheet)JeannineWorkBook.Sheets[2];
            }
            catch (Exception)
            {
                //File not found...
                Quit();
                return;
            }

            // Get the last date and create the 2D array for each log.
            lastDate = this.dateFromLogs(JeannineSheet1);
            JeannineLogArray = this.ConvertToStringArray2D(JeannineSheet1);
            DerekLogArray = this.ConvertToStringArray2D(DerekSheet1);
            RaulLogArray = this.ConvertToStringArray2D(RaulSheet1);
            employeeNameList = this.getEmployeeNamesList();

            this.Quit();
        }

        /// <summary>
        ///  All Public Accessors methods
        /// </summary>
        /// <returns></returns>
        //Return the Last Date AKA todays date
        public string getLastDate()
        {
            return this.lastDate;
        }

        //Get the number of rows that are associated with today
        public int getNumberofRows()
        {
            return this.numberOfRows(JeannineSheet1, this.lastDate);
        }

        //Get Jeannine 2dArray
        public string[,] getJeannineLog()
        {
            return this.JeannineLogArray;
        }

        //Get Raul 2dArray
        public string[,] getRaulLog()
        {
            return this.RaulLogArray;
        }

        //Get Derek 2dArray
        public string[,] getDerekLog()
        {
            return this.DerekLogArray;
        }

        public List<string> getEmployeeNames()
        {
            return this.employeeNameList;
        }

        private List<string> getEmployeeNamesList()
        {
            List<string> values = new List<string>();
            //Extract the name range
            Excel.Range last = databaseSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            int lastRow = databaseSheet.UsedRange.Rows.Count;
            Excel.Range nameRange = databaseSheet.get_Range("A2", "A" + (lastRow));
            //Convert to an array
            System.Array array = (System.Array)nameRange.Cells.Value2;
            
            foreach(string name in array)
            {
                if(name != null)
                {
                    values.Add(name.ToLower());
                }
            }
            return values;
        }

        /// <summary>
        /// All Private Modifiers are bellow
        /// 
        /// This method will return the date from us. (Does not reply on logs anymore)
        /// </summary>
        private string dateFromLogs(Excel.Worksheet ExSheet)
        {
            string dateToday = DateTime.Today.ToString("M/dd/yy");
            return dateToday;
        }

        /// <summary>
        /// This method return how many entries we need to copy over
        /// We start from the bottom of the excel sheet and look for the first null, or when date != the date in cell
        /// </summary>
        /// <param name="ExSheet"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private int numberOfRows(Excel.Worksheet ExSheet, string date)
        {

            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            int lastRow = ExSheet.UsedRange.Rows.Count;
            Excel.Range range = ExSheet.get_Range("B2", "B" + lastRow);

            int numberOfRows = 0;

            if (range.Rows.Count != 1)
            {
                //Export to array 
                System.Array array = (System.Array)range.Cells.Value2;

                for (int i = array.GetUpperBound(0);
                     i >= array.GetLowerBound(0); i--)
                {
                    //This finds all number of columns that happen today. 
                    if ((array.GetValue(i, 1) != null) && (array.GetValue(i, 1) is double)
                        && (DateTime.FromOADate(double.Parse((string)array.GetValue(i, 1).ToString())).ToString("M/dd/yy").Equals(date)))
                    {
                        numberOfRows++;
                    }
                }
            }
            else
            {
                //We just have one element in the array so we check if its in the time period.
                if ((range.Value2 != null) && (range.Value2 is double)
                        && (DateTime.FromOADate(double.Parse((string)range.Value2.ToString())).ToString("M/dd/yy").Equals(date)))
                {
                    numberOfRows++;
                }
            }
            return numberOfRows;
        }

        /// <summary>
        ///  This method creates a 2d array of all the events for today for the master log
        /// </summary>
        /// <param name="ExSheet"></param>
        /// <returns></returns>
        private string[,] ConvertToStringArray2D(Excel.Worksheet ExSheet)
        {
            DateTime startingTime = Convert.ToDateTime(this.startTime.ToString());
            DateTime endingTime = Convert.ToDateTime(this.endTime.ToString());

            //initialization of all the ranges that we are going to collect.
            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            int start = last.Row - this.numberOfRows(ExSheet, this.getLastDate());
            Excel.Range rangeA = ExSheet.get_Range("A" + start, "A" + last.Row);
            Excel.Range rangeB = ExSheet.get_Range("B" + start, "B" + last.Row);
            Excel.Range rangeC = ExSheet.get_Range("C" + start, "C" + last.Row);
            Excel.Range rangeD = ExSheet.get_Range("D" + start, "D" + last.Row);
            Excel.Range rangeE = ExSheet.get_Range("E" + start, "E" + last.Row);
            Excel.Range rangeF = ExSheet.get_Range("F" + start, "F" + last.Row);

            string[,] values = new string[(last.Row - start) + 1, 6];

            //If the range is not just one element we make arrays out of them
            //And get upper bound is greater than 0 (Avoid the array out of bounds)
            if (rangeA.Rows.Count != 1 && values.GetUpperBound(0) > 0)
            {
                //Convert all the ranges to a 1d array.
                System.Array arrayA = (System.Array)rangeA.Cells.Value2;
                System.Array arrayB = (System.Array)rangeB.Cells.Value2;
                System.Array arrayC = (System.Array)rangeC.Cells.Value2;
                System.Array arrayD = (System.Array)rangeD.Cells.Value2;
                System.Array arrayE = (System.Array)rangeE.Cells.Value2;
                System.Array arrayF = (System.Array)rangeF.Cells.Value2;

                //Add all the values from the arrays to a 2d array of strings,
                int index = 0;
                for (int i = 0; i < arrayA.GetUpperBound(0); i++)
                {
                    //Only going to get the events that are not Crestron Logouts
                    if ((arrayA.GetValue(i + 1, 1) != null) && (arrayC.GetValue(i + 1, 1) != null) && !(arrayA.GetValue(i + 1, 1).Equals("Crestron Logout")))
                    {
                        //Check if the event is between the selected times
                        DateTime check = DateTime.ParseExact(arrayC.GetValue(i + 1, 1).ToString(), "HHmm", null);
                        if ((check.TimeOfDay >= startingTime.TimeOfDay) && (check.TimeOfDay < endingTime.TimeOfDay))
                        {
                            //Tasks type
                            values[index, 0] = arrayA.GetValue(i + 1, 1).ToString();
                            //Date
                            values[index, 1] = DateTime.FromOADate(double.Parse((string)arrayB.GetValue(i + 1, 1).ToString())).ToString("M/dd/yy");
                            //Time
                            values[index, 2] = arrayC.GetValue(i + 1, 1).ToString();
                            //Building
                            values[index, 3] = arrayD.GetValue(i + 1, 1).ToString();
                            //Room
                            values[index, 4] = arrayE.GetValue(i + 1, 1).ToString();

                            //Comment, add a space if no comment is present
                            if (arrayF.GetValue(i + 1, 1) == null)
                            {
                                values[index, 5] = "";
                            }
                            else
                            {
                                values[index, 5] = arrayF.GetValue(i + 1, 1).ToString();
                            }
                            index++;
                        }
                    }
                }
            }
            //Else the array is one element so we add only that one element to output
            //get upper bound is greater than 0 (Avoid the array out of bounds)
            else if (values.GetUpperBound(0) > 0)
            {
                DateTime check = DateTime.ParseExact(rangeC.Cells.Value2.ToString(), "HHmm", null);
                if ((check.TimeOfDay >= startingTime.TimeOfDay) && (check.TimeOfDay <= endingTime.TimeOfDay))
                {
                    values[0, 0] = rangeA.Cells.Value2.ToString();
                    //Date
                    values[0, 1] = DateTime.FromOADate(double.Parse((string)rangeB.Cells.Value2.ToString())).ToString("M/dd/yy");
                    //Time
                    values[0, 2] = rangeC.Cells.Value2.ToString();
                    //Building
                    values[0, 3] = rangeD.Cells.Value2.ToString();
                    //Room
                    values[0, 4] = rangeE.Cells.Value2.ToString();

                    //Comment, add a space if no comment is present
                    if (rangeF.Cells.Value2 == null)
                    {
                        values[0, 5] = "";
                    }
                    else
                    {
                        values[0, 5] = rangeF.Cells.Value2.ToString();
                    }
                }
            }

            //Remove all null/empty rows
            string[,] temp = RemoveEmptyRows(values);
            return temp;
        }

        /// <summary>
        /// This method will remove all empty rows/null rows from the master logs
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string[,] RemoveEmptyRows(string[,] strs)
        {
            int length1 = strs.GetLength(0);
            int length2 = strs.GetLength(1);

            // First we count the non-empty rows
            int nonEmpty = 0;

            for (int i = 0; i < length1; i++)
            {
                for (int j = 0; j < length2; j++)
                {
                    if (strs[i, j] != null)
                    {
                        nonEmpty++;
                        break;
                    }
                }
            }
            // Then we create an array of the right size
            string[,] strs2 = new string[nonEmpty, length2];

            for (int i1 = 0, i2 = 0; i2 < nonEmpty; i1++)
            {
                for (int j = 0; j < length2; j++)
                {
                    if (strs[i1, j] != null)
                    {
                        // If the i1 row is not empty, we copy it
                        for (int k = 0; k < length2; k++)
                        {
                            strs2[i2, k] = strs[i1, k];
                        }

                        i2++;
                        break;
                    }
                }
            }
            return strs2;
        }

        /// <summary>
        /// /* Close all open instances of Excel and Garbage collects. 
        /// </summary>
        public void Quit()
        {
            if (JeannineWorkBook != null)
            {
                JeannineWorkBook.Close(0);
                JeannineLog.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(JeannineLog);

                JeannineLog = null;
                JeannineWorkBook = null;
                JeannineSheet1 = null;
            }
            if (RaulWorkBook != null)
            {
                RaulWorkBook.Close(0);
                RaulLog.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(RaulLog);

                RaulLog = null;
                RaulWorkBook = null;
                RaulSheet1 = null;
            }
            if (DerekWorkBook != null)
            {
                DerekWorkBook.Close(0);
                DerekLog.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(DerekLog);

                DerekLog = null;
                DerekWorkBook = null;
                DerekSheet1 = null;
            }
            GC.Collect();
        }
    }
}
