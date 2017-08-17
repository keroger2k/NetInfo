using NetInfo.Devices;
using NetInfo.Devices.IOS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetInfo.Audit.Cisco.IOS.Router;
using System.Reflection;
using NetInfo.Audit;

namespace NetInfo.Parse
{
    class Program
    {
        static void Main(string[] args)
        {

            Audit();

            //string path = @"C:\\cms-device-configurations";

            //DirectoryInfo Dir = new DirectoryInfo(path);
            //FileInfo[] FileList = Dir.GetFiles("*.*", SearchOption.AllDirectories);
            //Debug.WriteLine("Starting to process configurations....");
            //Debug.WriteLine("-------------------------------------------------------------------------------------------");
            //foreach (FileInfo FI in FileList)
            //{
            //    var asset = new AssetBlob();
            //    asset.Body = File.ReadAllText(FI.FullName);
            //    var device = new IOSDevice(asset);
            //    //Debug.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
            //    //    device.Hostname,
            //    //    device.Model,
            //    //    device.Image.ToString(),
            //    //    device.ShowVersion.ImageFileName,
            //    //    device.IsOSPFConfigured,
            //    //    device.IsBGPConfigured,
            //    //    device.IsEIGRPConfigured,
            //    //    device.EnableSecret.Value
            //    //    ));
               

            //    //Debug.WriteLine("\n-------------------------------------------------------------------------------------------");
            //    //Debug.WriteLine(string.Format("Device: {0}", device.Hostname));
            //    //Debug.WriteLine("-------------------------------------------------------------------------------------------\n");
            //    //Debug.WriteLine(string.Format("Device Type:{0}", device.Model));
            //    //Debug.WriteLine(string.Format("Device Image:{0}", device.Image.ToString()));
            //    //Debug.WriteLine(string.Format("Is OSPF Enabled: {0}", device.IsOSPFConfigured));
            //    //Debug.WriteLine(string.Format("Is BGP Enabled: {0}", device.IsBGPConfigured));
            //    //Debug.WriteLine(string.Format("Is EIGRP Enabled: {0}", device.IsEIGRPConfigured));
            //    //Debug.WriteLine("-------------------------------------------------------------------------------------------");


            //    //foreach (var t in device.ShowCdpNeighbors.Interfaces)
            //    //{
            //    //    Debug.WriteLine(string.Format("\tConnections On::{0}{1} -> Connected To:{2}::{3}{4}",
            //    //       t.SourceInterface, t.SourcePort.Trim(), t.DestinationHostname, t.DestinationInterface, t.DestinationPort));
            //    //}
            //}

            ////Console.ReadLine();

        }

        public static void NET1800(IOSDevice device)
        {
            var ir066_result = new NET1800(device);

            if (!ir066_result.Compliant())
                Debug.WriteLine(string.Format("Device: {0} failed for IR066", device.Hostname));

        }


        public static void Audit()
        {
            var instances = Assembly.GetAssembly(typeof(ICiscoRouterSecurityItem))
              .GetTypes()
              .Where(t => t.GetInterfaces().Contains(typeof(ICiscoRouterSecurityItem)))
              .OrderBy(c => c.Name);
            Debug.WriteLine(string.Format("Found {0} STIG to check.", instances.Count));

            foreach (var instance in instances)
            {
                CheckItem(instance);
            }
        }

        private static void CheckItem(Type t)
        {
            string path = @"C:\\cms-device-configurations";

            DirectoryInfo Dir = new DirectoryInfo(path);
            FileInfo[] FileList = Dir.GetFiles("*.*", SearchOption.AllDirectories);
            Debug.WriteLine("Starting to process configurations....");
            Debug.WriteLine("-------------------------------------------------------------------------------------------");
            foreach (FileInfo FI in FileList)
            {
                var asset = new AssetBlob();
                asset.Body = File.ReadAllText(FI.FullName);
                var device = new IOSDevice(asset);
                var query = (ICiscoRouterSecurityItem)Activator.CreateInstance(t, device);
                Debug.WriteLine(string.Format("Device: {0} is {1} for IR066", device.Hostname, query.Compliant()));
            }
        }
    }
}
