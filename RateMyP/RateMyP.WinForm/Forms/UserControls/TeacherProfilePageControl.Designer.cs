namespace RateMyP.WinForm.Forms.UserControls
    {
    partial class TeacherProfilePageControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TeacherNameLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.TeacherAcademicInfoLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.TeacherInfoLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.TeacherRatings = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_TeacherFeedbackArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_TeacherFillerArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.RatingsGridView = new System.Windows.Forms.DataGridView();
            this.OverallMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LevelOfDifficulty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Course = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WouldTakeAgain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeacherRateButton = new MetroSet_UI.Controls.MetroSetButton();
            this.TeacherProfilePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RatingsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherProfilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TeacherNameLabel
            // 
            this.TeacherNameLabel.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeacherNameLabel.Location = new System.Drawing.Point(250, 25);
            this.TeacherNameLabel.Name = "TeacherNameLabel";
            this.TeacherNameLabel.Size = new System.Drawing.Size(300, 50);
            this.TeacherNameLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherNameLabel.StyleManager = null;
            this.TeacherNameLabel.TabIndex = 1;
            this.TeacherNameLabel.Text = "NAME?";
            this.TeacherNameLabel.ThemeAuthor = "Narwin";
            this.TeacherNameLabel.ThemeName = "MetroLite";
            // 
            // TeacherAcademicInfoLabel
            // 
            this.TeacherAcademicInfoLabel.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeacherAcademicInfoLabel.Location = new System.Drawing.Point(250, 65);
            this.TeacherAcademicInfoLabel.Name = "TeacherAcademicInfoLabel";
            this.TeacherAcademicInfoLabel.Size = new System.Drawing.Size(200, 25);
            this.TeacherAcademicInfoLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherAcademicInfoLabel.StyleManager = null;
            this.TeacherAcademicInfoLabel.TabIndex = 2;
            this.TeacherAcademicInfoLabel.Text = "ACADEMIC INFO?";
            this.TeacherAcademicInfoLabel.ThemeAuthor = "Narwin";
            this.TeacherAcademicInfoLabel.ThemeName = "MetroLite";
            // 
            // TeacherInfoLabel
            // 
            this.TeacherInfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TeacherInfoLabel.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeacherInfoLabel.Location = new System.Drawing.Point(250, 125);
            this.TeacherInfoLabel.Name = "TeacherInfoLabel";
            this.TeacherInfoLabel.Size = new System.Drawing.Size(470, 100);
            this.TeacherInfoLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherInfoLabel.StyleManager = null;
            this.TeacherInfoLabel.TabIndex = 3;
            this.TeacherInfoLabel.Text = "DESCRIPTION, INFO, ETC?";
            this.TeacherInfoLabel.ThemeAuthor = "Narwin";
            this.TeacherInfoLabel.ThemeName = "MetroLite";
            // 
            // TeacherRatings
            // 
            this.TeacherRatings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TeacherRatings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TeacherRatings.Location = new System.Drawing.Point(25, 250);
            this.TeacherRatings.Name = "TeacherRatings";
            this.TeacherRatings.Size = new System.Drawing.Size(700, 325);
            this.TeacherRatings.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherRatings.StyleManager = null;
            this.TeacherRatings.TabIndex = 4;
            this.TeacherRatings.Text = "Ratings here";
            this.TeacherRatings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TeacherRatings.ThemeAuthor = "Narwin";
            this.TeacherRatings.ThemeName = "MetroLite";
            // 
            // temp_TeacherFeedbackArea
            // 
            this.temp_TeacherFeedbackArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_TeacherFeedbackArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_TeacherFeedbackArea.Location = new System.Drawing.Point(750, 250);
            this.temp_TeacherFeedbackArea.Name = "temp_TeacherFeedbackArea";
            this.temp_TeacherFeedbackArea.Size = new System.Drawing.Size(420, 325);
            this.temp_TeacherFeedbackArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_TeacherFeedbackArea.StyleManager = null;
            this.temp_TeacherFeedbackArea.TabIndex = 6;
            this.temp_TeacherFeedbackArea.Text = "LEAVE A FEED BACK HERE";
            this.temp_TeacherFeedbackArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_TeacherFeedbackArea.ThemeAuthor = "Narwin";
            this.temp_TeacherFeedbackArea.ThemeName = "MetroLite";
            // 
            // temp_TeacherFillerArea
            // 
            this.temp_TeacherFillerArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_TeacherFillerArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temp_TeacherFillerArea.Location = new System.Drawing.Point(570, 25);
            this.temp_TeacherFillerArea.Name = "temp_TeacherFillerArea";
            this.temp_TeacherFillerArea.Size = new System.Drawing.Size(150, 50);
            this.temp_TeacherFillerArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_TeacherFillerArea.StyleManager = null;
            this.temp_TeacherFillerArea.TabIndex = 7;
            this.temp_TeacherFillerArea.Text = "FILLER SPACE (BADGES, ACHIEVEMENTS ETC?)";
            this.temp_TeacherFillerArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_TeacherFillerArea.ThemeAuthor = "Narwin";
            this.temp_TeacherFillerArea.ThemeName = "MetroLite";
            // 
            // RatingsGridView
            // 
            this.RatingsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.RatingsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RatingsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OverallMark,
            this.LevelOfDifficulty,
            this.Course,
            this.Comment,
            this.WouldTakeAgain,
            this.Date});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RatingsGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.RatingsGridView.Location = new System.Drawing.Point(25, 250);
            this.RatingsGridView.Name = "RatingsGridView";
            this.RatingsGridView.Size = new System.Drawing.Size(1145, 325);
            this.RatingsGridView.TabIndex = 8;
            // 
            // OverallMark
            // 
            this.OverallMark.HeaderText = "Overall mark";
            this.OverallMark.Name = "OverallMark";
            this.OverallMark.ReadOnly = true;
            // 
            // LevelOfDifficulty
            // 
            this.LevelOfDifficulty.HeaderText = "Level of difficulty";
            this.LevelOfDifficulty.Name = "LevelOfDifficulty";
            this.LevelOfDifficulty.ReadOnly = true;
            // 
            // Course
            // 
            this.Course.HeaderText = "Course";
            this.Course.Name = "Course";
            this.Course.ReadOnly = true;
            // 
            // Comment
            // 
            this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Width = 500;
            // 
            // WouldTakeAgain
            // 
            this.WouldTakeAgain.HeaderText = "Would take again";
            this.WouldTakeAgain.Name = "WouldTakeAgain";
            this.WouldTakeAgain.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 200;
            // 
            // TeacherRateButton
            // 
            this.TeacherRateButton.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TeacherRateButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TeacherRateButton.DisabledForeColor = System.Drawing.Color.Gray;
            this.TeacherRateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TeacherRateButton.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.TeacherRateButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.TeacherRateButton.HoverTextColor = System.Drawing.Color.White;
            this.TeacherRateButton.Location = new System.Drawing.Point(750, 185);
            this.TeacherRateButton.Name = "TeacherRateButton";
            this.TeacherRateButton.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TeacherRateButton.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TeacherRateButton.NormalTextColor = System.Drawing.Color.White;
            this.TeacherRateButton.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.TeacherRateButton.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.TeacherRateButton.PressTextColor = System.Drawing.Color.White;
            this.TeacherRateButton.Size = new System.Drawing.Size(125, 40);
            this.TeacherRateButton.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherRateButton.StyleManager = null;
            this.TeacherRateButton.TabIndex = 9;
            this.TeacherRateButton.Text = "Leave a Rating";
            this.TeacherRateButton.ThemeAuthor = "Narwin";
            this.TeacherRateButton.ThemeName = "MetroLite";
            this.TeacherRateButton.Click += new System.EventHandler(this.TeacherRateButton_Click);
            // 
            // TeacherProfilePictureBox
            // 
            this.TeacherProfilePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TeacherProfilePictureBox.Image = global::RateMyP.WinForm.Properties.Resources.everydaywestrayfurtherfromgodslight;
            this.TeacherProfilePictureBox.Location = new System.Drawing.Point(25, 25);
            this.TeacherProfilePictureBox.Name = "TeacherProfilePictureBox";
            this.TeacherProfilePictureBox.Size = new System.Drawing.Size(200, 200);
            this.TeacherProfilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.TeacherProfilePictureBox.TabIndex = 0;
            this.TeacherProfilePictureBox.TabStop = false;
            // 
            // TeacherProfilePageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.TeacherRateButton);
            this.Controls.Add(this.RatingsGridView);
            this.Controls.Add(this.temp_TeacherFillerArea);
            this.Controls.Add(this.temp_TeacherFeedbackArea);
            this.Controls.Add(this.TeacherRatings);
            this.Controls.Add(this.TeacherInfoLabel);
            this.Controls.Add(this.TeacherAcademicInfoLabel);
            this.Controls.Add(this.TeacherNameLabel);
            this.Controls.Add(this.TeacherProfilePictureBox);
            this.Name = "TeacherProfilePageControl";
            this.Size = new System.Drawing.Size(1200, 600);
            ((System.ComponentModel.ISupportInitialize)(this.RatingsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherProfilePictureBox)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.PictureBox TeacherProfilePictureBox;
        private MetroSet_UI.Controls.MetroSetLabel TeacherNameLabel;
        private MetroSet_UI.Controls.MetroSetLabel TeacherAcademicInfoLabel;
        private MetroSet_UI.Controls.MetroSetLabel TeacherInfoLabel;
        private MetroSet_UI.Controls.MetroSetLabel TeacherRatings;
        private MetroSet_UI.Controls.MetroSetLabel temp_TeacherFeedbackArea;
        private MetroSet_UI.Controls.MetroSetLabel temp_TeacherFillerArea;
        private System.Windows.Forms.DataGridView RatingsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn OverallMark;
        private System.Windows.Forms.DataGridViewTextBoxColumn LevelOfDifficulty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Course;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn WouldTakeAgain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private MetroSet_UI.Controls.MetroSetButton TeacherRateButton;
        }
    }
