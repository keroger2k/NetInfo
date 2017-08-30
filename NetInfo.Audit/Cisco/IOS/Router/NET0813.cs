using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must authenticate all NTP messages received from NTP servers and peers.
    /// STIG ID:	NET0813     
    /// Rule ID:	SV-16089r3_rule
    /// Vuln ID:	V-14671       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0813 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0813(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.NetworkTimeProtocol.Keys.Any() && _device.NetworkTimeProtocol.TrustedKeys.Any() && _device.NetworkTimeProtocol.Authenticate;
        }
    }
}