using Microsoft.WindowsAPICodePack.Dialogs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GameLauncher
{
    // The main purpose of SelectFolderDialog class is to make it easy to replace old FolderBrowserDialog code. Should just work by replacing FolderBrowserDialog with SelectFolderDialog
    // ShowNewFolderButton enable/disable is NOT supported!!! User can right click to select [New]->Folder option from context menu, and this option is ALWAYS enabled.
    public class SelectFolderDialog  // A FolderBrowserDialog replacement class
    {
        protected CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog();
        public bool ShowNewFolderButton = true; // This is a do NOTHING variable used to placate original code using FolderBrowserDialog class
        public string SelectedPath // Same as InitialDirectory. This variable is here to simplify FolderBrowserDialog code replacement
        {
            get { return openFolderDialog.FileName; }
            set { openFolderDialog.InitialDirectory = value; }
        }
        public string InitialDirectory
        { // This property allows you to set the specific path where you want the file dialog to initially open. If you don't set this property or if the specified path doesn't exist, the dialog will typically open to the last used directory.
            get { return openFolderDialog.InitialDirectory; }
            set { openFolderDialog.InitialDirectory = value; }
        }
        public string DefaultDirectory
        { // The "default" directory in this context is essentially the location that the file dialog opens to when you haven't explicitly set the InitialDirectory property, or when the value of InitialDirectory is invalid. This is often the last directory that was opened by any file dialog within your application or even across different applications, and this value is stored in the system's registry. 
            get { return openFolderDialog.DefaultDirectory; }
            set { openFolderDialog.DefaultDirectory = value; }
        }
        public string Description // Same as Title. This variable is here to simplify FolderBrowserDialog code replacement
        {
            get { return openFolderDialog.Title; }
            set { openFolderDialog.Title = value; }
        }
        public string Title
        {
            get { return openFolderDialog.Title; }
            set { openFolderDialog.Title = value; }
        }
        public bool EnsurePathExists
        {
            get { return openFolderDialog.EnsurePathExists; }
            set { openFolderDialog.EnsurePathExists = value; }
        }
        public string FileName
        {
            get { return openFolderDialog.FileName; }
        }
        public List<string> FileNames
        {
            get
            {
                List<string> strings = new List<string>();
                foreach (string s in openFolderDialog.FileNames)
                    strings.Add(s);
                return strings;
            }
        }
        public bool EnsureValidNames
        {
            get { return openFolderDialog.EnsureValidNames; }
            set { openFolderDialog.EnsureValidNames = value; }
        }
        public bool EnsureReadOnly
        {
            get { return openFolderDialog.EnsureReadOnly; }
            set { openFolderDialog.EnsureReadOnly = value; }
        }
        public bool Multiselect
        {
            get { return openFolderDialog.Multiselect; }
            set { openFolderDialog.Multiselect = value; }
        }
        public bool RestoreDirectory
        {
            get { return openFolderDialog.RestoreDirectory; }
            set { openFolderDialog.RestoreDirectory = value; }
        }
        public bool ShowHiddenItems
        {
            get { return openFolderDialog.ShowHiddenItems; }
            set { openFolderDialog.ShowHiddenItems = value; }
        }
        public SelectFolderDialog(string InitialDirectory = "", string DialogTitle = "Select Directory", bool EnsurePathExists = true, bool Multiselect = false)
        {
            openFolderDialog.IsFolderPicker = true;
            this.InitialDirectory = InitialDirectory;
            this.Title = DialogTitle;
            this.EnsurePathExists = EnsurePathExists;
            this.Multiselect = Multiselect;
        }
        public DialogResult ShowDialog(string InitialDirectory = null, string DialogTitle = null)
        {
            if (InitialDirectory != null)
                this.InitialDirectory = InitialDirectory;
            if (DialogTitle != null)
                this.Title = DialogTitle;
            switch (openFolderDialog.ShowDialog())
            {
                case CommonFileDialogResult.Ok:
                    return DialogResult.OK;
                case CommonFileDialogResult.Cancel:
                    return DialogResult.Cancel;
                case CommonFileDialogResult.None:
                    return DialogResult.None;
            }
            return DialogResult.Cancel;
        }
        public DialogResult ShowDialog(bool Multiselect, string InitialDirectory = null, string DialogTitle = null)
        {
            this.Multiselect = Multiselect;
            return ShowDialog(InitialDirectory, DialogTitle);
        }
        public void Dispose() => openFolderDialog.Dispose();
    }
}
