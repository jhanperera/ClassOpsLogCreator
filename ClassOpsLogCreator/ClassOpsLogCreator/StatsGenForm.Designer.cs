namespace ClassOpsLogCreator
{
    partial class StatsGenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.eventChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buildingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridofEvents = new System.Windows.Forms.DataGridView();
            this.dataGridofBuildinds = new System.Windows.Forms.DataGridView();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.distrabutionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.eventChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofBuildinds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distrabutionChart)).BeginInit();
            this.SuspendLayout();
            // 
            // eventChart
            // 
            chartArea1.Name = "ChartArea1";
            this.eventChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.eventChart.Legends.Add(legend1);
            this.eventChart.Location = new System.Drawing.Point(579, 400);
            this.eventChart.Name = "eventChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Tasks";
            series1.YValuesPerPoint = 4;
            this.eventChart.Series.Add(series1);
            this.eventChart.Size = new System.Drawing.Size(557, 400);
            this.eventChart.TabIndex = 1;
            this.eventChart.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Task Breakdown";
            this.eventChart.Titles.Add(title1);
            // 
            // buildingChart
            // 
            chartArea2.Name = "ChartArea1";
            this.buildingChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.buildingChart.Legends.Add(legend2);
            this.buildingChart.Location = new System.Drawing.Point(23, 400);
            this.buildingChart.Name = "buildingChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Buildings";
            this.buildingChart.Series.Add(series2);
            this.buildingChart.Size = new System.Drawing.Size(550, 400);
            this.buildingChart.TabIndex = 2;
            this.buildingChart.Text = "chart2";
            title2.Name = "Title1";
            title2.Text = "Building Breakdown";
            this.buildingChart.Titles.Add(title2);
            // 
            // dataGridofEvents
            // 
            this.dataGridofEvents.AllowUserToAddRows = false;
            this.dataGridofEvents.AllowUserToDeleteRows = false;
            this.dataGridofEvents.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridofEvents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridofEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridofEvents.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridofEvents.Location = new System.Drawing.Point(242, 102);
            this.dataGridofEvents.Name = "dataGridofEvents";
            this.dataGridofEvents.ReadOnly = true;
            this.dataGridofEvents.Size = new System.Drawing.Size(675, 122);
            this.dataGridofEvents.TabIndex = 3;
            // 
            // dataGridofBuildinds
            // 
            this.dataGridofBuildinds.AllowUserToAddRows = false;
            this.dataGridofBuildinds.AllowUserToDeleteRows = false;
            this.dataGridofBuildinds.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridofBuildinds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridofBuildinds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridofBuildinds.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridofBuildinds.Location = new System.Drawing.Point(242, 272);
            this.dataGridofBuildinds.Name = "dataGridofBuildinds";
            this.dataGridofBuildinds.ReadOnly = true;
            this.dataGridofBuildinds.Size = new System.Drawing.Size(675, 122);
            this.dataGridofBuildinds.TabIndex = 4;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(522, 80);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(114, 19);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Number of Events";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(485, 250);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(189, 19);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Number of events per building";
            // 
            // distrabutionChart
            // 
            chartArea3.Name = "ChartArea1";
            this.distrabutionChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.distrabutionChart.Legends.Add(legend3);
            this.distrabutionChart.Location = new System.Drawing.Point(23, 806);
            this.distrabutionChart.Name = "distrabutionChart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series2";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series3";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series4";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series5";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series6";
            this.distrabutionChart.Series.Add(series3);
            this.distrabutionChart.Series.Add(series4);
            this.distrabutionChart.Series.Add(series5);
            this.distrabutionChart.Series.Add(series6);
            this.distrabutionChart.Series.Add(series7);
            this.distrabutionChart.Series.Add(series8);
            this.distrabutionChart.Size = new System.Drawing.Size(1113, 255);
            this.distrabutionChart.TabIndex = 7;
            this.distrabutionChart.Text = "chart1";
            title3.Name = "Title1";
            title3.Text = "Combined Data";
            this.distrabutionChart.Titles.Add(title3);
            // 
            // StatsGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 1084);
            this.Controls.Add(this.distrabutionChart);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.dataGridofBuildinds);
            this.Controls.Add(this.dataGridofEvents);
            this.Controls.Add(this.buildingChart);
            this.Controls.Add(this.eventChart);
            this.Name = "StatsGenForm";
            this.Text = "StatsGenForm";
            ((System.ComponentModel.ISupportInitialize)(this.eventChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofBuildinds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distrabutionChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart eventChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart buildingChart;
        private System.Windows.Forms.DataGridView dataGridofEvents;
        private System.Windows.Forms.DataGridView dataGridofBuildinds;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart distrabutionChart;
    }
}