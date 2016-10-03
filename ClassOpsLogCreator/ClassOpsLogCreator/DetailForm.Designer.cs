namespace ClassOpsLogCreator
{
    partial class DetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailForm));
            this.detailTextBox = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // detailTextBox
            // 
            // 
            // 
            // 
            this.detailTextBox.CustomButton.Image = null;
            this.detailTextBox.CustomButton.Location = new System.Drawing.Point(273, 2);
            this.detailTextBox.CustomButton.Name = "";
            this.detailTextBox.CustomButton.Size = new System.Drawing.Size(245, 245);
            this.detailTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.detailTextBox.CustomButton.TabIndex = 1;
            this.detailTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.detailTextBox.CustomButton.UseSelectable = true;
            this.detailTextBox.CustomButton.Visible = false;
            this.detailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.detailTextBox.Lines = new string[0];
            this.detailTextBox.Location = new System.Drawing.Point(20, 30);
            this.detailTextBox.MaxLength = 32767;
            this.detailTextBox.Multiline = true;
            this.detailTextBox.Name = "detailTextBox";
            this.detailTextBox.PasswordChar = '\0';
            this.detailTextBox.ReadOnly = true;
            this.detailTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.detailTextBox.SelectedText = "";
            this.detailTextBox.SelectionLength = 0;
            this.detailTextBox.SelectionStart = 0;
            this.detailTextBox.ShortcutsEnabled = true;
            this.detailTextBox.Size = new System.Drawing.Size(521, 250);
            this.detailTextBox.Style = MetroFramework.MetroColorStyle.Black;
            this.detailTextBox.TabIndex = 1;
            this.detailTextBox.Theme = MetroFramework.MetroThemeStyle.Light;
            this.detailTextBox.UseSelectable = true;
            this.detailTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.detailTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(561, 300);
            this.Controls.Add(this.detailTextBox);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Style = MetroFramework.MetroColorStyle.Black;
            this.Text = "DetailForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox detailTextBox;
    }
}