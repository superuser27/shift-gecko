using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Gecko;
using System.Windows.Forms.Integration;

namespace Gecko45WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WindowsFormsHost host;
        private GeckoWebBrowser browser;

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
            browser = new GeckoWebBrowser();// { Dock = System.Windows.Forms.DockStyle.Fill };
            host.Child = browser;
            // grid from xaml
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

                // how is this even working?!
                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;

                // this looks like the only way to update browser controll
                host.InvalidateVisual();
            }
        }

        private void btnMoveBrowser_Click(object sender, RoutedEventArgs e)
        {
            DraggableDock.SetValue(Canvas.LeftProperty, (double)DraggableDock.GetValue(Canvas.LeftProperty) + 10);
        }
    }
}
