using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must be configured to ensure passwords are not viewable when displaying configuration information.
    /// STIG ID:	NET0600    
    /// Rule ID:	SV-3062r4_rule
    /// Vuln ID:	V-3062       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class NET0600 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET0600(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.ServiceSettings.PasswordEncryption;
        }
    }
}