using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpsLogCreator
{
    public class StatsGen
    {
        //Holds the main form pointer
        private LogCreator mainForm;

        //Excel references
        Excel.Application masterExcel = null;
        Excel.Workbook masterExcelWorkBook = null;
        Excel.Worksheet masterExcelWorkSheet = null;
        Excel.Worksheet masterExcelDataBaseSheet = null;


        //Start and end time that are picked
        DateTime startDate;
        DateTime endDate;

        public StatsGen(LogCreator MainForm, DateTime StartDate, DateTime EndDate)
        {
            this.mainForm = MainForm;
            this.startDate = StartDate;
            this.endDate = EndDate;

            this.generateStats();
        }


        /// <summary>
        /// A helper/worker method that will generate statistics
        /// </summary>
        private void generateStats()
        {
            //Attempt to open the excel work book if not possible quite and return
            masterExcel = new Excel.Application();
            try
            {
                masterExcelWorkBook = masterExcel.Workbooks.Open(mainForm.EXISTING_MASTER_LOG);
                masterExcelWorkSheet = (Excel.Worksheet)masterExcelWorkBook.Worksheets[1];
                masterExcelDataBaseSheet = (Excel.Worksheet)masterExcelWorkBook.Sheets[2];
            }
            catch
            {
                Quit();
                return;
            }

            //Get our list of events and buildings from the database
            List<string> eventList = this.getEventList(masterExcelDataBaseSheet);
            List<string> buildingList = this.getBuildingList(masterExcelDataBaseSheet);

            //Keep track of how many events occur.
            Dictionary<string, int> eventCounter = new Dictionary<string, int>();
            Dictionary<string, int> buildingCounter = new Dictionary<string, int>();

            //Initialize the dictionary's
            foreach (string s in eventList)
            {
                eventCounter.Add(s, 0);
            }
            foreach(string s in buildingList)
            {
                buildingCounter.Add(s, 0);
            }

            //get the last filled cell
            Excel.Range last = masterExcelWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            //Get the first and last row of the time period we are looking for.
            int[,] rowNumbers = startAndEndingIndex(masterExcelWorkSheet, startDate, endDate);

            masterExcel.Visible = true;

            //Close all excel instances
            Quit();
        }

        /// <summary>
        /// Gets a list of events in the database
        /// </summary>
        /// <param name="ExSheet"></param>
        /// <returns></returns>
        private List<string> getEventList(Excel.Worksheet ExSheet)
        {
            List<string> eventList = new List<string>();

            //Get the events from the DataBase sheet 
            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            int lastRow = ExSheet.UsedRange.Rows.Count;
            Excel.Range eventRange = ExSheet.get_Range("B2", "B" + (lastRow));

            //Convert to an array
            System.Array eventArray = (System.Array)eventRange.Cells.Value2;
            foreach(object o in eventArray)
            {
                if(o != null)
                {
                    eventList.Add(o.ToString());
                }
            }

            return eventList;
        }

        /// <summary>
        /// Gets a list of all the buildings in our database
        /// </summary>
        /// <param name="ExSheet"></param>
        /// <returns></returns>
        private List<string> getBuildingList(Excel.Worksheet ExSheet)
        {
            List<string> eventList = new List<string>();

            //Get the events from the DataBase sheet 
            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            int lastRow = ExSheet.UsedRange.Rows.Count;
            Excel.Range eventRange = ExSheet.get_Range("C2", "C" + (lastRow));

            //Convert to an array
            System.Array eventArray = (System.Array)eventRange.Cells.Value2;
            foreach (object o in eventArray)
            {
                if (o != null)
                {
                    eventList.Add(o.ToString());
                }
            }

            return eventList;
        }

        private int[,] startAndEndingIndex(Excel.Worksheet ExSheet, DateTime startDate, DateTime endDate)
        {

            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

            int lastRow = ExSheet.UsedRange.Rows.Count;
            //Obtain all the dates
            Excel.Range range = ExSheet.get_Range("C1", "C" + lastRow);

            int[,] indexArray = new int[1, 2];

            if (range.Rows.Count != 1)
            {
                //Export to array 
                System.Array array = (System.Array)range.Cells.Value2;
                Tuple<DateTime, int>[] stringArray = convertAllToString(array);

                int firstIndex = this.first(stringArray, 0, stringArray.Length - 1, startDate);
                if (firstIndex == -1)
                {
                    firstIndex = 0;
                }
                int lastIndex = this.last(stringArray, firstIndex, stringArray.Length - 1, endDate);

                indexArray[0, 0] = stringArray[firstIndex].Item2;
                indexArray[0, 1] = stringArray[lastIndex].Item2;

            }

            last = null;
            range = null;

            return indexArray;
        }

        /// <summary>
        /// Converts the input array into a string of date time strings
        /// 
        /// Making it easier to parse but runs in O(n^2) time.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private Tuple<DateTime, int>[] convertAllToString(Array array)
        {
            Tuple<DateTime, int>[] resualt = new Tuple<DateTime, int>[array.Length + 1];

            int indexCounter = 0;
            for (int i = 1; i <= array.Length; i++, indexCounter++)
            {
                if (array.GetValue(i, 1) == null)
                {
                    indexCounter--;
                }
                else if (array.GetValue(i, 1) is double)
                {
                    DateTime dateToAdd = DateTime.FromOADate((double)array.GetValue(i, 1));
                    resualt[indexCounter] = Tuple.Create(dateToAdd, i);
                }
                else
                {
                    DateTime temp;
                    if (DateTime.TryParse(((string)array.GetValue(i, 1)), out temp))
                    {
                        resualt[indexCounter] = Tuple.Create(Convert.ToDateTime(array.GetValue(i, 1)), i);
                    }
                    else
                    {
                        indexCounter--;
                    }
                }
            }
            return resualt = resualt.Where(x => x != null).ToArray();
        }

        /// <summary>
        /// Finds the first occurrence of "startDate" in our given array
        /// 
        /// Return -1 if it does not exist. 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private int first(Tuple<DateTime, int>[] array, int low, int high, DateTime startDateL)
        {
            if (high >= low)
            {
                int mid = (low + high) / 2;
                DateTime checkDateMIDMINUS1 = Convert.ToDateTime(array[mid - 1].Item1);
                DateTime checkDateMID = Convert.ToDateTime(array[mid].Item1);
                if ((mid == 0 || startDateL > checkDateMIDMINUS1) && checkDateMID == startDateL)
                {
                    return mid;
                }
                else if (startDateL > checkDateMID)
                {
                    return first(array, (mid + 1), high, startDateL);
                }
                else
                {
                    return first(array, low, (mid - 1), startDateL);
                }
            }
            return -1;
        }

        /// <summary>
        /// Finds the last occurrence of "startDate" in our given array
        /// 
        /// Return -1 if it does not exist. 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="endDateL"></param>
        /// <returns></returns>
        private int last(Tuple<DateTime, int>[] array, int low, int high, DateTime endDateL)
        {
            if (high >= low)
            {
                int mid = (low + high) / 2;
                DateTime checkDateMIDPLUS1 = Convert.ToDateTime(array[mid + 1].Item1);
                DateTime checkDateMID = Convert.ToDateTime(array[mid].Item1);
                if ((mid == array.Length - 1 || endDateL < checkDateMIDPLUS1) && checkDateMID == endDateL)
                {
                    return mid;
                }
                else if (endDateL < checkDateMID)
                {
                    return last(array, low, (mid - 1), endDateL);

                }
                else
                {
                    return last(array, (mid + 1), high, endDateL);
                }
            }
            return -1;
        }

        //Close all instances of excel.
        private void Quit()
        {
            if (masterExcelWorkBook != null)
            {
                masterExcelWorkBook.Close(0);
                masterExcel.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(masterExcel);
                masterExcel = null;
                masterExcelWorkBook = null;
                masterExcelWorkSheet = null;
                masterExcelDataBaseSheet = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
