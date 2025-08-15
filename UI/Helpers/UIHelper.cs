using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalServicesApp.UI.Helpers
{
    public class UIHelper
    {
        public static void ShowErrorMessage(string message)
        {
            ShowCustomMessageBox(message, "⚠️ Error", MessageBoxButtons.OK,
                Color.FromArgb(231, 76, 60), Color.FromArgb(192, 57, 43));
        }

        public static void ShowSuccessMessage(string message)
        {
            ShowCustomMessageBox(message, "✅ Success", MessageBoxButtons.OK,
                Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96));
        }

        public static void ShowInfoMessage(string message)
        {
            ShowCustomMessageBox(message, "ℹ️ Information", MessageBoxButtons.OK,
                Color.FromArgb(52, 152, 219), Color.FromArgb(41, 128, 185));
        }

        public static void ShowComingSoonMessage()
        {
            ShowCustomMessageBox(
                "🚀 This amazing feature is coming soon!\n\n" +
                "✨ We're working hard to bring you:\n" +
                "• Enhanced functionality\n" +
                "• Better user experience\n" +
                "• More interactive features\n\n" +
                "📧 Stay tuned for updates!",
                "🔮 Coming Soon",
                MessageBoxButtons.OK,
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(142, 68, 173)
            );
        }

        private static void ShowCustomMessageBox(string message, string title, MessageBoxButtons buttons, Color primaryColor, Color secondaryColor)
        {
            // Create custom form
            Form customMessageBox = new Form()
            {
                Size = new Size(450,450),
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true
            };

            // Create rounded region
            customMessageBox.Region = CreateRoundedRegion(customMessageBox.ClientRectangle, 20);

            // Add drop shadow effect
            customMessageBox.Paint += (sender, e) =>
            {
                DrawCustomMessageBoxBackground(e.Graphics, customMessageBox.ClientRectangle, primaryColor, secondaryColor);
            };

            // Title panel
            Panel titlePanel = new Panel()
            {
                Size = new Size(450, 60),
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };

            Label titleLabel = new Label()
            {
                Text = title,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(450, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            titlePanel.Controls.Add(titleLabel);

            // Message panel
            Panel messagePanel = new Panel()
            {
                Size = new Size(410, 160),
                Location = new Point(20, 70),
                BackColor = Color.White
            };

            // Add rounded corners to message panel
            messagePanel.Paint += (sender, e) =>
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 15;
                    Rectangle rect = messagePanel.ClientRectangle;
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    e.Graphics.FillPath(Brushes.White, path);
                    using (Pen pen = new Pen(Color.FromArgb(200, Color.LightGray), 1))
                    {
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };

            Label messageLabel = new Label()
            {
                Text = message,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(52, 73, 94),
                Size = new Size(390, 140),
                Location = new Point(10, 10),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter
            };

            messagePanel.Controls.Add(messageLabel);

            // Button panel
            Panel buttonPanel = new Panel()
            {
                Size = new Size(450, 60),
                Location = new Point(0, 240),
                BackColor = Color.Transparent
            };

            Button okButton = CreateStyledButton("✓ OK", primaryColor, secondaryColor);
            okButton.Location = new Point(175, 15);
            okButton.Click += (sender, e) => customMessageBox.Close();

            buttonPanel.Controls.Add(okButton);

            // Add all panels to form
            customMessageBox.Controls.Add(titlePanel);
            customMessageBox.Controls.Add(messagePanel);
            customMessageBox.Controls.Add(buttonPanel);

            // Add fade-in animation
            customMessageBox.Opacity = 0;
            Timer fadeTimer = new Timer() { Interval = 20 };
            fadeTimer.Tick += (sender, e) =>
            {
                if (customMessageBox.Opacity < 1)
                    customMessageBox.Opacity += 0.1;
                else
                {
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                }
            };
            fadeTimer.Start();

            // Show the custom message box
            customMessageBox.ShowDialog();
        }

        private static void DrawCustomMessageBoxBackground(Graphics g, Rectangle rect, Color primaryColor, Color secondaryColor)
        {
            // Main gradient background
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, primaryColor, secondaryColor, LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rect);
            }

            // Top highlight
            using (LinearGradientBrush highlight = new LinearGradientBrush(
                new Rectangle(rect.X, rect.Y, rect.Width, 60),
                Color.FromArgb(100, Color.White),
                Color.Transparent,
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(highlight, new Rectangle(rect.X, rect.Y, rect.Width, 60));
            }

            // Border
            using (Pen borderPen = new Pen(Color.FromArgb(100, Color.White), 2))
            {
                g.DrawRectangle(borderPen, rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
            }
        }

        private static Button CreateStyledButton(string text, Color primaryColor, Color secondaryColor)
        {
            Button button = new Button()
            {
                Text = text,
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = primaryColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.Region = CreateRoundedRegion(button.ClientRectangle, 10);

            // Hover effects
            button.MouseEnter += (s, e) => button.BackColor = secondaryColor;
            button.MouseLeave += (s, e) => button.BackColor = primaryColor;

            return button;
        }

        private static Region CreateRoundedRegion(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return new Region(path);
        }
    }

    // Custom progress bar
    public class CustomProgressBar : Control
    {
        private int _value = 0;
        private int _maximum = 100;
        private Color _progressColor = Color.FromArgb(46, 204, 113);
        private Color _backgroundColor = Color.FromArgb(236, 240, 241);

        public int Value
        {
            get { return _value; }
            set
            {
                _value = Math.Max(0, Math.Min(_maximum, value));
                Invalidate();
            }
        }

        public int Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = Math.Max(1, value);
                Invalidate();
            }
        }

        public Color ProgressColor
        {
            get { return _progressColor; }
            set
            {
                _progressColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;

            // Background
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                rect, _backgroundColor, Color.FromArgb(189, 195, 199), LinearGradientMode.Vertical))
            {
                FillRoundedRectangle(e.Graphics, bgBrush, rect, 8);
            }

            // Progress
            if (_value > 0)
            {
                int progressWidth = (int)((float)_value / _maximum * rect.Width);
                Rectangle progressRect = new Rectangle(rect.X, rect.Y, progressWidth, rect.Height);

                using (LinearGradientBrush progressBrush = new LinearGradientBrush(
                    progressRect, _progressColor, Color.FromArgb(_progressColor.R - 20, _progressColor.G - 20, _progressColor.B - 20),
                    LinearGradientMode.Vertical))
                {
                    FillRoundedRectangle(e.Graphics, progressBrush, progressRect, 8);
                }

                // Highlight
                Rectangle highlightRect = new Rectangle(progressRect.X, progressRect.Y, progressRect.Width, progressRect.Height / 2);
                using (LinearGradientBrush highlight = new LinearGradientBrush(
                    highlightRect, Color.FromArgb(80, Color.White), Color.Transparent, LinearGradientMode.Vertical))
                {
                    FillRoundedRectangle(e.Graphics, highlight, highlightRect, 8);
                }
            }

            // Border
            using (Pen borderPen = new Pen(Color.FromArgb(127, 140, 141), 1))
            {
                DrawRoundedRectangle(e.Graphics, borderPen, rect, 8);
            }

            base.OnPaint(e);
        }

        private void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle rect, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                graphics.FillPath(brush, path);
            }
        }

        private void DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle rect, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                graphics.DrawPath(pen, path);
            }
        }
    }
}