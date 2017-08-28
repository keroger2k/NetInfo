using NetInfo.Devices.IOS;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{
    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network device must log all access control lists (ACL) deny statements.
    /// STIG ID:	NET1020 
    /// Rule ID:	SV-15474r3_rule
    /// Vuln ID:	V-3000       
    /// Severity:	CAT III  Class:	Unclass
    /// </summary>
    public class NET1020 : ICiscoRouterSecurityItem
    {
        private IIOSDevice _device;

        public NET1020(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var standardAcls = _device.StandardAccessLists.ToList();
            var extendedAcls = _device.ExtendedAccessLists.ToList();
            var appliedAccessGroups = _device.Interfaces.SelectMany(c => c.AccessGroups).Select(c => c.Name);
            var routeMapAcls = _device.RouteMaps.SelectMany(c => c.GetMatchStandardAccessLists()).ToList();
            //removing extended access groups that are not applied to an interface.
            extendedAcls = extendedAcls.Where(c => appliedAccessGroups.Contains(c.Name)).ToList();
            standardAcls = standardAcls.Where(c => !routeMapAcls.Contains(c.Number)).ToList();

            bool standardResult = false;
            bool extendedResult = false;

            if (standardAcls.Any())
            {
                standardResult = standardAcls
                  .Where(c => !new Regex(@"access-list\s+\d+\s+permit\s+ip\s+any", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success)
                  .All(c => new Regex(@"access-list\s+\d+\s+deny\s+any\s+log", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success);
            }
            else
            {
                standardResult = true;
            }

            if (extendedAcls.Any())
            {
                extendedResult = extendedAcls
                  .Where(c => !new Regex(@"permit\s+ip\s+any\s+any", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success)
                  .All(c => new Regex(@"deny\s+ip\s+any\s+any\s+log", RegexOptions.IgnoreCase).Match(c.RulesNoComments.Last()).Success);
            }
            else
            {
                extendedResult = true;
            }

            return standardResult && extendedResult;
        }
    }
}