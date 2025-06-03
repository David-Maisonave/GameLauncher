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
            this.toolStripMenuItem_CopyTitleToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemChangeViewROMDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAssignPreferredEmulator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeAssignedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripMenuItem_RecentGames = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxIconDisplay = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.changeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeDefaultEmulator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_ROMParentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRescanAllRoms = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScanNewRomsAllSystems = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScanSelectedSystemNewRoms = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CleanScanSelectedSystemNewRoms = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteRomsParentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DeleteDupRomsByTitleSameSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DeleteDupRomsByTitleAnySystem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DeleteDupRomsByChecksum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CompressRomFilesParentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertRomToZip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertRomTo7z = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertRomToTar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertRomToGzip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertRomToBZ2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertRomToLZ = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ImagesParentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CreateImageListCache = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ImageSearchSelectedDir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ScanSelectedSystemNewImages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ScanSelectedSystemRomsAndImages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ScanAllSystemNewRomsAndImages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ScanAllSystemsNewRomsAndImages = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DeleteDupImagesNotInDB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_MiscUtilParentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_RegexRenameFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DbUtilParentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ResetRomTitleCompress = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ResetImageTitleCompressInDB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ResetCompressNames = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_PopulateGameDetailsDB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_AddGameDetailsToGameLauncherDb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_ResetYearAndRatingFieldsInDB = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_SearchMatchingImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_FilterLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox_Filter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItemSearchAll = new System.Windows.Forms.ToolStripMenuItem();
            this.numericUpDown_Paginator = new System.Windows.Forms.NumericUpDown();
            this.toolStripMenuItem_ConvertRomToRAR = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertPngToJpg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ConvertAllPngToJpg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_CompressAllRoms = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox_ProgressBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Paginator)).BeginInit();
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
            this.toolStripMenuItem_CopyTitleToClipboard,
            this.toolStripMenuItemDeleteROM,
            this.toolStripSeparator1,
            this.ToolStripMenuItemChangeViewROMDetails,
            this.toolStripMenuItemAssignPreferredEmulator,
            this.toolStripMenuItemChangeAssignedImage,
            this.toolStripMenuItemChangeTitle,
            this.toolStripSeparator2,
            this.searchImageAtLaunchBoxToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(220, 214);
            // 
            // toolStripMenuItemPlayRom
            // 
            this.toolStripMenuItemPlayRom.Name = "toolStripMenuItemPlayRom";
            this.toolStripMenuItemPlayRom.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemPlayRom.Text = "&Play ROM (Game)";
            this.toolStripMenuItemPlayRom.Click += new System.EventHandler(this.myListViewContextMenu_Play_Click);
            // 
            // toolStripMenuItemRenameROM
            // 
            this.toolStripMenuItemRenameROM.AutoToolTip = true;
            this.toolStripMenuItemRenameROM.Name = "toolStripMenuItemRenameROM";
            this.toolStripMenuItemRenameROM.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemRenameROM.Text = "&Rename ROM";
            this.toolStripMenuItemRenameROM.ToolTipText = "Rename the ROM file on the file system.";
            this.toolStripMenuItemRenameROM.Click += new System.EventHandler(this.myListViewContextMenu_RenameROM_Click);
            // 
            // toolStripMenuItem_CopyTitleToClipboard
            // 
            this.toolStripMenuItem_CopyTitleToClipboard.Name = "toolStripMenuItem_CopyTitleToClipboard";
            this.toolStripMenuItem_CopyTitleToClipboard.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem_CopyTitleToClipboard.Text = "&Copy Title to Clipboard";
            this.toolStripMenuItem_CopyTitleToClipboard.Click += new System.EventHandler(this.toolStripMenuItem_CopyTitleToClipboard_Click);
            // 
            // toolStripMenuItemDeleteROM
            // 
            this.toolStripMenuItemDeleteROM.AutoToolTip = true;
            this.toolStripMenuItemDeleteROM.Name = "toolStripMenuItemDeleteROM";
            this.toolStripMenuItemDeleteROM.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemDeleteROM.Text = "&Delete ROM";
            this.toolStripMenuItemDeleteROM.ToolTipText = resources.GetString("toolStripMenuItemDeleteROM.ToolTipText");
            this.toolStripMenuItemDeleteROM.Click += new System.EventHandler(this.myListViewContextMenu_DeleteROM_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // ToolStripMenuItemChangeViewROMDetails
            // 
            this.ToolStripMenuItemChangeViewROMDetails.Name = "ToolStripMenuItemChangeViewROMDetails";
            this.ToolStripMenuItemChangeViewROMDetails.Size = new System.Drawing.Size(219, 22);
            this.ToolStripMenuItemChangeViewROMDetails.Text = "Edit \\ &View ROM Details";
            this.ToolStripMenuItemChangeViewROMDetails.Click += new System.EventHandler(this.ToolStripMenuItemChangeViewROMDetails_Click);
            // 
            // toolStripMenuItemAssignPreferredEmulator
            // 
            this.toolStripMenuItemAssignPreferredEmulator.AutoToolTip = true;
            this.toolStripMenuItemAssignPreferredEmulator.Name = "toolStripMenuItemAssignPreferredEmulator";
            this.toolStripMenuItemAssignPreferredEmulator.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemAssignPreferredEmulator.Text = "&Assign Preferred Emulator";
            this.toolStripMenuItemAssignPreferredEmulator.ToolTipText = "Assign preferred emulator for the selected ROM.";
            this.toolStripMenuItemAssignPreferredEmulator.Click += new System.EventHandler(this.myListViewContextMenu_AssignPreferredEmulator_Click);
            // 
            // toolStripMenuItemChangeAssignedImage
            // 
            this.toolStripMenuItemChangeAssignedImage.AutoToolTip = true;
            this.toolStripMenuItemChangeAssignedImage.Name = "toolStripMenuItemChangeAssignedImage";
            this.toolStripMenuItemChangeAssignedImage.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItemChangeAssignedImage.Text = "Change Assigned &Image";
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
            this.toolStripMenuItemChangeTitle.Text = "Change ROM &Title";
            this.toolStripMenuItemChangeTitle.ToolTipText = resources.GetString("toolStripMenuItemChangeTitle.ToolTipText");
            this.toolStripMenuItemChangeTitle.Click += new System.EventHandler(this.myListViewContextMenu_ChangeTitle_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(216, 6);
            // 
            // searchImageAtLaunchBoxToolStripMenuItem
            // 
            this.searchImageAtLaunchBoxToolStripMenuItem.Name = "searchImageAtLaunchBoxToolStripMenuItem";
            this.searchImageAtLaunchBoxToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.searchImageAtLaunchBoxToolStripMenuItem.Text = "Search Image at &LaunchBox";
            this.searchImageAtLaunchBoxToolStripMenuItem.ToolTipText = "Open browser to google searching ROM title with LaunchBox website.\r\nAlso copies t" +
    "itle to clipboard.";
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
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(6, 46);
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
            this.groupBox_ProgressBar.Location = new System.Drawing.Point(12, 107);
            this.groupBox_ProgressBar.Name = "groupBox_ProgressBar";
            this.groupBox_ProgressBar.Size = new System.Drawing.Size(560, 190);
            this.groupBox_ProgressBar.TabIndex = 90;
            this.groupBox_ProgressBar.TabStop = false;
            this.groupBox_ProgressBar.Text = "Progress on initial ROM database creation";
            this.groupBox_ProgressBar.Visible = false;
            // 
            // button_CancelScan
            // 
            this.button_CancelScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CancelScan.Location = new System.Drawing.Point(252, 147);
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
            this.progressBar_ROMs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_ROMs.Location = new System.Drawing.Point(6, 108);
            this.progressBar_ROMs.Name = "progressBar_ROMs";
            this.progressBar_ROMs.Size = new System.Drawing.Size(548, 23);
            this.progressBar_ROMs.Step = 1;
            this.progressBar_ROMs.TabIndex = 98;
            this.progressBar_ROMs.UseWaitCursor = true;
            this.progressBar_ROMs.Visible = false;
            // 
            // label_RomScanLabel
            // 
            this.label_RomScanLabel.AutoSize = true;
            this.label_RomScanLabel.Location = new System.Drawing.Point(8, 85);
            this.label_RomScanLabel.Name = "label_RomScanLabel";
            this.label_RomScanLabel.Size = new System.Drawing.Size(89, 13);
            this.label_RomScanLabel.TabIndex = 100;
            this.label_RomScanLabel.Text = "ROM scan status";
            this.label_RomScanLabel.Visible = false;
            // 
            // label_GameConsoleLabel
            // 
            this.label_GameConsoleLabel.AutoSize = true;
            this.label_GameConsoleLabel.Location = new System.Drawing.Point(8, 22);
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
            this.toolStripTextBox_Filter,
            this.toolStripMenuItemSearchAll});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(584, 30);
            this.menuStrip1.TabIndex = 91;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripComboBoxSystem
            // 
            this.toolStripComboBoxSystem.AutoToolTip = true;
            this.toolStripComboBoxSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSystem.Name = "toolStripComboBoxSystem";
            this.toolStripComboBoxSystem.Size = new System.Drawing.Size(248, 28);
            this.toolStripComboBoxSystem.ToolTipText = "Select desired game console system";
            this.toolStripComboBoxSystem.SelectedIndexChanged += new System.EventHandler(this.System_Change);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_RecentGames,
            this.toolStripComboBoxIconDisplay,
            this.toolStripSeparator4,
            this.changeViewToolStripMenuItem,
            this.toolStripMenuItemChangeDefaultEmulator,
            this.toolStripSeparator3,
            this.toolStripMenuItem_ROMParentMenu,
            this.toolStripMenuItem_ImagesParentMenu,
            this.toolStripMenuItem_MiscUtilParentMenu,
            this.toolStripSeparator5,
            this.settingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 28);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // toolStripMenuItem_RecentGames
            // 
            this.toolStripMenuItem_RecentGames.Name = "toolStripMenuItem_RecentGames";
            this.toolStripMenuItem_RecentGames.Size = new System.Drawing.Size(251, 22);
            this.toolStripMenuItem_RecentGames.Text = "Recent &Games";
            // 
            // toolStripComboBoxIconDisplay
            // 
            this.toolStripComboBoxIconDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxIconDisplay.Name = "toolStripComboBoxIconDisplay";
            this.toolStripComboBoxIconDisplay.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxIconDisplay.ToolTipText = "Select list icon size and format.";
            this.toolStripComboBoxIconDisplay.SelectedIndexChanged += new System.EventHandler(this.Display_Change);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(248, 6);
            // 
            // changeViewToolStripMenuItem
            // 
            this.changeViewToolStripMenuItem.Name = "changeViewToolStripMenuItem";
            this.changeViewToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.changeViewToolStripMenuItem.Text = "&Edit \\ View Game Console System";
            this.changeViewToolStripMenuItem.Click += new System.EventHandler(this.changeViewToolStripMenuItem_Click);
            // 
            // toolStripMenuItemChangeDefaultEmulator
            // 
            this.toolStripMenuItemChangeDefaultEmulator.AutoToolTip = true;
            this.toolStripMenuItemChangeDefaultEmulator.Name = "toolStripMenuItemChangeDefaultEmulator";
            this.toolStripMenuItemChangeDefaultEmulator.Size = new System.Drawing.Size(251, 22);
            this.toolStripMenuItemChangeDefaultEmulator.Text = "&Change Default Emulator";
            this.toolStripMenuItemChangeDefaultEmulator.ToolTipText = "Change the default emulator executable for the currently selected game console sy" +
    "stem.";
            this.toolStripMenuItemChangeDefaultEmulator.Click += new System.EventHandler(this.toolStripMenuItemChangeDefaultEmulator_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(248, 6);
            // 
            // toolStripMenuItem_ROMParentMenu
            // 
            this.toolStripMenuItem_ROMParentMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRescanAllRoms,
            this.toolStripMenuItemScanNewRomsAllSystems,
            this.toolStripMenuItemScanSelectedSystemNewRoms,
            this.toolStripMenuItem_CleanScanSelectedSystemNewRoms,
            this.toolStripMenuItemDeleteRomsParentMenu,
            this.toolStripMenuItem_CompressRomFilesParentMenu,
            this.toolStripMenuItem_CompressAllRoms});
            this.toolStripMenuItem_ROMParentMenu.Name = "toolStripMenuItem_ROMParentMenu";
            this.toolStripMenuItem_ROMParentMenu.Size = new System.Drawing.Size(251, 22);
            this.toolStripMenuItem_ROMParentMenu.Text = "&ROM\'s";
            // 
            // toolStripMenuItemRescanAllRoms
            // 
            this.toolStripMenuItemRescanAllRoms.Name = "toolStripMenuItemRescanAllRoms";
            this.toolStripMenuItemRescanAllRoms.Size = new System.Drawing.Size(368, 22);
            this.toolStripMenuItemRescanAllRoms.Text = "Rescan All ROM\'s";
            this.toolStripMenuItemRescanAllRoms.Click += new System.EventHandler(this.toolStripMenuItemRescanAllRoms_Click);
            // 
            // toolStripMenuItemScanNewRomsAllSystems
            // 
            this.toolStripMenuItemScanNewRomsAllSystems.Name = "toolStripMenuItemScanNewRomsAllSystems";
            this.toolStripMenuItemScanNewRomsAllSystems.Size = new System.Drawing.Size(368, 22);
            this.toolStripMenuItemScanNewRomsAllSystems.Text = "Scan for new ROM\'s on all systems";
            this.toolStripMenuItemScanNewRomsAllSystems.Click += new System.EventHandler(this.toolStripMenuItemScanNewRomsAllSystems_Click);
            // 
            // toolStripMenuItemScanSelectedSystemNewRoms
            // 
            this.toolStripMenuItemScanSelectedSystemNewRoms.Name = "toolStripMenuItemScanSelectedSystemNewRoms";
            this.toolStripMenuItemScanSelectedSystemNewRoms.Size = new System.Drawing.Size(368, 22);
            this.toolStripMenuItemScanSelectedSystemNewRoms.Text = "Scan selected system for new ROM\'s";
            this.toolStripMenuItemScanSelectedSystemNewRoms.Click += new System.EventHandler(this.toolStripMenuItemScanSelectedSystemNewRoms_Click);
            // 
            // toolStripMenuItem_CleanScanSelectedSystemNewRoms
            // 
            this.toolStripMenuItem_CleanScanSelectedSystemNewRoms.Name = "toolStripMenuItem_CleanScanSelectedSystemNewRoms";
            this.toolStripMenuItem_CleanScanSelectedSystemNewRoms.Size = new System.Drawing.Size(368, 22);
            this.toolStripMenuItem_CleanScanSelectedSystemNewRoms.Text = "Clean and scan selected system for new ROM\'s";
            this.toolStripMenuItem_CleanScanSelectedSystemNewRoms.Click += new System.EventHandler(this.toolStripMenuItem_CleanScanSelectedSystemNewRoms_Click);
            // 
            // toolStripMenuItemDeleteRomsParentMenu
            // 
            this.toolStripMenuItemDeleteRomsParentMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_DeleteDupRomsByTitleSameSystem,
            this.toolStripMenuItem_DeleteDupRomsByTitleAnySystem,
            this.toolStripMenuItem_DeleteDupRomsByChecksum});
            this.toolStripMenuItemDeleteRomsParentMenu.Name = "toolStripMenuItemDeleteRomsParentMenu";
            this.toolStripMenuItemDeleteRomsParentMenu.Size = new System.Drawing.Size(368, 22);
            this.toolStripMenuItemDeleteRomsParentMenu.Text = "Delete Duplicate ROM\'s";
            // 
            // toolStripMenuItem_DeleteDupRomsByTitleSameSystem
            // 
            this.toolStripMenuItem_DeleteDupRomsByTitleSameSystem.Name = "toolStripMenuItem_DeleteDupRomsByTitleSameSystem";
            this.toolStripMenuItem_DeleteDupRomsByTitleSameSystem.Size = new System.Drawing.Size(320, 22);
            this.toolStripMenuItem_DeleteDupRomsByTitleSameSystem.Text = "Delete duplicate ROM\'s by title in same system";
            this.toolStripMenuItem_DeleteDupRomsByTitleSameSystem.Click += new System.EventHandler(this.toolStripMenuItem_DeleteDupRomsByTitleSameSystem_Click);
            // 
            // toolStripMenuItem_DeleteDupRomsByTitleAnySystem
            // 
            this.toolStripMenuItem_DeleteDupRomsByTitleAnySystem.Name = "toolStripMenuItem_DeleteDupRomsByTitleAnySystem";
            this.toolStripMenuItem_DeleteDupRomsByTitleAnySystem.Size = new System.Drawing.Size(320, 22);
            this.toolStripMenuItem_DeleteDupRomsByTitleAnySystem.Text = "Delete duplicate ROM\'s by title in any system";
            this.toolStripMenuItem_DeleteDupRomsByTitleAnySystem.Click += new System.EventHandler(this.toolStripMenuItem_DeleteDupRomsByTitleAnySystem_Click);
            // 
            // toolStripMenuItem_DeleteDupRomsByChecksum
            // 
            this.toolStripMenuItem_DeleteDupRomsByChecksum.Name = "toolStripMenuItem_DeleteDupRomsByChecksum";
            this.toolStripMenuItem_DeleteDupRomsByChecksum.Size = new System.Drawing.Size(320, 22);
            this.toolStripMenuItem_DeleteDupRomsByChecksum.Text = "Delete duplicate ROM\'s by checksum";
            this.toolStripMenuItem_DeleteDupRomsByChecksum.Click += new System.EventHandler(this.toolStripMenuItem_DeleteDupRomsByChecksum_Click);
            // 
            // toolStripMenuItem_CompressRomFilesParentMenu
            // 
            this.toolStripMenuItem_CompressRomFilesParentMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ConvertRomToZip,
            this.toolStripMenuItem_ConvertRomTo7z,
            this.toolStripMenuItem_ConvertRomToTar,
            this.toolStripMenuItem_ConvertRomToGzip,
            this.toolStripMenuItem_ConvertRomToBZ2,
            this.toolStripMenuItem_ConvertRomToLZ,
            this.toolStripMenuItem_ConvertRomToRAR});
            this.toolStripMenuItem_CompressRomFilesParentMenu.Name = "toolStripMenuItem_CompressRomFilesParentMenu";
            this.toolStripMenuItem_CompressRomFilesParentMenu.Size = new System.Drawing.Size(368, 22);
            this.toolStripMenuItem_CompressRomFilesParentMenu.Text = "Compress ROM Files";
            // 
            // toolStripMenuItem_ConvertRomToZip
            // 
            this.toolStripMenuItem_ConvertRomToZip.Name = "toolStripMenuItem_ConvertRomToZip";
            this.toolStripMenuItem_ConvertRomToZip.Size = new System.Drawing.Size(288, 22);
            this.toolStripMenuItem_ConvertRomToZip.Text = "Convert ROM files to compress Zip files";
            this.toolStripMenuItem_ConvertRomToZip.Click += new System.EventHandler(this.toolStripMenuItem_ConvertRomToZip_Click);
            // 
            // toolStripMenuItem_ConvertRomTo7z
            // 
            this.toolStripMenuItem_ConvertRomTo7z.Name = "toolStripMenuItem_ConvertRomTo7z";
            this.toolStripMenuItem_ConvertRomTo7z.Size = new System.Drawing.Size(288, 22);
            this.toolStripMenuItem_ConvertRomTo7z.Text = "Convert ROM files to compress 7z files";
            this.toolStripMenuItem_ConvertRomTo7z.Click += new System.EventHandler(this.toolStripMenuItem_ConvertRomTo7z_Click);
            // 
            // toolStripMenuItem_ConvertRomToTar
            // 
            this.toolStripMenuItem_ConvertRomToTar.Name = "toolStripMenuItem_ConvertRomToTar";
            this.toolStripMenuItem_ConvertRomToTar.Size = new System.Drawing.Size(288, 22);
            this.toolStripMenuItem_ConvertRomToTar.Text = "Convert ROM files to compress Tar files";
            this.toolStripMenuItem_ConvertRomToTar.Click += new System.EventHandler(this.toolStripMenuItem_ConvertRomToTar_Click);
            // 
            // toolStripMenuItem_ConvertRomToGzip
            // 
            this.toolStripMenuItem_ConvertRomToGzip.Name = "toolStripMenuItem_ConvertRomToGzip";
            this.toolStripMenuItem_ConvertRomToGzip.Size = new System.Drawing.Size(288, 22);
            this.toolStripMenuItem_ConvertRomToGzip.Text = "Convert ROM files to compress Gzip files";
            this.toolStripMenuItem_ConvertRomToGzip.Click += new System.EventHandler(this.toolStripMenuItem_ConvertRomToGzip_Click);
            // 
            // toolStripMenuItem_ConvertRomToBZ2
            // 
            this.toolStripMenuItem_ConvertRomToBZ2.Name = "toolStripMenuItem_ConvertRomToBZ2";
            this.toolStripMenuItem_ConvertRomToBZ2.Size = new System.Drawing.Size(288, 22);
            this.toolStripMenuItem_ConvertRomToBZ2.Text = "Convert ROM files to compress Bz2 files";
            this.toolStripMenuItem_ConvertRomToBZ2.Click += new System.EventHandler(this.toolStripMenuItem_ConvertRomToBZ2_Click);
            // 
            // toolStripMenuItem_ConvertRomToLZ
            // 
            this.toolStripMenuItem_ConvertRomToLZ.Name = "toolStripMenuItem_ConvertRomToLZ";
            this.toolStripMenuItem_ConvertRomToLZ.Size = new System.Drawing.Size(288, 22);
            this.toolStripMenuItem_ConvertRomToLZ.Text = "Convert ROM files to compress Lz files";
            this.toolStripMenuItem_ConvertRomToLZ.Click += new System.EventHandler(this.toolStripMenuItem_ConvertRomToLZ_Click);
            // 
            // toolStripMenuItem_ImagesParentMenu
            // 
            this.toolStripMenuItem_ImagesParentMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_CreateImageListCache,
            this.toolStripMenuItem_ImageSearchSelectedDir,
            this.toolStripMenuItem_ScanSelectedSystemNewImages,
            this.toolStripMenuItem_ScanSelectedSystemRomsAndImages,
            this.toolStripMenuItem_ScanAllSystemNewRomsAndImages,
            this.toolStripMenuItem_ScanAllSystemsNewRomsAndImages,
            this.toolStripMenuItem_DeleteDupImagesNotInDB,
            this.toolStripMenuItem_ConvertPngToJpg,
            this.toolStripMenuItem_ConvertAllPngToJpg});
            this.toolStripMenuItem_ImagesParentMenu.Name = "toolStripMenuItem_ImagesParentMenu";
            this.toolStripMenuItem_ImagesParentMenu.Size = new System.Drawing.Size(251, 22);
            this.toolStripMenuItem_ImagesParentMenu.Text = "&Images";
            // 
            // toolStripMenuItem_CreateImageListCache
            // 
            this.toolStripMenuItem_CreateImageListCache.Name = "toolStripMenuItem_CreateImageListCache";
            this.toolStripMenuItem_CreateImageListCache.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_CreateImageListCache.Text = "Create Image-List Cache";
            this.toolStripMenuItem_CreateImageListCache.Click += new System.EventHandler(this.toolStripMenuItem_CreateImageListCache_Click);
            // 
            // toolStripMenuItem_ImageSearchSelectedDir
            // 
            this.toolStripMenuItem_ImageSearchSelectedDir.Name = "toolStripMenuItem_ImageSearchSelectedDir";
            this.toolStripMenuItem_ImageSearchSelectedDir.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_ImageSearchSelectedDir.Text = "Do image search in selected directory";
            this.toolStripMenuItem_ImageSearchSelectedDir.Click += new System.EventHandler(this.toolStripMenuItem_ImageSearchSelectedDir_Click);
            // 
            // toolStripMenuItem_ScanSelectedSystemNewImages
            // 
            this.toolStripMenuItem_ScanSelectedSystemNewImages.Name = "toolStripMenuItem_ScanSelectedSystemNewImages";
            this.toolStripMenuItem_ScanSelectedSystemNewImages.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_ScanSelectedSystemNewImages.Text = "Scan selected system for new images";
            this.toolStripMenuItem_ScanSelectedSystemNewImages.Click += new System.EventHandler(this.toolStripMenuItem_ScanSelectedSystemNewImages_Click);
            // 
            // toolStripMenuItem_ScanSelectedSystemRomsAndImages
            // 
            this.toolStripMenuItem_ScanSelectedSystemRomsAndImages.Name = "toolStripMenuItem_ScanSelectedSystemRomsAndImages";
            this.toolStripMenuItem_ScanSelectedSystemRomsAndImages.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_ScanSelectedSystemRomsAndImages.Text = "Scan selected system for new ROM\'s and images";
            this.toolStripMenuItem_ScanSelectedSystemRomsAndImages.Click += new System.EventHandler(this.toolStripMenuItem_ScanSelectedSystemRomsAndImages_Click);
            // 
            // toolStripMenuItem_ScanAllSystemNewRomsAndImages
            // 
            this.toolStripMenuItem_ScanAllSystemNewRomsAndImages.Name = "toolStripMenuItem_ScanAllSystemNewRomsAndImages";
            this.toolStripMenuItem_ScanAllSystemNewRomsAndImages.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_ScanAllSystemNewRomsAndImages.Text = "Scan for new ROM\'s and images on all systems";
            // 
            // toolStripMenuItem_ScanAllSystemsNewRomsAndImages
            // 
            this.toolStripMenuItem_ScanAllSystemsNewRomsAndImages.Name = "toolStripMenuItem_ScanAllSystemsNewRomsAndImages";
            this.toolStripMenuItem_ScanAllSystemsNewRomsAndImages.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_ScanAllSystemsNewRomsAndImages.Text = "Scan all systems for new ROM\'s and images";
            this.toolStripMenuItem_ScanAllSystemsNewRomsAndImages.Click += new System.EventHandler(this.toolStripMenuItem_ScanAllSystemsNewRomsAndImages_Click);
            // 
            // toolStripMenuItem_DeleteDupImagesNotInDB
            // 
            this.toolStripMenuItem_DeleteDupImagesNotInDB.Name = "toolStripMenuItem_DeleteDupImagesNotInDB";
            this.toolStripMenuItem_DeleteDupImagesNotInDB.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_DeleteDupImagesNotInDB.Text = "Delete duplicate image files not in database";
            this.toolStripMenuItem_DeleteDupImagesNotInDB.Click += new System.EventHandler(this.toolStripMenuItem_DeleteDupImagesNotInDB_Click);
            // 
            // toolStripMenuItem_MiscUtilParentMenu
            // 
            this.toolStripMenuItem_MiscUtilParentMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_RegexRenameFiles,
            this.toolStripMenuItem_DbUtilParentMenu});
            this.toolStripMenuItem_MiscUtilParentMenu.Name = "toolStripMenuItem_MiscUtilParentMenu";
            this.toolStripMenuItem_MiscUtilParentMenu.Size = new System.Drawing.Size(251, 22);
            this.toolStripMenuItem_MiscUtilParentMenu.Text = "&Misc Util";
            // 
            // toolStripMenuItem_RegexRenameFiles
            // 
            this.toolStripMenuItem_RegexRenameFiles.Name = "toolStripMenuItem_RegexRenameFiles";
            this.toolStripMenuItem_RegexRenameFiles.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem_RegexRenameFiles.Text = "Regex Rename Files";
            this.toolStripMenuItem_RegexRenameFiles.Click += new System.EventHandler(this.toolStripMenuItem_RegexRenameFiles_Click);
            // 
            // toolStripMenuItem_DbUtilParentMenu
            // 
            this.toolStripMenuItem_DbUtilParentMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ResetRomTitleCompress,
            this.toolStripMenuItem_ResetImageTitleCompressInDB,
            this.toolStripMenuItem_ResetCompressNames,
            this.toolStripSeparator7,
            this.toolStripMenuItem_PopulateGameDetailsDB,
            this.toolStripMenuItem_AddGameDetailsToGameLauncherDb,
            this.toolStripSeparator6,
            this.toolStripMenuItem_ResetYearAndRatingFieldsInDB,
            this.toolStripMenuItem_SearchMatchingImage});
            this.toolStripMenuItem_DbUtilParentMenu.Name = "toolStripMenuItem_DbUtilParentMenu";
            this.toolStripMenuItem_DbUtilParentMenu.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem_DbUtilParentMenu.Text = "Database Util";
            // 
            // toolStripMenuItem_ResetRomTitleCompress
            // 
            this.toolStripMenuItem_ResetRomTitleCompress.Name = "toolStripMenuItem_ResetRomTitleCompress";
            this.toolStripMenuItem_ResetRomTitleCompress.Size = new System.Drawing.Size(347, 22);
            this.toolStripMenuItem_ResetRomTitleCompress.Text = "Reset ROM title and compress in DB";
            this.toolStripMenuItem_ResetRomTitleCompress.Click += new System.EventHandler(this.toolStripMenuItem_ResetRomTitleCompress_Click);
            // 
            // toolStripMenuItem_ResetImageTitleCompressInDB
            // 
            this.toolStripMenuItem_ResetImageTitleCompressInDB.Name = "toolStripMenuItem_ResetImageTitleCompressInDB";
            this.toolStripMenuItem_ResetImageTitleCompressInDB.Size = new System.Drawing.Size(347, 22);
            this.toolStripMenuItem_ResetImageTitleCompressInDB.Text = "Reset Image title and compress in DB";
            this.toolStripMenuItem_ResetImageTitleCompressInDB.Click += new System.EventHandler(this.toolStripMenuItem_ResetImageTitleCompressInDB_Click);
            // 
            // toolStripMenuItem_ResetCompressNames
            // 
            this.toolStripMenuItem_ResetCompressNames.Name = "toolStripMenuItem_ResetCompressNames";
            this.toolStripMenuItem_ResetCompressNames.Size = new System.Drawing.Size(347, 22);
            this.toolStripMenuItem_ResetCompressNames.Text = "Reset both ROM + Image title and compress in DB";
            this.toolStripMenuItem_ResetCompressNames.ToolTipText = "Reset fields Title, compress, NameSimplified, and NameOrg in the ROM\'s and image " +
    "database table.\r\nAfter resetting fields, run a search for matching images for RO" +
    "M\'s missing images.";
            this.toolStripMenuItem_ResetCompressNames.Click += new System.EventHandler(this.toolStripMenuItem_ResetCompressNames_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(344, 6);
            // 
            // toolStripMenuItem_PopulateGameDetailsDB
            // 
            this.toolStripMenuItem_PopulateGameDetailsDB.Name = "toolStripMenuItem_PopulateGameDetailsDB";
            this.toolStripMenuItem_PopulateGameDetailsDB.Size = new System.Drawing.Size(347, 22);
            this.toolStripMenuItem_PopulateGameDetailsDB.Text = "Populate GameDetails Database";
            this.toolStripMenuItem_PopulateGameDetailsDB.Click += new System.EventHandler(this.toolStripMenuItem_PopulateGameDetailsDB_Click);
            // 
            // toolStripMenuItem_AddGameDetailsToGameLauncherDb
            // 
            this.toolStripMenuItem_AddGameDetailsToGameLauncherDb.Name = "toolStripMenuItem_AddGameDetailsToGameLauncherDb";
            this.toolStripMenuItem_AddGameDetailsToGameLauncherDb.Size = new System.Drawing.Size(347, 22);
            this.toolStripMenuItem_AddGameDetailsToGameLauncherDb.Text = "Add GameDetails data to GameLauncher database";
            this.toolStripMenuItem_AddGameDetailsToGameLauncherDb.Click += new System.EventHandler(this.toolStripMenuItem_AddGameDetailsToGameLauncherDb_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(344, 6);
            // 
            // toolStripMenuItem_ResetYearAndRatingFieldsInDB
            // 
            this.toolStripMenuItem_ResetYearAndRatingFieldsInDB.Name = "toolStripMenuItem_ResetYearAndRatingFieldsInDB";
            this.toolStripMenuItem_ResetYearAndRatingFieldsInDB.Size = new System.Drawing.Size(347, 22);
            this.toolStripMenuItem_ResetYearAndRatingFieldsInDB.Text = "Reset fields Year and rating to non-null values";
            this.toolStripMenuItem_ResetYearAndRatingFieldsInDB.ToolTipText = "This option may be needed if database was initialized using Beta version of this " +
    "program.";
            this.toolStripMenuItem_ResetYearAndRatingFieldsInDB.Click += new System.EventHandler(this.toolStripMenuItem_ResetYearAndRatingFieldsInDB_Click);
            // 
            // toolStripMenuItem_SearchMatchingImage
            // 
            this.toolStripMenuItem_SearchMatchingImage.Name = "toolStripMenuItem_SearchMatchingImage";
            this.toolStripMenuItem_SearchMatchingImage.Size = new System.Drawing.Size(347, 22);
            this.toolStripMenuItem_SearchMatchingImage.Text = "Search for image matches for games missing image";
            this.toolStripMenuItem_SearchMatchingImage.Click += new System.EventHandler(this.toolStripMenuItem_SearchMatchingImage_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(248, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.ToolTipText = "Select changes to default settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem_FilterLabel
            // 
            this.toolStripMenuItem_FilterLabel.AutoToolTip = true;
            this.toolStripMenuItem_FilterLabel.Name = "toolStripMenuItem_FilterLabel";
            this.toolStripMenuItem_FilterLabel.ShowShortcutKeys = false;
            this.toolStripMenuItem_FilterLabel.Size = new System.Drawing.Size(45, 28);
            this.toolStripMenuItem_FilterLabel.Text = "Filter";
            this.toolStripMenuItem_FilterLabel.ToolTipText = resources.GetString("toolStripMenuItem_FilterLabel.ToolTipText");
            this.toolStripMenuItem_FilterLabel.Click += new System.EventHandler(this.toolStripTextBox_Filter_Click);
            // 
            // toolStripTextBox_Filter
            // 
            this.toolStripTextBox_Filter.AcceptsReturn = true;
            this.toolStripTextBox_Filter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripTextBox_Filter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.toolStripTextBox_Filter.AutoToolTip = true;
            this.toolStripTextBox_Filter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox_Filter.Name = "toolStripTextBox_Filter";
            this.toolStripTextBox_Filter.Size = new System.Drawing.Size(100, 28);
            this.toolStripTextBox_Filter.ToolTipText = resources.GetString("toolStripTextBox_Filter.ToolTipText");
            this.toolStripTextBox_Filter.Click += new System.EventHandler(this.toolStripTextBox_Filter_Click);
            this.toolStripTextBox_Filter.DoubleClick += new System.EventHandler(this.toolStripTextBox_Filter_DbClick);
            this.toolStripTextBox_Filter.TextChanged += new System.EventHandler(this.toolStripTextBox_Filter_Change);
            // 
            // toolStripMenuItemSearchAll
            // 
            this.toolStripMenuItemSearchAll.AutoToolTip = true;
            this.toolStripMenuItemSearchAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemSearchAll.Image")));
            this.toolStripMenuItemSearchAll.Name = "toolStripMenuItemSearchAll";
            this.toolStripMenuItemSearchAll.Size = new System.Drawing.Size(36, 28);
            this.toolStripMenuItemSearchAll.ToolTipText = "Search all game console system.\r\nDisplay search results resulting less than 1000 " +
    "games.\r\nSupports SQL wild characters like % and _.\r\nNo REGEX support!\r\n";
            this.toolStripMenuItemSearchAll.Click += new System.EventHandler(this.toolStripMenuItemSearchAll_Click);
            // 
            // numericUpDown_Paginator
            // 
            this.numericUpDown_Paginator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown_Paginator.Enabled = false;
            this.numericUpDown_Paginator.Location = new System.Drawing.Point(511, 4);
            this.numericUpDown_Paginator.Name = "numericUpDown_Paginator";
            this.numericUpDown_Paginator.Size = new System.Drawing.Size(61, 20);
            this.numericUpDown_Paginator.TabIndex = 92;
            this.numericUpDown_Paginator.ValueChanged += new System.EventHandler(this.numericUpDown_Paginator_Changed);
            // 
            // toolStripMenuItem_ConvertRomToRAR
            // 
            this.toolStripMenuItem_ConvertRomToRAR.Name = "toolStripMenuItem_ConvertRomToRAR";
            this.toolStripMenuItem_ConvertRomToRAR.Size = new System.Drawing.Size(288, 22);
            this.toolStripMenuItem_ConvertRomToRAR.Text = "Convert ROM files to compress RAR files";
            this.toolStripMenuItem_ConvertRomToRAR.ToolTipText = "This option is only available if WinRar is installed in the following path:\r\nC:\\P" +
    "rogram Files\\WinRAR\\Rar.exe";
            this.toolStripMenuItem_ConvertRomToRAR.Click += new System.EventHandler(this.toolStripMenuItem_ConvertRomToRAR_Click);
            // 
            // toolStripMenuItem_ConvertPngToJpg
            // 
            this.toolStripMenuItem_ConvertPngToJpg.Name = "toolStripMenuItem_ConvertPngToJpg";
            this.toolStripMenuItem_ConvertPngToJpg.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_ConvertPngToJpg.Text = "Convert PNG files to JPG";
            this.toolStripMenuItem_ConvertPngToJpg.Click += new System.EventHandler(this.toolStripMenuItem_ConvertPngToJpg_Click);
            // 
            // toolStripMenuItem_ConvertAllPngToJpg
            // 
            this.toolStripMenuItem_ConvertAllPngToJpg.Name = "toolStripMenuItem_ConvertAllPngToJpg";
            this.toolStripMenuItem_ConvertAllPngToJpg.Size = new System.Drawing.Size(330, 22);
            this.toolStripMenuItem_ConvertAllPngToJpg.Text = "Convert all PNG files to JPG in all game systems";
            this.toolStripMenuItem_ConvertAllPngToJpg.ToolTipText = "Convert all PNG files to JPG in all game console systems.\r\nUpdate GameLauncher da" +
    "tabase.\r\nDelete orginal PNG files after conversion.";
            this.toolStripMenuItem_ConvertAllPngToJpg.Click += new System.EventHandler(this.toolStripMenuItem_ConvertAllPngToJpg_Click);
            // 
            // toolStripMenuItem_CompressAllRoms
            // 
            this.toolStripMenuItem_CompressAllRoms.Name = "toolStripMenuItem_CompressAllRoms";
            this.toolStripMenuItem_CompressAllRoms.Size = new System.Drawing.Size(368, 22);
            this.toolStripMenuItem_CompressAllRoms.Text = "Compress all ROM Files in all game systems ROM folder";
            this.toolStripMenuItem_CompressAllRoms.Click += new System.EventHandler(this.toolStripMenuItem_CompressAllRoms_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.numericUpDown_Paginator);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox_ProgressBar);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.myListView);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(598, 394);
            this.Name = "Form_Main";
            this.Text = "Game Launcher [Ver-1.0] by David Maisonave Sr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.myListView_OnFormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox_ProgressBar.ResumeLayout(false);
            this.groupBox_ProgressBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Paginator)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeDefaultEmulator;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_Filter;
        private System.Windows.Forms.ToolStripMenuItem searchImageAtLaunchBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_FilterLabel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSearchAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemChangeViewROMDetails;
        private System.Windows.Forms.ToolStripMenuItem changeViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ROMParentMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRescanAllRoms;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScanNewRomsAllSystems;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScanSelectedSystemNewRoms;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteRomsParentMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DeleteDupRomsByChecksum;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DeleteDupRomsByTitleSameSystem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DeleteDupRomsByTitleAnySystem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ImagesParentMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CreateImageListCache;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ScanSelectedSystemRomsAndImages;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DeleteDupImagesNotInDB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_MiscUtilParentMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_RegexRenameFiles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CleanScanSelectedSystemNewRoms;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ImageSearchSelectedDir;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ScanSelectedSystemNewImages;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ScanAllSystemNewRomsAndImages;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ScanAllSystemsNewRomsAndImages;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DbUtilParentMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ResetCompressNames;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_PopulateGameDetailsDB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_AddGameDetailsToGameLauncherDb;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ResetRomTitleCompress;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_SearchMatchingImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ResetYearAndRatingFieldsInDB;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ResetImageTitleCompressInDB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_RecentGames;
        private System.Windows.Forms.NumericUpDown numericUpDown_Paginator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CopyTitleToClipboard;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CompressRomFilesParentMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertRomToZip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertRomTo7z;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertRomToTar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertRomToGzip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertRomToBZ2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertRomToLZ;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertRomToRAR;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertPngToJpg;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ConvertAllPngToJpg;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_CompressAllRoms;
    }
}

