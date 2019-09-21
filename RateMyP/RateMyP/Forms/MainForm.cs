using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RateMyP
    {
    public partial class MainForm : Form
        {

        public static MainForm Self;

        public MainForm()
        {
            InitializeComponent();
            Self = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            browsePage.Hide();
            ratePage.Hide();
            leaderboardPage.Hide();
            profilePage.Hide();
            landingPage.Show();
            landingPage.BringToFront();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NewsLabel_Click(object sender, EventArgs e)
        {

        }

        private void MainMenuPageButton_Click(object sender, EventArgs e)
        {
            leaderboardPage.Hide();
            browsePage.Hide();
            profilePage.Hide();
            ratePage.Hide();
            landingPage.Show();
            landingPage.BringToFront();
        }

        private void BrowsePageButton_Click(object sender, EventArgs e)
        {
            landingPage.Hide();
            leaderboardPage.Hide();
            ratePage.Hide();
            profilePage.Hide();
            browsePage.Show();
            browsePage.BringToFront();
        }

        public void RatePageButton_Click(object sender, EventArgs e)
        {
            landingPage.Hide();
            browsePage.Hide();
            leaderboardPage.Hide();
            profilePage.Hide();
            ratePage.Show();
            ratePage.BringToFront();
        }

        private void LeaderboardsPageButton_Click(object sender, EventArgs e)
        {
            landingPage.Hide();
            browsePage.Hide();
            ratePage.Hide();
            profilePage.Hide();
            leaderboardPage.Show();
            leaderboardPage.BringToFront();
        }

        public void ProfilePageButton_Click(object sender, EventArgs e)
        {
            landingPage.Hide();
            browsePage.Hide();
            ratePage.Hide();
            leaderboardPage.Hide();
            profilePage.Show();
            profilePage.BringToFront();
        }

    }
    }
