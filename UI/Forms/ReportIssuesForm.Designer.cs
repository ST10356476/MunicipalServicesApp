namespace MunicipalServicesApp.UI.Forms
{
    partial class ReportIssuesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Form Controls - Professional Clean Design
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.Label locationHelpLabel;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label categoryHelpLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Label characterCountLabel;
        private System.Windows.Forms.RichTextBox descriptionRichTextBox;
        private System.Windows.Forms.Label descriptionHelpLabel;
        private System.Windows.Forms.Panel attachmentPanel;
        private System.Windows.Forms.Label attachmentLabel;
        private System.Windows.Forms.Button attachMediaBtn;
        private System.Windows.Forms.Button clearAttachmentBtn;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Panel progressPanel;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.ProgressBar formProgressBar;
        private System.Windows.Forms.Label progressPercentLabel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label footerLabel;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.formPanel = new System.Windows.Forms.Panel();
            this.descriptionHelpLabel = new System.Windows.Forms.Label();
            this.categoryHelpLabel = new System.Windows.Forms.Label();
            this.locationHelpLabel = new System.Windows.Forms.Label();
            this.descriptionRichTextBox = new System.Windows.Forms.RichTextBox();
            this.characterCountLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.locationLabel = new System.Windows.Forms.Label();
            this.attachmentPanel = new System.Windows.Forms.Panel();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.clearAttachmentBtn = new System.Windows.Forms.Button();
            this.attachMediaBtn = new System.Windows.Forms.Button();
            this.attachmentLabel = new System.Windows.Forms.Label();
            this.progressPanel = new System.Windows.Forms.Panel();
            this.progressPercentLabel = new System.Windows.Forms.Label();
            this.formProgressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.backBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.submitBtn = new System.Windows.Forms.Button();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.footerLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.formPanel.SuspendLayout();
            this.attachmentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.progressPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(700, 80);
            this.headerPanel.TabIndex = 0;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.subtitleLabel.Location = new System.Drawing.Point(30, 50);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(640, 20);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Help us improve your community by reporting issues";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(30, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(640, 35);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Report Municipal Issues";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formPanel
            // 
            this.formPanel.BackColor = System.Drawing.Color.White;
            this.formPanel.Controls.Add(this.descriptionHelpLabel);
            this.formPanel.Controls.Add(this.categoryHelpLabel);
            this.formPanel.Controls.Add(this.locationHelpLabel);
            this.formPanel.Controls.Add(this.descriptionRichTextBox);
            this.formPanel.Controls.Add(this.characterCountLabel);
            this.formPanel.Controls.Add(this.descriptionLabel);
            this.formPanel.Controls.Add(this.categoryComboBox);
            this.formPanel.Controls.Add(this.categoryLabel);
            this.formPanel.Controls.Add(this.locationTextBox);
            this.formPanel.Controls.Add(this.locationLabel);
            this.formPanel.Location = new System.Drawing.Point(30, 100);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(640, 300);
            this.formPanel.TabIndex = 1;
            // 
            // descriptionHelpLabel
            // 
            this.descriptionHelpLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.descriptionHelpLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.descriptionHelpLabel.Location = new System.Drawing.Point(20, 275);
            this.descriptionHelpLabel.Name = "descriptionHelpLabel";
            this.descriptionHelpLabel.Size = new System.Drawing.Size(500, 15);
            this.descriptionHelpLabel.TabIndex = 9;
            this.descriptionHelpLabel.Text = "Provide detailed information about the issue (what, when, how severe, etc.)";
            // 
            // categoryHelpLabel
            // 
            this.categoryHelpLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.categoryHelpLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.categoryHelpLabel.Location = new System.Drawing.Point(20, 145);
            this.categoryHelpLabel.Name = "categoryHelpLabel";
            this.categoryHelpLabel.Size = new System.Drawing.Size(500, 15);
            this.categoryHelpLabel.TabIndex = 5;
            this.categoryHelpLabel.Text = "Select the category that best describes your issue";
            // 
            // locationHelpLabel
            // 
            this.locationHelpLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.locationHelpLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.locationHelpLabel.Location = new System.Drawing.Point(20, 70);
            this.locationHelpLabel.Name = "locationHelpLabel";
            this.locationHelpLabel.Size = new System.Drawing.Size(500, 15);
            this.locationHelpLabel.TabIndex = 2;
            this.locationHelpLabel.Text = "Please be as specific as possible about the location";
            // 
            // descriptionRichTextBox
            // 
            this.descriptionRichTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.descriptionRichTextBox.Location = new System.Drawing.Point(20, 195);
            this.descriptionRichTextBox.MaxLength = 1000;
            this.descriptionRichTextBox.Name = "descriptionRichTextBox";
            this.descriptionRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.descriptionRichTextBox.Size = new System.Drawing.Size(500, 80);
            this.descriptionRichTextBox.TabIndex = 8;
            this.descriptionRichTextBox.Text = "";
            // 
            // characterCountLabel
            // 
            this.characterCountLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.characterCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.characterCountLabel.Location = new System.Drawing.Point(350, 170);
            this.characterCountLabel.Name = "characterCountLabel";
            this.characterCountLabel.Size = new System.Drawing.Size(170, 25);
            this.characterCountLabel.TabIndex = 7;
            this.characterCountLabel.Text = "0 / 1000 characters (minimum 10)";
            this.characterCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.descriptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.descriptionLabel.Location = new System.Drawing.Point(20, 170);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(120, 25);
            this.descriptionLabel.TabIndex = 6;
            this.descriptionLabel.Text = "Description: *";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.categoryComboBox.Location = new System.Drawing.Point(20, 120);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(500, 25);
            this.categoryComboBox.TabIndex = 4;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            // 
            // categoryLabel
            // 
            this.categoryLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.categoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.categoryLabel.Location = new System.Drawing.Point(20, 95);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(100, 25);
            this.categoryLabel.TabIndex = 3;
            this.categoryLabel.Text = "Category: *";
            // 
            // locationTextBox
            // 
            this.locationTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.locationTextBox.ForeColor = System.Drawing.Color.Gray;
            this.locationTextBox.Location = new System.Drawing.Point(20, 45);
            this.locationTextBox.MaxLength = 200;
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(500, 25);
            this.locationTextBox.TabIndex = 1;
            this.locationTextBox.Text = "Enter the specific location (e.g., Corner of Main St & Oak Ave)";
            // 
            // locationLabel
            // 
            this.locationLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.locationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.locationLabel.Location = new System.Drawing.Point(20, 20);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(100, 25);
            this.locationLabel.TabIndex = 0;
            this.locationLabel.Text = "Location: *";
            // 
            // attachmentPanel
            // 
            this.attachmentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.attachmentPanel.Controls.Add(this.previewPictureBox);
            this.attachmentPanel.Controls.Add(this.clearAttachmentBtn);
            this.attachmentPanel.Controls.Add(this.attachMediaBtn);
            this.attachmentPanel.Controls.Add(this.attachmentLabel);
            this.attachmentPanel.Location = new System.Drawing.Point(30, 420);
            this.attachmentPanel.Name = "attachmentPanel";
            this.attachmentPanel.Size = new System.Drawing.Size(640, 100);
            this.attachmentPanel.TabIndex = 2;
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.BackColor = System.Drawing.Color.White;
            this.previewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPictureBox.Location = new System.Drawing.Point(520, 15);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(100, 70);
            this.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewPictureBox.TabIndex = 3;
            this.previewPictureBox.TabStop = false;
            this.previewPictureBox.Visible = false;
            // 
            // clearAttachmentBtn
            // 
            this.clearAttachmentBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.clearAttachmentBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearAttachmentBtn.Enabled = false;
            this.clearAttachmentBtn.FlatAppearance.BorderSize = 0;
            this.clearAttachmentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearAttachmentBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.clearAttachmentBtn.ForeColor = System.Drawing.Color.White;
            this.clearAttachmentBtn.Location = new System.Drawing.Point(210, 45);
            this.clearAttachmentBtn.Name = "clearAttachmentBtn";
            this.clearAttachmentBtn.Size = new System.Drawing.Size(80, 35);
            this.clearAttachmentBtn.TabIndex = 2;
            this.clearAttachmentBtn.Text = "✖ Remove";
            this.clearAttachmentBtn.UseVisualStyleBackColor = false;
            // 
            // attachMediaBtn
            // 
            this.attachMediaBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.attachMediaBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.attachMediaBtn.FlatAppearance.BorderSize = 0;
            this.attachMediaBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attachMediaBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.attachMediaBtn.ForeColor = System.Drawing.Color.White;
            this.attachMediaBtn.Location = new System.Drawing.Point(20, 45);
            this.attachMediaBtn.Name = "attachMediaBtn";
            this.attachMediaBtn.Size = new System.Drawing.Size(180, 35);
            this.attachMediaBtn.TabIndex = 1;
            this.attachMediaBtn.Text = "📎 Attach Media/Document";
            this.attachMediaBtn.UseVisualStyleBackColor = false;
            // 
            // attachmentLabel
            // 
            this.attachmentLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.attachmentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.attachmentLabel.Location = new System.Drawing.Point(20, 15);
            this.attachmentLabel.Name = "attachmentLabel";
            this.attachmentLabel.Size = new System.Drawing.Size(200, 25);
            this.attachmentLabel.TabIndex = 0;
            this.attachmentLabel.Text = "Attachments (Optional):";
            // 
            // progressPanel
            // 
            this.progressPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.progressPanel.Controls.Add(this.progressPercentLabel);
            this.progressPanel.Controls.Add(this.formProgressBar);
            this.progressPanel.Controls.Add(this.progressLabel);
            this.progressPanel.Location = new System.Drawing.Point(30, 540);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(640, 60);
            this.progressPanel.TabIndex = 3;
            // 
            // progressPercentLabel
            // 
            this.progressPercentLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.progressPercentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.progressPercentLabel.Location = new System.Drawing.Point(550, 15);
            this.progressPercentLabel.Name = "progressPercentLabel";
            this.progressPercentLabel.Size = new System.Drawing.Size(40, 20);
            this.progressPercentLabel.TabIndex = 2;
            this.progressPercentLabel.Text = "0%";
            this.progressPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formProgressBar
            // 
            this.formProgressBar.Location = new System.Drawing.Point(240, 15);
            this.formProgressBar.Name = "formProgressBar";
            this.formProgressBar.Size = new System.Drawing.Size(300, 18);
            this.formProgressBar.TabIndex = 1;
            // 
            // progressLabel
            // 
            this.progressLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.progressLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.progressLabel.Location = new System.Drawing.Point(20, 15);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(200, 20);
            this.progressLabel.TabIndex = 0;
            this.progressLabel.Text = "Form Completion: Getting Started";
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonPanel.Controls.Add(this.backBtn);
            this.buttonPanel.Controls.Add(this.clearBtn);
            this.buttonPanel.Controls.Add(this.submitBtn);
            this.buttonPanel.Location = new System.Drawing.Point(30, 620);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(640, 60);
            this.buttonPanel.TabIndex = 4;
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.backBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backBtn.FlatAppearance.BorderSize = 0;
            this.backBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.backBtn.ForeColor = System.Drawing.Color.White;
            this.backBtn.Location = new System.Drawing.Point(460, 10);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(120, 40);
            this.backBtn.TabIndex = 2;
            this.backBtn.Text = "← Back to Menu";
            this.backBtn.UseVisualStyleBackColor = false;
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.clearBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearBtn.FlatAppearance.BorderSize = 0;
            this.clearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.clearBtn.ForeColor = System.Drawing.Color.White;
            this.clearBtn.Location = new System.Drawing.Point(340, 10);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(100, 40);
            this.clearBtn.TabIndex = 1;
            this.clearBtn.Text = "Clear Form";
            this.clearBtn.UseVisualStyleBackColor = false;
            // 
            // submitBtn
            // 
            this.submitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.submitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitBtn.Enabled = false;
            this.submitBtn.FlatAppearance.BorderSize = 0;
            this.submitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.submitBtn.ForeColor = System.Drawing.Color.White;
            this.submitBtn.Location = new System.Drawing.Point(200, 10);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(120, 40);
            this.submitBtn.TabIndex = 0;
            this.submitBtn.Text = "Submit Report";
            this.submitBtn.UseVisualStyleBackColor = false;
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.Transparent;
            this.footerPanel.Controls.Add(this.footerLabel);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 700);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(700, 30);
            this.footerPanel.TabIndex = 5;
            // 
            // footerLabel
            // 
            this.footerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.footerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.footerLabel.Location = new System.Drawing.Point(0, 5);
            this.footerLabel.Name = "footerLabel";
            this.footerLabel.Size = new System.Drawing.Size(700, 20);
            this.footerLabel.TabIndex = 0;
            this.footerLabel.Text = "Your information is secure and will only be used for municipal service purposes.";
            this.footerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReportIssuesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(700, 730);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.progressPanel);
            this.Controls.Add(this.attachmentPanel);
            this.Controls.Add(this.formPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportIssuesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Issues - Municipal Services";
            this.headerPanel.ResumeLayout(false);
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            this.attachmentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.progressPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}