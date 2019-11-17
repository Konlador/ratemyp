namespace RateMyP.WinForm.Forms.UserControls
    {
    partial class UserProfilePageControl
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
            this.UserProfilePictureBox = new System.Windows.Forms.PictureBox();
            this.UserNameLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.UserAcademicInfoLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_RecentActivityArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_UserBioArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_AwardsArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_UserContactInfoArea = new MetroSet_UI.Controls.MetroSetLabel();
            ((System.ComponentModel.ISupportInitialize)(this.UserProfilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // UserProfilePictureBox
            // 
            this.UserProfilePictureBox.Image = global::RateMyP.WinForm.Properties.Resources.profile;
            this.UserProfilePictureBox.Location = new System.Drawing.Point(25, 25);
            this.UserProfilePictureBox.Name = "UserProfilePictureBox";
            this.UserProfilePictureBox.Size = new System.Drawing.Size(200, 200);
            this.UserProfilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UserProfilePictureBox.TabIndex = 0;
            this.UserProfilePictureBox.TabStop = false;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.Location = new System.Drawing.Point(250, 25);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(300, 40);
            this.UserNameLabel.Style = MetroSet_UI.Design.Style.Light;
            this.UserNameLabel.StyleManager = null;
            this.UserNameLabel.TabIndex = 1;
            this.UserNameLabel.Text = "NAME?";
            this.UserNameLabel.ThemeAuthor = "Narwin";
            this.UserNameLabel.ThemeName = "MetroLite";
            // 
            // UserAcademicInfoLabel
            // 
            this.UserAcademicInfoLabel.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserAcademicInfoLabel.Location = new System.Drawing.Point(250, 65);
            this.UserAcademicInfoLabel.Name = "UserAcademicInfoLabel";
            this.UserAcademicInfoLabel.Size = new System.Drawing.Size(250, 25);
            this.UserAcademicInfoLabel.Style = MetroSet_UI.Design.Style.Light;
            this.UserAcademicInfoLabel.StyleManager = null;
            this.UserAcademicInfoLabel.TabIndex = 2;
            this.UserAcademicInfoLabel.Text = "FACULTY, STUDIES?";
            this.UserAcademicInfoLabel.ThemeAuthor = "Narwin";
            this.UserAcademicInfoLabel.ThemeName = "MetroLite";
            // 
            // temp_RecentActivityArea
            // 
            this.temp_RecentActivityArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_RecentActivityArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_RecentActivityArea.Location = new System.Drawing.Point(25, 250);
            this.temp_RecentActivityArea.Name = "temp_RecentActivityArea";
            this.temp_RecentActivityArea.Size = new System.Drawing.Size(750, 300);
            this.temp_RecentActivityArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_RecentActivityArea.StyleManager = null;
            this.temp_RecentActivityArea.TabIndex = 3;
            this.temp_RecentActivityArea.Text = "RECENT ACTIVITY AREA";
            this.temp_RecentActivityArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_RecentActivityArea.ThemeAuthor = "Narwin";
            this.temp_RecentActivityArea.ThemeName = "MetroLite";
            // 
            // temp_UserBioArea
            // 
            this.temp_UserBioArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_UserBioArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_UserBioArea.Location = new System.Drawing.Point(250, 125);
            this.temp_UserBioArea.Name = "temp_UserBioArea";
            this.temp_UserBioArea.Size = new System.Drawing.Size(525, 100);
            this.temp_UserBioArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_UserBioArea.StyleManager = null;
            this.temp_UserBioArea.TabIndex = 4;
            this.temp_UserBioArea.Text = "USER-SET BIO";
            this.temp_UserBioArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_UserBioArea.ThemeAuthor = "Narwin";
            this.temp_UserBioArea.ThemeName = "MetroLite";
            // 
            // temp_AwardsArea
            // 
            this.temp_AwardsArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_AwardsArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_AwardsArea.Location = new System.Drawing.Point(800, 25);
            this.temp_AwardsArea.Name = "temp_AwardsArea";
            this.temp_AwardsArea.Size = new System.Drawing.Size(375, 200);
            this.temp_AwardsArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_AwardsArea.StyleManager = null;
            this.temp_AwardsArea.TabIndex = 5;
            this.temp_AwardsArea.Text = "BADGES, ACHIEVEMENTS, AWARDS ETC ";
            this.temp_AwardsArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_AwardsArea.ThemeAuthor = "Narwin";
            this.temp_AwardsArea.ThemeName = "MetroLite";
            // 
            // temp_UserContactInfoArea
            // 
            this.temp_UserContactInfoArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_UserContactInfoArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_UserContactInfoArea.Location = new System.Drawing.Point(800, 250);
            this.temp_UserContactInfoArea.Name = "temp_UserContactInfoArea";
            this.temp_UserContactInfoArea.Size = new System.Drawing.Size(375, 300);
            this.temp_UserContactInfoArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_UserContactInfoArea.StyleManager = null;
            this.temp_UserContactInfoArea.TabIndex = 6;
            this.temp_UserContactInfoArea.Text = "CONTACT INFO? OTHER SHIT? IDK I RAN OUT OF IDEAS";
            this.temp_UserContactInfoArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_UserContactInfoArea.ThemeAuthor = "Narwin";
            this.temp_UserContactInfoArea.ThemeName = "MetroLite";
            // 
            // UserProfilePageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.temp_UserContactInfoArea);
            this.Controls.Add(this.temp_AwardsArea);
            this.Controls.Add(this.temp_UserBioArea);
            this.Controls.Add(this.temp_RecentActivityArea);
            this.Controls.Add(this.UserAcademicInfoLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.UserProfilePictureBox);
            this.Name = "UserProfilePageControl";
            this.Size = new System.Drawing.Size(1200, 600);
            ((System.ComponentModel.ISupportInitialize)(this.UserProfilePictureBox)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.PictureBox UserProfilePictureBox;
        private MetroSet_UI.Controls.MetroSetLabel UserNameLabel;
        private MetroSet_UI.Controls.MetroSetLabel UserAcademicInfoLabel;
        private MetroSet_UI.Controls.MetroSetLabel temp_RecentActivityArea;
        private MetroSet_UI.Controls.MetroSetLabel temp_UserBioArea;
        private MetroSet_UI.Controls.MetroSetLabel temp_AwardsArea;
        private MetroSet_UI.Controls.MetroSetLabel temp_UserContactInfoArea;
        }
    }
