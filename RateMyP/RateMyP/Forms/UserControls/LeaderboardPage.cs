using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RateMyP.Managers;

namespace RateMyP.Forms.UserControls
{
    public partial class LeaderboardPage : UserControl
    {
        public LeaderboardPage()
        {
            InitializeComponent();
            InitTopProfView();
        }
        // Connects to the data, gets all ratings and orders them max -> min, cross-references TeacherId to get extra teacher details and displays the results
        private void InitTopProfView()
        {
            var databaseConnection = new SQLDbConnection();
            databaseConnection.Clear();
            ITeacherManager teacherManager = new TeacherManager(databaseConnection);
            IRatingManager ratingManager = new RatingManager(databaseConnection);
            var ratings = ratingManager.GetAllRatings();
            topProfView.Items.Clear();
            ratings = ratings.OrderByDescending(o => o.OverallMark).ToList();
            foreach (var rating in ratings)
            {
                var teacher = teacherManager.GetTeacher(rating.TeacherId);
                var row = new string[] { teacher.Name + " " + teacher.Surname, rating.OverallMark.ToString() };
                var lvi = new ListViewItem(row);
                topProfView.Items.Add(lvi);
            }
        }
    }
}
