using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class displays the settings in the admin version. 
    /// 
    /// Settings for changing building names, adding/removing employees. etc.
    /// </summary>
    public partial class SettingForm : MetroFramework.Forms.MetroForm
    {
        //Excel variables 
        Excel.Application masterlog = null;
        Excel.Workbook masterWorkBook = null;
        Excel.Worksheet dataBaseSheet = null;
        Excel.Range last = null;
        Excel.Range buildingRange = null;
        Excel.Range employeeRange = null;

        System.Array buildingArray = null;
        System.Array employeeArray = null;

        private bool loginClicked = false;
        private bool canceledClicked = false;
        private LogCreator mainForm;

        private string statsFileToOpen = null;

        //Use a background worker to allow the GUI to still be functional and not hang.
        private static BackgroundWorker bw = null;

        //Start and end time that are picked
        private DateTime startDate;
        private DateTime endDate;


        /// <summary>
        /// Constructor
        /// </summary>
        public SettingForm(LogCreator MainForm)
        {
            InitializeComponent();

            this.mainForm = MainForm;

            //Add event handlers
            this.emailLoginTab.SelectedIndexChanged += MetroTabControl1_SelectedIndexChanged;
            this.weeklyRadio.CheckedChanged += new EventHandler(weeklyRadio_CheckedChanged);
            this.monthlyRadio.CheckedChanged += new EventHandler(monthlyRadio_CheckedChanged);
            this.yearlyRadio.CheckedChanged += new EventHandler(yearlyRadio_CheckedChanged);
            

            //Get last weeks start and end date.
            DateTime date = DateTime.Now.AddDays(-7);
            while (date.DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(-1);
            }
            DateTime startDateNow = date;
            DateTime endOfWeek = date.AddDays(6);

            //Set the min and max date for the picker
            dateTimePicker.MinDate = new DateTime(2016, 9, 1);
            dateTimePicker.MaxDate = endOfWeek;

            //Fill the combo boxes
            for (int i = 1; i <= 12; i++)
            {
                //Select 1
                this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = "00" });
                //15 minute intervals
                for (int k = 15; k <= 45; k += 15)
                {
                    //Select 1
                    this.startHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                    this.endHour1.Items.Add(new TimeItem { Hour = i.ToString(), Minute = k.ToString() });
                }
            }

            //Fill the am/pm selector
            this.am_pmCombo1_1.Items.Add("AM");
            this.am_pmCombo1_1.Items.Add("PM");
            this.am_pmCombo1_2.Items.Add("AM");
            this.am_pmCombo1_2.Items.Add("PM");

            //set the default view for the combo for tab 1
            this.startHour1.SelectedIndex = -1;
            this.endHour1.SelectedIndex = -1;
            this.am_pmCombo1_1.SelectedIndex = 1;
            this.am_pmCombo1_2.SelectedIndex = 1;

            //Set the version number
            this.versionLabel.Text += Application.ProductVersion;

            //Fill the password and user name field if we already have a user name and password saved.
            if (!Properties.Settings.Default.UserName.Equals("") || !Properties.Settings.Default.Password.Equals(""))
            {
                this.usernameTextBox.Text = Properties.Settings.Default.UserName;
                this.passwordTextBox.Text = Properties.Settings.Default.Password;
            }

            //Fill in the gmail and box
            this.gmailUsernameTextBox.Text = Properties.Settings.Default.gmailUserName;
            this.gmailPasswordTextBox.Text = Properties.Settings.Default.gmailPassword;
        }

        #region Radio button event handlers

        /// <summary>
        /// yearlyRadio event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void yearlyRadio_CheckedChanged(object sender, EventArgs e)
        {
            //hide the weekend checkbox
            this.weekendCheckBox.Visible = false;

            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "yyyy";
            selectorLabel.Text = "Select a year:";

        }

        /// <summary>
        /// monthlyRadio event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthlyRadio_CheckedChanged(object sender, EventArgs e)
        {
            //hide the weekend checkbox
            this.weekendCheckBox.Visible = false;

            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "MM/yyyy";
            selectorLabel.Text = "Select a month:";
        }

        /// <summary>
        /// weeklyRadio event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weeklyRadio_CheckedChanged(object sender, EventArgs e)
        {
            //show the weekend checkbox
            this.weekendCheckBox.Visible = true;

            dateTimePicker.Format = DateTimePickerFormat.Long;
            selectorLabel.Text = "Select a day:";
        }

        #endregion

        #region Public status click events

        /// <summary>
        /// Return if the login button is clicked
        /// </summary>
        /// <returns></returns>
        public bool isLoginClicked()
        {
            return this.loginClicked;
        }

        /// <summary>
        /// Return if the cancel is clicked
        /// </summary>
        /// <returns></returns>
        public bool isCanceledClicked()
        {
            return this.canceledClicked;
        }

        #endregion

        #region Admin Controls
        /// <summary>
        /// Check if we change the tab, if we change to tab 3 then we ask for a password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If the 4th tab is selected we present them with a password dialog
            if (emailLoginTab.SelectedIndex == 4)
            {
                masterlog = new Excel.Application();
                masterWorkBook = null;
                dataBaseSheet = null;
                masterlog.Visible = false;

                try
                {
                    //This should look for the file
                    masterWorkBook = masterlog.Workbooks.Open(mainForm.EXISTING_MASTER_LOG);
                    //Work in worksheet number 1
                    dataBaseSheet = (Excel.Worksheet)masterWorkBook.Sheets[2];

                }
                catch (Exception)
                {
                    this.Quit();
                    GC.WaitForPendingFinalizers();
                    throw new System.FieldAccessException("File not found!");
                }

                //Extract the name range
                last = dataBaseSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                int lastRow = dataBaseSheet.UsedRange.Rows.Count;


                if (buildingRange == null || employeeRange == null)
                {
                    buildingRange = dataBaseSheet.get_Range("C1", "C" + (lastRow));
                    employeeRange = dataBaseSheet.get_Range("A1", "A" + (lastRow));
                    //Convert to an array
                    buildingArray = (System.Array)buildingRange.Cells.Value2;
                    employeeArray = (System.Array)employeeRange.Cells.Value2;
                }
                //Clear the combo boxes
                buildingComboBox.Items.Clear();
                employeeComboBox.Items.Clear();
                //Add the buildings and names to the drop down mean
                foreach (object s in buildingArray)
                {
                    if(s != null && s.ToString() != "TEL")
                    {
                        this.buildingComboBox.Items.Add(s.ToString());
                    }               
                }

                foreach(object s in employeeArray)
                {
                    if(s != null)
                    {
                        this.employeeComboBox.Items.Add(s.ToString());
                    }
                }

                //Select the default value to display 
                this.buildingComboBox.SelectedIndex = 0;
                this.employeeComboBox.SelectedIndex = 0;

                //Make the save button visible.
                this.updateBTN.Visible = true;
                this.addBTN.Visible = true;
                this.removeBTN.Visible = true;
            }
        }
        #endregion

        #region Button Operations

        /// <summary>
        /// When the user clicked login. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginBTN_Click(object sender, EventArgs e)
        {
            if (this.usernameTextBox.Text == "" || this.passwordTextBox.Text == ""
                || this.gmailUsernameTextBox.Text == "" || this.gmailPasswordTextBox.Text == "")
            {
                MetroMessageBox.Show(this, "Please provide a User name and Password.");
                return;
            }
            else
            {
                this.loginClicked = true;

                Properties.Settings.Default.gmailUserName = this.gmailUsernameTextBox.Text;
                Properties.Settings.Default.gmailPassword = this.gmailPasswordTextBox.Text;

                Properties.Settings.Default.UserName = this.usernameTextBox.Text;
                Properties.Settings.Default.Password = this.passwordTextBox.Text;

                Properties.Settings.Default.Save();

                //start up test cases
                EmailSender eS = new EmailSender(true);
                EmailScanner eScanner = new EmailScanner(true);

                if (eS.isConnectionMade() && eScanner.isConnected())
                {
                    MetroMessageBox.Show(this, "Success: A connection was sucessfully made.", "Success",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MetroMessageBox.Show(this, "FAIL: A connection was unable to be established. Please check your login credentials and try again.", "Problem....",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
 
        }

        /// <summary>
        /// If the cancel button is closed we will close the login form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBTN_Click(object sender, EventArgs e)
        {
            this.canceledClicked = true;
            this.Close();
        }

        /// <summary>
        /// This will create the logout just for
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createBTN_Click(object sender, EventArgs e)
        {
            //createBTN.Enabled = false;
            /******************************INPUT VALIDATION********************************************/
            //Get the times set by the first set of combo boxes
            string startTimeFromCombo1 = this.startHour1.GetItemText(this.startHour1.SelectedItem)
                                 + "" + this.am_pmCombo1_1.GetItemText(this.am_pmCombo1_1.SelectedItem);
            string endTimeFromCombo1 = this.endHour1.GetItemText(this.endHour1.SelectedItem)
                                 + "" + this.am_pmCombo1_2.GetItemText(this.am_pmCombo1_2.SelectedItem);

            //Input Error checking!
            if (startTimeFromCombo1.Equals("PM") || startTimeFromCombo1.Equals("AM") || startTimeFromCombo1 == null ||
                endTimeFromCombo1.Equals("PM") || endTimeFromCombo1.Equals("AM") || endTimeFromCombo1 == null)
            {
                MessageBox.Show("Valid time must be set.",
                                 "Problem...",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button1);

                createBTN.Enabled = true;
                return;
            }
            else if (Convert.ToDateTime(startTimeFromCombo1) >= Convert.ToDateTime(endTimeFromCombo1))
            {
                MessageBox.Show("Valid time must be set.",
                                 "Problem...",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button1);

                createBTN.Enabled = true;
                return;
            }
            /****************************END INPUT VALIDATION********************************************/
            
            //Set the cursor to waiting
            Cursor.Current = Cursors.WaitCursor;

            ClassInfo classInfo = new ClassInfo(this.mainForm.getBuildingNames());

            //Get all the times from the logout importer
            LogoutLogImporter classRoomTimeLogs = new LogoutLogImporter(this.mainForm, startTimeFromCombo1, endTimeFromCombo1, classInfo);

            //Save all the data to the an array
            string[,] arrayClassRooms = classRoomTimeLogs.getLogOutArray();

            //Create the new excel file 
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                //Try and open the workbook and access the fist worksheet
                wb = excelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                ws = (Excel.Worksheet)wb.Worksheets[1];
            }
            catch (Exception)
            {
                throw new Exception("Unable to start Excel");
            }

            //Don't show it.
            excelApp.Visible = false;
            //Get the range we want to write the information to
            Excel.Range saveRange = (Excel.Range)ws.get_Range("A1", "D" + (arrayClassRooms.GetLength(0)));
            //Save the values into the range
            saveRange.Value2 = arrayClassRooms;

            //Sorting it by time column
            dynamic allDataRange = ws.UsedRange;
            allDataRange.Sort(allDataRange.Columns[1], Excel.XlSortOrder.xlAscending);

            //Close up, save, and cleanup.
            excelApp.DisplayAlerts = false;
            wb.SaveAs(mainForm.CLO_GENERATED_LOG);
            wb.Close();
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelApp);
            excelApp = null;
            wb = null;
            ws = null;
            saveRange = null;
            classRoomTimeLogs = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //Set the buttons back to normal
            createBTN.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Generate and send statistics according to the given date and time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateBTN_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Generate the stats for the week.
            if (weeklyRadio.Checked)
            {
                //get Monday and Friday of a selected week.
                DateTime date = this.dateTimePicker.Value.Date;
                while (date.DayOfWeek != DayOfWeek.Monday)
                {
                    date = date.AddDays(-1);
                }

                //Start of selected week and end of the given week.
                startDate = date;
                endDate = date.AddDays(4);

                if (this.weekendCheckBox.Checked)
                {
                    MetroMessageBox.Show(this, "Please note that if the selected week has no weekend work or the data is not in chronological order in the masterlog.xlsx the output will be incorrect!", "Alert!");
                    startDate = startDate.AddDays(-1);
                    endDate = endDate.AddDays(1);
                }

            }
            //Generate the stats for the month.
            else if (monthlyRadio.Checked)
            {
                var holidays = new List<DateTime> {/* list of observed holidays */};
                var i = DateTime.DaysInMonth(dateTimePicker.Value.Date.Year, dateTimePicker.Value.Date.Month);
                while (i > 0)
                {
                    var dtCurrent = new DateTime(dateTimePicker.Value.Date.Year, dateTimePicker.Value.Date.Month, i);
                    if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                     !holidays.Contains(dtCurrent))
                    {
                        endDate = dtCurrent;
                        i = 0;
                    }
                    else
                    {
                        i--;
                    }
                }

                var j = 1;
                while (j < 7)
                {
                    var dtCurrent = new DateTime(dateTimePicker.Value.Date.Year, dateTimePicker.Value.Date.Month, j);
                    if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                     !holidays.Contains(dtCurrent))
                    {
                        startDate = dtCurrent;
                        j = 8;
                    }
                    else
                    {
                        j++;
                    }

                }

            }
            //Generate the stats for the year.
            else
            {
                var holidays = new List<DateTime> {/* list of observed holidays */};
                var i = DateTime.DaysInMonth(dateTimePicker.Value.Date.Year, 12);
                while (i > 0)
                {
                    var dtCurrent = new DateTime(dateTimePicker.Value.Date.Year, 12, i);
                    if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                     !holidays.Contains(dtCurrent))
                    {
                        endDate = dtCurrent;
                        i = 0;
                    }
                    else
                    {
                        i--;
                    }
                }

                var j = 1;
                while (j < 7)
                {
                    var dtCurrent = new DateTime(dateTimePicker.Value.Date.Year, 1, j);
                    if (dtCurrent.DayOfWeek < DayOfWeek.Saturday && dtCurrent.DayOfWeek > DayOfWeek.Sunday &&
                     !holidays.Contains(dtCurrent))
                    {
                        startDate = dtCurrent;
                        j = 8;
                    }
                    else
                    {
                        j++;
                    }

                }

            }

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
                //Disable the button
                generateBTN.Enabled = false;

                //Run the work
                bw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// This will save the settings if we want to change a building name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBTN_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //First check if the building text box has text in it. 
            if(!string.IsNullOrWhiteSpace(newBuildingInit.Text.ToString()))
            {
                //Get the index of the selected building
                int selected = this.buildingComboBox.SelectedIndex;
                //Building was not selected correctly
                if(selected == 0)
                {
                    MetroMessageBox.Show(this, "Please select a building!", "Invalid Building",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //We get the index of the building we need to change
                    Excel.Range changeBuilding = (Excel.Range)dataBaseSheet.get_Range("C" + (selected + 1), "C" + (selected + 1));
                    //Save the building name for further references
                    string previousBuildingName = changeBuilding.Value2.ToString();
                    //Update the building name with the new one
                    changeBuilding.Value2 = this.newBuildingInit.Text.ToString().ToUpper();
                    //Save and inform the user that the save was successful
                    masterWorkBook.Save();
                    MetroMessageBox.Show(this, previousBuildingName + " was successfully changed to " + this.newBuildingInit.Text.ToString().ToUpper(),
                                    "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    changeBuilding = null;

                    //Update the comobox again
                    int lastRow = dataBaseSheet.UsedRange.Rows.Count;
                    buildingRange = dataBaseSheet.get_Range("C1", "C" + (lastRow));
                    //Convert to an array
                    buildingArray = (System.Array)buildingRange.Cells.Value2;

                    //Clear the combo box
                    this.buildingComboBox.Items.Clear();
                    this.newBuildingInit.Clear();

                    //Add the buildings and names to the drop down mean
                    foreach (object s in buildingArray)
                    {
                        if (s != null)
                        {
                            this.buildingComboBox.Items.Add(s.ToString());
                        }
                    }

                    //Select the default value to display 
                    this.buildingComboBox.SelectedIndex = 0;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Add the new staff name into the excel file database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBTN_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Check if the current
            if (!string.IsNullOrWhiteSpace(newEmployeeNameTextBox.Text.ToString()))
            {
                //Get the last row number
                int lastRow = dataBaseSheet.UsedRange.Rows.Count;
                //Get the name range to add the new employee
                Excel.Range addNameRange = (Excel.Range)dataBaseSheet.get_Range("A" + (lastRow + 1), "A" + (lastRow + 1));
                //Parse the input and ensure it ends up in a name format. First letter is capital. 
                string newName = this.newEmployeeNameTextBox.Text.First().ToString().ToUpper() +
                                        this.newEmployeeNameTextBox.Text.Substring(1).ToLower();

                //Check if the employee is already in the database. (NO DUPLICATES) 
                foreach(object s in employeeArray)
                {
                    if (s != null && s.ToString() == newName)
                    {
                        MetroMessageBox.Show(this, newName + " ready exists ", "Problem...",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                //If no duplicates then we add the name to the database
                addNameRange.Value2 = newName;
                //Save
                masterWorkBook.Save();
                //Inform the user the save was successful. 
                MetroMessageBox.Show(this, newName + " was successfully added. ", "Success!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                

                //Update the comobox again
                lastRow = dataBaseSheet.UsedRange.Rows.Count;
                addNameRange = dataBaseSheet.get_Range("A1", "A" + (lastRow));

                //Convert to an array
                System.Array nameArray = (System.Array)addNameRange.Cells.Value2;

                //Clear the combo box
                this.employeeComboBox.Items.Clear();

                //Add the buildings and names to the drop down mean
                foreach (object s in nameArray)
                {
                    if (s != null)
                    {
                        this.employeeComboBox.Items.Add(s.ToString());
                    }
                }

                //Select the default value to display 
                this.employeeComboBox.SelectedIndex = 0;

                addNameRange = null;
            }
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Remove staff from the excel file database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeBTN_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //Get the selected index
            int selected = this.employeeComboBox.SelectedIndex;

            //If we are in the first one we throw a error message
            if(selected == 0)
            {
                MetroMessageBox.Show(this, "Please select a employee!", "Invalid Employee",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //get the name from the selected position
                Excel.Range nameToDelete = (Excel.Range)dataBaseSheet.get_Range("A" + (selected + 1), "A" + (selected + 1));
                //save it for now
                string oldName = nameToDelete.Value2.ToString();
                //Delete and move it up
                nameToDelete.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);

                //Save the data
                masterlog.DisplayAlerts = false;
                masterWorkBook.Save();

                //Display the sucessful message
                MetroMessageBox.Show(this, oldName + " was successfully added. ", "Success!",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameToDelete = null;

                //Get the last row number
                int lastRow = dataBaseSheet.UsedRange.Rows.Count;

                //Refresh the employee combo box. 
                employeeRange = dataBaseSheet.get_Range("A1", "A" + (lastRow));
                //Convert to an array
                employeeArray = (System.Array)employeeRange.Cells.Value2;

                //Update the combo box
                employeeComboBox.Items.Clear();
                foreach (object s in employeeArray)
                {
                    if (s != null)
                    {
                        this.employeeComboBox.Items.Add(s.ToString());
                    }
                }

                //Select the default value to display 
                this.employeeComboBox.SelectedIndex = 0;

                employeeRange = null;
            }
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Thread work

        /// <summary>
        /// A work complete method to update files and send out messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                Cursor.Current = Cursors.Default;
                MetroMessageBox.Show(this, e.Error.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                generateBTN.Enabled = true;
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Canceled
                // flag may not have been set, even though
                // CancelAsync was called.
                generateBTN.Enabled = true;
                Cursor.Current = Cursors.Default;

            }
            else
            {
                Cursor.Current = Cursors.Default;
                //Open the pdf file and move the setting window to the back
                System.Diagnostics.Process.Start(mainForm.STATS_LOCATION + statsFileToOpen);
                this.SendToBack();
                generateBTN.Enabled = true;
            }
        }

        /// <summary>
        /// Progress change method to update the detail window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            return;
        }

        /// <summary>
        /// Do the work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            try
            {
                StatsGen statGenerator = new StatsGen(this.mainForm, this.startDate, this.endDate, "Manual");
                statsFileToOpen = statGenerator.getfileName();
                statGenerator = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception)
            {
                return;
            }
            return;
        }

        #endregion

        #region closing and clean up operations

        /// <summary>
        /// Some more added cleanup when the application is closed via the x button
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Cursor.Current = Cursors.Default;

            this.Quit();

            //We are going to use the base onFormClose operations and add more
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Quit and clean up. Garbage collect as well.
        /// </summary>
        private void Quit()
        {
            Cursor.Current = Cursors.Default;
            //close and garbage collect 
            if (masterWorkBook != null)
            {
                masterWorkBook.Close(false, Type.Missing, Type.Missing);
                masterlog.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(masterlog);
                masterlog = null;
                masterWorkBook = null;
                dataBaseSheet = null;
                last = null;
                buildingRange = null;
                employeeRange = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion

    }
}
