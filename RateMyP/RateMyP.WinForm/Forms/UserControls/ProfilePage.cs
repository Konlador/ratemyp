using System;
using System.Windows.Forms;
using RateMyP.Entities;

namespace RateMyP.Forms.UserControls
    {
    public partial class ProfilePage : UserControl
        {
        private Student m_student;

        public ProfilePage()
            {
            InitializeComponent();
            }

        public void LoadStudentInfo()
            {
            m_student = null; // Get current user and load it
            sName.Text = $"{m_student.FirstName} {m_student.LastName}";
            sFaculty.Text = m_student.Faculty;
            sStudies.Text = m_student.Studies;
            }
        }
    }
