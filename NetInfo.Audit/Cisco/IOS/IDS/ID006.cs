using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate this command exists: "standard-time-zone-name UTC"
  /// </summary>
  public class ID006 : ISTIGItem {

    public IDevice Device { get; private set; }

    public ID006(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var line = ((INMCIIDSDevice)Device).Timezone;
      return line.Equals("utc", StringComparison.InvariantCultureIgnoreCase);
    }
  }
}