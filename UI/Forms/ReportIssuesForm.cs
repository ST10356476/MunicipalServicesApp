using MunicipalServicesApp.Services.Business;
using MunicipalServicesApp.Services.Infrastructure;
using MunicipalServicesApp.UI.Helpers;
using MunicipalServicesApp.Common;
using MunicipalServicesApp.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp.UI.Forms
{
    public partial class ReportIssuesForm : Form
    {
        private readonly IssueService _issueService;
        private readonly FileService _fileService;

        // Form State
        private string _attachedFilePath = "";
        private bool _hasUnsavedChanges = false;

        // Events
        public event Action IssueSubmitted;

        public ReportIssuesForm(IssueService issueService, FileService fileService)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            InitializeComponent();
            SetupStyling();
            SetupEventHandlers();
            LoadCategories();
            UpdateFormProgress();
        }

        private void SetupStyling()
        {
            // Apply professional styling
            StyleHeaderPanel();
            StyleFormPanel();
            StyleAttachmentPanel();
            StyleProgressPanel();
        }

        private void StyleHeaderPanel()
        {
            headerPanel.Paint += (sender, e) =>
            {
                // Professional blue gradient header
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(41, 128, 185),
                    Color.FromArgb(52, 152, 219),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }

                // Bottom border
                using (Pen pen = new Pen(Color.FromArgb(30, 100, 150), 1))
                {
                    e.Graphics.DrawLine(pen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
                }
            };
        }

        private void StyleFormPanel()
        {
            formPanel.Paint += (sender, e) =>
            {
                // Clean white background with border
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(brush, formPanel.ClientRectangle);
                }

                // Professional border
                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, formPanel.Width - 1, formPanel.Height - 1);
                }
            };
        }

        private void StyleAttachmentPanel()
        {
            attachmentPanel.Paint += (sender, e) =>
            {
                // Light background for attachment section
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(249, 249, 249)))
                {
                    e.Graphics.FillRectangle(brush, attachmentPanel.ClientRectangle);
                }

                // Subtle border
                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, attachmentPanel.Width - 1, attachmentPanel.Height - 1);
                }
            };
        }

        private void StyleProgressPanel()
        {
            progressPanel.Paint += (sender, e) =>
            {
                // Progress section background
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    progressPanel.ClientRectangle,
                    Color.FromArgb(248, 249, 250),
                    Color.FromArgb(236, 240, 241),
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, progressPanel.ClientRectangle);
                }

                // Top border
                using (Pen pen = new Pen(Color.FromArgb(52, 152, 219), 2))
                {
                    e.Graphics.DrawLine(pen, 0, 0, progressPanel.Width, 0);
                }
            };

            // Style progress bar
            StyleProgressBar();
        }

        private void StyleProgressBar()
        {
            formProgressBar.Height = 18;
            formProgressBar.Paint += (sender, e) =>
            {
                Rectangle rect = formProgressBar.ClientRectangle;

                // Background
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(236, 240, 241)))
                {
                    e.Graphics.FillRectangle(bgBrush, rect);
                }

                // Progress fill
                if (formProgressBar.Value > 0)
                {
                    int progressWidth = (int)((float)formProgressBar.Value / formProgressBar.Maximum * rect.Width);
                    Rectangle progressRect = new Rectangle(rect.X, rect.Y, progressWidth, rect.Height);

                    Color progressColor = GetProgressColor(formProgressBar.Value);
                    using (SolidBrush progressBrush = new SolidBrush(progressColor))
                    {
                        e.Graphics.FillRectangle(progressBrush, progressRect);
                    }
                }

                // Border
                using (Pen borderPen = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, rect);
                }
            };
        }

        private Color GetProgressColor(int value)
        {
            if (value < 50) return Color.FromArgb(231, 76, 60);    // Red
            if (value < 100) return Color.FromArgb(243, 156, 18);  // Orange
            return Color.FromArgb(46, 204, 113);                   // Green
        }

        private void LoadCategories()
        {
            categoryComboBox.Items.AddRange(CategoryProvider.Categories);
        }

        private void SetupEventHandlers()
        {
            // Text change events for form validation
            locationTextBox.TextChanged += FormField_Changed;
            locationTextBox.Enter += LocationTextBox_Enter;
            locationTextBox.Leave += LocationTextBox_Leave;
            categoryComboBox.SelectedIndexChanged += FormField_Changed;
            descriptionRichTextBox.TextChanged += Description_TextChanged;

            // Button click events
            attachMediaBtn.Click += AttachMediaBtn_Click;
            submitBtn.Click += SubmitBtn_Click;
            clearBtn.Click += ClearBtn_Click;
            backBtn.Click += BackBtn_Click;
            clearAttachmentBtn.Click += ClearAttachment_Click;

            // Form events
            this.FormClosing += ReportIssuesForm_FormClosing;

            // Keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += ReportIssuesForm_KeyDown;
        }

        private void FormField_Changed(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
            UpdateFormProgress();
            ValidateAndEnableSubmit();
        }

        private void Description_TextChanged(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
            UpdateCharacterCount();
            UpdateFormProgress();
            ValidateAndEnableSubmit();
        }

        private void UpdateCharacterCount()
        {
            int charCount = descriptionRichTextBox.Text.Length;
            characterCountLabel.Text = $"{charCount} / 1000 characters (minimum 10)";

            if (charCount < 10)
                characterCountLabel.ForeColor = Color.FromArgb(231, 76, 60);
            else if (charCount > 900)
                characterCountLabel.ForeColor = Color.FromArgb(243, 156, 18);
            else
                characterCountLabel.ForeColor = Color.FromArgb(46, 204, 113);
        }

        private void UpdateFormProgress()
        {
            int completedFields = 0;
            int totalRequiredFields = 3; // Location, Category, Description

            if (!string.IsNullOrWhiteSpace(locationTextBox.Text) && !IsLocationPlaceholder())
                completedFields++;

            if (categoryComboBox.SelectedItem != null)
                completedFields++;

            if (!string.IsNullOrWhiteSpace(descriptionRichTextBox.Text) && descriptionRichTextBox.Text.Length >= 10)
                completedFields++;

            int progressPercentage = (completedFields * 100) / totalRequiredFields;
            formProgressBar.Value = progressPercentage;
            progressPercentLabel.Text = $"{progressPercentage}%";

            // Update progress text
            if (progressPercentage == 0)
                progressLabel.Text = "Form Completion: Getting Started";
            else if (progressPercentage < 100)
                progressLabel.Text = "Form Completion: In Progress";
            else
                progressLabel.Text = "Form Completion: Ready to Submit";

            // Force repaint
            formProgressBar.Invalidate();
        }

        private void ValidateAndEnableSubmit()
        {
            bool isValid = !string.IsNullOrWhiteSpace(locationTextBox.Text) &&
                          !IsLocationPlaceholder() &&
                          categoryComboBox.SelectedItem != null &&
                          !string.IsNullOrWhiteSpace(descriptionRichTextBox.Text) &&
                          descriptionRichTextBox.Text.Length >= 10;

            submitBtn.Enabled = isValid;
            submitBtn.BackColor = isValid ? Color.FromArgb(46, 204, 113) : Color.FromArgb(149, 165, 166);
        }

        private void AttachMediaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedFile = _fileService.SelectFile();
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    _attachedFilePath = selectedFile;
                    var fileName = _fileService.GetFileName(selectedFile);

                    // Update button text and appearance
                    attachMediaBtn.Text = $"📎 {fileName}";
                    attachMediaBtn.BackColor = Color.FromArgb(52, 152, 219);

                    // Enable clear button
                    clearAttachmentBtn.Enabled = true;
                    clearAttachmentBtn.BackColor = Color.FromArgb(231, 76, 60);

                    // Show preview for images
                    ShowImagePreview(selectedFile);

                    _hasUnsavedChanges = true;
                    UIHelper.ShowSuccessMessage($"File attached successfully: {fileName}");
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error attaching file: {ex.Message}");
            }
        }

        private void ShowImagePreview(string filePath)
        {
            try
            {
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                string extension = Path.GetExtension(filePath).ToLower();

                if (imageExtensions.Contains(extension))
                {
                    previewPictureBox.Image = Image.FromFile(filePath);
                    previewPictureBox.Visible = true;
                }
                else
                {
                    previewPictureBox.Visible = false;
                }
            }
            catch
            {
                previewPictureBox.Visible = false;
            }
        }

        private void ClearAttachment_Click(object sender, EventArgs e)
        {
            _attachedFilePath = "";
            attachMediaBtn.Text = "📎 Attach Media/Document";
            attachMediaBtn.BackColor = Color.FromArgb(52, 152, 219);
            clearAttachmentBtn.Enabled = false;
            clearAttachmentBtn.BackColor = Color.FromArgb(149, 165, 166);
            previewPictureBox.Visible = false;
            previewPictureBox.Image?.Dispose();
            previewPictureBox.Image = null;

            _hasUnsavedChanges = true;
            UIHelper.ShowInfoMessage("Attachment removed successfully.");
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Show loading state
                submitBtn.Text = "⏳ Submitting...";
                submitBtn.Enabled = false;
                submitBtn.BackColor = Color.FromArgb(149, 165, 166);
                Application.DoEvents();

                // Get actual location text (not placeholder)
                string locationText = IsLocationPlaceholder() ? "" : locationTextBox.Text;

                var issueId = _issueService.SubmitIssue(
                    locationText,
                    categoryComboBox.SelectedItem?.ToString(),
                    descriptionRichTextBox.Text,
                    _attachedFilePath
                );

                var successMessage = $"Issue reported successfully!\n\n" +
                                   $"Reference ID: {issueId.Substring(0, 8)}\n" +
                                   $"Location: {locationText.Trim()}\n" +
                                   $"Category: {categoryComboBox.SelectedItem}\n" +
                                   $"Status: Submitted\n" +
                                   $"Date: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n" +
                                   $"Thank you for helping improve our community services!\n\n" +
                                   $"Tip: Save your reference ID for future tracking.";

                UIHelper.ShowSuccessMessage(successMessage);

                IssueSubmitted?.Invoke();
                _hasUnsavedChanges = false;
                this.Close();
            }
            catch (ValidationException ex)
            {
                UIHelper.ShowErrorMessage(ex.Message);
                RestoreSubmitButton();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while submitting the issue: {ex.Message}");
                RestoreSubmitButton();
            }
        }

        private void RestoreSubmitButton()
        {
            submitBtn.Text = "Submit Report";
            submitBtn.Enabled = true;
            submitBtn.BackColor = Color.FromArgb(46, 204, 113);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to clear all form data?",
                "Confirm Clear",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ClearForm();
                _hasUnsavedChanges = false;
            }
        }

        private void ClearForm()
        {
            locationTextBox.Text = "Enter the specific location (e.g., Corner of Main St & Oak Ave)";
            locationTextBox.ForeColor = Color.Gray;
            categoryComboBox.SelectedIndex = -1;
            descriptionRichTextBox.Clear();

            // Reset attachment
            _attachedFilePath = "";
            attachMediaBtn.Text = "📎 Attach Media/Document";
            attachMediaBtn.BackColor = Color.FromArgb(52, 152, 219);
            clearAttachmentBtn.Enabled = false;
            clearAttachmentBtn.BackColor = Color.FromArgb(149, 165, 166);
            previewPictureBox.Visible = false;
            previewPictureBox.Image?.Dispose();
            previewPictureBox.Image = null;

            UpdateCharacterCount();
            UpdateFormProgress();
            ValidateAndEnableSubmit();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReportIssuesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Are you sure you want to close this form?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            // Clean up image resources
            previewPictureBox.Image?.Dispose();
        }

        private void ReportIssuesForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.S:
                    if (submitBtn.Enabled)
                        SubmitBtn_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.L:
                    ClearBtn_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    BackBtn_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F1:
                    ShowFormHelp();
                    e.Handled = true;
                    break;
            }
        }

        private void ShowFormHelp()
        {
            var helpMessage = "Report Issues - Help\n\n" +
                            "Location: Enter the specific location where the issue is occurring\n" +
                            "Category: Select the type of municipal issue\n" +
                            "Description: Provide detailed information (minimum 10 characters)\n" +
                            "Attachments: Optional photos or documents\n\n" +
                            "Keyboard Shortcuts:\n" +
                            "• Ctrl+S: Submit Report\n" +
                            "• Ctrl+L: Clear Form\n" +
                            "• Esc: Back to Main Menu\n" +
                            "• F1: Show this help\n\n" +
                            "Form completion progress is shown at the bottom.\n" +
                            "All information is secure and confidential.";

            MessageBox.Show(helpMessage, "Form Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Placeholder text simulation for .NET Framework compatibility
        private void LocationTextBox_Enter(object sender, EventArgs e)
        {
            if (locationTextBox.Text == "Enter the specific location (e.g., Corner of Main St & Oak Ave)" && locationTextBox.ForeColor == Color.Gray)
            {
                locationTextBox.Text = "";
                locationTextBox.ForeColor = Color.Black;
            }
        }

        private void LocationTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                locationTextBox.Text = "Enter the specific location (e.g., Corner of Main St & Oak Ave)";
                locationTextBox.ForeColor = Color.Gray;
            }
        }

        private bool IsLocationPlaceholder()
        {
            return locationTextBox.Text == "Enter the specific location (e.g., Corner of Main St & Oak Ave)" && locationTextBox.ForeColor == Color.Gray;
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}