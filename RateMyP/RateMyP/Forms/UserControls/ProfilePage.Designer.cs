namespace RateMyP.Forms.UserControls
{
    partial class ProfilePage
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Activity1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Activity2");
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.profilePictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.profileDegreeLabel = new System.Windows.Forms.Label();
            this.profileGenderLabel = new System.Windows.Forms.Label();
            this.profileFacultyLabel = new System.Windows.Forms.Label();
            this.profileNameLabel = new System.Windows.Forms.Label();
            this.activityView = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.activityView);
            this.splitContainer.Size = new System.Drawing.Size(657, 549);
            this.splitContainer.SplitterDistance = 212;
            this.splitContainer.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.profilePictureBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(657, 212);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 0;
            // 
            // profilePictureBox
            // 
            this.profilePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profilePictureBox.Image = global::RateMyP.Properties.Resources.profile;
            this.profilePictureBox.Location = new System.Drawing.Point(0, 0);
            this.profilePictureBox.Name = "profilePictureBox";
            this.profilePictureBox.Size = new System.Drawing.Size(225, 212);
            this.profilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePictureBox.TabIndex = 0;
            this.profilePictureBox.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.profileDegreeLabel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.profileGenderLabel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.profileFacultyLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.profileNameLabel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(428, 212);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // profileDegreeLabel
            // 
            this.profileDegreeLabel.AutoSize = true;
            this.profileDegreeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.profileDegreeLabel.Location = new System.Drawing.Point(3, 60);
            this.profileDegreeLabel.Name = "profileDegreeLabel";
            this.profileDegreeLabel.Size = new System.Drawing.Size(48, 20);
            this.profileDegreeLabel.TabIndex = 3;
            this.profileDegreeLabel.Text = "Degree: ";
            // 
            // profileGenderLabel
            // 
            this.profileGenderLabel.AutoSize = true;
            this.profileGenderLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.profileGenderLabel.Location = new System.Drawing.Point(3, 40);
            this.profileGenderLabel.Name = "profileGenderLabel";
            this.profileGenderLabel.Size = new System.Drawing.Size(48, 20);
            this.profileGenderLabel.TabIndex = 2;
            this.profileGenderLabel.Text = "Gender: ";
            // 
            // profileFacultyLabel
            // 
            this.profileFacultyLabel.AutoSize = true;
            this.profileFacultyLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.profileFacultyLabel.Location = new System.Drawing.Point(3, 20);
            this.profileFacultyLabel.Name = "profileFacultyLabel";
            this.profileFacultyLabel.Size = new System.Drawing.Size(47, 20);
            this.profileFacultyLabel.TabIndex = 1;
            this.profileFacultyLabel.Text = "Faculty: ";
            // 
            // profileNameLabel
            // 
            this.profileNameLabel.AutoSize = true;
            this.profileNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.profileNameLabel.Location = new System.Drawing.Point(3, 0);
            this.profileNameLabel.Name = "profileNameLabel";
            this.profileNameLabel.Size = new System.Drawing.Size(41, 20);
            this.profileNameLabel.TabIndex = 0;
            this.profileNameLabel.Text = "Name: ";
            // 
            // activityView
            // 
            this.activityView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityView.HideSelection = false;
            this.activityView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.activityView.Location = new System.Drawing.Point(0, 0);
            this.activityView.Name = "activityView";
            this.activityView.Size = new System.Drawing.Size(657, 333);
            this.activityView.TabIndex = 0;
            this.activityView.UseCompatibleStateImageBehavior = false;
            this.activityView.View = System.Windows.Forms.View.List;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 663F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(663, 555);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // ProfilePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProfilePage";
            this.Size = new System.Drawing.Size(663, 555);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox profilePictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label profileDegreeLabel;
        private System.Windows.Forms.Label profileGenderLabel;
        private System.Windows.Forms.Label profileFacultyLabel;
        private System.Windows.Forms.Label profileNameLabel;
        private System.Windows.Forms.ListView activityView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
