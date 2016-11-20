using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Concurrent;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using MetroFramework;
using MetroFramework.Forms;

namespace ClassOpsLogCreator
{
    public partial class LogCreator : MetroForm
    {
        #region Private Attributes/Variables

        public readonly string ROOM_SCHED = @"H:\CS\SHARE-PT\CLASSOPS\clo.xlsm";
        public readonly string JEANNINE_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"H:\CS\SHARE-PT\CLASSOPS\Derek\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG_COPY = @"H:\CS\SHARE-PT\PW\masterlog.xlsx";
        public readonly string EXISTING_MASTER_LOG = @"H:\CS\SHARE-PT\CLASSOPS\masterlog.xlsx";
        public readonly string CLO_GENERATED_LOG = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CLO_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xlsx";
        public readonly string STATS_LOCATION =  @"H:\CS\SHARE-PT\CLASSOPS\Statistics\"; 

        //DEBUG CODE! 
        //ONLY UNCOMMENT FOR LOCAL USE ONLY!
        /*private static string username = Environment.UserName; 
        public readonly string ROOM_SCHED = @"C:\Users\" + username+ @"\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\clo.xlsm";
        public readonly string JEANNINE_LOG = @"C:\Users\" + username + @"\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\Jeannine\Jeannine's log.xlsx";
        public readonly string RAUL_LOG = @"C:\Users\" + username + @"\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\Raul\Raul's Log.xlsx";
        public readonly string DEREK_LOG = @"C:\Users\" + username + @"\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\Derek\Derek's Log.xlsx";
        public readonly string EXISTING_MASTER_LOG_COPY = @"C:\Users\" + username + @"\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\PW\masterlog.xlsx";
        public readonly string EXISTING_MASTER_LOG = @"C:\Users\" + username + @"\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\masterlog.xlsx";
        public readonly string CLO_GENERATED_LOG = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\CLO_" + DateTime.Now.ToString("MM-dd-yyyy") + ".xlsx";
        public readonly string STATS_LOCATION = @"C:\Users\" + username + @"\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\Statistics\";*/

        //A stack for some thread safer operations
        private readonly ConcurrentQueue<System.Array> logNextQueue = new ConcurrentQueue<System.Array>();        
        private readonly ConcurrentStack<System.Array> logNextStack = new ConcurrentStack<System.Array>();
        private readonly ConcurrentStack<System.Array> logPretStack = new ConcurrentStack<System.Array>();

        //These long arrays will contain the start and end rows of the different logs in excel.
        long[,] rowNumbers1 = null;
        long[,] rowNumbers2 = null;
        long[,] rowNumbers3 = null;
        long[,] rowNumbers4 = null;

        //A string array that contains the times of each shift (Separately or all together)
        string[,] shiftTimeArray1 = null;
        string[,] shiftTimeArray2 = null;
        string[,] shiftTimeArray3 = null;
        string[,] shiftTimeArray4 = null;

        //The print dialog object to chose a printer
        PrintDialog printDlg;

        //Detail window to show information to users
        DetailForm detailForm;

        //All the excel elements we use to read and write data from and to.
        private static Excel.Application logoutMaster = null;
        private static Excel.Workbook logoutMasterWorkBook = null;
        private static Excel.Worksheet logoutMasterWorkSheet = null;

        private static Excel.Application existingMaster = null;
        private static Excel.Workbook existingMasterWorkBook = null;
        private static Excel.Worksheet existingMasterWorkSheet = null;
        private static Excel.Worksheet databaseSheet = null;

        //A list of employee Names
        List<string> employeeNames = null;
        List<string> buildingNames = null;

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
        private Boolean plusClicked1 = false;
        private Boolean plusClicked2 = false;
        private Boolean plusClicked3 = false;
        #endregion

        #region Constructor/Load Handlers
        /// <summary>
        /// Constructor for the system. (Changes here should be confirmed with everyone first)
        /// </summary>
        public LogCreator()
        {
            InitializeComponent();

            //Bring this to the font
            this.Activate();

            //Use this for smooth panel updates (double buffering is enabled)
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);

            //Setting the line divide height and auto size settings
            this.lineDivide1.AutoSize = false;
            this.lineDivide1.Height = 2;

            //create the detail from to show information about the system
            detailForm = new DetailForm("Starting Work...");
        }

        /// <summary>
        /// This is the "Load" event for the main window. All on lead activities go in here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogCreator_Load(object sender, EventArgs e)
        {
            //Get the last access times to display during the first message
            string JLastAccess = File.GetLastWriteTime(JEANNINE_LOG).ToString("dd MMMM yyyy - hh:mm tt");
            string RLastAccess = File.GetLastWriteTime(RAUL_LOG).ToString("dd MMMM yyyy - hh:mm tt");
            string DLastAccess = File.GetLastWriteTime(DEREK_LOG).ToString("dd MMMM yyyy - hh:mm tt");

            //A pop up message to ensure that the user is aware that the zone super logs need to be in before running this application
            DialogResult checkMessage = checkMessage = MetroMessageBox.Show(this, "Ensure all Zone logs have been submitted before preceding."
                               + Environment.NewLine + Environment.NewLine +
                               "Jeannine's log was last written to on:  " + JLastAccess + Environment.NewLine +
                               "Raul's log was last written to on:  " + RLastAccess + Environment.NewLine +
                               "Derek's log was last written to on:  " + DLastAccess + Environment.NewLine +
                               Environment.NewLine +
                               "Failure to do so will result in incorrect output being generated",
                               "Important Notice",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1, 250);

            //If the user click cancel we close the application
            if (checkMessage == DialogResult.Cancel)
            {
                //Use an anonymous event handler to take care of this
                this.BeginInvoke(new MethodInvoker(this.Close));
                this.Quit();
                return;
            }

            //Open the existing excel file
            existingMaster = new Excel.Application();
            existingMaster.Visible = false;
            try
            {
                existingMasterWorkBook = existingMaster.Workbooks.Open(EXISTING_MASTER_LOG);
                existingMasterWorkSheet = (Excel.Worksheet)existingMasterWorkBook.Worksheets[1];
                databaseSheet = (Excel.Worksheet)existingMasterWorkBook.Sheets[2];
            }
            catch (Exception)
            {
                Quit();
                return;
            }

            //Get the employee names and building name
            if (employeeNames == null || buildingNames == null)
            {
                // Get the employee names and save it to the employee list
                this.employeeNames = new List<string>();
                this.buildingNames = new List<string>();
                //Extract the name range
                Excel.Range last = databaseSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                int lastRow = databaseSheet.UsedRange.Rows.Count;
                Excel.Range nameRange = databaseSheet.get_Range("A2", "A" + (lastRow));
                Excel.Range buildingRange = databaseSheet.get_Range("C2", "C" + (lastRow));
                //Convert to an array
                System.Array array = (System.Array)nameRange.Cells.Value2;
                System.Array buildingArray = (System.Array)buildingRange.Cells.Value2;

                foreach (string name in array)
                {
                    if (name != null)
                    {
                        employeeNames.Add(name.ToLower());
                    }
                }

                foreach (string building in buildingArray)
                {
                    if (building != null)
                    {
                        buildingNames.Add(building.ToString());
                    }
                }

                last = null;
                nameRange = null;
                buildingRange = null;
            }

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

            //If save changes has been checked before we keep the state of it on this session
            if (Properties.Settings.Default.saveCheckedBoxState == true)
            {
                //Select 1
                this.saveSettingCheckBox.Checked = true;
                this.startHour1.SelectedIndex = Properties.Settings.Default.startHour1;
                this.endHour1.SelectedIndex = Properties.Settings.Default.endHour1;
                this.numberOfShiftsCombo1.SelectedIndex = Properties.Settings.Default.numberOfShiftsCombo1;
                this.am_pmCombo1_1.SelectedIndex = Properties.Settings.Default.am_pmCombo1_1;
                this.am_pmCombo1_2.SelectedIndex = Properties.Settings.Default.am_pmCombo1_2;

                //Select 2
                this.startHour2.SelectedIndex = Properties.Settings.Default.startHour2;
                this.endHour2.SelectedIndex = Properties.Settings.Default.endHour2;
                this.numberOfShiftsCombo2.SelectedIndex = Properties.Settings.Default.numberOfShiftsCombo2;
                this.am_pmCombo2_1.SelectedIndex = Properties.Settings.Default.am_pmCombo2_1;
                this.am_pmCombo2_2.SelectedIndex = Properties.Settings.Default.am_pmCombo2_2;

                //Select 3
                this.startHour3.SelectedIndex = Properties.Settings.Default.startHour3;
                this.endHour3.SelectedIndex = Properties.Settings.Default.endHour3;
                this.numberOfShiftsCombo3.SelectedIndex = Properties.Settings.Default.numberOfShiftsCombo3;
                this.am_pmCombo3_1.SelectedIndex = Properties.Settings.Default.am_pmCombo3_1;
                this.am_pmCombo3_2.SelectedIndex = Properties.Settings.Default.am_pmCombo3_2;

                //Select 4
                this.startHour4.SelectedIndex = Properties.Settings.Default.startHour4;
                this.endHour4.SelectedIndex = Properties.Settings.Default.endHour4;
                this.numberOfShiftsCombo4.SelectedIndex = Properties.Settings.Default.numberOfShiftsCombo4;
                this.am_pmCombo4_1.SelectedIndex = Properties.Settings.Default.am_pmCombo4_1;
                this.am_pmCombo4_2.SelectedIndex = Properties.Settings.Default.am_pmCombo4_2;
            }
            //If the settings aren't 
            else
            {
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

                
            }

        }
        #endregion

        #region Public Methods
        public List<string> getBuildingNames()
        {
            return this.buildingNames;
        }
        #endregion

        #region Button Operations

        /// <summary>
        /// When the user clicks the "Create" Button this is what will happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBTN_Click_1(object sender, EventArgs e)
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
                    MetroMessageBox.Show(this,"Valid start and end time must be set.",
                                     "Oops...there was a problem.",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1) ||
                        Convert.ToDateTime(startTimeFromCombo2) >= Convert.ToDateTime(endTimeFromCombo2))
                {
                    MetroMessageBox.Show(this, "Valid start and end time must be set.",
                                     "Oops...there was a problem",
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
                    MetroMessageBox.Show(this, "Valid start and end time must be set.",
                                     "Oops...there was a problem.",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1) ||
                        Convert.ToDateTime(startTimeFromCombo2) >= Convert.ToDateTime(endTimeFromCombo2) ||
                        Convert.ToDateTime(startTimeFromCombo3) >= Convert.ToDateTime(endTimeFromCombo3))
                {
                    MetroMessageBox.Show(this, "Valid start and end time must be set.",
                                      "Oops...there was a problem.",
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
                    MetroMessageBox.Show(this, "Valid start and end time must be set.",
                                      "Oops...there was a problem.",
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
                    MetroMessageBox.Show(this, "Valid start and end time must be set.",
                                      "Oops...there was a problem.",
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
                    MetroMessageBox.Show(this, "Valid start and end time must be set.",
                                     "Oops...there was a problem.",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1))
                {
                    MetroMessageBox.Show(this, "Valid start and end time must be set.",
                                     "Oops...there was a problem.",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button1);
                    return;
                }

            }
            /************************************END OF INPUT VALIDATION***********************/

            //Create the background worker
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
                //Check if we have the detailForm open
                if(!detailForm.Visible)
                {
                    if(detailForm.IsDisposed)
                    {
                        detailForm = new DetailForm("Starting Work...");
                    }
                    //If not we open it
                    detailForm.Show();
                }        

                //Throw this window into the background.
                this.TopMost = false;

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
        /// This will open a login page so we can get the users MyMail login
        /// for the email scanner class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsBTN_Click(object sender, EventArgs e)
        {
             SettingForm settings = new SettingForm(this);
             settings.ShowDialog();           
        }

        /// <summary>
        /// When the first + button is clicked
        /// 
        /// Make the new controls appear and extend the frame
        /// </summary>
        /// <param name="sender">a sender object (A controller)</param>
        /// <param name="e"> a helper argument</param>
        private void plusBTN1_Click_1(object sender, EventArgs e)
        {
            //initialize all components

            if (!plusClicked1)
            {
                //set the clicked flag
                this.plusClicked1 = true;
                this.plusBTN1.Text = "-";
                //Set the divider
                //this.lineDivide2.BorderStyle = BorderStyle.Fixed3D;
                this.lineDivide2.AutoSize = false;
                this.lineDivide2.Height = 2;

                //Make them all visible
                this.Height += 100;
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

                //Make them all visible
                this.Height -= 100;
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
        private void plusBTN2_Click(object sender, EventArgs e)
        {
            //initialize all components

            if (!plusClicked2)
            {
                //Disable the previous + button
                this.plusBTN1.Enabled = false;
                //set the clicked flag
                this.plusClicked2 = true;
                this.plusBTN2.Text = "-";
                //Set the divider
                //this.lineDivide3.BorderStyle = BorderStyle.Fixed3D;
                this.lineDivide3.AutoSize = false;
                this.lineDivide3.Height = 2;

                //Make them all visible
                this.Height += 100;
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

                //Make them all visible
                this.Height -= 100;
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
        /// <param name="e"></param>
        private void plusBTN3_Click(object sender, EventArgs e)
        {
            if (!plusClicked3)
            {
                //Disable the previous + button
                this.plusBTN2.Enabled = false;
                //set the clicked flag
                this.plusClicked3 = true;
                this.plusBTN3.Text = "-";
                //Set the divider
                //this.lineDivide4.BorderStyle = BorderStyle.Fixed3D;
                this.lineDivide4.AutoSize = false;
                this.lineDivide4.Height = 2;


                //Make them all visible
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

                //Make them all visible
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
        #endregion

        #region All Thread Related Work

        /// <summary>
        /// All log (tab1) work is done in this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //Sender to send info to progress bar
            var worker = sender as BackgroundWorker;

            worker.ReportProgress(10);

            //Create the new Excel file where we will store all the new information
            logoutMaster = new Excel.Application();
            logoutMasterWorkBook = logoutMaster.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            this.logCreationAndExcelWriter(1, startTimeFromCombo1, endTimeFromCombo1, numberOfShifts1, true, ref rowNumbers1, worker);

            //If the first plus button is clicked
            if (plusClicked1)
            {
                worker.ReportProgress(20);
                //Add a new worksheet to add the new 
                logoutMasterWorkBook.Sheets.Add(After: logoutMasterWorkBook.Sheets[logoutMasterWorkBook.Sheets.Count]);
                this.logCreationAndExcelWriter(2, startTimeFromCombo2, endTimeFromCombo2, numberOfShifts2, false, ref rowNumbers2, worker);

                //If the second plus button is clicked
                if (plusClicked2)
                {
                    worker.ReportProgress(30);
                    //Add a new worksheet to add the new 
                    logoutMasterWorkBook.Sheets.Add(After: logoutMasterWorkBook.Sheets[logoutMasterWorkBook.Sheets.Count]);
                    this.logCreationAndExcelWriter(3, startTimeFromCombo3, endTimeFromCombo3, numberOfShifts3, false, ref rowNumbers3, worker);

                    //If the third plus button is clicked
                    if (plusClicked3)
                    {
                        worker.ReportProgress(40);
                        //Add a new worksheet to add the new 
                        logoutMasterWorkBook.Sheets.Add(After: logoutMasterWorkBook.Sheets[logoutMasterWorkBook.Sheets.Count]);
                        this.logCreationAndExcelWriter(4, startTimeFromCombo4, endTimeFromCombo4, numberOfShifts4, false, ref rowNumbers4, worker);
                    }
                }
            }
           
            //Gracefully close all instances
            //Quit();

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
            if (e.ProgressPercentage == 10)
            {
                this.statusText.Text = "Working";
                this.detailForm.updateDetail("Preparing shift #1 files.");
            }
            else if(e.ProgressPercentage == 11)
            {
                this.detailForm.updateDetail("Importing the room schedule from: " + ROOM_SCHED);
            }
            else if (e.ProgressPercentage == 12)
            {
                this.detailForm.updateDetail("Successfully imported room schedule and Crestron Logout times.");
            }
            else if (e.ProgressPercentage == 13)
            {
                this.detailForm.updateDetail("Importing Zone Supervisor logs from: " + Environment.NewLine +
                                               JEANNINE_LOG + Environment.NewLine + DEREK_LOG + Environment.NewLine
                                               + RAUL_LOG);
            }
            else if (e.ProgressPercentage == 14)
            {
                this.detailForm.updateDetail("Successfully imported Zone Supervisor logs.");
            }
            else if (e.ProgressPercentage == 15)
            {
                this.detailForm.updateDetail("Sorting the events into zones.");
                this.detailForm.updateDetail("Generating PT-Staff logs.");
            }
            else if (e.ProgressPercentage == 16)
            {
                this.detailForm.updateDetail("Successfully generated PT-Staff logs.");
            }
            else if (e.ProgressPercentage == 17)
            {
                this.detailForm.updateDetail("Writing logs to the master file: " + EXISTING_MASTER_LOG);
            }
            else if (e.ProgressPercentage == 18)
            {
                this.detailForm.updateDetail("Successfully wrote logs to the master file.");
            }
            else if (e.ProgressPercentage == 20)
            {
                this.detailForm.updateDetail("Preparing shift #2 files.");
            }
            else if (e.ProgressPercentage == 30)
            {
                this.detailForm.updateDetail("Preparing shift #3 files.");
            }
            else if (e.ProgressPercentage == 40)
            {
                this.detailForm.updateDetail("Preparing shift #4 files.");
            }
            else if (e.ProgressPercentage > 95)
            {
                this.statusText.Text = "Done";
            }
            else
            {
                this.statusText.Text = "";
                this.detailForm.updateDetail("");
            }
            //this.workProgressBar.Value = e.ProgressPercentage;
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
                MetroMessageBox.Show(this, e.Error.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.detailForm.updateDetail("Error! - " + e.Error.Message);
                this.statusText.Text = "Error!";
                createBTN.Enabled = true;
                plusBTN1.Enabled = true;
                plusBTN2.Enabled = true;
                plusBTN3.Enabled = true;

                Quit();
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Canceled
                // flag may not have been set, even though
                // CancelAsync was called.
                this.detailForm.updateDetail("Canceled!");
                this.statusText.Text = "Canceled!";
                createBTN.Enabled = true;
                plusBTN1.Enabled = true;
                plusBTN2.Enabled = true;
                plusBTN3.Enabled = true;

                Quit();
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                printDlg = new PrintDialog();
                PrinterSettings defaultSettings = new PrinterSettings();
                string defaultPrinterName = defaultSettings.PrinterName;

                if (existingMaster == null)
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
                        //file not found
                        Quit();
                        throw new System.FieldAccessException("File not found!");
                    }
                }
              
                detailForm.updateDetail("Displaying Logs...");
                
                //Display all the logs
                if (plusClicked1 && !plusClicked2 && !plusClicked3)
                {
                    //Display the logs 
                    displayLogs(this.startTimeFromCombo1,  this.endTimeFromCombo1, rowNumbers1, numberOfShifts1, "Shift #1:", ref this.shiftTimeArray1);
                    displayLogs(this.startTimeFromCombo2,  this.endTimeFromCombo2, rowNumbers2, numberOfShifts2, "Shift #2:", ref this.shiftTimeArray2);

                    //Print the logs
                    if (printDlg.ShowDialog() == DialogResult.OK)
                    {
                        //Hide the windows.
                        this.Visible = false;
                        this.detailForm.Visible = false;

                        //Print out the logs
                        printOutLog(this.shiftTimeArray1, rowNumbers1, numberOfShifts1);
                        printOutLog(this.shiftTimeArray2, rowNumbers2, numberOfShifts2);

                        //Show the windows.
                        this.Visible = true;
                        this.detailForm.Visible = true;
                    }

                }
                else if (plusClicked1 && plusClicked2 && !plusClicked3)
                {
                    //Display the logs                                
                    displayLogs(this.startTimeFromCombo1, this.endTimeFromCombo1, rowNumbers1, numberOfShifts1, "Shift #1:", ref this.shiftTimeArray1);
                    displayLogs(this.startTimeFromCombo2, this.endTimeFromCombo2, rowNumbers2, numberOfShifts2, "Shift #2:", ref this.shiftTimeArray2);
                    displayLogs(this.startTimeFromCombo3, this.endTimeFromCombo3, rowNumbers3, numberOfShifts3, "Shift #3:", ref this.shiftTimeArray3);

                    //Print the logs
                    if (printDlg.ShowDialog() == DialogResult.OK)
                    {
                        //Hide the windows.
                        this.Visible = false;
                        this.detailForm.Visible = false;

                        //Print out the logs.
                        printOutLog(this.shiftTimeArray1, rowNumbers1, numberOfShifts1);
                        printOutLog(this.shiftTimeArray2, rowNumbers2, numberOfShifts2);
                        printOutLog(this.shiftTimeArray3, rowNumbers3, numberOfShifts3);

                        //Show the windows.
                        this.Visible = true;
                        this.detailForm.Visible = true;
                    }
                }
                else if (plusClicked1 && plusClicked2 && plusClicked3)
                {
                    //Display the logs                                                          
                    displayLogs(this.startTimeFromCombo1, this.endTimeFromCombo1, rowNumbers1, numberOfShifts1, "Shift #1:", ref this.shiftTimeArray1);
                    displayLogs(this.startTimeFromCombo2, this.endTimeFromCombo2, rowNumbers2, numberOfShifts2, "Shift #2:", ref this.shiftTimeArray2);
                    displayLogs(this.startTimeFromCombo3, this.endTimeFromCombo3, rowNumbers3, numberOfShifts3, "Shift #3:", ref this.shiftTimeArray3);
                    displayLogs(this.startTimeFromCombo4, this.endTimeFromCombo4, rowNumbers4, numberOfShifts4, "Shift #4:", ref this.shiftTimeArray4);

                    if (printDlg.ShowDialog() == DialogResult.OK)
                    {
                        //Hide the windows.
                        this.Visible = false;
                        this.detailForm.Visible = false;

                        //Print out the logs
                        printOutLog(this.shiftTimeArray1, rowNumbers1, numberOfShifts1);
                        printOutLog(this.shiftTimeArray2, rowNumbers2, numberOfShifts2);
                        printOutLog(this.shiftTimeArray3, rowNumbers3, numberOfShifts3);
                        printOutLog(this.shiftTimeArray4, rowNumbers4, numberOfShifts4);

                        //Show the windows.
                        this.Visible = true;
                        this.detailForm.Visible = true;
                    }
                }
                else
                {
                    //Display the logs 
                    displayLogs(this.startTimeFromCombo1, this.endTimeFromCombo1, rowNumbers1, numberOfShifts1, "Shift #1:", ref this.shiftTimeArray1);

                    //Print the logs
                    if (printDlg.ShowDialog() == DialogResult.OK)
                    {
                        //Hide the windows.
                        this.Visible = false;
                        this.detailForm.Visible = false;

                        //Print out the logs
                        printOutLog(this.shiftTimeArray1, rowNumbers1, numberOfShifts1);

                        //Show the windows.
                        this.Visible = true;
                        this.detailForm.Visible = true;
                    }
                }

                //Save and close the excel application
                existingMaster.DisplayAlerts = false;
                existingMasterWorkBook.SaveAs(EXISTING_MASTER_LOG);
                existingMasterWorkBook.Close();

                //Reset the default printer and close the print dialog
                SetDefaultPrinter(defaultPrinterName);
                printDlg.Dispose();

                //Make a copy of the excel file
                System.IO.File.Delete(EXISTING_MASTER_LOG_COPY);
                System.IO.File.Copy(EXISTING_MASTER_LOG, EXISTING_MASTER_LOG_COPY, true);
                //Make a new copied file not hidden
                System.IO.File.SetAttributes(EXISTING_MASTER_LOG_COPY, System.IO.FileAttributes.Hidden);

                //Bring this form to the front
                this.Activate();

                detailForm.updateDetail("Saving and cleaning up background processes.");

                //Quit
                Quit();

                //Check if we have a username or password
                if(Properties.Settings.Default.UserName != "" && Properties.Settings.Default.Password != "")
                {
                    ScheduleStatsGen SSG = new ScheduleStatsGen(this, detailForm);
                    SSG = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

                detailForm.updateDetail("Done!");

                //Enable the button again
                createBTN.Enabled = true;
                plusBTN1.Enabled = true;
                plusBTN2.Enabled = true;
                plusBTN3.Enabled = true;
            }
        }
        #endregion

        #region Helper/Worker Methods

        /// <summary>
        /// This method will create the logs and write to the excel file with assistance 
        /// from other helper methods
        /// </summary>
        /// <param name="worksheetNumber"></param>
        /// <param name="startTimeFromCombo"></param>
        /// <param name="endTimeFromCombo"></param>
        /// <param name="numberOfShifts"></param>
        /// <param name="redSeperator"></param>
        /// <param name="rowNumbers"></param>
        private void logCreationAndExcelWriter(int worksheetNumber, string startTimeFromCombo, string endTimeFromCombo, int numberOfShifts, bool redSeperator, ref long[,] rowNumbers, BackgroundWorker worker)
        {
            //Open up a new worksheet
            logoutMasterWorkSheet = (Excel.Worksheet)logoutMasterWorkBook.Worksheets[worksheetNumber];

            worker.ReportProgress(11); //Importing Room Schedule

            //Get the logout from the clo
            LogoutLogImporter classRoomTimeLogs = new LogoutLogImporter(this, startTimeFromCombo, endTimeFromCombo, new ClassInfo(this.buildingNames));
           
            string[,] arrayClassRooms = classRoomTimeLogs.getLogOutArray();

            worker.ReportProgress(12); //Complete importing room schedule

            worker.ReportProgress(13); //Importing Zone Super log events

            //Get all the zone super events
            ZoneSuperLogImporter ZoneLogs = new ZoneSuperLogImporter(this, startTimeFromCombo, endTimeFromCombo, ref arrayClassRooms);

                      //Get the three logs
            string[,] JInstruction = ZoneLogs.getJeannineLog();
            string[,] DInstruction = ZoneLogs.getDerekLog();
            string[,] RInstruction = ZoneLogs.getRaulLog();

            worker.ReportProgress(14); //Complete Importing Zone Super log events

            worker.ReportProgress(15); //Generating logs

            //write all the data to the excel file
            //merge the all the data together into the master log
            WriteLogOutArray(logoutMasterWorkSheet, arrayClassRooms, classRoomTimeLogs.getLogOutArrayCount(),
                                                                         JInstruction, DInstruction, RInstruction,
                                                                         true, startTimeFromCombo, endTimeFromCombo);

            worker.ReportProgress(16); //Complete generating logs

            logoutMaster.DisplayAlerts = false;

            worker.ReportProgress(17); //Writing and sorting logs into the master file

            //Merge all the data with the existing excel workbook
            this.mergeMasterWithExisting(logoutMasterWorkSheet, numberOfShifts, redSeperator, startTimeFromCombo, endTimeFromCombo, ref rowNumbers);

            worker.ReportProgress(18); //Complete Writing and sorting of logs into the master file.
        }

        /// <summary>
        /// This method will write our arrays to the excel file.
        /// This method generates the Excel output via the arrays
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="values"></param>
        /// <param name="index"></param>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <param name="array3"></param>
        /// <param name="includeACE"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        private static void WriteLogOutArray(Excel.Worksheet worksheet, string[,] values, int index,
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

            //Format for easy to read for "Crestron logout"
            taskType_range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            taskType_range.ColumnWidth = 20;
            taskType_range.Value2 = "Crestron Logout";

            //Format for east reading of the date
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

            //Add ACE017 to the log if we have are in the time period
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

            //Clean up the Excel range objects
            taskType_range = null;
            date_range = null;
            value_range = null;
            logRange1 = null;
            logRange2 = null;
            logRange3 = null;
            ace017CloseRange = null;
            allDataRange = null;
        }

        /// <summary>
        /// This method will merger our file with the already existing file in sorted order. 
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="numberOfShifts"></param>
        /// <param name="redSeperator"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="rowNumbers"></param>
        public void mergeMasterWithExisting(Excel.Worksheet worksheet, int numberOfShifts, bool redSeperator, string startTime, string endTime, ref long[,] rowNumbers)
        {
            if (existingMaster == null)
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
                    //file not found
                    Quit();
                    throw new System.FieldAccessException("File not found!");
                }
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
                SchoolZoning sz = new SchoolZoning(buildingNames);
                //Pass the zoning with the number of shifts
                destinationRange.Value2 = sz.generateZonedLog(range, numberOfShifts);
                //Get the number of rows
                int[] numberOfRowsPerZone = sz.numberOfRows();
                //divide the zones
                rowNumbers = this.dividedLogs(destinationRange, numberOfShifts, numberOfRowsPerZone);
            }
            else
            {
                //Set the destination value to the range value
                destinationRange.Value2 = range.Value2;

                //We open the log viewer at this point
                Excel.Range last = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

                //Send the range into the Stack so its thread safe
                System.Array destinationArray = (System.Array)destinationRange.Cells.Value2;

                //Save the rows and push into the stack
                rowNumbers = new long[,] { { lastRowDestination + 2, last.Row } };
                this.logNextQueue.Enqueue(destinationArray);
            }

            //Get the new last row
            Excel.Range last_row = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

            //High light all the other/pickup/demo/setup rows
            Color redBackground = Color.FromArgb(255, 199, 206);
            Color redFont = Color.FromArgb(156, 0, 6);
            Excel.Range task_range = existingMasterWorkSheet.get_Range("B" + (lastRowDestination + 2), "B" + (last_row.Row));
            task_range.WrapText = true;
            //High light all the cells that have lapel mics
            Color lightblue = Color.FromArgb(225, 246, 255);
            Excel.Range instuciton_range = existingMasterWorkSheet.get_Range("G" + (lastRowDestination + 2), "G" + (last_row.Row));
            foreach (Excel.Range cell in instuciton_range)
            {
                if (cell.Value2 is string &&  (string)cell.Value2.ToString().Trim() != "")
                {
                    cell.Interior.Color = lightblue;
                    Excel.Range task_color_change = existingMasterWorkSheet.get_Range("B" + cell.Row, "B" + cell.Row);
                    task_color_change.Interior.Color = lightblue;
                }
            }

            foreach (Excel.Range cell in task_range)
            {
                if (cell.Value2 is string && (string)cell.Value2 != "Crestron Logout")
                {
                    cell.Interior.Color = redBackground;
                    cell.Font.Color = redFont;
                    Excel.Range task_color_change = existingMasterWorkSheet.get_Range("G" + cell.Row, "G" + cell.Row);
                    task_color_change.Interior.Color = redBackground;
                    task_color_change.Font.Color = redFont;
                }
            }
            
            //Clean the range items
            range = null;
            dividerRange = null;
            destinationRange = null;
            last_row = null;
            task_range = null;
            instuciton_range = null;

            //Save
            existingMaster.DisplayAlerts = false;
            existingMasterWorkBook.SaveAs(EXISTING_MASTER_LOG);
        }


        /// <summary>
        /// This method is used to set the default printer when printing is called on the logs
        /// 
        /// This method accesses the users systems and sets the default printer to what is passed in
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
        /// employee working. This puts the logs into the stack 
        /// 
        /// This returns an array with the start row and the end row of each log
        /// array[i,j] where i is the starting row and j is the ending row
        /// 
        /// THIS METHOD ALSO ADDS THE LOG TO THE QUEUE! 
        /// </summary>
        /// <param name="range"></param>
        /// <param name="numberOfShifts"></param>
        /// <param name="numberOfRowsPerZone"></param>
        /// <returns></returns>
        private long[,] dividedLogs(Excel.Range range, int numberOfShifts, int[] numberOfRowsPerZone)
        {
            //Set the start and end row variables
            System.Array value = null;
            //Save the row values 
            long[,] rowValues = new long[numberOfShifts, 2];
            //The starting row
            long startRow = Int64.Parse(range.Row.ToString());
            
            for(int i = 0; i <= numberOfRowsPerZone.GetUpperBound(0); i++)
            {
                //get the range to add to the queue
                Excel.Range toArrayRange = existingMasterWorkSheet.get_Range("A" + startRow, "G" + (startRow + numberOfRowsPerZone[i]));
                value = (System.Array)toArrayRange.Value2;
                //Send the array to the Queue
                this.logNextQueue.Enqueue(value);

                //Save the row numbers in the array
                rowValues[i, 0] = startRow;
                rowValues[i, 1] = startRow + numberOfRowsPerZone[i];
                //Move to the next starting point
                startRow += numberOfRowsPerZone[i] + 1;
            }
            return rowValues;
        }

        /// <summary>
        /// This also accounts for the previous and next button being clicked
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="rowNumbers"></param>
        /// <param name="numberOfShifts"></param>
        /// <param name="shiftTitle"></param>
        private void displayLogs(string startTime, string endTime, long[,] rowNumbers, int numberOfShifts, string shiftTitle, ref string[,] timeArray)
        {
            int i = 0;
            timeArray = new string[numberOfShifts, 2];

            System.Array destinationArray = null;
            if (numberOfShifts == 1)
            {
                //Dequeue from the Queue and send the item to the log viewer
                if (this.logNextQueue.TryDequeue(out destinationArray))
                {
                    //Display the log viewer
                    LogViewer lv = new LogViewer(destinationArray, startTime, endTime, numberOfShifts, numberOfShifts, employeeNames, shiftTitle);
                    lv.ShowDialog();
                    //Set the employee name if the next button is clicked.
                    Excel.Range name_range = existingMasterWorkSheet.get_Range("A" + (rowNumbers[0, 0]), "A" + (rowNumbers[0, 1]));
                    name_range.Value2 = lv.getEmployeeName();
                    timeArray[i,0] = lv.getStartTime();
                    timeArray[i,1] = lv.getEndTime();
                    i++;
                }
            }
            else
            {
                int indexCount = 0; 
                int shiftCounter = 1;
                //Dequeue from the Queue and send the item to the log viewer
                while (indexCount <= rowNumbers.GetUpperBound(0) && this.logNextQueue.TryDequeue(out destinationArray))
                {
                    //Display the log viewer
                    LogViewer lv = new LogViewer(destinationArray, startTime, endTime, shiftCounter, numberOfShifts, employeeNames, shiftTitle);
                    lv.ShowDialog();
                    //Set the employee name
                    if (lv.isNextClicked())
                    {
                        //Put this log into the previous stack 
                        this.logPretStack.Push(destinationArray);
                        Excel.Range name_range = existingMasterWorkSheet.get_Range("A" + (rowNumbers[indexCount, 0]), "A" + (rowNumbers[indexCount, 1]));
                        name_range.Value2 = lv.getEmployeeName();
                        timeArray[i, 0] = lv.getStartTime();
                        timeArray[i, 1] = lv.getEndTime();
                        i++;
                        indexCount++;
                        shiftCounter++;
                    }
                    //If the previous button is clicked
                    else if(lv.isPreviousClicked() && shiftCounter > 1)
                    {                        
                        //Push the current destination array
                        this.logNextStack.Push(destinationArray);
                        while(this.logPretStack.TryPop(out destinationArray))
                        {
                            indexCount--;
                            shiftCounter--;
                            i--;
                            //Display the log viewer
                            lv = new LogViewer(destinationArray, timeArray[i, 0], timeArray[i, 1], shiftCounter, numberOfShifts, employeeNames, shiftTitle);
                            lv.ShowDialog();
                            if (lv.isNextClicked() && !this.logNextStack.IsEmpty)
                            {
                                //Put this log into the previous Stack 
                                this.logPretStack.Push(destinationArray);
                                Excel.Range name_range = existingMasterWorkSheet.get_Range("A" + (rowNumbers[indexCount, 0]), "A" + (rowNumbers[indexCount, 1]));
                                name_range.Value2 = lv.getEmployeeName();
                                timeArray[i, 0] = lv.getStartTime();
                                timeArray[i, 1] = lv.getEndTime();
                                i++;
                                indexCount++;
                                shiftCounter++;
                                //Try to pop from the next stack 
                                while (this.logNextStack.TryPop(out destinationArray))
                                {
                                    //display the log
                                    lv = new LogViewer(destinationArray, startTime, endTime, shiftCounter, numberOfShifts, employeeNames, shiftTitle);
                                    lv.ShowDialog();
                                    //If next is clicked we continue on the next stack and save
                                    if(lv.isNextClicked())
                                    {
                                        this.logPretStack.Push(destinationArray);
                                        name_range = existingMasterWorkSheet.get_Range("A" + (rowNumbers[indexCount, 0]), "A" + (rowNumbers[indexCount, 1]));
                                        name_range.Value2 = lv.getEmployeeName();
                                        timeArray[i, 0] = lv.getStartTime();
                                        timeArray[i, 1] = lv.getEndTime();
                                        i++;
                                        indexCount++;
                                        shiftCounter++;
                                    }
                                    //If previous is clicked we break out and go to previous sack
                                    if(lv.isPreviousClicked())
                                    {
                                        break;
                                    }
                                }  
                            }
                            //If the next stack is empty we need to break out
                            if(lv.isNextClicked() && this.logNextStack.IsEmpty)
                            {
                                break;
                            }
                            //Push it into the next stack if we have room in it.
                            this.logNextStack.Push(destinationArray);  
                        }
                    }         
                }
            }

            //Empty the stacks
            this.logNextStack.Clear();
            this.logPretStack.Clear();
        }

        /// <summary>
        /// This method will print out all the logs that have been named.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="numberOfShifts"></param>
        /// <param name="rowNumbers"></param>
        private void printOutLog(string[,] timeArray, long[,] rowNumbers, int numberOfShifts)
        {
            existingMaster.get_Range("C:C").EntireColumn.Hidden = true;
            //Print all the pages here
            if (numberOfShifts > 1)
            {
                SetDefaultPrinter(printDlg.PrinterSettings.PrinterName);
                existingMaster.Visible = true;

                 for (int i = 0; i <= rowNumbers.GetUpperBound(0) && (rowNumbers[i, 0] != 0 || rowNumbers[i, 1] != 0); i++)
                 {
                    Excel.Range logRange = existingMasterWorkSheet.get_Range("B" + (rowNumbers[i, 0]), "H" + (rowNumbers[i, 1]));
                    Excel.Range name = existingMasterWorkSheet.get_Range("A" + rowNumbers[i, 0]);

                    string nameText;
                    if (name.Cells.Value2 == null)
                    {
                        nameText = "null";
                    }
                    else
                    {
                       nameText = name.Cells.Value2.ToString();
                    }

                    existingMasterWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&20" + nameText + ", " + timeArray[i,0] + " to " + timeArray[i,1];
                    existingMasterWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&20&KFF0000" + DateTime.Now.ToString("ddd");
                    existingMasterWorkSheet.PageSetup.RightHeader = existingMasterWorkSheet.PageSetup.RightHeader + "&\"Calibri,Bold\"&20&K000000" + DateTime.Now.ToString(", MMM dd, yyyy");
                    logRange.PrintPreview(true);

                    logRange = null;
                    name = null;
                }
            }
            else
            {
                //We open the log viewer at this point
                Excel.Range last = existingMasterWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                Excel.Range logRange = existingMasterWorkSheet.get_Range("B" + (rowNumbers[0, 0]), "H" + (rowNumbers[0, 1]));


                Excel.Range name = existingMasterWorkSheet.get_Range("A" + (rowNumbers[0, 0]));
                string nameText = name.Cells.Value2.ToString();
            
                existingMasterWorkSheet.PageSetup.CenterHeader = "&\"Calibri,Bold\"&20" + nameText + ", " + timeArray[0, 0] + " to " + timeArray[0, 1];
                existingMasterWorkSheet.PageSetup.RightHeader = "&\"Calibri,Bold\"&20&KFF0000" + DateTime.Now.ToString("ddd");
                existingMasterWorkSheet.PageSetup.RightHeader = existingMasterWorkSheet.PageSetup.RightHeader + "&\"Calibri,Bold\"&20&K000000" + DateTime.Now.ToString(", MMM dd, yyyy");

                SetDefaultPrinter(printDlg.PrinterSettings.PrinterName); 
                existingMaster.Visible = true;
                logRange.PrintPreview(true);

                last = null;
                logRange = null;        
            }
            existingMaster.get_Range("C:C").EntireColumn.Hidden = false;
        }
        #endregion

        #region Cleanup/Closing Methods

        /// <summary>
        /// This will save all settings on closing
        /// </summary>
        /// <param name="e">Form Closing Event </param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            this.Quit();
            //We are going to use the base onFormClose operations and add more
            base.OnFormClosing(e);

            //If "Saved Selected" is check we are going to save the state of all the settings
            if (this.saveSettingCheckBox.Checked)
            {
                //Save the state of the check box
                Properties.Settings.Default.saveCheckedBoxState = true;

                //Save for select 1
                Properties.Settings.Default.startHour1 = this.startHour1.SelectedIndex;
                Properties.Settings.Default.endHour1 = this.endHour1.SelectedIndex;
                Properties.Settings.Default.numberOfShiftsCombo1 = this.numberOfShiftsCombo1.SelectedIndex;
                Properties.Settings.Default.am_pmCombo1_1 = this.am_pmCombo1_1.SelectedIndex;
                Properties.Settings.Default.am_pmCombo1_2 = this.am_pmCombo1_2.SelectedIndex;

                //Save for select 2
                Properties.Settings.Default.startHour2 = this.startHour2.SelectedIndex;
                Properties.Settings.Default.endHour2 = this.endHour2.SelectedIndex;
                Properties.Settings.Default.numberOfShiftsCombo2 = this.numberOfShiftsCombo2.SelectedIndex;
                Properties.Settings.Default.am_pmCombo2_1 = this.am_pmCombo2_1.SelectedIndex;
                Properties.Settings.Default.am_pmCombo2_2 = this.am_pmCombo2_2.SelectedIndex;

                //Save for select 3
                Properties.Settings.Default.startHour3 = this.startHour3.SelectedIndex;
                Properties.Settings.Default.endHour3 = this.endHour3.SelectedIndex;
                Properties.Settings.Default.numberOfShiftsCombo3 = this.numberOfShiftsCombo3.SelectedIndex;
                Properties.Settings.Default.am_pmCombo3_1 = this.am_pmCombo3_1.SelectedIndex;
                Properties.Settings.Default.am_pmCombo3_2 = this.am_pmCombo3_2.SelectedIndex;

                //Save for select 4
                Properties.Settings.Default.startHour4 = this.startHour4.SelectedIndex;
                Properties.Settings.Default.endHour4 = this.endHour4.SelectedIndex;
                Properties.Settings.Default.numberOfShiftsCombo4 = this.numberOfShiftsCombo4.SelectedIndex;
                Properties.Settings.Default.am_pmCombo4_1 = this.am_pmCombo4_1.SelectedIndex;
                Properties.Settings.Default.am_pmCombo4_2 = this.am_pmCombo4_2.SelectedIndex;

            }
            else
            {
                //Reset the check-box flag
                Properties.Settings.Default.saveCheckedBoxState = false;
                //Reset all the settings
                Properties.Settings.Default.Reset();
            }
            //Save settings to the xml file
            Properties.Settings.Default.Save();
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
                existingMaster.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(existingMaster);
                existingMaster = null;
                existingMasterWorkBook = null;
                existingMasterWorkSheet = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //TEST CODE!
            //If we still have open instance of excel lets force close
            /*System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }*/
            //TEST CODE!
        }
        #endregion
    }
}
