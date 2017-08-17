﻿using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure ACL67 is properly configured according to the ACL template
  /// </summary>
  public class IS135 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedAcl;

    public IS135(INMCIIOSDevice device, IEnumerable<string> approvedAcl) {
      this.Device = device;
      this._approvedAcl = approvedAcl;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      IEnumerable<string> acl67 = device.StandardAccessLists.FirstOrDefault(c => c.Number == 67).Rules.Select(c => c.Trim());
      return _approvedAcl.SequenceEqual(acl67);
    }
  }
}