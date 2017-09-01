using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.IOS;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetInfo.Parse
{
    public class OSPFCostScript
    {
        private IEnumerable<AssetBlob> _assets;

        public OSPFCostScript(IEnumerable<AssetBlob> assets)
        {
            this._assets = assets;
        }

        public void GenerateScripts()
        {
            foreach (var asset in _assets)
            {
                var device = new IOSDevice(asset);
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
}
