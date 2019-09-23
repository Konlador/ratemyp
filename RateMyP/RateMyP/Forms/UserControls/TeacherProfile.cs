using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
{
    public partial class TeacherProfile : UserControl
    {
        private Guid m_id;

        public TeacherProfile()
        {
            InitializeComponent();
            this.Hide();
        }

        //Updates labels with relevant info
        public void UpdateInfo(string teacherName, string teacherFaculty, string teacherStudies, string teacherRank, string teacherDescription, Guid id)
        {
            teacherNameLabel.Text = $"Name: {teacherName}";
            teacherFacultyLabel.Text = $"Faculty: { teacherFaculty}";
            teacherRankLabel.Text = $"Rank: {teacherRank}";
            teacherStudiesLabel.Text = $"Studies: {teacherStudies}";
            teacherInfoLabel.Text = $"Description: {teacherDescription}";
            m_id = id;
        }

        //Called when the button is clicked. Sends user to the RatePage
        private void TeacherRateButton_Click(object sender, EventArgs e)
        {
            if (m_id != Guid.Empty)
            {
                MainForm.self.ratePage.InitializeData(m_id);
                this.Hide();
                MainForm.self.ratePage.Show();
                MainForm.self.ratePage.BringToFront();
            }
            else Console.WriteLine("Shits on fire yo");
        }
    }
}
