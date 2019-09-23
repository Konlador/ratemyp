using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RateMyP.Entities;

namespace RateMyP.Forms.UserControls
{
    public partial class TeacherProfile : UserControl
    {
        private Teacher m_teacher;

        public TeacherProfile()
        {
            InitializeComponent();
            this.Hide();
        }

        //Updates labels with relevant info
        public void UpdateInfo(Teacher teacher)
        {
            teacherNameLabel.Text = $"Name: {teacher.Name} {teacher.Surname}";
            teacherFacultyLabel.Text = $"Faculty: {teacher.Faculty}";
            teacherRankLabel.Text = $"Rank: {teacher.Rank}";
            teacherStudiesLabel.Text = $"Studies: {teacher.Studies}";
            teacherInfoLabel.Text = $"Description: {teacher.Description}";
            m_teacher = teacher;
        }

        //Called when the button is clicked. Sends user to the RatePage
        private void TeacherRateButton_Click(object sender, EventArgs e)
        {
            if (m_teacher != null)
            {
                MainForm.self.ratePage.InitializeData(m_teacher);
                this.Hide();
                MainForm.self.ratePage.Show();
                MainForm.self.ratePage.BringToFront();
            }
            else Console.WriteLine("Shits on fire yo");
        }
    }
}
