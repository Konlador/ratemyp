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
        public BrowsePage()
        {
            InitializeComponent();
            InitProfListView();
        }

        //private void ProfListView_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var selectedItem = (Teacher)profListView.SelectedItems[0].Tag;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        // Connects to the data, gets all teacher class data and displays it in the ListView.
        private void InitProfListView()
        {
            var databaseConnection = new SQLDbConnection();
            databaseConnection.Clear();
            ITeacherManager t_manager = new TeacherManager(databaseConnection);
            var teachers = t_manager.GetAllTeachers();
            profListView.Items.Clear();
            foreach (var Teacher in teachers)
            {
                var row = new string[] { Teacher.Name + " " + Teacher.Surname, Teacher.Rank.ToString() };
                var lvi = new ListViewItem(row) { Tag = Teacher };
                profListView.Items.Add(lvi);
            }

            //test code for test list and class
            //var people = GetPeopleList();
            //profListView.Items.Clear();
            //foreach (var person in people)
            //{
            //    var row = new string[] { person.FN + " " + person.LN };
            //    var lvi = new ListViewItem(row);
            //    profListView.Items.Add(lvi);
            //}
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
                InitProfListView();
        }

        //TODO: Filtering

        //test list
        //private List<Person> GetPeopleList()
        //{
        //    var list = new List<Person>
        //    {
        //        new Person() { FN = "lis", LN = "bon" },
        //        new Person() { FN = "lis", LN = "bon" },
        //        new Person() { FN = "lis", LN = "bon" },
        //        new Person() { FN = "wasc", LN = "bytbv" },
        //        new Person() { FN = "wasc", LN = "bytbv" },
        //        new Person() { FN = "wasc", LN = "bytbv" }
        //    };
        //    return list;
        //}
    }

    //test class
    //internal class Person
    //{
    //    public Person()
    //    {
    //        Id = new Random().Next(1, 10000);
    //    }

    //    public int Id { get; set; }
    //    public string FN { get; set; }
    //    public string LN { get; set; }
    //}
}
