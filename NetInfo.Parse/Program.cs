using NetInfo.Audit;
using NetInfo.Devices;
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

            Audit();
        }

        public static void Audit()
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

        private static void CheckItem(Type t)
        {
            List<Tuple<string, string>> results = new List<Tuple<string, string>>();
            Debug.WriteLine(string.Format("\nProcessing configurations for STIG {0}",t.ToString()));
            foreach (var asset in _assets)
            {
                var device = new IOSDevice(asset);
                ICiscoRouterSecurityItem query = (ICiscoRouterSecurityItem)Activator.CreateInstance(t, device);
                results.Add(new Tuple<string, string>(query.Compliant() ? "PASSING" : "FAILING", device.Hostname));
               // if(!query.Compliant())
               //     Debug.WriteLine(string.Format("Device: {0} is {1}", device.Hostname, query.Compliant() ? "PASSING" : "FAILING"));
            }

            Debug.WriteLine(string.Format("Finished Processing.\n\tPassing: {0}\n\tFailing: {1}", 
                results.Count(c => c.Item1.Equals("PASSING", StringComparison.CurrentCultureIgnoreCase)),
                results.Count(c => c.Item1.Equals("FAILING", StringComparison.CurrentCultureIgnoreCase))
             ));

            foreach(var device in results.Where(c => c.Item1.Equals("FAILING", StringComparison.CurrentCultureIgnoreCase)).Take(5))
            {
                Debug.WriteLine(string.Format("Failing Device: {0}", device.Item2));
            }

            foreach (var device in results.Where(c => c.Item1.Equals("PASSING", StringComparison.CurrentCultureIgnoreCase)).Take(5))
            {
                Debug.WriteLine(string.Format("Passing Device: {0}", device.Item2));
            }


        }
    }
}
