using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must not use SSH Version 1 for administrative access.
    /// STIG ID:	NET1647     
    /// Rule ID:	SV-15460r2_rule
    /// Vuln ID:	V-14717       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET1647 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1647(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.IPSettings.SSH.Version == 2;
        }
    }
}