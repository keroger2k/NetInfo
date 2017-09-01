using NetInfo.Audit;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NetInfo.Parse
{
    public class AuditEngine
    {
        private IEnumerable<AssetBlob> _assets;

        public AuditEngine(IEnumerable<AssetBlob> assets)
        {
            this._assets = assets;
        }

        public void AuditAll()
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

        public void AuditItem(string check)
        {
            Debug.WriteLine("Checking for available STIGs to run...");
            var instance = Assembly.GetAssembly(typeof(ICiscoRouterSecurityItem))
              .GetTypes()
              .Where(t => t.GetInterfaces().Contains(typeof(ICiscoRouterSecurityItem)))
              .FirstOrDefault(c => c.ToString().Contains(check));
            Debug.WriteLine("Finished.  Found {0} STIG to check...", check);
            CheckItem(instance);
        }


        private void CheckItem(Type t)
        {
            var results = new List<Tuple<string, string>>();

            foreach (var asset in _assets)
            {
                var device = new IOSDevice(asset);
                var query = (ICiscoRouterSecurityItem)Activator.CreateInstance(t, device);
                results.Add(new Tuple<string, string>(query.Compliant() ? "PASSING" : "FAILING", device.Hostname));
            }

            foreach (var device in results)
            {
                Debug.WriteLine(string.Format("{0}, {1}, {2}", t.ToString(), device.Item1, device.Item2));
            }

        }
    }
}
