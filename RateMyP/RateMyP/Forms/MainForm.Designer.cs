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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.mainPageButton = new System.Windows.Forms.Button();
            this.browserPageButton = new System.Windows.Forms.Button();
            this.ratePageButton = new System.Windows.Forms.Button();
            this.leaderboardsPageButton = new System.Windows.Forms.Button();
            this.profilePageButton = new System.Windows.Forms.Button();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.leaderboardPage = new RateMyP.Forms.UserControls.LeaderboardPage();
            this.ratePage = new RateMyP.Forms.UserControls.RatePage();
            this.landingPage = new RateMyP.Forms.UserControls.LandingPage();
            this.browsePage = new RateMyP.Forms.UserControls.BrowsePage();
            this.profilePage = new RateMyP.Forms.UserControls.ProfilePage();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 669F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pagePanel, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 561);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.mainPageButton);
            this.flowLayoutPanel1.Controls.Add(this.browserPageButton);
            this.flowLayoutPanel1.Controls.Add(this.ratePageButton);
            this.flowLayoutPanel1.Controls.Add(this.leaderboardsPageButton);
            this.flowLayoutPanel1.Controls.Add(this.profilePageButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(109, 555);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // mainPageButton
            // 
            this.mainPageButton.Location = new System.Drawing.Point(3, 3);
            this.mainPageButton.Name = "mainPageButton";
            this.mainPageButton.Size = new System.Drawing.Size(100, 100);
            this.mainPageButton.TabIndex = 2;
            this.mainPageButton.Text = "Main";
            this.mainPageButton.UseVisualStyleBackColor = true;
            this.mainPageButton.Click += new System.EventHandler(this.MainMenuPageButton_Click);
            // 
            // browserPageButton
            // 
            this.browserPageButton.Location = new System.Drawing.Point(3, 109);
            this.browserPageButton.Name = "browserPageButton";
            this.browserPageButton.Size = new System.Drawing.Size(100, 100);
            this.browserPageButton.TabIndex = 3;
            this.browserPageButton.Text = "Browse";
            this.browserPageButton.UseVisualStyleBackColor = true;
            this.browserPageButton.Click += new System.EventHandler(this.BrowsePageButton_Click);
            // 
            // ratePageButton
            // 
            this.ratePageButton.Location = new System.Drawing.Point(3, 215);
            this.ratePageButton.Name = "ratePageButton";
            this.ratePageButton.Size = new System.Drawing.Size(100, 100);
            this.ratePageButton.TabIndex = 0;
            this.ratePageButton.Text = "Rate";
            this.ratePageButton.UseVisualStyleBackColor = true;
            this.ratePageButton.Click += new System.EventHandler(this.RatePageButton_Click);
            // 
            // leaderboardsPageButton
            // 
            this.leaderboardsPageButton.Location = new System.Drawing.Point(3, 321);
            this.leaderboardsPageButton.Name = "leaderboardsPageButton";
            this.leaderboardsPageButton.Size = new System.Drawing.Size(100, 100);
            this.leaderboardsPageButton.TabIndex = 1;
            this.leaderboardsPageButton.Text = "Leaderboards";
            this.leaderboardsPageButton.UseVisualStyleBackColor = true;
            this.leaderboardsPageButton.Click += new System.EventHandler(this.LeaderboardsPageButton_Click);
            // 
            // profilePageButton
            // 
            this.profilePageButton.Location = new System.Drawing.Point(3, 427);
            this.profilePageButton.Name = "profilePageButton";
            this.profilePageButton.Size = new System.Drawing.Size(100, 100);
            this.profilePageButton.TabIndex = 4;
            this.profilePageButton.Text = "Profile";
            this.profilePageButton.UseVisualStyleBackColor = true;
            this.profilePageButton.Click += new System.EventHandler(this.ProfilePageButton_Click);
            // 
            // pagePanel
            // 
            this.pagePanel.Controls.Add(this.profilePage);
            this.pagePanel.Controls.Add(this.leaderboardPage);
            this.pagePanel.Controls.Add(this.ratePage);
            this.pagePanel.Controls.Add(this.landingPage);
            this.pagePanel.Controls.Add(this.browsePage);
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.Location = new System.Drawing.Point(118, 3);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(663, 555);
            this.pagePanel.TabIndex = 1;
            // 
            // leaderboardPage
            // 
            this.leaderboardPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leaderboardPage.Location = new System.Drawing.Point(0, 0);
            this.leaderboardPage.Name = "leaderboardPage";
            this.leaderboardPage.Size = new System.Drawing.Size(663, 555);
            this.leaderboardPage.TabIndex = 5;
            // 
            // ratePage
            // 
            this.ratePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ratePage.Location = new System.Drawing.Point(0, 0);
            this.ratePage.Name = "ratePage";
            this.ratePage.Size = new System.Drawing.Size(663, 555);
            this.ratePage.TabIndex = 4;
            // 
            // landingPage
            // 
            this.landingPage.Location = new System.Drawing.Point(3, 0);
            this.landingPage.Name = "landingPage";
            this.landingPage.Size = new System.Drawing.Size(663, 555);
            this.landingPage.TabIndex = 1;
            // 
            // browsePage
            // 
            this.browsePage.BackColor = System.Drawing.SystemColors.Control;
            this.browsePage.Location = new System.Drawing.Point(3, 4);
            this.browsePage.Name = "browsePage";
            this.browsePage.Size = new System.Drawing.Size(663, 555);
            this.browsePage.TabIndex = 0;
            // 
            // profilePage
            // 
            this.profilePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profilePage.Location = new System.Drawing.Point(0, 0);
            this.profilePage.Name = "profilePage";
            this.profilePage.Size = new System.Drawing.Size(663, 555);
            this.profilePage.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pagePanel.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button browserPageButton;
        private System.Windows.Forms.Button ratePageButton;
        private System.Windows.Forms.Button leaderboardsPageButton;
        private System.Windows.Forms.Button mainPageButton;
        private System.Windows.Forms.Panel pagePanel;
        private Forms.UserControls.LandingPage landingPage;
        private Forms.UserControls.BrowsePage browsePage;
        public Forms.UserControls.RatePage ratePage;
        private Forms.UserControls.LeaderboardPage leaderboardPage;
        private System.Windows.Forms.Button profilePageButton;
        public Forms.UserControls.ProfilePage profilePage;
    }
    }

