using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must be password protected.
    /// STIG ID:	NET0230     
    /// Rule ID:	SV-3012r4_rule
    /// Vuln ID:	V-3012       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class NET0230 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0230(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !string.IsNullOrEmpty(_device.EnableSecret.Value);
        }
    }
}