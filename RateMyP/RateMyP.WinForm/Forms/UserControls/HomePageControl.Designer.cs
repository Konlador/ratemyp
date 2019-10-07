namespace RateMyP.WinForm.Forms.UserControls
    {
    partial class HomePageControl
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
            this.RecentCommentsList = new System.Windows.Forms.ListView();
            this.RecentCommentsLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_TrendingProfessorArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_TrendingCoursesArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.temp_NewsArea = new MetroSet_UI.Controls.MetroSetLabel();
            this.NewsHeaderLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.SuspendLayout();
            // 
            // RecentCommentsList
            // 
            this.RecentCommentsList.Location = new System.Drawing.Point(25, 275);
            this.RecentCommentsList.Name = "RecentCommentsList";
            this.RecentCommentsList.Size = new System.Drawing.Size(600, 300);
            this.RecentCommentsList.TabIndex = 0;
            this.RecentCommentsList.UseCompatibleStateImageBehavior = false;
            // 
            // RecentCommentsLabel
            // 
            this.RecentCommentsLabel.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecentCommentsLabel.Location = new System.Drawing.Point(25, 240);
            this.RecentCommentsLabel.Name = "RecentCommentsLabel";
            this.RecentCommentsLabel.Size = new System.Drawing.Size(175, 25);
            this.RecentCommentsLabel.Style = MetroSet_UI.Design.Style.Light;
            this.RecentCommentsLabel.StyleManager = null;
            this.RecentCommentsLabel.TabIndex = 1;
            this.RecentCommentsLabel.Text = "Recent Comments";
            this.RecentCommentsLabel.ThemeAuthor = "Narwin";
            this.RecentCommentsLabel.ThemeName = "MetroLite";
            // 
            // temp_TrendingProfessorArea
            // 
            this.temp_TrendingProfessorArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_TrendingProfessorArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_TrendingProfessorArea.Location = new System.Drawing.Point(650, 275);
            this.temp_TrendingProfessorArea.Name = "temp_TrendingProfessorArea";
            this.temp_TrendingProfessorArea.Size = new System.Drawing.Size(250, 300);
            this.temp_TrendingProfessorArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_TrendingProfessorArea.StyleManager = null;
            this.temp_TrendingProfessorArea.TabIndex = 2;
            this.temp_TrendingProfessorArea.Text = "TRENDING PROFESSORS";
            this.temp_TrendingProfessorArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_TrendingProfessorArea.ThemeAuthor = "Narwin";
            this.temp_TrendingProfessorArea.ThemeName = "MetroLite";
            // 
            // temp_TrendingCoursesArea
            // 
            this.temp_TrendingCoursesArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_TrendingCoursesArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_TrendingCoursesArea.Location = new System.Drawing.Point(925, 275);
            this.temp_TrendingCoursesArea.Name = "temp_TrendingCoursesArea";
            this.temp_TrendingCoursesArea.Size = new System.Drawing.Size(250, 300);
            this.temp_TrendingCoursesArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_TrendingCoursesArea.StyleManager = null;
            this.temp_TrendingCoursesArea.TabIndex = 3;
            this.temp_TrendingCoursesArea.Text = "TRENDING COURSES";
            this.temp_TrendingCoursesArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_TrendingCoursesArea.ThemeAuthor = "Narwin";
            this.temp_TrendingCoursesArea.ThemeName = "MetroLite";
            // 
            // temp_NewsArea
            // 
            this.temp_NewsArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.temp_NewsArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.temp_NewsArea.Location = new System.Drawing.Point(25, 75);
            this.temp_NewsArea.Name = "temp_NewsArea";
            this.temp_NewsArea.Size = new System.Drawing.Size(800, 100);
            this.temp_NewsArea.Style = MetroSet_UI.Design.Style.Light;
            this.temp_NewsArea.StyleManager = null;
            this.temp_NewsArea.TabIndex = 4;
            this.temp_NewsArea.Text = "NEWS AREA";
            this.temp_NewsArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.temp_NewsArea.ThemeAuthor = "Narwin";
            this.temp_NewsArea.ThemeName = "MetroLite";
            // 
            // NewsHeaderLabel
            // 
            this.NewsHeaderLabel.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewsHeaderLabel.Location = new System.Drawing.Point(25, 25);
            this.NewsHeaderLabel.Name = "NewsHeaderLabel";
            this.NewsHeaderLabel.Size = new System.Drawing.Size(150, 25);
            this.NewsHeaderLabel.Style = MetroSet_UI.Design.Style.Light;
            this.NewsHeaderLabel.StyleManager = null;
            this.NewsHeaderLabel.TabIndex = 5;
            this.NewsHeaderLabel.Text = "News";
            this.NewsHeaderLabel.ThemeAuthor = "Narwin";
            this.NewsHeaderLabel.ThemeName = "MetroLite";
            // 
            // HomePageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.NewsHeaderLabel);
            this.Controls.Add(this.temp_NewsArea);
            this.Controls.Add(this.temp_TrendingCoursesArea);
            this.Controls.Add(this.temp_TrendingProfessorArea);
            this.Controls.Add(this.RecentCommentsLabel);
            this.Controls.Add(this.RecentCommentsList);
            this.Name = "HomePageControl";
            this.Size = new System.Drawing.Size(1200, 600);
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.ListView RecentCommentsList;
        private MetroSet_UI.Controls.MetroSetLabel RecentCommentsLabel;
        private MetroSet_UI.Controls.MetroSetLabel temp_TrendingProfessorArea;
        private MetroSet_UI.Controls.MetroSetLabel temp_TrendingCoursesArea;
        private MetroSet_UI.Controls.MetroSetLabel temp_NewsArea;
        private MetroSet_UI.Controls.MetroSetLabel NewsHeaderLabel;
        }
    }
