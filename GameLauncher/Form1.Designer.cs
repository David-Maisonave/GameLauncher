﻿namespace GameLauncher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.myListView = new System.Windows.Forms.ListView();
            this.comboBoxSystem = new System.Windows.Forms.ComboBox();
            this.comboBoxIconDisplay = new System.Windows.Forms.ComboBox();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPlayRom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRenameROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeleteROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAssignPreferredEmulator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeAssignedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemChangeTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip_Info = new System.Windows.Forms.ToolTip(this.components);
            this.button_Rescan = new System.Windows.Forms.Button();
            this.button_Settings = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
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
            // comboBoxSystem
            // 
            this.comboBoxSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSystem.FormattingEnabled = true;
            this.comboBoxSystem.Location = new System.Drawing.Point(12, 3);
            this.comboBoxSystem.Name = "comboBoxSystem";
            this.comboBoxSystem.Size = new System.Drawing.Size(248, 21);
            this.comboBoxSystem.TabIndex = 1;
            this.toolTip_Info.SetToolTip(this.comboBoxSystem, "Select game console system");
            this.comboBoxSystem.SelectedIndexChanged += new System.EventHandler(this.comboBoxSystem_SelectedIndexChanged);
            // 
            // comboBoxIconDisplay
            // 
            this.comboBoxIconDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIconDisplay.FormattingEnabled = true;
            this.comboBoxIconDisplay.Location = new System.Drawing.Point(264, 3);
            this.comboBoxIconDisplay.Name = "comboBoxIconDisplay";
            this.comboBoxIconDisplay.Size = new System.Drawing.Size(88, 21);
            this.comboBoxIconDisplay.TabIndex = 2;
            this.toolTip_Info.SetToolTip(this.comboBoxIconDisplay, "Select list display format/type");
            this.comboBoxIconDisplay.SelectedIndexChanged += new System.EventHandler(this.comboBoxIconDisplay_SelectedIndexChanged);
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStatus.Location = new System.Drawing.Point(12, 346);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(560, 19);
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
            this.toolStripMenuItemChangeTitle});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(212, 136);
            // 
            // toolStripMenuItemPlayRom
            // 
            this.toolStripMenuItemPlayRom.Name = "toolStripMenuItemPlayRom";
            this.toolStripMenuItemPlayRom.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItemPlayRom.Text = "Play ROM (Game)";
            this.toolStripMenuItemPlayRom.Click += new System.EventHandler(this.myListViewContextMenu_Play_Click);
            // 
            // toolStripMenuItemRenameROM
            // 
            this.toolStripMenuItemRenameROM.AutoToolTip = true;
            this.toolStripMenuItemRenameROM.Name = "toolStripMenuItemRenameROM";
            this.toolStripMenuItemRenameROM.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItemRenameROM.Text = "Rename ROM (Game)";
            this.toolStripMenuItemRenameROM.ToolTipText = "Rename the ROM file on the file system.";
            this.toolStripMenuItemRenameROM.Click += new System.EventHandler(this.myListViewContextMenu_RenameROM_Click);
            // 
            // toolStripMenuItemDeleteROM
            // 
            this.toolStripMenuItemDeleteROM.AutoToolTip = true;
            this.toolStripMenuItemDeleteROM.Name = "toolStripMenuItemDeleteROM";
            this.toolStripMenuItemDeleteROM.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItemDeleteROM.Text = "Delete ROM (Game)";
            this.toolStripMenuItemDeleteROM.ToolTipText = resources.GetString("toolStripMenuItemDeleteROM.ToolTipText");
            this.toolStripMenuItemDeleteROM.Click += new System.EventHandler(this.myListViewContextMenu_DeleteROM_Click);
            // 
            // toolStripMenuItemAssignPreferredEmulator
            // 
            this.toolStripMenuItemAssignPreferredEmulator.AutoToolTip = true;
            this.toolStripMenuItemAssignPreferredEmulator.Name = "toolStripMenuItemAssignPreferredEmulator";
            this.toolStripMenuItemAssignPreferredEmulator.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItemAssignPreferredEmulator.Text = "Assign Preferred Emulator";
            this.toolStripMenuItemAssignPreferredEmulator.ToolTipText = "Assign preferred emulator for the selected ROM.";
            this.toolStripMenuItemAssignPreferredEmulator.Click += new System.EventHandler(this.myListViewContextMenu_AssignPreferredEmulator_Click);
            // 
            // toolStripMenuItemChangeAssignedImage
            // 
            this.toolStripMenuItemChangeAssignedImage.AutoToolTip = true;
            this.toolStripMenuItemChangeAssignedImage.Name = "toolStripMenuItemChangeAssignedImage";
            this.toolStripMenuItemChangeAssignedImage.Size = new System.Drawing.Size(211, 22);
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
            this.toolStripMenuItemChangeTitle.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItemChangeTitle.Text = "Change ROM Title";
            this.toolStripMenuItemChangeTitle.ToolTipText = resources.GetString("toolStripMenuItemChangeTitle.ToolTipText");
            this.toolStripMenuItemChangeTitle.Click += new System.EventHandler(this.myListViewContextMenu_ChangeTitle_Click);
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
            // button_Rescan
            // 
            this.button_Rescan.Location = new System.Drawing.Point(357, 1);
            this.button_Rescan.Margin = new System.Windows.Forms.Padding(2);
            this.button_Rescan.Name = "button_Rescan";
            this.button_Rescan.Size = new System.Drawing.Size(100, 28);
            this.button_Rescan.TabIndex = 8;
            this.button_Rescan.TabStop = false;
            this.button_Rescan.Text = "&Rescan ROM";
            this.toolTip_Info.SetToolTip(this.button_Rescan, "Rescan ROM files for selected game console system.");
            this.button_Rescan.UseVisualStyleBackColor = true;
            this.button_Rescan.Click += new System.EventHandler(this.myListView_button_Rescan_Click);
            // 
            // button_Settings
            // 
            this.button_Settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Settings.Location = new System.Drawing.Point(472, 1);
            this.button_Settings.Margin = new System.Windows.Forms.Padding(2);
            this.button_Settings.Name = "button_Settings";
            this.button_Settings.Size = new System.Drawing.Size(100, 28);
            this.button_Settings.TabIndex = 9;
            this.button_Settings.TabStop = false;
            this.button_Settings.Text = "&Settings";
            this.toolTip_Info.SetToolTip(this.button_Settings, "Select changes to default settings");
            this.button_Settings.UseVisualStyleBackColor = true;
            this.button_Settings.Click += new System.EventHandler(this.button_Settings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.button_Settings);
            this.Controls.Add(this.button_Rescan);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.comboBoxIconDisplay);
            this.Controls.Add(this.comboBoxSystem);
            this.Controls.Add(this.myListView);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.Text = "Game Launcher [Ver-0.9] by David Maisonave Sr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.myListView_OnFormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView myListView;
        private System.Windows.Forms.ComboBox comboBoxSystem;
        private System.Windows.Forms.ComboBox comboBoxIconDisplay;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAssignPreferredEmulator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRenameROM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteROM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeAssignedImage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPlayRom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemChangeTitle;
        private System.Windows.Forms.ToolTip toolTip_Info;
        private System.Windows.Forms.Button button_Rescan;
        private System.Windows.Forms.Button button_Settings;
    }
}

