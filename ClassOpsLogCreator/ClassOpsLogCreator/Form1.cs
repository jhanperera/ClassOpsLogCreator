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
        // RoomSched
        private static Excel.Application RoomSched = null;

        private static Excel.Workbook MyBookRoom = null;

        private static Excel.Worksheet MySheetRoom = null;

        public LogCreator()
        {
            InitializeComponent();
        }

        private void createBTN_Click(object sender, EventArgs e)
        {
            //Open the room excel file
            RoomSched = new Excel.Application();
            RoomSched.Visible = true;
            MyBookRoom = RoomSched.Workbooks.Open("C:\\Users\\pereraj\\Desktop\\room schedule.xlsx");
            MySheetRoom = MyBookRoom.Sheets[1];

            //Excel.Range last = MySheetRoom.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            Excel.Range range1 = MySheetRoom.get_Range("A5", "A33");

            System.Array array = (System.Array)range1.Cells.Value2;
            
            string[] arrayS = this.ConvertToStringArray(array);

            textBox1.Text = arrayS[5];
        }
        private string[] ConvertToStringArray(System.Array values)
        {
            string[] newArray = new string[values.Length];


            int index = 0;
            for (int i = values.GetLowerBound(0);
                  i <= values.GetUpperBound(0); i++)
            {
                for (int j = values.GetLowerBound(1);
                          j <= values.GetUpperBound(1); j++)
                {
                    if (values.GetValue(i, j) == null)
                    {
                        newArray[index] = "";
                    }
                    else
                    {
                        newArray[index] = (string)values.GetValue(i, j).ToString();
                    }
                    index++;
                }
            }
            return newArray;
        }
    }
}
