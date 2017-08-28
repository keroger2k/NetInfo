using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.IOS;
using System;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  VLAN 1 must not be used for user VLANs
    /// STIG ID:	NET-VLAN-004     
    /// Rule ID:	SV-3971r2_rule
    /// Vuln ID:	V-3971       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NETVLAN004 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NETVLAN004(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var accessPorts = _device.ShowInterfaceStatus.Interfaces.Where(c =>
                                    (c.Status == ShowInterfaceStatus.InterfaceStatus.connected ||
                                        c.Status == ShowInterfaceStatus.InterfaceStatus.inactive ||
                                        c.Status == ShowInterfaceStatus.InterfaceStatus.notconnect ||
                                        c.Status == ShowInterfaceStatus.InterfaceStatus.notconnected) &&
                                        !c.Vlan.Equals("trunk", StringComparison.OrdinalIgnoreCase));

            return accessPorts.All(c => !c.Vlan.Equals("1")) && _device.Interfaces
              .Where(c => c.Type.Equals("Vlan", StringComparison.InvariantCultureIgnoreCase) && c.Vlan == 1)
              .All(c => c.Address == null && c.Shutdown);
        }
    }
}