using BarCodeScanning.Contracts;
using BarCodeScanning.ServiceModel;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeScanning.Services
{
    public class GeoLocationProvider : IGeoLocationProvider
    {
        public async Task<Location> GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(timeout: new TimeSpan(100000));

            if (position != null)
                return new Location() { Altitude = position.Altitude, AltitudeAccuracy = position.AltitudeAccuracy, Latitide = position.Latitude, LocationAccuracy = position.Accuracy, Longitude = position.Longitude };
            else
                return null;
        }
    }
}
