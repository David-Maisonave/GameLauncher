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
    public partial class RomDetailsEditor : Form
    {
        public Rom rom { get; private set; }
        public bool Ok { get; private set; } = false;
        public RomDetailsEditor(Rom rom)
        {
            this.rom = rom;
            InitializeComponent();
            textBox_Checksum.Text = rom.Checksum;
            textBox_CompressChecksum.Text = rom.CompressChecksum;
            textBox_Compressed.Text = rom.Compressed;
            textBox_Description.Text = rom.Description;
            textBox_Developer.Text = rom.Developer;
            textBox_FileFormat.Text = rom.FileFormat;
            textBox_FilePath.Text = rom.FilePath;
            textBox_Genre.Text = rom.Genre;
            textBox_ImagePath.Text = rom.ImagePath;
            textBox_Language.Text = rom.Language;
            textBox_NameOrg.Text = rom.NameOrg;
            textBox_NameSimplified.Text = rom.NameSimplified;
            textBox_NotesCore.Text = rom.NotesCore;
            textBox_NotesUser.Text = rom.NotesUser;
            textBox_PreferredEmulatorID.Text = rom.PreferredEmulatorID.ToString();
            textBox_QtyPlayers.Text = rom.QtyPlayers.ToString();
            textBox_Region.Text = rom.Region;
            textBox_ReleaseDate.Text = rom.ReleaseDate;
            textBox_RomSize.Text = rom.RomSize.ToString();
            textBox_Status.Text = rom.Status;
            textBox_System.Text = rom.System.ToString();
            textBox_Title.Text = rom.Title;
            textBox_Version.Text = rom.Version;
        }
        private void button_Ok_Click(object sender, EventArgs e)
        {
            rom.Description = textBox_Description.Text;
            rom.Developer = textBox_Developer.Text;
            rom.FileFormat = textBox_FileFormat.Text;
            rom.Genre = textBox_Genre.Text;
            rom.ImagePath = textBox_ImagePath.Text;
            rom.Language = textBox_Language.Text;
            rom.NotesCore = textBox_NotesCore.Text;
            rom.NotesUser = textBox_NotesUser.Text;
            rom.PreferredEmulatorID = Int32.Parse(textBox_PreferredEmulatorID.Text);
            rom.QtyPlayers = Int32.Parse(textBox_QtyPlayers.Text);
            rom.Region = textBox_Region.Text;
            rom.ReleaseDate = textBox_ReleaseDate.Text;
            rom.Status = textBox_Status.Text;
            rom.Title = textBox_Title.Text;
            rom.Version = textBox_Version.Text;
            Ok = true;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e) => this.Close();

        private void Title_TextChanged(object sender, EventArgs e)
        {

        }

        private void RomDetailsEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
