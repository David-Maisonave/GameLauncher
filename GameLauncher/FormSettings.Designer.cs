namespace GameLauncher
{
    partial class Form_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Settings));
            this.checkBox_usePreviousCollectionCache = new System.Windows.Forms.CheckBox();
            this.checkBox_useJoystickController = new System.Windows.Forms.CheckBox();
            this.textBox_DbPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_EmulatorsBasePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_DefaultImagePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_DbPath = new System.Windows.Forms.Button();
            this.button_EmulatorsBasePath = new System.Windows.Forms.Button();
            this.button_DefaultImagePath = new System.Windows.Forms.Button();
            this.numericUpDown_MaxNumberOfPairThreadsPerList = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_largeIconSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_smallIconSize = new System.Windows.Forms.NumericUpDown();
            this.button_Ok = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabel_Github_GameLauncher = new System.Windows.Forms.LinkLabel();
            this.checkBox_disableAdvanceOptions = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableImageChecksum = new System.Windows.Forms.CheckBox();
            this.checkBox_EnableRomChecksum = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxNumberOfPairThreadsPerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_largeIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_smallIconSize)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_usePreviousCollectionCache
            // 
            this.checkBox_usePreviousCollectionCache.AutoSize = true;
            this.checkBox_usePreviousCollectionCache.Location = new System.Drawing.Point(319, 115);
            this.checkBox_usePreviousCollectionCache.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_usePreviousCollectionCache.Name = "checkBox_usePreviousCollectionCache";
            this.checkBox_usePreviousCollectionCache.Size = new System.Drawing.Size(244, 24);
            this.checkBox_usePreviousCollectionCache.TabIndex = 13;
            this.checkBox_usePreviousCollectionCache.Text = "&Use Previous Collection Cache";
            this.toolTip1.SetToolTip(this.checkBox_usePreviousCollectionCache, "Enable this option to improve performance when switching system console selection" +
        ". This option increases GameLauncher memory usage.");
            this.checkBox_usePreviousCollectionCache.UseVisualStyleBackColor = true;
            // 
            // checkBox_useJoystickController
            // 
            this.checkBox_useJoystickController.AutoSize = true;
            this.checkBox_useJoystickController.Location = new System.Drawing.Point(319, 146);
            this.checkBox_useJoystickController.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_useJoystickController.Name = "checkBox_useJoystickController";
            this.checkBox_useJoystickController.Size = new System.Drawing.Size(261, 24);
            this.checkBox_useJoystickController.TabIndex = 14;
            this.checkBox_useJoystickController.Text = "Enable &Joystick Controller Usage";
            this.toolTip1.SetToolTip(this.checkBox_useJoystickController, "Enable to allow using game controller to select and play games. See GameLauncher " +
        "Github link to get instructions on game controller button options.");
            this.checkBox_useJoystickController.UseVisualStyleBackColor = true;
            // 
            // textBox_DbPath
            // 
            this.textBox_DbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_DbPath.Location = new System.Drawing.Point(170, 14);
            this.textBox_DbPath.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_DbPath.Name = "textBox_DbPath";
            this.textBox_DbPath.Size = new System.Drawing.Size(391, 26);
            this.textBox_DbPath.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_DbPath, "This change only takes affect after program restart.  Chaning this can trigger a " +
        "new scan after program restart.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Database Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Emulators Directory";
            // 
            // textBox_EmulatorsBasePath
            // 
            this.textBox_EmulatorsBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_EmulatorsBasePath.Location = new System.Drawing.Point(170, 44);
            this.textBox_EmulatorsBasePath.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_EmulatorsBasePath.Name = "textBox_EmulatorsBasePath";
            this.textBox_EmulatorsBasePath.Size = new System.Drawing.Size(391, 26);
            this.textBox_EmulatorsBasePath.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBox_EmulatorsBasePath, "This is the base directory where all the emulators are stored. Changing this can " +
        "trigger a full ROM rescan.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Default &Image File";
            // 
            // textBox_DefaultImagePath
            // 
            this.textBox_DefaultImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_DefaultImagePath.Location = new System.Drawing.Point(170, 74);
            this.textBox_DefaultImagePath.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_DefaultImagePath.Name = "textBox_DefaultImagePath";
            this.textBox_DefaultImagePath.Size = new System.Drawing.Size(391, 26);
            this.textBox_DefaultImagePath.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBox_DefaultImagePath, "Select default image shown for ROM/games having no associated images.  Note: This" +
        " only takes affect on newly scanned ROM\'s.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "&Max Threads";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 145);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "&Large Icon Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 173);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "&Small Icon Size";
            // 
            // button_DbPath
            // 
            this.button_DbPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_DbPath.Location = new System.Drawing.Point(565, 14);
            this.button_DbPath.Margin = new System.Windows.Forms.Padding(2);
            this.button_DbPath.Name = "button_DbPath";
            this.button_DbPath.Size = new System.Drawing.Size(32, 24);
            this.button_DbPath.TabIndex = 2;
            this.button_DbPath.Text = "...";
            this.button_DbPath.UseVisualStyleBackColor = true;
            this.button_DbPath.Click += new System.EventHandler(this.button_DbPath_Click);
            // 
            // button_EmulatorsBasePath
            // 
            this.button_EmulatorsBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_EmulatorsBasePath.Location = new System.Drawing.Point(565, 44);
            this.button_EmulatorsBasePath.Margin = new System.Windows.Forms.Padding(2);
            this.button_EmulatorsBasePath.Name = "button_EmulatorsBasePath";
            this.button_EmulatorsBasePath.Size = new System.Drawing.Size(32, 24);
            this.button_EmulatorsBasePath.TabIndex = 4;
            this.button_EmulatorsBasePath.Text = "...";
            this.button_EmulatorsBasePath.UseVisualStyleBackColor = true;
            this.button_EmulatorsBasePath.Click += new System.EventHandler(this.button_EmulatorsBasePath_Click);
            // 
            // button_DefaultImagePath
            // 
            this.button_DefaultImagePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_DefaultImagePath.Location = new System.Drawing.Point(565, 74);
            this.button_DefaultImagePath.Margin = new System.Windows.Forms.Padding(2);
            this.button_DefaultImagePath.Name = "button_DefaultImagePath";
            this.button_DefaultImagePath.Size = new System.Drawing.Size(32, 24);
            this.button_DefaultImagePath.TabIndex = 6;
            this.button_DefaultImagePath.Text = "...";
            this.button_DefaultImagePath.UseVisualStyleBackColor = true;
            this.button_DefaultImagePath.Click += new System.EventHandler(this.button_DefaultImagePath_Click);
            // 
            // numericUpDown_MaxNumberOfPairThreadsPerList
            // 
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Location = new System.Drawing.Point(171, 115);
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Name = "numericUpDown_MaxNumberOfPairThreadsPerList";
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Size = new System.Drawing.Size(80, 26);
            this.numericUpDown_MaxNumberOfPairThreadsPerList.TabIndex = 8;
            this.toolTip1.SetToolTip(this.numericUpDown_MaxNumberOfPairThreadsPerList, "Maximum pair of threads used when creating ImageList. If set to 8, the program wi" +
        "ll use 16 threads on initial ImageList processing. Using more than 6 pair of thr" +
        "eads yields little in performance.");
            // 
            // numericUpDown_largeIconSize
            // 
            this.numericUpDown_largeIconSize.Increment = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDown_largeIconSize.Location = new System.Drawing.Point(171, 144);
            this.numericUpDown_largeIconSize.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_largeIconSize.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDown_largeIconSize.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDown_largeIconSize.Name = "numericUpDown_largeIconSize";
            this.numericUpDown_largeIconSize.Size = new System.Drawing.Size(80, 26);
            this.numericUpDown_largeIconSize.TabIndex = 10;
            this.numericUpDown_largeIconSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDown_largeIconSize.ValueChanged += new System.EventHandler(this.numericUpDown_largeIconSize_ValueChanged);
            // 
            // numericUpDown_smallIconSize
            // 
            this.numericUpDown_smallIconSize.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_smallIconSize.Location = new System.Drawing.Point(171, 173);
            this.numericUpDown_smallIconSize.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown_smallIconSize.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDown_smallIconSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_smallIconSize.Name = "numericUpDown_smallIconSize";
            this.numericUpDown_smallIconSize.Size = new System.Drawing.Size(80, 26);
            this.numericUpDown_smallIconSize.TabIndex = 12;
            this.numericUpDown_smallIconSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_smallIconSize.ValueChanged += new System.EventHandler(this.numericUpDown_smallIconSize_ValueChanged);
            // 
            // button_Ok
            // 
            this.button_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Ok.Location = new System.Drawing.Point(220, 328);
            this.button_Ok.Margin = new System.Windows.Forms.Padding(2);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(67, 26);
            this.button_Ok.TabIndex = 16;
            this.button_Ok.Text = "&OK";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(314, 328);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 28);
            this.button_Cancel.TabIndex = 17;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // linkLabel_Github_GameLauncher
            // 
            this.linkLabel_Github_GameLauncher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_Github_GameLauncher.AutoSize = true;
            this.linkLabel_Github_GameLauncher.Location = new System.Drawing.Point(177, 293);
            this.linkLabel_Github_GameLauncher.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel_Github_GameLauncher.Name = "linkLabel_Github_GameLauncher";
            this.linkLabel_Github_GameLauncher.Size = new System.Drawing.Size(375, 20);
            this.linkLabel_Github_GameLauncher.TabIndex = 22;
            this.linkLabel_Github_GameLauncher.TabStop = true;
            this.linkLabel_Github_GameLauncher.Text = "https://github.com/David-Maisonave/GameLauncher";
            this.toolTip1.SetToolTip(this.linkLabel_Github_GameLauncher, "GameLauncher Github Website");
            this.linkLabel_Github_GameLauncher.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
            // 
            // checkBox_disableAdvanceOptions
            // 
            this.checkBox_disableAdvanceOptions.AutoSize = true;
            this.checkBox_disableAdvanceOptions.Location = new System.Drawing.Point(319, 177);
            this.checkBox_disableAdvanceOptions.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_disableAdvanceOptions.Name = "checkBox_disableAdvanceOptions";
            this.checkBox_disableAdvanceOptions.Size = new System.Drawing.Size(206, 24);
            this.checkBox_disableAdvanceOptions.TabIndex = 15;
            this.checkBox_disableAdvanceOptions.Text = "Disable &Advance Options";
            this.toolTip1.SetToolTip(this.checkBox_disableAdvanceOptions, "When checked, context menu option and rescan options are disabled.");
            this.checkBox_disableAdvanceOptions.UseVisualStyleBackColor = true;
            // 
            // checkBox_EnableImageChecksum
            // 
            this.checkBox_EnableImageChecksum.AutoSize = true;
            this.checkBox_EnableImageChecksum.Location = new System.Drawing.Point(319, 239);
            this.checkBox_EnableImageChecksum.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_EnableImageChecksum.Name = "checkBox_EnableImageChecksum";
            this.checkBox_EnableImageChecksum.Size = new System.Drawing.Size(206, 24);
            this.checkBox_EnableImageChecksum.TabIndex = 24;
            this.checkBox_EnableImageChecksum.Text = "Enable Image Checksum";
            this.toolTip1.SetToolTip(this.checkBox_EnableImageChecksum, resources.GetString("checkBox_EnableImageChecksum.ToolTip"));
            this.checkBox_EnableImageChecksum.UseVisualStyleBackColor = true;
            // 
            // checkBox_EnableRomChecksum
            // 
            this.checkBox_EnableRomChecksum.AutoSize = true;
            this.checkBox_EnableRomChecksum.Location = new System.Drawing.Point(319, 208);
            this.checkBox_EnableRomChecksum.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_EnableRomChecksum.Name = "checkBox_EnableRomChecksum";
            this.checkBox_EnableRomChecksum.Size = new System.Drawing.Size(198, 24);
            this.checkBox_EnableRomChecksum.TabIndex = 23;
            this.checkBox_EnableRomChecksum.Text = "Enable ROM Checksum";
            this.toolTip1.SetToolTip(this.checkBox_EnableRomChecksum, resources.GetString("checkBox_EnableRomChecksum.ToolTip"));
            this.checkBox_EnableRomChecksum.UseVisualStyleBackColor = true;
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 361);
            this.Controls.Add(this.checkBox_EnableImageChecksum);
            this.Controls.Add(this.checkBox_EnableRomChecksum);
            this.Controls.Add(this.linkLabel_Github_GameLauncher);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.numericUpDown_smallIconSize);
            this.Controls.Add(this.numericUpDown_largeIconSize);
            this.Controls.Add(this.numericUpDown_MaxNumberOfPairThreadsPerList);
            this.Controls.Add(this.button_DefaultImagePath);
            this.Controls.Add(this.button_EmulatorsBasePath);
            this.Controls.Add(this.button_DbPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_DefaultImagePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_EmulatorsBasePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_DbPath);
            this.Controls.Add(this.checkBox_disableAdvanceOptions);
            this.Controls.Add(this.checkBox_useJoystickController);
            this.Controls.Add(this.checkBox_usePreviousCollectionCache);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1200, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(620, 400);
            this.Name = "Form_Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxNumberOfPairThreadsPerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_largeIconSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_smallIconSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_usePreviousCollectionCache;
        private System.Windows.Forms.CheckBox checkBox_useJoystickController;
        private System.Windows.Forms.TextBox textBox_DbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_EmulatorsBasePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_DefaultImagePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_DbPath;
        private System.Windows.Forms.Button button_EmulatorsBasePath;
        private System.Windows.Forms.Button button_DefaultImagePath;
        private System.Windows.Forms.NumericUpDown numericUpDown_MaxNumberOfPairThreadsPerList;
        private System.Windows.Forms.NumericUpDown numericUpDown_largeIconSize;
        private System.Windows.Forms.NumericUpDown numericUpDown_smallIconSize;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel linkLabel_Github_GameLauncher;
        private System.Windows.Forms.CheckBox checkBox_disableAdvanceOptions;
        private System.Windows.Forms.CheckBox checkBox_EnableImageChecksum;
        private System.Windows.Forms.CheckBox checkBox_EnableRomChecksum;
    }
}