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
            this.startHour1 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo1 = new System.Windows.Forms.ComboBox();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // createBTN
            // 
            this.createBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createBTN.Location = new System.Drawing.Point(18, 291);
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
            this.textBox1.Location = new System.Drawing.Point(291, 312);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(482, 22);
            this.textBox1.TabIndex = 1;
            // 
            // destinationLabel
            // 
            this.destinationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.destinationLabel.AutoEllipsis = true;
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(291, 291);
            this.destinationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(148, 16);
            this.destinationLabel.TabIndex = 2;
            this.destinationLabel.Text = "Master Log location:";
            // 
            // startHour1
            // 
            this.startHour1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startHour1.FormattingEnabled = true;
            this.startHour1.Location = new System.Drawing.Point(10, 44);
            this.startHour1.Margin = new System.Windows.Forms.Padding(4);
            this.startHour1.Name = "startHour1";
            this.startHour1.Size = new System.Drawing.Size(85, 24);
            this.startHour1.TabIndex = 3;
            // 
            // am_pmCombo1
            // 
            this.am_pmCombo1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.am_pmCombo1.FormattingEnabled = true;
            this.am_pmCombo1.Location = new System.Drawing.Point(108, 43);
            this.am_pmCombo1.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo1.Name = "am_pmCombo1";
            this.am_pmCombo1.Size = new System.Drawing.Size(86, 24);
            this.am_pmCombo1.TabIndex = 5;
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainGroupBox.Controls.Add(this.pictureBox1);
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
            this.mainGroupBox.Location = new System.Drawing.Point(18, 15);
            this.mainGroupBox.Margin = new System.Windows.Forms.Padding(4);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Padding = new System.Windows.Forms.Padding(4);
            this.mainGroupBox.Size = new System.Drawing.Size(765, 268);
            this.mainGroupBox.TabIndex = 6;
            this.mainGroupBox.TabStop = false;
            this.mainGroupBox.Text = "Fill out this information:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::ClassOpsLogCreator.Properties.Resources.www_zaxonusa_com_small_icon_schedual;
            this.pictureBox1.Location = new System.Drawing.Point(452, 17);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(297, 232);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // toLabel2
            // 
            this.toLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toLabel2.AutoSize = true;
            this.toLabel2.Location = new System.Drawing.Point(204, 164);
            this.toLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.toLabel2.Name = "toLabel2";
            this.toLabel2.Size = new System.Drawing.Size(21, 16);
            this.toLabel2.TabIndex = 28;
            this.toLabel2.Text = "to";
            // 
            // endHour2
            // 
            this.endHour2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endHour2.FormattingEnabled = true;
            this.endHour2.Location = new System.Drawing.Point(236, 160);
            this.endHour2.Margin = new System.Windows.Forms.Padding(4);
            this.endHour2.Name = "endHour2";
            this.endHour2.Size = new System.Drawing.Size(86, 24);
            this.endHour2.TabIndex = 25;
            // 
            // am_pmCombo4
            // 
            this.am_pmCombo4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.am_pmCombo4.FormattingEnabled = true;
            this.am_pmCombo4.Location = new System.Drawing.Point(334, 160);
            this.am_pmCombo4.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo4.Name = "am_pmCombo4";
            this.am_pmCombo4.Size = new System.Drawing.Size(86, 24);
            this.am_pmCombo4.TabIndex = 27;
            // 
            // numberOfShiftsLabel2
            // 
            this.numberOfShiftsLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberOfShiftsLabel2.AutoSize = true;
            this.numberOfShiftsLabel2.Location = new System.Drawing.Point(7, 197);
            this.numberOfShiftsLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberOfShiftsLabel2.Name = "numberOfShiftsLabel2";
            this.numberOfShiftsLabel2.Size = new System.Drawing.Size(123, 16);
            this.numberOfShiftsLabel2.TabIndex = 24;
            this.numberOfShiftsLabel2.Text = "Number of shifts:";
            // 
            // shiftTime2
            // 
            this.shiftTime2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftTime2.AutoSize = true;
            this.shiftTime2.Location = new System.Drawing.Point(7, 145);
            this.shiftTime2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shiftTime2.Name = "shiftTime2";
            this.shiftTime2.Size = new System.Drawing.Size(180, 16);
            this.shiftTime2.TabIndex = 23;
            this.shiftTime2.Text = "Please select shift times:";
            // 
            // numberOfShiftsCombo2
            // 
            this.numberOfShiftsCombo2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberOfShiftsCombo2.FormattingEnabled = true;
            this.numberOfShiftsCombo2.Location = new System.Drawing.Point(7, 217);
            this.numberOfShiftsCombo2.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfShiftsCombo2.Name = "numberOfShiftsCombo2";
            this.numberOfShiftsCombo2.Size = new System.Drawing.Size(180, 24);
            this.numberOfShiftsCombo2.TabIndex = 22;
            // 
            // startHour2
            // 
            this.startHour2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startHour2.FormattingEnabled = true;
            this.startHour2.Location = new System.Drawing.Point(7, 164);
            this.startHour2.Margin = new System.Windows.Forms.Padding(4);
            this.startHour2.Name = "startHour2";
            this.startHour2.Size = new System.Drawing.Size(86, 24);
            this.startHour2.TabIndex = 19;
            // 
            // am_pmCombo3
            // 
            this.am_pmCombo3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.am_pmCombo3.FormattingEnabled = true;
            this.am_pmCombo3.Location = new System.Drawing.Point(106, 164);
            this.am_pmCombo3.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo3.Name = "am_pmCombo3";
            this.am_pmCombo3.Size = new System.Drawing.Size(86, 24);
            this.am_pmCombo3.TabIndex = 21;
            // 
            // toLabel1
            // 
            this.toLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toLabel1.AutoSize = true;
            this.toLabel1.Location = new System.Drawing.Point(206, 47);
            this.toLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.toLabel1.Name = "toLabel1";
            this.toLabel1.Size = new System.Drawing.Size(21, 16);
            this.toLabel1.TabIndex = 18;
            this.toLabel1.Text = "to";
            // 
            // endHour1
            // 
            this.endHour1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endHour1.FormattingEnabled = true;
            this.endHour1.Location = new System.Drawing.Point(238, 43);
            this.endHour1.Margin = new System.Windows.Forms.Padding(4);
            this.endHour1.Name = "endHour1";
            this.endHour1.Size = new System.Drawing.Size(86, 24);
            this.endHour1.TabIndex = 15;
            // 
            // am_pmCombo2
            // 
            this.am_pmCombo2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.am_pmCombo2.FormattingEnabled = true;
            this.am_pmCombo2.Location = new System.Drawing.Point(336, 43);
            this.am_pmCombo2.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo2.Name = "am_pmCombo2";
            this.am_pmCombo2.Size = new System.Drawing.Size(86, 24);
            this.am_pmCombo2.TabIndex = 17;
            // 
            // numberOfShiftsLabel1
            // 
            this.numberOfShiftsLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberOfShiftsLabel1.AutoSize = true;
            this.numberOfShiftsLabel1.Location = new System.Drawing.Point(10, 76);
            this.numberOfShiftsLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberOfShiftsLabel1.Name = "numberOfShiftsLabel1";
            this.numberOfShiftsLabel1.Size = new System.Drawing.Size(123, 16);
            this.numberOfShiftsLabel1.TabIndex = 8;
            this.numberOfShiftsLabel1.Text = "Number of shifts:";
            // 
            // shiftTime1
            // 
            this.shiftTime1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shiftTime1.AutoSize = true;
            this.shiftTime1.Location = new System.Drawing.Point(10, 25);
            this.shiftTime1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shiftTime1.Name = "shiftTime1";
            this.shiftTime1.Size = new System.Drawing.Size(180, 16);
            this.shiftTime1.TabIndex = 7;
            this.shiftTime1.Text = "Please select shift times:";
            // 
            // numberOfShiftsCombo1
            // 
            this.numberOfShiftsCombo1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberOfShiftsCombo1.FormattingEnabled = true;
            this.numberOfShiftsCombo1.Location = new System.Drawing.Point(10, 96);
            this.numberOfShiftsCombo1.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfShiftsCombo1.Name = "numberOfShiftsCombo1";
            this.numberOfShiftsCombo1.Size = new System.Drawing.Size(180, 24);
            this.numberOfShiftsCombo1.TabIndex = 6;
            // 
            // workProgressBar
            // 
            this.workProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.workProgressBar.Location = new System.Drawing.Point(0, 342);
            this.workProgressBar.Margin = new System.Windows.Forms.Padding(4);
            this.workProgressBar.Name = "workProgressBar";
            this.workProgressBar.Size = new System.Drawing.Size(793, 25);
            this.workProgressBar.TabIndex = 7;
            // 
            // LogCreator
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(793, 367);
            this.Controls.Add(this.workProgressBar);
            this.Controls.Add(this.mainGroupBox);
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
            this.mainGroupBox.ResumeLayout(false);
            this.mainGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

