using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must be configured for a maximum number of unsuccessful SSH login attempts set at 3 before resetting the interface.
    /// STIG ID:	NET1646     
    /// Rule ID:	SV-15458r2_rule
    /// Vuln ID:	V-5613       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET1646 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1646(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.IPSettings.SSH.AuthenticationRetries == 2;
        }
    }
}