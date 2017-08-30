using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The Configuration auto-loading feature must be disabled.
    /// STIG ID:	NET0760     
    /// Rule ID:	SV-3080r3_rule
    /// Vuln ID:	V-3080       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0760 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET0760(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return !_device.BootNetwork && !_device.ServiceConfig;
        }
    }
}