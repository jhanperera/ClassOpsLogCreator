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
    public partial class LogCreator : Form
    {
        public LogCreator()
        {
            InitializeComponent();
        }

        private void open_and_setFile(TextBox textBoxname)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel file|*.xlsx";
            openFileDialog.Title = "Select a Excel File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxname.Text = openFileDialog.FileName;
            }
        }

        private void openBTN1_Click(object sender, EventArgs e)
        {
            open_and_setFile(fileTextBox1);
        }
      

        private void openBTN2_Click(object sender, EventArgs e)
        {
            open_and_setFile(fileTextBox2);
        }

        private void openBTN3_Click(object sender, EventArgs e)
        {
            open_and_setFile(fileTextBox3);
        }

        private void clearBTN_Click(object sender, EventArgs e)
        {

        }
    }
}
