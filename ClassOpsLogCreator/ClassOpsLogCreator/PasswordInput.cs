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
    public partial class PasswordInput : MetroForm
    {
        private string adminPassword = "cscoadmin";
        public PasswordInput()
        {
            InitializeComponent();
            this.TopMost = true;

            this.cancelBTN.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Password validation happens here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okBTN_Click(object sender, EventArgs e)
        {
            if(string.Compare(this.passwordTextBox.Text, adminPassword) == 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MetroMessageBox.Show(this, "Invalid Password, please try again.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
