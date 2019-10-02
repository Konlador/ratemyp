namespace RateMyP.Forms.UserControls
{
    partial class LeaderboardPage
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Historic Data1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Historic Data2");
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.topProfView = new System.Windows.Forms.ListView();
            this.tName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tRating = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.topCourseView = new System.Windows.Forms.ListView();
            this.historicDataView = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.TabIndex = 3;
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
            this.splitContainer.Panel2.Controls.Add(this.historicDataView);
            this.splitContainer.Size = new System.Drawing.Size(657, 549);
            this.splitContainer.SplitterDistance = 366;
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
            this.splitContainer1.Panel1.Controls.Add(this.topProfView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.topCourseView);
            this.splitContainer1.Size = new System.Drawing.Size(657, 366);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 0;
            // 
            // topProfView
            // 
            this.topProfView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tName,
            this.tRating});
            this.topProfView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topProfView.HideSelection = false;
            this.topProfView.Location = new System.Drawing.Point(0, 0);
            this.topProfView.Name = "topProfView";
            this.topProfView.Size = new System.Drawing.Size(328, 366);
            this.topProfView.TabIndex = 0;
            this.topProfView.UseCompatibleStateImageBehavior = false;
            this.topProfView.View = System.Windows.Forms.View.Details;
            // 
            // tName
            // 
            this.tName.Text = "Full Name";
            this.tName.Width = 248;
            // 
            // tRating
            // 
            this.tRating.Text = "Rating";
            this.tRating.Width = 76;
            // 
            // topCourseView
            // 
            this.topCourseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topCourseView.HideSelection = false;
            this.topCourseView.Location = new System.Drawing.Point(0, 0);
            this.topCourseView.Name = "topCourseView";
            this.topCourseView.Size = new System.Drawing.Size(325, 366);
            this.topCourseView.TabIndex = 0;
            this.topCourseView.UseCompatibleStateImageBehavior = false;
            this.topCourseView.View = System.Windows.Forms.View.List;
            // 
            // historicDataView
            // 
            this.historicDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historicDataView.HideSelection = false;
            this.historicDataView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.historicDataView.Location = new System.Drawing.Point(0, 0);
            this.historicDataView.Name = "historicDataView";
            this.historicDataView.Size = new System.Drawing.Size(657, 179);
            this.historicDataView.TabIndex = 0;
            this.historicDataView.UseCompatibleStateImageBehavior = false;
            this.historicDataView.View = System.Windows.Forms.View.List;
            // 
            // LeaderboardPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LeaderboardPage";
            this.Size = new System.Drawing.Size(663, 555);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView topProfView;
        private System.Windows.Forms.ListView topCourseView;
        private System.Windows.Forms.ListView historicDataView;
        private System.Windows.Forms.ColumnHeader tName;
        private System.Windows.Forms.ColumnHeader tRating;
    }
}
