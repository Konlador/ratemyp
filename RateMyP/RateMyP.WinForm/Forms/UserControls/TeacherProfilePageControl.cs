using System;
using RateMyP.Entities;
using System.Windows.Forms;

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
            TeacherAcademicInfoLabel.Text = $"{teacher.Rank.ToString()}, {teacher.Faculty}";
            TeacherInfoLabel.Text = $"{teacher.Description}";
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
