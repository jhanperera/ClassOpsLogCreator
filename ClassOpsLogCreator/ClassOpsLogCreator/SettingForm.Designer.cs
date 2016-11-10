namespace ClassOpsLogCreator
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.usernameLabel = new MetroFramework.Controls.MetroLabel();
            this.passwordLabel = new MetroFramework.Controls.MetroLabel();
            this.usernameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.passwordTextBox = new MetroFramework.Controls.MetroTextBox();
            this.loginBTN = new MetroFramework.Controls.MetroButton();
            this.cancelBTN = new MetroFramework.Controls.MetroButton();
            this.versionLabel = new MetroFramework.Controls.MetroLabel();
            this.emailLoginTab = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.createBTN = new MetroFramework.Controls.MetroButton();
            this.am_pmCombo1_2 = new MetroFramework.Controls.MetroComboBox();
            this.endHour1 = new MetroFramework.Controls.MetroComboBox();
            this.toLabel1 = new MetroFramework.Controls.MetroLabel();
            this.am_pmCombo1_1 = new MetroFramework.Controls.MetroComboBox();
            this.startHour1 = new MetroFramework.Controls.MetroComboBox();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.statisticsTab = new MetroFramework.Controls.MetroTabPage();
            this.dateTimePicker = new MetroFramework.Controls.MetroDateTime();
            this.generateBTN = new MetroFramework.Controls.MetroButton();
            this.yearlyRadio = new MetroFramework.Controls.MetroRadioButton();
            this.monthlyRadio = new MetroFramework.Controls.MetroRadioButton();
            this.weeklyRadio = new MetroFramework.Controls.MetroRadioButton();
            this.selectorLabel = new MetroFramework.Controls.MetroLabel();
            this.buildingUpdateTab = new MetroFramework.Controls.MetroTabPage();
            this.buildingDataGridView = new System.Windows.Forms.DataGridView();
            this.emailLoginTab.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.statisticsTab.SuspendLayout();
            this.buildingUpdateTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildingDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(87, 38);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(68, 19);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(87, 83);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(63, 19);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password";
            // 
            // usernameTextBox
            // 
            // 
            // 
            // 
            this.usernameTextBox.CustomButton.Image = null;
            this.usernameTextBox.CustomButton.Location = new System.Drawing.Point(214, 2);
            this.usernameTextBox.CustomButton.Name = "";
            this.usernameTextBox.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.usernameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.usernameTextBox.CustomButton.TabIndex = 1;
            this.usernameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.usernameTextBox.CustomButton.UseSelectable = true;
            this.usernameTextBox.CustomButton.Visible = false;
            this.usernameTextBox.Lines = new string[0];
            this.usernameTextBox.Location = new System.Drawing.Point(182, 38);
            this.usernameTextBox.MaxLength = 32767;
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.PasswordChar = '\0';
            this.usernameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.usernameTextBox.SelectedText = "";
            this.usernameTextBox.SelectionLength = 0;
            this.usernameTextBox.SelectionStart = 0;
            this.usernameTextBox.ShortcutsEnabled = true;
            this.usernameTextBox.Size = new System.Drawing.Size(234, 22);
            this.usernameTextBox.TabIndex = 0;
            this.usernameTextBox.UseSelectable = true;
            this.usernameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.usernameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // passwordTextBox
            // 
            // 
            // 
            // 
            this.passwordTextBox.CustomButton.Image = null;
            this.passwordTextBox.CustomButton.Location = new System.Drawing.Point(214, 2);
            this.passwordTextBox.CustomButton.Name = "";
            this.passwordTextBox.CustomButton.Size = new System.Drawing.Size(17, 17);
            this.passwordTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.passwordTextBox.CustomButton.TabIndex = 1;
            this.passwordTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.passwordTextBox.CustomButton.UseSelectable = true;
            this.passwordTextBox.CustomButton.Visible = false;
            this.passwordTextBox.Lines = new string[0];
            this.passwordTextBox.Location = new System.Drawing.Point(182, 83);
            this.passwordTextBox.MaxLength = 32767;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.passwordTextBox.SelectedText = "";
            this.passwordTextBox.SelectionLength = 0;
            this.passwordTextBox.SelectionStart = 0;
            this.passwordTextBox.ShortcutsEnabled = true;
            this.passwordTextBox.Size = new System.Drawing.Size(234, 22);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.UseSelectable = true;
            this.passwordTextBox.UseSystemPasswordChar = true;
            this.passwordTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.passwordTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // loginBTN
            // 
            this.loginBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginBTN.Highlight = true;
            this.loginBTN.Location = new System.Drawing.Point(95, 133);
            this.loginBTN.Name = "loginBTN";
            this.loginBTN.Size = new System.Drawing.Size(80, 22);
            this.loginBTN.TabIndex = 2;
            this.loginBTN.Text = "Login";
            this.loginBTN.UseSelectable = true;
            this.loginBTN.Click += new System.EventHandler(this.loginBTN_Click);
            // 
            // cancelBTN
            // 
            this.cancelBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelBTN.Location = new System.Drawing.Point(335, 133);
            this.cancelBTN.Name = "cancelBTN";
            this.cancelBTN.Size = new System.Drawing.Size(80, 22);
            this.cancelBTN.TabIndex = 5;
            this.cancelBTN.Text = "Cancel";
            this.cancelBTN.UseSelectable = true;
            this.cancelBTN.Click += new System.EventHandler(this.cancelBTN_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.versionLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.versionLabel.Location = new System.Drawing.Point(187, 87);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(79, 25);
            this.versionLabel.TabIndex = 6;
            this.versionLabel.Text = "Version: ";
            // 
            // emailLoginTab
            // 
            this.emailLoginTab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.emailLoginTab.Controls.Add(this.metroTabPage3);
            this.emailLoginTab.Controls.Add(this.metroTabPage2);
            this.emailLoginTab.Controls.Add(this.metroTabPage1);
            this.emailLoginTab.Controls.Add(this.statisticsTab);
            this.emailLoginTab.Controls.Add(this.buildingUpdateTab);
            this.emailLoginTab.Location = new System.Drawing.Point(25, 63);
            this.emailLoginTab.Name = "emailLoginTab";
            this.emailLoginTab.SelectedIndex = 0;
            this.emailLoginTab.Size = new System.Drawing.Size(510, 238);
            this.emailLoginTab.Style = MetroFramework.MetroColorStyle.Red;
            this.emailLoginTab.TabIndex = 7;
            this.emailLoginTab.UseSelectable = true;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.versionLabel);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 41);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(502, 193);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Version";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.createBTN);
            this.metroTabPage2.Controls.Add(this.am_pmCombo1_2);
            this.metroTabPage2.Controls.Add(this.endHour1);
            this.metroTabPage2.Controls.Add(this.toLabel1);
            this.metroTabPage2.Controls.Add(this.am_pmCombo1_1);
            this.metroTabPage2.Controls.Add(this.startHour1);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 41);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(502, 193);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Logout Generator";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // createBTN
            // 
            this.createBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createBTN.Location = new System.Drawing.Point(210, 113);
            this.createBTN.Name = "createBTN";
            this.createBTN.Size = new System.Drawing.Size(87, 27);
            this.createBTN.TabIndex = 18;
            this.createBTN.Text = "Create";
            this.createBTN.UseSelectable = true;
            this.createBTN.Click += new System.EventHandler(this.createBTN_Click);
            // 
            // am_pmCombo1_2
            // 
            this.am_pmCombo1_2.FormattingEnabled = true;
            this.am_pmCombo1_2.ItemHeight = 23;
            this.am_pmCombo1_2.Location = new System.Drawing.Point(362, 52);
            this.am_pmCombo1_2.Name = "am_pmCombo1_2";
            this.am_pmCombo1_2.Size = new System.Drawing.Size(53, 29);
            this.am_pmCombo1_2.TabIndex = 15;
            this.am_pmCombo1_2.TabStop = false;
            this.am_pmCombo1_2.UseSelectable = true;
            // 
            // endHour1
            // 
            this.endHour1.FormattingEnabled = true;
            this.endHour1.ItemHeight = 23;
            this.endHour1.Location = new System.Drawing.Point(268, 52);
            this.endHour1.Name = "endHour1";
            this.endHour1.Size = new System.Drawing.Size(88, 29);
            this.endHour1.TabIndex = 14;
            this.endHour1.TabStop = false;
            this.endHour1.UseSelectable = true;
            // 
            // toLabel1
            // 
            this.toLabel1.AutoSize = true;
            this.toLabel1.Location = new System.Drawing.Point(241, 54);
            this.toLabel1.Name = "toLabel1";
            this.toLabel1.Size = new System.Drawing.Size(21, 19);
            this.toLabel1.TabIndex = 17;
            this.toLabel1.Text = "to";
            // 
            // am_pmCombo1_1
            // 
            this.am_pmCombo1_1.FormattingEnabled = true;
            this.am_pmCombo1_1.ItemHeight = 23;
            this.am_pmCombo1_1.Location = new System.Drawing.Point(182, 52);
            this.am_pmCombo1_1.Name = "am_pmCombo1_1";
            this.am_pmCombo1_1.Size = new System.Drawing.Size(53, 29);
            this.am_pmCombo1_1.TabIndex = 13;
            this.am_pmCombo1_1.TabStop = false;
            this.am_pmCombo1_1.UseSelectable = true;
            // 
            // startHour1
            // 
            this.startHour1.ItemHeight = 23;
            this.startHour1.Location = new System.Drawing.Point(88, 52);
            this.startHour1.Name = "startHour1";
            this.startHour1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.startHour1.Size = new System.Drawing.Size(88, 29);
            this.startHour1.TabIndex = 12;
            this.startHour1.TabStop = false;
            this.startHour1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.cancelBTN);
            this.metroTabPage1.Controls.Add(this.usernameLabel);
            this.metroTabPage1.Controls.Add(this.loginBTN);
            this.metroTabPage1.Controls.Add(this.passwordLabel);
            this.metroTabPage1.Controls.Add(this.passwordTextBox);
            this.metroTabPage1.Controls.Add(this.usernameTextBox);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 41);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(502, 193);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Email Login";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // statisticsTab
            // 
            this.statisticsTab.Controls.Add(this.dateTimePicker);
            this.statisticsTab.Controls.Add(this.generateBTN);
            this.statisticsTab.Controls.Add(this.yearlyRadio);
            this.statisticsTab.Controls.Add(this.monthlyRadio);
            this.statisticsTab.Controls.Add(this.weeklyRadio);
            this.statisticsTab.Controls.Add(this.selectorLabel);
            this.statisticsTab.HorizontalScrollbarBarColor = true;
            this.statisticsTab.HorizontalScrollbarHighlightOnWheel = false;
            this.statisticsTab.HorizontalScrollbarSize = 10;
            this.statisticsTab.Location = new System.Drawing.Point(4, 41);
            this.statisticsTab.Name = "statisticsTab";
            this.statisticsTab.Size = new System.Drawing.Size(502, 193);
            this.statisticsTab.TabIndex = 4;
            this.statisticsTab.Text = "Statistics";
            this.statisticsTab.VerticalScrollbarBarColor = true;
            this.statisticsTab.VerticalScrollbarHighlightOnWheel = false;
            this.statisticsTab.VerticalScrollbarSize = 10;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dateTimePicker.Location = new System.Drawing.Point(216, 73);
            this.dateTimePicker.MinimumSize = new System.Drawing.Size(4, 29);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 29);
            this.dateTimePicker.Style = MetroFramework.MetroColorStyle.Red;
            this.dateTimePicker.TabIndex = 8;
            // 
            // generateBTN
            // 
            this.generateBTN.Location = new System.Drawing.Point(197, 126);
            this.generateBTN.Name = "generateBTN";
            this.generateBTN.Size = new System.Drawing.Size(117, 35);
            this.generateBTN.TabIndex = 7;
            this.generateBTN.Text = "Generate Statistics";
            this.generateBTN.UseSelectable = true;
            this.generateBTN.Click += new System.EventHandler(this.generateBTN_Click);
            // 
            // yearlyRadio
            // 
            this.yearlyRadio.AutoSize = true;
            this.yearlyRadio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.yearlyRadio.Location = new System.Drawing.Point(295, 26);
            this.yearlyRadio.Name = "yearlyRadio";
            this.yearlyRadio.Size = new System.Drawing.Size(61, 19);
            this.yearlyRadio.TabIndex = 6;
            this.yearlyRadio.Text = "Yearly";
            this.yearlyRadio.UseSelectable = true;
            // 
            // monthlyRadio
            // 
            this.monthlyRadio.AutoSize = true;
            this.monthlyRadio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.monthlyRadio.Location = new System.Drawing.Point(212, 26);
            this.monthlyRadio.Name = "monthlyRadio";
            this.monthlyRadio.Size = new System.Drawing.Size(77, 19);
            this.monthlyRadio.TabIndex = 5;
            this.monthlyRadio.Text = "Monthly";
            this.monthlyRadio.UseSelectable = true;
            // 
            // weeklyRadio
            // 
            this.weeklyRadio.AutoSize = true;
            this.weeklyRadio.Checked = true;
            this.weeklyRadio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.weeklyRadio.Location = new System.Drawing.Point(138, 26);
            this.weeklyRadio.Name = "weeklyRadio";
            this.weeklyRadio.Size = new System.Drawing.Size(68, 19);
            this.weeklyRadio.TabIndex = 4;
            this.weeklyRadio.TabStop = true;
            this.weeklyRadio.Text = "Weekly";
            this.weeklyRadio.UseSelectable = true;
            // 
            // selectorLabel
            // 
            this.selectorLabel.AutoSize = true;
            this.selectorLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.selectorLabel.Location = new System.Drawing.Point(87, 73);
            this.selectorLabel.Name = "selectorLabel";
            this.selectorLabel.Size = new System.Drawing.Size(106, 25);
            this.selectorLabel.TabIndex = 3;
            this.selectorLabel.Text = "Select a day:";
            // 
            // buildingUpdateTab
            // 
            this.buildingUpdateTab.Controls.Add(this.buildingDataGridView);
            this.buildingUpdateTab.HorizontalScrollbarBarColor = true;
            this.buildingUpdateTab.HorizontalScrollbarHighlightOnWheel = false;
            this.buildingUpdateTab.HorizontalScrollbarSize = 10;
            this.buildingUpdateTab.Location = new System.Drawing.Point(4, 41);
            this.buildingUpdateTab.Name = "buildingUpdateTab";
            this.buildingUpdateTab.Size = new System.Drawing.Size(502, 193);
            this.buildingUpdateTab.TabIndex = 3;
            this.buildingUpdateTab.Text = "Edit Buildings";
            this.buildingUpdateTab.VerticalScrollbarBarColor = true;
            this.buildingUpdateTab.VerticalScrollbarHighlightOnWheel = false;
            this.buildingUpdateTab.VerticalScrollbarSize = 10;
            // 
            // buildingDataGridView
            // 
            this.buildingDataGridView.AllowUserToDeleteRows = false;
            this.buildingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.buildingDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buildingDataGridView.Location = new System.Drawing.Point(0, 0);
            this.buildingDataGridView.Name = "buildingDataGridView";
            this.buildingDataGridView.Size = new System.Drawing.Size(502, 193);
            this.buildingDataGridView.TabIndex = 2;
            this.buildingDataGridView.Visible = false;
            // 
            // SettingForm
            // 
            this.AcceptButton = this.loginBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(560, 324);
            this.Controls.Add(this.emailLoginTab);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Padding = new System.Windows.Forms.Padding(22, 60, 22, 20);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Settings";
            this.TopMost = true;
            this.emailLoginTab.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.statisticsTab.ResumeLayout(false);
            this.statisticsTab.PerformLayout();
            this.buildingUpdateTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buildingDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel usernameLabel;
        private MetroFramework.Controls.MetroLabel passwordLabel;
        private MetroFramework.Controls.MetroTextBox usernameTextBox;
        private MetroFramework.Controls.MetroTextBox passwordTextBox;
        private MetroFramework.Controls.MetroButton loginBTN;
        private MetroFramework.Controls.MetroButton cancelBTN;
        private MetroFramework.Controls.MetroLabel versionLabel;
        private MetroFramework.Controls.MetroTabControl emailLoginTab;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroButton createBTN;
        private MetroFramework.Controls.MetroComboBox am_pmCombo1_2;
        private MetroFramework.Controls.MetroComboBox endHour1;
        private MetroFramework.Controls.MetroLabel toLabel1;
        private MetroFramework.Controls.MetroComboBox am_pmCombo1_1;
        private MetroFramework.Controls.MetroComboBox startHour1;
        private MetroFramework.Controls.MetroTabPage buildingUpdateTab;
        private MetroFramework.Controls.MetroTabPage statisticsTab;
        private MetroFramework.Controls.MetroRadioButton yearlyRadio;
        private MetroFramework.Controls.MetroRadioButton monthlyRadio;
        private MetroFramework.Controls.MetroRadioButton weeklyRadio;
        private MetroFramework.Controls.MetroLabel selectorLabel;
        private MetroFramework.Controls.MetroButton generateBTN;
        private MetroFramework.Controls.MetroDateTime dateTimePicker;
        private System.Windows.Forms.DataGridView buildingDataGridView;
    }
}