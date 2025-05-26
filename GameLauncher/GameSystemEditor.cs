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
    public partial class GameSystemEditor : Form
    {
        public GameSystem gameSystem { get; private set; }
        public bool Ok { get; private set; } = false;
        public GameSystemEditor(GameSystem gameSystem)
        {
            this.gameSystem = gameSystem;
            InitializeComponent();
            textBox_EmulatorPath1.Text  =  gameSystem.EmulatorPaths[0];
            textBox_EmulatorPath2.Text  =  gameSystem.EmulatorPaths[1];
            textBox_EmulatorPath3.Text  =  gameSystem.EmulatorPaths[2];
            textBox_EmulatorPath4.Text  =  gameSystem.EmulatorPaths[3];
            textBox_EmulatorPath5.Text  =  gameSystem.EmulatorPaths[4];
            textBox_EmulatorPath6.Text  =  gameSystem.EmulatorPaths[5];
            textBox_EmulatorPath7.Text  =  gameSystem.EmulatorPaths[6];
            textBox_EmulatorPath8.Text  =  gameSystem.EmulatorPaths[7];
            textBox_EmulatorPath9.Text  =  gameSystem.EmulatorPaths[8];
            textBox_EmulatorPath10.Text =  gameSystem.EmulatorPaths[9];
            textBox_ImageDirPath.Text = gameSystem.ImageDirPath;
            textBox_RomDirPath.Text = gameSystem.RomDirPath;
            textBox_System.Text = gameSystem.ID.ToString();
            textBox_Name.Text = gameSystem.Name;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            gameSystem.EmulatorPaths[0] =  textBox_EmulatorPath1.Text;
            gameSystem.EmulatorPaths[1] =  textBox_EmulatorPath2.Text;
            gameSystem.EmulatorPaths[2] =  textBox_EmulatorPath3.Text;
            gameSystem.EmulatorPaths[3] =  textBox_EmulatorPath4.Text;
            gameSystem.EmulatorPaths[4] =  textBox_EmulatorPath5.Text;
            gameSystem.EmulatorPaths[5] =  textBox_EmulatorPath6.Text;
            gameSystem.EmulatorPaths[6] =  textBox_EmulatorPath7.Text;
            gameSystem.EmulatorPaths[7] =  textBox_EmulatorPath8.Text;
            gameSystem.EmulatorPaths[8] =  textBox_EmulatorPath9.Text;
            gameSystem.EmulatorPaths[9] =  textBox_EmulatorPath10.Text;
            gameSystem.ImageDirPath = textBox_ImageDirPath.Text;
            gameSystem.RomDirPath = textBox_RomDirPath.Text;
            gameSystem.Name = textBox_Name.Text;
            Ok = true;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e) => this.Close();
    }
}
