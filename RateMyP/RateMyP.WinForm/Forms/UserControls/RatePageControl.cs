using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            LoadTags();
            ResetSelections();
            }

        // Workaround so the designer doesn't nuke it every time you rebuild.
        private void InitializeRatingList()
            {
            RateStarList = new List<PictureBox>
                {
                RateStarImageOne,
                RateStarImageTwo,
                RateStarImageThree,
                RateStarImageFour,
                RateStarImageFive
                };
            }

        public void ResetSelections()
            {
            TeacherTakeAgainSwitch.Switched = false;
            m_isLocked = false;
            m_currentRating = 0;
            m_difficultyRating = 1;
            ChangeStarImage(RateStarList, 4, false);
            RateCommentTextBox.Text = null;
            RateCommentTextBox.Lines = null;
            TeacherDifficultySlider.Value = 1;
            for (int i = 0; i < TeacherRateTagBox.Items.Count; i++)
                {
                TeacherRateTagBox.SetItemCheckState(i, CheckState.Unchecked);
                }
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

        private bool CheckIfValidRating()
            {
            if (m_currentRating == 0)
                {
                MessageBox.Show("Please leave a star rating.");
                return false;
                }
            if (RateCommentTextBox.Text.Length < 20)
                {
                MessageBox.Show("Please write at least 20 characters.");
                return false;
                };
            if (TeacherCoursesListView.SelectedItems.Count == 0)
                {
                MessageBox.Show("Please select a course from the list.");
                return false;
                }
            if (TeacherRateTagBox.CheckedItems.Count > 5)
                {
                MessageBox.Show("Please select no more than 5 tags");
                return false;
                }
            if (m_difficultyRating == 0)
                {
                MessageBox.Show("Please adjust the difficulty slider.");
                return false;
                }

            return true;
            }

        private void RatePageButtonSend_Click(object sender, EventArgs e)
            {
            if (!CheckIfValidRating()) return;

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

            newRating.Tags = GetAllSelectedTags(newRating.Id);
            
            try
                {
                RateMyPClient.Client.Ratings.Post(newRating);
                MessageBox.Show("Thank you for your feedback.");
                ResetSelections();
                RateMyProfessor.self.teacherProfilePageControl.RefreshRatings();
                RateMyProfessor.self.MenuTabControl.SelectTab(RateMyProfessor.self.MenuTabControl.SelectedIndex - 1);
                RateMyProfessor.self.MenuTabControl.TabPages.Remove(RateMyProfessor.self.TabPageRatePage);
                }
            catch (Exception ex)
                {
                MessageBox.Show("Feedback sending unsuccessful. Please try again later.");
                }
                
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

        private async void LoadTags()
            {
            var tags = await RateMyPClient.Client.Tags.GetAll();
            LoadTagList(tags);
            }

        private void LoadTagList(List<Tag> tags)
            {
            foreach (var tag in tags)
                {
                TeacherRateTagBox.Items.Add(tag);
                }
            }

        private List<RatingTag> GetAllSelectedTags(Guid ratingId)
            {
            var ratingTags = new List<RatingTag>();
            foreach (var tag in TeacherRateTagBox.CheckedItems)
                {
                ratingTags.Add(new RatingTag()
                    {
                    RatingId = ratingId,
                    TagId = ((Tag)tag).Id,
                    });
                }
            return ratingTags;
            }

        private void TeacherTakeAgainSwitch_Switch(object sender)
            {
            TeacherTakeAgainLabel.Text = TeacherTakeAgainSwitch.Switched ? "Yes" : "No";
            }
        }
    }
