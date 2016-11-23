namespace ClassOpsLogCreator
{
    partial class InitialEmailLoginForm
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
            this.emailQuestoinLabel = new MetroFramework.Controls.MetroLabel();
            this.lotusEmailTile = new MetroFramework.Controls.MetroTile();
            this.electMailTile = new MetroFramework.Controls.MetroTile();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.mainPanel = new MetroFramework.Controls.MetroPanel();
            this.emailLoginPanel = new MetroFramework.Controls.MetroPanel();
            this.cancelBTN = new MetroFramework.Controls.MetroButton();
            this.connectBTN = new MetroFramework.Controls.MetroButton();
            this.lotusEmailPasswordTextBox = new MetroFramework.Controls.MetroTextBox();
            this.lotusEmailPassLabel = new MetroFramework.Controls.MetroLabel();
            this.electronicEmailPasswordTextBox = new MetroFramework.Controls.MetroTextBox();
            this.elecMailPassLabel = new MetroFramework.Controls.MetroLabel();
            this.emailUserNameTextBox = new MetroFramework.Controls.MetroTextBox();
            this.usernameLabel = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.emailLoginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // emailQuestoinLabel
            // 
            this.emailQuestoinLabel.AutoSize = true;
            this.emailQuestoinLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.emailQuestoinLabel.Location = new System.Drawing.Point(138, 15);
            this.emailQuestoinLabel.Name = "emailQuestoinLabel";
            this.emailQuestoinLabel.Size = new System.Drawing.Size(276, 25);
            this.emailQuestoinLabel.TabIndex = 2;
            this.emailQuestoinLabel.Text = "Which Email system do you use? ";
            this.emailQuestoinLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // lotusEmailTile
            // 
            this.lotusEmailTile.ActiveControl = null;
            this.lotusEmailTile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lotusEmailTile.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.lotusEmailTile.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lotusEmailTile.Location = new System.Drawing.Point(280, 48);
            this.lotusEmailTile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lotusEmailTile.Name = "lotusEmailTile";
            this.lotusEmailTile.Size = new System.Drawing.Size(232, 186);
            this.lotusEmailTile.Style = MetroFramework.MetroColorStyle.Yellow;
            this.lotusEmailTile.TabIndex = 1;
            this.lotusEmailTile.Text = "I use Lotus Notes";
            this.lotusEmailTile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lotusEmailTile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lotusEmailTile.TileImage = global::ClassOpsLogCreator.Properties.Resources.oie_transparent;
            this.lotusEmailTile.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lotusEmailTile.UseSelectable = true;
            this.lotusEmailTile.UseTileImage = true;
            this.lotusEmailTile.Click += new System.EventHandler(this.lotusEmailTile_Click);
            // 
            // electMailTile
            // 
            this.electMailTile.ActiveControl = null;
            this.electMailTile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.electMailTile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.electMailTile.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.electMailTile.ForeColor = System.Drawing.Color.Black;
            this.electMailTile.Location = new System.Drawing.Point(24, 48);
            this.electMailTile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.electMailTile.Name = "electMailTile";
            this.electMailTile.Size = new System.Drawing.Size(232, 186);
            this.electMailTile.Style = MetroFramework.MetroColorStyle.Red;
            this.electMailTile.TabIndex = 0;
            this.electMailTile.Text = "I use Electronic Mail";
            this.electMailTile.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.electMailTile.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.electMailTile.TileImage = global::ClassOpsLogCreator.Properties.Resources._1479883104_5303___Gmail;
            this.electMailTile.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.electMailTile.UseSelectable = true;
            this.electMailTile.UseTileImage = true;
            this.electMailTile.Click += new System.EventHandler(this.electMailTile_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = null;
            this.metroStyleManager1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.lotusEmailTile);
            this.mainPanel.Controls.Add(this.emailQuestoinLabel);
            this.mainPanel.Controls.Add(this.electMailTile);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.HorizontalScrollbarBarColor = true;
            this.mainPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.mainPanel.HorizontalScrollbarSize = 10;
            this.mainPanel.Location = new System.Drawing.Point(22, 79);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(534, 271);
            this.mainPanel.TabIndex = 3;
            this.mainPanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mainPanel.VerticalScrollbarBarColor = true;
            this.mainPanel.VerticalScrollbarHighlightOnWheel = false;
            this.mainPanel.VerticalScrollbarSize = 10;
            // 
            // emailLoginPanel
            // 
            this.emailLoginPanel.Controls.Add(this.cancelBTN);
            this.emailLoginPanel.Controls.Add(this.connectBTN);
            this.emailLoginPanel.Controls.Add(this.lotusEmailPasswordTextBox);
            this.emailLoginPanel.Controls.Add(this.lotusEmailPassLabel);
            this.emailLoginPanel.Controls.Add(this.electronicEmailPasswordTextBox);
            this.emailLoginPanel.Controls.Add(this.elecMailPassLabel);
            this.emailLoginPanel.Controls.Add(this.emailUserNameTextBox);
            this.emailLoginPanel.Controls.Add(this.usernameLabel);
            this.emailLoginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailLoginPanel.HorizontalScrollbarBarColor = true;
            this.emailLoginPanel.HorizontalScrollbarHighlightOnWheel = false;
            this.emailLoginPanel.HorizontalScrollbarSize = 10;
            this.emailLoginPanel.Location = new System.Drawing.Point(22, 79);
            this.emailLoginPanel.Name = "emailLoginPanel";
            this.emailLoginPanel.Size = new System.Drawing.Size(534, 271);
            this.emailLoginPanel.TabIndex = 4;
            this.emailLoginPanel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.emailLoginPanel.VerticalScrollbarBarColor = true;
            this.emailLoginPanel.VerticalScrollbarHighlightOnWheel = false;
            this.emailLoginPanel.VerticalScrollbarSize = 10;
            this.emailLoginPanel.Visible = false;
            // 
            // cancelBTN
            // 
            this.cancelBTN.Location = new System.Drawing.Point(282, 220);
            this.cancelBTN.Name = "cancelBTN";
            this.cancelBTN.Size = new System.Drawing.Size(75, 23);
            this.cancelBTN.TabIndex = 9;
            this.cancelBTN.Text = "Cancel";
            this.cancelBTN.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.cancelBTN.UseSelectable = true;
            this.cancelBTN.Click += new System.EventHandler(this.cancelBTN_Click);
            // 
            // connectBTN
            // 
            this.connectBTN.Location = new System.Drawing.Point(177, 220);
            this.connectBTN.Name = "connectBTN";
            this.connectBTN.Size = new System.Drawing.Size(75, 23);
            this.connectBTN.TabIndex = 8;
            this.connectBTN.Text = "Login";
            this.connectBTN.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.connectBTN.UseSelectable = true;
            this.connectBTN.Click += new System.EventHandler(this.connectBTN_Click);
            // 
            // lotusEmailPasswordTextBox
            // 
            // 
            // 
            // 
            this.lotusEmailPasswordTextBox.CustomButton.Image = null;
            this.lotusEmailPasswordTextBox.CustomButton.Location = new System.Drawing.Point(92, 1);
            this.lotusEmailPasswordTextBox.CustomButton.Name = "";
            this.lotusEmailPasswordTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.lotusEmailPasswordTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.lotusEmailPasswordTextBox.CustomButton.TabIndex = 1;
            this.lotusEmailPasswordTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.lotusEmailPasswordTextBox.CustomButton.UseSelectable = true;
            this.lotusEmailPasswordTextBox.CustomButton.Visible = false;
            this.lotusEmailPasswordTextBox.Lines = new string[0];
            this.lotusEmailPasswordTextBox.Location = new System.Drawing.Point(300, 168);
            this.lotusEmailPasswordTextBox.MaxLength = 32767;
            this.lotusEmailPasswordTextBox.Name = "lotusEmailPasswordTextBox";
            this.lotusEmailPasswordTextBox.PasswordChar = '\0';
            this.lotusEmailPasswordTextBox.PromptText = "Lotus Notes Password";
            this.lotusEmailPasswordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.lotusEmailPasswordTextBox.SelectedText = "";
            this.lotusEmailPasswordTextBox.SelectionLength = 0;
            this.lotusEmailPasswordTextBox.SelectionStart = 0;
            this.lotusEmailPasswordTextBox.ShortcutsEnabled = true;
            this.lotusEmailPasswordTextBox.Size = new System.Drawing.Size(114, 23);
            this.lotusEmailPasswordTextBox.TabIndex = 7;
            this.lotusEmailPasswordTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.lotusEmailPasswordTextBox.UseSelectable = true;
            this.lotusEmailPasswordTextBox.WaterMark = "Lotus Notes Password";
            this.lotusEmailPasswordTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.lotusEmailPasswordTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // lotusEmailPassLabel
            // 
            this.lotusEmailPassLabel.AutoSize = true;
            this.lotusEmailPassLabel.Location = new System.Drawing.Point(122, 168);
            this.lotusEmailPassLabel.Name = "lotusEmailPassLabel";
            this.lotusEmailPassLabel.Size = new System.Drawing.Size(147, 20);
            this.lotusEmailPassLabel.TabIndex = 6;
            this.lotusEmailPassLabel.Text = "Lotus Notes Password:";
            this.lotusEmailPassLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // electronicEmailPasswordTextBox
            // 
            // 
            // 
            // 
            this.electronicEmailPasswordTextBox.CustomButton.Image = null;
            this.electronicEmailPasswordTextBox.CustomButton.Location = new System.Drawing.Point(92, 1);
            this.electronicEmailPasswordTextBox.CustomButton.Name = "";
            this.electronicEmailPasswordTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.electronicEmailPasswordTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.electronicEmailPasswordTextBox.CustomButton.TabIndex = 1;
            this.electronicEmailPasswordTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.electronicEmailPasswordTextBox.CustomButton.UseSelectable = true;
            this.electronicEmailPasswordTextBox.CustomButton.Visible = false;
            this.electronicEmailPasswordTextBox.Lines = new string[0];
            this.electronicEmailPasswordTextBox.Location = new System.Drawing.Point(300, 125);
            this.electronicEmailPasswordTextBox.MaxLength = 32767;
            this.electronicEmailPasswordTextBox.Name = "electronicEmailPasswordTextBox";
            this.electronicEmailPasswordTextBox.PasswordChar = '\0';
            this.electronicEmailPasswordTextBox.PromptText = "Electronic Email Password";
            this.electronicEmailPasswordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.electronicEmailPasswordTextBox.SelectedText = "";
            this.electronicEmailPasswordTextBox.SelectionLength = 0;
            this.electronicEmailPasswordTextBox.SelectionStart = 0;
            this.electronicEmailPasswordTextBox.ShortcutsEnabled = true;
            this.electronicEmailPasswordTextBox.Size = new System.Drawing.Size(114, 23);
            this.electronicEmailPasswordTextBox.TabIndex = 5;
            this.electronicEmailPasswordTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.electronicEmailPasswordTextBox.UseSelectable = true;
            this.electronicEmailPasswordTextBox.WaterMark = "Electronic Email Password";
            this.electronicEmailPasswordTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.electronicEmailPasswordTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // elecMailPassLabel
            // 
            this.elecMailPassLabel.AutoSize = true;
            this.elecMailPassLabel.Location = new System.Drawing.Point(111, 125);
            this.elecMailPassLabel.Name = "elecMailPassLabel";
            this.elecMailPassLabel.Size = new System.Drawing.Size(163, 20);
            this.elecMailPassLabel.TabIndex = 4;
            this.elecMailPassLabel.Text = "Electronic Mail Password:";
            this.elecMailPassLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // emailUserNameTextBox
            // 
            // 
            // 
            // 
            this.emailUserNameTextBox.CustomButton.Image = null;
            this.emailUserNameTextBox.CustomButton.Location = new System.Drawing.Point(92, 1);
            this.emailUserNameTextBox.CustomButton.Name = "";
            this.emailUserNameTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.emailUserNameTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.emailUserNameTextBox.CustomButton.TabIndex = 1;
            this.emailUserNameTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.emailUserNameTextBox.CustomButton.UseSelectable = true;
            this.emailUserNameTextBox.CustomButton.Visible = false;
            this.emailUserNameTextBox.Lines = new string[0];
            this.emailUserNameTextBox.Location = new System.Drawing.Point(300, 80);
            this.emailUserNameTextBox.MaxLength = 32767;
            this.emailUserNameTextBox.Name = "emailUserNameTextBox";
            this.emailUserNameTextBox.PasswordChar = '\0';
            this.emailUserNameTextBox.PromptText = "username@yorku.ca";
            this.emailUserNameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.emailUserNameTextBox.SelectedText = "";
            this.emailUserNameTextBox.SelectionLength = 0;
            this.emailUserNameTextBox.SelectionStart = 0;
            this.emailUserNameTextBox.ShortcutsEnabled = true;
            this.emailUserNameTextBox.Size = new System.Drawing.Size(114, 23);
            this.emailUserNameTextBox.TabIndex = 3;
            this.emailUserNameTextBox.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.emailUserNameTextBox.UseSelectable = true;
            this.emailUserNameTextBox.WaterMark = "username@yorku.ca";
            this.emailUserNameTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.emailUserNameTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(159, 80);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(80, 20);
            this.usernameLabel.TabIndex = 2;
            this.usernameLabel.Text = "Username: ";
            this.usernameLabel.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // InitialEmailLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 376);
            this.ControlBox = false;
            this.Controls.Add(this.emailLoginPanel);
            this.Controls.Add(this.mainPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Movable = false;
            this.Name = "InitialEmailLoginForm";
            this.Opacity = 0.95D;
            this.Padding = new System.Windows.Forms.Padding(22, 79, 22, 26);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "First time setup                                                                 " +
    "                                                 ";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.emailLoginPanel.ResumeLayout(false);
            this.emailLoginPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTile electMailTile;
        private MetroFramework.Controls.MetroTile lotusEmailTile;
        private MetroFramework.Controls.MetroLabel emailQuestoinLabel;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private MetroFramework.Controls.MetroPanel mainPanel;
        private MetroFramework.Controls.MetroPanel emailLoginPanel;
        private MetroFramework.Controls.MetroTextBox lotusEmailPasswordTextBox;
        private MetroFramework.Controls.MetroLabel lotusEmailPassLabel;
        private MetroFramework.Controls.MetroTextBox electronicEmailPasswordTextBox;
        private MetroFramework.Controls.MetroLabel elecMailPassLabel;
        private MetroFramework.Controls.MetroTextBox emailUserNameTextBox;
        private MetroFramework.Controls.MetroLabel usernameLabel;
        private MetroFramework.Controls.MetroButton cancelBTN;
        private MetroFramework.Controls.MetroButton connectBTN;
    }
}