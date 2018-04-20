using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace CreateDeviceId
{
    class Program
    {
        static RegistryManager registryManager;
        static string hvuIotHubConnectionString = @"HostName=hvuConnectTheDots.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=8puc3+vC0/N5gpkPy5GpecoeguL6+DnCzp/6SI5Fj84=";
        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(hvuIotHubConnectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }
        private static async Task AddDeviceAsync()
        {
            string deviceId = "hvuConnectTheDotsFirstDevice";
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
