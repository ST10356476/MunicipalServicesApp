using MunicipalServicesApp.Models;
using MunicipalServicesApp.Services.Business;
using MunicipalServicesApp.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MunicipalServicesApp.UI.Forms
{
    public partial class LocalEventsForm : Form
    {
        private readonly EventService _eventService;
        private List<Event> _currentEventsList;
        private List<Event> _recommendedEvents;
        private HashSet<string> _userCategories;
        private string _currentUserId = "default_user";

        // Search and filter state
        private Timer _searchTimer;
        private bool _isSearching = false;

        public LocalEventsForm()
        {
            _eventService = new EventService();
            _currentEventsList = new List<Event>();
            _recommendedEvents = new List<Event>();
            _userCategories = new HashSet<string>();

            InitializeComponent();
            SetupStyling();
            SetupEventHandlers();
            SetupSearchTimer();
            LoadInitialData();
        }

        private void SetupStyling()
        {
            StyleHeaderPanel();
            StyleSearchPanel();
            StyleMainContentPanel();
            StyleSidebarPanel();
            StyleStatsPanel();
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

        private void StyleSearchPanel()
        {
            searchPanel.Paint += (sender, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(248, 249, 250)))
                {
                    e.Graphics.FillRectangle(brush, searchPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawLine(pen, 0, searchPanel.Height - 1, searchPanel.Width, searchPanel.Height - 1);
                }
            };
        }

        private void StyleMainContentPanel()
        {
            mainContentPanel.Paint += (sender, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.FillRectangle(brush, mainContentPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, mainContentPanel.Width - 1, mainContentPanel.Height - 1);
                }
            };
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

        private void StyleStatsPanel()
        {
            statsPanel.Paint += (sender, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(236, 240, 241)))
                {
                    e.Graphics.FillRectangle(brush, statsPanel.ClientRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, statsPanel.Width - 1, statsPanel.Height - 1);
                }
            };
        }

        private void SetupEventHandlers()
        {
            // Search functionality
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
            searchButton.Click += SearchButton_Click;
            clearSearchButton.Click += ClearSearchButton_Click;

            // Filter functionality
            categoryFilterComboBox.SelectedIndexChanged += CategoryFilter_SelectedIndexChanged;
            dateFilterComboBox.SelectedIndexChanged += DateFilter_SelectedIndexChanged;
            priorityFilterComboBox.SelectedIndexChanged += PriorityFilter_SelectedIndexChanged;

            // List events
            eventsListBox.SelectedIndexChanged += EventsListBox_SelectedIndexChanged;
            eventsListBox.DoubleClick += EventsListBox_DoubleClick;

            // Buttons
            viewRecommendationsButton.Click += ViewRecommendationsButton_Click;
            viewRecentButton.Click += ViewRecentButton_Click;
            refreshButton.Click += RefreshButton_Click;
            backButton.Click += BackButton_Click;

            // Form events
            this.Load += LocalEventsForm_Load;
            this.KeyPreview = true;
            this.KeyDown += LocalEventsForm_KeyDown;
        }

        private void SetupSearchTimer()
        {
            _searchTimer = new Timer { Interval = 500 };
            _searchTimer.Tick += SearchTimer_Tick;
        }

        private void LoadInitialData()
        {
            LoadUpcomingEvents();
            LoadFilterOptions();
            LoadRecommendations();
            UpdateStatistics();
            UpdateNotificationsDisplay();
        }

        #region Data Loading Methods

        private void LoadUpcomingEvents()
        {
            try
            {
                _currentEventsList = _eventService.GetUpcomingEvents(20);
                DisplayEvents(_currentEventsList, "Upcoming Events");
                statusLabel.Text = $"Loaded {_currentEventsList.Count} upcoming events";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error loading events: {ex.Message}");
            }
        }

        private void LoadFilterOptions()
        {
            try
            {
                // Load categories
                var categories = _eventService.GetAvailableCategories().ToList();
                categories.Sort();
                categoryFilterComboBox.Items.Clear();
                categoryFilterComboBox.Items.Add("All Categories");
                categoryFilterComboBox.Items.AddRange(categories.ToArray());
                categoryFilterComboBox.SelectedIndex = 0;

                // Load date filters
                dateFilterComboBox.Items.Clear();
                dateFilterComboBox.Items.AddRange(new string[]
                {
                    "All Dates",
                    "Today",
                    "This Week",
                    "This Month",
                    "Next Month"
                });
                dateFilterComboBox.SelectedIndex = 0;

                // Load priority filters
                priorityFilterComboBox.Items.Clear();
                priorityFilterComboBox.Items.AddRange(new string[]
                {
                    "All Priorities",
                    "Critical",
                    "High",
                    "Medium",
                    "Low"
                });
                priorityFilterComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error loading filter options: {ex.Message}");
            }
        }

        private void LoadRecommendations()
        {
            try
            {
                _recommendedEvents = _eventService.GetRecommendedEvents(_currentUserId);

                recommendationsListBox.Items.Clear();
                foreach (var eventItem in _recommendedEvents.Take(5))
                {
                    recommendationsListBox.Items.Add($"{eventItem.Title} - {eventItem.Date:MMM dd}");
                }

                recommendationsCountLabel.Text = $"{_recommendedEvents.Count} recommendations available";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error loading recommendations: {ex.Message}");
            }
        }

        #endregion

        #region Display Methods

        private void DisplayEvents(List<Event> events, string title)
        {
            eventsListBox.Items.Clear();
            eventsTitleLabel.Text = title;

            if (events == null || events.Count == 0)
            {
                eventsListBox.Items.Add("No events found matching your criteria.");
                return;
            }

            foreach (var eventItem in events)
            {
                var displayText = $"[{eventItem.GetPriorityText()}] {eventItem.Title} - {eventItem.Date:MMM dd, yyyy} at {eventItem.Location}";
                eventsListBox.Items.Add(displayText);
            }

            // Update user preferences based on viewed categories
            foreach (var eventItem in events.Take(5))
            {
                _userCategories.Add(eventItem.Category);
            }
        }

        private void DisplayEventDetails(Event selectedEvent)
        {
            if (selectedEvent == null)
            {
                eventDetailsRichTextBox.Clear();
                return;
            }

            var details = $"📅 {selectedEvent.Title}\n\n" +
                         $"📝 Description:\n{selectedEvent.Description}\n\n" +
                         $"📍 Location: {selectedEvent.Location}\n" +
                         $"🗓️ Date & Time: {selectedEvent.Date:dddd, MMMM dd, yyyy 'at' HH:mm}\n" +
                         $"📂 Category: {selectedEvent.Category}\n" +
                         $"⚡ Priority: {selectedEvent.GetPriorityText()}\n" +
                         $"👥 Capacity: {selectedEvent.CurrentAttendees}/{selectedEvent.MaxCapacity}\n" +
                         $"🏢 Organized by: {selectedEvent.OrganizedBy}\n" +
                         $"📊 Status: {selectedEvent.Status}\n\n";

            if (selectedEvent.Tags.Count > 0)
            {
                details += $"🏷️ Tags: {string.Join(", ", selectedEvent.Tags)}\n\n";
            }

            if (selectedEvent.HasAvailableSpots())
            {
                details += $"✅ {selectedEvent.GetAvailableSpots()} spots available for registration!";
            }
            else
            {
                details += "❌ Event is fully booked.";
            }

            eventDetailsRichTextBox.Text = details;
        }

        #endregion

        #region Search and Filter Methods

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            if (!_isSearching)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            try
            {
                _isSearching = true;
                var searchTerm = searchTextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    LoadUpcomingEvents();
                    return;
                }

                var searchResults = _eventService.SearchEvents(searchTerm, _currentUserId);
                _currentEventsList = ApplyCurrentFilters(searchResults);

                DisplayEvents(_currentEventsList, $"Search Results for '{searchTerm}'");
                statusLabel.Text = $"Found {_currentEventsList.Count} events matching '{searchTerm}'";

                // Update recommendations based on search
                LoadRecommendations();
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error performing search: {ex.Message}");
            }
            finally
            {
                _isSearching = false;
            }
        }

        private List<Event> ApplyCurrentFilters(List<Event> events)
        {
            var filteredEvents = events;

            // Apply category filter
            if (categoryFilterComboBox.SelectedIndex > 0)
            {
                var selectedCategory = categoryFilterComboBox.SelectedItem.ToString();
                filteredEvents = filteredEvents.Where(e => e.Category == selectedCategory).ToList();
            }

            // Apply date filter
            if (dateFilterComboBox.SelectedIndex > 0)
            {
                var dateFilter = dateFilterComboBox.SelectedItem.ToString();
                filteredEvents = ApplyDateFilter(filteredEvents, dateFilter);
            }

            // Apply priority filter
            if (priorityFilterComboBox.SelectedIndex > 0)
            {
                var priorityFilter = priorityFilterComboBox.SelectedItem.ToString();
                var priorityValue = GetPriorityValue(priorityFilter);
                filteredEvents = filteredEvents.Where(e => e.Priority == priorityValue).ToList();
            }

            return filteredEvents;
        }

        private List<Event> ApplyDateFilter(List<Event> events, string dateFilter)
        {
            var now = DateTime.Now;

            switch (dateFilter)
            {
                case "Today":
                    return events.Where(e => e.Date.Date == now.Date).ToList();
                case "This Week":
                    return events.Where(e => e.Date >= now.Date && e.Date <= now.Date.AddDays(7)).ToList();
                case "This Month":
                    return events.Where(e => e.Date.Month == now.Month && e.Date.Year == now.Year).ToList();
                case "Next Month":
                    return events.Where(e => e.Date.Month == now.AddMonths(1).Month && e.Date.Year == now.AddMonths(1).Year).ToList();
                default:
                    return events;
            }
        }

        private int GetPriorityValue(string priorityText)
        {
            switch (priorityText)
            {
                case "Critical":
                    return 1;
                case "High":
                    return 2;
                case "Medium":
                    return 3;
                case "Low":
                    return 4;
                default:
                    return 0;
            }
        }

        #endregion

        #region Event Handlers

        private void SearchButton_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void ClearSearchButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
            LoadUpcomingEvents();
            statusLabel.Text = "Search cleared. Showing upcoming events.";
        }

        private void CategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentEventsList != null)
            {
                var filteredEvents = ApplyCurrentFilters(_currentEventsList);
                DisplayEvents(filteredEvents, "Filtered Events");
                statusLabel.Text = $"Showing {filteredEvents.Count} filtered events";
            }
        }

        private void DateFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryFilter_SelectedIndexChanged(sender, e);
        }

        private void PriorityFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryFilter_SelectedIndexChanged(sender, e);
        }

        private void EventsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventsListBox.SelectedIndex >= 0 && eventsListBox.SelectedIndex < _currentEventsList.Count)
            {
                var selectedEvent = _currentEventsList[eventsListBox.SelectedIndex];
                DisplayEventDetails(selectedEvent);

                // Track this as a viewed event
                _eventService.GetEvent(selectedEvent.Id);
            }
        }

        private void EventsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (eventsListBox.SelectedIndex >= 0 && eventsListBox.SelectedIndex < _currentEventsList.Count)
            {
                var selectedEvent = _currentEventsList[eventsListBox.SelectedIndex];
                ShowEventRegistrationDialog(selectedEvent);
            }
        }

        private void ViewRecommendationsButton_Click(object sender, EventArgs e)
        {
            try
            {
                _recommendedEvents = _eventService.GetRecommendedEvents(_currentUserId);
                _currentEventsList = _recommendedEvents;
                DisplayEvents(_currentEventsList, "Recommended Events");
                statusLabel.Text = $"Showing {_currentEventsList.Count} recommended events";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error loading recommendations: {ex.Message}");
            }
        }

        private void ViewRecentButton_Click(object sender, EventArgs e)
        {
            try
            {
                var recentEvents = _eventService.GetRecentlyViewedEvents();
                _currentEventsList = recentEvents;
                DisplayEvents(_currentEventsList, "Recently Viewed Events");
                statusLabel.Text = $"Showing {_currentEventsList.Count} recently viewed events";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error loading recent events: {ex.Message}");
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadInitialData();
            UIHelper.ShowSuccessMessage("Data refreshed successfully!");
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Statistics and Notifications

        private void UpdateStatistics()
        {
            try
            {
                var stats = _eventService.GetEventStatistics();
                var searchAnalytics = _eventService.GetSearchAnalytics();

                var statsText = $"📊 Event Statistics:\n\n" +
                               $"Total Events: {stats["TotalEvents"]}\n" +
                               $"Active Events: {stats["ActiveEvents"]}\n" +
                               $"Categories: {stats["TotalCategories"]}\n" +
                               $"Locations: {stats["TotalLocations"]}\n" +
                               $"Popular Tags: {stats["TotalTags"]}\n\n" +
                               $"📈 User Activity:\n" +
                               $"Recently Viewed: {stats["RecentlyViewedCount"]}\n" +
                               $"Pending Notifications: {stats["PendingNotifications"]}\n\n" +
                               $"🏆 Most Popular:\n" +
                               $"Category: {stats["MostPopularCategory"]}\n" +
                               $"Search Term: {stats["MostSearchedTerm"]}";

                statisticsRichTextBox.Text = statsText;
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error updating statistics: {ex.Message}");
            }
        }

        private void UpdateNotificationsDisplay()
        {
            try
            {
                var notifications = _eventService.GetAllPendingNotifications();
                notificationsListBox.Items.Clear();

                foreach (var notification in notifications.Take(10))
                {
                    var urgentIndicator = notification.IsUrgent ? "🚨 " : "📢 ";
                    var displayText = $"{urgentIndicator}{notification.Title} - {notification.PublishedDate:MMM dd}";
                    notificationsListBox.Items.Add(displayText);
                }

                notificationCountLabel.Text = $"{notifications.Count} pending notifications";
            }
            catch (Exception ex)
            {
                UIHelper.ShowErrorMessage($"Error updating notifications: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        private void ShowEventRegistrationDialog(Event eventItem)
        {
            if (!eventItem.HasAvailableSpots())
            {
                UIHelper.ShowInfoMessage("Sorry, this event is fully booked!");
                return;
            }

            var result = MessageBox.Show(
                $"Would you like to register for '{eventItem.Title}'?\n\n" +
                $"Date: {eventItem.Date:dddd, MMMM dd, yyyy}\n" +
                $"Location: {eventItem.Location}\n" +
                $"Available spots: {eventItem.GetAvailableSpots()}",
                "Event Registration",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Simulate registration
                eventItem.CurrentAttendees++;
                UIHelper.ShowSuccessMessage($"Successfully registered for '{eventItem.Title}'!\n\nYou will receive a confirmation email shortly.");
                DisplayEventDetails(eventItem);
            }
        }

        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            searchTextBox.Focus();
        }

        private void LocalEventsForm_KeyDown(object sender, KeyEventArgs e)
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
                case Keys.Escape:
                    BackButton_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F5:
                    RefreshButton_Click(null, null);
                    e.Handled = true;
                    break;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _searchTimer?.Stop();
                _searchTimer?.Dispose();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}