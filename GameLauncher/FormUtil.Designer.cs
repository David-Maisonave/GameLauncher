namespace GameLauncher
{
    partial class FormUtil
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
            this.button_Rescan = new System.Windows.Forms.Button();
            this.button_ScanSelectedSystemRoms = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_RegexRenameFiles = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_ScanForNewRomsOnAllSystems = new System.Windows.Forms.Button();
            this.button_ScanSelectedSystemRomsAndImages = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Rescan
            // 
            this.button_Rescan.Location = new System.Drawing.Point(11, 13);
            this.button_Rescan.Margin = new System.Windows.Forms.Padding(2);
            this.button_Rescan.Name = "button_Rescan";
            this.button_Rescan.Size = new System.Drawing.Size(442, 28);
            this.button_Rescan.TabIndex = 9;
            this.button_Rescan.TabStop = false;
            this.button_Rescan.Text = "&Rescan All ROM\'s";
            this.toolTip1.SetToolTip(this.button_Rescan, "Rescan all ROM\'s and images by first deleting ROM data in GameLauncher database.");
            this.button_Rescan.UseVisualStyleBackColor = true;
            this.button_Rescan.Click += new System.EventHandler(this.button_Rescan_Click);
            // 
            // button_ScanSelectedSystemRoms
            // 
            this.button_ScanSelectedSystemRoms.Location = new System.Drawing.Point(11, 78);
            this.button_ScanSelectedSystemRoms.Margin = new System.Windows.Forms.Padding(2);
            this.button_ScanSelectedSystemRoms.Name = "button_ScanSelectedSystemRoms";
            this.button_ScanSelectedSystemRoms.Size = new System.Drawing.Size(442, 28);
            this.button_ScanSelectedSystemRoms.TabIndex = 10;
            this.button_ScanSelectedSystemRoms.TabStop = false;
            this.button_ScanSelectedSystemRoms.Text = "&Scan selected system for new ROM\'s";
            this.toolTip1.SetToolTip(this.button_ScanSelectedSystemRoms, "Scan for new ROM\'s on selected System/Console. Do NOT scan for new images.");
            this.button_ScanSelectedSystemRoms.UseVisualStyleBackColor = true;
            this.button_ScanSelectedSystemRoms.Click += new System.EventHandler(this.button_ScanSelectedSystem_Click);
            // 
            // button_RegexRenameFiles
            // 
            this.button_RegexRenameFiles.Location = new System.Drawing.Point(11, 149);
            this.button_RegexRenameFiles.Name = "button_RegexRenameFiles";
            this.button_RegexRenameFiles.Size = new System.Drawing.Size(441, 29);
            this.button_RegexRenameFiles.TabIndex = 11;
            this.button_RegexRenameFiles.Text = "Regex Rename Files";
            this.button_RegexRenameFiles.UseVisualStyleBackColor = true;
            this.button_RegexRenameFiles.Click += new System.EventHandler(this.button_RegexRenameFiles_Click);
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Close.Location = new System.Drawing.Point(188, 222);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(86, 28);
            this.button_Close.TabIndex = 12;
            this.button_Close.Text = "&Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_ScanForNewRomsOnAllSystems
            // 
            this.button_ScanForNewRomsOnAllSystems.Location = new System.Drawing.Point(12, 46);
            this.button_ScanForNewRomsOnAllSystems.Margin = new System.Windows.Forms.Padding(2);
            this.button_ScanForNewRomsOnAllSystems.Name = "button_ScanForNewRomsOnAllSystems";
            this.button_ScanForNewRomsOnAllSystems.Size = new System.Drawing.Size(441, 28);
            this.button_ScanForNewRomsOnAllSystems.TabIndex = 13;
            this.button_ScanForNewRomsOnAllSystems.TabStop = false;
            this.button_ScanForNewRomsOnAllSystems.Text = "Scan for &new ROM\'s on all systems";
            this.toolTip1.SetToolTip(this.button_ScanForNewRomsOnAllSystems, "Scan for new ROM\'s on all system consoles. Does NOT scan for new images.");
            this.button_ScanForNewRomsOnAllSystems.UseVisualStyleBackColor = true;
            this.button_ScanForNewRomsOnAllSystems.Click += new System.EventHandler(this.button_ScanForNewRomsOnAllSystems_Click);
            // 
            // button_ScanSelectedSystemRomsAndImages
            // 
            this.button_ScanSelectedSystemRomsAndImages.Location = new System.Drawing.Point(11, 115);
            this.button_ScanSelectedSystemRomsAndImages.Margin = new System.Windows.Forms.Padding(2);
            this.button_ScanSelectedSystemRomsAndImages.Name = "button_ScanSelectedSystemRomsAndImages";
            this.button_ScanSelectedSystemRomsAndImages.Size = new System.Drawing.Size(442, 28);
            this.button_ScanSelectedSystemRomsAndImages.TabIndex = 14;
            this.button_ScanSelectedSystemRomsAndImages.TabStop = false;
            this.button_ScanSelectedSystemRomsAndImages.Text = "&Scan selected system for new ROM\'s and images";
            this.toolTip1.SetToolTip(this.button_ScanSelectedSystemRomsAndImages, "Scan for new ROM\'s and images on selected System/Console.");
            this.button_ScanSelectedSystemRomsAndImages.UseVisualStyleBackColor = true;
            this.button_ScanSelectedSystemRomsAndImages.Click += new System.EventHandler(this.button_ScanSelectedSystemRomsAndImages_Click);
            // 
            // FormUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 259);
            this.Controls.Add(this.button_ScanSelectedSystemRomsAndImages);
            this.Controls.Add(this.button_ScanForNewRomsOnAllSystems);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_RegexRenameFiles);
            this.Controls.Add(this.button_ScanSelectedSystemRoms);
            this.Controls.Add(this.button_Rescan);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(480, 298);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 298);
            this.Name = "FormUtil";
            this.Text = "FormUtil";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Rescan;
        private System.Windows.Forms.Button button_ScanSelectedSystemRoms;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_RegexRenameFiles;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button button_ScanForNewRomsOnAllSystems;
        private System.Windows.Forms.Button button_ScanSelectedSystemRomsAndImages;
    }
}