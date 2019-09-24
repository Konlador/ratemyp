namespace RateMyP.Forms.UserControls
{
    partial class TeacherProfile
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
            this.teacherPicture = new System.Windows.Forms.PictureBox();
            this.teacherInfoPanel = new System.Windows.Forms.TableLayoutPanel();
            this.teacherNameLabel = new System.Windows.Forms.Label();
            this.teacherFacultyLabel = new System.Windows.Forms.Label();
            this.teacherRankLabel = new System.Windows.Forms.Label();
            this.teacherStudiesLabel = new System.Windows.Forms.Label();
            this.teacherInfoLabel = new System.Windows.Forms.Label();
            this.teacherReviewPanel = new System.Windows.Forms.TableLayoutPanel();
            this.teacherRateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.teacherPicture)).BeginInit();
            this.teacherInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // teacherPicture
            // 
            this.teacherPicture.Image = global::RateMyP.Properties.Resources.profile;
            this.teacherPicture.ImageLocation = "";
            this.teacherPicture.InitialImage = null;
            this.teacherPicture.Location = new System.Drawing.Point(3, 3);
            this.teacherPicture.Name = "teacherPicture";
            this.teacherPicture.Size = new System.Drawing.Size(225, 212);
            this.teacherPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.teacherPicture.TabIndex = 0;
            this.teacherPicture.TabStop = false;
            // 
            // teacherInfoPanel
            // 
            this.teacherInfoPanel.ColumnCount = 2;
            this.teacherInfoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.teacherInfoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.teacherInfoPanel.Controls.Add(this.teacherNameLabel, 0, 0);
            this.teacherInfoPanel.Controls.Add(this.teacherFacultyLabel, 0, 1);
            this.teacherInfoPanel.Controls.Add(this.teacherRankLabel, 0, 2);
            this.teacherInfoPanel.Controls.Add(this.teacherStudiesLabel, 0, 3);
            this.teacherInfoPanel.Controls.Add(this.teacherInfoLabel, 0, 4);
            this.teacherInfoPanel.Controls.Add(this.teacherRateButton, 1, 5);
            this.teacherInfoPanel.Location = new System.Drawing.Point(234, 3);
            this.teacherInfoPanel.Name = "teacherInfoPanel";
            this.teacherInfoPanel.RowCount = 6;
            this.teacherInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.teacherInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.teacherInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.teacherInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.teacherInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.teacherInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.teacherInfoPanel.Size = new System.Drawing.Size(426, 212);
            this.teacherInfoPanel.TabIndex = 1;
            // 
            // teacherNameLabel
            // 
            this.teacherNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.teacherNameLabel.AutoSize = true;
            this.teacherNameLabel.Location = new System.Drawing.Point(3, 5);
            this.teacherNameLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.teacherNameLabel.Name = "teacherNameLabel";
            this.teacherNameLabel.Size = new System.Drawing.Size(38, 13);
            this.teacherNameLabel.TabIndex = 0;
            this.teacherNameLabel.Text = "Name:";
            // 
            // teacherFacultyLabel
            // 
            this.teacherFacultyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.teacherFacultyLabel.AutoSize = true;
            this.teacherFacultyLabel.Location = new System.Drawing.Point(3, 28);
            this.teacherFacultyLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.teacherFacultyLabel.Name = "teacherFacultyLabel";
            this.teacherFacultyLabel.Size = new System.Drawing.Size(44, 13);
            this.teacherFacultyLabel.TabIndex = 1;
            this.teacherFacultyLabel.Text = "Faculty:";
            this.teacherFacultyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teacherRankLabel
            // 
            this.teacherRankLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.teacherRankLabel.AutoSize = true;
            this.teacherRankLabel.Location = new System.Drawing.Point(3, 51);
            this.teacherRankLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.teacherRankLabel.Name = "teacherRankLabel";
            this.teacherRankLabel.Size = new System.Drawing.Size(56, 13);
            this.teacherRankLabel.TabIndex = 2;
            this.teacherRankLabel.Text = "Degree(s):";
            this.teacherRankLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teacherStudiesLabel
            // 
            this.teacherStudiesLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.teacherStudiesLabel.AutoSize = true;
            this.teacherStudiesLabel.Location = new System.Drawing.Point(3, 74);
            this.teacherStudiesLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.teacherStudiesLabel.Name = "teacherStudiesLabel";
            this.teacherStudiesLabel.Size = new System.Drawing.Size(45, 13);
            this.teacherStudiesLabel.TabIndex = 3;
            this.teacherStudiesLabel.Text = "Studies:";
            this.teacherStudiesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teacherInfoLabel
            // 
            this.teacherInfoLabel.AutoSize = true;
            this.teacherInfoLabel.Location = new System.Drawing.Point(3, 95);
            this.teacherInfoLabel.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.teacherInfoLabel.Name = "teacherInfoLabel";
            this.teacherInfoLabel.Size = new System.Drawing.Size(68, 13);
            this.teacherInfoLabel.TabIndex = 4;
            this.teacherInfoLabel.Text = "General Info:";
            this.teacherInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // teacherReviewPanel
            // 
            this.teacherReviewPanel.ColumnCount = 2;
            this.teacherReviewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.teacherReviewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.teacherReviewPanel.Location = new System.Drawing.Point(3, 221);
            this.teacherReviewPanel.Name = "teacherReviewPanel";
            this.teacherReviewPanel.RowCount = 2;
            this.teacherReviewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.teacherReviewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.teacherReviewPanel.Size = new System.Drawing.Size(657, 331);
            this.teacherReviewPanel.TabIndex = 2;
            // 
            // teacherRateButton
            // 
            this.teacherRateButton.Location = new System.Drawing.Point(286, 165);
            this.teacherRateButton.Margin = new System.Windows.Forms.Padding(73, 26, 0, 0);
            this.teacherRateButton.Name = "teacherRateButton";
            this.teacherRateButton.Size = new System.Drawing.Size(75, 23);
            this.teacherRateButton.TabIndex = 5;
            this.teacherRateButton.Text = "Rate Now!";
            this.teacherRateButton.UseVisualStyleBackColor = true;
            this.teacherRateButton.Click += new System.EventHandler(this.TeacherRateButton_Click);
            // 
            // TeacherProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.teacherReviewPanel);
            this.Controls.Add(this.teacherInfoPanel);
            this.Controls.Add(this.teacherPicture);
            this.Name = "TeacherProfile";
            this.Size = new System.Drawing.Size(663, 555);
            ((System.ComponentModel.ISupportInitialize)(this.teacherPicture)).EndInit();
            this.teacherInfoPanel.ResumeLayout(false);
            this.teacherInfoPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox teacherPicture;
        private System.Windows.Forms.TableLayoutPanel teacherInfoPanel;
        private System.Windows.Forms.Label teacherNameLabel;
        private System.Windows.Forms.Label teacherFacultyLabel;
        private System.Windows.Forms.Label teacherRankLabel;
        private System.Windows.Forms.Label teacherStudiesLabel;
        private System.Windows.Forms.Label teacherInfoLabel;
        private System.Windows.Forms.TableLayoutPanel teacherReviewPanel;
        private System.Windows.Forms.Button teacherRateButton;
    }
}
