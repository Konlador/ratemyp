using RateMyP.Managers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
    {
    public partial class LeaderboardPage : UserControl
        {
        public LeaderboardPage()
            {
            InitializeComponent();
            InitializeTopTeacherView();
            }

        // Connects to the data, gets all ratings and orders them max -> min, cross-references TeacherId to get extra teacher details and displays the results
        private void InitializeTopTeacherView()
            {
            var teacherManager = new TeacherManager();
            var ratingManager = new RatingManager();
            var ratings = ratingManager.GetAll();
            topProfView.Items.Clear();
            ratings = ratings.OrderByDescending(o => o.OverallMark).ToList();
            foreach (var rating in ratings)
                {
                var teacher = teacherManager.GetById(rating.TeacherId);
                if (null == teacher)
                    throw new Exception("No teacher found.");

                var row = new[] { $"{teacher.Name} {teacher.Surname}", rating.OverallMark.ToString() };
                var listViewItem = new ListViewItem(row);
                topProfView.Items.Add(listViewItem);
                }
            }
        }
    }
