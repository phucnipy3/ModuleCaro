namespace MultiConnections
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
            this.PnlPlayerList = new System.Windows.Forms.FlowLayoutPanel();
            this.timerCreateEmptyPlayer = new System.Windows.Forms.Timer(this.components);
            this.timerRefreshListPlayer = new System.Windows.Forms.Timer(this.components);
            this.timerCheckForConnection = new System.Windows.Forms.Timer(this.components);
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.btnNewMatch = new System.Windows.Forms.Button();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PnlPlayerList
            // 
            this.PnlPlayerList.Dock = System.Windows.Forms.DockStyle.Right;
            this.PnlPlayerList.Location = new System.Drawing.Point(636, 0);
            this.PnlPlayerList.Margin = new System.Windows.Forms.Padding(10);
            this.PnlPlayerList.Name = "PnlPlayerList";
            this.PnlPlayerList.Padding = new System.Windows.Forms.Padding(10);
            this.PnlPlayerList.Size = new System.Drawing.Size(140, 450);
            this.PnlPlayerList.TabIndex = 0;
            // 
            // timerCreateEmptyPlayer
            // 
            this.timerCreateEmptyPlayer.Enabled = true;
            this.timerCreateEmptyPlayer.Interval = 10;
            this.timerCreateEmptyPlayer.Tick += new System.EventHandler(this.timerCreateEmptyPlayer_Tick);
            // 
            // timerRefreshListPlayer
            // 
            this.timerRefreshListPlayer.Enabled = true;
            this.timerRefreshListPlayer.Tick += new System.EventHandler(this.timerRefreshListPlayer_Tick);
            // 
            // timerCheckForConnection
            // 
            this.timerCheckForConnection.Enabled = true;
            this.timerCheckForConnection.Interval = 1000;
            this.timerCheckForConnection.Tick += new System.EventHandler(this.timerCheckForConnection_Tick);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Location = new System.Drawing.Point(12, 97);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(616, 341);
            this.pnlContainer.TabIndex = 1;
            // 
            // btnNewMatch
            // 
            this.btnNewMatch.Location = new System.Drawing.Point(28, 23);
            this.btnNewMatch.Name = "btnNewMatch";
            this.btnNewMatch.Size = new System.Drawing.Size(77, 31);
            this.btnNewMatch.TabIndex = 2;
            this.btnNewMatch.Text = "New match";
            this.btnNewMatch.UseVisualStyleBackColor = true;
            this.btnNewMatch.Click += new System.EventHandler(this.btnNewMatch_Click);
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(319, 41);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(35, 13);
            this.lblIPAddress.TabIndex = 3;
            this.lblIPAddress.Text = "label1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(546, 54);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(77, 28);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.btnNewMatch);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.PnlPlayerList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel PnlPlayerList;
        private System.Windows.Forms.Timer timerCreateEmptyPlayer;
        private System.Windows.Forms.Timer timerRefreshListPlayer;
        private System.Windows.Forms.Timer timerCheckForConnection;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Button btnNewMatch;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Button btnRefresh;
    }
}

