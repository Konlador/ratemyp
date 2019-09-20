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
            ITeacherManager t_manager = new TeacherManager(databaseConnection);
            IRatingManager r_manager = new RatingManager(databaseConnection);
            var ratings = r_manager.GetAllRatings();
            topProfView.Items.Clear();
            ratings = ratings.OrderByDescending(o => o.OverallMark).ToList();
            foreach (var rating in ratings)
            {
                var Teacher = t_manager.GetTeacher(rating.TeacherId);
                var row = new string[] { Teacher.Name + " " + Teacher.Surname, rating.OverallMark.ToString() };
                var lvi = new ListViewItem(row);
                topProfView.Items.Add(lvi);
            }
        }
    }
}
