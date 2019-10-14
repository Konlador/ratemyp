using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RateMyP.Entities;
using System.Windows.Forms;
using RateMyP.Client;

namespace RateMyP.WinForm.Forms.UserControls
    {
    public partial class TeacherProfilePageControl : UserControl
        {
        private Teacher m_teacher;

        public TeacherProfilePageControl()
            {
            InitializeComponent();
            }

        public void UpdateInfo(Teacher teacher)
            {
            m_teacher = teacher;
            TeacherNameLabel.Text = $"{teacher.FirstName} {teacher.LastName}";
            TeacherAcademicInfoLabel.Text = $"{teacher.Rank}, {teacher.Faculty}";
            TeacherInfoLabel.Text = $"{teacher.Description}";

            LoadRatings();
            }

        private async Task LoadRatings()
            {
            var ratings = await RateMyPClient.Client.Ratings.GetTeacherRatings(m_teacher.Id);
            RatingsGridView.Rows.Clear();
            foreach (var rating in ratings)
                {
                var course = await RateMyPClient.Client.Courses.Get(rating.CourseId);
                var ratingInfo = new[] { rating.OverallMark.ToString(), rating.LevelOfDifficulty.ToString(), course.Name, rating.Comment, rating.WouldTakeTeacherAgain.ToString(), rating.DateCreated.ToString() };
                RatingsGridView.Rows.Add(ratingInfo);
                }
            }
        }
    }
