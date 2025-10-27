using MunicipalServicesApp.Data.Repositories;
using MunicipalServicesApp.Services.Business;
using MunicipalServicesApp.Services.Infrastructure;
using MunicipalServicesApp.UI.Helpers;
using MunicipalServicesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly IssueService _issueService;
        private readonly EngagementService _engagementService;
        private readonly EventService _eventService;

        // Store child forms
        private ReportIssuesForm _reportIssuesForm;
        private LocalEventsForm _localEventsForm;
        private ServiceRequestStatusForm _serviceRequestStatusForm;
        private Timer clockTimer;

        public MainForm()
        {
            var issueRepository = new IssueRepository();
            _issueService = new IssueService(issueRepository);
            _engagementService = new EngagementService();
            _eventService = new EventService();

            InitializeComponent();
            SetupStyling();
            SetupEventHandlers();
            SetupClock();
            UpdateEngagementDisplay();
            UpdateEventNotifications();
        }

        private void SetupStyling()
        {
            StyleHeaderPanel();
            StyleButtonsPanel();
            StyleEngagementPanel();
            StyleSidebarPanel();
        }

        private void StyleHeaderPanel()
        {
            headerPanel.Paint += (sender, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(41, 128, 185),
                    Color.FromArgb(52, 152, 219),
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(30, 100, 150), 1))
                {
                    e.Graphics.DrawLine(pen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
                }
            };
        }

        private void StyleButtonsPanel()
        {
            StyleButton(reportIssuesBtn, Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96), true);
            StyleButton(localEventsBtn, Color.FromArgb(52, 152, 219), Color.FromArgb(41, 128, 185), true);
            StyleButton(serviceStatusBtn, Color.FromArgb(155, 89, 182), Color.FromArgb(142, 68, 173), true);
        }

        private void StyleButton(Button btn, Color normalColor, Color hoverColor, bool isEnabled)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = normalColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.Cursor = isEnabled ? Cursors.Hand : Cursors.Default;
            btn.Enabled = isEnabled;

            if (isEnabled)
            {
                btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
                btn.MouseLeave += (s, e) => btn.BackColor = normalColor;
            }

            btn.Paint += (s, e) =>
            {
                if (isEnabled)
                {
                    DrawButtonShadow(e.Graphics, btn.ClientRectangle);
                }
            };
        }

        private void DrawButtonShadow(Graphics g, Rectangle rect)
        {
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(30, Color.Black)))
            {
                Rectangle shadowRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height);
                g.FillRectangle(shadowBrush, shadowRect);
            }
        }

        private void StyleEngagementPanel()
        {
            engagementPanel.Paint += (sender, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(brush, engagementPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, engagementPanel.Width - 1, engagementPanel.Height - 1);
                }

                using (Pen accentPen = new Pen(Color.FromArgb(52, 152, 219), 4))
                {
                    e.Graphics.DrawLine(accentPen, 0, 0, engagementPanel.Width, 0);
                }
            };

            StyleProgressBar();
        }

        private void StyleProgressBar()
        {
            engagementProgressBar.Height = 20;
            engagementProgressBar.Paint += (sender, e) =>
            {
                Rectangle rect = engagementProgressBar.ClientRectangle;

                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(236, 240, 241)))
                {
                    e.Graphics.FillRectangle(bgBrush, rect);
                }

                if (engagementProgressBar.Value > 0)
                {
                    int progressWidth = (int)((float)engagementProgressBar.Value / engagementProgressBar.Maximum * rect.Width);
                    Rectangle progressRect = new Rectangle(rect.X, rect.Y, progressWidth, rect.Height);

                    Color progressColor = GetProgressColor(engagementProgressBar.Value);
                    using (SolidBrush progressBrush = new SolidBrush(progressColor))
                    {
                        e.Graphics.FillRectangle(progressBrush, progressRect);
                    }
                }

                using (Pen borderPen = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, rect);
                }
            };
        }

        private Color GetProgressColor(int value)
        {
            if (value < 30) return Color.FromArgb(231, 76, 60);
            if (value < 70) return Color.FromArgb(243, 156, 18);
            return Color.FromArgb(46, 204, 113);
        }

        private void StyleSidebarPanel()
        {
            sidebarPanel.Paint += (sender, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    sidebarPanel.ClientRectangle,
                    Color.FromArgb(44, 62, 80),
                    Color.FromArgb(52, 73, 94),
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, sidebarPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(149, 165, 166), 1))
                {
                    e.Graphics.DrawLine(pen, sidebarPanel.Width - 1, 0, sidebarPanel.Width - 1, sidebarPanel.Height);
                }
            };
        }

        private void SetupClock()
        {
            clockTimer = new Timer { Interval = 1000 };
            clockTimer.Tick += (s, e) => UpdateClock();
            clockTimer.Start();
            UpdateClock();
        }

        private void UpdateClock()
        {
            clockLabel.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy\nHH:mm:ss");
        }

        private void SetupEventHandlers()
        {
            reportIssuesBtn.Click += ReportIssuesBtn_Click;
            localEventsBtn.Click += LocalEventsBtn_Click;
            serviceStatusBtn.Click += ServiceStatusBtn_Click;

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;

            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
        }

        private void ReportIssuesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create form if it doesn't exist
                if (_reportIssuesForm == null || _reportIssuesForm.IsDisposed)
                {
                    _reportIssuesForm = new ReportIssuesForm(_issueService, new FileService());
                    _reportIssuesForm.IssueSubmitted += OnIssueSubmitted;

                    // Configure form for embedding with scrolling
                    _reportIssuesForm.TopLevel = false;
                    _reportIssuesForm.FormBorderStyle = FormBorderStyle.None;
                    _reportIssuesForm.AutoScroll = true;
                }

                // Handle form closing event
                _reportIssuesForm.FormClosed -= ReportIssuesForm_Closed;
                _reportIssuesForm.FormClosed += ReportIssuesForm_Closed;

                // Hide main content
                HideMainContent();

                // Add form to main form directly, below menu
                if (!this.Controls.Contains(_reportIssuesForm))
                {
                    this.Controls.Add(_reportIssuesForm);
                }
                _reportIssuesForm.Dock = DockStyle.Fill;
                _reportIssuesForm.Show();
                _reportIssuesForm.BringToFront();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while opening the report form: {ex.Message}");
            }
        }

        private void ReportIssuesForm_Closed(object sender, FormClosedEventArgs e)
        {
            ShowMainContent();
        }

        private void LocalEventsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create form if it doesn't exist
                if (_localEventsForm == null || _localEventsForm.IsDisposed)
                {
                    _localEventsForm = new LocalEventsForm();

                    // Configure form for embedding with scrolling
                    _localEventsForm.TopLevel = false;
                    _localEventsForm.FormBorderStyle = FormBorderStyle.None;
                    _localEventsForm.AutoScroll = true;
                }

                // Handle form closing event
                _localEventsForm.FormClosed -= LocalEventsForm_Closed;
                _localEventsForm.FormClosed += LocalEventsForm_Closed;

                // Hide main content
                HideMainContent();

                // Add form to main form directly, below menu
                if (!this.Controls.Contains(_localEventsForm))
                {
                    this.Controls.Add(_localEventsForm);
                }
                _localEventsForm.Dock = DockStyle.Fill;
                _localEventsForm.Show();
                _localEventsForm.BringToFront();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while opening the events page: {ex.Message}");
            }
        }

        private void LocalEventsForm_Closed(object sender, FormClosedEventArgs e)
        {
            ShowMainContent();
            var engagementData = _engagementService.UpdateEngagement();
            UpdateEngagementDisplay(engagementData);
            UpdateStatistics();
            UpdateEventNotifications();
        }

        private void ServiceStatusBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create form if it doesn't exist
                if (_serviceRequestStatusForm == null || _serviceRequestStatusForm.IsDisposed)
                {
                    _serviceRequestStatusForm = new ServiceRequestStatusForm();

                    // Configure form for embedding with scrolling
                    _serviceRequestStatusForm.TopLevel = false;
                    _serviceRequestStatusForm.FormBorderStyle = FormBorderStyle.None;
                    _serviceRequestStatusForm.AutoScroll = true;
                }

                // Handle form closing event
                _serviceRequestStatusForm.FormClosed -= ServiceRequestStatusForm_Closed;
                _serviceRequestStatusForm.FormClosed += ServiceRequestStatusForm_Closed;

                // Hide main content
                HideMainContent();

                // Add form to main form directly, below menu
                if (!this.Controls.Contains(_serviceRequestStatusForm))
                {
                    this.Controls.Add(_serviceRequestStatusForm);
                }
                _serviceRequestStatusForm.Dock = DockStyle.Fill;
                _serviceRequestStatusForm.Show();
                _serviceRequestStatusForm.BringToFront();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while opening the service status: {ex.Message}");
            }
        }

        private void ServiceRequestStatusForm_Closed(object sender, FormClosedEventArgs e)
        {
            ShowMainContent();
        }

        private void HideMainContent()
        {
            // Just hide the panels, don't remove them
            sidebarPanel.Visible = false;
            headerPanel.Visible = false;
            mainContentPanel.Visible = false;
        }

        private void ShowMainContent()
        {
            // Remove embedded forms first
            if (_reportIssuesForm != null && this.Controls.Contains(_reportIssuesForm))
            {
                this.Controls.Remove(_reportIssuesForm);
            }

            if (_localEventsForm != null && this.Controls.Contains(_localEventsForm))
            {
                this.Controls.Remove(_localEventsForm);
            }

            if (_serviceRequestStatusForm != null && this.Controls.Contains(_serviceRequestStatusForm))
            {
                this.Controls.Remove(_serviceRequestStatusForm);
            }

            // Restore main panels in the correct order
            if (!this.Controls.Contains(mainContentPanel))
            {
                this.Controls.Add(mainContentPanel);
                mainContentPanel.Dock = DockStyle.Fill;
            }

            if (!this.Controls.Contains(headerPanel))
            {
                this.Controls.Add(headerPanel);
                headerPanel.Dock = DockStyle.Top;
            }

            if (!this.Controls.Contains(sidebarPanel))
            {
                this.Controls.Add(sidebarPanel);
                sidebarPanel.Dock = DockStyle.Left;
            }

            // Make all panels visible
            sidebarPanel.Visible = true;
            headerPanel.Visible = true;
            mainContentPanel.Visible = true;

            // Show all controls within main content panel
            foreach (Control control in mainContentPanel.Controls)
            {
                control.Visible = true;
            }

            // Refresh the layout
            this.PerformLayout();
            mainContentPanel.PerformLayout();
            this.Refresh();
        }

        private void OnIssueSubmitted()
        {
            try
            {
                var engagementData = _engagementService.UpdateEngagement();
                UpdateEngagementDisplay(engagementData);
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while updating engagement: {ex.Message}");
            }
        }

        private void UpdateEngagementDisplay(EngagementData engagementData = null)
        {
            try
            {
                engagementData = engagementData ?? _engagementService.GetCurrentEngagement();
                engagementProgressBar.Value = engagementData.Level;
                engagementLabel.Text = engagementData.Message;

                engagementLabel.ForeColor = GetProgressColor(engagementData.Level);

                engagementProgressBar.Invalidate();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while updating engagement display: {ex.Message}");
            }
        }

        private void UpdateEventNotifications()
        {
            try
            {
                var pendingNotifications = _eventService.GetPendingNotificationCount();
                var upcomingEvents = _eventService.GetUpcomingEvents(3);

                var eventInfo = upcomingEvents.Count > 0
                    ? $" | Next Event: {upcomingEvents[0].Title} on {upcomingEvents[0].Date:MMM dd}"
                    : " | No upcoming events";

                var currentStatsText = statsLabel.Text;
                if (currentStatsText.Contains("|"))
                {
                    var basePart = currentStatsText.Split('|')[0];
                    statsLabel.Text = basePart + eventInfo;
                }
                else
                {
                    statsLabel.Text = currentStatsText + eventInfo;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating event notifications: {ex.Message}");
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                var totalReports = _issueService.GetTotalSubmittedIssues();
                var engagementLevel = engagementProgressBar.Value;
                var eventStats = _eventService.GetEventStatistics();

                statsLabel.Text = $"Reports Submitted: {totalReports} | Engagement Level: {engagementLevel}% | Total Events: {eventStats["TotalEvents"]}";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while updating statistics: {ex.Message}");
            }
        }

        private void ShowAbout(object sender, EventArgs e)
        {
            var aboutMessage = "Municipal Services Application\n" +
                              "Version 3.0 - Advanced Edition\n\n" +
                              "Developed for South African Municipal Services\n" +
                              "Streamlining citizen engagement and service delivery\n\n" +
                              "Features:\n" +
                              "• Report Municipal Issues\n" +
                              "• Local Events and Announcements\n" +
                              "• Service Request Status Tracking\n" +
                              "• Community Engagement Tracking\n" +
                              "• Smart Recommendations System\n" +
                              "• Advanced Search and Filtering\n" +
                              "• Professional User Interface\n" +
                              "• Sustainable Design\n\n" +
                              "Advanced Data Structures:\n" +
                              "• Trees, BST, AVL Trees\n" +
                              "• Heaps and Priority Queues\n" +
                              "• Graphs and Graph Algorithms\n" +
                              "• Hash Tables and Dictionaries\n" +
                              "• Sets and Advanced Collections\n\n" +
                              "© 2025 Municipal Services Application";

            MessageBox.Show(aboutMessage, "About Municipal Services Application",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowUserGuide(object sender, EventArgs e)
        {
            var userGuideMessage = "User Guide - Municipal Services Application\n\n" +
                                 "How to Report Issues:\n" +
                                 "1. Click 'Report Issues' button\n" +
                                 "2. Fill in the location of the issue\n" +
                                 "3. Select appropriate category\n" +
                                 "4. Provide detailed description\n" +
                                 "5. Optionally attach media/documents\n" +
                                 "6. Click 'Submit Report'\n\n" +
                                 "How to Use Local Events:\n" +
                                 "1. Click 'Local Events & Announcements' button\n" +
                                 "2. Browse upcoming events or search for specific events\n" +
                                 "3. Use filters to narrow down results by category, date, or priority\n" +
                                 "4. View event details by selecting an event\n" +
                                 "5. Register for events by double-clicking\n" +
                                 "6. Check recommendations based on your interests\n" +
                                 "7. View recently viewed events and announcements\n\n" +
                                 "How to Track Service Requests:\n" +
                                 "1. Click 'Service Request Status' button\n" +
                                 "2. View all your submitted service requests\n" +
                                 "3. Search for specific requests\n" +
                                 "4. Filter by status or priority\n" +
                                 "5. Track request progress with unique IDs\n" +
                                 "6. View advanced data structure visualizations\n\n" +
                                 "Advanced Features:\n" +
                                 "• Search functionality with intelligent recommendations\n" +
                                 "• Priority-based event and request organization\n" +
                                 "• Category and location-based filtering\n" +
                                 "• Real-time statistics and analytics\n" +
                                 "• Advanced data structure visualizations\n\n" +
                                 "The engagement meter shows community participation levels.\n" +
                                 "Thank you for helping improve our community!";

            MessageBox.Show(userGuideMessage, "User Guide",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to exit the Municipal Services Application?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                clockTimer?.Stop();
                clockTimer?.Dispose();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var welcomeMessage = "Welcome to the Municipal Services Application v3.0!\n\n" +
                               "This platform helps you:\n" +
                               "• Report municipal issues in your area\n" +
                               "• Discover local events and announcements\n" +
                               "• Track service request status and progress\n" +
                               "• Monitor community engagement\n" +
                               "• Access municipal services efficiently\n" +
                               "• Get personalized recommendations\n\n" +
                               "New in Version 3.0:\n" +
                               "• Service Request Status tracking\n" +
                               "• Advanced data structure implementations\n" +
                               "• Graph and tree visualizations\n" +
                               "• Priority-based request management\n" +
                               "• Enhanced user experience\n\n" +
                               "Click any button to get started!\n" +
                               "Use F1 for help at any time.";

            MessageBox.Show(welcomeMessage, "Welcome!",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    ShowUserGuide(null, null);
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.R:
                    ReportIssuesBtn_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.E:
                    LocalEventsBtn_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.S:
                    ServiceStatusBtn_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.Alt | Keys.F4:
                case Keys.Escape:
                    this.Close();
                    e.Handled = true;
                    break;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAbout(sender, e);
        }

        private void userGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserGuide(sender, e);
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            // Initialization code if needed
        }
    }
}