namespace GameLauncher
{
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.myListView = new System.Windows.Forms.ListView();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPlayRom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRenameROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAssignPreferredEmulator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeAssignedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.searchImageAtLaunchBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip_Info = new System.Windows.Forms.ToolTip(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox_ProgressBar = new System.Windows.Forms.GroupBox();
            this.button_CancelScan = new System.Windows.Forms.Button();
            this.progressBar_ROMs = new System.Windows.Forms.ProgressBar();
            this.label_RomScanLabel = new System.Windows.Forms.Label();
            this.label_GameConsoleLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripComboBoxSystem = new System.Windows.Forms.ToolStripComboBox();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxIconDisplay = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItemChangeDefaultEmulator = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox_Filter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem_FilterLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox_ProgressBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // myListView
            // 
            this.myListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myListView.HideSelection = false;
            this.myListView.Location = new System.Drawing.Point(12, 30);
            this.myListView.MultiSelect = false;
            this.myListView.Name = "myListView";
            this.myListView.ShowGroups = false;
            this.myListView.ShowItemToolTips = true;
            this.myListView.Size = new System.Drawing.Size(560, 315);
            this.myListView.TabIndex = 0;
            this.myListView.UseCompatibleStateImageBehavior = false;
            this.myListView.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.myListView_ItemMouseHover);
            this.myListView.DoubleClick += new System.EventHandler(this.myListView_OnDbClick);
            this.myListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.myListView_KeyDown);
            this.myListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.myListView_MouseClick);
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStatus.Location = new System.Drawing.Point(12, 346);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(560, 13);
            this.textBoxStatus.TabIndex = 7;
            this.textBoxStatus.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPlayRom,
            this.toolStripMenuItemRenameROM,
            this.toolStripMenuItemDeleteROM,
            this.toolStripMenuItemAssignPreferredEmulator,
            this.toolStripMenuItemChangeAssignedImage,
            this.toolStripMenuItemChangeTitle,
            this.searchImageAtLaunchBoxToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(220, 158);
            // 
            // toolStripMenuItemPlayRom
            // 
            this.toolStripMenuItemPlayRom.Name = "toolStripMenuItemPlayRom";
            this.toolStripMenuItemPlayRom.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemPlayRom.Text = "Play ROM (Game)";
            this.toolStripMenuItemPlayRom.Click += new System.EventHandler(this.myListViewContextMenu_Play_Click);
            // 
            // toolStripMenuItemRenameROM
            // 
            this.toolStripMenuItemRenameROM.AutoToolTip = true;
            this.toolStripMenuItemRenameROM.Name = "toolStripMenuItemRenameROM";
            this.toolStripMenuItemRenameROM.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemRenameROM.Text = "Rename ROM (Game)";
            this.toolStripMenuItemRenameROM.ToolTipText = "Rename the ROM file on the file system.";
            this.toolStripMenuItemRenameROM.Click += new System.EventHandler(this.myListViewContextMenu_RenameROM_Click);
            // 
            // toolStripMenuItemDeleteROM
            // 
            this.toolStripMenuItemDeleteROM.AutoToolTip = true;
            this.toolStripMenuItemDeleteROM.Name = "toolStripMenuItemDeleteROM";
            this.toolStripMenuItemDeleteROM.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemDeleteROM.Text = "Delete ROM (Game)";
            this.toolStripMenuItemDeleteROM.ToolTipText = resources.GetString("toolStripMenuItemDeleteROM.ToolTipText");
            this.toolStripMenuItemDeleteROM.Click += new System.EventHandler(this.myListViewContextMenu_DeleteROM_Click);
            // 
            // toolStripMenuItemAssignPreferredEmulator
            // 
            this.toolStripMenuItemAssignPreferredEmulator.AutoToolTip = true;
            this.toolStripMenuItemAssignPreferredEmulator.Name = "toolStripMenuItemAssignPreferredEmulator";
            this.toolStripMenuItemAssignPreferredEmulator.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemAssignPreferredEmulator.Text = "Assign Preferred Emulator";
            this.toolStripMenuItemAssignPreferredEmulator.ToolTipText = "Assign preferred emulator for the selected ROM.";
            this.toolStripMenuItemAssignPreferredEmulator.Click += new System.EventHandler(this.myListViewContextMenu_AssignPreferredEmulator_Click);
            // 
            // toolStripMenuItemChangeAssignedImage
            // 
            this.toolStripMenuItemChangeAssignedImage.AutoToolTip = true;
            this.toolStripMenuItemChangeAssignedImage.Name = "toolStripMenuItemChangeAssignedImage";
            this.toolStripMenuItemChangeAssignedImage.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemChangeAssignedImage.Text = "Change Assigned Image";
            this.toolStripMenuItemChangeAssignedImage.ToolTipText = "Change image associated with selected ROM file.\r\nNote: The change is not reflecte" +
    "d on the list until after restarting GameLauncher or after changing the selected" +
    " game console system.\r\n";
            this.toolStripMenuItemChangeAssignedImage.Click += new System.EventHandler(this.myListViewContextMenu_ChangeAssignedImage_Click);
            // 
            // toolStripMenuItemChangeTitle
            // 
            this.toolStripMenuItemChangeTitle.AutoToolTip = true;
            this.toolStripMenuItemChangeTitle.Name = "toolStripMenuItemChangeTitle";
            this.toolStripMenuItemChangeTitle.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemChangeTitle.Text = "Change ROM Title";
            this.toolStripMenuItemChangeTitle.ToolTipText = resources.GetString("toolStripMenuItemChangeTitle.ToolTipText");
            this.toolStripMenuItemChangeTitle.Click += new System.EventHandler(this.myListViewContextMenu_ChangeTitle_Click);
            // 
            // searchImageAtLaunchBoxToolStripMenuItem
            // 
            this.searchImageAtLaunchBoxToolStripMenuItem.Name = "searchImageAtLaunchBoxToolStripMenuItem";
            this.searchImageAtLaunchBoxToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.searchImageAtLaunchBoxToolStripMenuItem.Text = "Search Image at LaunchBox";
            this.searchImageAtLaunchBoxToolStripMenuItem.Click += new System.EventHandler(this.searchImageAtLaunchBoxToolStripMenuItem_Click);
            // 
            // toolTip_Info
            // 
            this.toolTip_Info.AutomaticDelay = 1000;
            this.toolTip_Info.AutoPopDelay = 10000;
            this.toolTip_Info.InitialDelay = 1000;
            this.toolTip_Info.IsBalloon = true;
            this.toolTip_Info.ReshowDelay = 500;
            this.toolTip_Info.ShowAlways = true;
            this.toolTip_Info.ToolTipTitle = "Info";
            this.toolTip_Info.UseAnimation = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(6, 53);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(548, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 96;
            this.progressBar1.UseWaitCursor = true;
            this.progressBar1.Visible = false;
            // 
            // groupBox_ProgressBar
            // 
            this.groupBox_ProgressBar.Controls.Add(this.button_CancelScan);
            this.groupBox_ProgressBar.Controls.Add(this.progressBar_ROMs);
            this.groupBox_ProgressBar.Controls.Add(this.label_RomScanLabel);
            this.groupBox_ProgressBar.Controls.Add(this.progressBar1);
            this.groupBox_ProgressBar.Controls.Add(this.label_GameConsoleLabel);
            this.groupBox_ProgressBar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox_ProgressBar.Location = new System.Drawing.Point(12, 117);
            this.groupBox_ProgressBar.Name = "groupBox_ProgressBar";
            this.groupBox_ProgressBar.Size = new System.Drawing.Size(560, 196);
            this.groupBox_ProgressBar.TabIndex = 90;
            this.groupBox_ProgressBar.TabStop = false;
            this.groupBox_ProgressBar.Text = "Progress on initial ROM database creation";
            this.groupBox_ProgressBar.Visible = false;
            // 
            // button_CancelScan
            // 
            this.button_CancelScan.Location = new System.Drawing.Point(252, 155);
            this.button_CancelScan.Name = "button_CancelScan";
            this.button_CancelScan.Size = new System.Drawing.Size(75, 35);
            this.button_CancelScan.TabIndex = 101;
            this.button_CancelScan.Text = "&Cancel";
            this.button_CancelScan.UseVisualStyleBackColor = true;
            this.button_CancelScan.Visible = false;
            this.button_CancelScan.Click += new System.EventHandler(this.button_CancelScan_Click);
            // 
            // progressBar_ROMs
            // 
            this.progressBar_ROMs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_ROMs.Location = new System.Drawing.Point(6, 115);
            this.progressBar_ROMs.Name = "progressBar_ROMs";
            this.progressBar_ROMs.Size = new System.Drawing.Size(548, 23);
            this.progressBar_ROMs.Step = 1;
            this.progressBar_ROMs.TabIndex = 98;
            this.progressBar_ROMs.UseWaitCursor = true;
            this.progressBar_ROMs.Visible = false;
            // 
            // label_RomScanLabel
            // 
            this.label_RomScanLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_RomScanLabel.AutoSize = true;
            this.label_RomScanLabel.Location = new System.Drawing.Point(8, 92);
            this.label_RomScanLabel.Name = "label_RomScanLabel";
            this.label_RomScanLabel.Size = new System.Drawing.Size(89, 13);
            this.label_RomScanLabel.TabIndex = 100;
            this.label_RomScanLabel.Text = "ROM scan status";
            this.label_RomScanLabel.Visible = false;
            // 
            // label_GameConsoleLabel
            // 
            this.label_GameConsoleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_GameConsoleLabel.AutoSize = true;
            this.label_GameConsoleLabel.Location = new System.Drawing.Point(8, 29);
            this.label_GameConsoleLabel.Name = "label_GameConsoleLabel";
            this.label_GameConsoleLabel.Size = new System.Drawing.Size(146, 13);
            this.label_GameConsoleLabel.TabIndex = 99;
            this.label_GameConsoleLabel.Text = "Game Console System Status";
            this.label_GameConsoleLabel.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxSystem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem_FilterLabel,
            this.toolStripTextBox_Filter});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(584, 27);
            this.menuStrip1.TabIndex = 91;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripComboBoxSystem
            // 
            this.toolStripComboBoxSystem.AutoToolTip = true;
            this.toolStripComboBoxSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSystem.Name = "toolStripComboBoxSystem";
            this.toolStripComboBoxSystem.Size = new System.Drawing.Size(248, 23);
            this.toolStripComboBoxSystem.ToolTipText = "Select desired game console system";
            this.toolStripComboBoxSystem.SelectedIndexChanged += new System.EventHandler(this.System_Change);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxIconDisplay,
            this.toolStripMenuItemChangeDefaultEmulator,
            this.advancedOptionsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // toolStripComboBoxIconDisplay
            // 
            this.toolStripComboBoxIconDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxIconDisplay.Name = "toolStripComboBoxIconDisplay";
            this.toolStripComboBoxIconDisplay.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxIconDisplay.ToolTipText = "Select list icon size and format.";
            this.toolStripComboBoxIconDisplay.SelectedIndexChanged += new System.EventHandler(this.Display_Change);
            // 
            // toolStripMenuItemChangeDefaultEmulator
            // 
            this.toolStripMenuItemChangeDefaultEmulator.AutoToolTip = true;
            this.toolStripMenuItemChangeDefaultEmulator.Name = "toolStripMenuItemChangeDefaultEmulator";
            this.toolStripMenuItemChangeDefaultEmulator.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemChangeDefaultEmulator.Text = "Change Default Emulator";
            this.toolStripMenuItemChangeDefaultEmulator.ToolTipText = "Change the default emulator executable for the currently selected game console sy" +
    "stem.";
            this.toolStripMenuItemChangeDefaultEmulator.Click += new System.EventHandler(this.toolStripMenuItemChangeDefaultEmulator_Click);
            // 
            // advancedOptionsToolStripMenuItem
            // 
            this.advancedOptionsToolStripMenuItem.Name = "advancedOptionsToolStripMenuItem";
            this.advancedOptionsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.advancedOptionsToolStripMenuItem.Text = "Advanced Options";
            this.advancedOptionsToolStripMenuItem.Click += new System.EventHandler(this.advancedOptionsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.ToolTipText = "Select changes to default settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripTextBox_Filter
            // 
            this.toolStripTextBox_Filter.AcceptsReturn = true;
            this.toolStripTextBox_Filter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripTextBox_Filter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.toolStripTextBox_Filter.AutoToolTip = true;
            this.toolStripTextBox_Filter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_Filter.Name = "toolStripTextBox_Filter";
            this.toolStripTextBox_Filter.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox_Filter.ToolTipText = "Only display ROM files having the specified text within the title.";
            this.toolStripTextBox_Filter.Click += new System.EventHandler(this.toolStripTextBox_Filter_Click);
            this.toolStripTextBox_Filter.DoubleClick += new System.EventHandler(this.toolStripTextBox_Filter_DbClick);
            this.toolStripTextBox_Filter.TextChanged += new System.EventHandler(this.toolStripTextBox_Filter_Change);
            // 
            // toolStripMenuItem_FilterLabel
            // 
            this.toolStripMenuItem_FilterLabel.AutoToolTip = true;
            this.toolStripMenuItem_FilterLabel.Enabled = false;
            this.toolStripMenuItem_FilterLabel.Name = "toolStripMenuItem_FilterLabel";
            this.toolStripMenuItem_FilterLabel.ShowShortcutKeys = false;
            this.toolStripMenuItem_FilterLabel.Size = new System.Drawing.Size(45, 23);
            this.toolStripMenuItem_FilterLabel.Text = "Filter";
            this.toolStripMenuItem_FilterLabel.ToolTipText = resources.GetString("toolStripMenuItem_FilterLabel.ToolTipText");
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox_ProgressBar);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.myListView);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form_Main";
            this.Text = "Game Launcher [Ver-0.9.1] by David Maisonave Sr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.myListView_OnFormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox_ProgressBar.ResumeLayout(false);
            this.groupBox_ProgressBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView myListView;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAssignPreferredEmulator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRenameROM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteROM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeAssignedImage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlayRom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeTitle;
        private System.Windows.Forms.ToolTip toolTip_Info;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox_ProgressBar;
        private System.Windows.Forms.ProgressBar progressBar_ROMs;
        private System.Windows.Forms.Label label_GameConsoleLabel;
        private System.Windows.Forms.Label label_RomScanLabel;
        private System.Windows.Forms.Button button_CancelScan;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSystem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxIconDisplay;
        private System.Windows.Forms.ToolStripMenuItem advancedOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeDefaultEmulator;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_Filter;
        private System.Windows.Forms.ToolStripMenuItem searchImageAtLaunchBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FilterLabel;
    }
}

