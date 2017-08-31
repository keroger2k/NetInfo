using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.IOS;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Ensure ACL105 is properly configured according to the ACL template
    /// </summary>
    public class IR056 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;
        private const int APPROVED_ACL_NUMBER = 105;
        private const string APPROVED_ACL = @"access-list 105 remark Permit SSH For Network Management
access-list 105 permit tcp host 172.17.101.115 any eq 22 log
access-list 105 permit tcp host 172.16.1.115 any eq 22 log
access-list 105 permit tcp host 172.16.101.253 any eq 22 log
access-list 105 permit tcp host 172.16.202.101 any eq 22 log
access-list 105 permit tcp host 172.16.202.102 any eq 22 log
access-list 105 permit tcp host 172.16.202.103 any eq 22 log
access-list 105 permit tcp host 172.16.202.104 any eq 22 log
access-list 105 permit tcp host 172.16.202.105 any eq 22 log
access-list 105 permit tcp host 172.16.202.106 any eq 22 log
access-list 105 permit tcp host 172.16.202.108 any eq 22 log
access-list 105 permit tcp host 172.16.202.110 any eq 22 log
access-list 105 permit tcp host 172.16.202.254 any eq 22 log
access-list 105 permit tcp host 172.16.1.101 any eq 22 log
access-list 105 permit tcp host 172.16.1.102 any eq 22 log
access-list 105 permit tcp host 172.16.1.103 any eq 22 log
access-list 105 permit tcp host 172.16.1.104 any eq 22 log
access-list 105 permit tcp host 172.16.1.105 any eq 22 log
access-list 105 permit tcp host 172.16.1.106 any eq 22 log
access-list 105 permit tcp host 172.16.254.2 any eq 22 log
access-list 105 permit tcp host 172.17.202.112 any eq 22 log
access-list 105 permit tcp host 172.18.254.1 any eq 22 log
access-list 105 permit tcp host 172.16.17.101 any eq 22 log
access-list 105 permit tcp host 172.16.17.102 any eq 22 log
access-list 105 deny   ip any any log";


        public IR056(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            var _approvedAcl = new StandardAccessList();
            _approvedAcl.Rules = APPROVED_ACL.Split('\n');
            _approvedAcl.Rules = _approvedAcl.Rules.Select(c => c.Replace("\r", ""));

            var deviceAcl = _device.StandardAccessLists.FirstOrDefault(c => c.Number == APPROVED_ACL_NUMBER);
            if (deviceAcl == null) { return false; }
            var aclWithNOComments = deviceAcl.RulesNoComments.Select(c => c.Trim()).ToList();
            var accessListWithoutLastRule = aclWithNOComments.Take(aclWithNOComments.Count() - 1).OrderBy(c => c);
            var approvedSequence = _approvedAcl.RulesNoComments.Take(_approvedAcl.RulesNoComments.Count() - 1).Select(c => c.Trim()).OrderBy(c => c);
            var sequenceWithoutLastRule = new Regex(string.Format(@"access-list\s+{0}\s+deny\s+ip any any\s+log", APPROVED_ACL_NUMBER), RegexOptions.IgnoreCase).Match(aclWithNOComments.Last()).Success;
            var lastDenyAnyLine = approvedSequence.SequenceEqual(accessListWithoutLastRule);
            return lastDenyAnyLine && sequenceWithoutLastRule;
        }
    }
}