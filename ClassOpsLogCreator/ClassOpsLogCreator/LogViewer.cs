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
        public readonly string EXISTING_MASTER_LOG = @"C:\Users\jhan\Documents\Visual Studio 2015\Projects\ClassOpsLogCreator\CLASSOPS\masterlog.xlsx";

        public LogViewer()
        {
            InitializeComponent();

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
            dt.Columns.Add("name");
            dt.Columns.Add("address");
            // dt.Columns.Add("Status");
            dt.Columns.Add("Phone1");
            dt.Columns.Add("Phone2");
            dt.Columns.Add("Phone3");
            dt.Columns.Add("Phone4");
            dt.Columns.Add("Phone5s");

            Excel.Range last = NwSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range shtRange = NwSheet.get_Range("B2", last);
            System.Array classArray = (System.Array)shtRange.Cells.Value2;

            for (Rnum = 1; Rnum <= classArray.GetUpperBound(0); Rnum++)
            {
                DataRow dr = dt.NewRow();
                for (Cnum = 1; Cnum <= classArray.GetUpperBound(1); Cnum++)
                {
                    if(classArray.GetValue(Rnum, Cnum) == null)
                    {
                        dr[Cnum - 1] = "";
                    }
                    else
                    {
                        dr[Cnum - 1] = classArray.GetValue(Rnum, Cnum).ToString();
                    }                
                }
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
            workbook.Close(true);
            appExl.Quit();


            //Session["data"] = dt; 
            dataGridView1.DataSource = dt;
            //dataGridView1.DataBind();*/
        }
    }
}
