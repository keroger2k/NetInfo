using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Port trunking must be disabled on all access ports (do not configure trunk on, desirable, non-negotiate, or auto--only off).
    /// STIG ID:	NET-VLAN-007     
    /// Rule ID:	SV-5623r2_rule
    /// Vuln ID:	V-5623       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NETVLAN007 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NETVLAN007(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var a = _device.Interfaces.Where(c => c.Physical && !c.Shutdown);
            var b = a.Where(c => c.SwitchPort.Type == IOSInterface.SwitchPortSettings.PortType.Access);
            return b.All(c => !c.SwitchPort.AllowedVlans.Any()) && b.All(c => c.SwitchPort.Encapsulation == null);
        }
    }
}