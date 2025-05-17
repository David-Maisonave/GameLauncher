namespace GameLauncher
{
    partial class FormAdvance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdvance));
            this.button_Rescan = new System.Windows.Forms.Button();
            this.button_ScanSelectedSystemRoms = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_ScanForNewRomsOnAllSystems = new System.Windows.Forms.Button();
            this.button_ScanSelectedSystemRomsAndImages = new System.Windows.Forms.Button();
            this.button_ResetImageListCache = new System.Windows.Forms.Button();
            this.button_DelImgFilesNotInDb = new System.Windows.Forms.Button();
            this.button_DeleteDuplicateRomsByChecksum = new System.Windows.Forms.Button();
            this.button_RegexRenameFiles = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_DeleteDuplicateRomsByTitleInSameSystem = new System.Windows.Forms.Button();
            this.button_DeleteDuplicateRomsByTitleInAnySystem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Rescan
            // 
            this.button_Rescan.Location = new System.Drawing.Point(11, 13);
            this.button_Rescan.Margin = new System.Windows.Forms.Padding(2);
            this.button_Rescan.Name = "button_Rescan";
            this.button_Rescan.Size = new System.Drawing.Size(442, 28);
            this.button_Rescan.TabIndex = 0;
            this.button_Rescan.TabStop = false;
            this.button_Rescan.Text = "&Rescan All ROM\'s";
            this.toolTip1.SetToolTip(this.button_Rescan, "Rescan all ROM\'s and images by first deleting ROM data in GameLauncher database.");
            this.button_Rescan.UseVisualStyleBackColor = true;
            this.button_Rescan.Click += new System.EventHandler(this.button_Rescan_Click);
            // 
            // button_ScanSelectedSystemRoms
            // 
            this.button_ScanSelectedSystemRoms.Location = new System.Drawing.Point(11, 79);
            this.button_ScanSelectedSystemRoms.Margin = new System.Windows.Forms.Padding(2);
            this.button_ScanSelectedSystemRoms.Name = "button_ScanSelectedSystemRoms";
            this.button_ScanSelectedSystemRoms.Size = new System.Drawing.Size(442, 28);
            this.button_ScanSelectedSystemRoms.TabIndex = 2;
            this.button_ScanSelectedSystemRoms.TabStop = false;
            this.button_ScanSelectedSystemRoms.Text = "&Scan selected system for new ROM\'s";
            this.toolTip1.SetToolTip(this.button_ScanSelectedSystemRoms, "Scan for new ROM\'s on selected System/Console. Do NOT scan for new images.");
            this.button_ScanSelectedSystemRoms.UseVisualStyleBackColor = true;
            this.button_ScanSelectedSystemRoms.Click += new System.EventHandler(this.button_ScanSelectedSystem_Click);
            // 
            // button_ScanForNewRomsOnAllSystems
            // 
            this.button_ScanForNewRomsOnAllSystems.Location = new System.Drawing.Point(12, 46);
            this.button_ScanForNewRomsOnAllSystems.Margin = new System.Windows.Forms.Padding(2);
            this.button_ScanForNewRomsOnAllSystems.Name = "button_ScanForNewRomsOnAllSystems";
            this.button_ScanForNewRomsOnAllSystems.Size = new System.Drawing.Size(441, 28);
            this.button_ScanForNewRomsOnAllSystems.TabIndex = 1;
            this.button_ScanForNewRomsOnAllSystems.TabStop = false;
            this.button_ScanForNewRomsOnAllSystems.Text = "Scan for &new ROM\'s on all systems";
            this.toolTip1.SetToolTip(this.button_ScanForNewRomsOnAllSystems, "Scan for new ROM\'s on all system consoles. Does NOT scan for new images.");
            this.button_ScanForNewRomsOnAllSystems.UseVisualStyleBackColor = true;
            this.button_ScanForNewRomsOnAllSystems.Click += new System.EventHandler(this.button_ScanForNewRomsOnAllSystems_Click);
            // 
            // button_ScanSelectedSystemRomsAndImages
            // 
            this.button_ScanSelectedSystemRomsAndImages.Location = new System.Drawing.Point(11, 112);
            this.button_ScanSelectedSystemRomsAndImages.Margin = new System.Windows.Forms.Padding(2);
            this.button_ScanSelectedSystemRomsAndImages.Name = "button_ScanSelectedSystemRomsAndImages";
            this.button_ScanSelectedSystemRomsAndImages.Size = new System.Drawing.Size(442, 28);
            this.button_ScanSelectedSystemRomsAndImages.TabIndex = 3;
            this.button_ScanSelectedSystemRomsAndImages.TabStop = false;
            this.button_ScanSelectedSystemRomsAndImages.Text = "&Scan selected system for new ROM\'s and images";
            this.toolTip1.SetToolTip(this.button_ScanSelectedSystemRomsAndImages, "Scan for new ROM\'s and images on selected System/Console.");
            this.button_ScanSelectedSystemRomsAndImages.UseVisualStyleBackColor = true;
            this.button_ScanSelectedSystemRomsAndImages.Click += new System.EventHandler(this.button_ScanSelectedSystemRomsAndImages_Click);
            // 
            // button_ResetImageListCache
            // 
            this.button_ResetImageListCache.Location = new System.Drawing.Point(12, 145);
            this.button_ResetImageListCache.Name = "button_ResetImageListCache";
            this.button_ResetImageListCache.Size = new System.Drawing.Size(441, 29);
            this.button_ResetImageListCache.TabIndex = 4;
            this.button_ResetImageListCache.Text = "Create/Recreate ImageList Cache";
            this.toolTip1.SetToolTip(this.button_ResetImageListCache, "Create or recreate ImageList cache for all systems.");
            this.button_ResetImageListCache.UseVisualStyleBackColor = true;
            this.button_ResetImageListCache.Click += new System.EventHandler(this.button_ResetImageListCache_Click);
            // 
            // button_DelImgFilesNotInDb
            // 
            this.button_DelImgFilesNotInDb.Location = new System.Drawing.Point(11, 215);
            this.button_DelImgFilesNotInDb.Name = "button_DelImgFilesNotInDb";
            this.button_DelImgFilesNotInDb.Size = new System.Drawing.Size(441, 29);
            this.button_DelImgFilesNotInDb.TabIndex = 51;
            this.button_DelImgFilesNotInDb.Text = "Delete duplicate image files not in database";
            this.toolTip1.SetToolTip(this.button_DelImgFilesNotInDb, resources.GetString("button_DelImgFilesNotInDb.ToolTip"));
            this.button_DelImgFilesNotInDb.UseVisualStyleBackColor = true;
            this.button_DelImgFilesNotInDb.Click += new System.EventHandler(this.button_DelImgFilesNotInDb_Click);
            // 
            // button_DeleteDuplicateRomsByChecksum
            // 
            this.button_DeleteDuplicateRomsByChecksum.Location = new System.Drawing.Point(12, 250);
            this.button_DeleteDuplicateRomsByChecksum.Name = "button_DeleteDuplicateRomsByChecksum";
            this.button_DeleteDuplicateRomsByChecksum.Size = new System.Drawing.Size(441, 29);
            this.button_DeleteDuplicateRomsByChecksum.TabIndex = 52;
            this.button_DeleteDuplicateRomsByChecksum.Text = "Delete duplicate ROM\'s by checksum";
            this.toolTip1.SetToolTip(this.button_DeleteDuplicateRomsByChecksum, resources.GetString("button_DeleteDuplicateRomsByChecksum.ToolTip"));
            this.button_DeleteDuplicateRomsByChecksum.UseVisualStyleBackColor = true;
            this.button_DeleteDuplicateRomsByChecksum.Click += new System.EventHandler(this.button_DelRomFilesInDb_Click);
            // 
            // button_RegexRenameFiles
            // 
            this.button_RegexRenameFiles.Location = new System.Drawing.Point(12, 180);
            this.button_RegexRenameFiles.Name = "button_RegexRenameFiles";
            this.button_RegexRenameFiles.Size = new System.Drawing.Size(441, 29);
            this.button_RegexRenameFiles.TabIndex = 5;
            this.button_RegexRenameFiles.Text = "Regex Rename Files";
            this.button_RegexRenameFiles.UseVisualStyleBackColor = true;
            this.button_RegexRenameFiles.Click += new System.EventHandler(this.button_RegexRenameFiles_Click);
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Close.Location = new System.Drawing.Point(188, 366);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(86, 28);
            this.button_Close.TabIndex = 50;
            this.button_Close.Text = "&Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_DeleteDuplicateRomsByTitleInSameSystem
            // 
            this.button_DeleteDuplicateRomsByTitleInSameSystem.Location = new System.Drawing.Point(11, 285);
            this.button_DeleteDuplicateRomsByTitleInSameSystem.Name = "button_DeleteDuplicateRomsByTitleInSameSystem";
            this.button_DeleteDuplicateRomsByTitleInSameSystem.Size = new System.Drawing.Size(441, 29);
            this.button_DeleteDuplicateRomsByTitleInSameSystem.TabIndex = 53;
            this.button_DeleteDuplicateRomsByTitleInSameSystem.Text = "Delete duplicate ROM\'s by title in same system";
            this.toolTip1.SetToolTip(this.button_DeleteDuplicateRomsByTitleInSameSystem, resources.GetString("button_DeleteDuplicateRomsByTitleInSameSystem.ToolTip"));
            this.button_DeleteDuplicateRomsByTitleInSameSystem.UseVisualStyleBackColor = true;
            this.button_DeleteDuplicateRomsByTitleInSameSystem.Click += new System.EventHandler(this.button_DeleteDuplicateRomsByTitleInSameSystem_Click);
            // 
            // button_DeleteDuplicateRomsByTitleInAnySystem
            // 
            this.button_DeleteDuplicateRomsByTitleInAnySystem.Location = new System.Drawing.Point(11, 320);
            this.button_DeleteDuplicateRomsByTitleInAnySystem.Name = "button_DeleteDuplicateRomsByTitleInAnySystem";
            this.button_DeleteDuplicateRomsByTitleInAnySystem.Size = new System.Drawing.Size(441, 29);
            this.button_DeleteDuplicateRomsByTitleInAnySystem.TabIndex = 54;
            this.button_DeleteDuplicateRomsByTitleInAnySystem.Text = "Delete duplicate ROM\'s by title in any system";
            this.toolTip1.SetToolTip(this.button_DeleteDuplicateRomsByTitleInAnySystem, resources.GetString("button_DeleteDuplicateRomsByTitleInAnySystem.ToolTip"));
            this.button_DeleteDuplicateRomsByTitleInAnySystem.UseVisualStyleBackColor = true;
            this.button_DeleteDuplicateRomsByTitleInAnySystem.Click += new System.EventHandler(this.button_DeleteDuplicateRomsByTitleInAnySystem_Click);
            // 
            // FormAdvance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 403);
            this.Controls.Add(this.button_DeleteDuplicateRomsByTitleInAnySystem);
            this.Controls.Add(this.button_DeleteDuplicateRomsByTitleInSameSystem);
            this.Controls.Add(this.button_DeleteDuplicateRomsByChecksum);
            this.Controls.Add(this.button_DelImgFilesNotInDb);
            this.Controls.Add(this.button_ResetImageListCache);
            this.Controls.Add(this.button_ScanSelectedSystemRomsAndImages);
            this.Controls.Add(this.button_ScanForNewRomsOnAllSystems);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_RegexRenameFiles);
            this.Controls.Add(this.button_ScanSelectedSystemRoms);
            this.Controls.Add(this.button_Rescan);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAdvance";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Advance Options";
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
        private System.Windows.Forms.Button button_ResetImageListCache;
        private System.Windows.Forms.Button button_DelImgFilesNotInDb;
        private System.Windows.Forms.Button button_DeleteDuplicateRomsByChecksum;
        private System.Windows.Forms.Button button_DeleteDuplicateRomsByTitleInSameSystem;
        private System.Windows.Forms.Button button_DeleteDuplicateRomsByTitleInAnySystem;
    }
}