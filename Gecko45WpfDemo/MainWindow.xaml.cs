using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gecko;
using System.Windows.Forms.Integration;

namespace Gecko45WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isFirefoxInitialized = false;
        private int instaces = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddBrowser_Click(object sender, RoutedEventArgs e)
        {
            if (!isFirefoxInitialized)
            {
                Xpcom.Initialize("Firefox");
                isFirefoxInitialized = true;
            }
            WindowsFormsHost host = new WindowsFormsHost();
            GeckoWebBrowser browser = new GeckoWebBrowser();//{Dock = DockStyle.Fill};
            host.Child = browser;
            Grid grid = new Grid();
            grid.Children.Add(host);
            GridWeb.Children.Add(grid);
            Grid.SetRow(grid, 1);
            Grid.SetColumn(grid, instaces++);
            btnAddBrowser.Content += "*";
            browser.Navigate("https://google.com");
        }
    }
}
