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
        private Timer clockTimer;

        public MainForm()
        {
            var issueRepository = new IssueRepository();
            _issueService = new IssueService(issueRepository);
            _engagementService = new EngagementService();

            InitializeComponent();
            SetupStyling();
            SetupEventHandlers();
            SetupClock();
            UpdateEngagementDisplay();
        }

        private void SetupStyling()
        {
            // Apply professional styling
            StyleHeaderPanel();
            StyleButtonsPanel();
            StyleEngagementPanel();
            StyleSidebarPanel();
        }

        private void StyleHeaderPanel()
        {
            headerPanel.Paint += (sender, e) =>
            {
                // Clean gradient header
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    headerPanel.ClientRectangle,
                    Color.FromArgb(41, 128, 185), // Professional blue
                    Color.FromArgb(52, 152, 219), // Lighter blue
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
                }

                // Subtle bottom border
                using (Pen pen = new Pen(Color.FromArgb(30, 100, 150), 1))
                {
                    e.Graphics.DrawLine(pen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
                }
            };
        }

        private void StyleButtonsPanel()
        {
            // Style main action button
            StyleButton(reportIssuesBtn, Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96), true);

            // Style secondary buttons
            StyleButton(localEventsBtn, Color.FromArgb(149, 165, 166), Color.FromArgb(127, 140, 141), false);
            StyleButton(serviceStatusBtn, Color.FromArgb(149, 165, 166), Color.FromArgb(127, 140, 141), false);
        }

        private void StyleButton(Button btn, Color normalColor, Color hoverColor, bool isEnabled)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = normalColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.Cursor = isEnabled ? Cursors.Hand : Cursors.Default;

            // Simple hover effect for enabled buttons
            if (isEnabled)
            {
                btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
                btn.MouseLeave += (s, e) => btn.BackColor = normalColor;
            }

            // Add subtle shadow
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
            // Simple drop shadow effect
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
                // Clean white background with border
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(brush, engagementPanel.ClientRectangle);
                }

                // Professional border
                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 2))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, engagementPanel.Width - 1, engagementPanel.Height - 1);
                }

                // Top accent line
                using (Pen accentPen = new Pen(Color.FromArgb(52, 152, 219), 4))
                {
                    e.Graphics.DrawLine(accentPen, 0, 0, engagementPanel.Width, 0);
                }
            };

            // Style progress bar
            StyleProgressBar();
        }

        private void StyleProgressBar()
        {
            engagementProgressBar.Height = 20;
            engagementProgressBar.Paint += (sender, e) =>
            {
                Rectangle rect = engagementProgressBar.ClientRectangle;

                // Background
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(236, 240, 241)))
                {
                    e.Graphics.FillRectangle(bgBrush, rect);
                }

                // Progress fill
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

                // Border
                using (Pen borderPen = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawRectangle(borderPen, rect);
                }
            };
        }

        private Color GetProgressColor(int value)
        {
            if (value < 30) return Color.FromArgb(231, 76, 60);    // Red
            if (value < 70) return Color.FromArgb(243, 156, 18);   // Orange
            return Color.FromArgb(46, 204, 113);                   // Green
        }

        private void StyleSidebarPanel()
        {
            sidebarPanel.Paint += (sender, e) =>
            {
                // Professional sidebar background
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    sidebarPanel.ClientRectangle,
                    Color.FromArgb(44, 62, 80),   // Dark blue-gray
                    Color.FromArgb(52, 73, 94),   // Lighter blue-gray
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, sidebarPanel.ClientRectangle);
                }

                // Right border
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
            // Button click events
            reportIssuesBtn.Click += ReportIssuesBtn_Click;
            localEventsBtn.Click += DisabledButton_Click;
            serviceStatusBtn.Click += DisabledButton_Click;

            // Form events
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;

            // Keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
        }

        private void ReportIssuesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var reportForm = new ReportIssuesForm(_issueService, new FileService());
                reportForm.IssueSubmitted += OnIssueSubmitted;
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while opening the report form: {ex.Message}");
            }
        }

        private void DisabledButton_Click(object sender, EventArgs e)
        {
            UIHelper.ShowComingSoonMessage();
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

                // Update label color based on engagement level
                engagementLabel.ForeColor = GetProgressColor(engagementData.Level);

                // Force repaint of progress bar
                engagementProgressBar.Invalidate();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while updating engagement display: {ex.Message}");
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                var totalReports = _issueService.GetTotalSubmittedIssues();
                var engagementLevel = engagementProgressBar.Value;

                statsLabel.Text = $"Reports Submitted: {totalReports} | Engagement Level: {engagementLevel}%";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"An error occurred while updating statistics: {ex.Message}");
            }
        }

        private void ShowAbout(object sender, EventArgs e)
        {
            var aboutMessage = "Municipal Services Application\n" +
                              "Version 1.0 - Professional Edition\n\n" +
                              "Developed for South African Municipal Services\n" +
                              "Streamlining citizen engagement and service delivery\n\n" +
                              "Features:\n" +
                              "• Report Municipal Issues\n" +
                              "• Community Engagement Tracking\n" +
                              "• Professional User Interface\n" +
                              "• Sustainable Design\n\n" +
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
                                 "Features Coming Soon:\n" +
                                 "• Local Events and Announcements\n" +
                                 "• Service Request Status Tracking\n\n" +
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
            // Show welcome message
            var welcomeMessage = "Welcome to the Municipal Services Application!\n\n" +
                               "This platform helps you:\n" +
                               "• Report municipal issues in your area\n" +
                               "• Track community engagement\n" +
                               "• Access municipal services efficiently\n\n" +
                               "Click 'Report Issues' to get started!";

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
                case Keys.Alt | Keys.F4:
                case Keys.Escape:
                    this.Close();
                    e.Handled = true;
                    break;
            }
        }

        // Menu event handlers
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

        }
    }
}