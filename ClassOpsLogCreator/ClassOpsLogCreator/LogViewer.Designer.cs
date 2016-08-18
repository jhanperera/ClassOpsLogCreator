namespace ClassOpsLogCreator
{
    partial class LogViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogViewer));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.logCreatorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numberOfLogsLabel = new System.Windows.Forms.Label();
            this.timeLabel = new MetroFramework.Controls.MetroLabel();
            this.dateLabel = new MetroFramework.Controls.MetroLabel();
            this.nameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.previousBTN = new MetroFramework.Controls.MetroButton();
            this.nextBTN = new MetroFramework.Controls.MetroButton();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logCreatorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.TabStop = false;
            // 
            // numberOfLogsLabel
            // 
            resources.ApplyResources(this.numberOfLogsLabel, "numberOfLogsLabel");
            this.numberOfLogsLabel.Name = "numberOfLogsLabel";
            // 
            // timeLabel
            // 
            resources.ApplyResources(this.timeLabel, "timeLabel");
            this.timeLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.timeLabel.Name = "timeLabel";
            // 
            // dateLabel
            // 
            resources.ApplyResources(this.dateLabel, "dateLabel");
            this.dateLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.dateLabel.Name = "dateLabel";
            // 
            // nameTextBox
            // 
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.nameTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.nameTextBox, resources.GetString("nameTextBox.ToolTip"));
            // 
            // previousBTN
            // 
            resources.ApplyResources(this.previousBTN, "previousBTN");
            this.previousBTN.Name = "previousBTN";
            this.metroToolTip1.SetToolTip(this.previousBTN, resources.GetString("previousBTN.ToolTip"));
            this.previousBTN.Click += new System.EventHandler(this.previousBTN_Click);
            // 
            // nextBTN
            // 
            resources.ApplyResources(this.nextBTN, "nextBTN");
            this.nextBTN.Name = "nextBTN";
            this.metroToolTip1.SetToolTip(this.nextBTN, resources.GetString("nextBTN.ToolTip"));
            this.nextBTN.Click += new System.EventHandler(this.nextBTN_Click_1);
            // 
            // LogViewer
            // 
            this.AcceptButton = this.nextBTN;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.nextBTN);
            this.Controls.Add(this.previousBTN);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.numberOfLogsLabel);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "LogViewer";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Load += new System.EventHandler(this.LogViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logCreatorBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource logCreatorBindingSource;
        private System.Windows.Forms.Label numberOfLogsLabel;
        private MetroFramework.Controls.MetroLabel timeLabel;
        private MetroFramework.Controls.MetroLabel dateLabel;
        private MetroFramework.Controls.MetroTextBox nameTextBox;
        private MetroFramework.Controls.MetroButton previousBTN;
        private MetroFramework.Controls.MetroButton nextBTN;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}