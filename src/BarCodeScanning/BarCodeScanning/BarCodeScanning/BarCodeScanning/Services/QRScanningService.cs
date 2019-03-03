using BarCodeScanning.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;


namespace BarCodeScanning.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions() { };

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
                //UseCustomOverlay = true
            };
            //if(!scanner.IsTorchOn)
            scanner.ToggleTorch();

            var scanResult = await scanner.Scan(optionsCustom);
            //if (scanner.IsTorchOn)
            //    scanner.ToggleTorch();
            return scanResult!=null?scanResult.Text:"";
        }
    }
}
