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
    public partial class FormRomsToDelete : Form
    {
        private List<Rom> RomCandidatesToDelete = new List<Rom>();
        public List<string> RomSeletedToDelete { get; private set; } = new List<string>();
        private Dictionary<string, SortedSet<Rom>> Candidates = new Dictionary<string, SortedSet<Rom>>();
        private IComparer<Rom> GetComparisonType()
        {
            // ToDo: Add logic here to choose between below types depending on user settings or new dialog option
            // SortRomByFilePathLen, SortRomByFilePathLenRev, SortRomByFileSizeLen, SortRomByFileSizeLenRev
            return new SortRomByFilePathLenRev();
        }
        public FormRomsToDelete(List<Rom> romCandidatesToDelete)
        {
            InitializeComponent();
            RomCandidatesToDelete = romCandidatesToDelete;
            for (int i = 0;i < RomCandidatesToDelete.Count;i++) 
            {
                if (!Candidates.ContainsKey(RomCandidatesToDelete[i].Checksum))
                    Candidates[RomCandidatesToDelete[i].Checksum] = new SortedSet<Rom>(GetComparisonType());
                Candidates[RomCandidatesToDelete[i].Checksum].Add(RomCandidatesToDelete[i]);
            }
            treeView1.CheckBoxes = true;
            TreeNode parentNode = treeView1.Nodes.Add("Parent");
            foreach (string key in Candidates.Keys)
            {
                TreeNode childNode = treeView1.Nodes[0].Nodes.Add(key);
                bool isFirstNode = true;
                foreach(Rom rom in Candidates[key])
                {
                    TreeNode cc = childNode.Nodes.Add(rom.FilePath);
                    //cc.TreeView.CheckBoxes = true;
                    if (isFirstNode)
                        isFirstNode = false;
                    else
                        cc.Checked = true;
                }
            }
            treeView1.ExpandAll();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_DeleteSelectedRoms_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.TreeNode aNode in treeView1.Nodes) 
            {
                if (aNode.Checked)
                {
                    // Populate RomSeletedToDelete with all contents of RomCandidatesToDelete
                    // Give warning first.
                }
                else
                {
                    foreach (System.Windows.Forms.TreeNode acNode in aNode.Nodes)
                    {
                        foreach (System.Windows.Forms.TreeNode accNode in acNode.Nodes)
                        {
                            if (accNode.Checked)
                            {
                                string fileToDelete = accNode.ToString();
                                RomSeletedToDelete.Add(fileToDelete);
                            }
                        }
                    }
                }
            }
            this.Close();
        }
    }
}
