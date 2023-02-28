using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace DancingBars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                var random = new Random();
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Pb1.Value = random.Next(100);
                        Pb1.Background = new SolidColorBrush(Color.FromRgb(
                            (byte)random.Next(255),
                            (byte)random.Next(255),
                            (byte)random.Next(255)));
                        Pb1.Foreground = new SolidColorBrush(Color.FromRgb(
                            (byte)random.Next(255),
                            (byte)random.Next(255),
                            (byte)random.Next(255)));
                    });
                    Thread.Sleep(random.Next(100, 1000));
                }
            });
            Task.Run(() =>
            {
                var random = new Random();
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Pb2.Value = random.Next(100);
                        Pb2.Background = new SolidColorBrush(Color.FromRgb(
                            (byte)random.Next(255),
                            (byte)random.Next(255),
                            (byte)random.Next(255)));
                        Pb2.Foreground = new SolidColorBrush(Color.FromRgb(
                            (byte)random.Next(255),
                            (byte)random.Next(255),
                            (byte)random.Next(255)));
                    });
                    Thread.Sleep(random.Next(100, 1000));
                }
            });
            Task.Run(() =>
            {
                var random = new Random();
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Pb3.Value = random.Next(100);
                        Pb3.Background = new SolidColorBrush(Color.FromRgb(
                            (byte)random.Next(255),
                            (byte)random.Next(255),
                            (byte)random.Next(255)));
                        Pb3.Foreground = new SolidColorBrush(Color.FromRgb(
                            (byte)random.Next(255),
                            (byte)random.Next(255),
                            (byte)random.Next(255)));
                    });
                    Thread.Sleep(random.Next(100, 1000));
                }
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
