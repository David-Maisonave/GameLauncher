using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GameLauncher
{
    public partial class Form_SelectImage : Form
    {
        private HashSet<string> imageFileNames = null;
        public string selectedImage { get; private set; } = null ;
        public Form_SelectImage(HashSet<string> imageFileNames, string instructions = "Select Image")
        {
            InitializeComponent();
            label_Instructions.Text = instructions;
            this.imageFileNames = imageFileNames;
            string firstFilename = null;
            foreach (string fileName in imageFileNames)
            {
                if (firstFilename == null)
                    firstFilename = fileName;
                listView_FileList.Items.Add(fileName);
            }
            pictureBox_Preview.Image = Image.FromFile(firstFilename);
        }
        private void button_Ok_Click(object sender, EventArgs e)
        {
            selectedImage = listView_FileList.SelectedItems[0].Text;
            this.Close();
        }
        private void button_Cancel_Click(object sender, EventArgs e) => this.Close();
        private void listView_FileList_SelectedIndexChanged(object sender, EventArgs e) => pictureBox_Preview.Image = Image.FromFile(listView_FileList.SelectedItems[0].Text);
    }
}
