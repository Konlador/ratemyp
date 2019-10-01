using RateMyP.Entities;
using System;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
    {
    public partial class RatePage : UserControl
        {
        public RatePage()
            {
            InitializeComponent();
            //InitializeData(null);
            }

        
        private void TextBox1_TextChanged(object sender, EventArgs e)
            {

            }

        private void SearchRateButton_Click(object sender, EventArgs e)
            {
            //Search();
            }

        /*

    // Connects to the data, gets all ratings and compares the Id with the argument, if it matches, it gets added to a total.
    // The total gets divided by rating count and then teacher's rating data and labels are displayed.
    public void InitializeData(Teacher teacher)
        {
        if (teacher != null)
            {
            var ratingManager = new RatingManager();
            var ratings = ratingManager.GetAll();
            int totalDifficulty = 0, totalMark = 0, takeAgainTotal = 0, count = 0;
            tagsLabelsView.Clear();
            foreach (var rating in ratings)
                {
                if (rating.Teacher.Id.Equals(teacher.Id))
                    {
                    // Adds up data from all ratings
                    totalDifficulty += rating.LevelOfDifficulty;
                    totalMark += rating.OverallMark;
                    takeAgainTotal += rating.WouldTakeTeacherAgain ? 1 : 0;
                    count++;

                    var lvi = new ListViewItem(rating.Tags);
                    tagsLabelsView.Items.Add(lvi);
                    }
                }
            ratePageNameLabel.Text = $"Name: {teacher.FirstName} {teacher.LastName}";
            ratePageDegreeLabel.Text = $"Degree: {teacher.Rank.ToString()}";
            // Divides the added-up data by the number of ratings to show average ratings
            ratePageDifficultyLabel.Text = $"Difficulty: {(totalDifficulty / count).ToString()}";
            ratePageOverallMarkLabel.Text = $"Overall Mark: {(totalMark / count).ToString()}";
            ratePageTakeAgainLabel.Text = $"Would take teacher again: {(takeAgainTotal * 100 / count).ToString()}%";
            }
        else
            {
            ratePageNameLabel.Text = " ";
            ratePageDegreeLabel.Text = " ";
            ratePageDifficultyLabel.Text = " ";
            ratePageOverallMarkLabel.Text = " ";
            ratePageTakeAgainLabel.Text = " ";
            tagsLabelsView.Clear();
            }
        }



    // Gets all teacher data, performs a search by comparing search box contents with the full names of the teachers by using 'Contains' string method.
    // If teacher is found, initiates InitializeData method while passing along the teacher object, if not - passes along null
    private void Search()
        {
        var teacherManager = new TeacherManager();
        var teachers = teacherManager.GetAll();
        if (searchBoxRate.Text != "")
            {
            for (int i = teachers.Count - 1; i >= 0; i--)
                {
                var item = teachers[i];
                var fullName = $"{item.FirstName} {item.LastName}";
                if (!fullName.ToLower().Contains(searchBoxRate.Text.ToLower()))
                    teachers.Remove(item);
                }

            if (teachers.Count != 1) return;
            InitializeData(teachers[0]);
            }
        else
            InitializeData(null);
        }*/
        }
    }
