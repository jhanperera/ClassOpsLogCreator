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
        /*public readonly string ROOM_SCHED = @"H:\CS\SHARE-PT\CLASSOPS\clo.xlsm";
        public readonly string JEANNINE_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Derek\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG_COPY = @"H:\CS\SHARE-PT\PW\masterlog.xlsx";
        public readonly string EXISTING_MASTER_LOG = @"H:\CS\SHARE-PT\CLASSOPS\masterlog.xlsx";
        public readonly string CLO_GENERATED_LOG = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CLO_END_TIMES.xlsx";*/

        //DEBUG CODE! 
        //ONLY UNCOMMENT FOR LOCAL USE ONLY! 
        public static readonly string path = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator";
        public readonly string ROOM_SCHED = path + @"\CLASSOPS\clo.xlsm";
        public readonly string JEANNINE_LOG = path + @"\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = path + @"\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = path + @"\CLASSOPS\Derek\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG_COPY = path + @"\PW\masterlog.xlsx";
        public readonly string EXISTING_MASTER_LOG = path + @"\CLASSOPS\masterlog.xlsx";
        public readonly string CLO_GENERATED_LOG = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CLO_END_TIMES.xlsx";

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
        private Boolean plusClicked = false;

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
                //Tab 1 
                this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //Tab 2
                this.cloGenStart1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.cloGenEnd1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //15 minute intervals
                for (int k = 15; k <= 45; k += 15)
                {
                    //Tab 1
                    this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString()});
                    this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    //Tab 2
                    this.cloGenStart1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    this.cloGenEnd1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                }
            }

            //add number of shifts
            for(int j = 1; j <= 8; j ++)
            {
                this.numberOfShiftsCombo1.Items.Add(j.ToString());
            }

            //Fill the am/pm selector for tab 1
            this.am_pmCombo1.Items.Add("AM");
            this.am_pmCombo1.Items.Add("PM");
            this.am_pmCombo2.Items.Add("AM");
            this.am_pmCombo2.Items.Add("PM");
            //Fill the am/pm selector for tab 2
            this.cloAm_pmCombo1.Items.Add("AM");
            this.cloAm_pmCombo1.Items.Add("PM");
            this.cloAm_pmCombo2.Items.Add("AM");
            this.cloAm_pmCombo2.Items.Add("PM");

            //set the default view for the combo for tab 1
            this.startHour1.SelectedIndex = -1;
            this.endHour1.SelectedIndex = -1;
            this.numberOfShiftsCombo1.SelectedIndex = 0;
            this.am_pmCombo1.SelectedIndex = 1;
            this.am_pmCombo2.SelectedIndex = 1;
            ////set the default view for the combo for tab 1
            this.cloGenStart1.SelectedIndex = -1;
            this.cloGenEnd1.SelectedIndex = -1;
            this.cloAm_pmCombo1.SelectedIndex = 1;
            this.cloAm_pmCombo2.SelectedIndex = 1;


            //Make the combo box read only for tab 1
            this.startHour1.DropDownStyle = ComboBoxStyle.DropDownList; 
            this.endHour1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            //Make the combo box read only for tab 2
            this.cloGenStart1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cloGenEnd1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cloAm_pmCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cloAm_pmCombo2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// When the user clicks the "Create" Button this is what will happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBTN_Click_1(object sender, EventArgs e)
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
            bw.DoWork += new DoWorkEventHandler(Bw_DoWorkTab1);
            //Get progress changes
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //Get work completed events
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedTab1);
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
        /// When the user clicks "Create CLO log" on tab 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createCLOBTN_Click(object sender, EventArgs e)
        {
            //Get the times set by the combo box
            startTimeFromCombo = this.cloGenStart1.GetItemText(this.cloGenStart1.SelectedItem) + "" + this.cloAm_pmCombo1.GetItemText(this.cloAm_pmCombo1.SelectedItem);
            endTimeFromCombo = this.cloGenEnd1.GetItemText(this.cloGenEnd1.SelectedItem) + "" + this.cloAm_pmCombo2.GetItemText(this.cloAm_pmCombo2.SelectedItem);

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
            bw.DoWork += new DoWorkEventHandler(Bw_DoWorkTab2);
            //Get progress changes
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //Get work completed events
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedTab2);
            //Do all the work
            if (bw.IsBusy != true)
            {
                //Disable the button
                createCLOBTN.Enabled = false;
                //Run the work
                bw.RunWorkerAsync();
            }

        }

        /// <summary>
        /// Al log (tab1) work is done in this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_DoWorkTab1(object sender, DoWorkEventArgs e)
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
                                                                        JInstruction, DInstruction, RInstruction, true);

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
        /// Al clo (tab2) work is done in this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_DoWorkTab2(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            //We are going to only open the clo with set start times and end times
            LogoutLogImporter classRoomTimeLogs = new LogoutLogImporter(this, startTimeFromCombo, endTimeFromCombo);

            string[,] arrayClassRooms = classRoomTimeLogs.getLogOutArray();

            //Create the new Excel file where we will store all the new information
            logoutMaster = new Excel.Application();
            logoutMasterWorkBook = logoutMaster.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            logoutMasterWorkSheet = (Excel.Worksheet)logoutMasterWorkBook.Worksheets[1];

            //Get the three logs (Empty logs for this purpos)
            string[,] JInstruction = new string[1, 1];
            string[,] DInstruction = new string[1, 1];
            string[,] RInstruction = new string[1, 1];

            //write all the data to the excel file
            this.WriteLogOutArray(logoutMasterWorkSheet, arrayClassRooms, classRoomTimeLogs.getLogOutArrayCount(),
                                                                         JInstruction, DInstruction, RInstruction, false);
            //Format the sheet for easy reading
            Excel.Range G_range = logoutMasterWorkSheet.get_Range("G2", "G" + (classRoomTimeLogs.getLogOutArrayCount() + 1));
            G_range.ColumnWidth = 49;

            //Save to desktop
            logoutMaster.DisplayAlerts = false;
            logoutMasterWorkBook.SaveAs(CLO_GENERATED_LOG);

            //update progress bar
            worker.ReportProgress(100);

            //Clean up and close all instances
            Quit();

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
        /// background operation for tab 1 work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_RunWorkerCompletedTab1(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox1.Text = "Something went wrong!";
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
                System.IO.File.Delete(EXISTING_MASTER_LOG_COPY);
                System.IO.File.Copy(EXISTING_MASTER_LOG, EXISTING_MASTER_LOG_COPY, true);
                //Make a new copied file not hidden
                System.IO.File.SetAttributes(EXISTING_MASTER_LOG_COPY, System.IO.FileAttributes.Hidden);

                //Open the master log file
                Excel.Application excel = new Excel.Application();
                Excel.Workbook wb = excel.Workbooks.Open(EXISTING_MASTER_LOG);
                excel.Visible = true;
            }
        }

        // <summary>
        /// This event handler deals with the results of the
        /// background operation for tab2 work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_RunWorkerCompletedTab2(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                this.workProgressBar.Value = 0;
                this.workProgressBar.Refresh();
                Quit();
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                workDone = true;
                Quit();
            }
            //Enable the button
            createCLOBTN.Enabled = true;

            //Open the CLO file
            if (workDone)
            {
                Excel.Application excel = new Excel.Application();
                Excel.Workbook wb = excel.Workbooks.Open(CLO_GENERATED_LOG);
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
                                            string[,] array1, string[,] array2, string[,] array3, bool includeACE)
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
            if (includeACE &&(check.TimeOfDay >= startingTime.TimeOfDay) && (check.TimeOfDay <= endingTime.TimeOfDay))
            {
                string[] ace017String = {"CLOSE ACE017", today.ToString("M/d/yy"), "1600", "ACE", "017",
                @"Keys are in ACE 015 storeroom. Make sure all workstations have a keyboard and a mouse, shut down the lights and lock the door.If the room is already locked please report on your log."};
                ace017CloseRange.Value2 = ace017String; 
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
                SchoolZoning sz = new SchoolZoning();
                //Pass the zoning with the number of shifts
                destinationRange.Value2 = sz.generateZonedLog(range, numberOfShifts);
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
        /// This will add a small addiction to the closing operation of the application
        /// Clear he clo file and clean up the memory.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //We are going to use the base onFormClose operations and add more
            base.OnFormClosing(e);

            //If the sysstem gets shutdown we close eveything gracefully
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Closing, clear CLO?","Closing", MessageBoxButtons.YesNo))
            {
                //No the person does not want to close the application
                //Else we go to defualt case
                case DialogResult.No:
                    //Close with no clear
                    break;
                default:
                    Excel.Application roomSched = new Excel.Application();
                    Excel.Workbook roomWorkBook = null;
                    Excel.Worksheet roomSheet1 = null;
                    roomSched.Visible = false;

                    try
                    {
                        //This should look for the file
                        roomWorkBook = roomSched.Workbooks.Open(ROOM_SCHED);
                        //Work in worksheet number 1
                        roomSheet1 = (Excel.Worksheet)roomWorkBook.Sheets[1];

                    }
                    catch (Exception)
                    {
                        //File not found...

                        Quit();
                        return;
                    }

                    //Clean out the clo file
                    Excel.Range clearAllRange = roomSheet1.UsedRange;
                    clearAllRange.Clear();
                    //Save
                    roomWorkBook.Save();

                    //Clean up the memory
                    if (roomWorkBook != null)
                    {
                        roomWorkBook.Close(false, Type.Missing, Type.Missing);
                        roomSched.Quit();
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(roomSched);
                        roomSched = null;
                        roomWorkBook = null;
                        roomSheet1 = null;
                    }
                    break;
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

        /// <summary>
        /// This action listener opens up the "About" box when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }


        /// <summary>
        /// When the + button is clicked
        /// 
        /// Make the new controls apear and extend the frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusBTN_Click(object sender, EventArgs e)
        {
            this.Height = 475;
            //create a beveled line
            bevelLineLabel.AutoSize = false;
            bevelLineLabel.Height = 2;
            bevelLineLabel.BorderStyle = BorderStyle.Fixed3D;

            //Set them all to visabl
            this.bevelLineLabel.Visible = true;
            this.toLabel2.Visible = true;
            this.shiftTime2.Visible = true;
            this.startHour2.Visible = true;
            this.am_pmCombo3.Visible = true;
            this.endHour2.Visible = true;
            this.am_pmCombo4.Visible = true;
            this.numberOfShiftsLabel2.Visible = true;
            this.numberOfShiftsCombo2.Visible = true;
            
            this.plusBTN.Text = "-";
            this.plusClicked = true;
        }
    }
}
