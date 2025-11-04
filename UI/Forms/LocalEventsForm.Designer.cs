using System.Drawing;

namespace MunicipalServicesApp.UI.Forms
{
    partial class LocalEventsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Header Panel Controls
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;

        // Search Panel Controls
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button clearSearchButton;

        // Filter Controls
        private System.Windows.Forms.Label filtersLabel;
        private System.Windows.Forms.Label categoryFilterLabel;
        private System.Windows.Forms.ComboBox categoryFilterComboBox;
        private System.Windows.Forms.Label dateFilterLabel;
        private System.Windows.Forms.ComboBox dateFilterComboBox;
        private System.Windows.Forms.Label priorityFilterLabel;
        private System.Windows.Forms.ComboBox priorityFilterComboBox;

        // Main Content Panel
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Label eventsTitleLabel;
        private System.Windows.Forms.ListBox eventsListBox;
        private System.Windows.Forms.Label eventDetailsLabel;
        private System.Windows.Forms.RichTextBox eventDetailsRichTextBox;

        // Sidebar Panel
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Label recommendationsLabel;
        private System.Windows.Forms.ListBox recommendationsListBox;
        private System.Windows.Forms.Label recommendationsCountLabel;
        private System.Windows.Forms.Button viewRecommendationsButton;
        private System.Windows.Forms.Button viewRecentButton;

        // Notifications Panel
        private System.Windows.Forms.Label notificationsLabel;
        private System.Windows.Forms.ListBox notificationsListBox;
        private System.Windows.Forms.Label notificationCountLabel;

        // Statistics Panel
        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.Label statisticsLabel;
        private System.Windows.Forms.RichTextBox statisticsRichTextBox;

        // Button Panel
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button backButton;

        // Status Panel
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label statusLabel;

        #region Windows Form Designer code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.priorityFilterComboBox = new System.Windows.Forms.ComboBox();
            this.priorityFilterLabel = new System.Windows.Forms.Label();
            this.dateFilterComboBox = new System.Windows.Forms.ComboBox();
            this.dateFilterLabel = new System.Windows.Forms.Label();
            this.categoryFilterComboBox = new System.Windows.Forms.ComboBox();
            this.categoryFilterLabel = new System.Windows.Forms.Label();
            this.filtersLabel = new System.Windows.Forms.Label();
            this.clearSearchButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.eventDetailsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.eventDetailsLabel = new System.Windows.Forms.Label();
            this.eventsListBox = new System.Windows.Forms.ListBox();
            this.eventsTitleLabel = new System.Windows.Forms.Label();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.notificationCountLabel = new System.Windows.Forms.Label();
            this.notificationsListBox = new System.Windows.Forms.ListBox();
            this.notificationsLabel = new System.Windows.Forms.Label();
            this.viewRecentButton = new System.Windows.Forms.Button();
            this.viewRecommendationsButton = new System.Windows.Forms.Button();
            this.recommendationsCountLabel = new System.Windows.Forms.Label();
            this.recommendationsListBox = new System.Windows.Forms.ListBox();
            this.recommendationsLabel = new System.Windows.Forms.Label();
            this.statsPanel = new System.Windows.Forms.Panel();
            this.statisticsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.statisticsLabel = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.backButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.statsPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.statusPanel.SuspendLayout();
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
            this.headerPanel.Size = new System.Drawing.Size(1400, 80);
            this.headerPanel.TabIndex = 0;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.subtitleLabel.Location = new System.Drawing.Point(30, 50);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(1340, 25);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Discover upcoming events and important announcements in your community";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(30, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1340, 35);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Local Events & Announcements";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.searchPanel.Controls.Add(this.priorityFilterComboBox);
            this.searchPanel.Controls.Add(this.priorityFilterLabel);
            this.searchPanel.Controls.Add(this.dateFilterComboBox);
            this.searchPanel.Controls.Add(this.dateFilterLabel);
            this.searchPanel.Controls.Add(this.categoryFilterComboBox);
            this.searchPanel.Controls.Add(this.categoryFilterLabel);
            this.searchPanel.Controls.Add(this.filtersLabel);
            this.searchPanel.Controls.Add(this.clearSearchButton);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 80);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(1400, 100);
            this.searchPanel.TabIndex = 1;
            // 
            // priorityFilterComboBox
            // 
            this.priorityFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.priorityFilterComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.priorityFilterComboBox.Location = new System.Drawing.Point(800, 60);
            this.priorityFilterComboBox.Name = "priorityFilterComboBox";
            this.priorityFilterComboBox.Size = new System.Drawing.Size(120, 23);
            this.priorityFilterComboBox.TabIndex = 10;
            // 
            // priorityFilterLabel
            // 
            this.priorityFilterLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.priorityFilterLabel.Location = new System.Drawing.Point(800, 40);
            this.priorityFilterLabel.Name = "priorityFilterLabel";
            this.priorityFilterLabel.Size = new System.Drawing.Size(120, 20);
            this.priorityFilterLabel.TabIndex = 9;
            this.priorityFilterLabel.Text = "Priority:";
            // 
            // dateFilterComboBox
            // 
            this.dateFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateFilterComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateFilterComboBox.Location = new System.Drawing.Point(650, 60);
            this.dateFilterComboBox.Name = "dateFilterComboBox";
            this.dateFilterComboBox.Size = new System.Drawing.Size(130, 23);
            this.dateFilterComboBox.TabIndex = 8;
            // 
            // dateFilterLabel
            // 
            this.dateFilterLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dateFilterLabel.Location = new System.Drawing.Point(650, 40);
            this.dateFilterLabel.Name = "dateFilterLabel";
            this.dateFilterLabel.Size = new System.Drawing.Size(130, 20);
            this.dateFilterLabel.TabIndex = 7;
            this.dateFilterLabel.Text = "Date Range:";
            // 
            // categoryFilterComboBox
            // 
            this.categoryFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryFilterComboBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.categoryFilterComboBox.Location = new System.Drawing.Point(480, 60);
            this.categoryFilterComboBox.Name = "categoryFilterComboBox";
            this.categoryFilterComboBox.Size = new System.Drawing.Size(150, 23);
            this.categoryFilterComboBox.TabIndex = 6;
            // 
            // categoryFilterLabel
            // 
            this.categoryFilterLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.categoryFilterLabel.Location = new System.Drawing.Point(480, 40);
            this.categoryFilterLabel.Name = "categoryFilterLabel";
            this.categoryFilterLabel.Size = new System.Drawing.Size(150, 20);
            this.categoryFilterLabel.TabIndex = 5;
            this.categoryFilterLabel.Text = "Category:";
            // 
            // filtersLabel
            // 
            this.filtersLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.filtersLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.filtersLabel.Location = new System.Drawing.Point(480, 15);
            this.filtersLabel.Name = "filtersLabel";
            this.filtersLabel.Size = new System.Drawing.Size(100, 25);
            this.filtersLabel.TabIndex = 4;
            this.filtersLabel.Text = "Filters:";
            // 
            // clearSearchButton
            // 
            this.clearSearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.clearSearchButton.FlatAppearance.BorderSize = 0;
            this.clearSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearSearchButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.clearSearchButton.ForeColor = System.Drawing.Color.White;
            this.clearSearchButton.Location = new System.Drawing.Point(390, 55);
            this.clearSearchButton.Name = "clearSearchButton";
            this.clearSearchButton.Size = new System.Drawing.Size(60, 30);
            this.clearSearchButton.TabIndex = 3;
            this.clearSearchButton.Text = "Clear";
            this.clearSearchButton.UseVisualStyleBackColor = false;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(320, 55);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(60, 30);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.searchTextBox.Location = new System.Drawing.Point(30, 55);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(280, 25);
            this.searchTextBox.TabIndex = 1;
            // 
            // searchLabel
            // 
            this.searchLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.searchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.searchLabel.Location = new System.Drawing.Point(30, 15);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(200, 25);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = "Search Events:";
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.BackColor = System.Drawing.Color.White;
            this.mainContentPanel.Controls.Add(this.eventDetailsRichTextBox);
            this.mainContentPanel.Controls.Add(this.eventDetailsLabel);
            this.mainContentPanel.Controls.Add(this.eventsListBox);
            this.mainContentPanel.Controls.Add(this.eventsTitleLabel);
            this.mainContentPanel.Location = new System.Drawing.Point(0, 180);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Size = new System.Drawing.Size(1000, 550);
            this.mainContentPanel.TabIndex = 2;
            // 
            // eventDetailsRichTextBox
            // 
            this.eventDetailsRichTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.eventDetailsRichTextBox.Location = new System.Drawing.Point(520, 50);
            this.eventDetailsRichTextBox.Name = "eventDetailsRichTextBox";
            this.eventDetailsRichTextBox.ReadOnly = true;
            this.eventDetailsRichTextBox.Size = new System.Drawing.Size(460, 480);
            this.eventDetailsRichTextBox.TabIndex = 3;
            this.eventDetailsRichTextBox.Text = "";
            // 
            // eventDetailsLabel
            // 
            this.eventDetailsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.eventDetailsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.eventDetailsLabel.Location = new System.Drawing.Point(520, 20);
            this.eventDetailsLabel.Name = "eventDetailsLabel";
            this.eventDetailsLabel.Size = new System.Drawing.Size(460, 25);
            this.eventDetailsLabel.TabIndex = 2;
            this.eventDetailsLabel.Text = "Event Details";
            // 
            // eventsListBox
            // 
            this.eventsListBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.eventsListBox.FormattingEnabled = true;
            this.eventsListBox.ItemHeight = 15;
            this.eventsListBox.Location = new System.Drawing.Point(30, 50);
            this.eventsListBox.Name = "eventsListBox";
            this.eventsListBox.Size = new System.Drawing.Size(470, 480);
            this.eventsListBox.TabIndex = 1;
            // 
            // eventsTitleLabel
            // 
            this.eventsTitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.eventsTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.eventsTitleLabel.Location = new System.Drawing.Point(30, 20);
            this.eventsTitleLabel.Name = "eventsTitleLabel";
            this.eventsTitleLabel.Size = new System.Drawing.Size(470, 25);
            this.eventsTitleLabel.TabIndex = 0;
            this.eventsTitleLabel.Text = "Upcoming Events";
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.sidebarPanel.Controls.Add(this.notificationCountLabel);
            this.sidebarPanel.Controls.Add(this.notificationsListBox);
            this.sidebarPanel.Controls.Add(this.notificationsLabel);
            this.sidebarPanel.Controls.Add(this.viewRecentButton);
            this.sidebarPanel.Controls.Add(this.viewRecommendationsButton);
            this.sidebarPanel.Controls.Add(this.recommendationsCountLabel);
            this.sidebarPanel.Controls.Add(this.recommendationsListBox);
            this.sidebarPanel.Controls.Add(this.recommendationsLabel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidebarPanel.Location = new System.Drawing.Point(1000, 180);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(400, 550);
            this.sidebarPanel.TabIndex = 3;
            // 
            // notificationCountLabel
            // 
            this.notificationCountLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.notificationCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.notificationCountLabel.Location = new System.Drawing.Point(20, 530);
            this.notificationCountLabel.Name = "notificationCountLabel";
            this.notificationCountLabel.Size = new System.Drawing.Size(360, 15);
            this.notificationCountLabel.TabIndex = 7;
            this.notificationCountLabel.Text = "0 pending notifications";
            // 
            // notificationsListBox
            // 
            this.notificationsListBox.BackColor = Color.FromArgb(52, 73, 94);
            this.notificationsListBox.ForeColor = Color.White;
            this.notificationsListBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.notificationsListBox.FormattingEnabled = true;
            this.notificationsListBox.ItemHeight = 13;
            this.notificationsListBox.Location = new System.Drawing.Point(20, 390);
            this.notificationsListBox.Name = "notificationsListBox";
            this.notificationsListBox.Size = new System.Drawing.Size(360, 134);
            this.notificationsListBox.TabIndex = 6;
            // 
            // notificationsLabel
            // 
            this.notificationsLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.notificationsLabel.ForeColor = System.Drawing.Color.White;
            this.notificationsLabel.Location = new System.Drawing.Point(20, 360);
            this.notificationsLabel.Name = "notificationsLabel";
            this.notificationsLabel.Size = new System.Drawing.Size(360, 25);
            this.notificationsLabel.TabIndex = 5;
            this.notificationsLabel.Text = "Recent Announcements";
            // 
            // viewRecentButton
            // 
            this.viewRecentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.viewRecentButton.FlatAppearance.BorderSize = 0;
            this.viewRecentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewRecentButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.viewRecentButton.ForeColor = System.Drawing.Color.White;
            this.viewRecentButton.Location = new System.Drawing.Point(200, 180);
            this.viewRecentButton.Name = "viewRecentButton";
            this.viewRecentButton.Size = new System.Drawing.Size(180, 35);
            this.viewRecentButton.TabIndex = 4;
            this.viewRecentButton.Text = "View Recently Viewed";
            this.viewRecentButton.UseVisualStyleBackColor = false;
            // 
            // viewRecommendationsButton
            // 
            this.viewRecommendationsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.viewRecommendationsButton.FlatAppearance.BorderSize = 0;
            this.viewRecommendationsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewRecommendationsButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.viewRecommendationsButton.ForeColor = System.Drawing.Color.White;
            this.viewRecommendationsButton.Location = new System.Drawing.Point(20, 180);
            this.viewRecommendationsButton.Name = "viewRecommendationsButton";
            this.viewRecommendationsButton.Size = new System.Drawing.Size(170, 35);
            this.viewRecommendationsButton.TabIndex = 3;
            this.viewRecommendationsButton.Text = "View All Recommendations";
            this.viewRecommendationsButton.UseVisualStyleBackColor = false;
            // 
            // recommendationsCountLabel
            // 
            this.recommendationsCountLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.recommendationsCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.recommendationsCountLabel.Location = new System.Drawing.Point(20, 155);
            this.recommendationsCountLabel.Name = "recommendationsCountLabel";
            this.recommendationsCountLabel.Size = new System.Drawing.Size(360, 15);
            this.recommendationsCountLabel.TabIndex = 2;
            this.recommendationsCountLabel.Text = "0 recommendations available";
            // 
            // recommendationsListBox
            // 
            this.recommendationsListBox.BackColor = Color.FromArgb(52, 73, 94);
            this.recommendationsListBox.ForeColor = Color.White;
            this.recommendationsListBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.recommendationsListBox.FormattingEnabled = true;
            this.recommendationsListBox.ItemHeight = 13;
            this.recommendationsListBox.Location = new System.Drawing.Point(20, 50);
            this.recommendationsListBox.Name = "recommendationsListBox";
            this.recommendationsListBox.Size = new System.Drawing.Size(360, 95);
            this.recommendationsListBox.TabIndex = 1;
            // 
            // recommendationsLabel
            // 
            this.recommendationsLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.recommendationsLabel.ForeColor = System.Drawing.Color.White;
            this.recommendationsLabel.Location = new System.Drawing.Point(20, 20);
            this.recommendationsLabel.Name = "recommendationsLabel";
            this.recommendationsLabel.Size = new System.Drawing.Size(360, 25);
            this.recommendationsLabel.TabIndex = 0;
            this.recommendationsLabel.Text = "Recommended for You";
            // 
            // statsPanel
            // 
            this.statsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.statsPanel.Controls.Add(this.statisticsRichTextBox);
            this.statsPanel.Controls.Add(this.statisticsLabel);
            this.statsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statsPanel.Location = new System.Drawing.Point(0, 730);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Size = new System.Drawing.Size(1400, 150);
            this.statsPanel.TabIndex = 4;
            // 
            // statisticsRichTextBox
            // 
            this.statisticsRichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.statisticsRichTextBox.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.statisticsRichTextBox.Location = new System.Drawing.Point(30, 50);
            this.statisticsRichTextBox.Name = "statisticsRichTextBox";
            this.statisticsRichTextBox.ReadOnly = true;
            this.statisticsRichTextBox.Size = new System.Drawing.Size(1340, 80);
            this.statisticsRichTextBox.TabIndex = 1;
            this.statisticsRichTextBox.Text = "";
            // 
            // statisticsLabel
            // 
            this.statisticsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.statisticsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.statisticsLabel.Location = new System.Drawing.Point(30, 15);
            this.statisticsLabel.Name = "statisticsLabel";
            this.statisticsLabel.Size = new System.Drawing.Size(200, 25);
            this.statisticsLabel.TabIndex = 0;
            this.statisticsLabel.Text = "Platform Statistics";
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonPanel.Controls.Add(this.backButton);
            this.buttonPanel.Controls.Add(this.refreshButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 880);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(1400, 60);
            this.buttonPanel.TabIndex = 5;
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(1250, 15);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(120, 35);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "← Back to Menu";
            this.backButton.UseVisualStyleBackColor = false;
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.Location = new System.Drawing.Point(1120, 15);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(120, 35);
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "🔄 Refresh";
            this.refreshButton.UseVisualStyleBackColor = false;
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.statusPanel.Controls.Add(this.statusLabel);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Location = new System.Drawing.Point(0, 940);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(1400, 25);
            this.statusPanel.TabIndex = 6;
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(30, 5);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(1340, 15);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Ready";
            // 
            // LocalEventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1400, 965);
            this.Controls.Add(this.statusPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.statsPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "LocalEventsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Events & Announcements - Municipal Services";
            this.headerPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.mainContentPanel.ResumeLayout(false);
            this.sidebarPanel.ResumeLayout(false);
            this.statsPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.statusPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}