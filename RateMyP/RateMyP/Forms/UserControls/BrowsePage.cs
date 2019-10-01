using RateMyP.Managers;
using System;
using System.Windows.Forms;
using RateMyP.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RateMyP.Forms.UserControls
    {
    public partial class BrowsePage : UserControl
        {
        public BrowsePage()
            {
            InitializeComponent();
            InitializeTeacherListView();
            InitializeCourseListView();
            }

        // Connects to the data, gets all teacher class data and displays it in the ListView.
        private void InitializeTeacherListView()
            {
            var teacherManager = new TeacherManager();
            var teachers = teacherManager.GetAll();
            profListView.Items.Clear();
            foreach (var teacher in teachers)
                {
                var row = new string[] { $"{teacher.Name} {teacher.Surname}", teacher.Rank.ToString() };
                var lvi = new ListViewItem(row);
                profListView.Items.Add(lvi);
                }
            }

        // Gets a list of teachers and displays it in the ListView.
        private void InitializeTeacherListView(List<Teacher> teachers)
        {
            foreach (var teacher in teachers)
            {
                var row = new string[] { $"{teacher.Name} {teacher.Surname}", teacher.Rank.ToString() };
                var lvi = new ListViewItem(row);
                profListView.Items.Add(lvi);
            }
        }

        private void BrowseSearchButton_Click(object sender, EventArgs e)
            {
            Search();
            }

        // Performs a search by comparing search box contents with the full names of the teachers in the teacherManager.GetAll by using 'Contains' string method.
        // The teachers not matching the search input are removed from the list. After the list is shortened, it is passed to InitializeTeacherListView and
        // InitializeCourseListView methods, which then display the edited list.
        private void Search()
            {
            if (browseSearchBox.Text != "")
                {
                var teacherManager = new TeacherManager();
                var teachers = teacherManager.GetAll();
                foreach (var teacher in teachers)
                {
                    var fullname = $"{teacher.Name} {teacher.Surname}";
                    if (!fullname.ToLower().Contains(browseSearchBox.Text.ToLower()))
                    {
                        teachers.Remove(teacher);
                    }
                }
                InitializeTeacherListView(teachers);
                InitializeCourseListView(teachers);
                }
            else
                {
                    InitializeTeacherListView();
                    InitializeCourseListView();
                }
            }

        private void ProfListView_ItemActivate(object sender, EventArgs e)
            {
            ListViewItem selectedItem = profListView.SelectedItems[0];
            if (selectedItem.Text == null) return;
            var teacherManager = new TeacherManager();
            var teachers = teacherManager.GetAll();
            foreach (var teacher in teachers)
                {
                var fullName = $"{teacher.Name} {teacher.Surname}";
                if (selectedItem.Text != fullName) continue;
                Hide();
                MainForm.self.teacherProfilePage.UpdateInfo(teacher);
                MainForm.self.teacherProfilePage.Show();
                MainForm.self.teacherProfilePage.BringToFront();
                break;
                }
            }
        // Connects to the Course manager, gets all course data through GetAll method and displays the course name and faculty in the listView.
        private void InitializeCourseListView()
        {
            var courseManager = new CourseManager();
            var courses = courseManager.GetAll();
            courseListView.Clear();
            foreach (var course in courses)
            {
                var row = new string[] { course.Name, course.Faculty };
                var lvi = new ListViewItem(row);
                courseListView.Items.Add(lvi);
            }
        }
        // Gets a list of teachers, connects to activity and course managers and then cross references teacher data with course data through teacher activities.
        // It gets a list of courses which correspond with the teacher list and displays it in the courseListView.
        private void InitializeCourseListView(List<Teacher> teachers)
        {
            var teacherActivityManager = new TeacherActivityManager();
            var courseManager = new CourseManager();
            var activities = teacherActivityManager.GetAll();
            var courses = new List<Course>();
            courseListView.Clear();
            foreach (var teacher in teachers)
            {
                foreach (var activity in activities)
                {
                    if (teacher.Id == activity.TeacherId)
                    {
                        courses.Add(courseManager.GetById(activity.CourseId));  // adds courses to a list where the courseId matches with the one in activity
                    }
                }

            }
            var uniqueCourses = courses.Distinct().ToList();  // removes redundant courses
            foreach (var course in uniqueCourses)
            {
                var row = new string[] { course.Name, course.Faculty };
                var lvi = new ListViewItem(row);
                courseListView.Items.Add(lvi);
            }
        }
        //TODO: Filtering
    }
    }
