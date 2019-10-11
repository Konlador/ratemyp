namespace RateMyP.WinForm.Forms.UserControls
    {
    partial class BrowsePageControl
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
            this.TeacherListView = new System.Windows.Forms.ListView();
            this.teacherName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.teacherRank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CourseListView = new System.Windows.Forms.ListView();
            this.courseName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.faculty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SearchButton = new MetroSet_UI.Controls.MetroSetButton();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TeacherListView
            // 
            this.TeacherListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.TeacherListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.teacherName,
            this.teacherRank});
            this.TeacherListView.FullRowSelect = true;
            this.TeacherListView.HideSelection = false;
            this.TeacherListView.LabelWrap = false;
            this.TeacherListView.Location = new System.Drawing.Point(30, 75);
            this.TeacherListView.MultiSelect = false;
            this.TeacherListView.Name = "TeacherListView";
            this.TeacherListView.Size = new System.Drawing.Size(500, 500);
            this.TeacherListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.TeacherListView.TabIndex = 2;
            this.TeacherListView.UseCompatibleStateImageBehavior = false;
            this.TeacherListView.View = System.Windows.Forms.View.Details;
            this.TeacherListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.TeacherListView_ColumnWidthChanging);
            this.TeacherListView.ItemActivate += new System.EventHandler(this.TeacherListView_ItemActivate);
            this.TeacherListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.TeacherListView_ItemSelectionChanged);
            // 
            // teacherName
            // 
            this.teacherName.Text = "Teacher";
            this.teacherName.Width = 300;
            // 
            // teacherRank
            // 
            this.teacherRank.Text = "Degree";
            this.teacherRank.Width = 179;
            // 
            // CourseListView
            // 
            this.CourseListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.CourseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.courseName,
            this.faculty,
            this.type});
            this.CourseListView.FullRowSelect = true;
            this.CourseListView.HideSelection = false;
            this.CourseListView.LabelWrap = false;
            this.CourseListView.Location = new System.Drawing.Point(600, 75);
            this.CourseListView.MultiSelect = false;
            this.CourseListView.Name = "CourseListView";
            this.CourseListView.Size = new System.Drawing.Size(550, 500);
            this.CourseListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.CourseListView.TabIndex = 3;
            this.CourseListView.UseCompatibleStateImageBehavior = false;
            this.CourseListView.View = System.Windows.Forms.View.Details;
            this.CourseListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.CourseListView_ColumnWidthChanging);
            // 
            // courseName
            // 
            this.courseName.Text = "Course";
            this.courseName.Width = 370;
            // 
            // faculty
            // 
            this.faculty.Text = "Faculty";
            this.faculty.Width = 90;
            // 
            // type
            // 
            this.type.Text = "Type";
            this.type.Width = 69;
            // 
            // SearchButton
            // 
            this.SearchButton.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SearchButton.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SearchButton.DisabledForeColor = System.Drawing.Color.Gray;
            this.SearchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SearchButton.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.SearchButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.SearchButton.HoverTextColor = System.Drawing.Color.White;
            this.SearchButton.Location = new System.Drawing.Point(30, 30);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SearchButton.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SearchButton.NormalTextColor = System.Drawing.Color.White;
            this.SearchButton.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.SearchButton.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.SearchButton.PressTextColor = System.Drawing.Color.White;
            this.SearchButton.Size = new System.Drawing.Size(100, 30);
            this.SearchButton.Style = MetroSet_UI.Design.Style.Light;
            this.SearchButton.StyleManager = null;
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "Search";
            this.SearchButton.ThemeAuthor = "Narwin";
            this.SearchButton.ThemeName = "MetroLite";
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(137, 30);
            this.SearchTextBox.MaxLength = 50;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(393, 20);
            this.SearchTextBox.TabIndex = 4;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // BrowsePageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.CourseListView);
            this.Controls.Add(this.TeacherListView);
            this.Name = "BrowsePageControl";
            this.Size = new System.Drawing.Size(1200, 600);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.ListView TeacherListView;
        private System.Windows.Forms.ListView CourseListView;
        private MetroSet_UI.Controls.MetroSetButton SearchButton;

        private System.Windows.Forms.ColumnHeader teacherName;
        private System.Windows.Forms.ColumnHeader teacherRank;
        private System.Windows.Forms.ColumnHeader courseName;
        private System.Windows.Forms.ColumnHeader faculty;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.TextBox SearchTextBox;
        }
    }
