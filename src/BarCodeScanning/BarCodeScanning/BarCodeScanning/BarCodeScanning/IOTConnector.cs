using Java.IO;
using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeScanning
{
   public  class IOTConnector
    {
        private static readonly string _connectionString= "{IOT Hub device connection string}";
        private static readonly DeviceClient _iotClient;
       
        static IOTConnector()
        {
           
                _iotClient = DeviceClient.CreateFromConnectionString(_connectionString, TransportType.Http1);
                _iotClient.OpenAsync();
        }

        public static async Task OnMessage(Expression<Action<string>> exprFunc)
        {
               var funct= exprFunc.Compile();
                while (true)
                {
                try
                {
                    Message msg = await _iotClient.ReceiveAsync();
                    if (msg != null)
                    {
                        byte[] bytes = msg.GetBytes();
                        string content = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                        await _iotClient.CompleteAsync(msg);
                        funct.Invoke(content);
                       
                    }
                }
                    catch(Exception ex)
                    {
                    Debug.WriteLine(ex.ToString());
                }
                }
            }


          
       

        public  static void SendMessage(string eventData,string eventType)
        {
                var message = new Message(Encoding.UTF8.GetBytes(eventData));
                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("EventType", eventType);
                var ts = Task.Run(async () =>
                 {
                     try
                     {
                         await _iotClient.SendEventAsync(message);
                     }
                     catch(Exception ex)
                     {
                         Debug.WriteLine(ex.ToString());
                     }
                 }
                );
            }
        }


    }

