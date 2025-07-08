using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLauncher
{
    public partial class Form_RegexRenameFiles : Form
    {
        public Form_RegexRenameFiles()
        {
            InitializeComponent();
            textBox_Dir.Text = Properties.Settings.Default.RegexRenameFilesDir;
            textBox_Pattern.Text = Properties.Settings.Default.RegexRenameFilesPattern;
            textBox_ReplaceStr.Text = Properties.Settings.Default.RegexRenameFilesReplace;
        }

        private void button_dir_Click(object sender, EventArgs e)
        {
            SelectFolderDialog folderBrowserDialog1 = new SelectFolderDialog();
            folderBrowserDialog1.Description = "Select directory to perform search and replace.";
            folderBrowserDialog1.SelectedPath = textBox_Dir.Text;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
                textBox_Dir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            this.Hide();
            Properties.Settings.Default.RegexRenameFilesDir = textBox_Dir.Text;
            Properties.Settings.Default.RegexRenameFilesPattern = textBox_Pattern.Text;
            Properties.Settings.Default.RegexRenameFilesReplace = textBox_ReplaceStr.Text;
            Form_Main.RegexRenameFiles(textBox_Dir.Text, textBox_Pattern.Text, textBox_ReplaceStr.Text);
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
