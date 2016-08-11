using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// 
/// Description of class: This is the main thread class
/// all the main event handlers and work is done here. 
/// All output is generated from here and main features are 
/// all called here. 
///
/// Class Version: 0.2.0.0 - BETA - 852016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>
namespace ClassOpsLogCreator
{
    public partial class LogCreator : Form
    {
        #region Private Attributes/Variables

        /*public readonly string ROOM_SCHED = @"H:\CS\SHARE-PT\CLASSOPS\clo.xlsm";
        public readonly string JEANNINE_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Derek\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG_COPY = @"H:\CS\SHARE-PT\PW\masterlog.xlsx";
        public readonly string EXISTING_MASTER_LOG = @"H:\CS\SHARE-PT\CLASSOPS\masterlog.xlsx";
        public readonly string CLO_GENERATED_LOG = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CLO_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xlsx";*/

        //DEBUG CODE! 
        //ONLY UNCOMMENT FOR LOCAL USE ONLY! 
        public readonly string ROOM_SCHED = @"C:\Users\pereraj\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\clo.xlsm";
        public readonly string JEANNINE_LOG = @"C:\Users\pereraj\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"C:\Users\pereraj\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"C:\Users\pereraj\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\Derek\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG_COPY = @"C:\Users\pereraj\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\PW\masterlog.xlsx";
        public readonly string EXISTING_MASTER_LOG = @"C:\Users\pereraj\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\masterlog.xlsx";
        public readonly string CLO_GENERATED_LOG = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CLO_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xlsx";

        //A queue for some thread safer operations
        private readonly ConcurrentQueue<System.Array> _queue = new ConcurrentQueue<System.Array>();

        private static Excel.Application logoutMaster = null;
        private static Excel.Workbook logoutMasterWorkBook = null;
        private static Excel.Worksheet logoutMasterWorkSheet = null;

        private static Excel.Application existingMaster = null;
        private static Excel.Workbook existingMasterWorkBook = null;
        private static Excel.Worksheet existingMasterWorkSheet = null;

        //Use a background worker to allow the GUI to still be functional and not hang.
        private static BackgroundWorker bw = null;

        //This is for start time and end time variables 
        private string startTimeFromCombo1 = null;
        private string endTimeFromCombo1 = null;
        private int numberOfShifts1 = 0;
        //For second selection 
        private string startTimeFromCombo2 = null;
        private string endTimeFromCombo2 = null;
        private int numberOfShifts2 = 0;
        //For third selection
        private string startTimeFromCombo3 = null;
        private string endTimeFromCombo3 = null;
        private int numberOfShifts3 = 0;
        //For fourth selection
        private string startTimeFromCombo4 = null;
        private string endTimeFromCombo4 = null;
        private int numberOfShifts4 = 0;

        //Boolean values for setting flags through execution
        private Boolean workDone = false;
        private Boolean plusClicked1 = false;
        private Boolean plusClicked2 = false;
        private Boolean plusClicked3 = false;
        #endregion

        /// <summary>
        /// Constructor for the system. (Changes here should be confirmed with everyone first)
        /// </summary>
        public LogCreator()
        {
            InitializeComponent();

            //A pop up message to ensure that the user is aware that the zone super logs need to be in before running this application
            DialogResult checkMessage = checkMessage = MessageBox.Show("Ensure all Zone Supervisor Logs have been submitted before running this application. "
                               + Environment.NewLine +
                               "Failure to do so will result in incorrect output being generated",
                               "Important Notice",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);

            //If the user click cancel we close the application
            if (checkMessage == DialogResult.Cancel)
            {
                //Use an anonymous event handler to take care of this
                Load += (s, e) => Close();
                return;
            }

            //Use this for smooth panel updates (double buffering is enabled)
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            this.lineDivide1.BorderStyle = BorderStyle.Fixed3D;
            this.lineDivide1.AutoSize = false;
            this.lineDivide1.Height = 2;

            //fill the combo boxes
            for (int i = 1; i <= 12; i++)
            {
                //Select 1
                this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //Select 2
                this.startHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //Select 3
                this.startHour3.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour3.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //Select 4
                this.startHour4.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour4.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //15 minute intervals
                for (int k = 15; k <= 45; k += 15)
                {
                    //Select 1
                    this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    //Select 2
                    this.startHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    this.endHour2.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    //Select 3
                    this.startHour3.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    this.endHour3.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    //Select 4
                    this.startHour4.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    this.endHour4.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                }
            }

            //add number of shifts
            for (int j = 1; j <= 8; j++)
            {
                this.numberOfShiftsCombo1.Items.Add(j.ToString());
                this.numberOfShiftsCombo2.Items.Add(j.ToString());
                this.numberOfShiftsCombo3.Items.Add(j.ToString());
                this.numberOfShiftsCombo4.Items.Add(j.ToString());
            }

            //Fill the am/pm selector
            this.am_pmCombo1_1.Items.Add("AM");
            this.am_pmCombo1_1.Items.Add("PM");
            this.am_pmCombo1_2.Items.Add("AM");
            this.am_pmCombo1_2.Items.Add("PM");
            //Select 2
            this.am_pmCombo2_1.Items.Add("AM");
            this.am_pmCombo2_1.Items.Add("PM");
            this.am_pmCombo2_2.Items.Add("AM");
            this.am_pmCombo2_2.Items.Add("PM");
            //Select 3
            this.am_pmCombo3_1.Items.Add("AM");
            this.am_pmCombo3_1.Items.Add("PM");
            this.am_pmCombo3_2.Items.Add("AM");
            this.am_pmCombo3_2.Items.Add("PM");
            //Select 4
            this.am_pmCombo4_1.Items.Add("AM");
            this.am_pmCombo4_1.Items.Add("PM");
            this.am_pmCombo4_2.Items.Add("AM");
            this.am_pmCombo4_2.Items.Add("PM");


            //set the default view for the combo for tab 1
            this.startHour1.SelectedIndex = -1;
            this.endHour1.SelectedIndex = -1;
            this.numberOfShiftsCombo1.SelectedIndex = 0;
            this.am_pmCombo1_1.SelectedIndex = 1;
            this.am_pmCombo1_2.SelectedIndex = 1;

            //Select 2
            this.startHour2.SelectedIndex = -1;
            this.endHour2.SelectedIndex = -1;
            this.numberOfShiftsCombo2.SelectedIndex = 0;
            this.am_pmCombo2_1.SelectedIndex = 1;
            this.am_pmCombo2_2.SelectedIndex = 1;

            //Select 3
            this.startHour3.SelectedIndex = -1;
            this.endHour3.SelectedIndex = -1;
            this.numberOfShiftsCombo3.SelectedIndex = 0;
            this.am_pmCombo3_1.SelectedIndex = 1;
            this.am_pmCombo3_2.SelectedIndex = 1;

            //Select 4
            this.startHour4.SelectedIndex = -1;
            this.endHour4.SelectedIndex = -1;
            this.numberOfShiftsCombo4.SelectedIndex = 0;
            this.am_pmCombo4_1.SelectedIndex = 1;
            this.am_pmCombo4_2.SelectedIndex = 1;

            //Make the combo box read only for  select 1
            this.startHour1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.endHour1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo1_1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo1_2.DropDownStyle = ComboBoxStyle.DropDownList;

            //Make the combo box read only for select 2
            this.startHour2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.endHour2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo2_1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo2_2.DropDownStyle = ComboBoxStyle.DropDownList;

            //Make the combo box read only for select 3
            this.startHour3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.endHour3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo3_1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo3_2.DropDownStyle = ComboBoxStyle.DropDownList;

            //Make the combo box read only for select 4
            this.startHour4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.endHour4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.numberOfShiftsCombo4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo4_1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.am_pmCombo4_2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// When the user clicks the "Create" Button this is what will happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBTN_Click(object sender, EventArgs e)
        {
            /**************************************INPUT VALIDATION***********************/
            //If the first plus button is clicked
            if (plusClicked1 && !plusClicked2 && !plusClicked3)
            {
                //Get the times set by the first set of combo boxes
                startTimeFromCombo1 = this.startHour1.GetItemText(this.startHour1.SelectedItem)
                                    + "" + this.am_pmCombo1_1.GetItemText(this.am_pmCombo1_1.SelectedItem);
                endTimeFromCombo1 = this.endHour1.GetItemText(this.endHour1.SelectedItem)
                                    + "" + this.am_pmCombo1_2.GetItemText(this.am_pmCombo1_2.SelectedItem);
                numberOfShifts1 = int.Parse(this.numberOfShiftsCombo1.SelectedItem.ToString());
                //Get the times set by the second set of combo boxes
                startTimeFromCombo2 = this.startHour2.GetItemText(this.startHour2.SelectedItem)
                                     + "" + this.am_pmCombo2_1.GetItemText(this.am_pmCombo2_1.SelectedItem);
                endTimeFromCombo2 = this.endHour2.GetItemText(this.endHour2.SelectedItem)
                                     + "" + this.am_pmCombo2_2.GetItemText(this.am_pmCombo2_2.SelectedItem);
                numberOfShifts2 = int.Parse(this.numberOfShiftsCombo2.SelectedItem.ToString());

                //Input Error checking!
                if (startTimeFromCombo1.Equals("PM") || startTimeFromCombo1.Equals("AM") || startTimeFromCombo1 == null ||
                    endTimeFromCombo1.Equals("PM") || endTimeFromCombo1.Equals("AM") || endTimeFromCombo1 == null ||
                    startTimeFromCombo2.Equals("PM") || startTimeFromCombo2.Equals("AM") || startTimeFromCombo2 == null ||
                    endTimeFromCombo2.Equals("PM") || endTimeFromCombo2.Equals("AM") || endTimeFromCombo2 == null)
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1) ||
                        Convert.ToDateTime(startTimeFromCombo2) >= Convert.ToDateTime(endTimeFromCombo2))
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            else if (plusClicked1 && plusClicked2 && !plusClicked3)
            {
                //Get the times set by the first set of combo boxes
                startTimeFromCombo1 = this.startHour1.GetItemText(this.startHour1.SelectedItem)
                                     + "" + this.am_pmCombo1_1.GetItemText(this.am_pmCombo1_1.SelectedItem);
                endTimeFromCombo1 = this.endHour1.GetItemText(this.endHour1.SelectedItem)
                                     + "" + this.am_pmCombo1_2.GetItemText(this.am_pmCombo1_2.SelectedItem);
                numberOfShifts1 = int.Parse(this.numberOfShiftsCombo1.SelectedItem.ToString());
                //Get the times set by the second set of combo boxes
                startTimeFromCombo2 = this.startHour2.GetItemText(this.startHour2.SelectedItem)
                                     + "" + this.am_pmCombo2_1.GetItemText(this.am_pmCombo2_1.SelectedItem);
                endTimeFromCombo2 = this.endHour2.GetItemText(this.endHour2.SelectedItem)
                                     + "" + this.am_pmCombo2_2.GetItemText(this.am_pmCombo2_2.SelectedItem);
                numberOfShifts2 = int.Parse(this.numberOfShiftsCombo2.SelectedItem.ToString());
                //Get the times set by the third set of combo boxes
                startTimeFromCombo3 = this.startHour3.GetItemText(this.startHour3.SelectedItem)
                                     + "" + this.am_pmCombo3_1.GetItemText(this.am_pmCombo3_1.SelectedItem);
                endTimeFromCombo3 = this.endHour3.GetItemText(this.endHour3.SelectedItem)
                                     + "" + this.am_pmCombo3_2.GetItemText(this.am_pmCombo3_2.SelectedItem);
                numberOfShifts3 = int.Parse(this.numberOfShiftsCombo3.SelectedItem.ToString());

                //Input Error checking!
                if (startTimeFromCombo1.Equals("PM") || startTimeFromCombo1.Equals("AM") || startTimeFromCombo1 == null ||
                    endTimeFromCombo1.Equals("PM") || endTimeFromCombo1.Equals("AM") || endTimeFromCombo1 == null ||
                    startTimeFromCombo2.Equals("PM") || startTimeFromCombo2.Equals("AM") || startTimeFromCombo2 == null ||
                    endTimeFromCombo2.Equals("PM") || endTimeFromCombo2.Equals("AM") || endTimeFromCombo2 == null ||
                    startTimeFromCombo3.Equals("PM") || startTimeFromCombo3.Equals("AM") || startTimeFromCombo3 == null ||
                    endTimeFromCombo3.Equals("PM") || endTimeFromCombo3.Equals("AM") || endTimeFromCombo3 == null)
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1) ||
                        Convert.ToDateTime(startTimeFromCombo2) >= Convert.ToDateTime(endTimeFromCombo2) ||
                        Convert.ToDateTime(startTimeFromCombo3) >= Convert.ToDateTime(endTimeFromCombo3))
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            else if (plusClicked1 && plusClicked2 && plusClicked3)
            {
                //Get the times set by the first set of combo boxes
                startTimeFromCombo1 = this.startHour1.GetItemText(this.startHour1.SelectedItem)
                                     + "" + this.am_pmCombo1_1.GetItemText(this.am_pmCombo1_1.SelectedItem);
                endTimeFromCombo1 = this.endHour1.GetItemText(this.endHour1.SelectedItem)
                                     + "" + this.am_pmCombo1_2.GetItemText(this.am_pmCombo1_2.SelectedItem);
                numberOfShifts1 = int.Parse(this.numberOfShiftsCombo1.SelectedItem.ToString());
                //Get the times set by the second set of combo boxes
                startTimeFromCombo2 = this.startHour2.GetItemText(this.startHour2.SelectedItem)
                                     + "" + this.am_pmCombo2_1.GetItemText(this.am_pmCombo2_1.SelectedItem);
                endTimeFromCombo2 = this.endHour2.GetItemText(this.endHour2.SelectedItem)
                                     + "" + this.am_pmCombo2_2.GetItemText(this.am_pmCombo2_2.SelectedItem);
                numberOfShifts2 = int.Parse(this.numberOfShiftsCombo2.SelectedItem.ToString());
                //Get the times set by the third set of combo boxes
                startTimeFromCombo3 = this.startHour3.GetItemText(this.startHour3.SelectedItem)
                                     + "" + this.am_pmCombo3_1.GetItemText(this.am_pmCombo3_1.SelectedItem);
                endTimeFromCombo3 = this.endHour3.GetItemText(this.endHour3.SelectedItem)
                                     + "" + this.am_pmCombo3_2.GetItemText(this.am_pmCombo3_2.SelectedItem);
                numberOfShifts3 = int.Parse(this.numberOfShiftsCombo3.SelectedItem.ToString());
                //Get the times set by the fourth set of combo boxes
                startTimeFromCombo4 = this.startHour4.GetItemText(this.startHour4.SelectedItem)
                                    + "" + this.am_pmCombo4_1.GetItemText(this.am_pmCombo4_1.SelectedItem);
                endTimeFromCombo4 = this.endHour4.GetItemText(this.endHour4.SelectedItem)
                                    + "" + this.am_pmCombo4_2.GetItemText(this.am_pmCombo4_2.SelectedItem);
                numberOfShifts4 = int.Parse(this.numberOfShiftsCombo4.SelectedItem.ToString());

                //Input Error checking!
                if (startTimeFromCombo1.Equals("PM") || startTimeFromCombo1.Equals("AM") || startTimeFromCombo1 == null ||
                    endTimeFromCombo1.Equals("PM") || endTimeFromCombo1.Equals("AM") || endTimeFromCombo1 == null ||
                    startTimeFromCombo2.Equals("PM") || startTimeFromCombo2.Equals("AM") || startTimeFromCombo2 == null ||
                    endTimeFromCombo2.Equals("PM") || endTimeFromCombo2.Equals("AM") || endTimeFromCombo2 == null ||
                    startTimeFromCombo3.Equals("PM") || startTimeFromCombo3.Equals("AM") || startTimeFromCombo3 == null ||
                    endTimeFromCombo3.Equals("PM") || endTimeFromCombo3.Equals("AM") || endTimeFromCombo3 == null ||
                    startTimeFromCombo4.Equals("PM") || startTimeFromCombo4.Equals("AM") || startTimeFromCombo4 == null ||
                    endTimeFromCombo4.Equals("PM") || endTimeFromCombo4.Equals("AM") || endTimeFromCombo4 == null)
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1) ||
                        Convert.ToDateTime(startTimeFromCombo2) >= Convert.ToDateTime(endTimeFromCombo2) ||
                        Convert.ToDateTime(startTimeFromCombo3) >= Convert.ToDateTime(endTimeFromCombo3) ||
                        Convert.ToDateTime(startTimeFromCombo4) >= Convert.ToDateTime(endTimeFromCombo4))
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            else
            {
                //Get the times set by the first set of combo boxes
                startTimeFromCombo1 = this.startHour1.GetItemText(this.startHour1.SelectedItem)
                                     + "" + this.am_pmCombo1_1.GetItemText(this.am_pmCombo1_1.SelectedItem);
                endTimeFromCombo1 = this.endHour1.GetItemText(this.endHour1.SelectedItem)
                                     + "" + this.am_pmCombo1_2.GetItemText(this.am_pmCombo1_2.SelectedItem);
                numberOfShifts1 = int.Parse(this.numberOfShiftsCombo1.SelectedItem.ToString());

                //Input Error checking!
                if (startTimeFromCombo1.Equals("PM") || startTimeFromCombo1.Equals("AM") || startTimeFromCombo1 == null ||
                    endTimeFromCombo1.Equals("PM") || endTimeFromCombo1.Equals("AM") || endTimeFromCombo1 == null)
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1))
                {
                    MessageBox.Show("Valid time must be set.",
                                     "Problem...",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }

            }
            /************************************END OF INPUT VALIDATION***********************/

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
            if (bw.IsBusy != true)
            {
                //Disable the button
                createBTN.Enabled = false;
                plusBTN1.Enabled = false;
                plusBTN2.Enabled = false;
                plusBTN3.Enabled = false;
                //Run the work
                bw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// All log (tab1) work is done in this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //Sender to send info to progress bar
            var worker = sender as BackgroundWorker;

            worker.ReportProgress(15);
            //Create the new Excel file where we will store all the new information
            logoutMaster = new Excel.Application();
            logoutMasterWorkBook = logoutMaster.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            this.logCreationAndExcelWriter(1, startTimeFromCombo1, endTimeFromCombo1, numberOfShifts1, true);

            //If the first plus button is clicked
            if (plusClicked1)
            {
                worker.ReportProgress(45);
                //Add a new worksheet to add the new 
                logoutMasterWorkBook.Sheets.Add(After: logoutMasterWorkBook.Sheets[logoutMasterWorkBook.Sheets.Count]);
                this.logCreationAndExcelWriter(2, startTimeFromCombo2, endTimeFromCombo2, numberOfShifts2, false);

                //If the second plus button is clicked
                if (plusClicked2)
                {
                    worker.ReportProgress(65);
                    //Add a new worksheet to add the new 
                    logoutMasterWorkBook.Sheets.Add(After: logoutMasterWorkBook.Sheets[logoutMasterWorkBook.Sheets.Count]);
                    this.logCreationAndExcelWriter(3, startTimeFromCombo3, endTimeFromCombo3, numberOfShifts3, false);

                    //If the third plus button is clicked
                    if (plusClicked3)
                    {
                        worker.ReportProgress(85);
                        //Add a new worksheet to add the new 
                        logoutMasterWorkBook.Sheets.Add(After: logoutMasterWorkBook.Sheets[logoutMasterWorkBook.Sheets.Count]);
                        this.logCreationAndExcelWriter(4, startTimeFromCombo4, endTimeFromCombo4, numberOfShifts4, false);
                    }
                }
            }
            worker.ReportProgress(95);

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
            if(e.ProgressPercentage > 0 && e.ProgressPercentage <= 95)
            {
                this.statusText.Text = "Working...";
            }
            else if(e.ProgressPercentage > 95)
            {
                this.statusText.Text = "Done";
            }
            else
            {
                this.statusText.Text = "";
            }
            this.workProgressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// This event handler deals with the results of the
        /// background operation for tab 1 work
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
            //Enable the button again
            createBTN.Enabled = true;
            plusBTN1.Enabled = true;
            plusBTN2.Enabled = true;
            plusBTN3.Enabled = true;

            //Open the merged file
            if (workDone)
            {
                //Make a copy of the exel file
                System.IO.File.Delete(EXISTING_MASTER_LOG_COPY);
                System.IO.File.Copy(EXISTING_MASTER_LOG, EXISTING_MASTER_LOG_COPY, true);
                //Make a new copied file not hidden
                System.IO.File.SetAttributes(EXISTING_MASTER_LOG_COPY, System.IO.FileAttributes.Hidden);

                Quit();
            }
        }


        // ALL HELPER METHODS GO HERE BELLOW HERE!

        /// <summary>
        /// This method will create the logs and write to the excel file with assistance 
        /// from other helper methods
        /// </summary>
        /// <param name="worksheetNumber"></param>
        /// <param name="startTimeFromCombo"></param>
        /// <param name="endTimeFromCombo"></param>
        /// <param name="numberOfShifts"></param> 
        private void logCreationAndExcelWriter(int worksheetNumber, string startTimeFromCombo, string endTimeFromCombo, int numberOfShifts, bool redSeperator)
        {
            logoutMasterWorkSheet = (Excel.Worksheet)logoutMasterWorkBook.Worksheets[worksheetNumber];

            LogoutLogImporter classRoomTimeLogs = new LogoutLogImporter(this, startTimeFromCombo, endTimeFromCombo);

            string[,] arrayClassRooms = classRoomTimeLogs.getLogOutArray();

            ZoneSuperLogImporter ZoneLogs = new ZoneSuperLogImporter(this, startTimeFromCombo, endTimeFromCombo);

            //Get the three logs
            string[,] JInstruction = ZoneLogs.getJeannineLog();
            string[,] DInstruction = ZoneLogs.getDerekLog();
            string[,] RInstruction = ZoneLogs.getRaulLog();

            //write all the data to the excel file
            //merg the all the data together into the master log
            this.WriteLogOutArray(logoutMasterWorkSheet, arrayClassRooms, classRoomTimeLogs.getLogOutArrayCount(),
                                                                         JInstruction, DInstruction, RInstruction,
                                                                         true, startTimeFromCombo, endTimeFromCombo);

            //Saving and closing the new excel file
            logoutMaster.DisplayAlerts = false;

            this.mergeMasterWithExisting(logoutMasterWorkSheet, numberOfShifts, redSeperator, startTimeFromCombo, endTimeFromCombo);
        }

        /// <summary>
        /// 
        /// This method will write our arrays to the excel file.
        /// This method generates the Excel output via the arrays
        /// </summary>
        private void WriteLogOutArray(Excel.Worksheet worksheet, string[,] values, int index,
                                            string[,] array1, string[,] array2, string[,] array3, bool includeACE, string startTime, string endTime)
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
            DateTime startingTime = Convert.ToDateTime(startTime.ToString());
            DateTime endingTime = Convert.ToDateTime(endTime.ToString());
            DateTime check = DateTime.ParseExact("1600", "HHmm", null);
            if (includeACE && (check.TimeOfDay >= startingTime.TimeOfDay) && (check.TimeOfDay <= endingTime.TimeOfDay))
            {
                string[] ace017String = {"CLOSE ACE017", today.ToString("M/dd/yy"), "1600", "ACE", "017",
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
        public void mergeMasterWithExisting(Excel.Worksheet worksheet, int numberOfShifts, bool redSeperator, string startTime, string endTime)
        {
            //Open the existing excel file
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

            //Get the number of rows from the worksheet and the existing worksheet
            int sheetRowCount = worksheet.UsedRange.Rows.Count;
            int lastRowDestination = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;

            //Select the ranges from the worksheet and the existing work sheet we are going to work with. 
            Excel.Range range = worksheet.get_Range("A2", "G" + (sheetRowCount + 1));
            Excel.Range dividerRange = existingMasterWorkSheet.get_Range("A" + (lastRowDestination + 1)).EntireRow;
            Excel.Range destinationRange = existingMasterWorkSheet.get_Range("A" + (lastRowDestination + 2), "G"
                + (lastRowDestination + sheetRowCount + 1));

            //Put red across the divider with todays date in it
            Color darkRed = Color.FromArgb(204, 0, 51);
            if (redSeperator)
            {
                dividerRange.Interior.Color = darkRed;
                dividerRange.Font.Color = Color.White;
                dividerRange.Font.Bold = true;

                Excel.Range dayOfWeek = existingMasterWorkSheet.get_Range("D" + (lastRowDestination + 1));
                //Show the day of the week in the log
                dayOfWeek.Value2 = DateTime.Now.ToString("dddd");

            }
            else
            {
                //Make the interior white and make the borders are white
                dividerRange.Interior.Color = Color.White;
                dividerRange.Borders.Color = Color.White;
            }

            //Zoning is done here
            if (numberOfShifts > 1)
            {
                SchoolZoning sz = new SchoolZoning();
                //Pass the zoning with the number of shifts
                destinationRange.Value2 = sz.generateZonedLog(range, numberOfShifts);
                //divide the zones
                long[,] rowNumbers = this.dividedLogs(destinationRange, numberOfShifts);
                int indexCount = 0;


                //pop from the queue and send the item to the log viewer
                System.Array destinationArray = null;
                while (this._queue.TryDequeue(out destinationArray))
                {
                    //Display the log viewer
                    LogViewer lv = new LogViewer(destinationArray, startTime, endTime);
                    lv.ShowDialog();
                    //Set the employee name
                    Excel.Range name_range = existingMasterWorkSheet.get_Range("A" + (rowNumbers[indexCount, 0]), "A" + (rowNumbers[indexCount, 1]));
                    name_range.Value2 = lv.getEmployeeName();
                    indexCount++;
                }
            }
            else
            {
                //Set the destination value to the range value
                destinationRange.Value2 = range.Value2;

                //We open the log viewer at this point
                Excel.Range last = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

                //Send the range into the queue so its thread safe
                System.Array destinationArray = (System.Array)destinationRange.Cells.Value2;
                this._queue.Enqueue(destinationArray);


                //pop from the queue and send the item to the log viewer
                if (this._queue.TryDequeue(out destinationArray))
                {
                    //Display the logviewer
                    LogViewer lv = new LogViewer(destinationArray, startTime, endTime);
                    lv.ShowDialog();
                    //Set the employee name
                    Excel.Range name_range = existingMasterWorkSheet.get_Range("A" + (lastRowDestination + 2), "A" + (last.Row));
                    name_range.Value2 = lv.getEmployeeName();
                }
            }

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
            PrintDialog printDlg = new PrintDialog();
            PrinterSettings defaultSettings = new PrinterSettings();
            string defaultPrinterName = defaultSettings.PrinterName;

            //Print all the pages here
            if (numberOfShifts > 1)
            {
                
                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    //Prints all the various logs when there is more than one emplyee, this also accounts
                    //for when the zoning returns only one log.
                    long[,] rowNumbers = this.dividedLogs(destinationRange, numberOfShifts);

                    SetDefaultPrinter(printDlg.PrinterSettings.PrinterName);
                    existingMaster.Visible = true;

                    for (int i = 0; i <= rowNumbers.GetUpperBound(0) && (rowNumbers[i, 0] != 0 || rowNumbers[i, 1] != 0); i++)
                    {
                        Excel.Range logRange = existingMasterWorkSheet.get_Range("B" + (rowNumbers[i, 0]), "H" + (rowNumbers[i, 1]));
                        Excel.Range name = existingMasterWorkSheet.get_Range("A" + rowNumbers[i, 0]);
                        string nameText = name.Cells.Value2.ToString();

                        existingMasterWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&22" + nameText + ", " + startTime + " to " + endTime;

                        logRange.PrintPreview(true);
                    }
                }
 
            }
            else
            {
                //We open the log viewer at this point
                Excel.Range last = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                Excel.Range logRange = existingMasterWorkSheet.get_Range("B" + (lastRowDestination + 2), "H" + (last.Row));

                Excel.Range name = existingMasterWorkSheet.get_Range("A" + (lastRowDestination + 2));
                string nameText = name.Cells.Value2.ToString();

                existingMasterWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&22" + nameText + ", " + startTime + " to " + endTime;


                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    SetDefaultPrinter(printDlg.PrinterSettings.PrinterName);
                    existingMaster.Visible = true;
                    logRange.PrintPreview(true);
                }
            }

            SetDefaultPrinter(defaultPrinterName);

            //Empty the queue for the next operation
            System.Array someItem = null;
            while (!this._queue.IsEmpty)
            {
                this._queue.TryDequeue(out someItem);
            }

            //Close the workbook and the excel file
            existingMasterWorkBook.Close();
            existingMaster.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(existingMaster);
            existingMaster = null;
            existingMasterWorkBook = null;
            existingMasterWorkSheet = null;
        }


        /// <summary>
        /// This method is used to set the default printer when printing is called on the logs
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [DllImport("winspool.drv",
              CharSet = CharSet.Auto,
              SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean SetDefaultPrinter(String name);

        /// <summary>
        /// This method splits the logs if we have more than one
        /// employee working. This puts the logs into the Queue 
        /// 
        /// This returns an array with the start row and the end row of each log
        /// array[i,j] where i is the starting row and j is the ending row
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private long[,] dividedLogs(Excel.Range range, int numberOfShifts)
        {
            //Set the start and end row variables
            System.Array value = null;
            long[,] rowValues = new long[numberOfShifts, 2];

            long startRow = Int64.Parse(range.Row.ToString());
            long endRow = 0;
            int count = 0;
            int arrayCount = 0;

            //loop through the rows and find were the split is based on the next time
            for (int i = 1; (i < range.Rows.Count) && startRow < (startRow + range.Rows.Count); i++)
            {
                count++;
                //Get the time string from the excel sheet
                string startTimestring = (string)(range.Cells[i, 4] as Excel.Range).Value2;
                string nextTimestring = (string)(range.Cells[i + 1, 4] as Excel.Range).Value2;
                var firstTime = TimeSpan.Parse(startTimestring);
                var nextTime = TimeSpan.Parse(nextTimestring);

                //if the next time is less than the current time we know there is a split or we are at the end
                if (nextTime < firstTime || (i + 1) == range.Rows.Count)
                {
                    endRow = startRow + count;
                    //The case when we are dealing with the last log
                    if ((i + 1) == range.Rows.Count)
                    {
                        Excel.Range toArrayRange = existingMasterWorkSheet.get_Range("A" + startRow, "G" + (endRow));
                        value = (System.Array)toArrayRange.Value2;
                        //Send the array to the queue
                        this._queue.Enqueue(value);
                        //save the start and end times
                        rowValues[arrayCount, 0] = startRow;
                        rowValues[arrayCount, 1] = endRow;
                        //Move the new start row poitner
                        startRow = endRow;
                        arrayCount++;
                        count = 0;
                    }
                    else
                    {
                        Excel.Range toArrayRange = existingMasterWorkSheet.get_Range("A" + startRow, "G" + (endRow - 1));
                        value = (System.Array)toArrayRange.Value2;
                        //Send the array to the queue
                        this._queue.Enqueue(value);
                        //save the start and end times
                        rowValues[arrayCount, 0] = startRow;
                        rowValues[arrayCount, 1] = endRow - 1;
                        //Move the new start row poitner
                        startRow = endRow;
                        arrayCount++;
                        count = 0;
                    }
                }
            }
            return rowValues;
        }

        /// <summary>
        /// This will add a small addiction to the closing operation of the application
        /// Clear he clo file and clean up the memory.
        /// </summary>
        /// <param name="e">Form Closing Event </param>
        /*protected override void OnFormClosing(FormClosingEventArgs e)
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
        }*/

        /// <summary>
        /// When the first + button is clicked
        /// 
        /// Make the new controls appear and extend the frame
        /// </summary>
        /// <param name="sender">a sender object (A controller)</param>
        /// <param name="e"> a helper argument</param>
        private void plusBTN1_Click(object sender, EventArgs e)
        {
            //initalize all components

            if (!plusClicked1)
            {
                //set the clicked flag
                this.plusClicked1 = true;
                this.plusBTN1.Text = "-";
                //Set the divider
                this.lineDivide2.BorderStyle = BorderStyle.Fixed3D;
                this.lineDivide2.AutoSize = false;
                this.lineDivide2.Height = 2;

                //Make them all visable
                this.Height += 95;
                this.Top -= 72;
                this.shift2Label.Visible = true;
                this.lineDivide2.Visible = true;
                this.shiftTime2.Visible = true;
                this.startHour2.Visible = true;
                this.toLabel2.Visible = true;
                this.endHour2.Visible = true;
                this.am_pmCombo2_1.Visible = true;
                this.am_pmCombo2_2.Visible = true;
                this.numberOfShiftsLabel2.Visible = true;
                this.numberOfShiftsCombo2.Visible = true;
                this.plusBTN2.Visible = true;

            }
            else if (plusClicked1)
            {
                //set the clicked flag
                this.plusClicked1 = false;
                this.plusBTN1.Text = "+";

                //Make them all visable
                this.Height -= 95;
                this.Top += 72;
                this.shift2Label.Visible = false;
                this.lineDivide2.Visible = false;
                this.shiftTime2.Visible = false;
                this.startHour2.Visible = false;
                this.toLabel2.Visible = false;
                this.endHour2.Visible = false;
                this.am_pmCombo2_1.Visible = false;
                this.am_pmCombo2_2.Visible = false;
                this.numberOfShiftsLabel2.Visible = false;
                this.numberOfShiftsCombo2.Visible = false;
                this.plusBTN2.Visible = false;
            }
        }

        /// <summary>
        /// When the second + button is clicked
        /// 
        /// Make the new controls appear and extend the frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> a helper argument</param>
        private void plusBTN2_Click_1(object sender, EventArgs e)
        {
            //initalize all components

            if (!plusClicked2)
            {
                //Disable the previous + button
                this.plusBTN1.Enabled = false;
                //set the clicked flag
                this.plusClicked2 = true;
                this.plusBTN2.Text = "-";
                //Set the divider
                this.lineDivide3.BorderStyle = BorderStyle.Fixed3D;
                this.lineDivide3.AutoSize = false;
                this.lineDivide3.Height = 2;

                //Make them all visable
                this.Height += 95;
                this.Top -= 72;
                this.shift3Label.Visible = true;
                this.lineDivide3.Visible = true;
                this.shiftTime3.Visible = true;
                this.startHour3.Visible = true;
                this.toLabel3.Visible = true;
                this.endHour3.Visible = true;
                this.am_pmCombo3_1.Visible = true;
                this.am_pmCombo3_2.Visible = true;
                this.numberOfShiftsLabel3.Visible = true;
                this.numberOfShiftsCombo3.Visible = true;
                this.plusBTN3.Visible = true;

            }
            else if (plusClicked2)
            {
                //Disable the previous + button
                this.plusBTN1.Enabled = true;
                //set the clicked flag
                this.plusClicked2 = false;
                this.plusBTN2.Text = "+";

                //Make them all visable
                this.Height -= 95;
                this.Top += 72;
                this.shift3Label.Visible = false;
                this.lineDivide3.Visible = false;
                this.shiftTime3.Visible = false;
                this.startHour3.Visible = false;
                this.toLabel3.Visible = false;
                this.endHour3.Visible = false;
                this.am_pmCombo3_1.Visible = false;
                this.am_pmCombo3_2.Visible = false;
                this.numberOfShiftsLabel3.Visible = false;
                this.numberOfShiftsCombo3.Visible = false;
                this.plusBTN3.Visible = false;
            }
        }

        /// <summary>
        /// When the second + button is clicked
        /// 
        /// Make the new controls appear and extend the frame
        /// </summary>
        /// <param name="sender"></param>
        private void plusBTN3_Click_1(object sender, EventArgs e)
        {
            if (!plusClicked3)
            {
                //Disable the previous + button
                this.plusBTN2.Enabled = false;
                //set the clicked flag
                this.plusClicked3 = true;
                this.plusBTN3.Text = "-";
                //Set the divider
                this.lineDivide4.BorderStyle = BorderStyle.Fixed3D;
                this.lineDivide4.AutoSize = false;
                this.lineDivide4.Height = 2;


                //Make them all visable
                this.Height += 100;
                this.Top -= 72;
                this.shift4Label.Visible = true;
                this.lineDivide4.Visible = true;
                this.shiftTime4.Visible = true;
                this.startHour4.Visible = true;
                this.toLabel4.Visible = true;
                this.endHour4.Visible = true;
                this.am_pmCombo4_1.Visible = true;
                this.am_pmCombo4_2.Visible = true;
                this.numberOfShiftsLabel4.Visible = true;
                this.numberOfShiftsCombo4.Visible = true;
            }
            else if (plusClicked3)
            {
                //Disable the previous + button
                this.plusBTN2.Enabled = true;
                //set the clicked flag
                this.plusClicked3 = false;
                this.plusBTN3.Text = "+";

                //Make them all visable
                this.Height -= 100;
                this.Top += 72;
                this.shift4Label.Visible = false;
                this.lineDivide4.Visible = false;
                this.shiftTime4.Visible = false;
                this.startHour4.Visible = false;
                this.toLabel4.Visible = false;
                this.endHour4.Visible = false;
                this.am_pmCombo4_1.Visible = false;
                this.am_pmCombo4_2.Visible = false;
                this.numberOfShiftsLabel4.Visible = false;
                this.numberOfShiftsCombo4.Visible = false;
            }
        }

        /// <summary>
        /// Close all open instances of Excel and Garbage collects.
        /// </summary>
        private void Quit()
        {
            if (logoutMasterWorkBook != null)
            {

                logoutMasterWorkBook.Close(false, Type.Missing, Type.Missing);
                logoutMaster.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(logoutMaster);
                logoutMaster = null;
                logoutMasterWorkBook = null;
                logoutMasterWorkSheet = null;
            }

            if (existingMasterWorkBook != null)
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
