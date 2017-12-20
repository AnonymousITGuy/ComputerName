using System.Windows;
using System.Net;
using System.Linq;

namespace ComputerName

{
    public partial class MainWindow : Window
    {
        IPAddress[] addresses;
        string connectionStatus;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StatusIndicator_Initialized(object sender, System.EventArgs e)
        {

            StatusIndicator.Fill = System.Windows.Media.Brushes.Red;
            connectionStatus = "Connection Not Successful";

            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("https://www.google.co.uk"))
                {
                    StatusIndicator.Fill = System.Windows.Media.Brushes.Orange;
                    connectionStatus = "Internet Connection Successful";
                }
            }
            catch { }

            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://internalTestResource.company.com"))
                {
                    StatusIndicator.Fill = System.Windows.Media.Brushes.Green;
                    connectionStatus = "Internal Connection Successful";
                }
            }
            catch { }
            }

        private void MachineName_Initialized(object sender, System.EventArgs e)
        {
            MachineName.Text = (System.Environment.MachineName);
        }

        private void Domain_Initialized(object sender, System.EventArgs e)
        {
            Domain.Text = (System.Environment.UserDomainName);
        }

        private void IPs_Initialized(object sender, System.EventArgs e)
        {
            addresses = Dns.GetHostAddresses(Dns.GetHostName()).Where(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();

            foreach (object address in addresses)
            {
                IPs.Items.Add(address);
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            string copiedDetails = MachineName.Text + "\r\n" + Domain.Text + "\r\n" + connectionStatus + "\r\n";

            foreach (var ipAddress in addresses)
            {
                copiedDetails += ipAddress.ToString() + "\r\n";
            }

            Clipboard.SetText(copiedDetails);
        }
    }
}
