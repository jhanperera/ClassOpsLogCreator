namespace ClassOpsLogCreator
{
    partial class LogCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogCreator));
            this.createBTN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.workProgressBar = new System.Windows.Forms.ProgressBar();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.toLabel2 = new System.Windows.Forms.Label();
            this.endHour2 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo4 = new System.Windows.Forms.ComboBox();
            this.numberOfShiftsLabel2 = new System.Windows.Forms.Label();
            this.shiftTime2 = new System.Windows.Forms.Label();
            this.numberOfShiftsCombo2 = new System.Windows.Forms.ComboBox();
            this.startHour2 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo3 = new System.Windows.Forms.ComboBox();
            this.toLabel1 = new System.Windows.Forms.Label();
            this.endHour1 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo2 = new System.Windows.Forms.ComboBox();
            this.numberOfShiftsLabel1 = new System.Windows.Forms.Label();
            this.shiftTime1 = new System.Windows.Forms.Label();
            this.numberOfShiftsCombo1 = new System.Windows.Forms.ComboBox();
            this.startHour1 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // createBTN
            // 
            this.createBTN.Location = new System.Drawing.Point(13, 309);
            this.createBTN.Margin = new System.Windows.Forms.Padding(4);
            this.createBTN.Name = "createBTN";
            this.createBTN.Size = new System.Drawing.Size(264, 43);
            this.createBTN.TabIndex = 0;
            this.createBTN.Text = "Create Master Logout Log";
            this.createBTN.UseVisualStyleBackColor = true;
            this.createBTN.Click += new System.EventHandler(this.createBTN_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(288, 330);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(427, 22);
            this.textBox1.TabIndex = 1;
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoEllipsis = true;
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(288, 309);
            this.destinationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(148, 16);
            this.destinationLabel.TabIndex = 2;
            this.destinationLabel.Text = "Master Log location:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::ClassOpsLogCreator.Properties.Resources.www_zaxonusa_com_small_icon_schedual;
            this.pictureBox1.Location = new System.Drawing.Point(445, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(253, 273);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // workProgressBar
            // 
            this.workProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.workProgressBar.Location = new System.Drawing.Point(0, 360);
            this.workProgressBar.Margin = new System.Windows.Forms.Padding(4);
            this.workProgressBar.Name = "workProgressBar";
            this.workProgressBar.Size = new System.Drawing.Size(728, 25);
            this.workProgressBar.TabIndex = 7;
            // 
            // mainPanel
            // 
            this.mainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mainPanel.Controls.Add(this.toLabel2);
            this.mainPanel.Controls.Add(this.endHour2);
            this.mainPanel.Controls.Add(this.am_pmCombo4);
            this.mainPanel.Controls.Add(this.numberOfShiftsLabel2);
            this.mainPanel.Controls.Add(this.shiftTime2);
            this.mainPanel.Controls.Add(this.numberOfShiftsCombo2);
            this.mainPanel.Controls.Add(this.startHour2);
            this.mainPanel.Controls.Add(this.am_pmCombo3);
            this.mainPanel.Controls.Add(this.toLabel1);
            this.mainPanel.Controls.Add(this.endHour1);
            this.mainPanel.Controls.Add(this.am_pmCombo2);
            this.mainPanel.Controls.Add(this.numberOfShiftsLabel1);
            this.mainPanel.Controls.Add(this.shiftTime1);
            this.mainPanel.Controls.Add(this.numberOfShiftsCombo1);
            this.mainPanel.Controls.Add(this.startHour1);
            this.mainPanel.Controls.Add(this.am_pmCombo1);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Location = new System.Drawing.Point(13, 13);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(702, 281);
            this.mainPanel.TabIndex = 45;
            // 
            // toLabel2
            // 
            this.toLabel2.AutoSize = true;
            this.toLabel2.Location = new System.Drawing.Point(210, 158);
            this.toLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.toLabel2.Name = "toLabel2";
            this.toLabel2.Size = new System.Drawing.Size(21, 16);
            this.toLabel2.TabIndex = 60;
            this.toLabel2.Text = "to";
            // 
            // endHour2
            // 
            this.endHour2.FormattingEnabled = true;
            this.endHour2.Location = new System.Drawing.Point(242, 154);
            this.endHour2.Margin = new System.Windows.Forms.Padding(4);
            this.endHour2.Name = "endHour2";
            this.endHour2.Size = new System.Drawing.Size(83, 24);
            this.endHour2.TabIndex = 58;
            // 
            // am_pmCombo4
            // 
            this.am_pmCombo4.FormattingEnabled = true;
            this.am_pmCombo4.Location = new System.Drawing.Point(340, 154);
            this.am_pmCombo4.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo4.Name = "am_pmCombo4";
            this.am_pmCombo4.Size = new System.Drawing.Size(83, 24);
            this.am_pmCombo4.TabIndex = 59;
            // 
            // numberOfShiftsLabel2
            // 
            this.numberOfShiftsLabel2.AutoSize = true;
            this.numberOfShiftsLabel2.Location = new System.Drawing.Point(13, 191);
            this.numberOfShiftsLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberOfShiftsLabel2.Name = "numberOfShiftsLabel2";
            this.numberOfShiftsLabel2.Size = new System.Drawing.Size(123, 16);
            this.numberOfShiftsLabel2.TabIndex = 57;
            this.numberOfShiftsLabel2.Text = "Number of shifts:";
            // 
            // shiftTime2
            // 
            this.shiftTime2.AutoSize = true;
            this.shiftTime2.Location = new System.Drawing.Point(13, 139);
            this.shiftTime2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shiftTime2.Name = "shiftTime2";
            this.shiftTime2.Size = new System.Drawing.Size(180, 16);
            this.shiftTime2.TabIndex = 56;
            this.shiftTime2.Text = "Please select shift times:";
            // 
            // numberOfShiftsCombo2
            // 
            this.numberOfShiftsCombo2.FormattingEnabled = true;
            this.numberOfShiftsCombo2.Location = new System.Drawing.Point(13, 211);
            this.numberOfShiftsCombo2.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfShiftsCombo2.Name = "numberOfShiftsCombo2";
            this.numberOfShiftsCombo2.Size = new System.Drawing.Size(177, 24);
            this.numberOfShiftsCombo2.TabIndex = 55;
            // 
            // startHour2
            // 
            this.startHour2.FormattingEnabled = true;
            this.startHour2.Location = new System.Drawing.Point(13, 158);
            this.startHour2.Margin = new System.Windows.Forms.Padding(4);
            this.startHour2.Name = "startHour2";
            this.startHour2.Size = new System.Drawing.Size(83, 24);
            this.startHour2.TabIndex = 53;
            // 
            // am_pmCombo3
            // 
            this.am_pmCombo3.FormattingEnabled = true;
            this.am_pmCombo3.Location = new System.Drawing.Point(112, 158);
            this.am_pmCombo3.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo3.Name = "am_pmCombo3";
            this.am_pmCombo3.Size = new System.Drawing.Size(83, 24);
            this.am_pmCombo3.TabIndex = 54;
            // 
            // toLabel1
            // 
            this.toLabel1.AutoSize = true;
            this.toLabel1.Location = new System.Drawing.Point(209, 44);
            this.toLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.toLabel1.Name = "toLabel1";
            this.toLabel1.Size = new System.Drawing.Size(21, 16);
            this.toLabel1.TabIndex = 52;
            this.toLabel1.Text = "to";
            // 
            // endHour1
            // 
            this.endHour1.FormattingEnabled = true;
            this.endHour1.Location = new System.Drawing.Point(241, 40);
            this.endHour1.Margin = new System.Windows.Forms.Padding(4);
            this.endHour1.Name = "endHour1";
            this.endHour1.Size = new System.Drawing.Size(83, 24);
            this.endHour1.TabIndex = 50;
            // 
            // am_pmCombo2
            // 
            this.am_pmCombo2.FormattingEnabled = true;
            this.am_pmCombo2.Location = new System.Drawing.Point(339, 40);
            this.am_pmCombo2.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo2.Name = "am_pmCombo2";
            this.am_pmCombo2.Size = new System.Drawing.Size(83, 24);
            this.am_pmCombo2.TabIndex = 51;
            // 
            // numberOfShiftsLabel1
            // 
            this.numberOfShiftsLabel1.AutoSize = true;
            this.numberOfShiftsLabel1.Location = new System.Drawing.Point(13, 73);
            this.numberOfShiftsLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberOfShiftsLabel1.Name = "numberOfShiftsLabel1";
            this.numberOfShiftsLabel1.Size = new System.Drawing.Size(123, 16);
            this.numberOfShiftsLabel1.TabIndex = 49;
            this.numberOfShiftsLabel1.Text = "Number of shifts:";
            // 
            // shiftTime1
            // 
            this.shiftTime1.AutoSize = true;
            this.shiftTime1.Location = new System.Drawing.Point(13, 22);
            this.shiftTime1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shiftTime1.Name = "shiftTime1";
            this.shiftTime1.Size = new System.Drawing.Size(180, 16);
            this.shiftTime1.TabIndex = 48;
            this.shiftTime1.Text = "Please select shift times:";
            // 
            // numberOfShiftsCombo1
            // 
            this.numberOfShiftsCombo1.FormattingEnabled = true;
            this.numberOfShiftsCombo1.Location = new System.Drawing.Point(13, 93);
            this.numberOfShiftsCombo1.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfShiftsCombo1.Name = "numberOfShiftsCombo1";
            this.numberOfShiftsCombo1.Size = new System.Drawing.Size(177, 24);
            this.numberOfShiftsCombo1.TabIndex = 47;
            // 
            // startHour1
            // 
            this.startHour1.FormattingEnabled = true;
            this.startHour1.Location = new System.Drawing.Point(13, 41);
            this.startHour1.Margin = new System.Windows.Forms.Padding(4);
            this.startHour1.Name = "startHour1";
            this.startHour1.Size = new System.Drawing.Size(82, 24);
            this.startHour1.TabIndex = 45;
            // 
            // am_pmCombo1
            // 
            this.am_pmCombo1.FormattingEnabled = true;
            this.am_pmCombo1.Location = new System.Drawing.Point(111, 40);
            this.am_pmCombo1.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo1.Name = "am_pmCombo1";
            this.am_pmCombo1.Size = new System.Drawing.Size(83, 24);
            this.am_pmCombo1.TabIndex = 46;
            // 
            // LogCreator
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(728, 385);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.workProgressBar);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.createBTN);
            this.Controls.Add(this.textBox1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "LogCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Creator";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button createBTN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.ProgressBar workProgressBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label toLabel2;
        private System.Windows.Forms.ComboBox endHour2;
        private System.Windows.Forms.ComboBox am_pmCombo4;
        private System.Windows.Forms.Label numberOfShiftsLabel2;
        private System.Windows.Forms.Label shiftTime2;
        private System.Windows.Forms.ComboBox numberOfShiftsCombo2;
        private System.Windows.Forms.ComboBox startHour2;
        private System.Windows.Forms.ComboBox am_pmCombo3;
        private System.Windows.Forms.Label toLabel1;
        private System.Windows.Forms.ComboBox endHour1;
        private System.Windows.Forms.ComboBox am_pmCombo2;
        private System.Windows.Forms.Label numberOfShiftsLabel1;
        private System.Windows.Forms.Label shiftTime1;
        private System.Windows.Forms.ComboBox numberOfShiftsCombo1;
        private System.Windows.Forms.ComboBox startHour1;
        private System.Windows.Forms.ComboBox am_pmCombo1;
    }
}

