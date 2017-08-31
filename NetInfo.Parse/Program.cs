using NetInfo.Audit;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.IOS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NetInfo.Parse
{
    class Program
    {
        private static List<AssetBlob> _assets;

        static void Main(string[] args)
        {
            _assets = new List<AssetBlob>();
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

            //Audit();
            Audit("IR056");
            //GenerateScripts();
        }

        private static void Audit()
        {
            Debug.WriteLine("Checking for available STIGs to run...");
            var instances = Assembly.GetAssembly(typeof(ICiscoRouterSecurityItem))
              .GetTypes()
              .Where(t => t.GetInterfaces().Contains(typeof(ICiscoRouterSecurityItem)))
              .OrderBy(c => c.Name);
            Debug.WriteLine("Finished.  Found {0} STIG to check...", instances.Count());
            foreach (var instance in instances)
            {
                CheckItem(instance);
            }
        }

        private static void Audit(string check)
        {
            Debug.WriteLine("Checking for available STIGs to run...");
            var instance = Assembly.GetAssembly(typeof(ICiscoRouterSecurityItem))
              .GetTypes()
              .Where(t => t.GetInterfaces().Contains(typeof(ICiscoRouterSecurityItem)))
              .FirstOrDefault(c => c.ToString().Contains(check));
            Debug.WriteLine("Finished.  Found {0} STIG to check...", check);
            CheckItem(instance);
        }

        private static void CheckItem(Type t)
        {
            List<Tuple<string, string>> results = new List<Tuple<string, string>>();
            //Debug.WriteLine(string.Format("\nProcessing configurations for STIG {0}",t.ToString()));
            foreach (var asset in _assets)
            {
                var device = new IOSDevice(asset);
                ICiscoRouterSecurityItem query = (ICiscoRouterSecurityItem)Activator.CreateInstance(t, device);
                results.Add(new Tuple<string, string>(query.Compliant() ? "PASSING" : "FAILING", device.Hostname));
                // if(!query.Compliant())
                //     Debug.WriteLine(string.Format("Device: {0} is {1}", device.Hostname, query.Compliant() ? "PASSING" : "FAILING"));
            }

            //Debug.WriteLine(string.Format("{0},{1},{2}",
            //    t.ToString(),
            //    results.Count(c => c.Item1.Equals("PASSING", StringComparison.CurrentCultureIgnoreCase)),
            //    results.Count(c => c.Item1.Equals("FAILING", StringComparison.CurrentCultureIgnoreCase))
            // ));

            foreach (var device in results)
            {
                Debug.WriteLine(string.Format("{0}, {1}, {2}", t.ToString(), device.Item1, device.Item2));
            }

            //foreach (var device in results.Where(c => c.Item1.Equals("PASSING", StringComparison.CurrentCultureIgnoreCase)).Take(5))
            //{
            //    Debug.WriteLine(string.Format("Passing Device: {0}", device.Item2));
            //}
        }

        private static void GenerateScripts()
        {
            foreach (var asset in _assets)
            {
                OSPFCostScript.GenerateScript(new IOSDevice(asset));
            }
        }
    }

    public class OSPFCostScript
    {
        public static void GenerateScript(IIOSDevice device)
        {
            var configuredInterfaces = new List<IOSInterface>();

            foreach (var link in device.Interfaces)
            {
                if (link.IP.OSPF != null && link.IP.OSPF.Cost != 0)
                {
                    configuredInterfaces.Add(link);
                }
            }

            if (configuredInterfaces.Any())
            {
                Debug.WriteLine("--------------------------------------------------");
                Debug.WriteLine(string.Format("{0}", device.Hostname));
                Debug.WriteLine("--------------------------------------------------");
                Debug.WriteLine("!\n!\n!\n!");
                Debug.WriteLine("config t");
                Debug.WriteLine("!\n!\n!\n!");

                foreach (var i in configuredInterfaces)
                {
                    Debug.WriteLine(string.Format("interface {0}", i.ShortName));
                    Debug.WriteLine(string.Format(" no ip ospf cost {0}", i.IP.OSPF.Cost));
                    Debug.WriteLine(" no ip ospf cost");
                    Debug.WriteLine("!");
                }

                Debug.WriteLine(string.Format("router ospf {0}", device.OSPF.ProcessId));
                Debug.WriteLine(string.Format("auto-cost reference-bandwidth 1000"));
                Debug.WriteLine("!");

                Debug.WriteLine("end");
                Debug.WriteLine("!\n!\n!\n!");
                Debug.WriteLine("write mem");
                Debug.WriteLine("!\n!\n!\n!");
            }
        }
    }
}
