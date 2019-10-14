using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RateMyP.Entities;

namespace RateMyP.WinForm.Forms.UserControls
    {
    public partial class RatePageControl : UserControl
        {
        private Teacher m_teacher;
        private int m_currentRating;
        private bool m_isLocked;

        public RatePageControl()
            {
            InitializeComponent();
            }

        public void UpdateInfo(Teacher teacher)
            {
            m_teacher = teacher;
            TeacherNameLabel.Text = $"{teacher.FirstName} {teacher.LastName}";
            TeacherAcademicInfoLabel.Text = $"{teacher.Rank.ToString()}, {teacher.Faculty}";
            TeacherInfoLabel.Text = $"{teacher.Description}";
            }

        private void ChangeStarImage(List<PictureBox> starList, int count, bool activated)
            {
            for (int i = 0; i <= count; i++)
                {
                if (activated)
                    starList[i].Image = Properties.Resources.star;
                else
                    starList[i].Image = Properties.Resources.star_inactive;
                }
            }

        private void RateStarList_MouseEnter(object sender, EventArgs e)
            {
            if (m_isLocked) return;

            foreach (var star in RateStarList)
                {
                if (sender.Equals(star))
                    {
                    var selectedStars = RateStarList.IndexOf(star);
                    m_currentRating = selectedStars + 1;
                    ChangeStarImage(RateStarList, selectedStars, true);
                    Console.WriteLine(m_currentRating);
                    }
                }
            }

        private void RateStarList_MouseLeave(object sender, EventArgs e)
            {
            if (m_isLocked) return;

            foreach (var star in RateStarList)
                {
                if (sender.Equals(star))
                    {
                    ChangeStarImage(RateStarList, RateStarList.IndexOf(star), false);
                    m_currentRating = 0;
                    Console.WriteLine(m_currentRating);
                    }
                }
            }

        private void RateStarList_Click(object sender, EventArgs e)
            {
            foreach (var star in RateStarList)
                {
                if (sender.Equals(star))
                    {
                    var selection = RateStarList.IndexOf(star);
                    if (m_isLocked && selection < m_currentRating - 1)
                        {
                        m_currentRating = RateStarList.IndexOf(star) + 1;
                        Console.WriteLine(m_currentRating);
                        for (int i = m_currentRating; i < RateStarList.Count; i++)
                            {
                            RateStarList[i].Image = Properties.Resources.star_inactive;
                            }

                        return;
                        }
                    m_currentRating = RateStarList.IndexOf(star) + 1;
                    m_isLocked = true;
                    ChangeStarImage(RateStarList, m_currentRating - 1, true);
                    Console.WriteLine(m_currentRating);
                    }
                }
            }

        private void RatePageButtonSend_Click(object sender, EventArgs e)
            {
            if (m_currentRating == 0) return;
            if (RateCommentTextBox.Text.Length < 20) return;


            }
        }
    }
