namespace RateMyP
    {
    partial class MainForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.navigationButtonBrowse = new System.Windows.Forms.ToolStripButton();
            this.navigationButtonRate = new System.Windows.Forms.ToolStripButton();
            this.navigationButtonLeaderboard = new System.Windows.Forms.ToolStripButton();
            this.navigationButtonUserProfile = new System.Windows.Forms.ToolStripButton();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigationButtonBrowse,
            this.navigationButtonRate,
            this.navigationButtonLeaderboard,
            this.navigationButtonUserProfile});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(69, 450);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // navigationButtonBrowse
            // 
            this.navigationButtonBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigationButtonBrowse.Image = global::RateMyP.Properties.Resources.a;
            this.navigationButtonBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigationButtonBrowse.Name = "navigationButtonBrowse";
            this.navigationButtonBrowse.Size = new System.Drawing.Size(66, 68);
            this.navigationButtonBrowse.Text = "navigationButtonBrowse";
            this.navigationButtonBrowse.Click += new System.EventHandler(this.navigationButtonBrowse_Click);
            // 
            // navigationButtonRate
            // 
            this.navigationButtonRate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigationButtonRate.Image = global::RateMyP.Properties.Resources.b;
            this.navigationButtonRate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigationButtonRate.Name = "navigationButtonRate";
            this.navigationButtonRate.Size = new System.Drawing.Size(66, 68);
            this.navigationButtonRate.Text = "navigationButtonRate";
            this.navigationButtonRate.Click += new System.EventHandler(this.navigationButtonRate_Click);
            // 
            // navigationButtonLeaderboard
            // 
            this.navigationButtonLeaderboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigationButtonLeaderboard.Image = global::RateMyP.Properties.Resources.c;
            this.navigationButtonLeaderboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigationButtonLeaderboard.Name = "navigationButtonLeaderboard";
            this.navigationButtonLeaderboard.Size = new System.Drawing.Size(66, 68);
            this.navigationButtonLeaderboard.Text = "toolStripButton7";
            this.navigationButtonLeaderboard.Click += new System.EventHandler(this.navigationButtonLeaderboard_Click);
            // 
            // navigationButtonUserProfile
            // 
            this.navigationButtonUserProfile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.navigationButtonUserProfile.Image = global::RateMyP.Properties.Resources.d;
            this.navigationButtonUserProfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.navigationButtonUserProfile.Name = "navigationButtonUserProfile";
            this.navigationButtonUserProfile.Size = new System.Drawing.Size(66, 68);
            this.navigationButtonUserProfile.Text = "toolStripButton8";
            this.navigationButtonUserProfile.Click += new System.EventHandler(this.navigationButtonUserProfile_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton navigationButtonBrowse;
        private System.Windows.Forms.ToolStripButton navigationButtonRate;
        private System.Windows.Forms.ToolStripButton navigationButtonLeaderboard;
        private System.Windows.Forms.ToolStripButton navigationButtonUserProfile;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        }
    }

