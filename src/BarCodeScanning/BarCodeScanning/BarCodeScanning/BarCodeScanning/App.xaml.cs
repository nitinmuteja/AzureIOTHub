using BarCodeScanning.ServiceModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BarCodeScanning
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MasterMenu();
        }

        protected override void OnStart()
        {
           IOTConnector.SendMessage(JsonConvert.SerializeObject(new EventMessage() { Info = "Application Loaded" }), "Load");
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            IOTConnector.SendMessage(JsonConvert.SerializeObject(new EventMessage() { Info = "Application slept" }), "Sleep");
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
              IOTConnector.SendMessage(JsonConvert.SerializeObject(new EventMessage() { Info = "Application resumed" }), "Resume");
            // Handle when your app resumes
        }
    }
}
