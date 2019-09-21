using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static RateMyP.Constants;
using RateMyP.Managers;
using RateMyP.Entities;
using RateMyP.Forms.UserControls;

namespace RateMyP.Forms.UserControls
{
    public partial class BrowsePage : UserControl, ITeacherManager
    {
        TeacherManager teacherManager;
        private List<Teacher> teachers;

        public static String teacherName;
        public static AcademicRank teacherRank;
        public static Guid teacherId;
        public static String teacherFaculty;
        
        public BrowsePage()
        {
            InitializeComponent();
            teacherManager = new TeacherManager(new SQLDbConnection());
            teachers = teacherManager.GetAllTeachers();
            PopulateTeachers(teachers);
        }

        public void PopulateTeachers(List<Teacher> teachers)
        {
            foreach (var t in teachers)
            {
                ListViewItem teacher = new ListViewItem();
                teacher.Text = t.Name + " " + t.Surname;
                profListView.Items.Add(teacher);
            }
        }

        void ITeacherManager.AddTeacher(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        List<Teacher> ITeacherManager.GetAllTeachers()
        {
            throw new NotImplementedException();
        }

        Teacher ITeacherManager.GetTeacher(Guid teacherId)
        {
            throw new NotImplementedException();
        }

        private void ProfListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ProfListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem item = profListView.SelectedItems[0];        
            if (item.Text != null)
            {
                foreach (var t in teachers)
                {
                    if (item.Text == t.Name)
                    {
                        teacherRank = t.Rank;
                        teacherId = t.Id;
                        teacherFaculty = t.Faculty;
                    }
                }

                teacherName = item.Text;
                this.Hide();

                MainForm.Self.profilePage.UpdateInfo();
                MainForm.Self.profilePage.Show();
                MainForm.Self.profilePage.BringToFront();
            }
        }
    }
}
