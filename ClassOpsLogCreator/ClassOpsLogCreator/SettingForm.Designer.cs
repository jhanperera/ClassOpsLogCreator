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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.am_pmCombo1_2 = new MetroFramework.Controls.MetroComboBox();
            this.endHour1 = new MetroFramework.Controls.MetroComboBox();
            this.toLabel1 = new MetroFramework.Controls.MetroLabel();
            this.am_pmCombo1_1 = new MetroFramework.Controls.MetroComboBox();
            this.startHour1 = new MetroFramework.Controls.MetroComboBox();
            this.createBTN = new MetroFramework.Controls.MetroButton();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(3, 15);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(68, 19);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(3, 60);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(64, 19);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(98, 15);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(234, 22);
            this.usernameTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(98, 60);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.Size = new System.Drawing.Size(234, 22);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // loginBTN
            // 
            this.loginBTN.Highlight = true;
            this.loginBTN.Location = new System.Drawing.Point(11, 110);
            this.loginBTN.Name = "loginBTN";
            this.loginBTN.Size = new System.Drawing.Size(80, 22);
            this.loginBTN.TabIndex = 2;
            this.loginBTN.Text = "Login";
            this.loginBTN.Click += new System.EventHandler(this.loginBTN_Click);
            // 
            // cancelBTN
            // 
            this.cancelBTN.Location = new System.Drawing.Point(251, 110);
            this.cancelBTN.Name = "cancelBTN";
            this.cancelBTN.Size = new System.Drawing.Size(80, 22);
            this.cancelBTN.TabIndex = 5;
            this.cancelBTN.Text = "Cancel";
            this.cancelBTN.Click += new System.EventHandler(this.cancelBTN_Click);
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(140, 61);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(59, 19);
            this.versionLabel.TabIndex = 6;
            this.versionLabel.Text = "Version: ";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(25, 63);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(394, 179);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTabControl1.TabIndex = 7;
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
            this.metroTabPage1.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(386, 140);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Email Login";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
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
            this.metroTabPage2.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(386, 140);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Logout Generator";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.versionLabel);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 35);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(386, 140);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Version";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            // 
            // am_pmCombo1_2
            // 
            this.am_pmCombo1_2.FontWeight = MetroFramework.MetroLinkWeight.Bold;
            this.am_pmCombo1_2.FormattingEnabled = true;
            this.am_pmCombo1_2.ItemHeight = 23;
            this.am_pmCombo1_2.Location = new System.Drawing.Point(304, 31);
            this.am_pmCombo1_2.Name = "am_pmCombo1_2";
            this.am_pmCombo1_2.Size = new System.Drawing.Size(53, 29);
            this.am_pmCombo1_2.TabIndex = 15;
            this.am_pmCombo1_2.TabStop = false;
            // 
            // endHour1
            // 
            this.endHour1.FontWeight = MetroFramework.MetroLinkWeight.Bold;
            this.endHour1.FormattingEnabled = true;
            this.endHour1.ItemHeight = 23;
            this.endHour1.Location = new System.Drawing.Point(210, 31);
            this.endHour1.Name = "endHour1";
            this.endHour1.Size = new System.Drawing.Size(88, 29);
            this.endHour1.TabIndex = 14;
            this.endHour1.TabStop = false;
            // 
            // toLabel1
            // 
            this.toLabel1.AutoSize = true;
            this.toLabel1.Location = new System.Drawing.Point(183, 33);
            this.toLabel1.Name = "toLabel1";
            this.toLabel1.Size = new System.Drawing.Size(21, 19);
            this.toLabel1.TabIndex = 17;
            this.toLabel1.Text = "to";
            // 
            // am_pmCombo1_1
            // 
            this.am_pmCombo1_1.FontWeight = MetroFramework.MetroLinkWeight.Bold;
            this.am_pmCombo1_1.FormattingEnabled = true;
            this.am_pmCombo1_1.ItemHeight = 23;
            this.am_pmCombo1_1.Location = new System.Drawing.Point(124, 31);
            this.am_pmCombo1_1.Name = "am_pmCombo1_1";
            this.am_pmCombo1_1.Size = new System.Drawing.Size(53, 29);
            this.am_pmCombo1_1.TabIndex = 13;
            this.am_pmCombo1_1.TabStop = false;
            // 
            // startHour1
            // 
            this.startHour1.FontWeight = MetroFramework.MetroLinkWeight.Bold;
            this.startHour1.ItemHeight = 23;
            this.startHour1.Location = new System.Drawing.Point(30, 31);
            this.startHour1.Name = "startHour1";
            this.startHour1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.startHour1.Size = new System.Drawing.Size(88, 29);
            this.startHour1.TabIndex = 12;
            this.startHour1.TabStop = false;
            // 
            // createBTN
            // 
            this.createBTN.Location = new System.Drawing.Point(152, 92);
            this.createBTN.Name = "createBTN";
            this.createBTN.Size = new System.Drawing.Size(87, 27);
            this.createBTN.TabIndex = 18;
            this.createBTN.Text = "Create";
            // 
            // SettingForm
            // 
            this.AcceptButton = this.loginBTN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(444, 265);
            this.Controls.Add(this.metroTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Padding = new System.Windows.Forms.Padding(22, 60, 22, 20);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Settings";
            this.TopMost = true;
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
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
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroButton createBTN;
        private MetroFramework.Controls.MetroComboBox am_pmCombo1_2;
        private MetroFramework.Controls.MetroComboBox endHour1;
        private MetroFramework.Controls.MetroLabel toLabel1;
        private MetroFramework.Controls.MetroComboBox am_pmCombo1_1;
        private MetroFramework.Controls.MetroComboBox startHour1;
    }
}