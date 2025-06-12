using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncher
{
    public partial class FormGameSystemEditor : Form
    {
        public GameSystem gameSystem { get; private set; }
        public bool Ok { get; private set; } = false;
        public FormGameSystemEditor(GameSystem gameSystem)
        {
            this.gameSystem = gameSystem;
            InitializeComponent();
            textBox_EmulatorPath1.Text  =  gameSystem.EmulatorPaths[0];
            textBox_EmulatorPath2.Text  =  gameSystem.EmulatorPaths[1];
            textBox_EmulatorPath3.Text  =  gameSystem.EmulatorPaths[2];
            textBox_EmulatorPath4.Text  =  gameSystem.EmulatorPaths[3];
            textBox_EmulatorPath5.Text  =  gameSystem.EmulatorPaths[4];
            textBox_EmulatorPath6.Text  =  gameSystem.EmulatorPaths[5];
            textBox_EmulatorPath7.Text  =  gameSystem.EmulatorPaths[6];
            textBox_EmulatorPath8.Text  =  gameSystem.EmulatorPaths[7];
            textBox_EmulatorPath9.Text  =  gameSystem.EmulatorPaths[8];
            textBox_EmulatorPath10.Text =  gameSystem.EmulatorPaths[9];
            checkBox_ExtractArchive1.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[0]);
            checkBox_ExtractArchive2.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[1]);
            checkBox_ExtractArchive3.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[2]);
            checkBox_ExtractArchive4.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[3]);
            checkBox_ExtractArchive5.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[4]);
            checkBox_ExtractArchive6.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[5]);
            checkBox_ExtractArchive7.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[6]);
            checkBox_ExtractArchive8.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[7]);
            checkBox_ExtractArchive9.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[8]);
            checkBox_ExtractArchive10.Checked = Form_Main.EmulatorRequiresDecompression(gameSystem.EmulatorPaths[9]);
            textBox_ImageDirPath.Text = gameSystem.ImageDirPath;
            textBox_RomDirPath.Text = gameSystem.RomDirPath;
            textBox_System.Text = gameSystem.ID.ToString();
            textBox_Name.Text = gameSystem.Name;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            gameSystem.EmulatorPaths[0] =  textBox_EmulatorPath1.Text;
            gameSystem.EmulatorPaths[1] =  textBox_EmulatorPath2.Text;
            gameSystem.EmulatorPaths[2] =  textBox_EmulatorPath3.Text;
            gameSystem.EmulatorPaths[3] =  textBox_EmulatorPath4.Text;
            gameSystem.EmulatorPaths[4] =  textBox_EmulatorPath5.Text;
            gameSystem.EmulatorPaths[5] =  textBox_EmulatorPath6.Text;
            gameSystem.EmulatorPaths[6] =  textBox_EmulatorPath7.Text;
            gameSystem.EmulatorPaths[7] =  textBox_EmulatorPath8.Text;
            gameSystem.EmulatorPaths[8] =  textBox_EmulatorPath9.Text;
            gameSystem.EmulatorPaths[9] =  textBox_EmulatorPath10.Text;
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[0], checkBox_ExtractArchive1.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[1], checkBox_ExtractArchive2.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[2], checkBox_ExtractArchive3.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[3], checkBox_ExtractArchive4.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[4], checkBox_ExtractArchive5.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[5], checkBox_ExtractArchive6.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[6], checkBox_ExtractArchive7.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[7], checkBox_ExtractArchive8.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[8], checkBox_ExtractArchive9.Checked);
            Form_Main.UpdateEmulatorAttributes_DecompressFile(gameSystem.EmulatorPaths[9], checkBox_ExtractArchive10.Checked);
            gameSystem.ImageDirPath = textBox_ImageDirPath.Text;
            gameSystem.RomDirPath = textBox_RomDirPath.Text;
            gameSystem.Name = textBox_Name.Text;
            Ok = true;
            this.Close();
        }
        private void EmulatorPathPick(int id)
        {
            int preferredEmulator = id;
            string emulatorExecutable = gameSystem.EmulatorPaths[id];
            if (emulatorExecutable.Length == 0 && gameSystem.EmulatorPaths[0].Length > 0)
                emulatorExecutable = gameSystem.EmulatorPaths[0];
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"Emulator Executable File (*.exe)|*.exe|All Files (*.*)|*.*";
            saveFileDialog.Title = "Select preferred emulator executable";
            saveFileDialog.FileName = emulatorExecutable;
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(emulatorExecutable);
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = false;
            DialogResult results = saveFileDialog.ShowDialog();
            if (results == DialogResult.Cancel)
                return;
            if (saveFileDialog.FileName.Length == 0 || !File.Exists(saveFileDialog.FileName))
            {
                MessageBox.Show($"Error: Entered invalid file name for emulator", "Invalid Name!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (id)
            {
                case 0:
                    textBox_EmulatorPath1.Text = saveFileDialog.FileName;
                    break;
                case 1:
                    textBox_EmulatorPath2.Text = saveFileDialog.FileName;
                    break;
                case 2:
                    textBox_EmulatorPath3.Text = saveFileDialog.FileName;
                    break;
                case 3:
                    textBox_EmulatorPath4.Text = saveFileDialog.FileName;
                    break;
                case 4:
                    textBox_EmulatorPath5.Text = saveFileDialog.FileName;
                    break;
                case 5:
                    textBox_EmulatorPath6.Text = saveFileDialog.FileName;
                    break;
                case 6:
                    textBox_EmulatorPath7.Text = saveFileDialog.FileName;
                    break;
                case 7:
                    textBox_EmulatorPath8.Text = saveFileDialog.FileName;
                    break;
                case 8:
                    textBox_EmulatorPath9.Text = saveFileDialog.FileName;
                    break;
                case 9:
                    textBox_EmulatorPath10.Text = saveFileDialog.FileName;
                    break;
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e) => this.Close();

        private void button_EmulatorPathPick1_Click(object sender, EventArgs e) => EmulatorPathPick(0);
        private void button_EmulatorPathPick2_Click(object sender, EventArgs e) => EmulatorPathPick(1);
        private void button_EmulatorPathPick3_Click(object sender, EventArgs e) => EmulatorPathPick(2);
        private void button_EmulatorPathPick4_Click(object sender, EventArgs e) => EmulatorPathPick(3);
        private void button_EmulatorPathPick5_Click(object sender, EventArgs e) => EmulatorPathPick(4);
        private void button_EmulatorPathPick6_Click(object sender, EventArgs e) => EmulatorPathPick(5);
        private void button_EmulatorPathPick7_Click(object sender, EventArgs e) => EmulatorPathPick(6);
        private void button_EmulatorPathPick8_Click(object sender, EventArgs e) => EmulatorPathPick(7);
        private void button_EmulatorPathPick9_Click(object sender, EventArgs e) => EmulatorPathPick(8);
        private void button_EmulatorPathPick10_Click(object sender, EventArgs e) => EmulatorPathPick(9);
    }
}
