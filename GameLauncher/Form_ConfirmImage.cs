using System;
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
    public partial class Form_ConfirmImage : Form
    {
        public bool Ok { get; private set; } = false;
        public bool Quit { get; private set; } = false;
        private Rom rom = null;
        private string system;
        private Form_Main form_Main = null;
        public Form_ConfirmImage(Form_Main form_Main, string imageFileName, Rom rom, string instructions = "Select Image")
        {
            this.form_Main = form_Main;
            InitializeComponent();
            this.rom = rom;
            label_Instructions.Text = instructions;
            textBox_ImageFileName.Text = imageFileName;
            textBox_RomTitle.Text = rom.Title;
            textBox_RomFile.Text = rom.FilePath;
            try
            {
                pictureBox_Preview.Image = Image.FromFile(imageFileName);
            }
            catch
            {
                Form_Main.ErrorMessage($"Can NOT display image for file {imageFileName}!");
            }
            system = Form_Main.GetSystemNameByID(rom.System);
        }
        private void button_Ok_Click(object sender, EventArgs e)
        {
            Ok = true;
            this.Close();
        }
        private void button_Quit_Click(object sender, EventArgs e)
        {
            Quit = true;
            this.Close();
        }
        private void JumpToBrowser(string link)
        {
            Clipboard.SetText(rom.Title);
            System.Diagnostics.Process.Start(link);
        }
        private void button_Cancel_Click(object sender, EventArgs e) => this.Close();
        private void button_Play_Click(object sender, EventArgs e) => form_Main.PlaySelectedRom(rom);
        private void linkLabel_GoogleTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => JumpToBrowser($"https://www.google.com/search?q={rom.Title}");
        private void linkLabel_GoogleTitleAndSystem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => JumpToBrowser($"https://www.google.com/search?q={rom.Title}+{system}");
        private void linkLabel_GoogleTitleOnLaunchBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => JumpToBrowser($"https://www.google.com/search?q={rom.Title}+site:launchbox-app.com");
        private void linkLabel_GoogleTitleImageOnLaunchBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => JumpToBrowser($"https://www.google.com/search?q={rom.Title}+Images+site:launchbox-app.com&udm=2");
        private void linkLabel_SearchOnGamesDatabaseOrg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => JumpToBrowser($"https://www.gamesdatabase.org/list.aspx?in=1&searchtext={rom.Title}&searchtype=1");
        private void linkLabel_GoogleTitleAndSystemImageOnLaunchBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => JumpToBrowser($"https://www.google.com/search?q={rom.Title}+{system}+Images+site:launchbox-app.com&udm=2");
        private void linkLabel_GoogleTitleAndSystemOnLaunchBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => JumpToBrowser($"https://www.google.com/search?q={rom.Title}+{system}+site:launchbox-app.com");
    }
}
