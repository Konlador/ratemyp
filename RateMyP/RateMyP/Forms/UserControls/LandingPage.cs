using System;
using System.Windows.Forms;

namespace RateMyP.Forms.UserControls
    {
    public partial class LandingPage : UserControl
        {
        public int maxComments = 10;
        public LandingPage()
            {
            InitializeComponent();
            //InitializeNewComments();
            }

        private void TrendProfListView_SelectedIndexChanged(object sender, EventArgs e)
            {

            }

        /*
        //Connects to comment data and displays their content in the ListView. Displays up to maxComments most recent comments.
        private void InitializeNewComments()
            {
            var commentManager = new CommentManager();
            var comments = commentManager.GetAll();
            recentCommentsListView.Items.Clear();
            for (int i = comments.Count - 1; i > 0; i--)
                {
                var lvi = new ListViewItem(comments[i].Content);
                recentCommentsListView.Items.Add(lvi);
                if (recentCommentsListView.Items.Count > maxComments)
                    break;
                }
            }
            */
        }
    }
