using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must have DNS servers defined if it is configured as a client resolver.
    /// STIG ID:	NET0820     
    /// Rule ID:	SV-15330r2_rule
    /// Vuln ID:	V-3020
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0820 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;
        public NET0820(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.DomainLookup;
        }
    }
}