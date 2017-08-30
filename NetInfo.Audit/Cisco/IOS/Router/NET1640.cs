using NetInfo.Devices.IOS;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must log all attempts to establish a management connection for administrative access.
    /// STIG ID:	NET1640     
    /// Rule ID:	SV-15455r2_rule
    /// Vuln ID:	V-3070       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET1640 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1640(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var acls = _device.StandardAccessLists
              .Where(c => new int[] { 105 }
                .Contains(c.Number))
                .SelectMany(c => c.RulesNoComments)
                .ToList();
            return acls.All(c => new Regex(@"^access-list\s+\d+.*log", RegexOptions.IgnoreCase).Match(c).Success);
        }
    }
}