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
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.eventChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buildingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.distrabutionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.eventChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distrabutionChart)).BeginInit();
            this.SuspendLayout();
            // 
            // eventChart
            // 
            chartArea1.Name = "ChartArea1";
            this.eventChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.eventChart.Legends.Add(legend1);
            this.eventChart.Location = new System.Drawing.Point(59, 87);
            this.eventChart.Name = "eventChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Tasks";
            series1.YValuesPerPoint = 4;
            this.eventChart.Series.Add(series1);
            this.eventChart.Size = new System.Drawing.Size(1366, 575);
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
            this.buildingChart.Location = new System.Drawing.Point(23, 126);
            this.buildingChart.Name = "buildingChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Buildings";
            this.buildingChart.Series.Add(series2);
            this.buildingChart.Size = new System.Drawing.Size(1366, 575);
            this.buildingChart.TabIndex = 2;
            this.buildingChart.Text = "chart2";
            title2.Name = "Title1";
            title2.Text = "Building Breakdown";
            this.buildingChart.Titles.Add(title2);
            // 
            // distrabutionChart
            // 
            chartArea3.Name = "ChartArea1";
            this.distrabutionChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.distrabutionChart.Legends.Add(legend3);
            this.distrabutionChart.Location = new System.Drawing.Point(23, 63);
            this.distrabutionChart.Name = "distrabutionChart";
            this.distrabutionChart.Size = new System.Drawing.Size(1366, 575);
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
            this.ClientSize = new System.Drawing.Size(241, 95);
            this.Controls.Add(this.distrabutionChart);
            this.Controls.Add(this.buildingChart);
            this.Controls.Add(this.eventChart);
            this.Name = "StatsGenForm";
            this.Text = "StatsGenForm";
            ((System.ComponentModel.ISupportInitialize)(this.eventChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buildingChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distrabutionChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart eventChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart buildingChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart distrabutionChart;
    }
}