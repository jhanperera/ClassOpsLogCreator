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
        public readonly string EXISTING_MASTER_LOG = @"C:\Users\pereraj\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\masterlog.xlsx";

        public LogViewer()
        {
            InitializeComponent();

        }

        /// <summary>
        /// The main form load event all the work will happen here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogViewer_Load(object sender, EventArgs e)
        {
            //All test code 
            Excel.Application appExl;
            Excel.Workbook workbook;
            Excel.Worksheet NwSheet;
            Excel.Range ShtRange;
            appExl = new Excel.ApplicationClass();
            workbook = appExl.Workbooks.Open((EXISTING_MASTER_LOG));
            NwSheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);

            int Cnum = 0;
            int Rnum = 0;

            ShtRange = NwSheet.UsedRange;
            DataTable dt = new DataTable();
            dt.Columns.Add("Task Type");
            dt.Columns.Add("Date(MM/DD/YYYY)");
            dt.Columns.Add("Time");
            dt.Columns.Add("Building");
            dt.Columns.Add("Room");
            dt.Columns.Add("Special Instructions/Comments");

            Excel.Range last = NwSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range shtRange = NwSheet.get_Range("B1563", "G" + last.Row);

            System.Array classArray = (System.Array)shtRange.Cells.Value2;

            //Add all the elements in the range to the datatable
            for (Rnum = 1; Rnum <= classArray.GetUpperBound(0); Rnum++)
            {
                DataRow dr = dt.NewRow();
                for (Cnum = 1; Cnum <= classArray.GetUpperBound(1); Cnum++)
                {
                    if (classArray.GetValue(Rnum, Cnum) == null)
                    {
                        dr[Cnum - 1] = "";
                    }
                    else
                    {
                        dr[Cnum - 1] = classArray.GetValue(Rnum, Cnum).ToString().Trim();
                    }
                }
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }

            //close to book
            workbook.Close(true);
            appExl.Quit();


            //Session["data"] = dt; 
            dataGridView1.DataSource = dt;

            this.format_DataGirdView();
            
            
        }

        /// <summary>
        /// All the formatting of the datagrid view will go here
        /// This includes sizing and color of all the special cells
        /// </summary>
        private void format_DataGirdView()
        {
            //Increase the width of the last columns
            dataGridView1.Columns[5].Width = 360;
            dataGridView1.Columns[0].Width = 100;


            //Enable text wraping
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            //Allight to the center
            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}
