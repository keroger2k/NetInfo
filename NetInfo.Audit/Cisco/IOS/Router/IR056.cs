﻿using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure ACL99 is properly configured according to the ACL template
  /// </summary>
  public class IR056 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly StandardAccessList _approvedAcl;

    public IR056(INMCIIOSDevice device, StandardAccessList approvedAcl) {
      this.Device = device;
      this._approvedAcl = approvedAcl;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var deviceAcl = device.StandardAccessLists.FirstOrDefault(c => c.Number == 99);
      if (deviceAcl == null) { return false; }
      var aclWithNOComments = deviceAcl.RulesNoComments.Select(c => c.Trim()).ToList();
      var accessListWithoutLastRule = aclWithNOComments.Take(aclWithNOComments.Count() - 1).OrderBy(c => c);
      var approvedSequence = _approvedAcl.RulesNoComments.Take(_approvedAcl.RulesNoComments.Count() - 1).Select(c => c.Trim()).OrderBy(c => c);
      var sequenceWithoutLastRule = new Regex(@"access-list\s+99\s+deny\s+any\s+log", RegexOptions.IgnoreCase).Match(aclWithNOComments.Last()).Success;
      var lastDenyAnyLine = approvedSequence.SequenceEqual(accessListWithoutLastRule);
      return lastDenyAnyLine && sequenceWithoutLastRule;
    }
  }
}