using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeScanning.ServiceModel
{
   public class Location
    {
        public double Latitide { get; set; }
        public double Longitude { get; set; }

        public double LocationAccuracy { get; set; }

        public double Altitude { get; set; }

        public double AltitudeAccuracy { get; set; }


    }
}
