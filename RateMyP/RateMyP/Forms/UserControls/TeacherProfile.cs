using RateMyP.Entities;
using System;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
    {
    public partial class TeacherProfile : UserControl
        {
        private Teacher m_teacher;

        public TeacherProfile()
            {
            InitializeComponent();
            Hide();
            }

        //Updates labels with relevant info
        public void UpdateInfo(Teacher teacher)
            {
            m_teacher = teacher;
            teacherNameLabel.Text = $"Name: {teacher.FirstName} {teacher.LastName}";
            teacherFacultyLabel.Text = $"Faculty: {teacher.Faculty}";
            teacherRankLabel.Text = $"Rank: {teacher.Rank}";
            teacherInfoLabel.Text = $"Description: {teacher.Description}";
            }

        //Called when the button is clicked. Sends user to the RatePage
        private void TeacherRateButton_Click(object sender, EventArgs e)
            {
            if (m_teacher != null)
                {
                //MainForm.self.ratePage.InitializeData(m_teacher);
                Hide();
                MainForm.self.ratePage.Show();
                MainForm.self.ratePage.BringToFront();
                }
            else
                Console.WriteLine("Shits on fire yo");
            }
        }
    }
