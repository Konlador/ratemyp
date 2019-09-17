using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RateMyP.Forms;

namespace RateMyP
    {
    public partial class MainForm : Form
        {
        private BrowseForm m_browseForm;
        private RateForm m_rateForm;


        public MainForm()
            {
            InitializeComponent();
            }

        private void MainForm_Load(object sender, EventArgs e)
            {
            m_browseForm = new BrowseForm();
            m_rateForm = new RateForm();
            }

        private void navigationButtonBrowse_Click(object sender, EventArgs e)
            {
            m_browseForm.Location = this.Location;
            m_browseForm.StartPosition = FormStartPosition.Manual;
            m_browseForm.FormClosing += delegate { this.Show(); };
            m_browseForm.Show();
            this.Hide();
            }

        private void navigationButtonRate_Click(object sender, EventArgs e)
            {
            m_rateForm.Location = this.Location;
            m_rateForm.StartPosition = FormStartPosition.Manual;
            m_rateForm.FormClosing += delegate { this.Show(); };
            m_rateForm.Show();
            this.Hide();
            }

        private void navigationButtonLeaderboard_Click(object sender, EventArgs e)
            {

            }

        private void navigationButtonUserProfile_Click(object sender, EventArgs e)
            {

            }
        }
    }
