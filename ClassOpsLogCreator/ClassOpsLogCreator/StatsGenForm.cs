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
using System.Windows.Forms.DataVisualization.Charting;

namespace ClassOpsLogCreator
{
    public partial class StatsGenForm : MetroFramework.Forms.MetroForm
    {
        iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 19);

        /// <summary>
        /// Create a statics visualization and writes the data to a pdf
        /// </summary>
        /// <param name="eventList"></param>
        /// <param name="buildingList"></param>
        /// <param name="eventCounter"></param>
        /// <param name="buildingCounter"></param>
        public StatsGenForm(List<string> eventList, List<string> buildingList, Dictionary<string, int> eventCounter, Dictionary<string, int> buildingCounter, DateTime StartDate, DateTime EndDate)
        {
            InitializeComponent();

            string startDate = StartDate.ToString("ddd, MMM dd, yyyy");
            string endDate = EndDate.ToString("ddd, MMM dd, yyyy");

            //Create the event datagridview
            this.createDataGrids(this.dataGridofEvents, eventList, eventCounter);

            //Create the Building datagridview
            this.createDataGrids(this.dataGridofBuildinds, buildingList, buildingCounter);

            //Create the chart
            this.createEventChart(eventList, eventCounter);
            this.createBuildingChart(buildingList, buildingCounter);
            this.createBuildingtoEventChart();

            //eventChart memory block to add to pdf
            var eventChartImage = new MemoryStream();
            this.eventChart.SaveImage(eventChartImage, ChartImageFormat.Tiff);

            //eventChart memory block to add to pdf
            var buildingChartImage = new MemoryStream();
            this.buildingChart.SaveImage(buildingChartImage, ChartImageFormat.Tiff);

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
                Paragraph title1 = new Paragraph("Classroom Operations Statistics Report", titleFont);
                title1.Alignment = Element.ALIGN_LEFT;
                pdfDoc.Add(title1);

                //Create a line and add it in to the pdf
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                pdfDoc.Add(p);

                //Add a sub title
                Paragraph subTitle = new Paragraph(String.Format("Statistics from {0} to {1}", startDate ,endDate));
                addEmptyLine(subTitle, 1);
                pdfDoc.Add(subTitle);

                //Add the charts to a table
                iTextSharp.text.Image eventChart_Image = iTextSharp.text.Image.GetInstance(eventChartImage.GetBuffer());
                iTextSharp.text.Image buildingChart_Image = iTextSharp.text.Image.GetInstance(buildingChartImage.GetBuffer());

                eventChart_Image.ScaleAbsolute(200f, 50f);
                buildingChart_Image.ScaleAbsolute(200f, 50f);

                PdfPTable imageTable = new PdfPTable(1);
                imageTable.AddCell(eventChart_Image);
                imageTable.AddCell(buildingChart_Image);
                imageTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pdfDoc.Add(imageTable);

                //Add a new title
                Paragraph title2 = new Paragraph("Raw Data", titleFont);
                title1.Alignment = Element.ALIGN_LEFT;
                pdfDoc.Add(title2);

                //add a line and add it in to the pdf
                addEmptyLine(p, 1);
                pdfDoc.Add(p);

                //Add a title for the second table
                Paragraph title3 = new Paragraph("Task Count Data", titleFont);
                title2.Alignment = Element.ALIGN_CENTER;
                addEmptyLine(title2, 1);
                pdfDoc.Add(title2);

                //Add the fist table
                pdfDoc.Add(this.writeDataGridViewstoPDF(dataGridofEvents, 80));

                //Add some space
                Paragraph space = new Paragraph("");
                addEmptyLine(space, 1);
                pdfDoc.Add(space);

                //Add a title for the second table
                Paragraph title4 = new Paragraph("Building Count Data", titleFont);
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
        
        /// <summary>
        /// Create datagridviews for easy access to the dpf table
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="ColumnList"></param>
        /// <param name="dataCoutner"></param>
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

        /// <summary>
        /// Create the event chart
        /// </summary>
        /// <param name="eventList"></param>
        /// <param name="eventCounter"></param>
        private void createEventChart(List<string> eventList, Dictionary<string,int> eventCounter)
        {
            //Look though each of the events and add it as an x and y value 
            foreach(string s in eventList)
            {
                this.eventChart.Series["Tasks"].Points.AddXY(s, eventCounter[s]);
            }

            //Set the label to outside
            this.eventChart.Series["Tasks"]["PieLabelStyle"] = "Outside";
            //Make the chart 3D
            this.eventChart.ChartAreas[0].Area3DStyle.Enable3D = true;
            //Explode each of the values /slices
            for (int i = 0; i < this.eventChart.Series["Tasks"].Points.Count; i++)
            {
                this.eventChart.Series["Tasks"].Points[i]["Exploded"] = "True";
            }
            //Make the legend show percent and value
            this.eventChart.Series[0].LegendText = "#PERCENT #VALX";
            //Set the legend at the bottom
            this.eventChart.Legends[0].Docking = Docking.Bottom;
        }

        /// <summary>
        /// This method will create the stacked bar chart with the combined chart
        /// </summary>
        /// <param name="eventList"></param>
        /// <param name="buildingList"></param>
        /// <param name="combinedDic"></param>
        private void createBuildingtoEventChart(/*List<string> eventList, List<string> buildingList, Dictionary<string, Dictionary<string, int>> combinedDic*/)
        {
            //Add the eventList to the series
            /*foreach(string e in eventList)
            {
                Series seriesToAdd = new Series(e.ToString());
                this.distrabutionChart.Series.Add(seriesToAdd);
            }*/
            this.distrabutionChart.Series["Series1"].ChartType = SeriesChartType.StackedColumn;
            this.distrabutionChart.Series["Series2"].ChartType = SeriesChartType.StackedColumn;
            this.distrabutionChart.Series["Series3"].ChartType = SeriesChartType.StackedColumn;
            this.distrabutionChart.Series["Series4"].ChartType = SeriesChartType.StackedColumn;
            this.distrabutionChart.Series["Series5"].ChartType = SeriesChartType.StackedColumn;

            this.distrabutionChart.Series["Series1"].Points.AddXY(1, 50);
            this.distrabutionChart.Series["Series2"].Points.AddXY(1, 100);
            this.distrabutionChart.Series["Series3"].Points.AddXY(1, 0);
            this.distrabutionChart.Series["Series4"].Points.AddXY(1, 0);
            this.distrabutionChart.Series["Series5"].Points.AddXY(1, 0);

            this.distrabutionChart.Series["Series1"].Points.AddXY(2, 0);
            this.distrabutionChart.Series["Series2"].Points.AddXY(2, 50);
            this.distrabutionChart.Series["Series3"].Points.AddXY(2, 200);
            this.distrabutionChart.Series["Series4"].Points.AddXY(2, 0);
            this.distrabutionChart.Series["Series5"].Points.AddXY(2, 0);

            this.distrabutionChart.Series["Series1"].Points.AddXY(3, 0);
            this.distrabutionChart.Series["Series2"].Points.AddXY(3, 0);
            this.distrabutionChart.Series["Series3"].Points.AddXY(3, 100);
            this.distrabutionChart.Series["Series4"].Points.AddXY(3, 50);
            this.distrabutionChart.Series["Series5"].Points.AddXY(3, 0);

            this.distrabutionChart.Series["Series1"].Points.AddXY(5, 0);
            this.distrabutionChart.Series["Series2"].Points.AddXY(5, 0);
            this.distrabutionChart.Series["Series3"].Points.AddXY(5, 0);
            this.distrabutionChart.Series["Series4"].Points.AddXY(5, 0);
            this.distrabutionChart.Series["Series5"].Points.AddXY(5, 150);
        }

        /// <summary>
        /// Create the building chart
        /// </summary>
        /// <param name="buildingList"></param>
        /// <param name="buildingCounter"></param>
        private void createBuildingChart(List<string> buildingList, Dictionary<string, int> buildingCounter)
        {
            //look at each building and create a x and y axis value
            foreach (string s in buildingList)
            {
                this.buildingChart.Series["Buildings"].Points.AddXY(s, buildingCounter[s]);
            }

            //Set the label to outside
            this.buildingChart.Series["Buildings"]["PieLabelStyle"] = "Outside";
            //Make the chart 3D
            this.buildingChart.ChartAreas[0].Area3DStyle.Enable3D = true;
            //Exlose each value in the chart
            for (int i = 0; i < this.buildingChart.Series["Buildings"].Points.Count; i++)
            {
                this.buildingChart.Series["Buildings"].Points[i]["Exploded"] = "True";
            }
            //The legend should have percent and value
            this.buildingChart.Series[0].LegendText = "#PERCENT #VALX";
            //Dock the legend at the bottom.
            this.buildingChart.Legends[0].Docking = Docking.Bottom;
        }


        /// <summary>
        /// Write the datagrid to a pdf table. return a table to be added to the pdf
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="percentSize"></param>
        /// <returns></returns>
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
                //Add a chunk with the header text
                Chunk addtoHeader = new Chunk(column.HeaderText, FontFactory.GetFont(FontFactory.COURIER, 9, iTextSharp.text.Font.BOLD));
                //Set the skew to the header go on an angle
                addtoHeader.SetSkew(-30f,0f);
                //Add the chunk to the cell
                PdfPCell cell = new PdfPCell(new Phrase(addtoHeader));
                //Rotate the text 90 degrees
                cell.Rotation = 90;
                //Use ascending text
                cell.UseAscender = true;
                //Add some padding
                cell.PaddingBottom = 5;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
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
