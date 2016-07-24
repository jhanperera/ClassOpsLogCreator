using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// 
/// Description of class: This is the main thread class
/// all the main event handelers and work is done here. 
/// All output is genereated from here and main features are 
/// all called here. 
///
/// Class Version: 0.1.0.5 - BETA - 7242016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>
namespace ClassOpsLogCreator
{
    public partial class LogCreator : Form
    {
        //Public readonly attribues
        public readonly string ROOM_SCHED = @"H:\CS\SHARE-PT\CLASSOPS\clo.xlsx";
        public readonly string JEANNINE_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Derek\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG = @"H:\CS\SHARE-PT\PW\masterlog.xlsx";
        public readonly string EXISTING_MASTER_LOG_COPY = @"H:\CS\SHARE-PT\CLASSOPS\masterlog.xlsx";

        //DEBUG CODE! 
        //ONLY UNCOMMENT FOR LOCAL USE ONLY! 
        /*public readonly string ROOM_SCHED = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\clo.xlsx";
        public readonly string JEANNINE_LOG = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\new.xlsx";*/

        private static Excel.Application logoutMaster = null;
        private static Excel.Workbook logoutMasterWorkBook = null;
        private static Excel.Worksheet logoutMasterWorkSheet = null;

        private static Excel.Application existingMaster = null;
        private static Excel.Workbook existingMasterWorkBook = null;
        private static Excel.Worksheet existingMasterWorkSheet = null;

        //Use a background worker to allow the GUI to still be functional and not hang.
        private static BackgroundWorker bw = null;

        private string startTimeFromCombo = null;
        private string endTimeFromCombo = null;
        private int numberOfShifts = 0;

        private Boolean workDone = false;

        /// <summary>
        /// Constructor for the system. (Changes here should be confirmed with everyone first)
        /// </summary>
        public LogCreator()
        {
            InitializeComponent();

            //Make the text box readonly
            textBox1.ReadOnly = true;

            //fill the combo boxes
            for(int i = 1; i <= 12; i ++)
            {
                this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //15 minute intervals
                for (int k = 15; k <= 45; k += 15)
                {
                    this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString()});
                    this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                }
            }

            //add number of shifts
            for(int j = 1; j <= 8; j ++)
            {
                this.numberOfShiftsCombo1.Items.Add(j.ToString());
            }

            //Fill the am/pm selector
            this.am_pmCombo1.Items.Add("AM");
            this.am_pmCombo1.Items.Add("PM");
            this.am_pmCombo2.Items.Add("AM");
            this.am_pmCombo2.Items.Add("PM");

            //set the default view for the combo
            this.startHour1.SelectedIndex = -1;
            this.endHour1.SelectedIndex = -1;
            this.numberOfShiftsCombo1.SelectedIndex = 0;
            this.am_pmCombo1.SelectedIndex = 1;
            this.am_pmCombo2.SelectedIndex = 1;
 

            //Make the combo box read only
            this.startHour1.DropDownStyle = ComboBoxStyle.DropDownList; 
            this.endHour1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// When the user clicks the "Create" Button this is what will happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBTN_Click(object sender, EventArgs e)
        {
            //Get the times set by the combo box and the number of shifts
            startTimeFromCombo = this.startHour1.GetItemText(this.startHour1.SelectedItem) + "" + this.am_pmCombo1.GetItemText(this.am_pmCombo1.SelectedItem);
            endTimeFromCombo = this.endHour1.GetItemText(this.endHour1.SelectedItem) + "" + this.am_pmCombo2.GetItemText(this.am_pmCombo2.SelectedItem);
            numberOfShifts = int.Parse(this.numberOfShiftsCombo1.SelectedItem.ToString());

            //Input Error checking!
            if (startTimeFromCombo.Equals("PM") || startTimeFromCombo.Equals("AM") || startTimeFromCombo == null ||
                endTimeFromCombo.Equals("PM") || endTimeFromCombo.Equals("AM") || endTimeFromCombo == null)
            {
                MessageBox.Show("Valid time must be set.",
                                 "Problem...",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button1);
                return;
            }
            else if (Convert.ToDateTime(startTimeFromCombo) >= Convert.ToDateTime(endTimeFromCombo))
            {
                MessageBox.Show("Valid time must be set.",
                                 "Problem...",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button1);
                return;
            }

            bw = new BackgroundWorker();
            //Initialize the Background worker and report progress
            bw.WorkerReportsProgress = true;
            //Add Work to the worker thread
            bw.DoWork += new DoWorkEventHandler(Bw_DoWork);
            //Get progress changes
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //Get work completed events
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //Do all the work
            if(bw.IsBusy != true)
            {
                //Disable the button
                createBTN.Enabled = false;
                //Run the work
                bw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Al the work is done in this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //Sender to send info to progressbar
            var worker = sender as BackgroundWorker;

            worker.ReportProgress(15);

            //***********************CREATE MASTER LOG FILE PT 1**********************
            LogoutLogImporter classRoomTimeLogs = new LogoutLogImporter(this, startTimeFromCombo, endTimeFromCombo);

            string[,] arrayClassRooms = classRoomTimeLogs.getLogOutArray();
            
            //Create the new Excel file where we will store all the new information
            logoutMaster = new Excel.Application();
            logoutMasterWorkBook = logoutMaster.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            logoutMasterWorkSheet = (Excel.Worksheet)logoutMasterWorkBook.Worksheets[1];

            //***********************END OF CREATE MASTER LOGOUT FILE PT 1**************

            worker.ReportProgress(50);

            //***********************CREATE MASTER LOG FILE PT 2***********************
            ZoneSuperLogImporter ZoneLogs = new ZoneSuperLogImporter(this, startTimeFromCombo, endTimeFromCombo);
             
            //Get the three logs
            string[,] JInstruction = ZoneLogs.getJeannineLog();
            string[,] DInstruction = ZoneLogs.getDerekLog();
            string[,] RInstruction = ZoneLogs.getRaulLog();

            //write all the data to the excel file
            //merg the all the data together into the master log
           this.WriteLogOutArray(logoutMasterWorkSheet, arrayClassRooms, classRoomTimeLogs.getLogOutArrayCount(),
                                                                        JInstruction, DInstruction, RInstruction);

            //Saving and closing the new excel file
            logoutMaster.DisplayAlerts = false;

            //***********************END OF CREATE MASTER LOG FILES PT 2*******************
            worker.ReportProgress(85);

            //************************CONCATINATE CURRENT LOG WITH EXISTING MASTER*********

            this.mergeMasterWithExisting(logoutMasterWorkSheet);

            //********************END CONCATINATE CURRENT LOG WITH EXISTING MASTER**********

            //Gracefully close all instances
            Quit();

            //Send report that we are all done 100%
            worker.ReportProgress(100);

            return;
        }

        /// <summary>
        /// Update the progress bar 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //This is called on GUI/main thread, so you can access the controls properly
            this.workProgressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// This event handler deals with the results of the
        /// background operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox1.Text = "Please ensure files are in the correct location.";
                this.workProgressBar.Value = 0;
                this.workProgressBar.Refresh();
                Quit();
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                textBox1.Text = "Canceled";
                this.workProgressBar.Value = 0;
                this.workProgressBar.Refresh();
                Quit();
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                textBox1.Text = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory).ToString();
                workDone = true;
                Quit();
            }
            //Enable the button again
            createBTN.Enabled = true;

            //Open the merged file
            if (workDone)
            {
                //Make a copy of the exel file
                 System.IO.File.Copy(EXISTING_MASTER_LOG, EXISTING_MASTER_LOG_COPY, true);
                //Make a new copied file not hidden
                System.IO.File.SetAttributes(EXISTING_MASTER_LOG_COPY, System.IO.FileAttributes.Normal);

                //Open the master log file
                Excel.Application excel = new Excel.Application();
                Excel.Workbook wb = excel.Workbooks.Open(EXISTING_MASTER_LOG);
                excel.Visible = true;
            }
        }

        /// <summary>
        /// ALL HELPER METHODS GO HERE BELLOW HERE! 
        ///  
        /// This method will write our arrays to the excel file.
        /// This method generates the Excel output via the arrays
        /// </summary>
        private void WriteLogOutArray(Excel.Worksheet worksheet, string[,] values, int index, 
                                            string[,] array1, string[,] array2, string[,] array3)
        {
            //Setting up the cells to put the information into
            Excel.Range taskType_range = worksheet.get_Range("B2", "B" + (index + 1));
            Excel.Range date_range = worksheet.get_Range("C2", "C" + (index + 1));
            Excel.Range value_range = worksheet.get_Range("D2", "G" + (index + 1));

            //Get the ranges for the 3 arrays
            Excel.Range logRange1 = worksheet.get_Range("B" + (index + 2), "G" + (array1.GetLength(0) + index + 1));
            Excel.Range logRange2 = worksheet.get_Range("B" + (array1.GetLength(0) + index + 2), "G" + (array1.GetLength(0) + array2.GetLength(0) + index + 1));
            Excel.Range logRange3 = worksheet.get_Range("B" + (array1.GetLength(0) + array2.GetLength(0) + index + 2), "G" +
                                                                (array1.GetLength(0) + array2.GetLength(0) + array3.GetLength(0) + index + 1));
            Excel.Range ace017CloseRange = worksheet.get_Range("B" + (array1.GetLength(0) + array2.GetLength(0) + array3.GetLength(0) + index + 2),
                                                                "G" + (array1.GetLength(0) + array2.GetLength(0) + array3.GetLength(0) + index + 2));

            //Formatt for easy to read for "Crestron logout"
            taskType_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            taskType_range.ColumnWidth = 20;
            taskType_range.Value2 = "Crestron Logout";

            //Formatt for east reading of the date
            date_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            date_range.ColumnWidth = 10;
            DateTime today = DateTime.Today;
            date_range.Value2 = today.ToString("M/d/yy");
            //Set the date format for the whole column. 
            date_range.EntireColumn.NumberFormat = "M/d/yy";

            //Format for easy reading of Time, Building, and Room.
            value_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            value_range.ColumnWidth = 17;
            value_range.Value2 = values;

            //Add the three logs to the master
            logRange1.Value2 = array1;
            logRange2.Value2 = array2;
            logRange3.Value2 = array3;

            //Add ACE017 to the log if we have are in the time peiod
            DateTime startingTime = Convert.ToDateTime(this.startTimeFromCombo.ToString());
            DateTime endingTime = Convert.ToDateTime(this.endTimeFromCombo.ToString());
            DateTime check = DateTime.ParseExact("1600", "HHmm", null);
            if ((check.TimeOfDay >= startingTime.TimeOfDay) && (check.TimeOfDay <= endingTime.TimeOfDay))
            {
                //For one shift we have to leave the dat time standard
                if( numberOfShifts == 1)
                {
                    string[] ace017String = {"CLOSE ACE017", today.ToString("M/d/yy"), "1600", "ACE", "017",
                    @"Keys are in ACE 015 storeroom. Make sure all workstations have a keyboard and a mouse, shut down the lights and lock the door.If the room is already locked please report on your log."};
                    ace017CloseRange.Value2 = ace017String;
                }
                //For more than one shift we have to set the date time to an integer
                else
                {
                    string[] ace017String = {"CLOSE ACE017", ((int)today.ToOADate()).ToString(), "1600", "ACE", "017",
                    @"Keys are in ACE 015 storeroom. Make sure all workstations have a keyboard and a mouse, shut down the lights and lock the door.If the room is already locked please report on your log."};
                    ace017CloseRange.Value2 = ace017String;
                }  
            }

            //Sorting it by time column
            dynamic allDataRange = worksheet.UsedRange;
            allDataRange.Sort(allDataRange.Columns[3], Excel.XlSortOrder.xlAscending);
        }

        /// <summary>
        /// This method will merger our file with the already existing file in sorted order. 
        /// </summary>
        /// <param name="worksheet"></param>
        public void mergeMasterWithExisting(Excel.Worksheet worksheet)
        {

            //Open the exisitng excel file
            existingMaster = new Excel.Application();
            existingMaster.Visible = false;
            try
            {
                existingMasterWorkBook = existingMaster.Workbooks.Open(EXISTING_MASTER_LOG);
                existingMasterWorkSheet = (Excel.Worksheet)existingMasterWorkBook.Worksheets[1];
            }
            catch (Exception)
            {
                Quit();
                return;
            }

            //Get the number of rowms from the worksheet and the existing worksheet
            int sheetRowCount = worksheet.UsedRange.Rows.Count;
            int lastRowDestination = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;

            //Select the ranges from the worksheet and the existing work sheet we are going to work with. 
            Excel.Range range = worksheet.get_Range("A2", "G" + sheetRowCount);
            Excel.Range dividerRange = existingMasterWorkSheet.get_Range("A" + (lastRowDestination + 1)).EntireRow;
            Excel.Range destinationRange = existingMasterWorkSheet.get_Range("A" + (lastRowDestination + 2), "G"
                + (lastRowDestination + sheetRowCount));

            //Put red accross the divider
            Color darkRed = Color.FromArgb(204, 0, 51);
            dividerRange.Interior.Color = darkRed;

            //Zoning is done here
            if (numberOfShifts > 1)
            {
                //Pass the zoning with the number of shifts
                destinationRange.Value2 = this.generateZonedLog(range, numberOfShifts);
            }
            else
            {
                destinationRange.Value2 = range.Value2;
            }

            //Past the values from the current work sheet to the existing one
            

            //Get the new last row
            Excel.Range last_row = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

            //High light all the other/pickup/demo/setup rows
            Color redBackground = Color.FromArgb(255, 199, 206);
            Color redFont = Color.FromArgb(156, 0, 6);
            Excel.Range task_range = existingMasterWorkSheet.get_Range("B" + (lastRowDestination + 2), "B" + (last_row.Row));
            task_range.WrapText = true;
            foreach (Excel.Range cell in task_range)
            {
                if ((string)cell.Value2 != "Crestron Logout")
                {
                    cell.Interior.Color = redBackground;
                    cell.Font.Color = redFont;
                    Excel.Range task_color_change = existingMasterWorkSheet.get_Range("G" + cell.Row, "G" + cell.Row);
                    task_color_change.Interior.Color = redBackground;
                    task_color_change.Font.Color = redFont;
                }
            }

            //High light all the cells that have lapel mics
            Color lightblue = Color.FromArgb(225, 246, 255);
            Excel.Range instuciton_range = existingMasterWorkSheet.get_Range("G" + (lastRowDestination + 2), "G" + (last_row.Row));
            foreach (Excel.Range cell in instuciton_range)
            {
                if ((string)cell.Value2 == "Ensure neck mic goes back to equipment drawer.")
                {
                    cell.Interior.Color = lightblue;
                    Excel.Range task_color_change = existingMasterWorkSheet.get_Range("B" + cell.Row, "B" + cell.Row);
                    task_color_change.Interior.Color = lightblue;
                }
            }

            //Save
            existingMaster.DisplayAlerts = false;
            existingMasterWorkBook.SaveAs(EXISTING_MASTER_LOG);   
        }

        /// <summary>
        /// This method is resposible for the zoning of all the elements in the range.
        /// This will return an array with all the elements in it and with zones.
        /// </summary>
        /// <param name="range"></param>
        /// <param name="shiftNumber"></param>
        /// <returns></returns>
        private string[,] generateZonedLog(Excel.Range range, int shiftNumber)
        {
            //Setting up the ranges and the variables
            System.Array rangeArray = (System.Array)range.Cells.Value2;
            SchoolZoning sz = new SchoolZoning();
            string[,] zonedArray = covertToArray(rangeArray);       
            string[,] result = null;

            //If we have 2 shifts
            if (shiftNumber == 2)
            {
                List<string> zone1 = sz.getZone_2(1);
                List<string> zone2 = sz.getZone_2(2);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    if (zone1.Contains(zonedArray[i,4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if(zone2.Contains(zonedArray[i,4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
            }

            //If we have 3 Shifts
            else if (shiftNumber == 3)
            {
                List<string> zone1 = sz.getZone_3(1);
                List<string> zone2 = sz.getZone_3(2);
                List<string> zone3 = sz.getZone_3(3);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    if (zone1.Contains(zonedArray[i, 4]))
                    {                       
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
            }

            //If we have 4 Shifts
            else if (shiftNumber == 4)
            {
                List<string> zone1 = sz.getZone_4(1);
                List<string> zone2 = sz.getZone_4(2);
                List<string> zone3 = sz.getZone_4(3);
                List<string> zone4 = sz.getZone_4(4);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i,4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0) + zone4Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
            }

            //If we have 5 Shifts
            else if (shiftNumber == 5)
            {
                List<string> zone1 = sz.getZone_5(1);
                List<string> zone2 = sz.getZone_5(2);
                List<string> zone3 = sz.getZone_5(3);
                List<string> zone4 = sz.getZone_5(4);
                List<string> zone5 = sz.getZone_5(5);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) + 
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + 
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
            }

            //If we have 6 Shifts
            else if (shiftNumber == 6)
            {
                List<string> zone1 = sz.getZone_6(1);
                List<string> zone2 = sz.getZone_6(2);
                List<string> zone3 = sz.getZone_6(3);
                List<string> zone4 = sz.getZone_6(4);
                List<string> zone5 = sz.getZone_6(5);
                List<string> zone6 = sz.getZone_6(6);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone6Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;
                int zone6Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                    else if (zone6.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone6Array, zone6Index * zone6Array.GetLength(1), zonedArray.GetLength(1));
                        zone6Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);
                zone6Array = ZoneSuperLogImporter.RemoveEmptyRows(zone6Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0) + zone6Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
                AddToArray(result, zone6Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0));
            }

            //If we have 7 shifts
            else if (shiftNumber == 7)
            {
                List<string> zone1 = sz.getZone_7(1);
                List<string> zone2 = sz.getZone_7(2);
                List<string> zone3 = sz.getZone_7(3);
                List<string> zone4 = sz.getZone_7(4);
                List<string> zone5 = sz.getZone_7(5);
                List<string> zone6 = sz.getZone_7(6);
                List<string> zone7 = sz.getZone_7(7);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone6Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone7Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;
                int zone6Index = 0;
                int zone7Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                    else if (zone6.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone6Array, zone6Index * zone6Array.GetLength(1), zonedArray.GetLength(1));
                        zone6Index++;
                    }
                    else if (zone7.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone7Array, zone7Index * zone7Array.GetLength(1), zonedArray.GetLength(1));
                        zone7Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);
                zone6Array = ZoneSuperLogImporter.RemoveEmptyRows(zone6Array);
                zone7Array = ZoneSuperLogImporter.RemoveEmptyRows(zone7Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0) + zone6Array.GetLength(0) + 
                                        zone7Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
                AddToArray(result, zone6Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0));
                AddToArray(result, zone7Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0) +
                                    zone6Array.GetLength(0));
            }

            //If we have 8 shifts
            else if (shiftNumber == 8)
            {
                List<string> zone1 = sz.getZone_8(1);
                List<string> zone2 = sz.getZone_8(2);
                List<string> zone3 = sz.getZone_8(3);
                List<string> zone4 = sz.getZone_8(4);
                List<string> zone5 = sz.getZone_8(5);
                List<string> zone6 = sz.getZone_8(6);
                List<string> zone7 = sz.getZone_8(7);
                List<string> zone8 = sz.getZone_8(8);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone6Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone7Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone8Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;
                int zone6Index = 0;
                int zone7Index = 0;
                int zone8Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                    else if (zone6.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone6Array, zone6Index * zone6Array.GetLength(1), zonedArray.GetLength(1));
                        zone6Index++;
                    }
                    else if (zone7.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone7Array, zone7Index * zone7Array.GetLength(1), zonedArray.GetLength(1));
                        zone7Index++;
                    }
                    else if (zone8.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone8Array, zone8Index * zone8Array.GetLength(1), zonedArray.GetLength(1));
                        zone8Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);
                zone6Array = ZoneSuperLogImporter.RemoveEmptyRows(zone6Array);
                zone7Array = ZoneSuperLogImporter.RemoveEmptyRows(zone7Array);
                zone8Array = ZoneSuperLogImporter.RemoveEmptyRows(zone8Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0) + zone6Array.GetLength(0) +
                                        zone7Array.GetLength(0) + zone8Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
                AddToArray(result, zone6Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0));
                AddToArray(result, zone7Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0) +
                                    zone6Array.GetLength(0));
                AddToArray(result, zone8Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0) +
                                    zone6Array.GetLength(0) + zone7Array.GetLength(0));
            }


            //Return the merged array with the zones. 
            return result;
        }

        /// <summary>
        /// A Helper method to quickly convery an array object to a string array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string[,] covertToArray(System.Array array)
        {
            string[,] values = new string[array.GetUpperBound(0), array.GetUpperBound(1)];
            for (int i = 0; i < array.GetUpperBound(0); i++)
            {
                for (int j = 0; j < array.GetUpperBound(1); j++)
                {
                    if (array.GetValue(i + 1, j + 1) == null)
                    {
                        values[i, j] = "";
                    }
                    else
                    {
                        values[i, j] = array.GetValue(i + 1, j + 1).ToString();

                    }
                }
            }
            return values;
        }

        /// <summary>
        /// This method combines two rectange arrays together.
        /// This writes it element by element. (Might have to switch this to BlockCopy for optimization)
        /// </summary>
        /// <param name="result"></param>
        /// <param name="array"></param>
        /// <param name="start"></param>
        static void AddToArray(string[,] result, string[,] array, int start = 0)
        {
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    result[i + start, j] = array[i, j];
                }
            }
        }

        /// <summary>
        /// Close all open instances of Excel and Garbage collects.
        /// </summary>
        private void Quit()
        {            
            if(logoutMasterWorkBook != null)
            {
                logoutMasterWorkBook.Close(false, Type.Missing, Type.Missing);
                logoutMaster.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(logoutMaster);
                logoutMaster = null;
                logoutMasterWorkBook = null;
                logoutMasterWorkSheet = null;
            }

            if(existingMasterWorkBook != null)
            {
                existingMasterWorkBook.Close(0);
                existingMaster.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(existingMaster);
                existingMaster = null;
                existingMasterWorkBook = null;
                existingMasterWorkSheet = null;
            }
            GC.Collect();  
        }
    }
}
