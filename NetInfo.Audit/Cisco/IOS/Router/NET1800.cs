using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NetInfo.Devices.IOS;
using System;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: The network device must log all access control lists (ACL) deny statements.    /// STIG ID:	NET1020     
    /// Rule ID:	SV-15474r3_rule
    /// Vuln ID:	V-3000       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET1800 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;

        public NET1800(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var acls = _device.StandardAccessLists
                .SelectMany(c => c.RulesNoComments)
                .ToList();
            return acls
              .Where(c => new Regex(@"^access-list\s+\d+\s+deny", RegexOptions.IgnoreCase).Match(c).Success)
              .All(c => new Regex(@"^access-list\s+\d+\s+deny.*log", RegexOptions.IgnoreCase).Match(c).Success);
        }
    }
}