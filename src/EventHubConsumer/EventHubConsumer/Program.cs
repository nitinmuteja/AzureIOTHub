using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Common;
namespace EventHubConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //EventHubClient client=new EventHubClient
            CancellationToken tk = new CancellationToken();
          MonitorEventHubAsync(DateTime.UtcNow, tk, "$Default").GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task MonitorEventHubAsync(DateTime startTime, CancellationToken ct, string consumerGroupName)
        {
            EventHubClient eventHubClient = null;
            EventHubReceiver eventHubReceiver = null;
            string connectionstring = "{IOT Hub admin connection string}";
                try
            {
                string selectedDevice = "redmiy2";
                eventHubClient = EventHubClient.CreateFromConnectionString(connectionstring, "messages/events");
                Console.WriteLine("Receiving events...\r\n");
               int  eventHubPartitionsCount = eventHubClient.GetRuntimeInformation().PartitionCount;
                string partition = EventHubPartitionKeyResolver.ResolveToPartition(selectedDevice, eventHubPartitionsCount);
                eventHubReceiver = eventHubClient.GetConsumerGroup(consumerGroupName).CreateReceiver(partition, startTime);

                while (true)
                {
                    ct.ThrowIfCancellationRequested();

                    EventData eventData = await eventHubReceiver.ReceiveAsync(TimeSpan.FromSeconds(1));
                    if (eventData != null)
                    {
                        string data = Encoding.UTF8.GetString(eventData.GetBytes());
                        DateTime enqueuedTime = eventData.EnqueuedTimeUtc.ToLocalTime();

                        // Display only data from the selected device; otherwise, skip.
                        string connectionDeviceId = eventData.SystemProperties["iothub-connection-device-id"].ToString();

                        if (string.CompareOrdinal(selectedDevice, connectionDeviceId) == 0)
                        {
                            Console.WriteLine(String.Format("{0}> Device: [{1}], Data:[{2}]", enqueuedTime, connectionDeviceId, data));

                            if (eventData.Properties.Count > 0)
                            {
                                Console.WriteLine("Properties:");
                                foreach (var property in eventData.Properties)
                                {
                                    Console.WriteLine(String.Format("'{0}': '{1}'", property.Key, property.Value));
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (ct.IsCancellationRequested)
                {
                    Console.WriteLine(String.Format("Stopped Monitoring events. {0}\r\n", ex.Message));
                }
                else
                {

                    Console.WriteLine(String.Format("Stopped Monitoring events. {0}\r\n", ex.Message));
                }
                if (eventHubReceiver != null)
                {
                    eventHubReceiver.Close();
                }
                if (eventHubClient != null)
                {
                    eventHubClient.Close();
                }
            }
        }


    }
}
