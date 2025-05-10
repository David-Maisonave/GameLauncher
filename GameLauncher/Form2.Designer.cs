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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MaxNumberOfPairThreadsPerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_largeIconSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_smallIconSize)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_usePreviousCollectionCache
            // 
            this.checkBox_usePreviousCollectionCache.AutoSize = true;
            this.checkBox_usePreviousCollectionCache.Location = new System.Drawing.Point(551, 128);
            this.checkBox_usePreviousCollectionCache.Name = "checkBox_usePreviousCollectionCache";
            this.checkBox_usePreviousCollectionCache.Size = new System.Drawing.Size(251, 24);
            this.checkBox_usePreviousCollectionCache.TabIndex = 0;
            this.checkBox_usePreviousCollectionCache.Text = "Use Previous Collection Cache";
            this.checkBox_usePreviousCollectionCache.UseVisualStyleBackColor = true;
            // 
            // checkBox_useJoystickController
            // 
            this.checkBox_useJoystickController.AutoSize = true;
            this.checkBox_useJoystickController.Location = new System.Drawing.Point(551, 168);
            this.checkBox_useJoystickController.Name = "checkBox_useJoystickController";
            this.checkBox_useJoystickController.Size = new System.Drawing.Size(268, 24);
            this.checkBox_useJoystickController.TabIndex = 1;
            this.checkBox_useJoystickController.Text = "Enable Joystick Controller Usage";
            this.checkBox_useJoystickController.UseVisualStyleBackColor = true;
            // 
            // textBox_DbPath
            // 
            this.textBox_DbPath.Location = new System.Drawing.Point(255, 22);
            this.textBox_DbPath.Name = "textBox_DbPath";
            this.textBox_DbPath.Size = new System.Drawing.Size(576, 26);
            this.textBox_DbPath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Database Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Emulators Directory";
            // 
            // textBox_EmulatorsBasePath
            // 
            this.textBox_EmulatorsBasePath.Location = new System.Drawing.Point(255, 58);
            this.textBox_EmulatorsBasePath.Name = "textBox_EmulatorsBasePath";
            this.textBox_EmulatorsBasePath.Size = new System.Drawing.Size(576, 26);
            this.textBox_EmulatorsBasePath.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Default Image File";
            // 
            // textBox_DefaultImagePath
            // 
            this.textBox_DefaultImagePath.Location = new System.Drawing.Point(255, 94);
            this.textBox_DefaultImagePath.Name = "textBox_DefaultImagePath";
            this.textBox_DefaultImagePath.Size = new System.Drawing.Size(576, 26);
            this.textBox_DefaultImagePath.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Max Threads";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Large Icon Size";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Small Icon Size";
            // 
            // button_DbPath
            // 
            this.button_DbPath.Location = new System.Drawing.Point(838, 20);
            this.button_DbPath.Name = "button_DbPath";
            this.button_DbPath.Size = new System.Drawing.Size(55, 30);
            this.button_DbPath.TabIndex = 14;
            this.button_DbPath.Text = "...";
            this.button_DbPath.UseVisualStyleBackColor = true;
            this.button_DbPath.Click += new System.EventHandler(this.button_DbPath_Click);
            // 
            // button_EmulatorsBasePath
            // 
            this.button_EmulatorsBasePath.Location = new System.Drawing.Point(838, 56);
            this.button_EmulatorsBasePath.Name = "button_EmulatorsBasePath";
            this.button_EmulatorsBasePath.Size = new System.Drawing.Size(55, 30);
            this.button_EmulatorsBasePath.TabIndex = 15;
            this.button_EmulatorsBasePath.Text = "...";
            this.button_EmulatorsBasePath.UseVisualStyleBackColor = true;
            this.button_EmulatorsBasePath.Click += new System.EventHandler(this.button_EmulatorsBasePath_Click);
            // 
            // button_DefaultImagePath
            // 
            this.button_DefaultImagePath.Location = new System.Drawing.Point(838, 89);
            this.button_DefaultImagePath.Name = "button_DefaultImagePath";
            this.button_DefaultImagePath.Size = new System.Drawing.Size(55, 30);
            this.button_DefaultImagePath.TabIndex = 16;
            this.button_DefaultImagePath.Text = "...";
            this.button_DefaultImagePath.UseVisualStyleBackColor = true;
            this.button_DefaultImagePath.Click += new System.EventHandler(this.button_DefaultImagePath_Click);
            // 
            // numericUpDown_MaxNumberOfPairThreadsPerList
            // 
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Location = new System.Drawing.Point(255, 136);
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Name = "numericUpDown_MaxNumberOfPairThreadsPerList";
            this.numericUpDown_MaxNumberOfPairThreadsPerList.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown_MaxNumberOfPairThreadsPerList.TabIndex = 17;
            // 
            // numericUpDown_largeIconSize
            // 
            this.numericUpDown_largeIconSize.Increment = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDown_largeIconSize.Location = new System.Drawing.Point(255, 168);
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
            this.numericUpDown_largeIconSize.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown_largeIconSize.TabIndex = 18;
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
            this.numericUpDown_smallIconSize.Location = new System.Drawing.Point(255, 200);
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
            this.numericUpDown_smallIconSize.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown_smallIconSize.TabIndex = 19;
            this.numericUpDown_smallIconSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDown_smallIconSize.ValueChanged += new System.EventHandler(this.numericUpDown_smallIconSize_ValueChanged);
            // 
            // button_Ok
            // 
            this.button_Ok.Location = new System.Drawing.Point(330, 258);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(100, 40);
            this.button_Ok.TabIndex = 20;
            this.button_Ok.Text = "OK";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(473, 258);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(113, 43);
            this.button_Cancel.TabIndex = 21;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 310);
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
            this.Controls.Add(this.checkBox_useJoystickController);
            this.Controls.Add(this.checkBox_usePreviousCollectionCache);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
    }
}