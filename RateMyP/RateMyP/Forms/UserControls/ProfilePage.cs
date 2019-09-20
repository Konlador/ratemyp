using System;
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
        private void InitDb(Guid sId)
        {
            if (sId != Guid.Empty)
            {
                var databaseConnection = new SQLDbConnection();
                databaseConnection.Clear();
                IStudentManager s_manager = new StudentManager(databaseConnection);
                var selStudent = s_manager.GetStudent(sId);
                sName.Text = selStudent.Name + " " + selStudent.Surname;
                sFaculty.Text = selStudent.Faculty;
                sStudies.Text = selStudent.Studies;
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
