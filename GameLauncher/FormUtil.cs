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
    public partial class FormUtil : Form
    {
        private Form_Main form_Main = null;
        public FormUtil(Form_Main form_main)
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
    }
}
