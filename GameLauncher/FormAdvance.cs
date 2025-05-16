using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static GameLauncher.Form_Main;

namespace GameLauncher
{
    public partial class FormAdvance : Form
    {
        private Form_Main form_Main = null;
        public FormAdvance(Form_Main form_main)
        {
            form_Main = form_main;
            InitializeComponent();
        }
        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_RegexRenameFiles_Click(object sender, EventArgs e)
        {
            FormRegexRenameFiles formRegexRenameFiles = new FormRegexRenameFiles();
            formRegexRenameFiles.ShowDialog();
            this.Close();
        }
        private void button_Rescan_Click(object sender, EventArgs e)
        {
            form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateRedoDbInit), form_Main.GetDelegateAction(form_Main, null));
            this.Close();
        }
        private void button_ScanSelectedSystemRomsAndImages_Click(object sender, EventArgs e)
        {
            form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateScanForNewRomsOnAllSystems), form_Main.GetDelegateAction(form_Main, true));
            this.Close();
        }

        private void button_ScanForNewRomsOnAllSystems_Click(object sender, EventArgs e)
        {
            form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateScanForNewRomsOnAllSystems), form_Main.GetDelegateAction(form_Main, false));
            this.Close();
        }

        private void button_ScanSelectedSystem_Click(object sender, EventArgs e)
        {
            form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateRescanSelectedSystem), form_Main.GetDelegateAction(form_Main, null));
            this.Close();
        }

        private void button_ResetImageListCache_Click(object sender, EventArgs e)
        {
            form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateCreateCacheForDisplaySystemIcons), form_Main.GetDelegateAction(form_Main, null));
            this.Close();
        }

        private void button_DelImgFilesNotInDb_Click(object sender, EventArgs e)
        {
            DialogResult results = MessageBox.Show($"Do you want to delete all image files that are not in the GameLauncher database?\nClick YES for silent deletion.\nClick NO to DELETE the image files but give a prompt before each deletion.\nClick CANCEL to exit this option (No Deletions).", "Delete Image", MessageBoxButtons.YesNoCancel);
            if (results == DialogResult.Cancel)
                return;
            form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateDeleteImageFilesNotInDatabase), form_Main.GetDelegateAction(form_Main, results == DialogResult.Yes));
        }

        private void button_DelRomFilesInDb_Click(object sender, EventArgs e)
        {
            //DialogResult results = MessageBox.Show($"Do you want to delete duplicate ROM files that have the same checksum in GameLauncher database?\nClick YES for silent deletion.\nClick NO to DELETE the ROM files but give a prompt before each deletion.\nClick CANCEL to exit this option (No Deletions).", "Delete ROM", MessageBoxButtons.YesNoCancel);
            //if (results == DialogResult.Cancel)
            //    return;
            //form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateDeleteDuplicateRomFilesInDatabase), form_Main.GetDelegateAction(form_Main, results == DialogResult.Yes));
            form_Main.BeginInvoke(new MyDelegateForm_Main(form_Main.DelegateDeleteDuplicateRomFilesInDatabase), form_Main.GetDelegateAction(form_Main, true));
        }
    }
}
