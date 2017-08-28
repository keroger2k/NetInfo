using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network device must require authentication for console access.
    /// STIG ID:	NET1623  
    /// Rule ID:	SV-19270r3_rule
    /// Vuln ID:	V-4582       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class NET1623 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1623(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.AAA.Authentication.LoginGroupTacacsEnable && _device.AAA.Authentication.EnableGroupTacacsEnable;
        }
    }
}