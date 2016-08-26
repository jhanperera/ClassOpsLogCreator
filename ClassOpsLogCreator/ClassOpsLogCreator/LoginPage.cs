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
    public partial class LoginPage : MetroFramework.Forms.MetroForm
    {
        private bool loginClicked = false;
        private bool canceledClicked = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            if(Properties.Settings.Default.UserName != "" || Properties.Settings.Default.Password != "")
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
                Properties.Settings.Default.UserName = usernameTextBox.Text;
                Properties.Settings.Default.Password = passwordTextBox.Text;
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
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
