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
    public partial class Form_ImageDeletionPrompt : Form
    {
        public bool Yes { get; private set; } = false;
        public bool CancelAll { get; private set; } = false;
        public Form_ImageDeletionPrompt(string imageFile, string imageInDbFile)
        {
            InitializeComponent();
            textBox_FileToDelete.Text = imageFile;
            Image tempImage = Image.FromFile(imageFile);
            Bitmap tempBitmap = new Bitmap(tempImage);
            tempImage.Dispose();
            pictureBox_FileToDelete.Image = tempBitmap;
            pictureBox_ImageInDb.Load(imageInDbFile);
            toolTip1.SetToolTip(pictureBox_ImageInDb, imageInDbFile);
            toolTip1.SetToolTip(textBox_FileToDelete, imageFile);
            toolTip1.SetToolTip(pictureBox_FileToDelete, imageFile);
        }

        private void button_Yes_Click(object sender, EventArgs e)
        {
            Yes = true;
            this.Close();
        }

        private void button_No_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_CancelAll_Click(object sender, EventArgs e)
        {
            CancelAll = true;
            this.Close();
        }
    }
}
