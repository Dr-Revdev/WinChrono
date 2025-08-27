using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinChrono
{
    public class MainForm : Form
    {

        private Label labelTime;
        private int elapsedMs = 0;
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

            UpdateView();
        }

        private void UpdateView()
        {
            var ts = TimeSpan.FromMilliseconds(elapsedMs);
            labelTime.Text = $"{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
        }
    }
}