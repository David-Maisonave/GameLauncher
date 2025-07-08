namespace GameLauncher
{
    partial class Form_RegexRenameFiles
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Dir = new System.Windows.Forms.TextBox();
            this.button_dir = new System.Windows.Forms.Button();
            this.textBox_Pattern = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ReplaceStr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Ok = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Directory";
            // 
            // textBox_Dir
            // 
            this.textBox_Dir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Dir.Location = new System.Drawing.Point(133, 13);
            this.textBox_Dir.Name = "textBox_Dir";
            this.textBox_Dir.Size = new System.Drawing.Size(396, 26);
            this.textBox_Dir.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_Dir, "Enter target directory to rename files. Example: C:\\Emulator\\NintendoDS\\roms");
            // 
            // button_dir
            // 
            this.button_dir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_dir.Location = new System.Drawing.Point(535, 13);
            this.button_dir.Name = "button_dir";
            this.button_dir.Size = new System.Drawing.Size(36, 27);
            this.button_dir.TabIndex = 2;
            this.button_dir.Text = "..";
            this.button_dir.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_dir.UseVisualStyleBackColor = true;
            this.button_dir.Click += new System.EventHandler(this.button_dir_Click);
            // 
            // textBox_Pattern
            // 
            this.textBox_Pattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Pattern.Location = new System.Drawing.Point(133, 45);
            this.textBox_Pattern.Name = "textBox_Pattern";
            this.textBox_Pattern.Size = new System.Drawing.Size(396, 26);
            this.textBox_Pattern.TabIndex = 4;
            this.toolTip1.SetToolTip(this.textBox_Pattern, "Enter REGEX search pattern. Example: (roms\\\\\\\\)[0-9][0-9][0-9][0-9][\\\\s_]-[\\\\s_](" +
        ".*)");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Search Pattern";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox_ReplaceStr
            // 
            this.textBox_ReplaceStr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ReplaceStr.Location = new System.Drawing.Point(133, 77);
            this.textBox_ReplaceStr.Name = "textBox_ReplaceStr";
            this.textBox_ReplaceStr.Size = new System.Drawing.Size(396, 26);
            this.textBox_ReplaceStr.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBox_ReplaceStr, "Enter REGEX replacement string. Use $1 to replace () brace data that\'s in the sea" +
        "rch pattern, where $1 replaces first () brace set, $2 replaces second () brace s" +
        "et...etc... Example Usage: $1$2");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "&Replace String";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(319, 124);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(73, 28);
            this.button_Cancel.TabIndex = 8;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Ok
            // 
            this.button_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Ok.Location = new System.Drawing.Point(201, 124);
            this.button_Ok.Margin = new System.Windows.Forms.Padding(2);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(65, 26);
            this.button_Ok.TabIndex = 7;
            this.button_Ok.Text = "&OK";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // FormRegexRenameFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 161);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.textBox_ReplaceStr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Pattern);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_dir);
            this.Controls.Add(this.textBox_Dir);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1600, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "FormRegexRenameFiles";
            this.Text = "Regex Rename Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Dir;
        private System.Windows.Forms.Button button_dir;
        private System.Windows.Forms.TextBox textBox_Pattern;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ReplaceStr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}