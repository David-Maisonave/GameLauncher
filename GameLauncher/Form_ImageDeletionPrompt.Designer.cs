namespace GameLauncher
{
    partial class Form_ImageDeletionPrompt
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
            this.textBox_Msg = new System.Windows.Forms.TextBox();
            this.pictureBox_FileToDelete = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_FileToDelete = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox_ImageInDb = new System.Windows.Forms.PictureBox();
            this.button_No = new System.Windows.Forms.Button();
            this.button_Yes = new System.Windows.Forms.Button();
            this.button_CancelAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_FileToDelete)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ImageInDb)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Msg
            // 
            this.textBox_Msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Msg.Location = new System.Drawing.Point(12, 12);
            this.textBox_Msg.Multiline = true;
            this.textBox_Msg.Name = "textBox_Msg";
            this.textBox_Msg.ReadOnly = true;
            this.textBox_Msg.Size = new System.Drawing.Size(544, 74);
            this.textBox_Msg.TabIndex = 0;
            this.textBox_Msg.TabStop = false;
            this.textBox_Msg.Text = "Click yes to delete file in the File-To-Delete field.\r\nClick no to not delete thi" +
    "s file.\r\nClick Cancel-All to skip all remaining deleteions.";
            // 
            // pictureBox_FileToDelete
            // 
            this.pictureBox_FileToDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_FileToDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_FileToDelete.Location = new System.Drawing.Point(6, 25);
            this.pictureBox_FileToDelete.Name = "pictureBox_FileToDelete";
            this.pictureBox_FileToDelete.Size = new System.Drawing.Size(256, 256);
            this.pictureBox_FileToDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_FileToDelete.TabIndex = 1;
            this.pictureBox_FileToDelete.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox_FileToDelete);
            this.groupBox1.Location = new System.Drawing.Point(12, 107);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 293);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File To Delete";
            // 
            // textBox_FileToDelete
            // 
            this.textBox_FileToDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_FileToDelete.Location = new System.Drawing.Point(18, 407);
            this.textBox_FileToDelete.Name = "textBox_FileToDelete";
            this.textBox_FileToDelete.ReadOnly = true;
            this.textBox_FileToDelete.Size = new System.Drawing.Size(545, 20);
            this.textBox_FileToDelete.TabIndex = 2;
            this.textBox_FileToDelete.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox_ImageInDb);
            this.groupBox2.Location = new System.Drawing.Point(293, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(270, 293);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image In DB";
            // 
            // pictureBox_ImageInDb
            // 
            this.pictureBox_ImageInDb.Location = new System.Drawing.Point(6, 25);
            this.pictureBox_ImageInDb.Name = "pictureBox_ImageInDb";
            this.pictureBox_ImageInDb.Size = new System.Drawing.Size(256, 256);
            this.pictureBox_ImageInDb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_ImageInDb.TabIndex = 1;
            this.pictureBox_ImageInDb.TabStop = false;
            // 
            // button_No
            // 
            this.button_No.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_No.Location = new System.Drawing.Point(250, 437);
            this.button_No.Margin = new System.Windows.Forms.Padding(2);
            this.button_No.Name = "button_No";
            this.button_No.Size = new System.Drawing.Size(73, 28);
            this.button_No.TabIndex = 6;
            this.button_No.Text = "&No";
            this.button_No.UseVisualStyleBackColor = true;
            this.button_No.Click += new System.EventHandler(this.button_No_Click);
            // 
            // button_Yes
            // 
            this.button_Yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Yes.Location = new System.Drawing.Point(181, 438);
            this.button_Yes.Margin = new System.Windows.Forms.Padding(2);
            this.button_Yes.Name = "button_Yes";
            this.button_Yes.Size = new System.Drawing.Size(65, 26);
            this.button_Yes.TabIndex = 5;
            this.button_Yes.Text = "&Yes";
            this.button_Yes.UseVisualStyleBackColor = true;
            this.button_Yes.Click += new System.EventHandler(this.button_Yes_Click);
            // 
            // button_CancelAll
            // 
            this.button_CancelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_CancelAll.Location = new System.Drawing.Point(327, 438);
            this.button_CancelAll.Margin = new System.Windows.Forms.Padding(2);
            this.button_CancelAll.Name = "button_CancelAll";
            this.button_CancelAll.Size = new System.Drawing.Size(114, 26);
            this.button_CancelAll.TabIndex = 7;
            this.button_CancelAll.Text = "&Cancel All";
            this.button_CancelAll.UseVisualStyleBackColor = true;
            this.button_CancelAll.Click += new System.EventHandler(this.button_CancelAll_Click);
            // 
            // Form_ImageDeletionPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 477);
            this.Controls.Add(this.textBox_FileToDelete);
            this.Controls.Add(this.button_CancelAll);
            this.Controls.Add(this.button_No);
            this.Controls.Add(this.button_Yes);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_Msg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_ImageDeletionPrompt";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delete Image File?";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_FileToDelete)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ImageInDb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Msg;
        private System.Windows.Forms.PictureBox pictureBox_FileToDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox_ImageInDb;
        private System.Windows.Forms.Button button_No;
        private System.Windows.Forms.Button button_Yes;
        private System.Windows.Forms.Button button_CancelAll;
        private System.Windows.Forms.TextBox textBox_FileToDelete;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}