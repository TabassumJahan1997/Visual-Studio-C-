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
using System.Windows.Shapes;

namespace AirforceEquipmentStoreApp
{
    /// <summary>
    /// Interaction logic for AircraftViewWindow.xaml
    /// </summary>
    public partial class AircraftViewWindow : Window
    {
        AircraftWindow aircraft = new AircraftWindow();

        public AircraftViewWindow()
        {
            InitializeComponent();
        }

        /* Back to list window */

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AircraftListWindow listWindow = new AircraftListWindow();
            aircraft.ShowAllAircraft();
            this.Hide();
        }


        /* Exit */

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }      
    }
}
