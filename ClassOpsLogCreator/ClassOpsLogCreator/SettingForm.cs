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
    public partial class SettingForm : MetroFramework.Forms.MetroForm
    {
        private bool loginClicked = false;
        private bool canceledClicked = false;
        private LogCreator mainForm;

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
            if (!Properties.Settings.Default.UserName.Equals("")||!Properties.Settings.Default.Password.Equals(""))
            {
                this.usernameTextBox.Text = Properties.Settings.Default.UserName;
                this.passwordTextBox.Text = Properties.Settings.Default.Password;
            }
        }

        #region Radio button event handlers

        /// <summary>
        /// yearlyRadio event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void yearlyRadio_CheckedChanged(object sender, EventArgs e)
        {
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
            if(emailLoginTab.SelectedIndex == 4)
            {
                PasswordInput passInput = new PasswordInput();
                passInput.ShowDialog(this);
                if(passInput.DialogResult == DialogResult.OK)
                {
                    //Make all controls visible.
                }
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

            if (this.usernameTextBox.Text == "" || this.passwordTextBox.Text == "")
            {
                MetroMessageBox.Show(this, "Please provide a User name and Password.");
                return;
            }
            else
            {
                this.loginClicked = true;
                Properties.Settings.Default.UserName = this.usernameTextBox.Text;
                Properties.Settings.Default.Password = this.passwordTextBox.Text;
                Properties.Settings.Default.Save();
                EmailSender eS = new EmailSender(true);
                if(eS.isConnectionMade())
                {
                    MetroMessageBox.Show(this, "Success: A connection was made", "Success",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MetroMessageBox.Show(this, "FAIL: A connection was unable to be established", "Problem....",
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
            /*string startTimeFromCombo1 = this.startHour1.GetItemText(this.startHour1.SelectedItem)
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

            /*Cursor.Current = Cursors.WaitCursor;

            LogoutLogImporter classRoomTimeLogs = new LogoutLogImporter(this.mainForm, startTimeFromCombo1, endTimeFromCombo1);

            string[,] arrayClassRooms = classRoomTimeLogs.getLogOutArray();

            createBTN.Enabled = true;
            Cursor.Current = Cursors.Default;*/

        }

        /// <summary>
        /// Generate and send statistics according to the given date and time.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateBTN_Click(object sender, EventArgs e)
        {
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

            }
            //Generate the stats for the month.
            else if(monthlyRadio.Checked)
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
               
            }
            else
            {
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

            StatsGen statGenerator = new StatsGen(this.mainForm, this.startDate, this.endDate);
            return;
        }

        #endregion

    }
}
