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
            this.workProgressBar = new System.Windows.Forms.ProgressBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.toLabel1 = new System.Windows.Forms.Label();
            this.endHour1 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo2 = new System.Windows.Forms.ComboBox();
            this.numberOfShiftsLabel1 = new System.Windows.Forms.Label();
            this.shiftTime1 = new System.Windows.Forms.Label();
            this.numberOfShiftsCombo1 = new System.Windows.Forms.ComboBox();
            this.startHour1 = new System.Windows.Forms.ComboBox();
            this.am_pmCombo1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.createBTN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.toLabel = new System.Windows.Forms.Label();
            this.cloGenEnd1 = new System.Windows.Forms.ComboBox();
            this.cloAm_pmCombo2 = new System.Windows.Forms.ComboBox();
            this.cloGenStart1 = new System.Windows.Forms.ComboBox();
            this.cloAm_pmCombo1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCLOBTN = new System.Windows.Forms.Button();
            this.selectTimeLabel = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tab1.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tab2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // workProgressBar
            // 
            this.workProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.workProgressBar.Location = new System.Drawing.Point(0, 293);
            this.workProgressBar.Margin = new System.Windows.Forms.Padding(4);
            this.workProgressBar.Name = "workProgressBar";
            this.workProgressBar.Size = new System.Drawing.Size(590, 25);
            this.workProgressBar.TabIndex = 7;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tab1);
            this.tabControl.Controls.Add(this.tab2);
            this.tabControl.Location = new System.Drawing.Point(10, 34);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(570, 250);
            this.tabControl.TabIndex = 8;
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.mainPanel);
            this.tab1.Controls.Add(this.destinationLabel);
            this.tab1.Controls.Add(this.createBTN);
            this.tab1.Controls.Add(this.textBox1);
            this.tab1.Location = new System.Drawing.Point(4, 25);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(562, 221);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Log Creator";
            this.tab1.UseVisualStyleBackColor = true;
            // 
            // mainPanel
            // 
            this.mainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mainPanel.Controls.Add(this.toLabel1);
            this.mainPanel.Controls.Add(this.endHour1);
            this.mainPanel.Controls.Add(this.am_pmCombo2);
            this.mainPanel.Controls.Add(this.numberOfShiftsLabel1);
            this.mainPanel.Controls.Add(this.shiftTime1);
            this.mainPanel.Controls.Add(this.numberOfShiftsCombo1);
            this.mainPanel.Controls.Add(this.startHour1);
            this.mainPanel.Controls.Add(this.am_pmCombo1);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Location = new System.Drawing.Point(-4, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(566, 165);
            this.mainPanel.TabIndex = 53;
            // 
            // toLabel1
            // 
            this.toLabel1.AutoSize = true;
            this.toLabel1.Location = new System.Drawing.Point(348, 53);
            this.toLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.toLabel1.Name = "toLabel1";
            this.toLabel1.Size = new System.Drawing.Size(21, 16);
            this.toLabel1.TabIndex = 52;
            this.toLabel1.Text = "to";
            // 
            // endHour1
            // 
            this.endHour1.FormattingEnabled = true;
            this.endHour1.Location = new System.Drawing.Point(377, 50);
            this.endHour1.Margin = new System.Windows.Forms.Padding(4);
            this.endHour1.Name = "endHour1";
            this.endHour1.Size = new System.Drawing.Size(83, 24);
            this.endHour1.TabIndex = 50;
            // 
            // am_pmCombo2
            // 
            this.am_pmCombo2.FormattingEnabled = true;
            this.am_pmCombo2.Location = new System.Drawing.Point(468, 50);
            this.am_pmCombo2.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo2.Name = "am_pmCombo2";
            this.am_pmCombo2.Size = new System.Drawing.Size(67, 24);
            this.am_pmCombo2.TabIndex = 51;
            // 
            // numberOfShiftsLabel1
            // 
            this.numberOfShiftsLabel1.AutoSize = true;
            this.numberOfShiftsLabel1.Location = new System.Drawing.Point(183, 78);
            this.numberOfShiftsLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberOfShiftsLabel1.Name = "numberOfShiftsLabel1";
            this.numberOfShiftsLabel1.Size = new System.Drawing.Size(123, 16);
            this.numberOfShiftsLabel1.TabIndex = 49;
            this.numberOfShiftsLabel1.Text = "Number of shifts:";
            // 
            // shiftTime1
            // 
            this.shiftTime1.AutoSize = true;
            this.shiftTime1.Location = new System.Drawing.Point(183, 27);
            this.shiftTime1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.shiftTime1.Name = "shiftTime1";
            this.shiftTime1.Size = new System.Drawing.Size(180, 16);
            this.shiftTime1.TabIndex = 48;
            this.shiftTime1.Text = "Please select shift times:";
            // 
            // numberOfShiftsCombo1
            // 
            this.numberOfShiftsCombo1.FormattingEnabled = true;
            this.numberOfShiftsCombo1.Location = new System.Drawing.Point(183, 98);
            this.numberOfShiftsCombo1.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfShiftsCombo1.Name = "numberOfShiftsCombo1";
            this.numberOfShiftsCombo1.Size = new System.Drawing.Size(177, 24);
            this.numberOfShiftsCombo1.TabIndex = 47;
            // 
            // startHour1
            // 
            this.startHour1.FormattingEnabled = true;
            this.startHour1.Location = new System.Drawing.Point(183, 50);
            this.startHour1.Margin = new System.Windows.Forms.Padding(4);
            this.startHour1.Name = "startHour1";
            this.startHour1.Size = new System.Drawing.Size(82, 24);
            this.startHour1.TabIndex = 45;
            // 
            // am_pmCombo1
            // 
            this.am_pmCombo1.FormattingEnabled = true;
            this.am_pmCombo1.Location = new System.Drawing.Point(273, 50);
            this.am_pmCombo1.Margin = new System.Windows.Forms.Padding(4);
            this.am_pmCombo1.Name = "am_pmCombo1";
            this.am_pmCombo1.Size = new System.Drawing.Size(67, 24);
            this.am_pmCombo1.TabIndex = 46;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::ClassOpsLogCreator.Properties.Resources.Main_thread_image;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoEllipsis = true;
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Location = new System.Drawing.Point(7, 172);
            this.destinationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(148, 16);
            this.destinationLabel.TabIndex = 52;
            this.destinationLabel.Text = "Master Log location:";
            // 
            // createBTN
            // 
            this.createBTN.Location = new System.Drawing.Point(357, 172);
            this.createBTN.Margin = new System.Windows.Forms.Padding(4);
            this.createBTN.Name = "createBTN";
            this.createBTN.Size = new System.Drawing.Size(195, 43);
            this.createBTN.TabIndex = 50;
            this.createBTN.Text = "Create Logs";
            this.createBTN.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(6, 192);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(340, 22);
            this.textBox1.TabIndex = 51;
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.selectTimeLabel);
            this.tab2.Controls.Add(this.createCLOBTN);
            this.tab2.Controls.Add(this.toLabel);
            this.tab2.Controls.Add(this.cloGenEnd1);
            this.tab2.Controls.Add(this.cloAm_pmCombo2);
            this.tab2.Controls.Add(this.cloGenStart1);
            this.tab2.Controls.Add(this.cloAm_pmCombo1);
            this.tab2.Location = new System.Drawing.Point(4, 25);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(562, 221);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "CLO Generator";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(271, 95);
            this.toLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(21, 16);
            this.toLabel.TabIndex = 57;
            this.toLabel.Text = "to";
            // 
            // cloGenEnd1
            // 
            this.cloGenEnd1.FormattingEnabled = true;
            this.cloGenEnd1.Location = new System.Drawing.Point(300, 92);
            this.cloGenEnd1.Margin = new System.Windows.Forms.Padding(4);
            this.cloGenEnd1.Name = "cloGenEnd1";
            this.cloGenEnd1.Size = new System.Drawing.Size(83, 24);
            this.cloGenEnd1.TabIndex = 55;
            // 
            // cloAm_pmCombo2
            // 
            this.cloAm_pmCombo2.FormattingEnabled = true;
            this.cloAm_pmCombo2.Location = new System.Drawing.Point(391, 92);
            this.cloAm_pmCombo2.Margin = new System.Windows.Forms.Padding(4);
            this.cloAm_pmCombo2.Name = "cloAm_pmCombo2";
            this.cloAm_pmCombo2.Size = new System.Drawing.Size(67, 24);
            this.cloAm_pmCombo2.TabIndex = 56;
            // 
            // cloGenStart1
            // 
            this.cloGenStart1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cloGenStart1.FormattingEnabled = true;
            this.cloGenStart1.Location = new System.Drawing.Point(106, 92);
            this.cloGenStart1.Margin = new System.Windows.Forms.Padding(4);
            this.cloGenStart1.Name = "cloGenStart1";
            this.cloGenStart1.Size = new System.Drawing.Size(82, 24);
            this.cloGenStart1.TabIndex = 53;
            // 
            // cloAm_pmCombo1
            // 
            this.cloAm_pmCombo1.FormattingEnabled = true;
            this.cloAm_pmCombo1.Location = new System.Drawing.Point(196, 92);
            this.cloAm_pmCombo1.Margin = new System.Windows.Forms.Padding(4);
            this.cloAm_pmCombo1.Name = "cloAm_pmCombo1";
            this.cloAm_pmCombo1.Size = new System.Drawing.Size(67, 24);
            this.cloAm_pmCombo1.TabIndex = 54;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(590, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printToolStripMenuItem.Text = "&Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // createCLOBTN
            // 
            this.createCLOBTN.Location = new System.Drawing.Point(185, 146);
            this.createCLOBTN.Name = "createCLOBTN";
            this.createCLOBTN.Size = new System.Drawing.Size(195, 43);
            this.createCLOBTN.TabIndex = 58;
            this.createCLOBTN.Text = "Create CLO log";
            this.createCLOBTN.UseVisualStyleBackColor = true;
            this.createCLOBTN.Click += new System.EventHandler(this.createCLOBTN_Click);
            // 
            // selectTimeLabel
            // 
            this.selectTimeLabel.AutoSize = true;
            this.selectTimeLabel.Location = new System.Drawing.Point(219, 51);
            this.selectTimeLabel.Name = "selectTimeLabel";
            this.selectTimeLabel.Size = new System.Drawing.Size(127, 16);
            this.selectTimeLabel.TabIndex = 59;
            this.selectTimeLabel.Text = "Set a Valid Time:";
            // 
            // LogCreator
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(590, 318);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.workProgressBar);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "LogCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Creator";
            this.TransparencyKey = System.Drawing.SystemColors.ControlDarkDark;
            this.tabControl.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar workProgressBar;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tab1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label toLabel1;
        private System.Windows.Forms.ComboBox endHour1;
        private System.Windows.Forms.ComboBox am_pmCombo2;
        private System.Windows.Forms.Label numberOfShiftsLabel1;
        private System.Windows.Forms.Label shiftTime1;
        private System.Windows.Forms.ComboBox numberOfShiftsCombo1;
        private System.Windows.Forms.ComboBox startHour1;
        private System.Windows.Forms.ComboBox am_pmCombo1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.Button createBTN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tab2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.ComboBox cloGenEnd1;
        private System.Windows.Forms.ComboBox cloAm_pmCombo2;
        private System.Windows.Forms.ComboBox cloGenStart1;
        private System.Windows.Forms.ComboBox cloAm_pmCombo1;
        private System.Windows.Forms.Button createCLOBTN;
        private System.Windows.Forms.Label selectTimeLabel;
    }
}

