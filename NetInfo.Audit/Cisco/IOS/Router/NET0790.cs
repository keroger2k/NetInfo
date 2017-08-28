using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  IP directed broadcast must be disabled on all layer 3 interfaces.
    /// STIG ID:	NET0790   
    /// Rule ID:	SV-3083r3_rule
    /// Vuln ID:	V-3083       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0790 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0790(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.Interfaces
              .Where(c => !c.Shutdown && c.Address != null)
              .All(c => !c.IP.DirectedBroadcast);
        }
    }
}