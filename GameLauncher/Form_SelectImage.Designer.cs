namespace GameLauncher
{
    partial class Form_SelectImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SelectImage));
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Ok = new System.Windows.Forms.Button();
            this.listView_FileList = new System.Windows.Forms.ListView();
            this.pictureBox_Preview = new System.Windows.Forms.PictureBox();
            this.label_Instructions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(273, 333);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 28);
            this.button_Cancel.TabIndex = 19;
            this.button_Cancel.Text = "&Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Ok
            // 
            this.button_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Ok.Location = new System.Drawing.Point(186, 333);
            this.button_Ok.Margin = new System.Windows.Forms.Padding(2);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(67, 26);
            this.button_Ok.TabIndex = 18;
            this.button_Ok.Text = "&OK";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // listView_FileList
            // 
            this.listView_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView_FileList.HideSelection = false;
            this.listView_FileList.Location = new System.Drawing.Point(3, 38);
            this.listView_FileList.Name = "listView_FileList";
            this.listView_FileList.Size = new System.Drawing.Size(256, 256);
            this.listView_FileList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_FileList.TabIndex = 20;
            this.listView_FileList.UseCompatibleStateImageBehavior = false;
            this.listView_FileList.SelectedIndexChanged += new System.EventHandler(this.listView_FileList_SelectedIndexChanged);
            // 
            // pictureBox_Preview
            // 
            this.pictureBox_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_Preview.Location = new System.Drawing.Point(265, 38);
            this.pictureBox_Preview.Name = "pictureBox_Preview";
            this.pictureBox_Preview.Size = new System.Drawing.Size(256, 256);
            this.pictureBox_Preview.TabIndex = 21;
            this.pictureBox_Preview.TabStop = false;
            // 
            // label_Instructions
            // 
            this.label_Instructions.AutoSize = true;
            this.label_Instructions.Location = new System.Drawing.Point(7, 2);
            this.label_Instructions.MaximumSize = new System.Drawing.Size(510, 32);
            this.label_Instructions.MinimumSize = new System.Drawing.Size(510, 32);
            this.label_Instructions.Name = "label_Instructions";
            this.label_Instructions.Size = new System.Drawing.Size(510, 32);
            this.label_Instructions.TabIndex = 22;
            this.label_Instructions.Text = "Select Image";
            this.label_Instructions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form_SelectImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 361);
            this.Controls.Add(this.label_Instructions);
            this.Controls.Add(this.pictureBox_Preview);
            this.Controls.Add(this.listView_FileList);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(540, 10000);
            this.MinimumSize = new System.Drawing.Size(540, 400);
            this.Name = "Form_SelectImage";
            this.Text = "Select Image";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.ListView listView_FileList;
        private System.Windows.Forms.PictureBox pictureBox_Preview;
        private System.Windows.Forms.Label label_Instructions;
    }
}