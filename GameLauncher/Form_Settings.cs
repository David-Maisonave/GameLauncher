﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameLauncher
{
    public partial class Form_Settings : Form
    {
        public bool IconSizeChanged  { get; private set; }
        public bool EmulatorsBasePathChanged { get; private set; }

        private int InitialLargeIconSize = 0;
        private int InitialSmallIconSize = 0;
        private decimal PreviousLargeIconSize = 0;
        private decimal PreviousSmallIconSize = 0;
        private string InitialEmulatorsBasePath = null;
        public Form_Settings()
        {
            InitializeComponent();
            IconSizeChanged = false;
            EmulatorsBasePathChanged = false;
            numericUpDown_LoggingLevel.Value = Properties.Settings.Default.LoggingLevel;
            numericUpDown_MaxNumberOfPairThreadsPerList.Value = Properties.Settings.Default.MaxNumberOfPairThreadsPerList;
            numericUpDown_largeIconSize.Value = Properties.Settings.Default.largeIconSize;
            numericUpDown_smallIconSize.Value = Properties.Settings.Default.smallIconSize;
            textBox_DbPath.Text = Properties.Settings.Default.DbPath;
            textBox_EmulatorsBasePath.Text = Properties.Settings.Default.EmulatorsBasePath;
            textBox_DefaultImagePath.Text = Properties.Settings.Default.DefaultImagePath;
            textBox_ValidRomFileExtensions.Text = Properties.Settings.Default.ValidROMTypes;
            checkBox_usePreviousCollectionCache.Checked = Properties.Settings.Default.usePreviousCollectionCache;
            checkBox_useJoystickController.Checked = Properties.Settings.Default.useJoystickController;
            checkBox_disableAdvanceOptions.Checked = Properties.Settings.Default.disableAdvanceOptions;
            checkBox_AutoCompleteCustomSourceLiveUpdate.Checked = Properties.Settings.Default.AutoCompleteCustomSourceLiveUpdate;

            checkBox_EnableRomChecksum.Checked = Properties.Settings.Default.DoRomChecksum;
            checkBox_EnableImageChecksum.Checked = Properties.Settings.Default.DoImageChecksum;
            checkBox_EnableZipChecksum.Checked = Properties.Settings.Default.DoZipChecksum;
            checkBox_SHA256OverMD5.Checked = Properties.Settings.Default.SHA256OverMD5;
            checkBox_ConvertImageFilesToJpg.Checked = Properties.Settings.Default.AutoConvertImageFilesToJpg;
            checkBox_DeleteOldImageFile.Checked = Properties.Settings.Default.AutoDeleteOldImageFileAfterConversion;
            checkBox_WatchImageFolderForChanges.Checked = Properties.Settings.Default.WatchImageFolderForChanges;
            checkBox_OnlyImportConvertedImageFiles.Checked = Properties.Settings.Default.OnlyImportConvertedImageFiles;

            InitialLargeIconSize = Properties.Settings.Default.largeIconSize;
            InitialSmallIconSize = Properties.Settings.Default.smallIconSize;
            PreviousLargeIconSize = Properties.Settings.Default.largeIconSize;
            PreviousSmallIconSize = Properties.Settings.Default.smallIconSize;
            InitialEmulatorsBasePath = Properties.Settings.Default.EmulatorsBasePath;
            numericUpDown_MaxMRU.Value = Properties.Settings.Default.MaxMRU;
            textBox_DefaultRomSubDir.Text = Properties.Settings.Default.romSubFolderName;
            textBox_DefaultImageSubDir.Text = Properties.Settings.Default.imageSubFolderName;
            checkBox_PreviewOverBoxArt.Checked = Properties.Settings.Default.PreviewOverBoxArt;

            checkBox_EnableZipChecksum.Enabled = checkBox_EnableRomChecksum.Checked;
            checkBox_SHA256OverMD5.Enabled = checkBox_EnableRomChecksum.Checked || checkBox_EnableImageChecksum.Checked;
            checkBox_DeleteOldImageFile.Enabled = checkBox_ConvertImageFilesToJpg.Checked;
        }
        int[] AllowedLargeIconSize = {64, 128, 256};
        int[] AllowedSmallIconSize = {8, 16, 24, 32, 64 };
        private void button_Ok_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox_EmulatorsBasePath.Text))
            {
                MessageBox.Show($"Directory \"{textBox_EmulatorsBasePath.Text}\" does not exists.\nPlease select an existing directory for emulator directory before closing this window.", "Invalid Emulator Directory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!AllowedLargeIconSize.Contains((int)numericUpDown_largeIconSize.Value))
            {
                if (numericUpDown_largeIconSize.Value > 512)
                    numericUpDown_largeIconSize.Value = 1024;
                else if (numericUpDown_largeIconSize.Value > 256)
                    numericUpDown_largeIconSize.Value = 512;
                else if (numericUpDown_largeIconSize.Value > 128)
                    numericUpDown_largeIconSize.Value = 256;
            }
            if (!AllowedSmallIconSize.Contains((int)numericUpDown_smallIconSize.Value))
                numericUpDown_largeIconSize.Value = numericUpDown_smallIconSize.Value > 32 ? 64 : 32;
            Properties.Settings.Default.PreviewOverBoxArt = checkBox_PreviewOverBoxArt.Checked;
            Properties.Settings.Default.DoImageChecksum = checkBox_EnableImageChecksum.Checked;
            Properties.Settings.Default.DoRomChecksum = checkBox_EnableRomChecksum.Checked;
            Properties.Settings.Default.DoZipChecksum = checkBox_EnableZipChecksum.Checked;
            Properties.Settings.Default.SHA256OverMD5 = checkBox_SHA256OverMD5.Checked;
            Properties.Settings.Default.AutoConvertImageFilesToJpg = checkBox_ConvertImageFilesToJpg.Checked;
            Properties.Settings.Default.AutoDeleteOldImageFileAfterConversion = checkBox_DeleteOldImageFile.Checked;
            Properties.Settings.Default.disableAdvanceOptions = checkBox_disableAdvanceOptions.Checked;
            Properties.Settings.Default.AutoCompleteCustomSourceLiveUpdate = checkBox_AutoCompleteCustomSourceLiveUpdate.Checked;
            Properties.Settings.Default.useJoystickController = checkBox_useJoystickController.Checked;
            Properties.Settings.Default.usePreviousCollectionCache = checkBox_usePreviousCollectionCache.Checked;
            Properties.Settings.Default.WatchImageFolderForChanges = checkBox_WatchImageFolderForChanges.Checked;
            Properties.Settings.Default.OnlyImportConvertedImageFiles = checkBox_OnlyImportConvertedImageFiles.Checked;
            Properties.Settings.Default.DbPath = textBox_DbPath.Text;
            Properties.Settings.Default.EmulatorsBasePath = textBox_EmulatorsBasePath.Text;
            Properties.Settings.Default.DefaultImagePath = textBox_DefaultImagePath.Text;
            Properties.Settings.Default.ValidROMTypes = textBox_ValidRomFileExtensions.Text;
            Properties.Settings.Default.LoggingLevel = (int)numericUpDown_LoggingLevel.Value;
            Properties.Settings.Default.MaxNumberOfPairThreadsPerList = (int)numericUpDown_MaxNumberOfPairThreadsPerList.Value;
            Properties.Settings.Default.largeIconSize = (int)numericUpDown_largeIconSize.Value;
            Properties.Settings.Default.smallIconSize = (int)numericUpDown_smallIconSize.Value;
            Properties.Settings.Default.MaxMRU = (int)numericUpDown_MaxMRU.Value;
            Properties.Settings.Default.romSubFolderName = textBox_DefaultRomSubDir.Text;
            Properties.Settings.Default.imageSubFolderName = textBox_DefaultImageSubDir.Text;
            Properties.Settings.Default.Save();

            if (InitialLargeIconSize != Properties.Settings.Default.largeIconSize || InitialSmallIconSize != Properties.Settings.Default.smallIconSize)
                IconSizeChanged = true;
            if (!InitialEmulatorsBasePath.Equals(Properties.Settings.Default.EmulatorsBasePath))
                EmulatorsBasePathChanged = true;
            this.Close();
        }
        private void button_Cancel_Click(object sender, EventArgs e) => this.Close();
        private void numericUpDown_smallIconSize_ValueChanged(object sender, EventArgs e)
        {
            bool inc = PreviousSmallIconSize < numericUpDown_smallIconSize.Value;
            if (!AllowedSmallIconSize.Contains((int)numericUpDown_smallIconSize.Value))
                numericUpDown_smallIconSize.Value = numericUpDown_smallIconSize.Value > 32 ? (inc ? 64 : 32) : 32;
            PreviousSmallIconSize = numericUpDown_smallIconSize.Value;
        }
        private void numericUpDown_largeIconSize_ValueChanged(object sender, EventArgs e)
        {
            bool inc = PreviousLargeIconSize < numericUpDown_largeIconSize.Value;
            if (!AllowedLargeIconSize.Contains((int)numericUpDown_largeIconSize.Value))
            {
                if (numericUpDown_largeIconSize.Value > 128)
                    numericUpDown_largeIconSize.Value = inc ? 256 : 128;
            }
            PreviousLargeIconSize = numericUpDown_largeIconSize.Value;
        }

        private void button_DbPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.db,*.database)|*.db;*.database|All Files (*.*)|*.*";
            saveFileDialog.Title = "Select database file";
            saveFileDialog.FileName = textBox_DbPath.Text;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(textBox_DbPath.Text);
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.OverwritePrompt = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "")
            {
                MessageBox.Show("Error: Entered invalid file name.", "Invalid Name!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saveFileDialog.FileName.ToLower().Equals(textBox_DbPath.Text.ToLower()))
            {
                MessageBox.Show($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox_DbPath.Text = saveFileDialog.FileName;
        }
        private void SetEmulatorsBasePath(bool addToExistingPath)
        {
            SelectFolderDialog folderBrowserDialog1 = new SelectFolderDialog();
            folderBrowserDialog1.Description = "Select root (base) directory containing emulators and associated ROM files.";
            folderBrowserDialog1.SelectedPath = textBox_EmulatorsBasePath.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_EmulatorsBasePath.Text = addToExistingPath ? $"{textBox_EmulatorsBasePath.Text};{folderBrowserDialog1.SelectedPath}" : folderBrowserDialog1.SelectedPath;
        }
        private void button_EmulatorsBasePath_Click(object sender, EventArgs e) => SetEmulatorsBasePath(false);
        private void button_DefaultImagePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.png,*.bmp)|*.png;*.bmp|All Files (*.*)|*.*";
            saveFileDialog.Title = "Select default image";
            saveFileDialog.FileName = textBox_DefaultImagePath.Text;
            saveFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(textBox_DefaultImagePath.Text);
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName == "")
            {
                MessageBox.Show("Error: Entered invalid file name.", "Invalid Name!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saveFileDialog.FileName.ToLower().Equals(textBox_DefaultImagePath.Text.ToLower()))
            {
                MessageBox.Show($"Nothing to do, because the new file name is the same as the old file name:\n{saveFileDialog.FileName}", "Same Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox_DefaultImagePath.Text = saveFileDialog.FileName;
        }
        private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel_Github_GameLauncher.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/David-Maisonave/GameLauncher");
        }
        private void checkBox_EnableRomChecksum_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_EnableZipChecksum.Enabled = checkBox_EnableRomChecksum.Checked;
            checkBox_SHA256OverMD5.Enabled = checkBox_EnableRomChecksum.Checked || checkBox_EnableImageChecksum.Checked;
        }
        private void checkBox_EnableImageChecksum_CheckedChanged(object sender, EventArgs e) => checkBox_SHA256OverMD5.Enabled = checkBox_EnableRomChecksum.Checked || checkBox_EnableImageChecksum.Checked;
        private void button_EmulatorsBasePath_Add_Click(object sender, EventArgs e) => SetEmulatorsBasePath(true);
        private void checkBox_ConvertImageFilesToJpg_CheckedChanged(object sender, EventArgs e) => checkBox_DeleteOldImageFile.Enabled = checkBox_OnlyImportConvertedImageFiles.Enabled = checkBox_ConvertImageFilesToJpg.Checked;
    }
}
