using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.IOS;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must time out access to the console port after 10 minutes or less of inactivity.
    /// STIG ID:	NET1624     
    /// Rule ID:	SV-15444r2_rule
    /// Vuln ID:	V-3967       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET1624 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1624(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var lines = _device.Lines;
            var consoles = lines.Where(c => c.Type == LineType.CONSOLE);

            foreach (var line in consoles)
            {
                foreach (var command in line.Commands)
                {
                    var execTimeoutRegex = new Regex(@"^ exec-timeout (?<timeout>\d+) (\d+)$", RegexOptions.IgnoreCase);
                    var result = execTimeoutRegex.Match(command);

                    if (result.Success)
                    {
                        var to = int.Parse(result.Groups["timeout"].Value);
                        return to <= 10 && to != 0;
                    }

                }
            }

            return false;
        }
    }
}