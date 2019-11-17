using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RateMyP.WinForm.Forms.UserControls
    {
    public partial class HomePageControl : UserControl
        {
        public const int MaxComments = 10;

        public HomePageControl()
            {
            InitializeComponent();
            LoadRecentComments();
            }

        private void LoadRecentComments()
            {
            //using (var context = new RateMyPDbContext())
            //    {
            //    var comments = (from c in context.Comments
            //                    orderby c.DateCreated descending
            //                    select c).Take(MaxComments).ToList();
            //    RecentCommentsList.Clear();
            //    var items = comments.Select(c => new ListViewItem(c.Content)).ToArray();
            //    RecentCommentsList.Items.AddRange(items);
            //    }
            }
        }
    }
