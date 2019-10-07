using System;
using System.Windows.Forms;
using RateMyP.Entities;

namespace RateMyP.WinForm.Forms.UserControls
    {
    public partial class UserProfilePageControl : UserControl
        {
        private Student m_student;
        public UserProfilePageControl()
            {
            InitializeComponent();
            }

        public void LoadStudentInfo()
            {
            m_student = null; // Get current user and load it
            UserNameLabel.Text = $"{m_student.FirstName} {m_student.LastName}";
            UserAcademicInfoLabel.Text = $"{m_student.Faculty}, {m_student.Studies}";
            }
        }
    }
