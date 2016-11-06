using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ClassOpsLogCreator
{
    public partial class StatsGenForm : MetroFramework.Forms.MetroForm
    {
        iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 19);
        public StatsGenForm(List<string> eventList, List<string> buildingList, Dictionary<string, int> eventCounter, Dictionary<string, int> buildingCounter)
        {
            InitializeComponent();

            //Create the event datagridview
            this.createDataGrids(this.dataGridofEvents, eventList, eventCounter);

            //Create the Building datagridview
            this.createDataGrids(this.dataGridofBuildinds, buildingList, buildingCounter);



            //Exporting to PDF
            /*string folderPath = @"‪C:\Users\jhan\Desktop\PDF";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }*/
            using (FileStream stream = new FileStream("DataGridViewExport.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                //Create a title
                Paragraph title1 = new Paragraph("Event Count Data", titleFont);
                title1.Alignment = Element.ALIGN_CENTER;
                addEmptyLine(title1, 1);
                pdfDoc.Add(title1);

                //Add the fist table
                pdfDoc.Add(this.writeDataGridViewstoPDF(dataGridofEvents, 100));

                //Add some space
                Paragraph space = new Paragraph("");
                addEmptyLine(space, 1);
                pdfDoc.Add(space);

                //Add a title for the second table
                //Create a title
                Paragraph title2 = new Paragraph("Building Count Data", titleFont);
                title2.Alignment = Element.ALIGN_CENTER;
                addEmptyLine(title2, 2);
                pdfDoc.Add(title2);

                //Add the second table
                pdfDoc.Add(this.writeDataGridViewstoPDF(dataGridofBuildinds, 100));

                //Close the streams
                pdfDoc.Close();
                stream.Close();
            }

        }

        private void createDataGrids(DataGridView dataGridView, List<string> ColumnList, Dictionary<string, int> dataCoutner)
        {
            //Use a data table to store all the data and then apply it to the datagrid view
            DataTable dt = new DataTable();

            //Write the columsn
            foreach (string s in ColumnList)
            {
                dt.Columns.Add(s);
            }

            //Create a new row
            DataRow dr = dt.NewRow();

            for (int Cnum = 0; Cnum < dataCoutner.Count; Cnum++)
            {
                dr[Cnum] = dataCoutner[dt.Columns[Cnum].ToString()];
            }
            //Add the row to the table
            dt.Rows.Add(dr);

            //Accept the changes
            dt.AcceptChanges();

            //Set the datagrid data source to the dataTable
            dataGridView.DataSource = dt;

            for (int Cnum = 0; Cnum < dataGridView.ColumnCount; Cnum++)
            {
                dataGridView.Columns[Cnum].Width = 25;
            }

            //Clear the default selected
            dataGridView.ClearSelection();

            //Do not accept the system style
            dataGridView.RowHeadersVisible = false;
        }

        private PdfPTable writeDataGridViewstoPDF(DataGridView dataGridView1, int percentSize)
        {

            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable.DefaultCell.Padding = 1;
            pdfTable.WidthPercentage = percentSize;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 0;

            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont(FontFactory.COURIER, 9, iTextSharp.text.Font.BOLD)));
                cell.Rotation = 90;
                cell.UseAscender = true;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                cell.BackgroundColor = new BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    PdfPCell cellToAdd = new PdfPCell(new Phrase(cell.Value.ToString(), FontFactory.GetFont(FontFactory.COURIER, 9)));
                    cellToAdd.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellToAdd.UseAscender = true;
                    cellToAdd.PaddingTop = 5;
                    cellToAdd.PaddingLeft = 5;
                    cellToAdd.PaddingRight = 5;
                    cellToAdd.PaddingBottom = 5;
                    //cellToAdd.FixedHeight = 30;
                    pdfTable.AddCell(cellToAdd);
                }
            }
            return pdfTable;

        }

        private static void addEmptyLine(Paragraph paragraph, int number)
        {
            for (int i = 0; i < number; i++)
            {
                paragraph.Add(new Paragraph(" "));
            }
        }
    }
}
