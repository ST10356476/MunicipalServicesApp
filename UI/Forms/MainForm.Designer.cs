namespace MunicipalServicesApp.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Form Controls - Clean Professional Design
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Label logoLabel;
        private System.Windows.Forms.Label appTitleLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label clockLabel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Panel buttonsPanel;
        private System.Windows.Forms.Button reportIssuesBtn;
        private System.Windows.Forms.Button localEventsBtn;
        private System.Windows.Forms.Button serviceStatusBtn;
        private System.Windows.Forms.Panel engagementPanel;
        private System.Windows.Forms.Label engagementTitleLabel;
        private System.Windows.Forms.Label engagementLabel;
        private System.Windows.Forms.ProgressBar engagementProgressBar;
        private System.Windows.Forms.Label statsLabel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.Label footerLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userGuideToolStripMenuItem;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.clockLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.appTitleLabel = new System.Windows.Forms.Label();
            this.logoLabel = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.footerLabel = new System.Windows.Forms.Label();
            this.engagementPanel = new System.Windows.Forms.Panel();
            this.statsLabel = new System.Windows.Forms.Label();
            this.engagementProgressBar = new System.Windows.Forms.ProgressBar();
            this.engagementLabel = new System.Windows.Forms.Label();
            this.engagementTitleLabel = new System.Windows.Forms.Label();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.serviceStatusBtn = new System.Windows.Forms.Button();
            this.localEventsBtn = new System.Windows.Forms.Button();
            this.reportIssuesBtn = new System.Windows.Forms.Button();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sidebarPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.engagementPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.sidebarPanel.Controls.Add(this.clockLabel);
            this.sidebarPanel.Controls.Add(this.versionLabel);
            this.sidebarPanel.Controls.Add(this.appTitleLabel);
            this.sidebarPanel.Controls.Add(this.logoLabel);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 24);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(220, 676);
            this.sidebarPanel.TabIndex = 0;
            // 
            // clockLabel
            // 
            this.clockLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clockLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.clockLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.clockLabel.Location = new System.Drawing.Point(20, 600);
            this.clockLabel.Name = "clockLabel";
            this.clockLabel.Size = new System.Drawing.Size(180, 60);
            this.clockLabel.TabIndex = 3;
            this.clockLabel.Text = "Loading...";
            this.clockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionLabel
            // 
            this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.versionLabel.Location = new System.Drawing.Point(20, 150);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(180, 20);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Text = "Version 2.0";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // appTitleLabel
            // 
            this.appTitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.appTitleLabel.ForeColor = System.Drawing.Color.White;
            this.appTitleLabel.Location = new System.Drawing.Point(20, 90);
            this.appTitleLabel.Name = "appTitleLabel";
            this.appTitleLabel.Size = new System.Drawing.Size(180, 60);
            this.appTitleLabel.TabIndex = 1;
            this.appTitleLabel.Text = "Municipal Services\r\nSouth Africa";
            this.appTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logoLabel
            // 
            this.logoLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.logoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.logoLabel.Location = new System.Drawing.Point(20, 30);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Size = new System.Drawing.Size(180, 50);
            this.logoLabel.TabIndex = 0;
            this.logoLabel.Text = "🏛️";
            this.logoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.headerPanel.Controls.Add(this.subtitleLabel);
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(220, 24);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(780, 120);
            this.headerPanel.TabIndex = 1;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.subtitleLabel.Location = new System.Drawing.Point(30, 70);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(720, 30);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Streamlining Municipal Services for South African Citizens";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(30, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(720, 50);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Municipal Services Portal";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.mainContentPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mainContentPanel.BackgroundImage")));
            this.mainContentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainContentPanel.Controls.Add(this.footerPanel);
            this.mainContentPanel.Controls.Add(this.engagementPanel);
            this.mainContentPanel.Controls.Add(this.buttonsPanel);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(220, 144);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(30);
            this.mainContentPanel.Size = new System.Drawing.Size(780, 556);
            this.mainContentPanel.TabIndex = 2;
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.Transparent;
            this.footerPanel.Controls.Add(this.footerLabel);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(30, 496);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(720, 30);
            this.footerPanel.TabIndex = 2;
            // 
            // footerLabel
            // 
            this.footerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.footerLabel.ForeColor = System.Drawing.Color.Black;
            this.footerLabel.Location = new System.Drawing.Point(0, 0);
            this.footerLabel.Name = "footerLabel";
            this.footerLabel.Size = new System.Drawing.Size(720, 30);
            this.footerLabel.TabIndex = 0;
            this.footerLabel.Text = "© 2025 Municipal Services - Improving Citizen Engagement";
            this.footerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // engagementPanel
            // 
            this.engagementPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.engagementPanel.BackColor = System.Drawing.Color.White;
            this.engagementPanel.Controls.Add(this.statsLabel);
            this.engagementPanel.Controls.Add(this.engagementProgressBar);
            this.engagementPanel.Controls.Add(this.engagementLabel);
            this.engagementPanel.Controls.Add(this.engagementTitleLabel);
            this.engagementPanel.Location = new System.Drawing.Point(50, 320);
            this.engagementPanel.Name = "engagementPanel";
            this.engagementPanel.Size = new System.Drawing.Size(680, 150);
            this.engagementPanel.TabIndex = 1;
            // 
            // statsLabel
            // 
            this.statsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.statsLabel.Location = new System.Drawing.Point(30, 120);
            this.statsLabel.Name = "statsLabel";
            this.statsLabel.Size = new System.Drawing.Size(620, 25);
            this.statsLabel.TabIndex = 3;
            this.statsLabel.Text = "Reports Submitted: 0 | Engagement Level: 25%";
            this.statsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // engagementProgressBar
            // 
            this.engagementProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.engagementProgressBar.Location = new System.Drawing.Point(60, 95);
            this.engagementProgressBar.Name = "engagementProgressBar";
            this.engagementProgressBar.Size = new System.Drawing.Size(560, 20);
            this.engagementProgressBar.TabIndex = 2;
            this.engagementProgressBar.Value = 25;
            // 
            // engagementLabel
            // 
            this.engagementLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.engagementLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.engagementLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.engagementLabel.Location = new System.Drawing.Point(30, 60);
            this.engagementLabel.Name = "engagementLabel";
            this.engagementLabel.Size = new System.Drawing.Size(620, 25);
            this.engagementLabel.TabIndex = 1;
            this.engagementLabel.Text = "Community Engagement Level: Building...";
            this.engagementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // engagementTitleLabel
            // 
            this.engagementTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.engagementTitleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.engagementTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.engagementTitleLabel.Location = new System.Drawing.Point(30, 20);
            this.engagementTitleLabel.Name = "engagementTitleLabel";
            this.engagementTitleLabel.Size = new System.Drawing.Size(620, 30);
            this.engagementTitleLabel.TabIndex = 0;
            this.engagementTitleLabel.Text = "Community Engagement Dashboard";
            this.engagementTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonsPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonsPanel.Controls.Add(this.serviceStatusBtn);
            this.buttonsPanel.Controls.Add(this.localEventsBtn);
            this.buttonsPanel.Controls.Add(this.reportIssuesBtn);
            this.buttonsPanel.Location = new System.Drawing.Point(50, 50);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(680, 250);
            this.buttonsPanel.TabIndex = 0;
            // 
            // serviceStatusBtn
            // 
            this.serviceStatusBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.serviceStatusBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.serviceStatusBtn.Enabled = false;
            this.serviceStatusBtn.FlatAppearance.BorderSize = 0;
            this.serviceStatusBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serviceStatusBtn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.serviceStatusBtn.ForeColor = System.Drawing.Color.White;
            this.serviceStatusBtn.Location = new System.Drawing.Point(380, 120);
            this.serviceStatusBtn.Name = "serviceStatusBtn";
            this.serviceStatusBtn.Size = new System.Drawing.Size(200, 50);
            this.serviceStatusBtn.TabIndex = 2;
            this.serviceStatusBtn.Text = "Service Request\r\nStatus";
            this.serviceStatusBtn.UseVisualStyleBackColor = false;
            // 
            // localEventsBtn
            // 
            this.localEventsBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.localEventsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.localEventsBtn.Enabled = true;
            this.localEventsBtn.FlatAppearance.BorderSize = 0;
            this.localEventsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.localEventsBtn.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.localEventsBtn.ForeColor = System.Drawing.Color.White;
            this.localEventsBtn.Location = new System.Drawing.Point(100, 120);
            this.localEventsBtn.Name = "localEventsBtn";
            this.localEventsBtn.Size = new System.Drawing.Size(200, 50);
            this.localEventsBtn.TabIndex = 1;
            this.localEventsBtn.Text = "Local Events &\r\nAnnouncements";
            this.localEventsBtn.UseVisualStyleBackColor = false;
            // 
            // reportIssuesBtn
            // 
            this.reportIssuesBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.reportIssuesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.reportIssuesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.reportIssuesBtn.FlatAppearance.BorderSize = 0;
            this.reportIssuesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reportIssuesBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.reportIssuesBtn.ForeColor = System.Drawing.Color.White;
            this.reportIssuesBtn.Location = new System.Drawing.Point(240, 30);
            this.reportIssuesBtn.Name = "reportIssuesBtn";
            this.reportIssuesBtn.Size = new System.Drawing.Size(200, 60);
            this.reportIssuesBtn.TabIndex = 0;
            this.reportIssuesBtn.Text = "Report Issues";
            this.reportIssuesBtn.UseVisualStyleBackColor = false;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.mainMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1000, 24);
            this.mainMenuStrip.TabIndex = 3;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.userGuideToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // userGuideToolStripMenuItem
            // 
            this.userGuideToolStripMenuItem.Name = "userGuideToolStripMenuItem";
            this.userGuideToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.userGuideToolStripMenuItem.Text = "User Guide";
            this.userGuideToolStripMenuItem.Click += new System.EventHandler(this.userGuideToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Municipal Services Portal - South Africa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            this.sidebarPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.mainContentPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.engagementPanel.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}