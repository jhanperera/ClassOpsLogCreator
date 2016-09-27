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
            this.endTextBox = new MetroFramework.Controls.MetroTextBox();
            this.startTextBox = new MetroFramework.Controls.MetroTextBox();
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
            // 
            // 
            // 
            this.nameTextBox.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.nameTextBox.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location")));
            this.nameTextBox.CustomButton.Name = "";
            this.nameTextBox.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size")));
            this.nameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.nameTextBox.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex")));
            this.nameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.nameTextBox.CustomButton.UseSelectable = true;
            this.nameTextBox.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible")));
            this.nameTextBox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.nameTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nameTextBox.Lines = new string[0];
            this.nameTextBox.MaxLength = 32767;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.PasswordChar = '\0';
            this.nameTextBox.PromptText = "Name";
            this.nameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.nameTextBox.SelectedText = "";
            this.nameTextBox.SelectionLength = 0;
            this.nameTextBox.SelectionStart = 0;
            this.nameTextBox.ShortcutsEnabled = true;
            this.nameTextBox.Style = MetroFramework.MetroColorStyle.Yellow;
            this.nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.nameTextBox, resources.GetString("nameTextBox.ToolTip"));
            this.nameTextBox.UseSelectable = true;
            this.nameTextBox.WaterMark = "Name";
            this.nameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.nameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // previousBTN
            // 
            resources.ApplyResources(this.previousBTN, "previousBTN");
            this.previousBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.previousBTN.Name = "previousBTN";
            this.previousBTN.UseSelectable = true;
            this.previousBTN.Click += new System.EventHandler(this.previousBTN_Click);
            // 
            // nextBTN
            // 
            resources.ApplyResources(this.nextBTN, "nextBTN");
            this.nextBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextBTN.Highlight = true;
            this.nextBTN.Name = "nextBTN";
            this.nextBTN.UseSelectable = true;
            this.nextBTN.Click += new System.EventHandler(this.nextBTN_Click_1);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // endTextBox
            // 
            resources.ApplyResources(this.endTextBox, "endTextBox");
            // 
            // 
            // 
            this.endTextBox.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.endTextBox.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location1")));
            this.endTextBox.CustomButton.Name = "";
            this.endTextBox.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size1")));
            this.endTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.endTextBox.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex1")));
            this.endTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.endTextBox.CustomButton.UseSelectable = true;
            this.endTextBox.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible1")));
            this.endTextBox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.endTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.endTextBox.Lines = new string[0];
            this.endTextBox.MaxLength = 32767;
            this.endTextBox.Name = "endTextBox";
            this.endTextBox.PasswordChar = '\0';
            this.endTextBox.PromptText = "##:##PM";
            this.endTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.endTextBox.SelectedText = "";
            this.endTextBox.SelectionLength = 0;
            this.endTextBox.SelectionStart = 0;
            this.endTextBox.ShortcutsEnabled = true;
            this.endTextBox.Style = MetroFramework.MetroColorStyle.Yellow;
            this.endTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.endTextBox, resources.GetString("endTextBox.ToolTip"));
            this.endTextBox.UseSelectable = true;
            this.endTextBox.WaterMark = "##:##PM";
            this.endTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.endTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // startTextBox
            // 
            resources.ApplyResources(this.startTextBox, "startTextBox");
            // 
            // 
            // 
            this.startTextBox.CustomButton.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.startTextBox.CustomButton.Location = ((System.Drawing.Point)(resources.GetObject("resource.Location2")));
            this.startTextBox.CustomButton.Name = "";
            this.startTextBox.CustomButton.Size = ((System.Drawing.Size)(resources.GetObject("resource.Size2")));
            this.startTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.startTextBox.CustomButton.TabIndex = ((int)(resources.GetObject("resource.TabIndex2")));
            this.startTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.startTextBox.CustomButton.UseSelectable = true;
            this.startTextBox.CustomButton.Visible = ((bool)(resources.GetObject("resource.Visible2")));
            this.startTextBox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.startTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startTextBox.Lines = new string[0];
            this.startTextBox.MaxLength = 32767;
            this.startTextBox.Name = "startTextBox";
            this.startTextBox.PasswordChar = '\0';
            this.startTextBox.PromptText = "##:##PM";
            this.startTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.startTextBox.SelectedText = "";
            this.startTextBox.SelectionLength = 0;
            this.startTextBox.SelectionStart = 0;
            this.startTextBox.ShortcutsEnabled = true;
            this.startTextBox.Style = MetroFramework.MetroColorStyle.Yellow;
            this.startTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.startTextBox, resources.GetString("startTextBox.ToolTip"));
            this.startTextBox.UseSelectable = true;
            this.startTextBox.WaterMark = "##:##PM";
            this.startTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.startTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // LogViewer
            // 
            this.AcceptButton = this.nextBTN;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ControlBox = false;
            this.Controls.Add(this.startTextBox);
            this.Controls.Add(this.endTextBox);
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
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Red;
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
        private MetroFramework.Controls.MetroTextBox endTextBox;
        private MetroFramework.Controls.MetroTextBox startTextBox;
    }
}