using Android;
using Android.Content.PM;
using BarCodeScanning.ServiceModel;
using BarCodeScanning.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarCodeScanning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBarcode : ContentPage
    {
        public CreateBarcode()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
         
            GeoLocationProvider location = new GeoLocationProvider();
         Location loc=  await location.GetCurrentLocation();
            lblLocation.Text = "Latitude:"+loc.Latitide+" Longitude:"+loc.Longitude+" Altitude:"+loc.Altitude+" Accuracy location,altitude:"+loc.LocationAccuracy+","+loc.AltitudeAccuracy;
        }
    }
}