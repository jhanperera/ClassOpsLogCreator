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
            this.mainTabContainer = new System.Windows.Forms.TabControl();
            this.step1Tab = new System.Windows.Forms.TabPage();
            this.openBTN3 = new System.Windows.Forms.Button();
            this.openBTN2 = new System.Windows.Forms.Button();
            this.openBTN1 = new System.Windows.Forms.Button();
            this.fileText3 = new System.Windows.Forms.Label();
            this.fileText2 = new System.Windows.Forms.Label();
            this.fileText1 = new System.Windows.Forms.Label();
            this.fileTextBox3 = new System.Windows.Forms.TextBox();
            this.fileTextBox2 = new System.Windows.Forms.TextBox();
            this.fileTextBox1 = new System.Windows.Forms.TextBox();
            this.setOneText = new System.Windows.Forms.Label();
            this.clearBTN = new System.Windows.Forms.Button();
            this.submitBTN = new System.Windows.Forms.Button();
            this.step2Tab = new System.Windows.Forms.TabPage();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainTabContainer.SuspendLayout();
            this.step1Tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabContainer
            // 
            this.mainTabContainer.Controls.Add(this.step1Tab);
            this.mainTabContainer.Controls.Add(this.step2Tab);
            this.mainTabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabContainer.Location = new System.Drawing.Point(0, 0);
            this.mainTabContainer.Name = "mainTabContainer";
            this.mainTabContainer.SelectedIndex = 0;
            this.mainTabContainer.Size = new System.Drawing.Size(659, 257);
            this.mainTabContainer.TabIndex = 0;
            // 
            // step1Tab
            // 
            this.step1Tab.Controls.Add(this.openBTN3);
            this.step1Tab.Controls.Add(this.openBTN2);
            this.step1Tab.Controls.Add(this.openBTN1);
            this.step1Tab.Controls.Add(this.fileText3);
            this.step1Tab.Controls.Add(this.fileText2);
            this.step1Tab.Controls.Add(this.fileText1);
            this.step1Tab.Controls.Add(this.fileTextBox3);
            this.step1Tab.Controls.Add(this.fileTextBox2);
            this.step1Tab.Controls.Add(this.fileTextBox1);
            this.step1Tab.Controls.Add(this.setOneText);
            this.step1Tab.Controls.Add(this.clearBTN);
            this.step1Tab.Controls.Add(this.submitBTN);
            this.step1Tab.Location = new System.Drawing.Point(4, 22);
            this.step1Tab.Name = "step1Tab";
            this.step1Tab.Padding = new System.Windows.Forms.Padding(3);
            this.step1Tab.Size = new System.Drawing.Size(651, 231);
            this.step1Tab.TabIndex = 0;
            this.step1Tab.Text = "Step 1";
            this.step1Tab.UseVisualStyleBackColor = true;
            // 
            // openBTN3
            // 
            this.openBTN3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.openBTN3.Location = new System.Drawing.Point(496, 113);
            this.openBTN3.Name = "openBTN3";
            this.openBTN3.Size = new System.Drawing.Size(147, 20);
            this.openBTN3.TabIndex = 11;
            this.openBTN3.Text = "Open";
            this.openBTN3.UseVisualStyleBackColor = true;
            this.openBTN3.Click += new System.EventHandler(this.openBTN3_Click);
            // 
            // openBTN2
            // 
            this.openBTN2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.openBTN2.Location = new System.Drawing.Point(496, 87);
            this.openBTN2.Name = "openBTN2";
            this.openBTN2.Size = new System.Drawing.Size(147, 20);
            this.openBTN2.TabIndex = 10;
            this.openBTN2.Text = "Open";
            this.openBTN2.UseVisualStyleBackColor = true;
            this.openBTN2.Click += new System.EventHandler(this.openBTN2_Click);
            // 
            // openBTN1
            // 
            this.openBTN1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.openBTN1.Location = new System.Drawing.Point(496, 61);
            this.openBTN1.Name = "openBTN1";
            this.openBTN1.Size = new System.Drawing.Size(147, 20);
            this.openBTN1.TabIndex = 9;
            this.openBTN1.Text = "Open";
            this.openBTN1.UseVisualStyleBackColor = true;
            this.openBTN1.Click += new System.EventHandler(this.openBTN1_Click);
            // 
            // fileText3
            // 
            this.fileText3.AutoSize = true;
            this.fileText3.Location = new System.Drawing.Point(34, 113);
            this.fileText3.Name = "fileText3";
            this.fileText3.Size = new System.Drawing.Size(53, 13);
            this.fileText3.TabIndex = 8;
            this.fileText3.Text = "File three:";
            // 
            // fileText2
            // 
            this.fileText2.AutoSize = true;
            this.fileText2.Location = new System.Drawing.Point(34, 90);
            this.fileText2.Name = "fileText2";
            this.fileText2.Size = new System.Drawing.Size(46, 13);
            this.fileText2.TabIndex = 7;
            this.fileText2.Text = "File two:";
            // 
            // fileText1
            // 
            this.fileText1.AutoSize = true;
            this.fileText1.Location = new System.Drawing.Point(34, 64);
            this.fileText1.Name = "fileText1";
            this.fileText1.Size = new System.Drawing.Size(47, 13);
            this.fileText1.TabIndex = 6;
            this.fileText1.Text = "File one:";
            // 
            // fileTextBox3
            // 
            this.fileTextBox3.Location = new System.Drawing.Point(87, 113);
            this.fileTextBox3.Name = "fileTextBox3";
            this.fileTextBox3.Size = new System.Drawing.Size(396, 20);
            this.fileTextBox3.TabIndex = 5;
            // 
            // fileTextBox2
            // 
            this.fileTextBox2.Location = new System.Drawing.Point(87, 87);
            this.fileTextBox2.Name = "fileTextBox2";
            this.fileTextBox2.Size = new System.Drawing.Size(396, 20);
            this.fileTextBox2.TabIndex = 4;
            // 
            // fileTextBox1
            // 
            this.fileTextBox1.Location = new System.Drawing.Point(87, 61);
            this.fileTextBox1.Name = "fileTextBox1";
            this.fileTextBox1.Size = new System.Drawing.Size(396, 20);
            this.fileTextBox1.TabIndex = 3;
            // 
            // setOneText
            // 
            this.setOneText.AutoSize = true;
            this.setOneText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setOneText.Location = new System.Drawing.Point(8, 15);
            this.setOneText.Name = "setOneText";
            this.setOneText.Size = new System.Drawing.Size(429, 29);
            this.setOneText.TabIndex = 2;
            this.setOneText.Text = "Please select the Excel Documents:";
            // 
            // clearBTN
            // 
            this.clearBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearBTN.Location = new System.Drawing.Point(37, 179);
            this.clearBTN.Name = "clearBTN";
            this.clearBTN.Size = new System.Drawing.Size(130, 35);
            this.clearBTN.TabIndex = 1;
            this.clearBTN.Text = "Clear";
            this.clearBTN.UseVisualStyleBackColor = true;
            this.clearBTN.Click += new System.EventHandler(this.clearBTN_Click);
            // 
            // submitBTN
            // 
            this.submitBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.submitBTN.AutoEllipsis = true;
            this.submitBTN.Location = new System.Drawing.Point(496, 188);
            this.submitBTN.Name = "submitBTN";
            this.submitBTN.Size = new System.Drawing.Size(130, 35);
            this.submitBTN.TabIndex = 0;
            this.submitBTN.Text = "Submit";
            this.submitBTN.UseVisualStyleBackColor = true;
            // 
            // step2Tab
            // 
            this.step2Tab.Location = new System.Drawing.Point(4, 22);
            this.step2Tab.Name = "step2Tab";
            this.step2Tab.Padding = new System.Windows.Forms.Padding(3);
            this.step2Tab.Size = new System.Drawing.Size(651, 231);
            this.step2Tab.TabIndex = 1;
            this.step2Tab.Text = "Step 2";
            this.step2Tab.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // LogCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 257);
            this.Controls.Add(this.mainTabContainer);
            this.Name = "LogCreator";
            this.Text = "Log Creator";
            this.mainTabContainer.ResumeLayout(false);
            this.step1Tab.ResumeLayout(false);
            this.step1Tab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabContainer;
        private System.Windows.Forms.TabPage step1Tab;
        private System.Windows.Forms.TabPage step2Tab;
        private System.Windows.Forms.Label setOneText;
        private System.Windows.Forms.Button clearBTN;
        private System.Windows.Forms.Button submitBTN;
        private System.Windows.Forms.Button openBTN3;
        private System.Windows.Forms.Button openBTN2;
        private System.Windows.Forms.Button openBTN1;
        private System.Windows.Forms.Label fileText3;
        private System.Windows.Forms.Label fileText2;
        private System.Windows.Forms.Label fileText1;
        private System.Windows.Forms.TextBox fileTextBox3;
        private System.Windows.Forms.TextBox fileTextBox2;
        private System.Windows.Forms.TextBox fileTextBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

