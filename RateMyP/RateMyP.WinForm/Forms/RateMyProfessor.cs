using System;
using System.Windows.Forms;
using MetroSet_UI.Forms;

namespace RateMyP.WinForm.Forms
    {
    public partial class RateMyProfessor : MetroSetForm
        {
        public static RateMyProfessor self;

        public RateMyProfessor()
            {
            InitializeComponent();
            self = this;
            }

        private void MenuTabControl_MouseClick(object sender, MouseEventArgs e)
            {
            if (e.Button == MouseButtons.Right && MenuTabControl.SelectedTab == TabPageTeacherProfile)
                TabContextMenu.Show(MousePosition);
            }

        private void closeToolStrip_Click(object sender, EventArgs e)
            {
            MenuTabControl.Speed = 1000;
            MenuTabControl.SelectTab(MenuTabControl.SelectedIndex - 1);
            MenuTabControl.TabPages.Remove(TabPageTeacherProfile);
            MenuTabControl.Speed = 100;
            TabContextMenu.Hide();
            }
        }
    }
