using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network devices must require authentication prior to establishing a management connection for administrative access.
    /// STIG ID:	NET1636     
    /// Rule ID:	SV-15448r3_rule
    /// Vuln ID:	V-3175       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class NET1636 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1636(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.AAA.Authentication.LoginGroupTacacsEnable && _device.AAA.Authentication.EnableGroupTacacsEnable;
        }
    }
}