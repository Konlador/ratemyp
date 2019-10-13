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
        }
    }
