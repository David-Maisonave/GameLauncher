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
    public partial class Form_RomDetailsEditor : Form
    {
        private Form_Main form_Main = null;
        public Rom rom { get; private set; }
        public bool Ok { get; private set; } = false;
        public Form_RomDetailsEditor(Rom rom, Form_Main form_Main)
        {
            this.rom = rom;
            this.form_Main = form_Main;
            InitializeComponent();
            // Read-Only fields
            textBox_System.Text = rom.System.ToString();
            textBox_FilePath.Text = rom.FilePath;
            checkBox_Disable.Checked = rom.Disable;
            textBox_RomSize.Text = rom.RomSize.ToString();
            textBox_PlayCount.Text = rom.PlayCount.ToString();
            textBox_StarRating.Text = rom.StarRating.ToString();
            textBox_StarRatingVoteCount.Text = rom.StarRatingVoteCount.ToString();
            // Edit fields
            textBox_Description.Text = rom.Description;
            textBox_Developer.Text = rom.Developer;
            textBox_Publisher.Text = rom.Publisher;
            textBox_FileFormat.Text = rom.FileFormat;
            textBox_Genre.Text = rom.Genre;
            textBox_ImagePath.Text = rom.ImagePath;
            textBox_Language.Text = rom.Language;
            textBox_NotesCore.Text = rom.NotesCore;
            textBox_NotesUser.Text = rom.NotesUser;
            textBox_PreferredEmulatorID.Text = rom.PreferredEmulatorID.ToString();
            textBox_QtyPlayers.Text = rom.QtyPlayers.ToString();
            textBox_Region.Text = rom.Region;
            textBox_ReleaseDate.Text = rom.ReleaseDate;
            textBox_Status.Text = rom.Status;
            textBox_Title.Text = rom.Title;
            textBox_Version.Text = rom.Version;
            textBox_Year.Text = rom.Year.ToString();
            textBox_Rating.Text = rom.Rating;
            checkBox_Hide.Checked = rom.Hide;
            checkBox_Favorite.Checked = rom.Favorite;
            textBox_WikipediaUrl.Text = rom.WikipediaURL;
            // Read-Only but modifiable via button function
            textBox_Checksum.Text = rom.Checksum;
            textBox_CompressChecksum.Text = rom.CompressChecksum;
            // Read-Only but modifiable via reset button
            textBox_NameOrg.Text = rom.NameOrg;
            textBox_Compressed.Text = rom.Compressed;
            textBox_NameSimplified.Text = rom.NameSimplified;
        }
        private void button_Ok_Click(object sender, EventArgs e)
        {
            rom.Description = textBox_Description.Text;
            rom.Developer = textBox_Developer.Text;
            rom.Publisher = textBox_Publisher.Text;
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
            rom.Year = Int32.Parse(textBox_Year.Text);
            rom.Rating = textBox_Rating.Text;
            rom.Hide = checkBox_Hide.Checked;
            rom.Favorite = checkBox_Favorite.Checked;
            rom.WikipediaURL = textBox_WikipediaUrl.Text;
            // Read-Only but modifiable via button function
            rom.CompressChecksum = textBox_CompressChecksum.Text;
            // Read-Only but modifiable via reset button
            rom.NameOrg = textBox_NameOrg.Text;
            rom.Compressed = textBox_Compressed.Text;
            rom.NameSimplified = textBox_NameSimplified.Text;
            Ok = true;
            this.Close();
        }
        private void button_Cancel_Click(object sender, EventArgs e) => this.Close();
        private void button_CreateNewMD5Checksum_Click(object sender, EventArgs e) => textBox_Checksum.Text = Form_Main.GetRomChecksum(rom.FilePath, Form_Main.ChecksumType.MD5);
        private void button_CreateNewSHA256Checksum_Click(object sender, EventArgs e) => textBox_Checksum.Text = Form_Main.GetRomChecksum(rom.FilePath, Form_Main.ChecksumType.SHA256);
        private void button_CreateNewMD5ChecksumForCompressChecksum_Click(object sender, EventArgs e) => textBox_CompressChecksum.Text = Form_Main.GetRomCompressChecksum(rom.FilePath, Form_Main.ChecksumType.MD5);
        private void button_CreateNewSHA256ChecksumForCompressChecksum_Click(object sender, EventArgs e) => textBox_CompressChecksum.Text = Form_Main.GetRomCompressChecksum(rom.FilePath, Form_Main.ChecksumType.SHA256);
        private void button_Reset_Click(object sender, EventArgs e)
        {
            textBox_NameOrg.Text = Form_Main.GetFileNameWithoutExtensionAndWithoutBin(textBox_FilePath.Text);
            textBox_NameSimplified.Text = form_Main.ConvertToNameSimplified(textBox_NameOrg.Text);
            textBox_Compressed.Text = form_Main.ConvertToCompress(textBox_NameOrg.Text);
            textBox_Title.Text = form_Main.ConvertToTitle(textBox_NameOrg.Text);
        }
    }
}
