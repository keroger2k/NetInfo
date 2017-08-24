using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must be password protected.
    /// STIG ID:	NET0728     
    /// Rule ID:	SV-5617r2_rule
    /// Vuln ID:	V-5617       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0728 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0728(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.Dhcp && !_device.ServiceSettings.Dhcp;
        }
    }
}