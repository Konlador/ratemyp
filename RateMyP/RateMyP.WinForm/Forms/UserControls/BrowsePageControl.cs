using RateMyP.Client;
using RateMyP.Entities;
using RateMyP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private async void LoadTeachers()
            {
            var teachers = await RateMyPClient.Client.Teachers.GetAll();
            LoadTeachersListView(teachers);
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

        private async void LoadCourses(IEnumerable<Course> courses = null)
            {
            if (courses == null)
                courses = await RateMyPClient.Client.Courses.GetAll();
            CourseListView.Items.Clear();
            foreach (var course in courses)
                {
                var courseInfo = new[] { course.Name, course.Faculty };
                var courseItem = new ListViewItem(courseInfo);
                CourseListView.Items.Add(courseItem);
                }
            }

        public async void SearchTeachers()
            {
            var allTeachers = await RateMyPClient.Client.Teachers.GetAll();
            var teachers = (from t in allTeachers
                            where (t.FirstName + " " + t.LastName).ToLower().Denationalize().Contains(SearchTextBox.Text)
                            select t).ToList();
            LoadTeachersListView(teachers);
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

        private async void TeacherListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
            {
            //var selectedTeacher = (Teacher)e.Item.Tag;
            //var courseList = await GetCoursesFromTeachers(new List<Teacher> { selectedTeacher });
            //LoadCourses(courseList);
            }

        private async Task<List<Course>> GetCoursesFromTeachers(List<Teacher> teachers)
            {
            var courses = new List<Course>();
            foreach (var teacher in teachers)
                {
                var activities = await RateMyPClient.Client.Teachers.GetTeacherActivities(teacher.Id);
                if (activities != null)
                    courses.AddRange(activities.Select(ta => ta.Course));
                }
            return courses.Distinct().ToList();
            }
        }
    }
