using System.Drawing;
using System.Windows.Forms;

namespace WinChrono
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Chronomètre";
            ClientSize = new Size(400, 220);
            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}