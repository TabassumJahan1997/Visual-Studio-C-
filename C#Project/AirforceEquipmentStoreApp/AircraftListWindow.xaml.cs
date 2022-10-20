using AirforceEquipmentStoreApp.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AircraftListWindow.xaml
    /// </summary>
    public partial class AircraftListWindow : Window
    {
        AircraftWindow aircraftWindow = new AircraftWindow();

        public string aircraftFilename = @"aircraft.json";
        public string folderpath = @"..\..\Images\";
        public string defaultimage= "DefaultAircraft.png";

        public AircraftListWindow()
        {
            InitializeComponent();          
        }


        /* Back to aircraft insert window */

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AircraftWindow aircraftWindow = new AircraftWindow();
            this.Hide();
            aircraftWindow.Show();
            
        }


        /* Exit */

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /* Edit button in list move to update window */

        private void AircraftInfoEdit_Click(object sender, RoutedEventArgs e)
        {
            AircraftUpdateWindow updateWindow = new AircraftUpdateWindow();
            updateWindow.Show();
            this.Hide();         

            Button b = sender as Button;
            Aircraft listaircraft = b.CommandParameter as Aircraft;

            updateWindow.txtSlNo.IsEnabled = false;
            updateWindow.txtSlNo.Text = listaircraft.SlNo.ToString();
            updateWindow.txtAircraft.Text = listaircraft.AircraftName.ToString();
            updateWindow.txtCrew.Text = listaircraft.Crew.ToString();
            updateWindow.cmbType.Text = listaircraft.Type.ToString();
            updateWindow.cmbStatus.Text = listaircraft.Status.ToString();
            updateWindow.txtOrigin.Text = listaircraft.Origin.ToString();
            updateWindow.txtRole.Text = listaircraft.Role.ToString();
            updateWindow.txtLength.Text = listaircraft.Length.ToString();
            updateWindow.txtMaxSpeed.Text = listaircraft.MaxSpeed.ToString();
            updateWindow.txtEmptyWeight.Text = listaircraft.EmptyWeight.ToString();
            updateWindow.datepickerFirstFlight.Text = listaircraft.FirstFlight.ToString();
            updateWindow.ImageDisplay.Source = listaircraft.Imagesource;

            if (listaircraft.IsContainArms == true)
            {
                updateWindow.rdbtnYes.IsChecked = true;
            }
            else if (listaircraft.IsContainArms == false)
            {
                updateWindow.rdbtnNo.IsChecked = true;
            }
        }

        /* Delete button in list */

        private void AircraftInfoDelete_Click(object sender, RoutedEventArgs e)
        {
            var jsonD = File.ReadAllText(aircraftFilename);
            var jsonObj = JObject.Parse(jsonD);
            var aircraftjson = jsonObj.GetValue("Aircraft").ToString();
            var aircraftlist = JsonConvert.DeserializeObject<List<Aircraft>>(aircraftjson);
            
            Button b = sender as Button;
            Aircraft airc = b.CommandParameter as Aircraft;

            int slno = airc.SlNo;
            
            ImageSource source = airc.Imagesource;

            MessageBoxResult result = MessageBox.Show($"Do you want to delete aircraft slno - {slno}", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes) 
            {
                var aircraftToDelete = aircraftlist.Find(x => x.SlNo == slno);
                string imagetitle = aircraftToDelete.ImageTitle;

                aircraftlist.Remove(aircraftToDelete);   
                
                JArray aircraftarray = JArray.FromObject(aircraftlist);       
                jsonObj["Aircraft"] = aircraftarray;                    
                var newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);

                try
                {
                    FileInfo thisFile = new FileInfo(aircraftWindow.GetImagePath(folderpath) + imagetitle);
                    if (thisFile.Name != defaultimage)
                    {
                        thisFile.Delete();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                File.WriteAllText(aircraftFilename, newJsonResult);

                MessageBox.Show("Data Deleted Successfully !!", "Delete", MessageBoxButton.OK, MessageBoxImage.Question);
                aircraftWindow.ShowAllAircraft();               
            }
            else
            {
                return;
            }
        }

        /* View button in list move to view window */

        private void AircraftInfoView_Click(object sender, RoutedEventArgs e)
        {
            AircraftViewWindow viewWindow = new AircraftViewWindow();
            viewWindow.Show();
            this.Hide();

            Button b = sender as Button;
            Aircraft listaircraft = b.CommandParameter as Aircraft;

            viewWindow.txtblname.Text = listaircraft.AircraftName.ToString();
            viewWindow.txtblslno.Text = listaircraft.SlNo.ToString();            
            viewWindow.txtblStatus.Text = $"Currently this aircraft is "+listaircraft.Status.ToString()+".";
            viewWindow.txtbltype.Text = listaircraft.Type.ToString();
            
            viewWindow.txtblaircraft.Text = listaircraft.AircraftName.ToString() + " is an aircraft manufactured by " + $"{ listaircraft.Origin.ToString()}";
            viewWindow.txtblrole.Text = $"The {viewWindow.txtblname.Text} is used as "+listaircraft.Role.ToString()+".";
            viewWindow.txtblCrew.Text = listaircraft.Crew.ToString();
            viewWindow.txtbllength.Text = listaircraft.Length.ToString()+$" meter";
            viewWindow.txtblweight.Text = listaircraft.EmptyWeight.ToString()+$" kg";
            viewWindow.txtblMaxSpeed.Text = listaircraft.MaxSpeed.ToString() + $" km/h";
            viewWindow.txtblFirstFlight.Text = "It conducted it's first flight on "+listaircraft.FirstFlight.ToLongDateString()+".";
            viewWindow.imagedisplay.Source = listaircraft.Imagesource;

            if (listaircraft.IsContainArms == true)
            {
                viewWindow.txtblcontainarms.Text = "Yes";
            }
            else if (listaircraft.IsContainArms == false)
            {
                viewWindow.txtblcontainarms.Text = "No";
            }
        }       
    }
}
