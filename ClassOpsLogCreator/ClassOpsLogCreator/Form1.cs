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

            Excel.Range range1 = MySheetRoom.get_Range("A5", "A10");
        }
    }
}
