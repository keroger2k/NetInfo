using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must have the PAD service disabled.
    /// STIG ID:	NET0722     
    /// Rule ID:	SV-5614r3_rule
    /// Vuln ID:	V-5614       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0722 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0722(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.ServiceSettings.Pad;
        }
    }
}