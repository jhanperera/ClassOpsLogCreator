using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpsLogCreator
{
    public partial class LogCreator : Form
    {
        //Create our Excel objects
        private static Excel.Application Excel1 = null;
        private static Excel.Application Excel2 = null;
        private static Excel.Application Excel3 = null;

        //The work books for those excel objects
        private static Excel.Workbook MyBook1 = null;
        private static Excel.Workbook MyBook2 = null;
        private static Excel.Workbook MyBook3 = null;

        //The sheets for those excel work books.
        private static Excel.Worksheet MySheet1 = null;
        private static Excel.Worksheet MySheet2 = null;
        private static Excel.Worksheet MySheet3 = null;

        public LogCreator()
        {
            InitializeComponent();
        }

        //Open a file dialog and set the text box with the file path
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

        //call open_and_setFile() and create the excel components.
        private void openBTN1_Click(object sender, EventArgs e)
        {
            open_and_setFile(fileTextBox1);
            Excel1 = new Excel.Application();
            Excel1.Visible = false;
            MyBook1 = Excel1.Workbooks.Open(fileTextBox1.Text);
            MySheet1 = MyBook1.Sheets[1];
            
        }
      
        private void openBTN2_Click(object sender, EventArgs e)
        {
            open_and_setFile(fileTextBox2);
            Excel2 = new Excel.Application();
            Excel2.Visible = false;
            MyBook2 = Excel2.Workbooks.Open(fileTextBox2.Text);
            MySheet2 = MyBook2.Sheets[1];
        }

        private void openBTN3_Click(object sender, EventArgs e)
        {
            open_and_setFile(fileTextBox3);
            Excel3 = new Excel.Application();
            Excel3.Visible = false;
            MyBook3 = Excel3.Workbooks.Open(fileTextBox3.Text);
            MySheet3 = MyBook3.Sheets[1];
        }

        private void clearBTN_Click(object sender, EventArgs e)
        {
            MyBook1.SaveAs("Text.pdf");
            MyBook1.Close();
        }

    }
}
