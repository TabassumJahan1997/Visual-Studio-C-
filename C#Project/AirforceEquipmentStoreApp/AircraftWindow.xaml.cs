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
    /// Interaction logic for AircraftWindow.xaml
    /// </summary>
    public partial class AircraftWindow : Window
    {
        public string aircraftFilename = @"aircraft.json";
        public string defaultImage = "DefaultAircraft.png";
        public string folderPath = @"..\..\Images\";

        public FileInfo TempImageFile { get; set; }
        public BitmapImage DefaultImage => new BitmapImage(new Uri(GetImagePath(folderPath) + defaultImage));

        
        /*Create image instance rather than referencing image file*/

        public ImageSource ImageInstance(Uri path)  
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = path;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.DecodePixelWidth = 300;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        /*Method to Get the Image Directory Path Where Image is stored*/

        public string GetImagePath(string folderpath)    
        {
            var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            string assemblyDirectory = System.IO.Path.GetDirectoryName(currentAssembly.Location);             
            string ImagePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(assemblyDirectory, folderpath));    // ..\..\ Navigate two levels up => Project folder

            return ImagePath;
        }


        /*Method to check whether file contains json format or not*/

        public bool IsValidJson(string data)   
        {
            try
            {
                var temp = JObject.Parse(data);  
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*Method to check if "Aircraft" exists in json file*/

        public bool IsParentnodeExists(string parentnode,string filename)      
        {
            string jsonFileData = File.ReadAllText(filename);
            var jsonobject = JObject.Parse(jsonFileData);
            var aircraftJson = jsonobject[parentnode];     

            return (aircraftJson != null) ? true : false;   // if false returns null
        }


        /*Method to keep slno unique*/

        public bool IsSlnoExists(int inputSlno)    
        {
            string jsonFileData = File.ReadAllText(aircraftFilename);
            var jsonObject = JObject.Parse(jsonFileData);              
            var aircraftJson = jsonObject.GetValue("Aircraft").ToString();
            var aircraftList = JsonConvert.DeserializeObject<List<Aircraft>>(aircraftJson);

            var exists = aircraftList.Find(a => a.SlNo == inputSlno);                 

            if (exists != null)
            {
                MessageBox.Show($"Sl no. - {exists.SlNo} exists\nTry with different Sl no.", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else
            {
                return false;
            }

        }

        public AircraftWindow()
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
            cmbStatus.SelectedIndex = 0;

            var path = System.IO.Path.GetDirectoryName(GetImagePath(folderPath));
            if (!File.Exists(aircraftFilename))
            {
                File.CreateText(aircraftFilename).Close();
            }
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            ImageDisplay.Source = DefaultImage;
            

        }

        /* Save */

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Aircraft aircraft = new Aircraft();

            try
            {
                aircraft.ImageTitle = (TempImageFile != null) ? $"{int.Parse(txtSlno.Text) + TempImageFile.Extension}" : defaultImage;
                aircraft.SlNo = int.Parse(txtSlno.Text);
                aircraft.AircraftName = txtAircraft.Text;
                aircraft.Origin = txtOrigin.Text;
                aircraft.Type = cmbType.SelectedItem.ToString();
                aircraft.Status = cmbStatus.SelectedItem.ToString();
                aircraft.FirstFlight = Convert.ToDateTime(datepickerFirstFlight.ToString());
                aircraft.Role = txtRole.Text;
                aircraft.Crew = int.Parse(txtCrew.Text);
                aircraft.Length = double.Parse(txtLength.Text);
                aircraft.EmptyWeight = double.Parse(txtEmptyWeight.Text);
                aircraft.MaxSpeed = double.Parse(txtMaxSpeed.Text);


                if (rdbtnYes.IsChecked == true)
                {
                    aircraft.IsContainArms = true;
                }
                else if (rdbtnNo.IsChecked == true)
                {
                    aircraft.IsContainArms = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Exception",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }           

            string jsonFiledata = File.ReadAllText(aircraftFilename);

            if(IsValidJson(jsonFiledata) && IsParentnodeExists("Aircraft", aircraftFilename) && !IsSlnoExists(aircraft.SlNo))
            {
                var jsonObject = JObject.Parse(jsonFiledata);

                var aircraftJson = jsonObject.GetValue("Aircraft").ToString();

                var aircraftList = JsonConvert.DeserializeObject<List<Aircraft>>(aircraftJson);

                aircraftList.Add(aircraft);

                JArray aircraftArray = JArray.FromObject(aircraftList);

                jsonObject["Aircraft"] = aircraftArray;

                var newJsonResult = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

                if (TempImageFile != null)
                {
                    TempImageFile.CopyTo(GetImagePath(folderPath) + aircraft.ImageTitle);
                    TempImageFile = null;
                    ImageDisplay.Source = DefaultImage;
                }

                File.WriteAllText(aircraftFilename, newJsonResult);
            }
            else
            {
                return;
            }
            
            

            if (!IsValidJson(jsonFiledata))
            {
                var airc = new
                {
                    Aircraft = new Aircraft[]
                    {
                        aircraft
                    }
                };

                string newJsonResult = JsonConvert.SerializeObject(airc, Formatting.Indented);

                if (TempImageFile != null)
                {
                    TempImageFile.CopyTo(GetImagePath(folderPath) + aircraft.ImageTitle);
                    TempImageFile = null;
                    ImageDisplay.Source = DefaultImage;
                }

                File.WriteAllText(aircraftFilename, newJsonResult);

            }

            MessageBox.Show("Data saved successfully.", "Save", MessageBoxButton.OK, MessageBoxImage.Information);
            ShowAllAircraft();
        }


        /* Show All in list window */

        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            ShowAllAircraft();
        }


        /*Method to show all aircraft in list window*/
        
        public void ShowAllAircraft()
        {
            AircraftListWindow listWindow = new AircraftListWindow();

            try
            {
                var jsonFiledata = File.ReadAllText(aircraftFilename);
                var jsonObject = JObject.Parse(jsonFiledata);
                if (jsonObject != null)
                {
                    JArray aircraftArray = (JArray)jsonObject["Aircraft"];

                    List<Aircraft> aircraftList = new List<Aircraft>();

                    foreach (var item in aircraftArray)
                    {
                        aircraftList.Add(new Aircraft()
                        {
                            SlNo = Convert.ToInt32(item["SlNo"]),
                            AircraftName = item["AircraftName"].ToString(),
                            Origin = item["Origin"].ToString(),
                            Type = item["Type"].ToString(),
                            Status = item["Status"].ToString(),
                            FirstFlight = Convert.ToDateTime(item["FirstFlight"]).Date,
                            Role = item["Role"].ToString(),
                            Crew = Convert.ToInt32(item["Crew"]),
                            Length = Convert.ToDouble(item["Length"]),
                            EmptyWeight = Convert.ToDouble(item["EmptyWeight"]),
                            MaxSpeed = Convert.ToDouble(item["MaxSpeed"]),
                            IsContainArms = Convert.ToBoolean(item["IsContainArms"]),
                            Imagesource = ImageInstance(new Uri(GetImagePath(folderPath) + item["ImageTitle"]))
                        });

                        aircraftList = aircraftList.OrderBy(a => a.SlNo).ToList();
                        int totalAircraftCount = aircraftList.Count();


                        listWindow.Show();
                        this.Hide();

                        listWindow.lstAircraftInfo.ItemsSource = aircraftList;
                        listWindow.txtTotalCount.Text = "Total: "+totalAircraftCount.ToString();
                        listWindow.txtTotalCount.IsEnabled = false;
                    }

                    listWindow.lstAircraftInfo.Items.Refresh();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        /* Exit */
      
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /* Upload image */

        private void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            UploadImage();
        }


        /*Method to upload image*/

        public void UploadImage()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png;";
            fd.Title = "Select an Image";
            if (fd.ShowDialog().Value == true)
            {
                ImageDisplay.Source = new BitmapImage(new Uri(fd.FileName));
                TempImageFile = new FileInfo(fd.FileName);
            }
        }

        private void RdbtnYes_Checked(object sender, RoutedEventArgs e)
        {


        }

        private void RdbtnNo_Checked(object sender, RoutedEventArgs e)
        {



        }

        /*Method to clear all textboxes and readiobutton*/

        public void AllClear()
        {
            txtSlno.Clear();
            txtAircraft.Clear();
            txtOrigin.Clear();
            txtRole.Clear();
            txtCrew.Clear();
            txtMaxSpeed.Clear();
            txtLength.Clear();
            rdbtnYes.IsChecked = false;
            rdbtnNo.IsChecked = false;
            txtEmptyWeight.Clear();
            datepickerFirstFlight.Text = "";
            cmbType.SelectedIndex = -1;
            cmbStatus.SelectedIndex = 0;
            txtSlno.IsEnabled = true;
        }
    }       
}
