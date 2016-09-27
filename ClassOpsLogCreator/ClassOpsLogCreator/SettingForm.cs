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

namespace ClassOpsLogCreator
{
    public partial class SettingForm : MetroFramework.Forms.MetroForm
    {
        private bool loginClicked = false;
        private bool canceledClicked = false;
        private LogCreator mainForm;

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingForm(LogCreator MainForm)
        {
            InitializeComponent();

            this.mainForm = MainForm;

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
                this.Close();
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
    }
}
