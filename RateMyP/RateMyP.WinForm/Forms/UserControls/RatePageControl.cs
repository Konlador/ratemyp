using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Controls;
using RateMyP.Client;
using RateMyP.Entities;

namespace RateMyP.WinForm.Forms.UserControls
    {
    public partial class RatePageControl : UserControl
        {
        private Teacher m_teacher;
        private int m_currentRating;
        private int m_difficultyRating;
        private bool m_isLocked;

        public RatePageControl()
            {
            InitializeComponent();
            InitializeRatingList();
            }

        // Workaround so the designer doesn't nuke it every time you rebuild.
        private void InitializeRatingList()
            {
            RateStarList = new List<PictureBox>();
            RateStarList.Add(RateStarImageOne);
            RateStarList.Add(RateStarImageTwo);
            RateStarList.Add(RateStarImageThree);
            RateStarList.Add(RateStarImageFour);
            RateStarList.Add(RateStarImageFive);
            }

        public async void UpdateInfo(Teacher teacher)
            {
            m_teacher = teacher;
            TeacherNameLabel.Text = $"{teacher.FirstName} {teacher.LastName}";
            TeacherAcademicInfoLabel.Text = $"{teacher.Rank.ToString()}, {teacher.Faculty}";
            TeacherInfoLabel.Text = $"{teacher.Description}";
            var courseList = await GetCoursesFromTeacher(m_teacher);
            LoadCourses(courseList);
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
            if (m_currentRating == 0)
                {
                Console.WriteLine("Please leave a star rating");
                return;
                }
            if (RateCommentTextBox.Text.Length < 20)
                {
                Console.WriteLine("Please write at least 20 characters");
                return;
                };
            if (TeacherCoursesListView.SelectedItems.Count == 0)
                {
                Console.WriteLine("Please select a Course from the list");
                return;
                }

            var course = (Course)(TeacherCoursesListView.SelectedItems[0].Tag);
            var newRating = new Rating()
                {
                Comment = RateCommentTextBox.Text,
                CourseId = course.Id,
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                LevelOfDifficulty = m_difficultyRating,
                OverallMark = m_currentRating,
                Tags = null,
                TeacherId = m_teacher.Id,
                WouldTakeTeacherAgain = TeacherTakeAgainSwitch.Switched
                };

            RateMyPClient.Client.Ratings.Post(newRating);
            Console.WriteLine(TeacherDifficultySlider.Value);
            Console.WriteLine(TeacherTakeAgainSwitch.Switched);

            }

        private void TeacherCoursesListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
            {
            e.Cancel = true;
            e.NewWidth = TeacherCoursesListView.Columns[e.ColumnIndex].Width;
            }

        private async Task<List<Course>> GetCoursesFromTeacher(Teacher teacher)
            {
            var courses = new List<Course>();
            if (teacher.Activities != null)
                {
                foreach (var activity in teacher.Activities)
                    {
                    var course = await RateMyPClient.Client.Courses.Get(activity.CourseId);
                    courses.Add(course);
                    }
                }
            return courses.Distinct().ToList();
            }

        private void LoadCourses(IEnumerable<Course> courses = null)
            {
            if (courses == null)
                return;
            TeacherCoursesListView.Items.Clear();
            foreach (var course in courses)
                {
                var courseInfo = new[] { course.Name, course.Faculty };
                var courseItem = new ListViewItem(courseInfo) { Tag = course };
                TeacherCoursesListView.Items.Add(courseItem);
                }
            }

        private void TeacherDifficultySlider_Scroll(object sender)
            {
            var trackbar = (MetroSetTrackBar)sender;
            TeacherDifficultyLabel.Text = trackbar.Value.ToString();
            m_difficultyRating = trackbar.Value;
            }
        }
    }
