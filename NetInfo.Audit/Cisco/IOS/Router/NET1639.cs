using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must timeout management connections for administrative access after 10 minutes or less of inactivity.
    /// STIG ID:	NET1639    
    /// Rule ID:	SV-15453r2_rule
    /// Vuln ID:	V-3014      
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET1639 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET1639(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var lines = _device.Lines;
            var vtys = lines.Where(c => c.Type == LineType.VTY);

            foreach (var line in vtys)
            {
                foreach(var command in line.Commands)
                {
                    var execTimeoutRegex = new Regex(@"^ exec-timeout (?<timeout>\d+) (\d+)$", RegexOptions.IgnoreCase);
                    var result = execTimeoutRegex.Match(command);

                    if(result.Success)
                    {
                        var to = int.Parse(result.Groups["timeout"].Value);
                        return to <= 10;
                    }

                }
            }

            return false;
        }
    }
}