namespace RateMyP.Forms.UserControls
{
    partial class RatePage
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "#tag1"}, -1, System.Drawing.SystemColors.MenuText, System.Drawing.SystemColors.ScrollBar, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "#tag2"}, -1, System.Drawing.SystemColors.MenuText, System.Drawing.SystemColors.ScrollBar, null);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.searchRateButton = new System.Windows.Forms.Button();
            this.searchBoxRate = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.profPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ratePageNameLabel = new System.Windows.Forms.Label();
            this.ratePageDegreeLabel = new System.Windows.Forms.Label();
            this.ratePageOverallMarkLabel = new System.Windows.Forms.Label();
            this.ratePageDifficultyLabel = new System.Windows.Forms.Label();
            this.ratePageTakeAgainLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tagsLabelsView = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profPictureBox)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.splitContainer.Panel1.Controls.Add(this.searchRateButton);
            this.splitContainer.Panel1.Controls.Add(this.searchBoxRate);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer.Size = new System.Drawing.Size(657, 549);
            this.splitContainer.SplitterDistance = 80;
            this.splitContainer.TabIndex = 1;
            // 
            // searchRateButton
            // 
            this.searchRateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchRateButton.Location = new System.Drawing.Point(574, 27);
            this.searchRateButton.Name = "searchRateButton";
            this.searchRateButton.Size = new System.Drawing.Size(75, 23);
            this.searchRateButton.TabIndex = 1;
            this.searchRateButton.Text = "Search";
            this.searchRateButton.UseVisualStyleBackColor = true;
            this.searchRateButton.Click += new System.EventHandler(this.SearchRateButton_Click);
            // 
            // searchBoxRate
            // 
            this.searchBoxRate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBoxRate.Location = new System.Drawing.Point(3, 29);
            this.searchBoxRate.Name = "searchBoxRate";
            this.searchBoxRate.Size = new System.Drawing.Size(565, 20);
            this.searchBoxRate.TabIndex = 0;
            this.searchBoxRate.Text = "Search";
            this.searchBoxRate.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(657, 465);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.profPictureBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer2.Size = new System.Drawing.Size(657, 219);
            this.splitContainer2.SplitterDistance = 192;
            this.splitContainer2.TabIndex = 0;
            // 
            // profPictureBox
            // 
            this.profPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.profPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profPictureBox.Image = global::RateMyP.Properties.Resources.profile;
            this.profPictureBox.Location = new System.Drawing.Point(0, 0);
            this.profPictureBox.Name = "profPictureBox";
            this.profPictureBox.Size = new System.Drawing.Size(192, 219);
            this.profPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profPictureBox.TabIndex = 0;
            this.profPictureBox.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.ratePageNameLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ratePageDegreeLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ratePageOverallMarkLabel, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.ratePageDifficultyLabel, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.ratePageTakeAgainLabel, 0, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(461, 219);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // ratePageNameLabel
            // 
            this.ratePageNameLabel.AutoSize = true;
            this.ratePageNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ratePageNameLabel.Location = new System.Drawing.Point(3, 0);
            this.ratePageNameLabel.Name = "ratePageNameLabel";
            this.ratePageNameLabel.Size = new System.Drawing.Size(41, 19);
            this.ratePageNameLabel.TabIndex = 0;
            this.ratePageNameLabel.Text = "Name: ";
            // 
            // ratePageDegreeLabel
            // 
            this.ratePageDegreeLabel.AutoSize = true;
            this.ratePageDegreeLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ratePageDegreeLabel.Location = new System.Drawing.Point(3, 19);
            this.ratePageDegreeLabel.Name = "ratePageDegreeLabel";
            this.ratePageDegreeLabel.Size = new System.Drawing.Size(48, 19);
            this.ratePageDegreeLabel.TabIndex = 1;
            this.ratePageDegreeLabel.Text = "Degree: ";
            // 
            // ratePageOverallMarkLabel
            // 
            this.ratePageOverallMarkLabel.AutoSize = true;
            this.ratePageOverallMarkLabel.Location = new System.Drawing.Point(3, 76);
            this.ratePageOverallMarkLabel.Name = "ratePageOverallMarkLabel";
            this.ratePageOverallMarkLabel.Size = new System.Drawing.Size(70, 13);
            this.ratePageOverallMarkLabel.TabIndex = 4;
            this.ratePageOverallMarkLabel.Text = "Overall Mark:";
            // 
            // ratePageDifficultyLabel
            // 
            this.ratePageDifficultyLabel.AutoSize = true;
            this.ratePageDifficultyLabel.Location = new System.Drawing.Point(3, 95);
            this.ratePageDifficultyLabel.Name = "ratePageDifficultyLabel";
            this.ratePageDifficultyLabel.Size = new System.Drawing.Size(35, 13);
            this.ratePageDifficultyLabel.TabIndex = 5;
            this.ratePageDifficultyLabel.Text = "label2";
            // 
            // ratePageTakeAgainLabel
            // 
            this.ratePageTakeAgainLabel.AutoSize = true;
            this.ratePageTakeAgainLabel.Location = new System.Drawing.Point(3, 194);
            this.ratePageTakeAgainLabel.Name = "ratePageTakeAgainLabel";
            this.ratePageTakeAgainLabel.Size = new System.Drawing.Size(35, 13);
            this.ratePageTakeAgainLabel.TabIndex = 6;
            this.ratePageTakeAgainLabel.Text = "label3";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tagsLabelsView, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(657, 242);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tagsLabelsView
            // 
            this.tagsLabelsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagsLabelsView.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.tagsLabelsView.HideSelection = false;
            this.tagsLabelsView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.tagsLabelsView.Location = new System.Drawing.Point(331, 3);
            this.tagsLabelsView.Name = "tagsLabelsView";
            this.tagsLabelsView.Size = new System.Drawing.Size(323, 236);
            this.tagsLabelsView.TabIndex = 0;
            this.tagsLabelsView.UseCompatibleStateImageBehavior = false;
            // 
            // RatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RatePage";
            this.Size = new System.Drawing.Size(663, 555);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profPictureBox)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox searchBoxRate;
        private System.Windows.Forms.Button searchRateButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox profPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListView tagsLabelsView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label ratePageNameLabel;
        private System.Windows.Forms.Label ratePageDegreeLabel;
        private System.Windows.Forms.Label ratePageOverallMarkLabel;
        private System.Windows.Forms.Label ratePageDifficultyLabel;
        private System.Windows.Forms.Label ratePageTakeAgainLabel;
    }
}
