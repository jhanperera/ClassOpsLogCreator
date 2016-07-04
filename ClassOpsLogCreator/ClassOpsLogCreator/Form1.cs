using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpsLogCreator
{
    public partial class LogCreator : Form
    {
        //Public readonly attribues
        public readonly string ROOM_SCHED = @"H:\CS\SHARE-PT\CLASSOPS\clo.xlsx";
        public readonly string JEANNINE_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Derek\Derek's Log.xlsx";

        //DEBUG CODE! 
        //ONLY UNCOMMENT FOR LOCAL USE ONLY! 
        /*public readonly string ROOM_SCHED = @"C:\Users\Jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\room schedule.xlsx";
        public readonly string JEANNINE_LOG = @"C:\Users\Jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"C:\Users\Jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\Derek's Log.xlsx";*/

        private static Excel.Application logoutMaster = null;
        private static Excel.Workbook logoutMasterWorkBook = null;
        private static Excel.Worksheet logoutMasterWorkSheet = null;

        private static Excel.Application MasterLog = null;
        private static Excel.Workbook MasterLogWorkBook = null;
        private static Excel.Worksheet MasterLogWorkSheet = null;

        //Use a background worker to allow the GUI to still be functional and not hang.
        private static BackgroundWorker bw = new BackgroundWorker();

        /** Constructor for the system. (Changes here should be confirmed with everyone first) */
        public LogCreator()
        {
            InitializeComponent();

            //Make the text box readonly
            textBox1.ReadOnly = true;

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
            //Initialize the Background worker and report progress
            bw.WorkerReportsProgress = true;
            //Add Work to the worker thread
            bw.DoWork += new DoWorkEventHandler(Bw_DoWork);
            //Get progress changes
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //Get work completed events
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //Do all the wok 
            if(bw.IsBusy != true)
            {
                //Disable the button
                createBTN.Enabled = false;
                //Run the work
                bw.RunWorkerAsync();
            }
            //***********************DEGUB CODE***************************************
            //textBox1.Text = DateTime.FromOADate(double.Parse(arrayTimes[0])).ToString("hh:mm:tt");
            //textBox1.Text = Environment.GetFolderPath(
                         //System.Environment.SpecialFolder.DesktopDirectory).ToString();
            //***********************END OF DEGUB CODE*********************************
        }

        /** Al the work is done in this method
         */
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //Sender to send info to progressbar
            var worker = sender as BackgroundWorker;

            worker.ReportProgress(15);
            //***********************CREATE MASTER LOGOUT FILE**********************
            LogoutLogImporter classRoomTimeLogs = new LogoutLogImporter(this);

            string[] arrayClassRooms = classRoomTimeLogs.getClassRooms();
            string[] arrayLastTimes = classRoomTimeLogs.getLastTImes();

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

            //***********************END OF CREATE MASTER LOGOUT FILE**************

            worker.ReportProgress(50);

            //***********************CREATE MASTER LOG FILE!***********************
            ZoneSuperLogImporter ZoneLogs = new ZoneSuperLogImporter(this);

            //Get the three logs
            string[,] JInstruction = ZoneLogs.getJeannineLog();
            string[,] DInstruction = ZoneLogs.getDerekLog();
            string[,] RInstruction = ZoneLogs.getRaulLog();

            //Create the new Excel file where we will store all the new information
            MasterLog = new Excel.Application();
            MasterLog.Visible = false;
            MasterLogWorkBook = MasterLog.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            MasterLogWorkSheet = (Excel.Worksheet)MasterLogWorkBook.Worksheets[1];

            //write all the data to the excel file
            //merg the 3 array logs into a master excel log.
            this.WriteMasterLog(MasterLogWorkSheet, JInstruction, DInstruction, RInstruction);

            //Saving and closing the new excel file
            MasterLogWorkBook.SaveAs(Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory) + @"\Master_Log.xlsx");
            //***********************END OF CREATE MASTER LOG FILES*******************
            worker.ReportProgress(90    );
            //Gracefully close all instances
            Quit();
            //Send report that we are all done 100%
            worker.ReportProgress(100);
            return;
        }

        /** Update the progress bar 
         */
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //This is called on GUI/main thread, so you can access the controls properly
            this.workProgressBar.Value = e.ProgressPercentage;
        }

        /** This event handler deals with the results of the
         *  background operation.
         */
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
            }
            //Enable the button again
            createBTN.Enabled = true;
        }

        /// <summary>
        ///  ALL HELPER METHODS GO HERE BELLOW HERE! 
        /// </summary>

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
                    newArray[index] = DateTime.FromOADate(double.Parse(array[i])).ToString("hh:mm tt");
                    index++;
                }
            } 
            //Return an array with no null characters. 
            return newArray = newArray.Where(n => n != null).ToArray();
        }

        
        /**This method will write our arrays to the excel file.
         * 
         * This method generates the Excel output via the arrays
         */
        private void WriteLogOutArray(Excel.Worksheet worksheet, string[] arrayClass, string[] arrayTime)
        {
            ClassInfo classList = new ClassInfo();
            string[,] values = new string[arrayClass.Length, 2];
            DateTime fourPM = DateTime.FromOADate(0.666);
            DateTime tenPM = DateTime.FromOADate(0.920);
            //Add all the elements of the array's into one array. 
            int index = 0;
            for (int i = 0; i < arrayClass.Length; i++)
            {
                //Add only the times between 4pm and 10pm
                //and remove all classes with no crestron. 
                DateTime check = Convert.ToDateTime(arrayTime[i]);
                if((check.TimeOfDay >= fourPM.TimeOfDay) && (check.TimeOfDay <= tenPM.TimeOfDay)
                    && (classList.hasCrestron(arrayClass[i])))
                {
                    values[index, 0] = arrayTime[i];
                    values[index, 1] = arrayClass[i];
                    index++;
                }  
            }

            //Setting up the cells to put the information into
            Excel.Range taskType_range = worksheet.get_Range("B2", "B" + (index + 1)); 
            Excel.Range value_range = worksheet.get_Range("C2", "D" + (index + 1));

            //Formatt for easy to read for "Crestron logout"
            taskType_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            taskType_range.ColumnWidth = 20;
            taskType_range.Value2 = "Crestron Logout";

            //Format for easy reading of Time, Building, and Room.
            value_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            value_range.ColumnWidth = 17;
            value_range.Value2 = values;

            //Sorting it by column 2
            dynamic allDataRange = worksheet.UsedRange;
            allDataRange.Sort(allDataRange.Columns[2], Excel.XlSortOrder.xlAscending);
        }

        /**This method will write our arrays to the excel file.
        * 
        * This method generates the Excel output via the arrays
        */
        private void WriteMasterLog(Excel.Worksheet worksheet, string[,] array1, string[,] array2, string[,] array3)
        {
            //Get the range to inser the 2d arrayinto
            Excel.Range logRange1 = worksheet.get_Range("B2", "G"+ (array1.GetLength(0) + 1));
            Excel.Range logRange2 = worksheet.get_Range("B" + (array1.GetLength(0) + 2), "G" + (array1.GetLength(0) + array2.GetLength(0) + 1));
            Excel.Range logRange3 = worksheet.get_Range("B" + (array1.GetLength(0) + array2.GetLength(0) + 2), "G" + 
                                                                (array1.GetLength(0) + array2.GetLength(0) + array3.GetLength(0) + 1));
            //Save all the values to the range. 
            logRange1.Value2 = array1;
            logRange2.Value2 = array2;
            logRange3.Value2 = array3;

            //Format the worksheet
            this.formatWorkSheet(worksheet);

        }

        /**This method will format the work sheet to be easy to read and 
         * work with
         */
        public void formatWorkSheet(Excel.Worksheet worksheet)
        {

            //Some color valiables 
            Color yellow = Color.FromArgb(255, 235, 156);
            Color brown = Color.FromArgb(156, 101, 0);

            //Set the headers range
            Excel.Range staffNameRange = worksheet.get_Range("A1", "A1");
            Excel.Range taskTypeRange = worksheet.get_Range("B1", "B1");
            Excel.Range dateRange = worksheet.get_Range("C1", "C1");
            Excel.Range timeRange = worksheet.get_Range("D1", "D1");
            Excel.Range buildingRange = worksheet.get_Range("E1", "E1");
            Excel.Range roomRange = worksheet.get_Range("F1", "F1");
            Excel.Range instructionsRange = worksheet.get_Range("G1", "G1");
            Excel.Range initialRange = worksheet.get_Range("H1", "H1");

            //Add the headers and format the cells
            //Staff Name header
            staffNameRange.ColumnWidth = 11;
            staffNameRange.Interior.Color = yellow;
            staffNameRange.Font.Color = brown;
            staffNameRange.Font.Bold = true;
            staffNameRange.Value2 = "Staff Name";

            //Task Type header
            taskTypeRange.ColumnWidth = 22;
            taskTypeRange.Interior.Color = yellow;
            taskTypeRange.Font.Color = brown;
            taskTypeRange.Font.Bold = true;
            taskTypeRange.Value2 = "Task Type";

            //Date header
            dateRange.ColumnWidth = 10;
            dateRange.Interior.Color = yellow;
            dateRange.Font.Color = brown;
            dateRange.Font.Bold = true;
            dateRange.Value2 = "Date";

            //Time header
            
            timeRange.ColumnWidth = 7;
            timeRange.Interior.Color = yellow;
            timeRange.Font.Color = brown;
            timeRange.Font.Bold = true;
            timeRange.Value2 = "Time";

            //Building header
            buildingRange.ColumnWidth = 14;
            buildingRange.Interior.Color = yellow;
            buildingRange.Font.Color = brown;
            buildingRange.Font.Bold = true;
            buildingRange.Value2 = "Building";

            //Room header
            roomRange.ColumnWidth = 11;
            roomRange.Interior.Color = yellow;
            roomRange.Font.Color = brown;
            roomRange.Font.Bold = true;
            roomRange.Value2 = "Room";

            //Instructions header;
            instructionsRange.ColumnWidth = 42;
            instructionsRange.Interior.Color = yellow;
            instructionsRange.Font.Color = brown;
            instructionsRange.Font.Bold = true;
            instructionsRange.Value2 = "Special Instructions/Comments";

            //Initial header
            initialRange.ColumnWidth = 11;
            initialRange.Interior.Color = yellow;
            initialRange.Font.Color = brown;
            initialRange.Font.Bold = true;
            initialRange.Value2 = "Initial Here";

            //outline around all boxes 
            Excel.Range fullRange = worksheet.UsedRange;
            fullRange.Borders.Color = System.Drawing.Color.Black.ToArgb();
            fullRange.WrapText = true;
            fullRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


        }
        


        /** Close all open instances of Excel and Garbage collects. 
         * 
         */
        private void Quit()
        {            
            if(logoutMasterWorkBook != null)
            {
                logoutMasterWorkBook.Close(0);
                logoutMaster.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(logoutMaster);
            }

            if(MasterLogWorkBook != null)
            {
                MasterLogWorkBook.Close(0);
                MasterLog.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(MasterLog);
            }

            logoutMaster = null;
            logoutMasterWorkBook = null;
            logoutMasterWorkSheet = null;

            MasterLog = null;
            MasterLogWorkBook = null;
            MasterLogWorkSheet = null;

            GC.Collect();
        }
    }
}
