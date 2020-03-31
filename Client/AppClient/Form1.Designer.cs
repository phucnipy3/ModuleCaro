namespace AppClient
{
    partial class Form1
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
            this.pnlConnectServer = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnectAndSendName = new System.Windows.Forms.Button();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.rtxtNotification = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUploadImage = new System.Windows.Forms.Label();
            this.ptbAvatar = new System.Windows.Forms.PictureBox();
            this.timerCheckServerFound = new System.Windows.Forms.Timer(this.components);
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.pnlConnectServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlConnectServer
            // 
            this.pnlConnectServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConnectServer.Controls.Add(this.lblStatus);
            this.pnlConnectServer.Controls.Add(this.label5);
            this.pnlConnectServer.Controls.Add(this.btnConnectAndSendName);
            this.pnlConnectServer.Controls.Add(this.txtServerIP);
            this.pnlConnectServer.Controls.Add(this.label3);
            this.pnlConnectServer.Location = new System.Drawing.Point(14, 98);
            this.pnlConnectServer.Name = "pnlConnectServer";
            this.pnlConnectServer.Size = new System.Drawing.Size(374, 83);
            this.pnlConnectServer.TabIndex = 9;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblStatus.Location = new System.Drawing.Point(74, 55);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(78, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "No Connection";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Status:";
            // 
            // btnConnectAndSendName
            // 
            this.btnConnectAndSendName.Location = new System.Drawing.Point(281, 50);
            this.btnConnectAndSendName.Name = "btnConnectAndSendName";
            this.btnConnectAndSendName.Size = new System.Drawing.Size(75, 23);
            this.btnConnectAndSendName.TabIndex = 2;
            this.btnConnectAndSendName.Text = "Connect";
            this.btnConnectAndSendName.UseVisualStyleBackColor = true;
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(119, 23);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(237, 20);
            this.txtServerIP.TabIndex = 1;
            this.txtServerIP.Text = "192.168.1.86";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Server\'s IPAddress:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Your name:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(92, 55);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(296, 20);
            this.txtName.TabIndex = 7;
            this.txtName.Text = "phucni";
            // 
            // rtxtNotification
            // 
            this.rtxtNotification.Location = new System.Drawing.Point(17, 219);
            this.rtxtNotification.Name = "rtxtNotification";
            this.rtxtNotification.Size = new System.Drawing.Size(371, 129);
            this.rtxtNotification.TabIndex = 12;
            this.rtxtNotification.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Server\'s Notification:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Connect to Server";
            // 
            // lblUploadImage
            // 
            this.lblUploadImage.BackColor = System.Drawing.Color.Transparent;
            this.lblUploadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUploadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblUploadImage.Location = new System.Drawing.Point(17, 43);
            this.lblUploadImage.Name = "lblUploadImage";
            this.lblUploadImage.Size = new System.Drawing.Size(63, 29);
            this.lblUploadImage.TabIndex = 14;
            this.lblUploadImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUploadImage.Visible = false;
            this.lblUploadImage.Click += new System.EventHandler(this.lblUploadImage_Click);
            this.lblUploadImage.MouseEnter += new System.EventHandler(this.lblUploadImage_MouseEnter);
            this.lblUploadImage.MouseLeave += new System.EventHandler(this.lblUploadImage_MouseLeave);
            // 
            // ptbAvatar
            // 
            this.ptbAvatar.BackColor = System.Drawing.Color.Transparent;
            this.ptbAvatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ptbAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbAvatar.Location = new System.Drawing.Point(14, 6);
            this.ptbAvatar.Name = "ptbAvatar";
            this.ptbAvatar.Size = new System.Drawing.Size(69, 69);
            this.ptbAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbAvatar.TabIndex = 13;
            this.ptbAvatar.TabStop = false;
            this.ptbAvatar.MouseEnter += new System.EventHandler(this.ptbAvatar_MouseEnter);
            // 
            // timerCheckServerFound
            // 
            this.timerCheckServerFound.Enabled = true;
            this.timerCheckServerFound.Interval = 10;
            this.timerCheckServerFound.Tick += new System.EventHandler(this.timerCheckServerFound_Tick);
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblServerStatus.Location = new System.Drawing.Point(72, 184);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(92, 13);
            this.lblServerStatus.TabIndex = 4;
            this.lblServerStatus.Text = "Looking for server";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 359);
            this.Controls.Add(this.lblServerStatus);
            this.Controls.Add(this.lblUploadImage);
            this.Controls.Add(this.pnlConnectServer);
            this.Controls.Add(this.ptbAvatar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.rtxtNotification);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.pnlConnectServer.ResumeLayout(false);
            this.pnlConnectServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlConnectServer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnectAndSendName;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.RichTextBox rtxtNotification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUploadImage;
        private System.Windows.Forms.PictureBox ptbAvatar;
        private System.Windows.Forms.Timer timerCheckServerFound;
        private System.Windows.Forms.Label lblServerStatus;
    }
}

