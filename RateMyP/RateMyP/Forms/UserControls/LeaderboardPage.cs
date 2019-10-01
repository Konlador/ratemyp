using RateMyP.Managers;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RateMyP.Entities;
using System.Collections.Generic;

namespace RateMyP.Forms.UserControls
    {
    public partial class LeaderboardPage : UserControl
        {
        public LeaderboardPage()
            {
            InitializeComponent();
            InitializeTopTeacherView();
            InitializeTopCourseView();
            }

        // Connects to course and rating data, creates a list of RatedCourse and populates it by cross-referencing the courseId in ratings and course objects
        // Adds up all overallMarks of a course and divides it by the rating count. Creates ratedCourse object with a Course and average Rating parameters
        // Adds these objects to the RatedCourses List.
        private List<RatedCourse> RatedCourses()
        {
            var courseManager = new CourseManager();
            var ratingManager = new RatingManager();
            var courses = courseManager.GetAll();
            var ratings = ratingManager.GetAll();
            List<RatedCourse> ratedCourses = new List<RatedCourse>();
            foreach (var course in courses)
            {
                int totalMark = 0, count = 0;
                foreach (var rating in ratings)
                {
                    if (rating.CourseId == course.Id)
                    {
                        // Adds up data from all ratings
                        totalMark += rating.OverallMark;
                        count++;
                    }
                }
                int averageMark = totalMark / count;
                var ratedCourse = new RatedCourse { Course = course, Rating = averageMark };
                ratedCourses.Add(ratedCourse);
            }

            return ratedCourses;
        }

        // Connects to teacher and rating data, creates a list of RatedTeacher and populates it by cross-referencing the teacherId in ratings and teacher objects
        // Adds up all overallMarks of a teacher and divides it by the rating count. Creates ratedTeacher object with a Teacher and average Rating parameters
        // Adds these objects to the RatedTeachers List.
        private List<RatedTeacher> RatedTeachers()
        {
            var teacherManager = new TeacherManager();
            var ratingManager = new RatingManager();
            var teachers = teacherManager.GetAll();
            var ratings = ratingManager.GetAll();
            List<RatedTeacher> ratedTeachers = new List<RatedTeacher>();
            foreach (var teacher in teachers)
            {
                int totalMark = 0, count = 0;
                foreach (var rating in ratings)
                {
                    if (rating.TeacherId == teacher.Id)
                    {
                        // Adds up data from all ratings
                        totalMark += rating.OverallMark;
                        count++;
                    }
                }
                int averageMark = totalMark / count;
                var ratedTeacher = new RatedTeacher { Teacher = teacher, Rating = averageMark };
                ratedTeachers.Add(ratedTeacher);
            }

            return ratedTeachers;
        }

        // Gets RatedTeachers list, orders it by rating and displays the details in the ListView
        private void InitializeTopTeacherView()
            {
            var ratedTeachers = RatedTeachers();
            ratedTeachers = ratedTeachers.OrderByDescending(o => o.Rating).ToList();
            topProfView.Items.Clear();
            foreach (var ratedTeacher in ratedTeachers)
                {
                var row = new[] { $"{ratedTeacher.Teacher.Name} {ratedTeacher.Teacher.Surname}", ratedTeacher.Rating.ToString() };
                var listViewItem = new ListViewItem(row);
                topProfView.Items.Add(listViewItem);
                }
        }

        // Gets RatedCourses list, orders it by rating and displays the details in the ListView
        private void InitializeTopCourseView()
        {
            var ratedCourses = RatedCourses();
            ratedCourses = ratedCourses.OrderByDescending(o => o.Rating).ToList();
            topCourseView.Items.Clear();
            foreach (var ratedCourse in ratedCourses)
            {

                var listViewItem = new ListViewItem(ratedCourse.Course.Name);
                topCourseView.Items.Add(listViewItem);
            }
        }
    }
    // Class to store Teacher/averageRating combination objects
    public class RatedTeacher
    {
        public Teacher Teacher { get; set; }
        public int Rating { get; set; }
    }

    // Class to store Course/averageRating combination objects
    public class RatedCourse
    {
        public Course Course { get; set; }
        public int Rating { get; set; }
    }
}
