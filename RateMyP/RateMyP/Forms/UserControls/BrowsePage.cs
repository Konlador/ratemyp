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
    public partial class BrowsePage : UserControl
    {
        SQLDbConnection databaseConnection = new SQLDbConnection();

        public BrowsePage()
        {
            InitializeComponent();
            InitializeTeacherListView();
        }

        // Connects to the data, gets all teacher class data and displays it in the ListView.
        private void InitializeTeacherListView()
        {
            
            var teacherManager = new TeacherManager(databaseConnection);
            var teachers = teacherManager.GetAllTeachers();
            profListView.Items.Clear();
            foreach (var teacher in teachers)
            {
                var row = new string[] { $"{teacher.Name} {teacher.Surname}", teacher.Rank.ToString() };
                var lvi = new ListViewItem(row);
                profListView.Items.Add(lvi);
            }
        }

        private void BrowseSearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }
        // Performs a search by comparing search box contents with the full names of the teachers in the profListView by using 'Contains' string method.
        // The list is shortened to match the search terms
        private void Search()
        {
            if (browseSearchBox.Text != "")
            {
                for (int i = profListView.Items.Count - 1; i >= 0; i--)
                {
                    var item = profListView.Items[i];
                    if (item.Text.ToLower().Contains(browseSearchBox.Text.ToLower()))
                    {
                        item.BackColor = SystemColors.Highlight;
                        item.ForeColor = SystemColors.HighlightText;
                    }
                    else
                    {
                        profListView.Items.Remove(item);
                    }
                }
                if (profListView.SelectedItems.Count == 1)
                {
                    profListView.Focus();
                }
            }
            else
                InitializeTeacherListView();
        }

        private void ProfListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = profListView.SelectedItems[0];
            if (selectedItem.Text != null)
            {
                var teacherManager = new TeacherManager(databaseConnection);
                var teachers = teacherManager.GetAllTeachers();
                string fullName;
                foreach (var teacher in teachers)
                {
                    fullName = $"{teacher.Name} {teacher.Surname}";
                    if (selectedItem.Text == fullName)
                    {
                        var guid = teacher.Id;
                        this.Hide();
                        MainForm.self.teacherProfilePage.UpdateInfo(fullName, teacher.Faculty, teacher.Studies, teacher.Rank.ToString(), 
                                                                    teacher.Description, teacher.Id);
                        MainForm.self.teacherProfilePage.Show();
                        MainForm.self.teacherProfilePage.BringToFront();
                        break;
                    }
                }
            }
        }
        //TODO: Filtering
    }
}
