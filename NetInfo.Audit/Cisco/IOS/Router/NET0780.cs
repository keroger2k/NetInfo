using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  IP Proxy ARP must be disabled on all external interfaces.
    /// STIG ID:	NET0780   
    /// Rule ID:	SV-3082r2_rule
    /// Vuln ID:	V-3082       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0780 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0780(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var enabledInterfaces = _device.Interfaces.Where(c => !c.Shutdown).ToList();
            var externalInterfaces = enabledInterfaces.Where(c => !_device.ShowCdpInterface.Interfaces.Select(d => d.Name).Contains(c.ShortName)).ToList();
            return externalInterfaces.All(c => !c.IP.ProxyArp);
        }
    }
}