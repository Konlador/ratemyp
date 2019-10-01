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

        // Gets all ratings and orders them max -> min
        private List<Rating> GetOrderedRatings()
        {
            var ratingManager = new RatingManager();
            var ratings = ratingManager.GetAll();
            ratings = ratings.OrderByDescending(o => o.OverallMark).ToList();
            return ratings;
        }

        // Connects to the data, gets ordered ratings, cross-references TeacherId to get extra teacher details and displays the results
        private void InitializeTopTeacherView()
            {
            var teacherManager = new TeacherManager();
            var ratings = GetOrderedRatings();
            topProfView.Items.Clear();
            ratings = ratings.OrderByDescending(o => o.OverallMark).ToList();
            foreach (var rating in ratings)
                {
                var teacher = teacherManager.GetById(rating.TeacherId);
                if (null == teacher)
                    continue;

                var row = new[] { $"{teacher.Name} {teacher.Surname}", rating.OverallMark.ToString() };
                var listViewItem = new ListViewItem(row);
                topProfView.Items.Add(listViewItem);
                }
            }

        // Connects to the data, gets ordered ratings, cross-references CourseId to get course details and displays the results
        private void InitializeTopCourseView()
        {
            var courseManager = new CourseManager();
            var ratings = GetOrderedRatings();
            topCourseView.Items.Clear();
            foreach (var rating in ratings)
            {
                var course = courseManager.GetById(rating.CourseId);
                if (null == course)
                    continue;

                var listViewItem = new ListViewItem(course.Name);
                topProfView.Items.Add(listViewItem);
            }
        }
    }
    }
