using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  VLAN 1 must be pruned from all trunk and access ports that do not require it.
    /// STIG ID:	NET-VLAN-005     
    /// Rule ID:	SV-3972r2_rule
    /// Vuln ID:	V-3972       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NETVLAN005 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NETVLAN005(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var a = _device.Interfaces.Where(c => c.Physical);
            var b = a.Where(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Trunk);
            var d = !b.Any() || b.Any(c => !c.SwitchPort.AllowedVlans.Contains(1));
            var e = !_device.Interfaces.Any(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access && c.Vlan == 1);
            return d && e;
        }
    }
}