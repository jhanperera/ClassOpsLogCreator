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
    public partial class LogViewer : Form
    {
 
        private System.Array rangeArray = null;
        private string startTime = null;
        private string endTime = null;

        //A lock object to lock this thread from being accessed accross memory
        private Object thisLock = new Object();
        private bool done = false;

        /// <summary>
        /// Constructor for the log viewer
        /// </summary>
        public LogViewer(System.Array Range, string StartTime, string EndTime)
        {
            InitializeComponent();

            //Get the array represenation of the range
            this.rangeArray = Range;
            //Start and end times
            this.startTime = StartTime;
            this.endTime = EndTime;
        }

        /// <summary>
        /// This return the employee name that was entered into the text field
        /// </summary>
        /// <returns></returns>
        public string getEmployeeName()
        {
            return this.nameTextBox.Text.ToString();
        }

        /// <summary>
        /// The main form load event all the work will happen here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogViewer_Load(object sender, EventArgs e)
        {
            //Set the time label for the shift time
            this.timeLabel.Text = this.startTime + " to " + this.endTime;
            
            //Use a data table to store all the data and then apply it to the datagrid view
            DataTable dt = new DataTable();
            dt.Columns.Add("Task Type");
            dt.Columns.Add("Date(MM/DD/YYYY)");
            dt.Columns.Add("Time");
            dt.Columns.Add("Building");
            dt.Columns.Add("Room");
            dt.Columns.Add("Special Instructions/Comments");

            int Cnum = 0;
            int Rnum = 0;

            //Lock the thread so we don't get a cross thread issue 
            if(!done)
            {
                lock (thisLock)
                {
                    if(!done)
                    {
                        //Add all the elements in the range to the datatable
                        for (Rnum = 1; Rnum <= rangeArray.GetUpperBound(0); Rnum++)
                        {
                            DataRow dr = dt.NewRow();
                            for (Cnum = 2; Cnum <= rangeArray.GetUpperBound(1); Cnum++)
                            {
                                DateTime temp;
                                //Reading in null values
                                if (rangeArray.GetValue(Rnum, Cnum) == null)
                                {
                                    dr[Cnum - 2] = "";
                                }
                                //Formatting the time from excel to be correct
                                else if ((Cnum - 1) == 2 && (!DateTime.TryParse((rangeArray.GetValue(Rnum, Cnum).ToString()), out temp)))
                                {
                                    dr[Cnum - 2] = DateTime.FromOADate(double.Parse(rangeArray.GetValue(Rnum, Cnum).ToString())).ToString("M/dd/yyyy");
                                }
                                //everything else
                                else
                                {
                                    dr[Cnum - 2] = rangeArray.GetValue(Rnum, Cnum).ToString().Trim();
                                }
                            }
                            //Add the row to the the data table
                            dt.Rows.Add(dr);
                            //Accept the changes
                            dt.AcceptChanges();
                        }
                        done = true;
                    }
                }
            }
            
            //Set the datagrid data source to the dataTable
            dataGridView1.DataSource = dt;

            //Format the datagrid to look like the excel file
            this.format_DataGirdView();

            //Clear the default selected
            dataGridView1.ClearSelection();
        }

        /// <summary>
        /// All the formatting of the datagrid view will go here
        /// This includes sizing and color of all the special cells
        /// </summary>
        private void format_DataGirdView()
        {
            //Set some color formats
            Color redBackground = Color.FromArgb(255, 199, 206);
            Color redFont = Color.FromArgb(156, 0, 6);
            Color lightblue = Color.FromArgb(225, 246, 255);
            Color headerText = Color.FromArgb(156, 101, 0);
            Color headerBackcolor = Color.FromArgb(235, 241, 222);


            //Increase the width of the last columns
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 75;
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].Width = 75;
            dataGridView1.Columns[5].Width = 360;

            //Enable text wraping
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //Allight to the center and format 
            foreach(DataGridViewColumn col in dataGridView1.Columns)
            {
                //Disable sorting
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

                //Format the headers
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                col.HeaderCell.Style.Font = new Font("Calibri", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
                col.HeaderCell.Style.BackColor = headerBackcolor;
                col.HeaderCell.Style.ForeColor = headerText;

                //Center the column text
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            //Color the cells accordingly
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if(dataGridView1.Rows[i].Cells[0].Value.ToString() != "Crestron Logout")
                {
                    //Background
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = redBackground;
                    dataGridView1.Rows[i].Cells[5].Style.BackColor = redBackground;
                    //Font
                    dataGridView1.Rows[i].Cells[0].Style.ForeColor = redFont;
                    dataGridView1.Rows[i].Cells[5].Style.ForeColor = redFont;
                }
                //Change the color of the neck mic tasks
                if(dataGridView1.Rows[i].Cells[5].Value.ToString() == "Ensure neck mic goes back to equipment drawer.")
                {
                    //Background
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = lightblue;
                    dataGridView1.Rows[i].Cells[5].Style.BackColor = lightblue;
                }
            }

            //Do not accept the system style
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        /// <summary>
        /// When the next button is clicked we close the current window and return
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextBTN_Click(object sender, EventArgs e)
        {
            //INPUT VALIDATION!
            if(this.nameTextBox.Text == "" || this.nameTextBox.Text == null)
            {
                MessageBox.Show("Text box cannot be empty!");
            }
            else
            {
                this.Close();
            }          
        }

        private void printBTN_Click(object sender, EventArgs e)
        {
              
        }
    }
}
