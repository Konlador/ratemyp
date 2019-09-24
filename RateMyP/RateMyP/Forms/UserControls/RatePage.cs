using RateMyP.Entities;
using RateMyP.Managers;
using System;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
    {
    public partial class RatePage : UserControl
        {
        public RatePage()
            {
            InitializeComponent();
            InitializeData(null);
            }

        private void TextBox1_TextChanged(object sender, EventArgs e)
            {

            }

        // Connects to the data, gets all ratings and compares the TeacherId with the argument, if it matches, displays teacher's rating data.
        public void InitializeData(Teacher teacher)
            {
            if (teacher != null)
                {
                var ratingManager = new RatingManager();
                ratePageNameLabel.Text = $"Name: {teacher.Name} {teacher.Surname}";
                ratePageDegreeLabel.Text = $"Degree: {teacher.Rank.ToString()}";
                var ratings = ratingManager.GetAll();
                foreach (var rating in ratings)
                    {
                    if (rating.TeacherId == teacher.Id)
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
            var teacherManager = new TeacherManager();
            var teachers = teacherManager.GetAll();
            if (searchBoxRate.Text != "")
                {
                for (int i = teachers.Count - 1; i >= 0; i--)
                    {
                    var item = teachers[i];
                    var fullName = $"{item.Name} {item.Surname}";
                    if (!fullName.ToLower().Contains(searchBoxRate.Text.ToLower()))
                        teachers.Remove(item);
                    }

                if (teachers.Count != 1) return;
                InitializeData(teachers[0]);
                }
            else
                InitializeData(null);
            }
        }
    }
