using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOTDeviceController
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "{IOT Hub admin connection string}";
            string device = "redmiy2";
            ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            string input = Console.ReadLine();
            while (input != "exit")
                {

                var serviceMessage = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes(input));
                serviceMessage.Ack = DeliveryAcknowledgement.Full;
                serviceMessage.MessageId = Guid.NewGuid().ToString();
                serviceClient.SendAsync(device, serviceMessage).GetAwaiter().GetResult();
                Console.WriteLine(String.Format("Sent to Device ID: [{0}], Message:\"{1}\", message Id: {2}\n", device, input, serviceMessage.MessageId));
                input = Console.ReadLine();
            }
            serviceClient.CloseAsync().GetAwaiter().GetResult();
        }
    }
}
