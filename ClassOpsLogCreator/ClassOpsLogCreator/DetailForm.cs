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
    public partial class DetailForm : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// Create the detail window with the initString
        /// </summary>
        /// <param name="initString"></param>
        public DetailForm(string initString)
        {
            InitializeComponent();

            this.detailTextBox.Text = initString;

            //Set the location to the left of the main Window
            int y = (Screen.PrimaryScreen.Bounds.Bottom/2) -  (this.Height/2);
            this.Location = new Point(0, y);
        }

        /// <summary>
        /// Send a string that will be written to the Detail window
        /// </summary>
        /// <param name="updateString"></param>
        public void updateDetail(string updateString)
        {
            string time = DateTime.Now.ToString("H:mm:ss");
            this.detailTextBox.AppendText(Environment.NewLine +
                                    "(" + time + ") " + updateString);
            this.Invalidate();
            this.Update();
            this.Refresh();
            Application.DoEvents();
        }
    }
}
