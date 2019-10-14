﻿using System;
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
            LoadRatingsListView(ratings);
            RatingsGridView.AutoResizeColumns();
            }

        private void LoadRatingsListView(List<Rating> ratings)
            {
            RatingsGridView.Rows.Clear();
            foreach (var rating in ratings)
                {
                var ratingInfo = new[] { rating.OverallMark.ToString(), rating.LevelOfDifficulty.ToString(), rating.CourseId.ToString(), rating.Comment, rating.WouldTakeTeacherAgain.ToString(), rating.DateCreated.ToString() };
                RatingsGridView.Rows.Add(ratingInfo);
                }
            }

        private void TeacherRateButton_Click(object sender, EventArgs e)
            {
            RateMyProfessor.self.ratePageControl.UpdateInfo(m_teacher);
            if (!RateMyProfessor.self.MenuTabControl.TabPages.Contains(RateMyProfessor.self.TabPageRatePage))
                RateMyProfessor.self.MenuTabControl.TabPages.Insert(3, RateMyProfessor.self.TabPageRatePage);
            RateMyProfessor.self.MenuTabControl.SelectedTab = RateMyProfessor.self.TabPageRatePage;
            }
        }
    }
