namespace ClassLibraryServer
{
    partial class UCMatch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNameOfPlayer2 = new System.Windows.Forms.Label();
            this.lblNameOfPlayer1 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.ptbAvatar2 = new System.Windows.Forms.PictureBox();
            this.ptbAvatar1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnByGame = new System.Windows.Forms.Button();
            this.btnByMatch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNameOfPlayer2
            // 
            this.lblNameOfPlayer2.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfPlayer2.Location = new System.Drawing.Point(149, 93);
            this.lblNameOfPlayer2.Name = "lblNameOfPlayer2";
            this.lblNameOfPlayer2.Size = new System.Drawing.Size(73, 14);
            this.lblNameOfPlayer2.TabIndex = 3;
            this.lblNameOfPlayer2.Text = "Player\'s Name";
            this.lblNameOfPlayer2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameOfPlayer1
            // 
            this.lblNameOfPlayer1.Font = new System.Drawing.Font("Times New Roman", 5.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfPlayer1.Location = new System.Drawing.Point(-2, 93);
            this.lblNameOfPlayer1.Name = "lblNameOfPlayer1";
            this.lblNameOfPlayer1.Size = new System.Drawing.Size(73, 14);
            this.lblNameOfPlayer1.TabIndex = 4;
            this.lblNameOfPlayer1.Text = "Player\'s Name";
            this.lblNameOfPlayer1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Location = new System.Drawing.Point(77, 46);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(66, 61);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // ptbAvatar2
            // 
            this.ptbAvatar2.Location = new System.Drawing.Point(168, 60);
            this.ptbAvatar2.Name = "ptbAvatar2";
            this.ptbAvatar2.Size = new System.Drawing.Size(30, 30);
            this.ptbAvatar2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbAvatar2.TabIndex = 6;
            this.ptbAvatar2.TabStop = false;
            // 
            // ptbAvatar1
            // 
            this.ptbAvatar1.Location = new System.Drawing.Point(20, 60);
            this.ptbAvatar1.Name = "ptbAvatar1";
            this.ptbAvatar1.Size = new System.Drawing.Size(30, 30);
            this.ptbAvatar1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbAvatar1.TabIndex = 7;
            this.ptbAvatar1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(209, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(12, 10);
            this.btnClose.TabIndex = 8;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(191, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(12, 10);
            this.button2.TabIndex = 8;
            this.button2.Text = "button1";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnByGame
            // 
            this.btnByGame.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnByGame.Location = new System.Drawing.Point(43, 118);
            this.btnByGame.Name = "btnByGame";
            this.btnByGame.Size = new System.Drawing.Size(60, 21);
            this.btnByGame.TabIndex = 9;
            this.btnByGame.Text = "1 Game";
            this.btnByGame.UseVisualStyleBackColor = true;
            this.btnByGame.Click += new System.EventHandler(this.btnByGame_Click);
            // 
            // btnByMatch
            // 
            this.btnByMatch.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnByMatch.Location = new System.Drawing.Point(121, 118);
            this.btnByMatch.Name = "btnByMatch";
            this.btnByMatch.Size = new System.Drawing.Size(60, 21);
            this.btnByMatch.TabIndex = 9;
            this.btnByMatch.Text = "All Game";
            this.btnByMatch.UseVisualStyleBackColor = true;
            this.btnByMatch.Click += new System.EventHandler(this.btnByMatch_Click);
            // 
            // UCMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Controls.Add(this.btnByMatch);
            this.Controls.Add(this.btnByGame);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblNameOfPlayer2);
            this.Controls.Add(this.lblNameOfPlayer1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.ptbAvatar2);
            this.Controls.Add(this.ptbAvatar1);
            this.Name = "UCMatch";
            this.Size = new System.Drawing.Size(225, 156);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNameOfPlayer2;
        private System.Windows.Forms.Label lblNameOfPlayer1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox ptbAvatar2;
        private System.Windows.Forms.PictureBox ptbAvatar1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnByGame;
        private System.Windows.Forms.Button btnByMatch;
    }
}
