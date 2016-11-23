using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        /// When the electronig Mail Tile is clicked 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void electMailTile_Click(object sender, EventArgs e)
        {
            //make the main panel disapear.
            this.mainPanel.Visible = false;
            //Make the email panel apear.
            this.emailLoginPanel.Visible = true;
            //Ensure the label and text box show up
            this.lotusEmailPassLabel.Visible = false;
            this.lotusEmailPasswordTextBox.Visible = false;
        }

        /// <summary>
        /// When the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lotusEmailTile_Click(object sender, EventArgs e)
        {
            //Make the mail pattern disapear
            this.mainPanel.Visible = false;
            //Make the email panel apear
            this.emailLoginPanel.Visible = true;
            //Ensure the proper labels an text box show up
            this.lotusEmailPassLabel.Visible = true;
            this.lotusEmailPasswordTextBox.Visible = true;
        }

        /// <summary>
        /// When the cancel button is clicked we make this panel disapear
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

        }
    }
}
