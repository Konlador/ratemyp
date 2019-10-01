using System;
using System.Linq;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
    {
    public partial class LandingPage : UserControl
        {
        public const int MaxComments = 10;

        public LandingPage()
            {
            InitializeComponent();
            LoadRecentComments();
            }

        private void TrendProfListView_SelectedIndexChanged(object sender, EventArgs e)
            {

            }

        // Displays up to maxComments most recent comments.
        private void LoadRecentComments()
            {
            using (var context = new RateMyPDbContext())
                {
                var comments = (from c in context.Comments
                                orderby c.DateCreated descending
                                select c).Take(MaxComments).ToList();
                RecentCommentsListView.Clear();
                var items = comments.Select(c => new ListViewItem(c.Content)).ToArray();
                RecentCommentsListView.Items.AddRange(items);
                }
            }
        }
    }
