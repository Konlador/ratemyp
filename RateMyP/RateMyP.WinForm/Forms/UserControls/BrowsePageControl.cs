using System;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using System.Collections.Generic;
using System.Linq;
using RateMyP.Entities;

namespace RateMyP.WinForm.Forms.UserControls
    {
    public partial class BrowsePageControl : UserControl
        {
        public BrowsePageControl()
            {
            InitializeComponent();
            LoadTeachers();
            LoadCourses();
            }

        private void TeacherListView_ItemActivate(object sender, EventArgs e)
            {
            var selectedTeacherItem = TeacherListView.SelectedItems[0];
            var selectedTeacher = (Teacher)selectedTeacherItem.Tag;
            RateMyProfessor.self.teacherProfilePageControl.UpdateInfo(selectedTeacher);
            if (!RateMyProfessor.self.MenuTabControl.TabPages.Contains(RateMyProfessor.self.TabPageTeacherProfile))
                RateMyProfessor.self.MenuTabControl.TabPages.Insert(2, RateMyProfessor.self.TabPageTeacherProfile);
            RateMyProfessor.self.MenuTabControl.SelectedTab = RateMyProfessor.self.TabPageTeacherProfile;
            }

        private void LoadTeachers()
            {
            using (var context = new RateMyPDbContext())
                {
                var teachers = context.Teachers.ToList();
                LoadTeachersListView(teachers);
                }
            }

        private void LoadTeachersListView(List<Teacher> teachers)
            {
            TeacherListView.Items.Clear();
            foreach (var teacher in teachers)
                {
                var teacherInfo = new[] { $"{teacher.FirstName} {teacher.LastName}", teacher.Rank };
                var teacherItem = new ListViewItem(teacherInfo) { Tag = teacher };
                TeacherListView.Items.Add(teacherItem);
                }
            }

        private void LoadCourses()
            {
            using (var context = new RateMyPDbContext())
                {
                var courses = context.Courses.ToList();
                CourseListView.Items.Clear();
                foreach (var course in courses)
                    {
                    var courseInfo = new[] { course.Name, course.Faculty, course.CourseType.ToString() };
                    var courseItem = new ListViewItem(courseInfo);
                    CourseListView.Items.Add(courseItem);
                    }
                }
            }

        private void LoadCourses(List<Course> courses)
            {
            CourseListView.Items.Clear();
            foreach (var course in courses)
                {
                var courseInfo = new[] { course.Name, course.Faculty, course.CourseType.ToString() };
                var courseItem = new ListViewItem(courseInfo);
                CourseListView.Items.Add(courseItem);
                }
            }

        public void SearchTeachers()
            {
            using (var context = new RateMyPDbContext())
                {
                var teachers = (from t in context.Teachers
                                where (t.FirstName + " " + t.LastName).Contains(SearchTextBox.Text)
                                select t).ToList();
                LoadTeachersListView(teachers);
                }
            }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
            {
            SearchTeachers();
            }

        private void SearchButton_Click(object sender, EventArgs e)
            {
            SearchTeachers();
            }

        private void CourseListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
            {
            e.Cancel = true;
            e.NewWidth = CourseListView.Columns[e.ColumnIndex].Width;
            }

        private void TeacherListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
            {
            e.Cancel = true;
            e.NewWidth = TeacherListView.Columns[e.ColumnIndex].Width;
            }

        private void TeacherListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
            {
            var selectedTeacher = (Teacher)e.Item.Tag;
            List<Teacher> teachers = new List<Teacher>() { selectedTeacher };
            var courseList = GetCoursesFromTeachers(teachers);
            LoadCourses(courseList);
            }

        private List<Course> GetCoursesFromTeachers(List<Teacher> teachers)
            {
            var courses = new List<Course>();
            using (var context = new RateMyPDbContext())
                {
                foreach (var teacher in teachers)
                    {
                    var activities = (from ta in context.TeacherActivities
                                      where ta.Teacher.Id.Equals(teacher.Id)
                                      select ta).ToList();
                    courses.AddRange(activities.Select(ta => ta.Course));
                    }
                }
            return courses.Distinct().ToList();
            }
    }
    }
