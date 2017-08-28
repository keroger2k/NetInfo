using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  Network devices must be password protected.
    /// STIG ID:	NET0230     
    /// Rule ID:	SV-3012r4_rule
    /// Vuln ID:	V-3012       
    /// Severity:	CAT I  Class:	Unclass
    /// </summary>
    public class IR058 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public IR058(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var line = _device.Lines.SingleOrDefault(c => c.Type == LineType.VTY &&
              c.Name.Equals("line vty 0 4", System.StringComparison.OrdinalIgnoreCase));
            return (line != null) ? line.Commands.Any(c => c.Trim().Equals("transport input ssh")) : true;
        }
    }
}