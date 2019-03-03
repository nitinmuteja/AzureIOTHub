using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BarCodeScanning
{
  public static class CloudMessageProcessor
    {

   public  static void ProcessMessage(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
     
    }
}
