using System.Drawing;

namespace RateMyP.WinForm.Forms
    {
    partial class RateMyProfessor
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
            this.MenuTabControl = new MetroSet_UI.Controls.MetroSetTabControl();
            this.TabPageHome = new System.Windows.Forms.TabPage();
            this.TabPageBrowse = new System.Windows.Forms.TabPage();
            this.TabPageLeaderboards = new System.Windows.Forms.TabPage();
            this.TabPageUserProfile = new System.Windows.Forms.TabPage();
            this.TabPageSettings = new System.Windows.Forms.TabPage();
            this.TabPageTeacherProfile = new System.Windows.Forms.TabPage();
            this.browsePageControl = new RateMyP.WinForm.Forms.UserControls.BrowsePageControl();
            this.homePageControl = new RateMyP.WinForm.Forms.UserControls.HomePageControl();
            this.teacherProfilePageControl = new RateMyP.WinForm.Forms.UserControls.TeacherProfilePageControl();
            this.userProfilePageControl = new RateMyP.WinForm.Forms.UserControls.UserProfilePageControl();
            this.settingsPageControl = new RateMyP.WinForm.Forms.UserControls.SettingsPageControl();
            this.leaderboardsPageControl = new RateMyP.WinForm.Forms.UserControls.LeaderboardsPageControl();
            this.TabContextMenu = new MetroSet_UI.Controls.MetroSetContextMenuStrip();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTabControl.SuspendLayout();
            this.TabPageBrowse.SuspendLayout();
            this.TabPageTeacherProfile.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuTabControl
            // 
            this.MenuTabControl.Controls.Add(this.TabPageHome);
            this.MenuTabControl.Controls.Add(this.TabPageBrowse);
            this.MenuTabControl.Controls.Add(this.TabPageLeaderboards);
            this.MenuTabControl.Controls.Add(this.TabPageUserProfile);
            this.MenuTabControl.Controls.Add(this.TabPageSettings);
            this.MenuTabControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.MenuTabControl.ItemSize = new System.Drawing.Size(100, 38);
            this.MenuTabControl.Location = new System.Drawing.Point(15, 73);
            this.MenuTabControl.Name = "MenuTabControl";
            this.MenuTabControl.SelectedIndex = 0;
            this.MenuTabControl.Size = new System.Drawing.Size(1250, 632);
            this.MenuTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MenuTabControl.Speed = 100;
            //this.MenuTabControl.Style = MetroSet_UI.Design.Style.Light;
            this.MenuTabControl.StyleManager = null;
            this.MenuTabControl.TabIndex = 0;
            this.MenuTabControl.TabStyle = MetroSet_UI.Enums.TabStyle.Style1;
            this.MenuTabControl.ThemeAuthor = "Narwin";
            this.MenuTabControl.ThemeName = "MetroLite";
            this.MenuTabControl.UseAnimation = true;
            //this.MenuTabControl.MouseHover += new System.EventHandler(this.MenuTabControl_MouseHover);
            this.MenuTabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MenuTabControl_MouseClick);
            // 
            // TabPageHome
            // 
            this.TabPageHome.Controls.Add(this.homePageControl);
            this.TabPageHome.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TabPageHome.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.TabPageHome.Location = new System.Drawing.Point(4, 42);
            this.TabPageHome.Name = "TabPageHome";
            this.TabPageHome.Size = new System.Drawing.Size(1276, 678);
            this.TabPageHome.TabIndex = 0;
            this.TabPageHome.Text = "Home";
            // 
            // TabPageBrowse
            // 
            this.TabPageBrowse.Controls.Add(this.browsePageControl);
            this.TabPageBrowse.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TabPageBrowse.Location = new System.Drawing.Point(4, 42);
            this.TabPageBrowse.Name = "TabPageBrowse";
            this.TabPageBrowse.Size = new System.Drawing.Size(1242, 586);
            this.TabPageBrowse.TabIndex = 1;
            this.TabPageBrowse.Text = "Browse";
            // 
            // TabPageLeaderboards
            // 
            this.TabPageLeaderboards.Controls.Add(this.leaderboardsPageControl);
            this.TabPageLeaderboards.Location = new System.Drawing.Point(4, 42);
            this.TabPageLeaderboards.Name = "TabPageLeaderboards";
            this.TabPageLeaderboards.Size = new System.Drawing.Size(1242, 586);
            this.TabPageLeaderboards.TabIndex = 2;
            this.TabPageLeaderboards.Text = "Leaderboards";
            // 
            // TabPageUserProfile
            // 
            this.TabPageUserProfile.Controls.Add(this.userProfilePageControl);
            this.TabPageUserProfile.Location = new System.Drawing.Point(4, 42);
            this.TabPageUserProfile.Name = "TabPageUserProfile";
            this.TabPageUserProfile.Size = new System.Drawing.Size(1242, 586);
            this.TabPageUserProfile.TabIndex = 3;
            this.TabPageUserProfile.Text = "User Profile";
            // 
            // TabPageSettings
            // 
            this.TabPageSettings.Controls.Add(this.settingsPageControl);
            this.TabPageSettings.Location = new System.Drawing.Point(4, 42);
            this.TabPageSettings.Name = "TabPageSettings";
            this.TabPageSettings.Size = new System.Drawing.Size(1242, 586);
            this.TabPageSettings.TabIndex = 4;
            this.TabPageSettings.Text = "Settings";
            //
            // TabPageTeacherProfile
            this.TabPageTeacherProfile.Controls.Add(this.teacherProfilePageControl);
            this.TabPageTeacherProfile.Location = new System.Drawing.Point(4, 42);
            this.TabPageTeacherProfile.Name = "TabPageTeacherProfile";
            this.TabPageTeacherProfile.Size = new System.Drawing.Size(1242, 586);
            this.TabPageTeacherProfile.TabIndex = 5;
            this.TabPageTeacherProfile.Text = "Teacher Profile";
            //
            // homePageControl
            //
            this.browsePageControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.browsePageControl.Location = new System.Drawing.Point(0, 0);
            this.browsePageControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.browsePageControl.Name = "homePageControl";
            this.browsePageControl.Size = new System.Drawing.Size(1200, 600);
            this.browsePageControl.TabIndex = 0;
            // 
            // browsePageControl
            // 
            this.browsePageControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.browsePageControl.Location = new System.Drawing.Point(0, 0);
            this.browsePageControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.browsePageControl.Name = "browsePageControl";
            this.browsePageControl.Size = new System.Drawing.Size(1200, 600);
            this.browsePageControl.TabIndex = 1;
            //
            // leaderboardsPageControl
            //
            this.leaderboardsPageControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.leaderboardsPageControl.Location = new System.Drawing.Point(0, 0);
            this.leaderboardsPageControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.leaderboardsPageControl.Name = "leaderboardsPageControl";
            this.leaderboardsPageControl.Size = new System.Drawing.Size(1200, 600);
            this.leaderboardsPageControl.TabIndex = 2;
            //
            // userProfilePageControl
            //
            this.browsePageControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.browsePageControl.Location = new System.Drawing.Point(0, 0);
            this.browsePageControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.browsePageControl.Name = "userProfilePageControl";
            this.browsePageControl.Size = new System.Drawing.Size(1200, 600);
            this.browsePageControl.TabIndex = 3;
            //
            // settingsPageControl
            // 
            this.settingsPageControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.settingsPageControl.Location = new System.Drawing.Point(0, 0);
            this.settingsPageControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.settingsPageControl.Name = "settingsPageControl";
            this.settingsPageControl.Size = new System.Drawing.Size(1200, 600);
            this.settingsPageControl.TabIndex = 4;
            // 
            // teacherProfilePageControl
            // 
            this.teacherProfilePageControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.teacherProfilePageControl.Location = new System.Drawing.Point(0, 0);
            this.teacherProfilePageControl.Margin = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.teacherProfilePageControl.Name = "teacherProfilePageControl";
            this.teacherProfilePageControl.Size = new System.Drawing.Size(1200, 600);
            //
            // TabContextMenu
            //
            this.TabContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.TabContextMenu.Name = "metroSetContextMenuStrip1";
            this.TabContextMenu.Size = new System.Drawing.Size(181, 48);
            this.TabContextMenu.Style = MetroSet_UI.Design.Style.Light;
            this.TabContextMenu.StyleManager = null;
            this.TabContextMenu.ThemeAuthor = "Narwin";
            this.TabContextMenu.ThemeName = "MetroLite";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStrip_Click);
            //
            // RateMyProfessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.MenuTabControl);
            this.Name = "RateMyProfessor";
            this.Text = "RateMyProfessor";
            this.MenuTabControl.ResumeLayout(false);
            this.TabPageBrowse.ResumeLayout(false);
            this.ResumeLayout(false);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            }

        #endregion
        public MetroSet_UI.Controls.MetroSetTabControl MenuTabControl;
        private System.Windows.Forms.TabPage TabPageHome;
        private System.Windows.Forms.TabPage TabPageBrowse;
        public System.Windows.Forms.TabPage TabPageLeaderboards;
        public System.Windows.Forms.TabPage TabPageUserProfile;
        public System.Windows.Forms.TabPage TabPageSettings;
        public System.Windows.Forms.TabPage TabPageTeacherProfile;

        private UserControls.BrowsePageControl browsePageControl;
        private UserControls.HomePageControl homePageControl;
        private UserControls.UserProfilePageControl userProfilePageControl;
        private UserControls.SettingsPageControl settingsPageControl;
        private UserControls.LeaderboardsPageControl leaderboardsPageControl;
        public UserControls.TeacherProfilePageControl teacherProfilePageControl;

        private MetroSet_UI.Controls.MetroSetContextMenuStrip TabContextMenu;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        }
    }