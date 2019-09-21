using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static RateMyP.Managers.TeacherManager;

namespace RateMyP.Forms.UserControls
{
    public partial class ProfilePage : UserControl
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ProfileNameLabel_Click(object sender, EventArgs e)
        {

        }

        public void UpdateInfo()
        {
            profileNameLabel.Text = "Name: " + BrowsePage.teacherName;
            profileDegreeLabel.Text = "Rank: " + BrowsePage.teacherRank;
            profileFacultyLabel.Text = "Faculty: " + BrowsePage.teacherFaculty;
        }

        private void Rate_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm.Self.ratePage.Show();
            MainForm.Self.ratePage.BringToFront();
            MainForm.Self.ratePage.UpdateInfo();
        }
    }
}
