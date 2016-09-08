using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassOpsLogCreator
{
    public partial class SettingForm : MetroFramework.Forms.MetroForm
    {
        private bool loginClicked = false;
        private bool canceledClicked = false;
        private Form mainForm;

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingForm(Form MainForm)
        {
            InitializeComponent();

            this.mainForm = MainForm;

            //Set the version number
            this.versionLabel.Text += Application.ProductVersion;

            //Fill the password and username field if we already have a username and password saved.
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
                MessageBox.Show("Please provide a User name and Password.");
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
    }
}
