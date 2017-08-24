using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must have identification support disabled.
    /// STIG ID:	NET0726     
    /// Rule ID:	SV-5616r3_rule
    /// Vuln ID:	V-5616       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET0726 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0726(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.IPSettings.Identd;
        }
    }
}