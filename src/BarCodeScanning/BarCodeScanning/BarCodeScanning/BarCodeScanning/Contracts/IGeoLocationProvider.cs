using BarCodeScanning.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeScanning.Contracts
{
   public interface IGeoLocationProvider
    {
        Task<Location> GetCurrentLocation();
    }
}
