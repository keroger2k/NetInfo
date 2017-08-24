using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must use at least two NTP servers to synchronize time.
    /// STIG ID:	NET0812     
    /// Rule ID:	SV-28651r4_rule
    /// Vuln ID:	V-23747       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0812 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0812(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.NetworkTimeProtocol.Servers.Count() > 1;
        }
    }
}