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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridofEvents = new System.Windows.Forms.DataGridView();
            this.dataGridofBuildinds = new System.Windows.Forms.DataGridView();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofBuildinds)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(23, 400);
            this.chart1.Name = "chart1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            series5.YValuesPerPoint = 4;
            this.chart1.Series.Add(series5);
            this.chart1.Size = new System.Drawing.Size(340, 290);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea6.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart2.Legends.Add(legend6);
            this.chart2.Location = new System.Drawing.Point(358, 400);
            this.chart2.Name = "chart2";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart2.Series.Add(series6);
            this.chart2.Size = new System.Drawing.Size(340, 290);
            this.chart2.TabIndex = 2;
            this.chart2.Text = "chart2";
            // 
            // dataGridofEvents
            // 
            this.dataGridofEvents.AllowUserToAddRows = false;
            this.dataGridofEvents.AllowUserToDeleteRows = false;
            this.dataGridofEvents.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridofEvents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridofEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridofEvents.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridofEvents.Location = new System.Drawing.Point(23, 102);
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
            this.dataGridofBuildinds.Location = new System.Drawing.Point(23, 272);
            this.dataGridofBuildinds.Name = "dataGridofBuildinds";
            this.dataGridofBuildinds.ReadOnly = true;
            this.dataGridofBuildinds.Size = new System.Drawing.Size(675, 122);
            this.dataGridofBuildinds.TabIndex = 4;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(303, 80);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(114, 19);
            this.metroLabel1.TabIndex = 5;
            this.metroLabel1.Text = "Number of Events";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(266, 250);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(189, 19);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Number of events per building";
            // 
            // StatsGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 713);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.dataGridofBuildinds);
            this.Controls.Add(this.dataGridofEvents);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Name = "StatsGenForm";
            this.Text = "StatsGenForm";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridofBuildinds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataGridView dataGridofEvents;
        private System.Windows.Forms.DataGridView dataGridofBuildinds;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
    }
}