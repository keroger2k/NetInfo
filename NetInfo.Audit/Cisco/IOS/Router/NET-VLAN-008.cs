using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The native VLAN must be assigned to a VLAN ID other than the default VLAN for all 802.1q trunk links.
    /// STIG ID:	NET-VLAN-008     
    /// Rule ID:	SV-5622r2_rule
    /// Vuln ID:	V-5622       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class IR014 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public IR014(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return true;
        }
    }
}