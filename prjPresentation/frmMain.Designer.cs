namespace prjPresentation
{
    partial class frmMain
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
            this.picBoxVideo = new System.Windows.Forms.PictureBox();
            this.btnTakePhoto = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnAzure = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.azureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amazonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCurrentGroup = new System.Windows.Forms.TextBox();
            this.lblAzureGroup = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVideo)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBoxVideo
            // 
            this.picBoxVideo.Location = new System.Drawing.Point(9, 59);
            this.picBoxVideo.Margin = new System.Windows.Forms.Padding(2);
            this.picBoxVideo.Name = "picBoxVideo";
            this.picBoxVideo.Size = new System.Drawing.Size(624, 423);
            this.picBoxVideo.TabIndex = 0;
            this.picBoxVideo.TabStop = false;
            // 
            // btnTakePhoto
            // 
            this.btnTakePhoto.Location = new System.Drawing.Point(11, 506);
            this.btnTakePhoto.Margin = new System.Windows.Forms.Padding(2);
            this.btnTakePhoto.Name = "btnTakePhoto";
            this.btnTakePhoto.Size = new System.Drawing.Size(168, 23);
            this.btnTakePhoto.TabIndex = 1;
            this.btnTakePhoto.Text = "Smile :)";
            this.btnTakePhoto.UseVisualStyleBackColor = true;
            this.btnTakePhoto.Click += new System.EventHandler(this.btnTakePhoto_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(860, 506);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(177, 19);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(665, 59);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(371, 423);
            this.txtResult.TabIndex = 3;
            // 
            // btnAzure
            // 
            this.btnAzure.Location = new System.Drawing.Point(427, 506);
            this.btnAzure.Name = "btnAzure";
            this.btnAzure.Size = new System.Drawing.Size(75, 23);
            this.btnAzure.TabIndex = 4;
            this.btnAzure.Text = "Azure";
            this.btnAzure.UseVisualStyleBackColor = true;
            this.btnAzure.Click += new System.EventHandler(this.btnAzure_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.azureToolStripMenuItem,
            this.amazonToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1048, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // azureToolStripMenuItem
            // 
            this.azureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createGroupToolStripMenuItem,
            this.addPersonToolStripMenuItem,
            this.trainingGroupToolStripMenuItem});
            this.azureToolStripMenuItem.Name = "azureToolStripMenuItem";
            this.azureToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.azureToolStripMenuItem.Text = "Azure";
            // 
            // createGroupToolStripMenuItem
            // 
            this.createGroupToolStripMenuItem.Name = "createGroupToolStripMenuItem";
            this.createGroupToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.createGroupToolStripMenuItem.Text = "Create Group";
            this.createGroupToolStripMenuItem.Click += new System.EventHandler(this.createGroupToolStripMenuItem_Click);
            // 
            // amazonToolStripMenuItem
            // 
            this.amazonToolStripMenuItem.Name = "amazonToolStripMenuItem";
            this.amazonToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.amazonToolStripMenuItem.Text = "Amazon";
            // 
            // addPersonToolStripMenuItem
            // 
            this.addPersonToolStripMenuItem.Name = "addPersonToolStripMenuItem";
            this.addPersonToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.addPersonToolStripMenuItem.Text = "Add Person";
            this.addPersonToolStripMenuItem.Click += new System.EventHandler(this.addPersonToolStripMenuItem_Click);
            // 
            // trainingGroupToolStripMenuItem
            // 
            this.trainingGroupToolStripMenuItem.Name = "trainingGroupToolStripMenuItem";
            this.trainingGroupToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.trainingGroupToolStripMenuItem.Text = "Training Group";
            this.trainingGroupToolStripMenuItem.Click += new System.EventHandler(this.trainingGroupToolStripMenuItem_Click);
            // 
            // txtCurrentGroup
            // 
            this.txtCurrentGroup.Location = new System.Drawing.Point(321, 509);
            this.txtCurrentGroup.Name = "txtCurrentGroup";
            this.txtCurrentGroup.Size = new System.Drawing.Size(100, 20);
            this.txtCurrentGroup.TabIndex = 7;
            // 
            // lblAzureGroup
            // 
            this.lblAzureGroup.AutoSize = true;
            this.lblAzureGroup.Location = new System.Drawing.Point(243, 511);
            this.lblAzureGroup.Name = "lblAzureGroup";
            this.lblAzureGroup.Size = new System.Drawing.Size(69, 13);
            this.lblAzureGroup.TabIndex = 8;
            this.lblAzureGroup.Text = "Azure Group:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 557);
            this.Controls.Add(this.lblAzureGroup);
            this.Controls.Add(this.txtCurrentGroup);
            this.Controls.Add(this.btnAzure);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnTakePhoto);
            this.Controls.Add(this.picBoxVideo);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captue Video";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxVideo)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxVideo;
        private System.Windows.Forms.Button btnTakePhoto;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnAzure;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem azureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amazonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingGroupToolStripMenuItem;
        private System.Windows.Forms.TextBox txtCurrentGroup;
        private System.Windows.Forms.Label lblAzureGroup;
    }
}

