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
    public partial class RatePage : UserControl
    {
        public RatePage()
        {
            InitializeComponent();
            InitDb(Guid.Empty);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Connects to the data, gets all ratings and compares the TeacherId with the argument, if it matches, displays teacher's rating data.
        private void InitDb(Guid tId)
        {
            if(tId != Guid.Empty)
            {
                var databaseConnection = new SQLDbConnection();
                databaseConnection.Clear();
                ITeacherManager t_manager = new TeacherManager(databaseConnection);
                IRatingManager r_manager = new RatingManager(databaseConnection);
                var selTeacher = t_manager.GetTeacher(tId);
                nameLabel.Text = selTeacher.Name + " " + selTeacher.Surname;
                rankLabel.Text = selTeacher.Rank.ToString();
                var ratings = r_manager.GetAllRatings();
                foreach (var rating in ratings)
                {
                    if (rating.TeacherId == tId)
                    {
                        difficultyLabel.Text = rating.LevelOfDifficulty.ToString();
                        overallMarkLabel.Text = rating.OverallMark.ToString();
                        againLabel.Text = rating.WouldTakeTeacherAgain.ToString();
                    }
                }
            }
            else
            {
                nameLabel.Text = " ";
                rankLabel.Text = " ";
                difficultyLabel.Text = " ";
                overallMarkLabel.Text = " ";
                againLabel.Text = " ";
            }
        }

        private void SearchRateButton_Click(object sender, EventArgs e)
        {
            Search();
        }
        // Gets all teacher data, performs a search by comparing search box contents with the full names of the teachers by using 'Contains' string method.
        // If teacher is found, initiates InitDb method while passing along the teacher's Id
        private void Search()
        {
            var databaseConnection = new SQLDbConnection();
            databaseConnection.Clear();
            ITeacherManager t_manager = new TeacherManager(databaseConnection);
            var Teachers = t_manager.GetAllTeachers();
            if (searchBoxRate.Text != "")
            {
                for (int i = Teachers.Count - 1; i >= 0; i--)
                {
                    var item = Teachers[i];
                    string fullName = item.Name + " " + item.Surname;
                    if (!fullName.ToLower().Contains(searchBoxRate.Text.ToLower()))
                        Teachers.Remove(item);
                }
                if (Teachers.Count == 1)
                {
                    foreach (var n in Teachers)
                        InitDb(n.Id);
                }
            }
            else
                InitDb(Guid.Empty);
        }
    }
}
