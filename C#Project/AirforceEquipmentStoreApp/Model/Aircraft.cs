using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AirforceEquipmentStoreApp.Model
{
    public class Aircraft
    {
        public ImageSource Imagesource { get; set; }
        public string ImageTitle { get; set; }
        public int SlNo { get; set; }
        public string AircraftName { get; set; }
        public string Origin { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime FirstFlight { get; set; }
        public string Role { get; set; }
        public int Crew { get; set; }
        public double Length { get; set; }
        public double EmptyWeight { get; set; }
        public double MaxSpeed { get; set; }
        public bool IsContainArms { get; set; }
    }
}
