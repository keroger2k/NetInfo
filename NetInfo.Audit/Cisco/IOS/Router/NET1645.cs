using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must be configured to timeout after 60 seconds or less for incomplete or broken SSH sessions.
    /// STIG ID:	NET1645     
    /// Rule ID:	SV-15457r2_rule
    /// Vuln ID:	V-5612       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET1645 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1645(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.IPSettings.SSH.Timeout == 30;
        }
    }
}