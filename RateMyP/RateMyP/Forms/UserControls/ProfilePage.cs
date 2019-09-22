﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RateMyP.Managers;

namespace RateMyP.Forms.UserControls
{
    public partial class ProfilePage : UserControl
    {
        public ProfilePage()
        {
            InitializeComponent();
            InitDb(Guid.Empty);  // current student Id in place of 'empty'
        }

        // Connects to the data, gets student class data by matching the StudentId and displays it in the labels.
        private void InitDb(Guid studentId)
        {
            if (studentId != Guid.Empty)
            {
                var databaseConnection = new SQLDbConnection();
                databaseConnection.Clear();
                IStudentManager studentManager = new StudentManager(databaseConnection);
                var selectedStudent = studentManager.GetStudent(studentId);
                sName.Text = selectedStudent.Name + " " + selectedStudent.Surname;
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
