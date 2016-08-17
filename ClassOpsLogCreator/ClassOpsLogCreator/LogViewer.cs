using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace ClassOpsLogCreator
{
    public partial class LogViewer : Form
    {
 
        //Private variables to hold some important vales
        private System.Array rangeArray = null;
        private string startTime = null;
        private string endTime = null;
        private int shiftNumber = 0;
        private int numberOfShifts = 0;

        //Flags for when the buttons are clicked
        private bool previousClicked = false;
        private bool nextClicked = false;

        //List of employees
        List<string> employeeNameList;

        //A lock object to lock this thread from being accessed across memory
        private Object thisLock = new Object();
        private bool done = false;

        /// <summary>
        /// Constructor for the log viewer
        /// </summary>
        public LogViewer(System.Array Range, string StartTime, string EndTime, int ShiftNumber, int NumberOfShifts, List<string> EmployeeNameList)
        {
            InitializeComponent();

            this.nameTextBox.ForeColor = SystemColors.GrayText;
            this.nameTextBox.Text = "Name";
            this.nameTextBox.Leave += new System.EventHandler(this.nameTextBox_Leave);
            this.nameTextBox.Enter += new System.EventHandler(this.nameTextBox_Enter);

            //Get the array representation of the range
            this.rangeArray = Range;
            //Start and end times
            this.startTime = StartTime;
            this.endTime = EndTime;
            //Number of shifts
            this.shiftNumber = ShiftNumber;
            this.numberOfShifts = NumberOfShifts;
            //EmployeeList
            this.employeeNameList = EmployeeNameList;
        }

        /// <summary>
        /// This return the employee name that was entered into the text field
        /// </summary>
        /// <returns></returns>
        public string getEmployeeName()
        {
            if (this.nameTextBox.Text == null)
                return null;

            if (this.nameTextBox.Text.Length > 1)
                return char.ToUpper(this.nameTextBox.Text[0]) + this.nameTextBox.Text.Substring(1);

            return this.nameTextBox.Text.ToUpper();
        }

        /// <summary>
        /// Return whether the previous button was clicked
        /// </summary>
        /// <returns></returns>
        public bool isPreviousClicked()
        {
            return this.previousClicked;
        }

        /// <summary>
        /// Return whether the next button was clicked
        /// </summary>
        /// <returns></returns>
        public bool isNextClicked()
        {
            return this.nextClicked;
        }

        /// <summary>
        /// The main form load event all the work will happen here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogViewer_Load(object sender, EventArgs e)
        {
            if(this.numberOfShifts == 1)
            {
                this.previousBTN.Enabled = false;
            }

            //Set the labels
            this.timeLabel.Text = this.startTime + " to " + this.endTime;
            this.numberOfLogsLabel.Text = this.shiftNumber + " of " + this.numberOfShifts;
            this.dateLabel.Text = DateTime.Now.ToString("M/dd/yyyy");

            //Use a data table to store all the data and then apply it to the datagrid view
            DataTable dt = new DataTable();
            dt.Columns.Add("Task Type");
            dt.Columns.Add("Date");
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
                                    this.dateLabel.Text = DateTime.FromOADate(double.Parse(rangeArray.GetValue(Rnum, Cnum).ToString())).ToString("M/dd/yyyy");
                                }
                                //everything else
                                else
                                {
                                    dr[Cnum - 2] = rangeArray.GetValue(Rnum, Cnum).ToString().Trim();
                                }
                            }
                            //Add the row to the data table
                            dt.Rows.Add(dr);
                            //Accept the changes
                            dt.AcceptChanges();
                        }
                        done = true;
                    }
                }
            }
            dt.Columns.Remove("Date");
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
            dataGridView1.Columns[1].Width = 75;
            dataGridView1.Columns[2].Width = 75;
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].Width = 400;

            //Enable text wrapping
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            //Allin to the center and format 
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
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = redBackground;
                    //Font
                    dataGridView1.Rows[i].Cells[0].Style.ForeColor = redFont;
                    dataGridView1.Rows[i].Cells[4].Style.ForeColor = redFont;
                }
                //Change the color of the neck mic tasks
                if(dataGridView1.Rows[i].Cells[4].Value.ToString() == "Ensure neck mic goes back to equipment drawer.")
                {
                    //Background
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = lightblue;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = lightblue;
                }
            }

            //Do not accept the system style
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void nameTextBox_Leave(object sender, EventArgs e)
        {
            if (nameTextBox.Text.Length == 0)
            {
                nameTextBox.Text = "Name";
                nameTextBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void nameTextBox_Enter(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "Name")
            {
                nameTextBox.Text = "";
                nameTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        /// <summary>
        /// When the next button is clicked we close the current window and return
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextBTN_Click(object sender, EventArgs e)
        {
            //INPUT VALIDATION!
            if(this.nameTextBox.Text.Equals("Name"))
            {
                MessageBox.Show("Name Box cannot be empty!");
            }
            else if(!(employeeNameList.Contains(this.nameTextBox.Text.ToLower())))
            {
                MessageBox.Show("Invalid employee name!");
            }
            else //Everything is good
            {
                this.previousClicked = false;
                this.nextClicked = true;
                this.Close();
            }          
        }

        /// <summary>
        /// When the previous button is clicked we close the current window and send a signal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousBTN_Click(object sender, EventArgs e)
        {
            this.nextClicked = false;
            this.previousClicked = true;
            this.Close();
        }
    }
}
