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

namespace AirforceEquipmentStoreApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CanvasOptions.Visibility = Visibility.Hidden;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            CanvasOptions.Visibility = Visibility.Visible;

            BorderWelcome.Visibility = Visibility.Hidden;
            btnStart.Visibility = Visibility.Hidden;
            btnExit.Visibility = Visibility.Hidden;
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        

        private void BtnAircraft_Click(object sender, RoutedEventArgs e)
        {
            AircraftWindow aircraftWindow = new AircraftWindow();
            aircraftWindow.Show();
            this.Hide();
        }

        private void BtnOptionsExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            CanvasOptions.Visibility = Visibility.Hidden;

            BorderWelcome.Visibility = Visibility.Visible;
            btnStart.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;

        }

        

        
    }
}
