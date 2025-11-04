using System.Windows.Forms;

namespace MunicipalServicesApp.UI.Forms
{
    partial class ServiceRequestStatusForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button clearSearchButton;
        private System.Windows.Forms.Button trackRequestButton; // Moved to top
        private System.Windows.Forms.Button refreshButton; // Moved to top

        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.ComboBox statusFilterComboBox;
        private System.Windows.Forms.ComboBox priorityFilterComboBox;
        private System.Windows.Forms.Label statusFilterLabel;
        private System.Windows.Forms.Label priorityFilterLabel;

        private System.Windows.Forms.Panel requestsPanel;
        private System.Windows.Forms.ListBox requestsListBox;
        private System.Windows.Forms.Label requestsCountLabel;

        private System.Windows.Forms.Panel detailsPanel;
        private System.Windows.Forms.RichTextBox detailsRichTextBox;

        private System.Windows.Forms.Panel visualizationPanel;
        private System.Windows.Forms.Label visualizationTitleLabel;
        private System.Windows.Forms.Button showBSTButton;
        private System.Windows.Forms.Button showAVLButton;
        private System.Windows.Forms.Button showGraphButton;
        private System.Windows.Forms.Button showHeapButton;

        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button backButton; // Only back button remains at bottom

        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.Label statsLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();

            this.mainPanel = new System.Windows.Forms.Panel();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.clearSearchButton = new System.Windows.Forms.Button();
            this.trackRequestButton = new System.Windows.Forms.Button(); // Moved to top
            this.refreshButton = new System.Windows.Forms.Button(); // Moved to top

            this.filterPanel = new System.Windows.Forms.Panel();
            this.statusFilterComboBox = new System.Windows.Forms.ComboBox();
            this.priorityFilterComboBox = new System.Windows.Forms.ComboBox();
            this.statusFilterLabel = new System.Windows.Forms.Label();
            this.priorityFilterLabel = new System.Windows.Forms.Label();

            this.requestsPanel = new System.Windows.Forms.Panel();
            this.requestsListBox = new System.Windows.Forms.ListBox();
            this.requestsCountLabel = new System.Windows.Forms.Label();

            this.detailsPanel = new System.Windows.Forms.Panel();
            this.detailsRichTextBox = new System.Windows.Forms.RichTextBox();

            this.visualizationPanel = new System.Windows.Forms.Panel();
            this.visualizationTitleLabel = new System.Windows.Forms.Label();
            this.showBSTButton = new System.Windows.Forms.Button();
            this.showAVLButton = new System.Windows.Forms.Button();
            this.showGraphButton = new System.Windows.Forms.Button();
            this.showHeapButton = new System.Windows.Forms.Button();

            this.buttonPanel = new System.Windows.Forms.Panel();
            this.backButton = new System.Windows.Forms.Button(); // Only back button remains

            this.statsPanel = new System.Windows.Forms.Panel();
            this.statsLabel = new System.Windows.Forms.Label();

            // Header Panel
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Size = new System.Drawing.Size(1200, 80);
            this.headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(20, 15);
            this.titleLabel.Size = new System.Drawing.Size(1160, 35);
            this.titleLabel.Text = "Service Request Status";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.White;
            this.subtitleLabel.Location = new System.Drawing.Point(20, 50);
            this.subtitleLabel.Size = new System.Drawing.Size(1160, 25);
            this.subtitleLabel.Text = "Track and monitor your municipal service requests";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.subtitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Controls.Add(this.subtitleLabel);

            // Main Panel
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 80);
            this.mainPanel.Size = new System.Drawing.Size(1200, 650);
            this.mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Search Panel - Now includes action buttons
            this.searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.searchPanel.Location = new System.Drawing.Point(20, 10);
            this.searchPanel.Size = new System.Drawing.Size(1160, 50);
            this.searchPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.searchTextBox.Location = new System.Drawing.Point(10, 12);
            this.searchTextBox.Size = new System.Drawing.Size(250, 25);
            this.searchTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.searchButton.Location = new System.Drawing.Point(270, 12);
            this.searchButton.Size = new System.Drawing.Size(80, 25);
            this.searchButton.Text = "Search";
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.clearSearchButton.Location = new System.Drawing.Point(360, 12);
            this.clearSearchButton.Size = new System.Drawing.Size(80, 25);
            this.clearSearchButton.Text = "Clear";
            this.clearSearchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.clearSearchButton.ForeColor = System.Drawing.Color.White;
            this.clearSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearSearchButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Action buttons moved to top right
            this.trackRequestButton.Location = new System.Drawing.Point(850, 12);
            this.trackRequestButton.Size = new System.Drawing.Size(120, 25);
            this.trackRequestButton.Text = "Track Request";
            this.trackRequestButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.trackRequestButton.ForeColor = System.Drawing.Color.White;
            this.trackRequestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.trackRequestButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.refreshButton.Location = new System.Drawing.Point(980, 12);
            this.refreshButton.Size = new System.Drawing.Size(80, 25);
            this.refreshButton.Text = "Refresh";
            this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.refreshButton.ForeColor = System.Drawing.Color.White;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.clearSearchButton);
            this.searchPanel.Controls.Add(this.trackRequestButton);
            this.searchPanel.Controls.Add(this.refreshButton);

            // Filter Panel
            this.filterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.filterPanel.Location = new System.Drawing.Point(20, 70);
            this.filterPanel.Size = new System.Drawing.Size(1160, 50);
            this.filterPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.statusFilterLabel.Location = new System.Drawing.Point(10, 15);
            this.statusFilterLabel.Size = new System.Drawing.Size(80, 25);
            this.statusFilterLabel.Text = "Status:";
            this.statusFilterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusFilterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.statusFilterComboBox.Location = new System.Drawing.Point(100, 15);
            this.statusFilterComboBox.Size = new System.Drawing.Size(150, 25);
            this.statusFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusFilterComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.priorityFilterLabel.Location = new System.Drawing.Point(270, 15);
            this.priorityFilterLabel.Size = new System.Drawing.Size(80, 25);
            this.priorityFilterLabel.Text = "Priority:";
            this.priorityFilterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.priorityFilterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.priorityFilterComboBox.Location = new System.Drawing.Point(360, 15);
            this.priorityFilterComboBox.Size = new System.Drawing.Size(150, 25);
            this.priorityFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.priorityFilterComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.filterPanel.Controls.Add(this.statusFilterLabel);
            this.filterPanel.Controls.Add(this.statusFilterComboBox);
            this.filterPanel.Controls.Add(this.priorityFilterLabel);
            this.filterPanel.Controls.Add(this.priorityFilterComboBox);

            // Requests Panel
            this.requestsPanel.BackColor = System.Drawing.Color.White;
            this.requestsPanel.Location = new System.Drawing.Point(20, 130);
            this.requestsPanel.Size = new System.Drawing.Size(400, 300);
            this.requestsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;

            this.requestsListBox.Location = new System.Drawing.Point(10, 10);
            this.requestsListBox.Size = new System.Drawing.Size(380, 250);
            this.requestsListBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.requestsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.requestsCountLabel.Location = new System.Drawing.Point(10, 270);
            this.requestsCountLabel.Size = new System.Drawing.Size(380, 20);
            this.requestsCountLabel.Text = "0 requests found";
            this.requestsCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.requestsCountLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.requestsPanel.Controls.Add(this.requestsListBox);
            this.requestsPanel.Controls.Add(this.requestsCountLabel);

            // Details Panel
            this.detailsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.detailsPanel.Location = new System.Drawing.Point(430, 130);
            this.detailsPanel.Size = new System.Drawing.Size(750, 300);
            this.detailsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.detailsRichTextBox.Location = new System.Drawing.Point(10, 10);
            this.detailsRichTextBox.Size = new System.Drawing.Size(730, 280);
            this.detailsRichTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.detailsRichTextBox.ReadOnly = true;
            this.detailsRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.detailsRichTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.detailsPanel.Controls.Add(this.detailsRichTextBox);

            // Visualization Panel
            this.visualizationPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.visualizationPanel.Location = new System.Drawing.Point(20, 440);
            this.visualizationPanel.Size = new System.Drawing.Size(1160, 150);
            this.visualizationPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.visualizationTitleLabel.Location = new System.Drawing.Point(10, 10);
            this.visualizationTitleLabel.Size = new System.Drawing.Size(1140, 25);
            this.visualizationTitleLabel.Text = "Advanced Data Structure Visualizations";
            this.visualizationTitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.visualizationTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.visualizationTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.showBSTButton.Location = new System.Drawing.Point(50, 45);
            this.showBSTButton.Size = new System.Drawing.Size(150, 35);
            this.showBSTButton.Text = "Show BST";
            this.showBSTButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.showBSTButton.ForeColor = System.Drawing.Color.White;
            this.showBSTButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showBSTButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.showAVLButton.Location = new System.Drawing.Point(220, 45);
            this.showAVLButton.Size = new System.Drawing.Size(150, 35);
            this.showAVLButton.Text = "Show AVL Tree";
            this.showAVLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.showAVLButton.ForeColor = System.Drawing.Color.White;
            this.showAVLButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showAVLButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.showGraphButton.Location = new System.Drawing.Point(390, 45);
            this.showGraphButton.Size = new System.Drawing.Size(150, 35);
            this.showGraphButton.Text = "Show Graph";
            this.showGraphButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.showGraphButton.ForeColor = System.Drawing.Color.White;
            this.showGraphButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showGraphButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.showHeapButton.Location = new System.Drawing.Point(560, 45);
            this.showHeapButton.Size = new System.Drawing.Size(150, 35);
            this.showHeapButton.Text = "Show Heap";
            this.showHeapButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.showHeapButton.ForeColor = System.Drawing.Color.White;
            this.showHeapButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showHeapButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            this.visualizationPanel.Controls.Add(this.visualizationTitleLabel);
            this.visualizationPanel.Controls.Add(this.showBSTButton);
            this.visualizationPanel.Controls.Add(this.showAVLButton);
            this.visualizationPanel.Controls.Add(this.showGraphButton);
            this.visualizationPanel.Controls.Add(this.showHeapButton);

            // Button Panel - Now only contains Back button
            this.buttonPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonPanel.Location = new System.Drawing.Point(20, 600);
            this.buttonPanel.Size = new System.Drawing.Size(1160, 50);
            this.buttonPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.backButton.Location = new System.Drawing.Point(1050, 10);
            this.backButton.Size = new System.Drawing.Size(100, 35);
            this.backButton.Text = "Back";
            this.backButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            this.buttonPanel.Controls.Add(this.backButton);

            // Stats Panel
            this.statsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.statsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statsPanel.Location = new System.Drawing.Point(0, 730);
            this.statsPanel.Size = new System.Drawing.Size(1200, 30);
            this.statsPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.statsLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statsLabel.ForeColor = System.Drawing.Color.White;
            this.statsLabel.Location = new System.Drawing.Point(10, 5);
            this.statsLabel.Size = new System.Drawing.Size(1180, 20);
            this.statsLabel.Text = "Loading statistics...";
            this.statsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statsLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            this.statsPanel.Controls.Add(this.statsLabel);

            // Add panels to main panel
            this.mainPanel.Controls.Add(this.searchPanel);
            this.mainPanel.Controls.Add(this.filterPanel);
            this.mainPanel.Controls.Add(this.requestsPanel);
            this.mainPanel.Controls.Add(this.detailsPanel);
            this.mainPanel.Controls.Add(this.visualizationPanel);
            this.mainPanel.Controls.Add(this.buttonPanel);

            // Add to form
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.statsPanel);
            this.Controls.Add(this.headerPanel);

            // Form settings
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Text = "Service Request Status - Municipal Services";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}