// ServiceRequestStatusForm.cs
using MunicipalServicesApp.Models;
using MunicipalServicesApp.Services.Business;
using MunicipalServicesApp.Data.Repositories;
using MunicipalServicesApp.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MunicipalServicesApp.UI.Forms
{
    public partial class ServiceRequestStatusForm : Form
    {
        private readonly ServiceRequestService _serviceRequestService;
        private List<ServiceRequest> _allRequests;
        private string _currentUserId = "default_user";

        // Tree visualization
        private TreeView _bstTreeView;
        private TreeView _avlTreeView;
        private PictureBox _graphVisualization;

        public ServiceRequestStatusForm()
        {
            var repository = new ServiceRequestRepository();
            _serviceRequestService = new ServiceRequestService(repository);
            _allRequests = new List<ServiceRequest>();

            InitializeComponent();
            SetupStyling();
            SetupEventHandlers();
            LoadServiceRequests();
            InitializeAdvancedVisualizations();
        }

        private void SetupStyling()
        {
            StyleHeaderPanel();
            StyleMainPanel();
            StyleDetailsPanel();
            StyleVisualizationPanel();
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

        private void StyleMainPanel()
        {
            mainPanel.Paint += (sender, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(brush, mainPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, mainPanel.Width - 1, mainPanel.Height - 1);
                }
            };
        }

        private void StyleDetailsPanel()
        {
            detailsPanel.Paint += (sender, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(248, 249, 250)))
                {
                    e.Graphics.FillRectangle(brush, detailsPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, detailsPanel.Width - 1, detailsPanel.Height - 1);
                }
            };
        }

        private void StyleVisualizationPanel()
        {
            visualizationPanel.Paint += (sender, e) =>
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    visualizationPanel.ClientRectangle,
                    Color.FromArgb(236, 240, 241),
                    Color.FromArgb(248, 249, 250),
                    LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, visualizationPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(52, 152, 219), 2))
                {
                    e.Graphics.DrawLine(pen, 0, 0, visualizationPanel.Width, 0);
                }
            };
        }

        private void SetupEventHandlers()
        {
            // Search and filter
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            searchButton.Click += SearchButton_Click;
            clearSearchButton.Click += ClearSearchButton_Click;
            statusFilterComboBox.SelectedIndexChanged += Filter_SelectedIndexChanged;
            priorityFilterComboBox.SelectedIndexChanged += Filter_SelectedIndexChanged;

            // Request list
            requestsListBox.SelectedIndexChanged += RequestsListBox_SelectedIndexChanged;
            requestsListBox.DoubleClick += RequestsListBox_DoubleClick;

            // Buttons
            trackRequestButton.Click += TrackRequestButton_Click;
            refreshButton.Click += RefreshButton_Click;
            backButton.Click += BackButton_Click;
            showBSTButton.Click += ShowBSTButton_Click;
            showAVLButton.Click += ShowAVLButton_Click;
            showGraphButton.Click += ShowGraphButton_Click;
            showHeapButton.Click += ShowHeapButton_Click;

            // Form events
            this.Load += ServiceRequestStatusForm_Load;
            this.KeyPreview = true;
            this.KeyDown += ServiceRequestStatusForm_KeyDown;
        }

        private void InitializeAdvancedVisualizations()
        {
            // BST Tree View
            _bstTreeView = new TreeView
            {
                Location = new Point(20, 50),
                Size = new Size(300, 200),
                Visible = false
            };
            visualizationPanel.Controls.Add(_bstTreeView);

            // AVL Tree View
            _avlTreeView = new TreeView
            {
                Location = new Point(20, 50),
                Size = new Size(300, 200),
                Visible = false
            };
            visualizationPanel.Controls.Add(_avlTreeView);

            // Graph Visualization
            _graphVisualization = new PictureBox
            {
                Location = new Point(20, 50),
                Size = new Size(400, 300),
                Visible = false,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            _graphVisualization.Paint += GraphVisualization_Paint;
            visualizationPanel.Controls.Add(_graphVisualization);
        }

        private void LoadServiceRequests()
        {
            try
            {
                _allRequests = _serviceRequestService.GetAllRequests();
                DisplayRequests(_allRequests);
                UpdateStatistics();
                LoadFilterOptions();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error loading service requests: {ex.Message}");
            }
        }

        private void LoadFilterOptions()
        {
            // Status filter
            statusFilterComboBox.Items.Clear();
            statusFilterComboBox.Items.AddRange(new string[]
            {
                "All Statuses",
                "Submitted",
                "InProgress",
                "Resolved",
                "Closed"
            });
            statusFilterComboBox.SelectedIndex = 0;

            // Priority filter
            priorityFilterComboBox.Items.Clear();
            priorityFilterComboBox.Items.AddRange(new string[]
            {
                "All Priorities",
                "Critical (1)",
                "High (2)",
                "Medium (3)",
                "Low (4)"
            });
            priorityFilterComboBox.SelectedIndex = 0;
        }

        private void DisplayRequests(List<ServiceRequest> requests)
        {
            requestsListBox.Items.Clear();

            if (requests == null || requests.Count == 0)
            {
                requestsListBox.Items.Add("No service requests found.");
                return;
            }

            foreach (var request in requests)
            {
                var statusIcon = GetStatusIcon(request.Status);
                var priorityIcon = GetPriorityIcon(request.Priority);
                var displayText = $"{statusIcon} {priorityIcon} {request.Title} - {request.Status}";

                requestsListBox.Items.Add(new RequestListBoxItem
                {
                    DisplayText = displayText,
                    Request = request
                });
            }

            requestsCountLabel.Text = $"{requests.Count} requests found";
        }

        private string GetStatusIcon(string status)
        {
            switch (status)
            {
                case "Submitted":
                    return "📋";
                case "InProgress":
                    return "🔄";
                case "Resolved":
                    return "✅";
                case "Closed":
                    return "🔒";
                default:
                    return "📄";
            }
        }

        private string GetPriorityIcon(int priority)
        {
            switch (priority)
            {
                case 1:
                    return "🚨"; // Critical  
                case 2:
                    return "⚠️";  // High  
                case 3:
                    return "📝";  // Medium  
                case 4:
                    return "💤";  // Low  
                default:
                    return "📄";
            }
        }

        private void DisplayRequestDetails(ServiceRequest request)
        {
            if (request == null)
            {
                detailsRichTextBox.Clear();
                return;
            }

            var details = $"📋 {request.Title}\n\n" +
                         $"📝 Description:\n{request.Description}\n\n" +
                         $"📍 Location: {request.Location}\n" +
                         $"📂 Category: {request.Category}\n" +
                         $"⚡ Priority: {request.GetPriorityText()} ({request.Priority})\n" +
                         $"🏢 Department: {request.AssignedDepartment}\n" +
                         $"📊 Status: {request.Status}\n" +
                         $"📅 Created: {request.CreatedDate:dd/MM/yyyy HH:mm}\n" +
                         $"⏱️ Estimated Duration: {request.EstimatedDuration} hours\n";

            if (request.ResolvedDate.HasValue)
            {
                details += $"✅ Resolved: {request.ResolvedDate.Value:dd/MM/yyyy HH:mm}\n";
            }

            details += $"\n🆔 Request ID: {request.Id}\n";

            if (request.StatusUpdates.Count > 0)
            {
                details += $"\n📋 Status History:\n";
                foreach (var update in request.StatusUpdates.OrderBy(u => u.Timestamp))
                {
                    details += $"   • {update.Timestamp:dd/MM HH:mm}: {update.Status} - {update.Description}\n";
                }
            }

            detailsRichTextBox.Text = details;
        }

        #region Search and Filter Methods

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ClearSearchButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            statusFilterComboBox.SelectedIndex = 0;
            priorityFilterComboBox.SelectedIndex = 0;
            DisplayRequests(_allRequests);
        }

        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filteredRequests = _allRequests;

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                var searchTerm = searchTextBox.Text.ToLower();
                filteredRequests = filteredRequests.Where(r =>
                    r.Title.ToLower().Contains(searchTerm) ||
                    r.Description.ToLower().Contains(searchTerm) ||
                    r.Location.ToLower().Contains(searchTerm) ||
                    r.Category.ToLower().Contains(searchTerm)
                ).ToList();
            }

            // Apply status filter
            if (statusFilterComboBox.SelectedIndex > 0)
            {
                var selectedStatus = statusFilterComboBox.SelectedItem.ToString();
                filteredRequests = filteredRequests.Where(r => r.Status == selectedStatus).ToList();
            }

            // Apply priority filter
            if (priorityFilterComboBox.SelectedIndex > 0)
            {
                var selectedPriority = priorityFilterComboBox.SelectedIndex; // 1=Critical, 2=High, etc.
                filteredRequests = filteredRequests.Where(r => r.Priority == selectedPriority).ToList();
            }

            DisplayRequests(filteredRequests);
        }

        #endregion

        #region Advanced Data Structure Visualizations

        private void ShowBSTButton_Click(object sender, EventArgs e)
        {
            try
            {
                var chronologicalRequests = _serviceRequestService.GetChronologicalRequests();
                _bstTreeView.Nodes.Clear();

                if (chronologicalRequests.Count > 0)
                {
                    var rootNode = new TreeNode("BST Root: " + chronologicalRequests[0].Title.Substring(0, 20) + "...");
                    _bstTreeView.Nodes.Add(rootNode);

                    // Build a simple BST visualization (simplified for display)
                    for (int i = 1; i < Math.Min(chronologicalRequests.Count, 10); i++)
                    {
                        var node = new TreeNode($"{chronologicalRequests[i].CreatedDate:MM/dd}: {chronologicalRequests[i].Title.Substring(0, 15)}...");
                        if (i % 2 == 0)
                            rootNode.Nodes.Add(node);
                        else
                            rootNode.Nodes.Add(node);
                    }

                    _bstTreeView.ExpandAll();
                }

                ShowVisualization(_bstTreeView, "Binary Search Tree - Chronological Order");
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error displaying BST: {ex.Message}");
            }
        }

        private void ShowAVLButton_Click(object sender, EventArgs e)
        {
            try
            {
                var highPriorityRequests = _serviceRequestService.GetHighPriorityRequests();
                _avlTreeView.Nodes.Clear();

                if (highPriorityRequests.Count > 0)
                {
                    var rootNode = new TreeNode($"AVL Root: Priority {highPriorityRequests[0].Priority} - {highPriorityRequests[0].Title.Substring(0, 20)}...");
                    _avlTreeView.Nodes.Add(rootNode);

                    foreach (var request in highPriorityRequests.Take(5))
                    {
                        var node = new TreeNode($"Priority {request.Priority}: {request.Title.Substring(0, 15)}...");
                        rootNode.Nodes.Add(node);
                    }

                    _avlTreeView.ExpandAll();
                }

                ShowVisualization(_avlTreeView, "AVL Tree - High Priority Requests");
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error displaying AVL Tree: {ex.Message}");
            }
        }

        private void ShowGraphButton_Click(object sender, EventArgs e)
        {
            try
            {
                var departmentNetwork = _serviceRequestService.GetFullDepartmentNetwork();
                _graphVisualization.Tag = departmentNetwork;
                _graphVisualization.Invalidate();

                ShowVisualization(_graphVisualization, "Department Collaboration Graph (MST)");
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error displaying graph: {ex.Message}");
            }
        }

        private void GraphVisualization_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(Color.White);

            var departmentNetwork = _graphVisualization.Tag as List<(string from, string to, double weight)>;
            if (departmentNetwork == null || departmentNetwork.Count == 0) return;

            // Simple graph visualization
            var nodePositions = new Dictionary<string, Point>();
            var center = new Point(_graphVisualization.Width / 2, _graphVisualization.Height / 2);
            var radius = Math.Min(center.X, center.Y) - 50;

            // Position nodes in a circle
            var departments = departmentNetwork.SelectMany(x => new[] { x.from, x.to }).Distinct().ToList();
            double angleStep = 2 * Math.PI / departments.Count;

            for (int i = 0; i < departments.Count; i++)
            {
                double angle = i * angleStep;
                int x = center.X + (int)(radius * Math.Cos(angle));
                int y = center.Y + (int)(radius * Math.Sin(angle));
                nodePositions[departments[i]] = new Point(x, y);
            }

            // Draw edges
            using (var pen = new Pen(Color.Blue, 2))
            {
                foreach (var (from, to, weight) in departmentNetwork)
                {
                    if (nodePositions.ContainsKey(from) && nodePositions.ContainsKey(to))
                    {
                        graphics.DrawLine(pen, nodePositions[from], nodePositions[to]);

                        // Draw weight
                        var midPoint = new Point(
                            (nodePositions[from].X + nodePositions[to].X) / 2,
                            (nodePositions[from].Y + nodePositions[to].Y) / 2
                        );
                        graphics.DrawString(weight.ToString("F1"),
                            new Font("Arial", 8),
                            Brushes.Red,
                            midPoint);
                    }
                }
            }

            // Draw nodes
            using (var brush = new SolidBrush(Color.FromArgb(52, 152, 219)))
            {
                foreach (var dept in departments)
                {
                    var pos = nodePositions[dept];
                    graphics.FillEllipse(brush, pos.X - 20, pos.Y - 20, 40, 40);
                    graphics.DrawString(dept,
                        new Font("Arial", 8, FontStyle.Bold),
                        Brushes.White,
                        pos.X - 18,
                        pos.Y - 8);
                }
            }
        }

        private void ShowHeapButton_Click(object sender, EventArgs e)
        {
            try
            {
                var nextPriorityRequest = _serviceRequestService.GetNextPriorityRequest();
                var message = nextPriorityRequest != null
                    ? $"Next High Priority Request:\n\n" +
                      $"Title: {nextPriorityRequest.Title}\n" +
                      $"Priority: {nextPriorityRequest.GetPriorityText()}\n" +
                      $"Department: {nextPriorityRequest.AssignedDepartment}\n" +
                      $"Status: {nextPriorityRequest.Status}"
                    : "No pending high priority requests";


                UIHelper.ShowInfoMessage(message); // Removed the second argument to match the method signature
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error displaying heap: {ex.Message}");
            }
        }

        private void ShowVisualization(Control control, string title)
        {
            // Hide all visualizations
            _bstTreeView.Visible = false;
            _avlTreeView.Visible = false;
            _graphVisualization.Visible = false;

            // Show selected visualization
            control.Visible = true;
            visualizationTitleLabel.Text = title;
        }

        #endregion

        #region Event Handlers

        private void RequestsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (requestsListBox.SelectedItem is RequestListBoxItem listBoxItem)
            {
                DisplayRequestDetails(listBoxItem.Request);
            }
        }

        private void RequestsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (requestsListBox.SelectedItem is RequestListBoxItem listBoxItem)
            {
                ShowRequestDetailsDialog(listBoxItem.Request);
            }
        }

        private void TrackRequestButton_Click(object sender, EventArgs e)
        {
            var requestId = Interaction.InputBox(
                "Enter your Service Request ID:",
                "Track Service Request",
                "");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                var request = _serviceRequestService.GetRequest(requestId);
                if (request != null)
                {
                    ShowRequestDetailsDialog(request);
                }
                else
                {
                    UIHelper.ShowErrorMessage("Service request not found. Please check the ID and try again.");
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadServiceRequests();
            UIHelper.ShowSuccessMessage("Service requests refreshed successfully!");
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ServiceRequestStatusForm_Load(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        private void ServiceRequestStatusForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Control | Keys.F:
                    searchTextBox.Focus();
                    e.Handled = true;
                    break;
                case Keys.Control | Keys.R:
                    RefreshButton_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F5:
                    RefreshButton_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    BackButton_Click(null, null);
                    e.Handled = true;
                    break;
            }
        }

        #endregion

        #region Helper Methods

        private void ShowRequestDetailsDialog(ServiceRequest request)
        {
            var details = $"📋 SERVICE REQUEST DETAILS\n\n" +
                         $"Title: {request.Title}\n" +
                         $"Description: {request.Description}\n" +
                         $"Location: {request.Location}\n" +
                         $"Category: {request.Category}\n" +
                         $"Priority: {request.GetPriorityText()}\n" +
                         $"Department: {request.AssignedDepartment}\n" +
                         $"Status: {request.Status}\n" +
                         $"Created: {request.CreatedDate:dd/MM/yyyy HH:mm}\n" +
                         $"Request ID: {request.Id}\n\n" +
                         $"STATUS UPDATES:\n";

            foreach (var update in request.StatusUpdates.OrderBy(u => u.Timestamp))
            {
                details += $"{update.Timestamp:dd/MM HH:mm}: {update.Status} - {update.Description}\n";
            }

            MessageBox.Show(details, "Service Request Details",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateStatistics()
        {
            try
            {
                var totalRequests = _allRequests.Count;
                var submitted = _allRequests.Count(r => r.Status == "Submitted");
                var inProgress = _allRequests.Count(r => r.Status == "InProgress");
                var resolved = _allRequests.Count(r => r.Status == "Resolved");
                var critical = _allRequests.Count(r => r.Priority == 1);

                statsLabel.Text = $"Total: {totalRequests} | Submitted: {submitted} | In Progress: {inProgress} | Resolved: {resolved} | Critical: {critical}";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error updating statistics: {ex.Message}");
            }
        }

        #endregion

        // Helper class for list box items
        private class RequestListBoxItem
        {
            public string DisplayText { get; set; }
            public ServiceRequest Request { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }

    }
}