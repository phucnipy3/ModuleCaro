namespace MultiConnections
{
    partial class FormCreateMatch
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
            this.PnlPlayerList = new System.Windows.Forms.FlowLayoutPanel();
            this.timerRefreshListPlayer = new System.Windows.Forms.Timer(this.components);
            this.ptbAvatar1 = new System.Windows.Forms.PictureBox();
            this.ptbAvatar2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnGoOn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numBestOf = new System.Windows.Forms.NumericUpDown();
            this.chkRule = new System.Windows.Forms.CheckBox();
            this.lblNameOfPlayer1 = new System.Windows.Forms.Label();
            this.lblNameOfPlayer2 = new System.Windows.Forms.Label();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.ptbFirstPlay1 = new System.Windows.Forms.PictureBox();
            this.ptbFirstPlay2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBestOf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFirstPlay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFirstPlay2)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlPlayerList
            // 
            this.PnlPlayerList.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlPlayerList.Location = new System.Drawing.Point(356, 0);
            this.PnlPlayerList.Margin = new System.Windows.Forms.Padding(10);
            this.PnlPlayerList.Name = "PnlPlayerList";
            this.PnlPlayerList.Padding = new System.Windows.Forms.Padding(10);
            this.PnlPlayerList.Size = new System.Drawing.Size(140, 326);
            this.PnlPlayerList.TabIndex = 1;
            // 
            // timerRefreshListPlayer
            // 
            this.timerRefreshListPlayer.Enabled = true;
            this.timerRefreshListPlayer.Interval = 10;
            this.timerRefreshListPlayer.Tick += new System.EventHandler(this.timerRefreshListPlayer_Tick);
            // 
            // ptbAvatar1
            // 
            this.ptbAvatar1.Location = new System.Drawing.Point(23, 149);
            this.ptbAvatar1.Name = "ptbAvatar1";
            this.ptbAvatar1.Size = new System.Drawing.Size(70, 70);
            this.ptbAvatar1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbAvatar1.TabIndex = 2;
            this.ptbAvatar1.TabStop = false;
            // 
            // ptbAvatar2
            // 
            this.ptbAvatar2.Location = new System.Drawing.Point(254, 149);
            this.ptbAvatar2.Name = "ptbAvatar2";
            this.ptbAvatar2.Size = new System.Drawing.Size(70, 70);
            this.ptbAvatar2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbAvatar2.TabIndex = 2;
            this.ptbAvatar2.TabStop = false;
            this.ptbAvatar2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(120, 133);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(106, 101);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnGoOn
            // 
            this.btnGoOn.Location = new System.Drawing.Point(130, 275);
            this.btnGoOn.Name = "btnGoOn";
            this.btnGoOn.Size = new System.Drawing.Size(86, 28);
            this.btnGoOn.TabIndex = 3;
            this.btnGoOn.Text = "Go on";
            this.btnGoOn.UseVisualStyleBackColor = true;
            this.btnGoOn.Click += new System.EventHandler(this.btnGoOn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Best of";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // numBestOf
            // 
            this.numBestOf.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numBestOf.Location = new System.Drawing.Point(107, 24);
            this.numBestOf.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numBestOf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBestOf.Name = "numBestOf";
            this.numBestOf.Size = new System.Drawing.Size(31, 20);
            this.numBestOf.TabIndex = 5;
            this.numBestOf.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkRule
            // 
            this.chkRule.AutoSize = true;
            this.chkRule.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRule.Font = new System.Drawing.Font("Rockwell", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRule.Location = new System.Drawing.Point(19, 57);
            this.chkRule.Name = "chkRule";
            this.chkRule.Size = new System.Drawing.Size(232, 29);
            this.chkRule.TabIndex = 6;
            this.chkRule.Text = "Block two ends rule";
            this.chkRule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRule.UseVisualStyleBackColor = true;
            // 
            // lblNameOfPlayer1
            // 
            this.lblNameOfPlayer1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfPlayer1.Location = new System.Drawing.Point(1, 219);
            this.lblNameOfPlayer1.Name = "lblNameOfPlayer1";
            this.lblNameOfPlayer1.Size = new System.Drawing.Size(113, 48);
            this.lblNameOfPlayer1.TabIndex = 1;
            this.lblNameOfPlayer1.Text = "Player\'s Name";
            this.lblNameOfPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameOfPlayer2
            // 
            this.lblNameOfPlayer2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfPlayer2.Location = new System.Drawing.Point(232, 219);
            this.lblNameOfPlayer2.Name = "lblNameOfPlayer2";
            this.lblNameOfPlayer2.Size = new System.Drawing.Size(113, 48);
            this.lblNameOfPlayer2.TabIndex = 1;
            this.lblNameOfPlayer2.Text = "Player\'s Name";
            this.lblNameOfPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitch.Location = new System.Drawing.Point(96, 92);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(149, 30);
            this.btnSwitch.TabIndex = 7;
            this.btnSwitch.Text = "Switch first player";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // ptbFirstPlay1
            // 
            this.ptbFirstPlay1.BackColor = System.Drawing.Color.Lime;
            this.ptbFirstPlay1.Location = new System.Drawing.Point(23, 149);
            this.ptbFirstPlay1.Name = "ptbFirstPlay1";
            this.ptbFirstPlay1.Size = new System.Drawing.Size(20, 20);
            this.ptbFirstPlay1.TabIndex = 8;
            this.ptbFirstPlay1.TabStop = false;
            // 
            // ptbFirstPlay2
            // 
            this.ptbFirstPlay2.BackColor = System.Drawing.Color.Lime;
            this.ptbFirstPlay2.Location = new System.Drawing.Point(254, 149);
            this.ptbFirstPlay2.Name = "ptbFirstPlay2";
            this.ptbFirstPlay2.Size = new System.Drawing.Size(20, 20);
            this.ptbFirstPlay2.TabIndex = 8;
            this.ptbFirstPlay2.TabStop = false;
            this.ptbFirstPlay2.Visible = false;
            // 
            // FormCreateMatch
            // 
            this.AcceptButton = this.btnGoOn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 326);
            this.Controls.Add(this.ptbFirstPlay2);
            this.Controls.Add(this.ptbFirstPlay1);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.chkRule);
            this.Controls.Add(this.lblNameOfPlayer2);
            this.Controls.Add(this.lblNameOfPlayer1);
            this.Controls.Add(this.numBestOf);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGoOn);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.ptbAvatar2);
            this.Controls.Add(this.ptbAvatar1);
            this.Controls.Add(this.PnlPlayerList);
            this.Name = "FormCreateMatch";
            this.Text = "FormCreateMatch";
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBestOf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFirstPlay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFirstPlay2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel PnlPlayerList;
        private System.Windows.Forms.Timer timerRefreshListPlayer;
        private System.Windows.Forms.PictureBox ptbAvatar1;
        private System.Windows.Forms.PictureBox ptbAvatar2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnGoOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBestOf;
        private System.Windows.Forms.CheckBox chkRule;
        private System.Windows.Forms.Label lblNameOfPlayer1;
        private System.Windows.Forms.Label lblNameOfPlayer2;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.PictureBox ptbFirstPlay1;
        private System.Windows.Forms.PictureBox ptbFirstPlay2;
    }
}