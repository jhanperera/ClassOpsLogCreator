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

            lastDate = this.dateFromLogs(JeannineSheet1);
        }

        /// <summary>
        ///  All Public Accesor methods
        /// </summary>
        /// <returns></returns>
        public string getLastDate()
        {
            return this.lastDate;
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
                //This takes care of white space
                if (DateTime.FromOADate(double.Parse((string)array.GetValue(array.GetUpperBound(0), 1).ToString())).ToString("M/dd/yy")
                     == date)
                {
                    numberOfRows++;
                }
                
            }
            return numberOfRows;
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
