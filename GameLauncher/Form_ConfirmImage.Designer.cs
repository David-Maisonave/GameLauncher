namespace GameLauncher
{
    partial class Form_ConfirmImage
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
            this.label_Instructions = new System.Windows.Forms.Label();
            this.pictureBox_Preview = new System.Windows.Forms.PictureBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_Ok = new System.Windows.Forms.Button();
            this.textBox_ImageFileName = new System.Windows.Forms.TextBox();
            this.label_ImageFile = new System.Windows.Forms.Label();
            this.label_RomFile = new System.Windows.Forms.Label();
            this.textBox_RomFile = new System.Windows.Forms.TextBox();
            this.label_RomTitle = new System.Windows.Forms.Label();
            this.textBox_RomTitle = new System.Windows.Forms.TextBox();
            this.linkLabel_GoogleTitle = new System.Windows.Forms.LinkLabel();
            this.linkLabel_GoogleTitleAndSystem = new System.Windows.Forms.LinkLabel();
            this.linkLabel_GoogleTitleOnLaunchBox = new System.Windows.Forms.LinkLabel();
            this.linkLabel_GoogleTitleImageOnLaunchBox = new System.Windows.Forms.LinkLabel();
            this.linkLabel_SearchOnGamesDatabaseOrg = new System.Windows.Forms.LinkLabel();
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox = new System.Windows.Forms.LinkLabel();
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox = new System.Windows.Forms.LinkLabel();
            this.button_Quit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button_Play = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Instructions
            // 
            this.label_Instructions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Instructions.AutoSize = true;
            this.label_Instructions.Location = new System.Drawing.Point(7, 1);
            this.label_Instructions.MaximumSize = new System.Drawing.Size(610, 48);
            this.label_Instructions.MinimumSize = new System.Drawing.Size(610, 48);
            this.label_Instructions.Name = "label_Instructions";
            this.label_Instructions.Size = new System.Drawing.Size(610, 48);
            this.label_Instructions.TabIndex = 26;
            this.label_Instructions.Text = "Confirm Image";
            // 
            // pictureBox_Preview
            // 
            this.pictureBox_Preview.Location = new System.Drawing.Point(10, 141);
            this.pictureBox_Preview.Name = "pictureBox_Preview";
            this.pictureBox_Preview.Size = new System.Drawing.Size(256, 256);
            this.pictureBox_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Preview.TabIndex = 25;
            this.pictureBox_Preview.TabStop = false;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(275, 405);
            this.button_Cancel.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 28);
            this.button_Cancel.TabIndex = 24;
            this.button_Cancel.Text = "&No";
            this.toolTip1.SetToolTip(this.button_Cancel, "Do not accept this image for this ROM file.");
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_Ok
            // 
            this.button_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Ok.Location = new System.Drawing.Point(187, 405);
            this.button_Ok.Margin = new System.Windows.Forms.Padding(2);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(67, 26);
            this.button_Ok.TabIndex = 23;
            this.button_Ok.Text = "&Yes";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // textBox_ImageFileName
            // 
            this.textBox_ImageFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ImageFileName.Location = new System.Drawing.Point(73, 110);
            this.textBox_ImageFileName.MaximumSize = new System.Drawing.Size(540, 16);
            this.textBox_ImageFileName.MinimumSize = new System.Drawing.Size(540, 16);
            this.textBox_ImageFileName.Name = "textBox_ImageFileName";
            this.textBox_ImageFileName.Size = new System.Drawing.Size(540, 20);
            this.textBox_ImageFileName.TabIndex = 27;
            // 
            // label_ImageFile
            // 
            this.label_ImageFile.AutoSize = true;
            this.label_ImageFile.Location = new System.Drawing.Point(7, 110);
            this.label_ImageFile.MaximumSize = new System.Drawing.Size(60, 16);
            this.label_ImageFile.MinimumSize = new System.Drawing.Size(60, 16);
            this.label_ImageFile.Name = "label_ImageFile";
            this.label_ImageFile.Size = new System.Drawing.Size(60, 16);
            this.label_ImageFile.TabIndex = 28;
            this.label_ImageFile.Text = "Image File";
            // 
            // label_RomFile
            // 
            this.label_RomFile.AutoSize = true;
            this.label_RomFile.Location = new System.Drawing.Point(7, 86);
            this.label_RomFile.MaximumSize = new System.Drawing.Size(60, 16);
            this.label_RomFile.MinimumSize = new System.Drawing.Size(60, 16);
            this.label_RomFile.Name = "label_RomFile";
            this.label_RomFile.Size = new System.Drawing.Size(60, 16);
            this.label_RomFile.TabIndex = 30;
            this.label_RomFile.Text = "Rom File";
            // 
            // textBox_RomFile
            // 
            this.textBox_RomFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_RomFile.Location = new System.Drawing.Point(73, 86);
            this.textBox_RomFile.MaximumSize = new System.Drawing.Size(540, 16);
            this.textBox_RomFile.MinimumSize = new System.Drawing.Size(450, 16);
            this.textBox_RomFile.Name = "textBox_RomFile";
            this.textBox_RomFile.Size = new System.Drawing.Size(460, 16);
            this.textBox_RomFile.TabIndex = 29;
            // 
            // label_RomTitle
            // 
            this.label_RomTitle.AutoSize = true;
            this.label_RomTitle.Location = new System.Drawing.Point(7, 62);
            this.label_RomTitle.MaximumSize = new System.Drawing.Size(60, 16);
            this.label_RomTitle.MinimumSize = new System.Drawing.Size(60, 16);
            this.label_RomTitle.Name = "label_RomTitle";
            this.label_RomTitle.Size = new System.Drawing.Size(60, 16);
            this.label_RomTitle.TabIndex = 32;
            this.label_RomTitle.Text = "Rom Title";
            // 
            // textBox_RomTitle
            // 
            this.textBox_RomTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_RomTitle.Location = new System.Drawing.Point(73, 62);
            this.textBox_RomTitle.MaximumSize = new System.Drawing.Size(540, 16);
            this.textBox_RomTitle.MinimumSize = new System.Drawing.Size(540, 16);
            this.textBox_RomTitle.Name = "textBox_RomTitle";
            this.textBox_RomTitle.Size = new System.Drawing.Size(540, 20);
            this.textBox_RomTitle.TabIndex = 31;
            // 
            // linkLabel_GoogleTitle
            // 
            this.linkLabel_GoogleTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_GoogleTitle.AutoSize = true;
            this.linkLabel_GoogleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_GoogleTitle.Location = new System.Drawing.Point(272, 140);
            this.linkLabel_GoogleTitle.MaximumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitle.MinimumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitle.Name = "linkLabel_GoogleTitle";
            this.linkLabel_GoogleTitle.Size = new System.Drawing.Size(233, 21);
            this.linkLabel_GoogleTitle.TabIndex = 33;
            this.linkLabel_GoogleTitle.Text = "GoogleTitle";
            this.linkLabel_GoogleTitle.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GoogleTitle_LinkClicked);
            // 
            // linkLabel_GoogleTitleAndSystem
            // 
            this.linkLabel_GoogleTitleAndSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_GoogleTitleAndSystem.AutoSize = true;
            this.linkLabel_GoogleTitleAndSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_GoogleTitleAndSystem.Location = new System.Drawing.Point(272, 175);
            this.linkLabel_GoogleTitleAndSystem.MaximumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystem.MinimumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystem.Name = "linkLabel_GoogleTitleAndSystem";
            this.linkLabel_GoogleTitleAndSystem.Size = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystem.TabIndex = 34;
            this.linkLabel_GoogleTitleAndSystem.Text = "Google Title and System Console";
            this.linkLabel_GoogleTitleAndSystem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GoogleTitleAndSystem_LinkClicked);
            // 
            // linkLabel_GoogleTitleOnLaunchBox
            // 
            this.linkLabel_GoogleTitleOnLaunchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_GoogleTitleOnLaunchBox.AutoSize = true;
            this.linkLabel_GoogleTitleOnLaunchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_GoogleTitleOnLaunchBox.Location = new System.Drawing.Point(272, 213);
            this.linkLabel_GoogleTitleOnLaunchBox.MaximumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleOnLaunchBox.MinimumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleOnLaunchBox.Name = "linkLabel_GoogleTitleOnLaunchBox";
            this.linkLabel_GoogleTitleOnLaunchBox.Size = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleOnLaunchBox.TabIndex = 35;
            this.linkLabel_GoogleTitleOnLaunchBox.Text = "Google Title at LaunchBox Site";
            this.linkLabel_GoogleTitleOnLaunchBox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GoogleTitleOnLaunchBox_LinkClicked);
            // 
            // linkLabel_GoogleTitleImageOnLaunchBox
            // 
            this.linkLabel_GoogleTitleImageOnLaunchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_GoogleTitleImageOnLaunchBox.AutoSize = true;
            this.linkLabel_GoogleTitleImageOnLaunchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_GoogleTitleImageOnLaunchBox.Location = new System.Drawing.Point(272, 289);
            this.linkLabel_GoogleTitleImageOnLaunchBox.MaximumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleImageOnLaunchBox.MinimumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleImageOnLaunchBox.Name = "linkLabel_GoogleTitleImageOnLaunchBox";
            this.linkLabel_GoogleTitleImageOnLaunchBox.Size = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleImageOnLaunchBox.TabIndex = 36;
            this.linkLabel_GoogleTitleImageOnLaunchBox.Text = "Google Title Image at LaunchBox Site";
            this.linkLabel_GoogleTitleImageOnLaunchBox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GoogleTitleImageOnLaunchBox_LinkClicked);
            // 
            // linkLabel_SearchOnGamesDatabaseOrg
            // 
            this.linkLabel_SearchOnGamesDatabaseOrg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_SearchOnGamesDatabaseOrg.AutoSize = true;
            this.linkLabel_SearchOnGamesDatabaseOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_SearchOnGamesDatabaseOrg.Location = new System.Drawing.Point(272, 365);
            this.linkLabel_SearchOnGamesDatabaseOrg.MaximumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_SearchOnGamesDatabaseOrg.MinimumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_SearchOnGamesDatabaseOrg.Name = "linkLabel_SearchOnGamesDatabaseOrg";
            this.linkLabel_SearchOnGamesDatabaseOrg.Size = new System.Drawing.Size(350, 32);
            this.linkLabel_SearchOnGamesDatabaseOrg.TabIndex = 37;
            this.linkLabel_SearchOnGamesDatabaseOrg.Text = "Search Title on GamesDatabase.Org";
            this.linkLabel_SearchOnGamesDatabaseOrg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_SearchOnGamesDatabaseOrg_LinkClicked);
            // 
            // linkLabel_GoogleTitleAndSystemImageOnLaunchBox
            // 
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.AutoSize = true;
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.Location = new System.Drawing.Point(272, 327);
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.MaximumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.MinimumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.Name = "linkLabel_GoogleTitleAndSystemImageOnLaunchBox";
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.Size = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.TabIndex = 38;
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.Text = "Google Title and System Console Image at LaunchBox Site";
            this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox_LinkClicked);
            // 
            // linkLabel_GoogleTitleAndSystemOnLaunchBox
            // 
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.AutoSize = true;
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.Location = new System.Drawing.Point(272, 251);
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.MaximumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.MinimumSize = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.Name = "linkLabel_GoogleTitleAndSystemOnLaunchBox";
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.Size = new System.Drawing.Size(350, 32);
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.TabIndex = 39;
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.Text = "Google Title and System Console at LaunchBox Site";
            this.linkLabel_GoogleTitleAndSystemOnLaunchBox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_GoogleTitleAndSystemOnLaunchBox_LinkClicked);
            // 
            // button_Quit
            // 
            this.button_Quit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Quit.Location = new System.Drawing.Point(373, 405);
            this.button_Quit.Margin = new System.Windows.Forms.Padding(2);
            this.button_Quit.Name = "button_Quit";
            this.button_Quit.Size = new System.Drawing.Size(75, 28);
            this.button_Quit.TabIndex = 40;
            this.button_Quit.Text = "&Quit";
            this.toolTip1.SetToolTip(this.button_Quit, "Cancel Search");
            this.button_Quit.UseVisualStyleBackColor = true;
            this.button_Quit.Click += new System.EventHandler(this.button_Quit_Click);
            // 
            // button_Play
            // 
            this.button_Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Play.Location = new System.Drawing.Point(539, 82);
            this.button_Play.Margin = new System.Windows.Forms.Padding(2);
            this.button_Play.MaximumSize = new System.Drawing.Size(75, 32);
            this.button_Play.MinimumSize = new System.Drawing.Size(75, 24);
            this.button_Play.Name = "button_Play";
            this.button_Play.Size = new System.Drawing.Size(75, 24);
            this.button_Play.TabIndex = 41;
            this.button_Play.Text = "&Run";
            this.toolTip1.SetToolTip(this.button_Play, "Cancel Search");
            this.button_Play.UseVisualStyleBackColor = true;
            this.button_Play.Click += new System.EventHandler(this.button_Play_Click);
            // 
            // Form_ConfirmImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.button_Play);
            this.Controls.Add(this.button_Quit);
            this.Controls.Add(this.linkLabel_GoogleTitleAndSystemOnLaunchBox);
            this.Controls.Add(this.linkLabel_GoogleTitleAndSystemImageOnLaunchBox);
            this.Controls.Add(this.linkLabel_SearchOnGamesDatabaseOrg);
            this.Controls.Add(this.linkLabel_GoogleTitleImageOnLaunchBox);
            this.Controls.Add(this.linkLabel_GoogleTitleOnLaunchBox);
            this.Controls.Add(this.linkLabel_GoogleTitleAndSystem);
            this.Controls.Add(this.linkLabel_GoogleTitle);
            this.Controls.Add(this.label_RomTitle);
            this.Controls.Add(this.textBox_RomTitle);
            this.Controls.Add(this.label_RomFile);
            this.Controls.Add(this.textBox_RomFile);
            this.Controls.Add(this.label_ImageFile);
            this.Controls.Add(this.textBox_ImageFileName);
            this.Controls.Add(this.label_Instructions);
            this.Controls.Add(this.pictureBox_Preview);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Ok);
            this.MaximumSize = new System.Drawing.Size(1600, 500);
            this.MinimumSize = new System.Drawing.Size(640, 450);
            this.Name = "Form_ConfirmImage";
            this.Text = "Confirm Image";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Instructions;
        private System.Windows.Forms.PictureBox pictureBox_Preview;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.TextBox textBox_ImageFileName;
        private System.Windows.Forms.Label label_ImageFile;
        private System.Windows.Forms.Label label_RomFile;
        private System.Windows.Forms.TextBox textBox_RomFile;
        private System.Windows.Forms.Label label_RomTitle;
        private System.Windows.Forms.TextBox textBox_RomTitle;
        private System.Windows.Forms.LinkLabel linkLabel_GoogleTitle;
        private System.Windows.Forms.LinkLabel linkLabel_GoogleTitleAndSystem;
        private System.Windows.Forms.LinkLabel linkLabel_GoogleTitleOnLaunchBox;
        private System.Windows.Forms.LinkLabel linkLabel_GoogleTitleImageOnLaunchBox;
        private System.Windows.Forms.LinkLabel linkLabel_SearchOnGamesDatabaseOrg;
        private System.Windows.Forms.LinkLabel linkLabel_GoogleTitleAndSystemImageOnLaunchBox;
        private System.Windows.Forms.LinkLabel linkLabel_GoogleTitleAndSystemOnLaunchBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button_Quit;
        private System.Windows.Forms.Button button_Play;
    }
}