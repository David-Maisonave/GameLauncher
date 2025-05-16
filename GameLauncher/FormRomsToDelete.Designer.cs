namespace GameLauncher
{
    partial class FormRomsToDelete
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
            this.button_DeleteSelectedRoms = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // button_DeleteSelectedRoms
            // 
            this.button_DeleteSelectedRoms.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_DeleteSelectedRoms.Location = new System.Drawing.Point(78, 410);
            this.button_DeleteSelectedRoms.Name = "button_DeleteSelectedRoms";
            this.button_DeleteSelectedRoms.Size = new System.Drawing.Size(203, 28);
            this.button_DeleteSelectedRoms.TabIndex = 1;
            this.button_DeleteSelectedRoms.Text = "Delete Selected ROM\'s";
            this.button_DeleteSelectedRoms.UseVisualStyleBackColor = true;
            this.button_DeleteSelectedRoms.Click += new System.EventHandler(this.button_DeleteSelectedRoms_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(339, 410);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 28);
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
            this.treeView1.Location = new System.Drawing.Point(12, 13);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(587, 380);
            this.treeView1.TabIndex = 3;
            // 
            // FormRomsToDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 450);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_DeleteSelectedRoms);
            this.MinimizeBox = false;
            this.Name = "FormRomsToDelete";
            this.Text = "ROM\'s To Delete";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_DeleteSelectedRoms;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TreeView treeView1;
    }
}