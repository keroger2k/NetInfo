using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Management connections to a network device must be established using secure protocols with FIPS 140-2 validated cryptographic modules.
    /// STIG ID:	NET1638     
    /// Rule ID:	SV-15451r4_rule
    /// Vuln ID:	V-3069       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET1638 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1638(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var line1 = _device.Lines.SingleOrDefault(c => c.Type == LineType.VTY &&
              c.Name.Equals("line vty 5 15", System.StringComparison.OrdinalIgnoreCase));
            var result1 = (line1 != null) ? line1.Commands.Any(c => c.Trim().Equals("transport input none")) : true;

            var line2 = _device.Lines.SingleOrDefault(c => c.Type == LineType.VTY &&
              c.Name.Equals("line vty 0 4", System.StringComparison.OrdinalIgnoreCase));
            var result2 =  (line2 != null) ? line2.Commands.Any(c => c.Trim().Equals("transport input ssh")) : true;

            return result1 && result2;
        }
    }
}