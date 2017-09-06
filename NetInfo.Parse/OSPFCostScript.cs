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

        public void CheckInterfacesIP()
        {
            foreach (var asset in _assets)
            {
                var device = new IOSDevice(asset);

                foreach(var tface in device.Interfaces)
                {
                    if(tface.Address != null)
                    {
                        Debug.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"", device.Hostname, tface.Address.NetworkAddress, tface.Address.NetworkMask,tface.Description, tface.ShortName, tface.Shutdown));
                    }
                }

            }
        }

        public void CheckOSPFInterfaces()
        {
            var areaList = new HashSet<string>();

            foreach (var asset in _assets)
            {
                var device = new IOSDevice(asset);
                var areas = new List<string>();

                if(device.OSPF.ProcessId != -1)
                {
                    foreach(var inter in device.Interfaces)
                    {
                        if (!string.IsNullOrEmpty(inter.IP.OSPF.Area))
                        {
                            areaList.Add(inter.IP.OSPF.Area);
                            Debug.WriteLine(string.Format("{0}: Interface {1} configured with OSPF Area {2}", 
                                device.Hostname,
                                inter.ShortName,
                                inter.IP.OSPF.Area
                                ));

                        }
                    }
                    foreach(var network in device.OSPF.Networks)
                    {
                        areaList.Add(network.Area);
                        //Debug.WriteLine(string.Format("{0},{1},{2}", network.Network, network.InverseMask, network.Area));
                    }
                }

            }

            //foreach(var area in areaList)
            //{
            //    Debug.WriteLine(string.Format("{0}", area));
            //}
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
