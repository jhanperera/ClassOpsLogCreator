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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.createBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // createBTN
            // 
            this.createBTN.Location = new System.Drawing.Point(77, 44);
            this.createBTN.Name = "createBTN";
            this.createBTN.Size = new System.Drawing.Size(176, 53);
            this.createBTN.TabIndex = 0;
            this.createBTN.Text = "Create";
            this.createBTN.UseVisualStyleBackColor = true;
            this.createBTN.Click += new System.EventHandler(this.createBTN_Click);
            // 
            // LogCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 163);
            this.Controls.Add(this.createBTN);
            this.Name = "LogCreator";
            this.Text = "Log Creator";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button createBTN;
    }
}

