namespace ClassOpsLogCreator
{
    partial class LoadingScreen
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
            this.progressSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.messageLabel = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // progressSpinner
            // 
            this.progressSpinner.Location = new System.Drawing.Point(23, 63);
            this.progressSpinner.Maximum = 100;
            this.progressSpinner.Name = "progressSpinner";
            this.progressSpinner.Size = new System.Drawing.Size(55, 55);
            this.progressSpinner.Style = MetroFramework.MetroColorStyle.Red;
            this.progressSpinner.TabIndex = 0;
            this.progressSpinner.UseSelectable = true;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.messageLabel.Location = new System.Drawing.Point(84, 75);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(234, 19);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.Text = "Preparing files... ( Sit back and relax ) ";
            // 
            // LoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 133);
            this.ControlBox = false;
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.progressSpinner);
            this.Movable = false;
            this.Name = "LoadingScreen";
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Please wait...";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroProgressSpinner progressSpinner;
        private MetroFramework.Controls.MetroLabel messageLabel;
    }
}