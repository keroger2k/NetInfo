using NetInfo.Devices;
using System.Linq;
using NetInfo.Devices.IOS;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetInfo.Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            var _assets = new List<AssetBlob>();
            string path = @"C:\\cms-device-configurations";

            DirectoryInfo Dir = new DirectoryInfo(path);
            FileInfo[] FileList = Dir.GetFiles("*.*", SearchOption.AllDirectories);
            Debug.WriteLine("Reading device configurations into memory....");
            foreach (FileInfo FI in FileList)
            {
                var asset = new AssetBlob();
                asset.Body = File.ReadAllText(FI.FullName);
                _assets.Add(asset);
            }
            Debug.WriteLine("Finished.  Found {0} device(s) configurations...", _assets.Count);
            var auditEngine = new AuditEngine(_assets);
            var scriptGen = new OSPFCostScript(_assets);

            //auditEngineAuditAll();
            //auditEngine.AuditItem("IR056");

            //scriptGen.CheckOSPFInterfaces();
            //scriptGen.CheckInterfacesIP();

            foreach(var asset in _assets)
            {
                var device = new IOSDevice(asset);
                if(device.ShowIpRoute.RouteTableNetorks.Any())
                {
                    foreach(var route in device.ShowIpRoute.RouteTableNetorks)
                    {
                        Debug.WriteLine(string.Format(@"Device: {0}, Route: {1}/{2}",
                            device.Hostname,
                            route.Network,
                            route.CIDRMask));
                    }

                }

                //Debug.WriteLine(string.Format(@"Device: {0}, Gateway of last resort is not set",
                //        device.Hostname));
            }

            //scriptGen.GenerateScripts();
        }
    }
}
