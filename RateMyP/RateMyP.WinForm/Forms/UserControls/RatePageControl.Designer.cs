using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RateMyP.WinForm.Forms.UserControls
    {
    partial class RatePageControl
        {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
            {
            if (disposing && (components != null))
                {
                components.Dispose();
                }
            base.Dispose(disposing);
            }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
            {
            this.TeacherNameLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.TeacherInfoLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.TeacherAcademicInfoLabel = new MetroSet_UI.Controls.MetroSetLabel();
            this.RateCommentTextBox = new MetroSet_UI.Controls.MetroSetTextBox();
            this.TeacherProfilePictureBox = new System.Windows.Forms.PictureBox();
            this.RateStarImageOne = new System.Windows.Forms.PictureBox();
            this.RateStarImageTwo = new System.Windows.Forms.PictureBox();
            this.RateStarImageThree = new System.Windows.Forms.PictureBox();
            this.RateStarImageFour = new System.Windows.Forms.PictureBox();
            this.RateStarImageFive = new System.Windows.Forms.PictureBox();
            this.RatePageButtonSend = new MetroSet_UI.Controls.MetroSetButton();
            ((System.ComponentModel.ISupportInitialize)(this.TeacherProfilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageTwo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageThree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageFour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageFive)).BeginInit();
            this.SuspendLayout();
            // 
            // TeacherNameLabel
            // 
            this.TeacherNameLabel.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeacherNameLabel.Location = new System.Drawing.Point(250, 25);
            this.TeacherNameLabel.Name = "TeacherNameLabel";
            this.TeacherNameLabel.Size = new System.Drawing.Size(300, 50);
            this.TeacherNameLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherNameLabel.StyleManager = null;
            this.TeacherNameLabel.TabIndex = 2;
            this.TeacherNameLabel.Text = "NAME?";
            this.TeacherNameLabel.ThemeAuthor = "Narwin";
            this.TeacherNameLabel.ThemeName = "MetroLite";
            // 
            // TeacherInfoLabel
            // 
            this.TeacherInfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TeacherInfoLabel.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeacherInfoLabel.Location = new System.Drawing.Point(250, 125);
            this.TeacherInfoLabel.Name = "TeacherInfoLabel";
            this.TeacherInfoLabel.Size = new System.Drawing.Size(470, 100);
            this.TeacherInfoLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherInfoLabel.StyleManager = null;
            this.TeacherInfoLabel.TabIndex = 5;
            this.TeacherInfoLabel.Text = "DESCRIPTION, INFO, ETC?";
            this.TeacherInfoLabel.ThemeAuthor = "Narwin";
            this.TeacherInfoLabel.ThemeName = "MetroLite";
            // 
            // TeacherAcademicInfoLabel
            // 
            this.TeacherAcademicInfoLabel.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeacherAcademicInfoLabel.Location = new System.Drawing.Point(250, 65);
            this.TeacherAcademicInfoLabel.Name = "TeacherAcademicInfoLabel";
            this.TeacherAcademicInfoLabel.Size = new System.Drawing.Size(200, 25);
            this.TeacherAcademicInfoLabel.Style = MetroSet_UI.Design.Style.Light;
            this.TeacherAcademicInfoLabel.StyleManager = null;
            this.TeacherAcademicInfoLabel.TabIndex = 4;
            this.TeacherAcademicInfoLabel.Text = "ACADEMIC INFO?";
            this.TeacherAcademicInfoLabel.ThemeAuthor = "Narwin";
            this.TeacherAcademicInfoLabel.ThemeName = "MetroLite";
            // 
            // RateCommentTextBox
            // 
            this.RateCommentTextBox.AutoCompleteCustomSource = null;
            this.RateCommentTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.RateCommentTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.RateCommentTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.RateCommentTextBox.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.RateCommentTextBox.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.RateCommentTextBox.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.RateCommentTextBox.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RateCommentTextBox.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.RateCommentTextBox.Image = null;
            this.RateCommentTextBox.Lines = new string[0];
            this.RateCommentTextBox.Location = new System.Drawing.Point(25, 350);
            this.RateCommentTextBox.MaxLength = 750;
            this.RateCommentTextBox.Multiline = true;
            this.RateCommentTextBox.Name = "RateCommentTextBox";
            this.RateCommentTextBox.ReadOnly = false;
            this.RateCommentTextBox.Size = new System.Drawing.Size(695, 225);
            this.RateCommentTextBox.Style = MetroSet_UI.Design.Style.Light;
            this.RateCommentTextBox.StyleManager = null;
            this.RateCommentTextBox.TabIndex = 6;
            this.RateCommentTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.RateCommentTextBox.ThemeAuthor = "Narwin";
            this.RateCommentTextBox.ThemeName = "MetroLite";
            this.RateCommentTextBox.UseSystemPasswordChar = false;
            this.RateCommentTextBox.WatermarkText = "Write your astonishing review here...";
            // 
            // TeacherProfilePictureBox
            // 
            this.TeacherProfilePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TeacherProfilePictureBox.Image = global::RateMyP.WinForm.Properties.Resources.everydaywestrayfurtherfromgodslight;
            this.TeacherProfilePictureBox.Location = new System.Drawing.Point(25, 25);
            this.TeacherProfilePictureBox.Name = "TeacherProfilePictureBox";
            this.TeacherProfilePictureBox.Size = new System.Drawing.Size(200, 200);
            this.TeacherProfilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.TeacherProfilePictureBox.TabIndex = 1;
            this.TeacherProfilePictureBox.TabStop = false;
            // 
            // RateStarImageOne
            // 
            this.RateStarImageOne.Image = global::RateMyP.WinForm.Properties.Resources.star_inactive;
            this.RateStarImageOne.Location = new System.Drawing.Point(25, 275);
            this.RateStarImageOne.Name = "RateStarImageOne";
            this.RateStarImageOne.Size = new System.Drawing.Size(50, 50);
            this.RateStarImageOne.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RateStarImageOne.TabIndex = 7;
            this.RateStarImageOne.TabStop = false;
            this.RateStarImageOne.Click += new System.EventHandler(this.RateStarList_Click);
            this.RateStarImageOne.MouseEnter += new System.EventHandler(this.RateStarList_MouseEnter);
            this.RateStarImageOne.MouseLeave += new System.EventHandler(this.RateStarList_MouseLeave);
            // 
            // RateStarImageTwo
            // 
            this.RateStarImageTwo.Image = global::RateMyP.WinForm.Properties.Resources.star_inactive;
            this.RateStarImageTwo.Location = new System.Drawing.Point(75, 275);
            this.RateStarImageTwo.Name = "RateStarImageTwo";
            this.RateStarImageTwo.Size = new System.Drawing.Size(50, 50);
            this.RateStarImageTwo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RateStarImageTwo.TabIndex = 8;
            this.RateStarImageTwo.TabStop = false;
            this.RateStarImageTwo.Click += new System.EventHandler(this.RateStarList_Click);
            this.RateStarImageTwo.MouseEnter += new System.EventHandler(this.RateStarList_MouseEnter);
            this.RateStarImageTwo.MouseLeave += new System.EventHandler(this.RateStarList_MouseLeave);
            // 
            // RateStarImageThree
            // 
            this.RateStarImageThree.Image = global::RateMyP.WinForm.Properties.Resources.star_inactive;
            this.RateStarImageThree.Location = new System.Drawing.Point(125, 275);
            this.RateStarImageThree.Name = "RateStarImageThree";
            this.RateStarImageThree.Size = new System.Drawing.Size(50, 50);
            this.RateStarImageThree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RateStarImageThree.TabIndex = 9;
            this.RateStarImageThree.TabStop = false;
            this.RateStarImageThree.Click += new System.EventHandler(this.RateStarList_Click);
            this.RateStarImageThree.MouseEnter += new System.EventHandler(this.RateStarList_MouseEnter);
            this.RateStarImageThree.MouseLeave += new System.EventHandler(this.RateStarList_MouseLeave);
            // 
            // RateStarImageFour
            // 
            this.RateStarImageFour.Image = global::RateMyP.WinForm.Properties.Resources.star_inactive;
            this.RateStarImageFour.Location = new System.Drawing.Point(175, 275);
            this.RateStarImageFour.Name = "RateStarImageFour";
            this.RateStarImageFour.Size = new System.Drawing.Size(50, 50);
            this.RateStarImageFour.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RateStarImageFour.TabIndex = 10;
            this.RateStarImageFour.TabStop = false;
            this.RateStarImageFour.Click += new System.EventHandler(this.RateStarList_Click);
            this.RateStarImageFour.MouseEnter += new System.EventHandler(this.RateStarList_MouseEnter);
            this.RateStarImageFour.MouseLeave += new System.EventHandler(this.RateStarList_MouseLeave);
            // 
            // RateStarImageFive
            // 
            this.RateStarImageFive.Image = global::RateMyP.WinForm.Properties.Resources.star_inactive;
            this.RateStarImageFive.Location = new System.Drawing.Point(225, 275);
            this.RateStarImageFive.Name = "RateStarImageFive";
            this.RateStarImageFive.Size = new System.Drawing.Size(50, 50);
            this.RateStarImageFive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RateStarImageFive.TabIndex = 11;
            this.RateStarImageFive.TabStop = false;
            this.RateStarImageFive.Click += new System.EventHandler(this.RateStarList_Click);
            this.RateStarImageFive.MouseEnter += new System.EventHandler(this.RateStarList_MouseEnter);
            this.RateStarImageFive.MouseLeave += new System.EventHandler(this.RateStarList_MouseLeave);
            // 
            // RatePageButtonSend
            // 
            this.RatePageButtonSend.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.RatePageButtonSend.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.RatePageButtonSend.DisabledForeColor = System.Drawing.Color.Gray;
            this.RatePageButtonSend.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RatePageButtonSend.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.RatePageButtonSend.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(207)))), ((int)(((byte)(255)))));
            this.RatePageButtonSend.HoverTextColor = System.Drawing.Color.White;
            this.RatePageButtonSend.Location = new System.Drawing.Point(595, 275);
            this.RatePageButtonSend.Name = "RatePageButtonSend";
            this.RatePageButtonSend.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.RatePageButtonSend.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.RatePageButtonSend.NormalTextColor = System.Drawing.Color.White;
            this.RatePageButtonSend.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.RatePageButtonSend.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(147)))), ((int)(((byte)(195)))));
            this.RatePageButtonSend.PressTextColor = System.Drawing.Color.White;
            this.RatePageButtonSend.Size = new System.Drawing.Size(125, 50);
            this.RatePageButtonSend.Style = MetroSet_UI.Design.Style.Light;
            this.RatePageButtonSend.StyleManager = null;
            this.RatePageButtonSend.TabIndex = 12;
            this.RatePageButtonSend.Text = "Send Feedback";
            this.RatePageButtonSend.ThemeAuthor = "Narwin";
            this.RatePageButtonSend.ThemeName = "MetroLite";
            this.RatePageButtonSend.Click += new System.EventHandler(this.RatePageButtonSend_Click);
            // 
            // RatePageControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.RatePageButtonSend);
            this.Controls.Add(this.RateStarImageFive);
            this.Controls.Add(this.RateStarImageFour);
            this.Controls.Add(this.RateStarImageThree);
            this.Controls.Add(this.RateStarImageTwo);
            this.Controls.Add(this.RateStarImageOne);
            this.Controls.Add(this.RateCommentTextBox);
            this.Controls.Add(this.TeacherInfoLabel);
            this.Controls.Add(this.TeacherAcademicInfoLabel);
            this.Controls.Add(this.TeacherNameLabel);
            this.Controls.Add(this.TeacherProfilePictureBox);
            this.Name = "RatePageControl";
            this.Size = new System.Drawing.Size(1200, 600);
            ((System.ComponentModel.ISupportInitialize)(this.TeacherProfilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageTwo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageThree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageFour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RateStarImageFive)).EndInit();
            this.ResumeLayout(false);

            }

        #endregion

        private System.Windows.Forms.PictureBox TeacherProfilePictureBox;
        private MetroSet_UI.Controls.MetroSetLabel TeacherNameLabel;
        private MetroSet_UI.Controls.MetroSetLabel TeacherInfoLabel;
        private MetroSet_UI.Controls.MetroSetLabel TeacherAcademicInfoLabel;
        private MetroSet_UI.Controls.MetroSetTextBox RateCommentTextBox;
        public System.Windows.Forms.PictureBox RateStarImageOne;
        private System.Windows.Forms.PictureBox RateStarImageTwo;
        private System.Windows.Forms.PictureBox RateStarImageThree;
        private System.Windows.Forms.PictureBox RateStarImageFour;
        private System.Windows.Forms.PictureBox RateStarImageFive;

        private List<System.Windows.Forms.PictureBox> RateStarList;
        private MetroSet_UI.Controls.MetroSetButton RatePageButtonSend;
        }
    }
