namespace RateMyP.WinForm.Forms.UserControls
    {
    partial class LeaderboardsPageControl
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
            this.TopTeachersList = new System.Windows.Forms.ListView();
            this.TopCoursesList = new System.Windows.Forms.ListView();
            this.TopTeachersLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.TopCoursesLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.SuspendLayout();
            // 
            // TopTeachersList
            // 
            this.TopTeachersList.Location = new System.Drawing.Point(50, 75);
            this.TopTeachersList.Name = "TopTeachersList";
            this.TopTeachersList.Size = new System.Drawing.Size(500, 500);
            this.TopTeachersList.TabIndex = 0;
            this.TopTeachersList.UseCompatibleStateImageBehavior = false;
            // 
            // TopCoursesList
            // 
            this.TopCoursesList.Location = new System.Drawing.Point(650, 75);
            this.TopCoursesList.Name = "TopCoursesList";
            this.TopCoursesList.Size = new System.Drawing.Size(500, 500);
            this.TopCoursesList.TabIndex = 1;
            this.TopCoursesList.UseCompatibleStateImageBehavior = false;
            // 
            // TopTeachersLabel
            // 
            this.TopTeachersLabel.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopTeachersLabel.Location = new System.Drawing.Point(50, 25);
            this.TopTeachersLabel.Name = "TopTeachersLabel";
            this.TopTeachersLabel.Size = new System.Drawing.Size(200, 25);
            this.TopTeachersLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TopTeachersLabel.StyleManager = null;
            this.TopTeachersLabel.TabIndex = 2;
            this.TopTeachersLabel.Text = "Top Teachers";
            this.TopTeachersLabel.ThemeAuthor = "Narwin";
            this.TopTeachersLabel.ThemeName = "MetroLite";
            // 
            // TopCoursesLabel
            // 
            this.TopCoursesLabel.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopCoursesLabel.Location = new System.Drawing.Point(650, 25);
            this.TopCoursesLabel.Name = "TopCoursesLabel";
            this.TopCoursesLabel.Size = new System.Drawing.Size(200, 25);
            this.TopCoursesLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TopCoursesLabel.StyleManager = null;
            this.TopCoursesLabel.TabIndex = 3;
            this.TopCoursesLabel.Text = "Top Courses";
            this.TopCoursesLabel.ThemeAuthor = "Narwin";
            this.TopCoursesLabel.ThemeName = "MetroLite";
            // 
            // LeaderboardsPageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.TopCoursesLabel);
            this.Controls.Add(this.TopTeachersLabel);
            this.Controls.Add(this.TopCoursesList);
            this.Controls.Add(this.TopTeachersList);
            this.Name = "LeaderboardsPageControl";
            this.Size = new System.Drawing.Size(1200, 600);
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.ListView TopTeachersList;
        private System.Windows.Forms.ListView TopCoursesList;
        private MetroSet_UI.Controls.MetroSetLabel TopTeachersLabel;
        private MetroSet_UI.Controls.MetroSetLabel TopCoursesLabel;
        }
    }
