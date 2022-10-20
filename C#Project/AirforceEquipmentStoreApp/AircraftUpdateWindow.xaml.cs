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
    /// Interaction logic for AircraftUpdateWindow.xaml
    /// </summary>
    public partial class AircraftUpdateWindow : Window
    {
        public string aircraftFilename = @"aircraft.json";
        public string folderPath = @"..\..\Images\";
        public string defaultImage = "DefaultAircraft.png";
        public FileInfo TempImageFile { get; set; }
        public FileInfo OldImageFile { get; set; }

        AircraftWindow aircraftWindow = new AircraftWindow();

        public AircraftUpdateWindow()
        {
            InitializeComponent();
            string[] type = new string[]
            {
                "Combat Aircraft",
                "Transport",
                "Trainer Aircraft",
                "Helicopter",
                "UAV"
            };
            cmbType.ItemsSource = type;
            cmbType.SelectedIndex = -1;

            string[] status = new string[]
            {
                "In Service",
                "On Order",
                "Crashed",
                "Retired",
                "In Repair"
            };
            cmbStatus.ItemsSource = status;
            cmbStatus.SelectedIndex = -1;
        }


        /* Update */

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var slno = int.Parse(txtSlNo.Text);
            var name = txtAircraft.Text;
            var origin = txtOrigin.Text;
            var crew = int.Parse(txtCrew.Text);
            var type = cmbType.SelectedItem.ToString();
            var status = cmbStatus.SelectedItem.ToString();
            var role = txtRole.Text;
            var firstflight = Convert.ToDateTime(datepickerFirstFlight.ToString()).Date;
            var length = double.Parse(txtLength.Text);
            var weight = double.Parse(txtEmptyWeight.Text);
            var maxspeed = double.Parse(txtMaxSpeed.Text);
            var containarms = true;

            if (rdbtnYes.IsChecked == true)
            {
                 containarms=true;
            }
            else if (rdbtnNo.IsChecked == true)
            {
                containarms = false;
            }

            var json = File.ReadAllText(aircraftFilename);
            var jsonObject = JObject.Parse(json);
            var aircraftJson = jsonObject.GetValue("Aircraft").ToString();
            var aircraftlist = JsonConvert.DeserializeObject<List<Aircraft>>(aircraftJson);

            foreach (var item in aircraftlist.Where(a=>a.SlNo==slno))
            {
                item.SlNo = slno;
                item.AircraftName = name;
                item.Origin = origin;
                item.Crew = crew;
                item.Type = type;
                item.Status = status;
                item.Role = role;
                item.Length = length;
                item.EmptyWeight = weight;
                item.MaxSpeed = maxspeed;
                item.FirstFlight = firstflight;
                item.IsContainArms = containarms;

                OldImageFile = (item.ImageTitle != defaultImage) ? new FileInfo(aircraftWindow.GetImagePath(folderPath) + item.ImageTitle) : null;

                if(TempImageFile!=null && OldImageFile == null)
                {
                    TempImageFile.CopyTo(aircraftWindow.GetImagePath(folderPath) + item.SlNo + TempImageFile.Extension);

                    item.ImageTitle = item.SlNo + TempImageFile.Extension;

                    TempImageFile = null;
                }

                if(OldImageFile!=null&& TempImageFile!=null && File.Exists(OldImageFile.FullName))
                {
                    item.ImageTitle = item.SlNo + TempImageFile.Extension;

                    OldImageFile.Delete();

                    TempImageFile.CopyTo(aircraftWindow.GetImagePath(folderPath) + slno + TempImageFile.Extension);

                    TempImageFile = null;
                }

            }

            var aircraftarray = JArray.FromObject(aircraftlist);
            jsonObject["Aircraft"] = aircraftarray;
            string output = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            File.WriteAllText(aircraftFilename, output);

            this.Close();
            aircraftWindow.ShowAllAircraft();
            MessageBox.Show("Data Updated Successfully", "Update", MessageBoxButton.OK, MessageBoxImage.Information);

        }


        /* UpdateImage */

        private void BtnUpdateImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.png;)|*.jpg; *.jpeg; *.png;";
            fd.Title = "Select an Image";

            if (fd.ShowDialog().Value == true)
            {
                ImageDisplay.Source = aircraftWindow.ImageInstance(new Uri(fd.FileName));
                TempImageFile = new FileInfo(fd.FileName);
            }
        }


        /* Back */

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            AircraftListWindow listWindow = new AircraftListWindow();
            listWindow.Show();
            aircraftWindow.ShowAllAircraft();
            this.Hide();
        }


        /* Show All */

        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            aircraftWindow.ShowAllAircraft();
        }


        /* Exit */

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
