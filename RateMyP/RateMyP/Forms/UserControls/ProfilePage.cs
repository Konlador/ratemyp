using RateMyP.Managers;
using System;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
    {
    public partial class ProfilePage : UserControl
        {
        public ProfilePage()
            {
            InitializeComponent();
            InitializeData(Guid.Empty);  // current student Id in place of 'empty'
            }

        // Connects to the data, gets student class data by matching the StudentId and displays it in the labels.
        private void InitializeData(Guid studentId)
            {
            if (studentId != Guid.Empty)
                {
                var studentManager = new StudentManager();
                var selectedStudent = studentManager.GetById(studentId);
                sName.Text = $"{selectedStudent.Name} {selectedStudent.Surname}";
                sFaculty.Text = selectedStudent.Faculty;
                sStudies.Text = selectedStudent.Studies;
                }
            else
                {
                sName.Text = " ";
                sFaculty.Text = " ";
                sStudies.Text = " ";
                }
            }

        //TODO: Tags
        }
    }
