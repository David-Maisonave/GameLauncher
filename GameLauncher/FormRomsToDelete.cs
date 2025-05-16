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
    public partial class FormRomsToDelete : Form
    {
        public List<string> RomSeletedToDelete { get; private set; } = new List<string>();
        private List<Rom> RomCandidatesToDelete = new List<Rom>();
        private Dictionary<string, SortedSet<Rom>> Candidates = new Dictionary<string, SortedSet<Rom>>();
        enum SortTypeSelected
        {
            None = 0,
            PathLenRev = 1,
            PathLen = 2,
            RomSizeRev = 3,
            RomSize = 4
        }
        private SortTypeSelected sortTypeSelected = SortTypeSelected.PathLenRev;
        private IComparer<Rom> GetComparisonType()
        {
            // ToDo: Add logic here to choose between below types depending on user settings or new dialog option
            // SortRomByFilePathLen, SortRomByFilePathLenRev, SortRomByFileSizeLen, SortRomByFileSizeLenRev
            switch (sortTypeSelected)
            {
                case SortTypeSelected.PathLenRev:
                    return new SortRomByFilePathLenRev();
                case SortTypeSelected.PathLen:
                    return new SortRomByFilePathLen();
                case SortTypeSelected.RomSizeRev:
                    return new SortRomByFileSizeLenRev();
                case SortTypeSelected.RomSize:
                    return new SortRomByFileSizeLen();
            }
            return new SortRomByFilePathLenRev();
        }
        private void PopulateTreeView(SortTypeSelected select = SortTypeSelected.None)
        {
            if (select != SortTypeSelected.None)
                sortTypeSelected = select;
            //treeView1.CollapseAll(); 
            treeView1.Nodes.Clear();
            Candidates.Clear();
            for (int i = 0; i < RomCandidatesToDelete.Count; i++)
            {
                if (!Candidates.ContainsKey(RomCandidatesToDelete[i].Checksum))
                    Candidates[RomCandidatesToDelete[i].Checksum] = new SortedSet<Rom>(GetComparisonType());
                Candidates[RomCandidatesToDelete[i].Checksum].Add(RomCandidatesToDelete[i]);
            }
            treeView1.CheckBoxes = true;
            TreeNode parentNode = treeView1.Nodes.Add("Duplicates");
            TreeNode firstChildNode = null;
            TreeNode lastChildNode = null;
            //toolTip1.SetToolTip(treeView1, "Checkbox for parent node is ignore. Only use checkbox at file name level.");
            foreach (string key in Candidates.Keys)
            {
                TreeNode childNode = treeView1.Nodes[0].Nodes.Add($"Checksum={key}");
                if (firstChildNode == null)
                    firstChildNode = childNode;
                lastChildNode = childNode;
                bool isFirstNode = true;
                foreach (Rom rom in Candidates[key])
                {
                    TreeNode cc = childNode.Nodes.Add(rom.FilePath);
                    if (isFirstNode)
                        isFirstNode = false;
                    else
                        cc.Checked = true;
                    string tip = $"Size={rom.RomSize}\nTitle={rom.Title}\nRegion={rom.Region}\nStatus={rom.Status}\nNotesCore={rom.NotesCore}\nNotesUser={rom.NotesUser}\nDescription={rom.Description}\nLanguage={rom.Language}";
                    //toolTip1.SetToolTip(cc.TreeView, tip);
                    cc.ToolTipText = tip;
                }
            }
            //if (lastChildNode != null)
            //    lastChildNode.EnsureVisible();
            treeView1.ExpandAll();
        }
        public FormRomsToDelete(List<Rom> romCandidatesToDelete)
        {
            InitializeComponent();
            RomCandidatesToDelete = romCandidatesToDelete;
            PopulateTreeView();
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
        private void ChangeSelection(bool select)
        {
            foreach (System.Windows.Forms.TreeNode aNode in treeView1.Nodes)
            {
                foreach (System.Windows.Forms.TreeNode acNode in aNode.Nodes)
                {
                    foreach (System.Windows.Forms.TreeNode accNode in acNode.Nodes) 
                        accNode.Checked = select;
                }
            }
        }
        private void longestFileNameToolStripMenuItem_Click(object sender, EventArgs e) => PopulateTreeView(SortTypeSelected.PathLenRev);
        private void shortestFileNameToolStripMenuItem_Click(object sender, EventArgs e) => PopulateTreeView(SortTypeSelected.PathLen);
        private void largestFileSizeToolStripMenuItem_Click(object sender, EventArgs e) => PopulateTreeView(SortTypeSelected.RomSize);
        private void smallestFileSizeToolStripMenuItem_Click(object sender, EventArgs e) => PopulateTreeView(SortTypeSelected.RomSizeRev);
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => ChangeSelection(true);
        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e) => ChangeSelection(false);
        private void selectAllButFirstToolStripMenuItem_Click(object sender, EventArgs e) => PopulateTreeView();
    }
}
