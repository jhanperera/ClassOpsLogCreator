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
        public DetailForm(string initString)
        {
            InitializeComponent();

            this.detailLabel.Text = initString;

            //Set the location to the left of the main Window
            int y = (Screen.PrimaryScreen.Bounds.Bottom/2) -  (this.Height/2);
            this.Location = new Point(0, y);
        }

        public void updateDetail(string updateString)
        {
            this.detailLabel.Text = this.detailLabel.Text + Environment.NewLine + updateString;
        }
    }
}
