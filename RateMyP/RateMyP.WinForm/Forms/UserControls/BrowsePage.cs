using System;
using System.Windows.Forms;
using RateMyP.Entities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RateMyP.Forms.UserControls
    {
    public partial class BrowsePage : UserControl
        {
        public BrowsePage()
            {
            InitializeComponent();
            LoadTeachers();
            LoadCourses();
            }

        private void BrowseSearchButton_Click(object sender, EventArgs e)
            {
            SearchTeachers();
            }

        private void TeacherListView_ItemActivate(object sender, EventArgs e)
            {
            var selectedTeacherItem = TeacherListView.SelectedItems[0];
            var selectedTeacher = (Teacher)selectedTeacherItem.Tag;
            Hide();
            MainForm.self.teacherProfilePage.UpdateInfo(selectedTeacher);
            MainForm.self.teacherProfilePage.Show();
            MainForm.self.teacherProfilePage.BringToFront();
            }

        // Queries teachers and loads them into the teachers ListView.
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
                var teacherItem = new ListViewItem(teacherInfo) {Tag = teacher};
                TeacherListView.Items.Add(teacherItem);
                }
            }

        // Queries courses and loads them into the courses ListView.
        private void LoadCourses()
            {
            using (var context = new RateMyPDbContext())
                {
                var courses = context.Courses.ToList();
                courseListView.Items.Clear();
                foreach (var course in courses)
                    {
                    var courseInfo = new[] { course.Name, course.Faculty };
                    var courseItem = new ListViewItem(courseInfo);
                    courseListView.Items.Add(courseItem);
                    }
                }
            }

        private void SearchBox_Enter(object sender, EventArgs e)
            {
            if (SearchBox.Text.Equals("Search"))
                {
                SearchBox.Text = "";
                SearchBox.ForeColor = Color.Black;
                }
            }

        private void SearchBox_Leave(object sender, EventArgs e)
            {
            if (string.IsNullOrEmpty(SearchBox.Text))
                {
                SearchBox.Text = "Search";
                SearchBox.ForeColor = Color.Silver;
                }
            }

        private void SearchBox_TextChanged(object sender, EventArgs e)
            {
            SearchTeachers();
            }

        public void SearchTeachers()
            {
            using (var context = new RateMyPDbContext())
                {
                var teachers = (from t in context.Teachers
                                where (t.FirstName + " " + t.LastName).Contains(SearchBox.Text)
                                select t).ToList();
                LoadTeachersListView(teachers);
                }
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
