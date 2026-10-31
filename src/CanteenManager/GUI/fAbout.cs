using System.Diagnostics;

namespace CanteenManager
{
    public partial class fAbout : Form
    {
        public fAbout()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var processStartInfor = new ProcessStartInfo("https://github.com/vqlong?tab=repositories")
            {
                UseShellExecute = true
            };

            Process.Start(processStartInfor);
            
        }
    }
}
