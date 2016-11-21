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
    /// <summary>
    /// A StatsGen helper that builds the graphs and writes the raw data to a pdf file
    /// </summary>
    public partial class StatsGenForm : MetroFramework.Forms.MetroForm
    {
        private iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 19);
        private iTextSharp.text.Font smallertitleFont = FontFactory.GetFont("Arial", 12);


        /// <summary>
        /// Create a statics visualization and writes the data to a pdf
        /// </summary>
        /// <param name="eventList"></param>
        /// <param name="buildingList"></param>
        /// <param name="eventCounter"></param>
        /// <param name="buildingCounter"></param>
        /// <param name="combineDic"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="fileLocation"></param>
        /// <param name="fileName"></param>
        public StatsGenForm(List<string> eventList, List<string> buildingList, Dictionary<string, int> eventCounter, Dictionary<string, int> buildingCounter, 
                            Dictionary<string,Dictionary<string,int>> combineDic , DateTime StartDate, DateTime EndDate, string fileLocation, string fileName)
        {
            InitializeComponent();

            //Set the start and end date format.
            string startDate = StartDate.ToString("ddd, MMM dd, yyyy");
            string endDate = EndDate.ToString("ddd, MMM dd, yyyy");

            //Get a totalCount
            int totalCount = 0;
            foreach(string s in eventList)
            {
                totalCount += eventCounter[s.ToString()];
            }

            //Create the chart
            this.createEventChart(eventCounter);
            this.createEventChartNOCLO(eventCounter);
            this.createBuildingtoEventChart(eventCounter, buildingCounter, combineDic);

            //eventChart memory block to add to pdf
            var eventChartImage = new MemoryStream();
            this.eventChart.SaveImage(eventChartImage, ChartImageFormat.Png);

            //buildingChart memory block to add to pdf
            var buildingChartImage = new MemoryStream();
            this.eventChartNOCLO.SaveImage(buildingChartImage, ChartImageFormat.Png);

            //eventChart memory block to add to pdf
            var combinedChartImage = new MemoryStream();
            this.distrabutionChart.SaveImage(combinedChartImage, ChartImageFormat.Png);

            //Exporting to PDF
            string folderPath = fileLocation;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + fileName, FileMode.Create))
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
                iTextSharp.text.Image distrabutionChart_Image = iTextSharp.text.Image.GetInstance(combinedChartImage.GetBuffer());

                PdfPTable imageTable = new PdfPTable(1);
                imageTable.DefaultCell.Border = 0;
                imageTable.AddCell(eventChart_Image);
                imageTable.AddCell(buildingChart_Image);
                imageTable.AddCell(distrabutionChart_Image);
                imageTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageTable.HorizontalAlignment = Element.ALIGN_LEFT;
                imageTable.WidthPercentage = 100;
                pdfDoc.Add(imageTable);

                //Add a new title
                Paragraph title2 = new Paragraph("Raw Data", titleFont);
                title2.Alignment = Element.ALIGN_LEFT;
                pdfDoc.Add(title2);

                //add a line and add it in to the pdf
                addEmptyLine(p, 1);
                pdfDoc.Add(p);

                //Write tabular data to a master table to have them side by side
                PdfPTable tableToAdd1 = this.writeDataGridViewstoPDF(eventList, eventCounter, 45, "Task Data");
                PdfPTable tableToAdd2 = this.writeDataGridViewstoPDF(buildingList, buildingCounter, 45, "Building Data");
                PdfPTable masterTable = new PdfPTable(2);
                masterTable.AddCell(tableToAdd1);
                masterTable.AddCell(tableToAdd2);
                masterTable.AddCell(new Phrase("Total: " + totalCount.ToString()));
                masterTable.AddCell(new Phrase(""));
                masterTable.WidthPercentage = 80;
                
                //Add the fist table
                pdfDoc.Add(masterTable);

                Paragraph emptySpsace = new Paragraph("");
                addEmptyLine(emptySpsace, 5);
                pdfDoc.Add(emptySpsace);

                //Add a new title
                Paragraph title3 = new Paragraph("Combined Raw Data", titleFont);
                title2.Alignment = Element.ALIGN_LEFT;
                pdfDoc.Add(title3);

                //add a line and add it in to the pdf
                addEmptyLine(p, 3);
                pdfDoc.Add(p);

                //Add combinedData
                PdfPTable combineTable = writeCombinedDatatoPDF(eventList, buildingList, combineDic);
                combineTable.HorizontalAlignment = Element.ALIGN_LEFT;
                combineTable.WidthPercentage = 95;
                pdfDoc.Add(combineTable);

                //Close the streams
                pdfDoc.Close();
                stream.Close();
            }
        }
        
        /// <summary>
        /// Create the event chart
        /// </summary>
        /// <param name="eventCounter"></param>
        private void createEventChart(Dictionary<string,int> eventCounter)
        {
            //Look though each of the events and add it as an x and y value 
            foreach(string s in eventCounter.Keys)
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
            this.eventChart.Series[0].LegendText = "#PERCENT{P0} #VALX";
            //Set the legend at the bottom
            this.eventChart.Legends[0].Docking = Docking.Left;
            this.eventChart.Legends[0].Font = new System.Drawing.Font("Verdona", 11);
        }

        /// <summary>
        /// Create the building chart
        /// </summary>
        /// <param name="buildingList"></param>
        /// <param name="buildingCounter"></param>
        private void createEventChartNOCLO(Dictionary<string, int> eventCounter)
        {
            //look at each building and create a x and y axis value
            foreach (string s in eventCounter.Keys)
            {
                if (s != "Crestron Logout")
                {
                    this.eventChartNOCLO.Series["Tasks"].Points.AddXY(s, eventCounter[s]);
                }
            }

            //Set the label to outside
            this.eventChartNOCLO.Series["Tasks"]["PieLabelStyle"] = "Outside";
            //Make the chart 3D
            this.eventChartNOCLO.ChartAreas[0].Area3DStyle.Enable3D = true;
            //Expose each value in the chart
            for (int i = 0; i < this.eventChartNOCLO.Series["Tasks"].Points.Count; i++)
            {
                this.eventChartNOCLO.Series["Tasks"].Points[i]["Exploded"] = "True";
            }
            //The legend should have percent and value
            this.eventChartNOCLO.Series[0].LegendText = "#PERCENT{P0} #VALX";
            //Dock the legend at the bottom.
            this.eventChartNOCLO.Legends[0].Docking = Docking.Right;
            this.eventChartNOCLO.Legends[0].Font = new System.Drawing.Font("Verdona", 11);
        }

        /// <summary>
        /// This method will create the stacked bar chart with the combined chart
        /// </summary>
        /// <param name="eventList"></param>
        /// <param name="buildingList"></param>
        /// <param name="combinedDic"></param>
        private void createBuildingtoEventChart(Dictionary<string, int> eventList, Dictionary<string, int> buildingList, Dictionary<string, Dictionary<string, int>> combinedDic)
        {
            //Add the eventList to the series
            foreach (string e in eventList.Keys)
            {
                if (e != "Crestron Logout" && eventList[e] > 0)
                {
                    //add the event as a series
                    Series seriesToAdd = new Series(e.ToString());
                    this.distrabutionChart.Series.Add(seriesToAdd);
                    //Set it as a Stacking Column
                    this.distrabutionChart.Series[e.ToString()].ChartType = SeriesChartType.StackedColumn;
                    foreach (string s in buildingList.Keys)
                    {
                        if (buildingList[s] > 0)
                        {
                            //Add out X value to the building, and our Y value as the number of occurrences.
                            this.distrabutionChart.Series[e.ToString()].Points.AddXY(s, (combinedDic[s])[e]);
                        }
                    }
                }             
            }
            this.distrabutionChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
            this.distrabutionChart.Legends[0].Docking = Docking.Bottom;
            this.distrabutionChart.Legends[0].Font = new System.Drawing.Font("Verdona", 11);
            this.distrabutionChart.Palette = ChartColorPalette.BrightPastel;
        }

        /// <summary>
        /// Write the datagrid to a pdf table. return a table to be added to the pdf
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="percentSize"></param>
        /// <returns></returns>
        private PdfPTable writeDataGridViewstoPDF(List<string> dataKeys, Dictionary<string,int> data, int percentSize, string tabelTitel)
        {

            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(2);
            pdfTable.DefaultCell.Padding = 1;
            pdfTable.WidthPercentage = percentSize;
            pdfTable.DefaultCell.BorderWidth = 0;

            PdfPCell taskHeader = new PdfPCell(new Phrase(tabelTitel));
            PdfPCell counterHeader = new PdfPCell(new Phrase("Number of Tasks"));
            taskHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            taskHeader.BackgroundColor = new BaseColor(255, 0, 0);
            counterHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            counterHeader.BackgroundColor = new BaseColor(255, 0, 0);
            pdfTable.AddCell(taskHeader);
            pdfTable.AddCell(counterHeader);

            foreach(string e in dataKeys)
            {
                PdfPCell taskLabel = new PdfPCell(new Phrase(e.ToString()));
                taskLabel.BackgroundColor = new BaseColor(156, 156, 156);
                taskLabel.HorizontalAlignment = Element.ALIGN_CENTER;
                int datatoAdd = data[e];
                PdfPCell dataLabel = new PdfPCell(new Phrase(datatoAdd.ToString()));
                dataLabel.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.AddCell(taskLabel);
                pdfTable.AddCell(dataLabel);
            }

            //Add a total
            return pdfTable;
        }

        /// <summary>
        /// This method will generate a matrix table of all the information.
        /// </summary>
        /// <param name="eventList"></param>
        /// <param name="buildingList"></param>
        /// <param name="combinedData"></param>
        /// <returns></returns>
        private PdfPTable writeCombinedDatatoPDF(List<string> eventList, List<string> buildingList, Dictionary<string, Dictionary<string, int>> combinedData)
        {
            //Create a table to write the data too
            PdfPTable pdfTable = new PdfPTable(eventList.Count + 1);
            //Add a space here
            PdfPCell spaceToAdd = new PdfPCell(new Phrase(""));
            spaceToAdd.Border = 0;
            pdfTable.AddCell(spaceToAdd);

            //Adding the header
            foreach (string e in eventList)
            {
                Chunk chuckToAdd = new Chunk(e.ToString());
                chuckToAdd.SetSkew(-30f, 0f);
                PdfPCell cellToAdd = new PdfPCell(new Phrase(chuckToAdd));
                cellToAdd.Rotation = 90;
                cellToAdd.UseAscender = true;
                cellToAdd.Border = 0;
                pdfTable.AddCell(cellToAdd);
            }

            int count = 0;
            //For each building we display all the data.
            foreach (string s in buildingList)
            {
                //Add a new header
                if(count > 0 && count % 10 == 0)
                {
                    //Add a space here
                    PdfPCell callToAdd = new PdfPCell(new Phrase(""));
                    spaceToAdd.Border = 0;
                    pdfTable.AddCell(spaceToAdd);

                    //Adding the header
                    foreach (string e in eventList)
                    {
                        Chunk chuckToAdd = new Chunk(e.ToString());
                        chuckToAdd.SetSkew(-30f, 0f);
                        PdfPCell callToAdd1 = new PdfPCell(new Phrase(chuckToAdd));
                        callToAdd1.Rotation = 90;
                        callToAdd1.UseAscender = true;
                        callToAdd1.Border = 0;
                        pdfTable.AddCell(callToAdd1);
                    }
                }
                PdfPCell cellToAdd = new PdfPCell(new Phrase(s.ToString()));
                cellToAdd.Border = 0;
                cellToAdd.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellToAdd.NoWrap = true;
                pdfTable.AddCell(cellToAdd);
                foreach (string e in eventList)
                {
                    int value = (combinedData[s.ToString()])[e.ToString()];
                    PdfPCell valuecellToAdd = new PdfPCell(new Phrase(value.ToString()));
                    valuecellToAdd.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable.AddCell(valuecellToAdd);
                }
                count++;
            }

            //Set alternative colored rows
            bool b = false;
            count = 0;
            foreach(PdfPRow r in pdfTable.Rows)
            {
                foreach(PdfPCell c in r.GetCells())
                {
                    if (count > 0 && count % 11 == 0 && b == true)
                    {
                        b = false;
                    }
                    if (b)
                    {
                        c.BackgroundColor = new BaseColor(156, 156, 156);
                    }
                }
                b = !b;
                count++;
            }

            //Set the widths of the first column to be twice as large
            float[] tableWidths = new float[(eventList.Count + 1)];
            tableWidths[0] = 2f;
            for (int i = 1; i <= tableWidths.GetUpperBound(0); i++)
            {
                tableWidths[i] = 1.5f;
            }
            tableWidths[3] = 2f;
            pdfTable.SetWidths(tableWidths);

            //Return the pdfTable
            return pdfTable;
        }

        /// <summary>
        /// Addes an empty line to the pdf
        /// </summary>
        /// <param name="paragraph"></param>
        /// <param name="number"></param>
        private static void addEmptyLine(Paragraph paragraph, int number)
        {
            for (int i = 0; i < number; i++)
            {
                paragraph.Add(new Paragraph(" "));
            }
        }
    }
}
