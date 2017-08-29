using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Disabled switch ports must be placed in an unused VLAN (do not use VLAN1).
    /// STIG ID:	NET-VLAN-002     
    /// Rule ID:	SV-3973r2_rule
    /// Vuln ID:	V-3973       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NETVLAN002 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NETVLAN002(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var interfaces = _device.Interfaces;
            return !interfaces.Any(c => c.Physical && c.Shutdown && c.Vlan == 1);
        }
    }
}