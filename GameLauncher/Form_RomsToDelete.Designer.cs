namespace GameLauncher
{
    partial class Form_RomsToDelete
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_DeleteSelectedRoms = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sortOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.longestFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortestFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largestFileSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallestFileSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rOMVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllButFirstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_OnlyRemoveFromDb = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_DeleteSelectedRoms
            // 
            this.button_DeleteSelectedRoms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_DeleteSelectedRoms.Location = new System.Drawing.Point(18, 631);
            this.button_DeleteSelectedRoms.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_DeleteSelectedRoms.Name = "button_DeleteSelectedRoms";
            this.button_DeleteSelectedRoms.Size = new System.Drawing.Size(219, 43);
            this.button_DeleteSelectedRoms.TabIndex = 1;
            this.button_DeleteSelectedRoms.Text = "&Delete Selected ROM\'s";
            this.button_DeleteSelectedRoms.UseVisualStyleBackColor = true;
            this.button_DeleteSelectedRoms.Click += new System.EventHandler(this.button_DeleteSelectedRoms_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(556, 631);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(112, 43);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(18, 42);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(649, 578);
            this.treeView1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortOrderToolStripMenuItem,
            this.selectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(687, 35);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sortOrderToolStripMenuItem
            // 
            this.sortOrderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.longestFileNameToolStripMenuItem,
            this.shortestFileNameToolStripMenuItem,
            this.largestFileSizeToolStripMenuItem,
            this.smallestFileSizeToolStripMenuItem,
            this.rOMVersionToolStripMenuItem});
            this.sortOrderToolStripMenuItem.Name = "sortOrderToolStripMenuItem";
            this.sortOrderToolStripMenuItem.Size = new System.Drawing.Size(188, 29);
            this.sortOrderToolStripMenuItem.Text = "&Sort Order (Sub Set)";
            // 
            // longestFileNameToolStripMenuItem
            // 
            this.longestFileNameToolStripMenuItem.Name = "longestFileNameToolStripMenuItem";
            this.longestFileNameToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.longestFileNameToolStripMenuItem.Text = "Longest File Name";
            this.longestFileNameToolStripMenuItem.Click += new System.EventHandler(this.longestFileNameToolStripMenuItem_Click);
            // 
            // shortestFileNameToolStripMenuItem
            // 
            this.shortestFileNameToolStripMenuItem.Name = "shortestFileNameToolStripMenuItem";
            this.shortestFileNameToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.shortestFileNameToolStripMenuItem.Text = "Shortest File Name";
            this.shortestFileNameToolStripMenuItem.Click += new System.EventHandler(this.shortestFileNameToolStripMenuItem_Click);
            // 
            // largestFileSizeToolStripMenuItem
            // 
            this.largestFileSizeToolStripMenuItem.Name = "largestFileSizeToolStripMenuItem";
            this.largestFileSizeToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.largestFileSizeToolStripMenuItem.Text = "Largest File Size";
            this.largestFileSizeToolStripMenuItem.Click += new System.EventHandler(this.largestFileSizeToolStripMenuItem_Click);
            // 
            // smallestFileSizeToolStripMenuItem
            // 
            this.smallestFileSizeToolStripMenuItem.Name = "smallestFileSizeToolStripMenuItem";
            this.smallestFileSizeToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.smallestFileSizeToolStripMenuItem.Text = "Smallest File Size";
            this.smallestFileSizeToolStripMenuItem.Click += new System.EventHandler(this.smallestFileSizeToolStripMenuItem_Click);
            // 
            // rOMVersionToolStripMenuItem
            // 
            this.rOMVersionToolStripMenuItem.Name = "rOMVersionToolStripMenuItem";
            this.rOMVersionToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.rOMVersionToolStripMenuItem.Text = "ROM Version";
            this.rOMVersionToolStripMenuItem.Click += new System.EventHandler(this.rOMVersionToolStripMenuItem_Click);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.deselectAllToolStripMenuItem,
            this.selectAllButFirstToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(99, 29);
            this.selectionToolStripMenuItem.Text = "S&election";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // deselectAllToolStripMenuItem
            // 
            this.deselectAllToolStripMenuItem.Name = "deselectAllToolStripMenuItem";
            this.deselectAllToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.deselectAllToolStripMenuItem.Text = "Deselect All";
            this.deselectAllToolStripMenuItem.Click += new System.EventHandler(this.deselectAllToolStripMenuItem_Click);
            // 
            // selectAllButFirstToolStripMenuItem
            // 
            this.selectAllButFirstToolStripMenuItem.Name = "selectAllButFirstToolStripMenuItem";
            this.selectAllButFirstToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.selectAllButFirstToolStripMenuItem.Text = "Select All But First";
            this.selectAllButFirstToolStripMenuItem.Click += new System.EventHandler(this.selectAllButFirstToolStripMenuItem_Click);
            // 
            // button_OnlyRemoveFromDb
            // 
            this.button_OnlyRemoveFromDb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OnlyRemoveFromDb.Location = new System.Drawing.Point(272, 631);
            this.button_OnlyRemoveFromDb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_OnlyRemoveFromDb.Name = "button_OnlyRemoveFromDb";
            this.button_OnlyRemoveFromDb.Size = new System.Drawing.Size(251, 43);
            this.button_OnlyRemoveFromDb.TabIndex = 5;
            this.button_OnlyRemoveFromDb.Text = "&Only Remove From Database";
            this.button_OnlyRemoveFromDb.UseVisualStyleBackColor = true;
            this.button_OnlyRemoveFromDb.Click += new System.EventHandler(this.button_OnlyRemoveFromDb_Click);
            // 
            // FormRomsToDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 692);
            this.Controls.Add(this.button_OnlyRemoveFromDb);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_DeleteSelectedRoms);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "FormRomsToDelete";
            this.Text = "ROM\'s To Delete";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_DeleteSelectedRoms;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sortOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem longestFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortestFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largestFileSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallestFileSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllButFirstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rOMVersionToolStripMenuItem;
        private System.Windows.Forms.Button button_OnlyRemoveFromDb;
    }
}