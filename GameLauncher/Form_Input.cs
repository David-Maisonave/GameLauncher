using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GameLauncher
{
    public partial class InputForm : Form
    {
        public bool Ok { get; private set; }
        public string Value { get; private set; }
        public InputForm(string label, string title, string defaultValue = "", string textTooltip = "", string labelTooltip = "")
        {
            InitializeComponent();
            Ok = false;
            // None of the following worked to align label to the right. Workaround is to prefix spaces in input variable label.
            ///////////////////////////////////////////////////////////
            //label1.AutoSize = false;
            //label1.TextAlign = ContentAlignment.TopRight;
            //label1.RightToLeft = RightToLeft.Yes;
            ///////////////////////////////////////////////////////////
            label1.Text = label;
            textBox1.Text = defaultValue;
            toolTip1.SetToolTip(textBox1, textTooltip);
            toolTip1.SetToolTip(label1, labelTooltip);
            this.Text = title;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            Ok = true;
            Value = textBox1.Text;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
