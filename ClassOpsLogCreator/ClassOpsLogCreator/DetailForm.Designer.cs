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
            this.detailTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // detailTextBox
            // 
            this.detailTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.detailTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.detailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.detailTextBox.Location = new System.Drawing.Point(20, 30);
            this.detailTextBox.Name = "detailTextBox";
            this.detailTextBox.ReadOnly = true;
            this.detailTextBox.Size = new System.Drawing.Size(662, 559);
            this.detailTextBox.TabIndex = 2;
            this.detailTextBox.Text = "";
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(702, 609);
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
        private System.Windows.Forms.RichTextBox detailTextBox;
    }
}