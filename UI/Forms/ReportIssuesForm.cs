using MunicipalServicesApp.Common.Exceptions;
using MunicipalServicesApp.Common;
using MunicipalServicesApp.Services.Business;
using MunicipalServicesApp.Services.Infrastructure;
using MunicipalServicesApp.UI.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace MunicipalServicesApp.UI.Forms
{
    public partial class ReportIssuesForm : Form
    {
        private readonly IssueService _issueService;
        private readonly FileService _fileService;
        private readonly GoogleMapsService _googleMapsService;

        // Form State
        private string _attachedFilePath = "";
        private bool _hasUnsavedChanges = false;
        private ListBox _locationSuggestionsListBox;
        private Timer _searchTimer;
        private bool _isSelectingFromSuggestions = false;

        // Events
        public event Action IssueSubmitted;

        public ReportIssuesForm(IssueService issueService, FileService fileService)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _googleMapsService = new GoogleMapsService();

            InitializeComponent();
            SetupLocationAutocomplete();
            SetupStyling();
            SetupEventHandlers();
            LoadCategories();
            UpdateFormProgress();
        }

        private void SetupLocationAutocomplete()
        {
            // Create location suggestions listbox
            _locationSuggestionsListBox = new ListBox
            {
                Size = new Size(500, 150),
                Location = new Point(20, 70),
                Font = new Font("Segoe UI", 9F),
                Visible = false,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                TabStop = false,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = 30
            };

            // Custom drawing for better appearance
            _locationSuggestionsListBox.DrawItem += LocationSuggestions_DrawItem;

            // Add to form panel and bring to front
            formPanel.Controls.Add(_locationSuggestionsListBox);
            _locationSuggestionsListBox.BringToFront();

            // Setup search timer to avoid too many API calls
            _searchTimer = new Timer { Interval = 300 }; // Reduced delay for better responsiveness
            _searchTimer.Tick += SearchTimer_Tick;

            // Setup suggestions listbox events
            _locationSuggestionsListBox.Click += LocationSuggestions_Click;
            _locationSuggestionsListBox.DoubleClick += LocationSuggestions_Click;
            _locationSuggestionsListBox.KeyDown += LocationSuggestions_KeyDown;
            _locationSuggestionsListBox.MouseMove += LocationSuggestions_MouseMove;
        }

        private void LocationSuggestions_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index >= 0 && e.Index < _locationSuggestionsListBox.Items.Count)
            {
                var suggestion = _locationSuggestionsListBox.Items[e.Index] as LocationSuggestion;
                if (suggestion != null)
                {
                    // Colors
                    Color textColor = (e.State & DrawItemState.Selected) != 0 ? Color.White : Color.FromArgb(52, 73, 94);
                    Color secondaryColor = (e.State & DrawItemState.Selected) != 0 ? Color.FromArgb(200, 255, 255, 255) : Color.FromArgb(127, 140, 141);

                    // Main address (bold)
                    using (Font boldFont = new Font("Segoe UI", 9F, FontStyle.Bold))
                    using (SolidBrush textBrush = new SolidBrush(textColor))
                    {
                        e.Graphics.DrawString(suggestion.MainText, boldFont, textBrush, e.Bounds.X + 8, e.Bounds.Y + 4);
                    }

                    // Secondary address (smaller, lighter)
                    using (Font regularFont = new Font("Segoe UI", 8F, FontStyle.Regular))
                    using (SolidBrush secondaryBrush = new SolidBrush(secondaryColor))
                    {
                        e.Graphics.DrawString(suggestion.SecondaryText, regularFont, secondaryBrush, e.Bounds.X + 8, e.Bounds.Y + 17);
                    }
                }
            }

            e.DrawFocusRectangle();
        }

        private void LocationSuggestions_MouseMove(object sender, MouseEventArgs e)
        {
            int index = _locationSuggestionsListBox.IndexFromPoint(e.Location);
            if (index >= 0 && index < _locationSuggestionsListBox.Items.Count)
            {
                _locationSuggestionsListBox.SelectedIndex = index;
            }
        }

        private async void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();

            if (!string.IsNullOrWhiteSpace(locationTextBox.Text) &&
                !IsLocationPlaceholder() &&
                locationTextBox.Text.Length >= 3 &&
                !_isSelectingFromSuggestions)
            {
                await SearchLocations(locationTextBox.Text);
            }
        }

        private async Task SearchLocations(string query)
        {
            try
            {
                var suggestions = await _googleMapsService.GetLocationSuggestions(query);

                if (suggestions != null && suggestions.Any())
                {
                    _locationSuggestionsListBox.Items.Clear();

                    foreach (var suggestion in suggestions.Take(5)) // Show top 5 results
                    {
                        _locationSuggestionsListBox.Items.Add(suggestion);
                    }

                    // Position suggestions box below location textbox
                    _locationSuggestionsListBox.Location = new Point(
                        locationTextBox.Location.X,
                        locationTextBox.Location.Y + locationTextBox.Height + 5
                    );

                    // Adjust height based on number of items
                    int itemCount = Math.Min(5, suggestions.Count());
                    _locationSuggestionsListBox.Height = (itemCount * _locationSuggestionsListBox.ItemHeight) + 4;

                    _locationSuggestionsListBox.Visible = true;
                    _locationSuggestionsListBox.BringToFront();

                    // Auto-select first item
                    if (_locationSuggestionsListBox.Items.Count > 0)
                    {
                        _locationSuggestionsListBox.SelectedIndex = 0;
                    }
                }
                else
                {
                    _locationSuggestionsListBox.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Fail silently for location suggestions
                _locationSuggestionsListBox.Visible = false;
                System.Diagnostics.Debug.WriteLine($"Location search error: {ex.Message}");
            }
        }

        private void LocationSuggestions_Click(object sender, EventArgs e)
        {
            if (_locationSuggestionsListBox.SelectedItem != null)
            {
                var selectedSuggestion = _locationSuggestionsListBox.SelectedItem as LocationSuggestion;
                if (selectedSuggestion != null)
                {
                    _isSelectingFromSuggestions = true;
                    locationTextBox.Text = selectedSuggestion.Description;
                    locationTextBox.ForeColor = Color.Black;
                    _locationSuggestionsListBox.Visible = false;
                    _isSelectingFromSuggestions = false;

                    // Store the place ID for potential future use
                    locationTextBox.Tag = selectedSuggestion.PlaceId;

                    // Trigger form validation
                    FormField_Changed(locationTextBox, EventArgs.Empty);

                    // Move focus to next field
                    categoryComboBox.Focus();
                }
            }
        }

        private void LocationSuggestions_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    LocationSuggestions_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    _locationSuggestionsListBox.Visible = false;
                    locationTextBox.Focus();
                    e.Handled = true;
                    break;
                case Keys.Up:
                    if (_locationSuggestionsListBox.SelectedIndex > 0)
                    {
                        _locationSuggestionsListBox.SelectedIndex--;
                    }
                    e.Handled = true;
                    break;
                case Keys.Down:
                    if (_locationSuggestionsListBox.SelectedIndex < _locationSuggestionsListBox.Items.Count - 1)
                    {
                        _locationSuggestionsListBox.SelectedIndex++;
                    }
                    e.Handled = true;
                    break;
            }
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
            locationTextBox.TextChanged += LocationTextBox_TextChanged;
            locationTextBox.Enter += LocationTextBox_Enter;
            locationTextBox.Leave += LocationTextBox_Leave;
            locationTextBox.KeyDown += LocationTextBox_KeyDown;
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
            this.Click += Form_Click; // Hide suggestions when clicking elsewhere

            // Keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += ReportIssuesForm_KeyDown;
        }

        private void LocationTextBox_TextChanged(object sender, EventArgs e)
        {
            // Stop previous timer
            _searchTimer.Stop();

            if (!string.IsNullOrWhiteSpace(locationTextBox.Text) &&
                !IsLocationPlaceholder() &&
                locationTextBox.Text.Length >= 3 &&
                !_isSelectingFromSuggestions)
            {
                // Start new search timer
                _searchTimer.Start();
            }
            else
            {
                _locationSuggestionsListBox.Visible = false;
            }

            // Trigger standard form validation
            FormField_Changed(sender, e);
        }

        private void LocationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_locationSuggestionsListBox.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        if (_locationSuggestionsListBox.Items.Count > 0)
                        {
                            _locationSuggestionsListBox.Focus();
                            if (_locationSuggestionsListBox.SelectedIndex < 0)
                                _locationSuggestionsListBox.SelectedIndex = 0;
                        }
                        e.Handled = true;
                        break;
                    case Keys.Up:
                        if (_locationSuggestionsListBox.Items.Count > 0)
                        {
                            _locationSuggestionsListBox.Focus();
                            _locationSuggestionsListBox.SelectedIndex = _locationSuggestionsListBox.Items.Count - 1;
                        }
                        e.Handled = true;
                        break;
                    case Keys.Escape:
                        _locationSuggestionsListBox.Visible = false;
                        e.Handled = true;
                        break;
                    case Keys.Enter:
                        if (_locationSuggestionsListBox.SelectedIndex >= 0)
                        {
                            LocationSuggestions_Click(_locationSuggestionsListBox, e);
                            e.Handled = true;
                        }
                        break;
                }
            }
            else if (e.KeyCode == Keys.Down && !string.IsNullOrWhiteSpace(locationTextBox.Text) && !IsLocationPlaceholder())
            {
                // Trigger search when pressing down arrow
                _searchTimer.Stop();
                _searchTimer.Start();
                e.Handled = true;
            }
        }

        private void Form_Click(object sender, EventArgs e)
        {
            // Hide suggestions when clicking elsewhere on the form
            _locationSuggestionsListBox.Visible = false;
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

                var successMessage = $"✅ Issue reported successfully!\n\n" +
                                   $"📋 Reference ID: {issueId.Substring(0, 8)}\n" +
                                   $"📍 Location: {locationText.Trim()}\n" +
                                   $"📂 Category: {categoryComboBox.SelectedItem}\n" +
                                   $"📊 Status: Submitted\n" +
                                   $"📅 Date: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n" +
                                   $"🙏 Thank you for helping improve our community services!\n\n" +
                                   $"💡 Tip: Save your reference ID for future tracking.";

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

            // Hide suggestions
            _locationSuggestionsListBox.Visible = false;

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
                    if (_locationSuggestionsListBox.Visible)
                        _locationSuggestionsListBox.Visible = false;
                    else
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
            var helpMessage = "📋 Report Issues - Help\n\n" +
                            "📍 Location: Start typing for location suggestions\n" +
                            "📂 Category: Select the type of municipal issue\n" +
                            "📝 Description: Provide detailed information (minimum 10 characters)\n" +
                            "📎 Attachments: Optional photos or documents\n\n" +
                            "⌨️ Keyboard Shortcuts:\n" +
                            "• Ctrl+S: Submit Report\n" +
                            "• Ctrl+L: Clear Form\n" +
                            "• Esc: Back to Main Menu\n" +
                            "• F1: Show this help\n" +
                            "• Arrow Down: Navigate to location suggestions\n\n" +
                            "✅ Form completion progress is shown at the bottom.\n" +
                            "🔒 All information is secure and confidential.";

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

            // Hide suggestions when leaving field
            _locationSuggestionsListBox.Visible = false;
        }

        private bool IsLocationPlaceholder()
        {
            return locationTextBox.Text == "Enter the specific location (e.g., Corner of Main St & Oak Ave)" && locationTextBox.ForeColor == Color.Gray;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _searchTimer?.Stop();
                _searchTimer?.Dispose();
                _googleMapsService?.Dispose();
                previewPictureBox.Image?.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }

    // ===== GoogleMapsService.cs =====
    // Google Maps Service for location autocomplete
    public class GoogleMapsService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleMapsService()
        {
            _httpClient = new HttpClient();
            // Replace with your actual Google Places API key
            _apiKey = "AIzaSyAoBPoQVZ6af48mPesSRzMH8ksat0Wn9ts";
        }

        public async Task<List<LocationSuggestion>> GetLocationSuggestions(string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_apiKey) || _apiKey == "AIzaSyAoBPoQVZ6af48mPesSRzMH8ksat0Wn9ts")
                {
                    // Return mock data for demonstration, but make it more dynamic
                    return GetDynamicMockLocationSuggestions(query);
                }

                // Google Places Autocomplete API endpoint with enhanced parameters
                string encodedQuery = Uri.EscapeDataString(query);
                string url = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?" +
                           $"input={encodedQuery}&" +
                           $"components=country:za&" + // Restrict to South Africa
                           $"types=address|establishment|geocode&" + // Include all address types
                           $"language=en&" + // English language
                           $"key={_apiKey}";

                var response = await _httpClient.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<GooglePlacesResponse>(response);

                if (result?.Status == "OK" && result.Predictions != null)
                {
                    return result.Predictions.Select(p => new LocationSuggestion
                    {
                        MainText = ExtractMainText(p.Description),
                        SecondaryText = ExtractSecondaryText(p.Description),
                        Description = p.Description,
                        PlaceId = p.PlaceId
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Google Places API Error: {ex.Message}");
            }

            // Fallback to dynamic mock data
            return GetDynamicMockLocationSuggestions(query);
        }

        private string ExtractMainText(string fullDescription)
        {
            // Extract the main part before the first comma
            var parts = fullDescription.Split(',');
            return parts.Length > 0 ? parts[0].Trim() : fullDescription;
        }

        private string ExtractSecondaryText(string fullDescription)
        {
            // Extract everything after the first comma
            var parts = fullDescription.Split(',');
            if (parts.Length > 1)
            {
                return string.Join(", ", parts.Skip(1)).Trim();
            }
            return "";
        }

        private List<LocationSuggestion> GetDynamicMockLocationSuggestions(string query)
        {
            var suggestions = new List<LocationSuggestion>();

            // Generate dynamic suggestions based on the user's input
            if (!string.IsNullOrWhiteSpace(query))
            {
                var lowerQuery = query.ToLower();

                // Generate street number suggestions for common patterns
                if (char.IsDigit(query[0]) || lowerQuery.Contains("street") || lowerQuery.Contains("road") || lowerQuery.Contains("avenue"))
                {
                    suggestions.AddRange(GenerateStreetSuggestions(query));
                }

                // Generate area/suburb suggestions
                suggestions.AddRange(GenerateAreaSuggestions(query));

                // Generate business/establishment suggestions
                suggestions.AddRange(GenerateEstablishmentSuggestions(query));

                // Generate landmark suggestions
                suggestions.AddRange(GenerateLandmarkSuggestions(query));
            }

            return suggestions.Take(5).ToList();
        }

        private List<LocationSuggestion> GenerateStreetSuggestions(string query)
        {
            var suggestions = new List<LocationSuggestion>();
            var commonStreets = new[]
            {
                "Main Street", "Church Street", "Market Street", "High Street", "Victoria Street",
                "Oxford Road", "Commissioner Street", "Long Street", "Jan Smuts Avenue",
                "Rivonia Road", "Sandton Drive", "Nelson Mandela Square", "Adderley Street",
                "Loop Street", "Bree Street", "Kloof Street", "Sea Point Main Road"
            };

            var areas = new[]
            {
                "CBD, Johannesburg, Gauteng",
                "Sandton, Gauteng",
                "Cape Town City Centre, Western Cape",
                "Sea Point, Cape Town, Western Cape",
                "Rosebank, Johannesburg, Gauteng",
                "Centurion, Gauteng",
                "Durban Central, KwaZulu-Natal",
                "Pretoria Central, Gauteng"
            };

            foreach (var street in commonStreets)
            {
                if (street.ToLower().Contains(query.ToLower()))
                {
                    var area = areas[new Random().Next(areas.Length)];
                    var streetNumber = new Random().Next(1, 999);

                    suggestions.Add(new LocationSuggestion
                    {
                        MainText = $"{streetNumber} {street}",
                        SecondaryText = $"{area}, South Africa",
                        Description = $"{streetNumber} {street}, {area}, South Africa",
                        PlaceId = $"mock_{Guid.NewGuid():N}"
                    });
                }
            }

            return suggestions;
        }

        private List<LocationSuggestion> GenerateAreaSuggestions(string query)
        {
            var areas = new Dictionary<string, string[]>
            {
                ["johannesburg"] = new[] { "Sandton", "Rosebank", "Melville", "Braamfontein", "Newtown", "Maboneng", "Parktown", "Houghton" },
                ["cape town"] = new[] { "Sea Point", "Camps Bay", "Clifton", "Green Point", "V&A Waterfront", "Gardens", "Tamboerskloof", "Bo-Kaap" },
                ["pretoria"] = new[] { "Hatfield", "Brooklyn", "Menlyn", "Centurion", "Arcadia", "Sunnyside", "Lynnwood", "Waterkloof" },
                ["durban"] = new[] { "Umhlanga", "Morningside", "Berea", "Glenwood", "Musgrave", "Florida Road", "Point Waterfront", "Ballito" }
            };

            var suggestions = new List<LocationSuggestion>();

            foreach (var city in areas.Keys)
            {
                if (city.Contains(query.ToLower()) || query.ToLower().Contains(city))
                {
                    foreach (var area in areas[city])
                    {
                        if (area.ToLower().Contains(query.ToLower()))
                        {
                            var province = city == "johannesburg" || city == "pretoria" ? "Gauteng" :
                                         city == "cape town" ? "Western Cape" : "KwaZulu-Natal";

                            suggestions.Add(new LocationSuggestion
                            {
                                MainText = area,
                                SecondaryText = $"{CapitalizeFirst(city)}, {province}, South Africa",
                                Description = $"{area}, {CapitalizeFirst(city)}, {province}, South Africa",
                                PlaceId = $"mock_{Guid.NewGuid():N}"
                            });
                        }
                    }
                }
            }

            return suggestions;
        }

        private List<LocationSuggestion> GenerateEstablishmentSuggestions(string query)
        {
            var establishments = new[]
            {
                new { Name = "Sandton City Mall", Area = "Sandton, Gauteng" },
                new { Name = "V&A Waterfront", Area = "Cape Town, Western Cape" },
                new { Name = "Gateway Theatre of Shopping", Area = "Durban, KwaZulu-Natal" },
                new { Name = "Menlyn Park Shopping Centre", Area = "Pretoria, Gauteng" },
                new { Name = "Canal Walk Shopping Centre", Area = "Cape Town, Western Cape" },
                new { Name = "Nelson Mandela Square", Area = "Sandton, Gauteng" },
                new { Name = "Gold Reef City", Area = "Johannesburg, Gauteng" },
                new { Name = "Rosebank Mall", Area = "Rosebank, Gauteng" }
            };

            return establishments
                .Where(e => e.Name.ToLower().Contains(query.ToLower()))
                .Select(e => new LocationSuggestion
                {
                    MainText = e.Name,
                    SecondaryText = $"{e.Area}, South Africa",
                    Description = $"{e.Name}, {e.Area}, South Africa",
                    PlaceId = $"mock_{Guid.NewGuid():N}"
                })
                .ToList();
        }

        private List<LocationSuggestion> GenerateLandmarkSuggestions(string query)
        {
            var landmarks = new[]
            {
                new { Name = "Union Buildings", Area = "Pretoria, Gauteng" },
                new { Name = "Table Mountain", Area = "Cape Town, Western Cape" },
                new { Name = "Carlton Centre", Area = "Johannesburg, Gauteng" },
                new { Name = "Moses Mabhida Stadium", Area = "Durban, KwaZulu-Natal" },
                new { Name = "Constitutional Hill", Area = "Johannesburg, Gauteng" },
                new { Name = "Castle of Good Hope", Area = "Cape Town, Western Cape" },
                new { Name = "OR Tambo International Airport", Area = "Kempton Park, Gauteng" },
                new { Name = "Cape Town International Airport", Area = "Cape Town, Western Cape" }
            };

            return landmarks
                .Where(l => l.Name.ToLower().Contains(query.ToLower()))
                .Select(l => new LocationSuggestion
                {
                    MainText = l.Name,
                    SecondaryText = $"{l.Area}, South Africa",
                    Description = $"{l.Name}, {l.Area}, South Africa",
                    PlaceId = $"mock_{Guid.NewGuid():N}"
                })
                .ToList();
        }

        private string CapitalizeFirst(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    // ===== Data Models =====
    public class LocationSuggestion
    {
        public string MainText { get; set; }        // Street address or main location
        public string SecondaryText { get; set; }   // City, province, country
        public string Description { get; set; }     // Full combined address
        public string PlaceId { get; set; }         // Google Place ID or mock ID
    }

    public class GooglePlacesResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("predictions")]
        public List<GooglePlacesPrediction> Predictions { get; set; }
    }

    public class GooglePlacesPrediction
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
    }
}
