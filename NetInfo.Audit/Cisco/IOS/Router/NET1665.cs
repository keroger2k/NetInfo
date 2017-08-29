using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network device must not use the default or well-known SNMP community strings public and private.    /// STIG ID:	NET1665     
    /// Rule ID:	SV-3210r4_rule
    /// Vuln ID:	V-3210       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class NET1665 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1665(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            return _device.SNMPSettings.Communities.Count() == 0;
        }
    }
}