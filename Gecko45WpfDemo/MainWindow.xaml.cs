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
using System.Windows.Threading;

namespace Gecko45WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WindowsFormsHost host;

        protected bool isDragging;
        private Point clickPosition;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAddBrowser_Click(object sender, RoutedEventArgs e)
        {
            btnAddBrowser.IsEnabled = false;

            Xpcom.Initialize("Firefox");
            host = new WindowsFormsHost();
            GeckoWebBrowser browser = new GeckoWebBrowser();// { Dock = System.Windows.Forms.DockStyle.Fill };
            host.Child = browser;
            GridWeb.Children.Add(host);

            browser.Navigate("https://google.com");
        }

        private void DraggableDock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            var draggableControl = sender as DockPanel;
            clickPosition = e.GetPosition(this);
            draggableControl.CaptureMouse();
        }

        private void DraggableDock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggable = sender as DockPanel;
            draggable.ReleaseMouseCapture();
            
        }

        private void DraggableDock_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableControl = sender as DockPanel;

            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(this.Parent as UIElement);

                var transform = draggableControl.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }

                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;

                host.InvalidateVisual();
            }
        }

        private void btnMoveBrowser_Click(object sender, RoutedEventArgs e)
        {
            DraggableDock.SetValue(Canvas.LeftProperty, (double)DraggableDock.GetValue(Canvas.LeftProperty) + 10);
        }

        private void btnMoveText_Click(object sender, RoutedEventArgs e)
        {
            lblText.SetValue(Canvas.LeftProperty, (double) lblText.GetValue(Canvas.LeftProperty) + 10);
        }
    }
}
