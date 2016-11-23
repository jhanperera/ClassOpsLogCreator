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
    /// <summary>
    /// This from will launch to ensure we chose the correct login process for reading and sending emails
    /// </summary>
    public partial class InitialEmailLoginForm : MetroForm
    {

        private bool isLotus = false;
        /// <summary>
        /// Creates the Windows Form to ask the email questions
        /// </summary>
        public InitialEmailLoginForm()
        {
            InitializeComponent();
            this.electMailTile.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            this.lotusEmailTile.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
        }

        /// <summary>
        /// When the electronic Mail Tile is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void electMailTile_Click(object sender, EventArgs e)
        {
            //make the main panel disappear.
            this.mainPanel.Visible = false;
            //Make the email panel appear.
            this.emailLoginPanel.Visible = true;
            //Ensure the label and text box show up
            this.lotusEmailPassLabel.Visible = false;
            this.lotusEmailPasswordTextBox.Visible = false;
            this.isLotus = false;
        }

        /// <summary>
        /// When the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lotusEmailTile_Click(object sender, EventArgs e)
        {
            //Make the mail pattern disappear
            this.mainPanel.Visible = false;
            //Make the email panel appear
            this.emailLoginPanel.Visible = true;
            //Ensure the proper labels an text box show up
            this.lotusEmailPassLabel.Visible = true;
            this.lotusEmailPasswordTextBox.Visible = true;
            this.isLotus = true;
        }

        /// <summary>
        /// When the cancel button is clicked we make this panel disappear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBTN_Click(object sender, EventArgs e)
        {
            this.emailLoginPanel.Visible = false;
            this.mainPanel.Visible = true;
        }

        /// <summary>
        /// When the login button is clicked we need to check the settings are okay and save/close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectBTN_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //We are in the lotus notes segments Try to connect
            if(lotusEmailPasswordTextBox.Visible == true)
            {
                Properties.Settings.Default.UserName = this.emailUserNameTextBox.Text;
                Properties.Settings.Default.Password = this.electronicEmailPasswordTextBox.Text;
                Properties.Settings.Default.lotusPassword = this.lotusEmailPasswordTextBox.Text;
                Properties.Settings.Default.isLotusAccount = true;
                Properties.Settings.Default.Save();
                //Test incoming connection to notes
                EmailScanner ES = new EmailScanner(true);
                EmailSender ESend = new EmailSender(true);
                if (ES.isConnected() && ESend.isConnectionMade())
                {
                    MetroMessageBox.Show(this, "Success: A connection was made to the email server", "Success!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.Save();
                    this.Dispose();

                }
                else
                {
                    MetroMessageBox.Show(this, "FAIL: A connection was unable to be established to the email server. Please check your login credentials", "Problem....",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Properties.Settings.Default.UserName = this.emailUserNameTextBox.Text;
                Properties.Settings.Default.Password = this.electronicEmailPasswordTextBox.Text;
                Properties.Settings.Default.isLotusAccount = false;
                Properties.Settings.Default.Save();
                //Test incoming connection to notes
                EmailScanner ES = new EmailScanner(true);
                EmailSender ESend = new EmailSender(true);
                if (ES.isConnected() && ESend.isConnectionMade())
                {
                    MetroMessageBox.Show(this, "Success: A connection was made to the email server", "Success!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.Save();
                    this.Dispose();
                    
                }
                else
                {
                    MetroMessageBox.Show(this, "FAIL: A connection was unable to be established to the email server. Please check your login credentials", "Problem....",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }             

            }
            Cursor.Current = Cursors.Default;
        }
    }
}
