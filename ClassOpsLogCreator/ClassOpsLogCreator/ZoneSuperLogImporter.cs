using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace ClassOpsLogCreator
{
    public class ZoneSuperLogImporter
    {
        //Private attributes
        private LogCreator Form1 = null;

        private static Excel.Application JeannineLog = null;
        private static Excel.Workbook JeannineWorkBook = null;
        private static Excel.Worksheet JeannineSheet1 = null;

        private string lastDate = null;
        private string[,] JeannineLogArray = null;
        private string[,] RaulLogArray = null;
        private string[,] DerekLogArray = null;

        /// <summary>
        /// This Class will import all the zone supervisor logs and assist with 
        /// modification and find operations. 
        /// </summary>
        public ZoneSuperLogImporter(LogCreator form1)
        {
            this.Form1 = form1;

            JeannineLog = new Excel.Application();
            JeannineLog.Visible = false;

            try
            {
                //This should look for the file
                //JeannineWorkBook = JeannineLog.Workbooks.Open(Form1.JEANNINE_LOG);
                JeannineWorkBook = JeannineLog.Workbooks.Open(@"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\Jeannine's log.xlsx");

                //Work in worksheet number 1
                JeannineSheet1 = JeannineWorkBook.Sheets[2];
            }
            catch (Exception ex)
            {
                //File not found...
                MessageBox.Show("Error: FILE NOT FOUND " + ex.ToString());
                Quit();
                return;
            }

            // Get the last date and create the 2D array for each log.
            lastDate = this.dateFromLogs(JeannineSheet1);
            JeannineLogArray = this.ConvertToStringArray2D(JeannineSheet1);
            DerekLogArray = this.ConvertToStringArray2D(null);
            RaulLogArray = this.ConvertToStringArray2D(null);
        }

        /// <summary>
        ///  All Public Accesor methods
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


        /// <summary>
        /// All Private Modifiers are bellow
        /// </summary>

        /* This method will extract the date from the logs.
         */
        private string dateFromLogs(Excel.Worksheet ExSheet)
        {
            // get all the dates
            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range = ExSheet.get_Range("C" + (last.Row - 10), "C" + last.Row);

            //Export to array 
            System.Array array = (System.Array)range.Cells.Value2;

            //Return the last time in the format of Month/Day/Year
            return DateTime.FromOADate(double.Parse((string)array.GetValue(array.GetUpperBound(0), 1).ToString())).ToString("M/dd/yy");
        }

        /* This method retun how many entries we need to copy over
         * We start from the bottom of the excel sheet and look for the first null, or when date != the date in cell
         */
        private int numberOfRows(Excel.Worksheet ExSheet, string date)
        {
            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range = ExSheet.get_Range("C" + (last.Row - 100), "C" + last.Row);

            //Export to array 
            System.Array array = (System.Array)range.Cells.Value2;

            int numberOfRows = 0;
            for (int i = array.GetUpperBound(0);
                 i > array.GetLowerBound(0); i--)
            {
                //This finds all number of columsn that happen today. 
                if ((array.GetValue(i,1) != null) && (array.GetValue(i,1) is double ) 
                    && (DateTime.FromOADate(double.Parse((string)array.GetValue(i, 1).ToString())).ToString("M/dd/yy").Equals(date)))
                {
                    numberOfRows++;
                }   
            }
            return numberOfRows;
        }

        /* This method creates a 2d array of all the events for today for the master log
         */
        private string[,] ConvertToStringArray2D(Excel.Worksheet ExSheet)
        {
            //initialization of all the ranges that we are going to collect.
            int start = this.numberOfRows(ExSheet, this.getLastDate()) -1;
            Excel.Range last = ExSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range rangeB = ExSheet.get_Range("B" + (last.Row - start), "B" + last.Row);
            Excel.Range rangeC = ExSheet.get_Range("C" + (last.Row - start), "C" + last.Row);
            Excel.Range rangeD = ExSheet.get_Range("D" + (last.Row - start), "D" + last.Row);
            Excel.Range rangeE = ExSheet.get_Range("E" + (last.Row - start), "E" + last.Row);
            Excel.Range rangeF = ExSheet.get_Range("F" + (last.Row - start), "F" + last.Row);
            Excel.Range rangeG = ExSheet.get_Range("G" + (last.Row - start), "G" + last.Row);

            //Convert all the ranges to a 1d array.
            System.Array arrayB = (System.Array)rangeB.Cells.Value2;
            System.Array arrayC = (System.Array)rangeC.Cells.Value2;
            System.Array arrayD = (System.Array)rangeD.Cells.Value2;
            System.Array arrayE = (System.Array)rangeE.Cells.Value2;
            System.Array arrayF = (System.Array)rangeF.Cells.Value2;
            System.Array arrayG = (System.Array)rangeG.Cells.Value2;

            //Add all the values from the arrays to a 2d array of strings,
            string[,] values = new string[start + 1, 6];
            for (int i = 0; i <= start; i ++)
            {
                values[i, 0] = arrayB.GetValue(i + 1, 1).ToString();
                values[i, 1] = arrayC.GetValue(i + 1, 1).ToString();
                values[i, 2] = arrayD.GetValue(i + 1, 1).ToString();
                values[i, 3] = arrayE.GetValue(i + 1, 1).ToString();
                values[i, 4] = arrayF.GetValue(i + 1, 1).ToString();
                values[i, 5] = arrayG.GetValue(i + 1, 1).ToString();

            }
            return values;
        }
       
        /* Close all open instances of Excel and Garbage collects. 
         * 
         */
        private void Quit()
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
            GC.Collect();
        }
    }
}
