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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.createBTN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.startHour1 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo1 = new System.Windows.Forms.ComboBox();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
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
            this.workProgressBar = new System.Windows.Forms.ProgressBar();
            this.mainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // createBTN
            // 
            this.createBTN.Location = new System.Drawing.Point(12, 273);
            this.createBTN.Name = "createBTN";
            this.createBTN.Size = new System.Drawing.Size(176, 37);
            this.createBTN.TabIndex = 0;
            this.createBTN.Text = "Create Master Logout Log";
            this.createBTN.UseVisualStyleBackColor = true;
            this.createBTN.Click += new System.EventHandler(this.createBTN_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(194, 290);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(323, 20);
            this.textBox1.TabIndex = 1;
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoEllipsis = true;
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(194, 273);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(103, 13);
            this.destinationLabel.TabIndex = 2;
            this.destinationLabel.Text = "Master Log location:";
            // 
            // startHour1
            // 
            this.startHour1.FormattingEnabled = true;
            this.startHour1.Location = new System.Drawing.Point(7, 36);
            this.startHour1.Name = "startHour1";
            this.startHour1.Size = new System.Drawing.Size(58, 21);
            this.startHour1.TabIndex = 3;
            // 
            // am_pmCombo1
            // 
            this.am_pmCombo1.FormattingEnabled = true;
            this.am_pmCombo1.Location = new System.Drawing.Point(72, 35);
            this.am_pmCombo1.Name = "am_pmCombo1";
            this.am_pmCombo1.Size = new System.Drawing.Size(59, 21);
            this.am_pmCombo1.TabIndex = 5;
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Controls.Add(this.toLabel2);
            this.mainGroupBox.Controls.Add(this.endHour2);
            this.mainGroupBox.Controls.Add(this.am_pmCombo4);
            this.mainGroupBox.Controls.Add(this.numberOfShiftsLabel2);
            this.mainGroupBox.Controls.Add(this.shiftTime2);
            this.mainGroupBox.Controls.Add(this.numberOfShiftsCombo2);
            this.mainGroupBox.Controls.Add(this.startHour2);
            this.mainGroupBox.Controls.Add(this.am_pmCombo3);
            this.mainGroupBox.Controls.Add(this.toLabel1);
            this.mainGroupBox.Controls.Add(this.endHour1);
            this.mainGroupBox.Controls.Add(this.am_pmCombo2);
            this.mainGroupBox.Controls.Add(this.numberOfShiftsLabel1);
            this.mainGroupBox.Controls.Add(this.shiftTime1);
            this.mainGroupBox.Controls.Add(this.numberOfShiftsCombo1);
            this.mainGroupBox.Controls.Add(this.startHour1);
            this.mainGroupBox.Controls.Add(this.am_pmCombo1);
            this.mainGroupBox.Location = new System.Drawing.Point(12, 12);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(505, 255);
            this.mainGroupBox.TabIndex = 6;
            this.mainGroupBox.TabStop = false;
            this.mainGroupBox.Text = "Fill out this information:";
            // 
            // toLabel2
            // 
            this.toLabel2.AutoSize = true;
            this.toLabel2.Location = new System.Drawing.Point(137, 142);
            this.toLabel2.Name = "toLabel2";
            this.toLabel2.Size = new System.Drawing.Size(16, 13);
            this.toLabel2.TabIndex = 28;
            this.toLabel2.Text = "to";
            // 
            // endHour2
            // 
            this.endHour2.FormattingEnabled = true;
            this.endHour2.Location = new System.Drawing.Point(159, 139);
            this.endHour2.Name = "endHour2";
            this.endHour2.Size = new System.Drawing.Size(59, 21);
            this.endHour2.TabIndex = 25;
            // 
            // am_pmCombo4
            // 
            this.am_pmCombo4.FormattingEnabled = true;
            this.am_pmCombo4.Location = new System.Drawing.Point(224, 139);
            this.am_pmCombo4.Name = "am_pmCombo4";
            this.am_pmCombo4.Size = new System.Drawing.Size(59, 21);
            this.am_pmCombo4.TabIndex = 27;
            // 
            // numberOfShiftsLabel2
            // 
            this.numberOfShiftsLabel2.AutoSize = true;
            this.numberOfShiftsLabel2.Location = new System.Drawing.Point(6, 169);
            this.numberOfShiftsLabel2.Name = "numberOfShiftsLabel2";
            this.numberOfShiftsLabel2.Size = new System.Drawing.Size(86, 13);
            this.numberOfShiftsLabel2.TabIndex = 24;
            this.numberOfShiftsLabel2.Text = "Number of shifts:";
            // 
            // shiftTime2
            // 
            this.shiftTime2.AutoSize = true;
            this.shiftTime2.Location = new System.Drawing.Point(6, 127);
            this.shiftTime2.Name = "shiftTime2";
            this.shiftTime2.Size = new System.Drawing.Size(122, 13);
            this.shiftTime2.TabIndex = 23;
            this.shiftTime2.Text = "Please select shift times:";
            // 
            // numberOfShiftsCombo2
            // 
            this.numberOfShiftsCombo2.FormattingEnabled = true;
            this.numberOfShiftsCombo2.Location = new System.Drawing.Point(6, 185);
            this.numberOfShiftsCombo2.Name = "numberOfShiftsCombo2";
            this.numberOfShiftsCombo2.Size = new System.Drawing.Size(121, 21);
            this.numberOfShiftsCombo2.TabIndex = 22;
            // 
            // startHour2
            // 
            this.startHour2.FormattingEnabled = true;
            this.startHour2.Location = new System.Drawing.Point(6, 142);
            this.startHour2.Name = "startHour2";
            this.startHour2.Size = new System.Drawing.Size(59, 21);
            this.startHour2.TabIndex = 19;
            // 
            // am_pmCombo3
            // 
            this.am_pmCombo3.FormattingEnabled = true;
            this.am_pmCombo3.Location = new System.Drawing.Point(72, 142);
            this.am_pmCombo3.Name = "am_pmCombo3";
            this.am_pmCombo3.Size = new System.Drawing.Size(59, 21);
            this.am_pmCombo3.TabIndex = 21;
            // 
            // toLabel1
            // 
            this.toLabel1.AutoSize = true;
            this.toLabel1.Location = new System.Drawing.Point(137, 38);
            this.toLabel1.Name = "toLabel1";
            this.toLabel1.Size = new System.Drawing.Size(16, 13);
            this.toLabel1.TabIndex = 18;
            this.toLabel1.Text = "to";
            // 
            // endHour1
            // 
            this.endHour1.FormattingEnabled = true;
            this.endHour1.Location = new System.Drawing.Point(159, 35);
            this.endHour1.Name = "endHour1";
            this.endHour1.Size = new System.Drawing.Size(59, 21);
            this.endHour1.TabIndex = 15;
            // 
            // am_pmCombo2
            // 
            this.am_pmCombo2.FormattingEnabled = true;
            this.am_pmCombo2.Location = new System.Drawing.Point(224, 35);
            this.am_pmCombo2.Name = "am_pmCombo2";
            this.am_pmCombo2.Size = new System.Drawing.Size(59, 21);
            this.am_pmCombo2.TabIndex = 17;
            // 
            // numberOfShiftsLabel1
            // 
            this.numberOfShiftsLabel1.AutoSize = true;
            this.numberOfShiftsLabel1.Location = new System.Drawing.Point(7, 62);
            this.numberOfShiftsLabel1.Name = "numberOfShiftsLabel1";
            this.numberOfShiftsLabel1.Size = new System.Drawing.Size(86, 13);
            this.numberOfShiftsLabel1.TabIndex = 8;
            this.numberOfShiftsLabel1.Text = "Number of shifts:";
            // 
            // shiftTime1
            // 
            this.shiftTime1.AutoSize = true;
            this.shiftTime1.Location = new System.Drawing.Point(7, 20);
            this.shiftTime1.Name = "shiftTime1";
            this.shiftTime1.Size = new System.Drawing.Size(122, 13);
            this.shiftTime1.TabIndex = 7;
            this.shiftTime1.Text = "Please select shift times:";
            // 
            // numberOfShiftsCombo1
            // 
            this.numberOfShiftsCombo1.FormattingEnabled = true;
            this.numberOfShiftsCombo1.Location = new System.Drawing.Point(7, 78);
            this.numberOfShiftsCombo1.Name = "numberOfShiftsCombo1";
            this.numberOfShiftsCombo1.Size = new System.Drawing.Size(121, 21);
            this.numberOfShiftsCombo1.TabIndex = 6;
            // 
            // workProgressBar
            // 
            this.workProgressBar.Location = new System.Drawing.Point(12, 316);
            this.workProgressBar.Name = "workProgressBar";
            this.workProgressBar.Size = new System.Drawing.Size(505, 23);
            this.workProgressBar.TabIndex = 7;
            // 
            // LogCreator
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(524, 344);
            this.Controls.Add(this.workProgressBar);
            this.Controls.Add(this.mainGroupBox);
            this.Controls.Add(this.destinationLabel);
            this.Controls.Add(this.createBTN);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LogCreator";
            this.Text = "Log Creator";
            this.mainGroupBox.ResumeLayout(false);
            this.mainGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button createBTN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.ComboBox startHour1;
        private System.Windows.Forms.ComboBox am_pmCombo1;
        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.Label numberOfShiftsLabel1;
        private System.Windows.Forms.Label shiftTime1;
        private System.Windows.Forms.ComboBox numberOfShiftsCombo1;
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
        private System.Windows.Forms.ProgressBar workProgressBar;
    }
}

