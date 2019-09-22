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
        private void InitDb(Guid teacherId)
        {
            if(teacherId != Guid.Empty)
            {
                var databaseConnection = new SQLDbConnection();                             //
                databaseConnection.Clear();                                                 //Connection to
                ITeacherManager teacherManager = new TeacherManager(databaseConnection);    //data
                IRatingManager ratingManager = new RatingManager(databaseConnection);       //

                var selectedTeacher = teacherManager.GetTeacher(teacherId);
                nameLabel.Text = selectedTeacher.Name + " " + selectedTeacher.Surname;
                rankLabel.Text = selectedTeacher.Rank.ToString();
                var ratings = ratingManager.GetAllRatings();
                foreach (var rating in ratings)
                {
                    if (rating.TeacherId == teacherId)
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
        // If teacher is found, initiates InitDb method while passing along the teacher's Id, if not - passes along Guid.Empty
        private void Search()
        {
            var databaseConnection = new SQLDbConnection();
            databaseConnection.Clear();
            ITeacherManager teacherManager = new TeacherManager(databaseConnection);
            var teachers = teacherManager.GetAllTeachers();
            if (searchBoxRate.Text != "")
            {
                for (int i = teachers.Count - 1; i >= 0; i--)
                {
                    var item = teachers[i];
                    string fullName = item.Name + " " + item.Surname;
                    if (!fullName.ToLower().Contains(searchBoxRate.Text.ToLower()))
                        teachers.Remove(item);
                }
                if (teachers.Count == 1)
                {
                    foreach (var n in teachers)
                        InitDb(n.Id);
                }
            }
            else
                InitDb(Guid.Empty);
        }
    }
}
