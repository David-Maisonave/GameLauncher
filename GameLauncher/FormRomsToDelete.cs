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
        public List<string> RomSelectedToDelete { get; private set; } = new List<string>();
        public bool OnlyRemoveFromDb { get; private set; } = false;
        private DeleteDuplicateBy deleteDuplicateBy;
        private List<Rom> RomCandidatesToDelete = new List<Rom>();
        private Dictionary<string, SortedSet<Rom>> Candidates = new Dictionary<string, SortedSet<Rom>>();
        enum SortTypeSelected
        {
            None = 0,
            PathLenRev = 1,
            PathLen = 2,
            RomSizeRev = 3,
            RomSize = 4,
            RomVersion = 5,
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
                case SortTypeSelected.RomVersion:
                    return new SortRomByFileVersion();
            }
            return new SortRomByFilePathLenRev();
        }
        private void PopulateTreeView(SortTypeSelected select = SortTypeSelected.None)
        {
            string KeyName = "Checksum";
            if (select != SortTypeSelected.None)
                sortTypeSelected = select;
            treeView1.Nodes.Clear();
            Candidates.Clear();
            for (int i = 0; i < RomCandidatesToDelete.Count; i++)
            {
                if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateChecksum)
                {
                    if (RomCandidatesToDelete[i].Checksum.Contains("2laYvhe5tGliM1eZd5++yozl1JHA0mJDuv756hg3qdg="))
                    {
                        string k = RomCandidatesToDelete[i].Checksum;
                        string k2 = RomCandidatesToDelete[i].CompressChecksum;
                    }
                    if (!Candidates.ContainsKey(RomCandidatesToDelete[i].Checksum))
                        Candidates[RomCandidatesToDelete[i].Checksum] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[RomCandidatesToDelete[i].Checksum].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressChecksum)
                {
                    if (!Candidates.ContainsKey(RomCandidatesToDelete[i].CompressChecksum))
                        Candidates[RomCandidatesToDelete[i].CompressChecksum] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[RomCandidatesToDelete[i].CompressChecksum].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInAnySystem)
                {
                    KeyName = "Title";
                    if (!Candidates.ContainsKey(RomCandidatesToDelete[i].Title))
                        Candidates[RomCandidatesToDelete[i].Title] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[RomCandidatesToDelete[i].Title].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInAnySystem)
                {
                    KeyName = "NameSimplified";
                    if (!Candidates.ContainsKey(RomCandidatesToDelete[i].NameSimplified))
                        Candidates[RomCandidatesToDelete[i].NameSimplified] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[RomCandidatesToDelete[i].NameSimplified].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInAnySystem)
                {
                    KeyName = "NameOrg";
                    if (!Candidates.ContainsKey(RomCandidatesToDelete[i].NameOrg))
                        Candidates[RomCandidatesToDelete[i].NameOrg] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[RomCandidatesToDelete[i].NameOrg].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInAnySystem)
                {
                    KeyName = "Compressed";
                    if (!Candidates.ContainsKey(RomCandidatesToDelete[i].Compressed))
                        Candidates[RomCandidatesToDelete[i].Compressed] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[RomCandidatesToDelete[i].Compressed].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInSameSystem)
                {
                    KeyName = "Title";
                    string system = $"[{Form_Main.GetSystemNameByID(RomCandidatesToDelete[i].System)}]";
                    if (!Candidates.ContainsKey($"{system}-{RomCandidatesToDelete[i].Title}"))
                        Candidates[$"{system}-{RomCandidatesToDelete[i].Title}"] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[$"{system}-{RomCandidatesToDelete[i].Title}"].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInSameSystem)
                {
                    KeyName = "NameSimplified";
                    string system = $"[{Form_Main.GetSystemNameByID(RomCandidatesToDelete[i].System)}]";
                    if (!Candidates.ContainsKey($"{system}-{RomCandidatesToDelete[i].NameSimplified}"))
                        Candidates[$"{system}-{RomCandidatesToDelete[i].NameSimplified}"] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[$"{system}-{RomCandidatesToDelete[i].NameSimplified}"].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInSameSystem)
                {
                    KeyName = "NameOrg";
                    string system = $"[{Form_Main.GetSystemNameByID(RomCandidatesToDelete[i].System)}]";
                    if (!Candidates.ContainsKey($"{system}-{RomCandidatesToDelete[i].NameOrg}"))
                        Candidates[$"{system}-{RomCandidatesToDelete[i].NameOrg}"] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[$"{system}-{RomCandidatesToDelete[i].NameOrg}"].Add(RomCandidatesToDelete[i]);
                }
                else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInSameSystem)
                {
                    KeyName = "Compressed";
                    string system = $"[{Form_Main.GetSystemNameByID(RomCandidatesToDelete[i].System)}]";
                    if (!Candidates.ContainsKey($"{system}-{RomCandidatesToDelete[i].Compressed}"))
                        Candidates[$"{system}-{RomCandidatesToDelete[i].Compressed}"] = new SortedSet<Rom>(GetComparisonType());
                    Candidates[$"{system}-{RomCandidatesToDelete[i].Compressed}"].Add(RomCandidatesToDelete[i]);
                }
            }
            treeView1.CheckBoxes = true;
            treeView1.Nodes.Add("Duplicates");
            /*TreeNode parentNode = treeView1.Nodes.Add("Duplicates"); */
            //TreeNode firstChildNode = null;
            //TreeNode lastChildNode = null;
            foreach (string key in Candidates.Keys)
            {
                if (Candidates[key].Count < 2)  // This should not be needed unless there's a bug in the code
                    continue;
                TreeNode childNode = treeView1.Nodes[0].Nodes.Add($"{KeyName}={key}");
                //if (firstChildNode == null)
                //    firstChildNode = childNode;
                //lastChildNode = childNode;
                bool isFirstNode = true;
                foreach (Rom rom in Candidates[key])
                {
                    TreeNode cc = childNode.Nodes.Add(rom.FilePath);
                    if (key.Contains("2laYvhe5tGliM1eZd5++yozl1JHA0mJDuv756hg3qdg="))
                    {
                        string k2 = Candidates[key].ToString();
                        string k = key;
                        foreach (Rom _rom in Candidates[key])
                        {
                            k2 = _rom.Title;
                            k = _rom.Checksum;
                            k2 = _rom.Title;
                        }
                    }
                    if (isFirstNode)
                        isFirstNode = false;
                    else
                        cc.Checked = true;
                    string tip = $"Size={rom.RomSize}\nTitle={rom.Title}\nRegion={rom.Region}\nStatus={rom.Status}\nNotesCore={rom.NotesCore}\nNotesUser={rom.NotesUser}\nDescription={rom.Description}\nLanguage={rom.Language}";
                    cc.ToolTipText = tip;
                }
            }
            treeView1.ExpandAll();
        }
        public FormRomsToDelete(List<Rom> romCandidatesToDelete, DeleteDuplicateBy delete_duplicate_by)
        {
            InitializeComponent();
            RomCandidatesToDelete = romCandidatesToDelete;
            deleteDuplicateBy = delete_duplicate_by;
            if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateChecksum)
                PopulateTreeView();
            else if (deleteDuplicateBy == DeleteDuplicateBy.DuplicateTitleInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameSimplifiedInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateNameOrgInAnySystem || deleteDuplicateBy == DeleteDuplicateBy.DuplicateCompressedInAnySystem)
                PopulateTreeView(SortTypeSelected.RomSizeRev);
            else
                PopulateTreeView(SortTypeSelected.RomVersion);
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
                    // Populate RomSelectedToDelete with all contents of RomCandidatesToDelete
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
                                RomSelectedToDelete.Add(fileToDelete);
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
        private void rOMVersionToolStripMenuItem_Click(object sender, EventArgs e) => PopulateTreeView(SortTypeSelected.RomVersion);
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => ChangeSelection(true);
        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e) => ChangeSelection(false);
        private void selectAllButFirstToolStripMenuItem_Click(object sender, EventArgs e) => PopulateTreeView();

        private void button_OnlyRemoveFromDb_Click(object sender, EventArgs e)
        {
            OnlyRemoveFromDb = true;
            button_DeleteSelectedRoms_Click(sender,e);
        }
    }
}
