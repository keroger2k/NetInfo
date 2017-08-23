using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  A service or feature that calls home to the vendor must be disabled. 
    /// STIG ID:	NET0405    
    /// Rule ID:	SV-38003r2_rule
    /// Vuln ID:	V-28784       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0405 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0405(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.ServiceSettings.CallHome;
        }
    }
}