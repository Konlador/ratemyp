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
        SQLDbConnection databaseConnection = new SQLDbConnection();

        public RatePage()
        {
            InitializeComponent();
            InitializeData(Guid.Empty);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Connects to the data, gets all ratings and compares the TeacherId with the argument, if it matches, displays teacher's rating data.
        public void InitializeData(Guid teacherId)
        {
            if(teacherId != Guid.Empty)
            {
                var teacherManager = new TeacherManager(databaseConnection);          //Connection todata
                var ratingManager = new RatingManager(databaseConnection);

                var selectedTeacher = teacherManager.GetTeacher(teacherId);
                ratePageNameLabel.Text = $"Name: {selectedTeacher.Name} {selectedTeacher.Surname}";
                ratePageDegreeLabel.Text = $"Degree: {selectedTeacher.Rank.ToString()}";
                var ratings = ratingManager.GetAllRatings();
                foreach (var rating in ratings)
                {
                    if (rating.TeacherId == teacherId)
                    {
                        ratePageDifficultyLabel.Text = $"Difficulty: {rating.LevelOfDifficulty.ToString()}";
                        ratePageOverallMarkLabel.Text = $"Overall Mark: {rating.OverallMark.ToString()}";
                        ratePageTakeAgainLabel.Text = $"Would take teacher again: {rating.WouldTakeTeacherAgain.ToString()}";
                    }
                }
            }
            else
            {
                ratePageNameLabel.Text = " ";
                ratePageDegreeLabel.Text = " ";
                ratePageDifficultyLabel.Text = " ";
                ratePageOverallMarkLabel.Text = " ";
                ratePageTakeAgainLabel.Text = " ";
            }
        }

        private void SearchRateButton_Click(object sender, EventArgs e)
        {
            Search();
        }
        // Gets all teacher data, performs a search by comparing search box contents with the full names of the teachers by using 'Contains' string method.
        // If teacher is found, initiates InitializeData method while passing along the teacher's Id, if not - passes along Guid.Empty
        private void Search()
        {
            var teacherManager = new TeacherManager(databaseConnection);
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
                        InitializeData(n.Id);
                }
            }
            else
                InitializeData(Guid.Empty);
        }
    }
}
