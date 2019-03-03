using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Plugin.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarCodeScanning.Droid
{
    [Activity(Label = "BarCodeScanning", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            ZXing.Mobile.MobileBarcodeScanner.Initialize(this.Application);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            var requiredPermissions = new List<string>();
            if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this.ApplicationContext, Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted)
            {
                requiredPermissions.Add(Manifest.Permission.AccessFineLocation);
            }
            if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this.ApplicationContext, Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted)
            {
                requiredPermissions.Add(Manifest.Permission.AccessCoarseLocation);
            }
            if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this.ApplicationContext, Manifest.Permission.Camera) != Android.Content.PM.Permission.Granted)
            {
                requiredPermissions.Add(Manifest.Permission.Camera);
           }
            if(requiredPermissions.Count!=0)
            Android.Support.V4.App.ActivityCompatApi23.RequestPermissions(this, requiredPermissions.ToArray(), 3);

            Task.Run(async () => await IOTConnector.OnMessage(message => RunOnUiThread(() =>
            ExecuteMethod(message)
           )));
    }


        void ExecuteMethod(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            System.Diagnostics.Debug.WriteLine("OnRequestPermissionsResult"+requestCode + " " + String.Join(",", permissions) + " " + string.Join(",", grantResults));
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

