using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.IOS;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:   The network element must only allow management connections for administrative access from hosts residing in to the management network.
    /// STIG ID:	NET1637     
    /// Rule ID:	SV-15449r3_rule
    /// Vuln ID:	V-5611       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET1637 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1637(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var lines = _device.Lines;
            var vtys = lines.Where(c => c.Type == LineType.VTY);
            var accessClassRegex = new Regex(@"\s*access-class (\d+) in$", RegexOptions.IgnoreCase);

            foreach (var line in vtys)
            {
                if (!line.Commands.Any(c => accessClassRegex.Match(c).Success))
                {
                    return false;
                }
                var l = line.Commands.SingleOrDefault(c => accessClassRegex.Match(c).Success);
                return l != null;
            }

            return true;
        }
    }
}