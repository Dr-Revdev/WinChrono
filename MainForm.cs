using System;
using System.Drawing;
using System.Windows.Forms;


namespace WinChrono
{
    public class MainForm : Form
    {
        private System.Windows.Forms.Timer uiTimer;
        private Label labelTime;
        private Button btnStartPause;
        private Button btnReset;
        private int elapsedMs = 0;
        private bool isRunning = false;
        private DateTime? startUtc = null;
        public MainForm()
        {
            Text = "Chronomètre";
            ClientSize = new Size(400, 220);
            StartPosition = FormStartPosition.CenterScreen;

            labelTime = new Label
            {
                Dock = DockStyle.Top,
                Height = 100,
                Font = new Font(Font.FontFamily, 36, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(labelTime);

            btnStartPause = new Button
            {
                Text = "Démarrer",
                Width = 120,
                Height = 40,
                Left = 50,
                Top = 120,
                TabIndex = 0
            };
            btnStartPause.Click += BtnStartPause_Click;
            Controls.Add(btnStartPause);

            btnReset = new Button
            {
                Text = "Remise à zéro",
                Width = 120,
                Height = 40,
                Left = 230,
                Top = 120,
                TabIndex = 1
            };
            btnReset.Click += BtnReset_Click;
            Controls.Add(btnReset);

            AcceptButton = btnStartPause;
            CancelButton = btnReset;

            uiTimer = new System.Windows.Forms.Timer { Interval = 50 };
            uiTimer.Tick += UiTimer_Tick;

            KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            UpdateView();
        }

        private void BtnStartPause_Click(object? sender, EventArgs e)
        {
            if (!isRunning)
            {
                startUtc = DateTime.UtcNow;
                isRunning = true;
                btnStartPause.Text = "Pause";
                uiTimer.Start();
            }
            else
            {
                if (startUtc.HasValue)
                {
                    elapsedMs += (int)(DateTime.UtcNow - startUtc.Value).TotalMilliseconds;
                }
                startUtc = null;
                isRunning = false;
                btnStartPause.Text = "Démarrer";
                uiTimer.Stop();
                UpdateView();
            }
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            isRunning = false;
            startUtc = null;
            elapsedMs = 0;
            btnStartPause.Text = "Démarrer";
            uiTimer.Stop();
            UpdateView();
        }

        private void UpdateView()
        {
            int displayMs = elapsedMs;
            if (isRunning && startUtc.HasValue)
                displayMs = elapsedMs + (int)(DateTime.UtcNow - startUtc.Value).TotalMilliseconds;

            var ts = TimeSpan.FromMilliseconds(displayMs);
            labelTime.Text = $"{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
        }

        private void UiTimer_Tick(object? sender, EventArgs e)
        {
            if (isRunning)
                UpdateView();
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                BtnStartPause_Click(btnStartPause, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}