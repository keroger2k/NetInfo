using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Cisco Express Forwarding (CEF) must be enabled on all supported Cisco Layer 3 IP devices.
    /// STIG ID:	NET0949     
    /// Rule ID:	SV-5645r4_rule
    /// Vuln ID:	V-5645       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0949 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0949(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.ShowIpInterface.Interfaces
              .Where(c => c.InternetAddress != null)
              .Where(c => !c.NetworkAddressTranslationEnabled)
              .All(c => c.IpCefSwitchingEanbled);
        }
    }
}