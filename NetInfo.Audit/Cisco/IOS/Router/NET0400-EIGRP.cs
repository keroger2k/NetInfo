using NetInfo.Devices.Infrastructure.ExtensionMethods;
using NetInfo.Devices.IOS;
using System;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: The network element must authenticate all IGP peers.
    /// STIG ID:	NET0400  
    /// Rule ID:	SV-15290r2_rule
    /// Vuln ID:	V-3034       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0400EIGRP : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0400EIGRP(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var eigrpInterface = _device.GetCoveredInterfaces().Where(c => !c.Shutdown);
            return eigrpInterface.All(c => c.IP.EIGRP.Mode.Equals("md5", StringComparison.OrdinalIgnoreCase));
        }
    }
}