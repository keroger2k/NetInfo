using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set snmp community "comm_string" Read-Only Trap-on is correct and up to date
  /// </summary>
  public class VP019 : ISTIGItem {
    private IEnumerable<string> _communityStrings;

    public IDevice Device { get; private set; }

    public VP019(INMCIScreenOSDevice device, IEnumerable<string> communityStrings) {
      this.Device = device;
      this._communityStrings = communityStrings;
    }

    public bool Compliant() {
      var comm = ((INMCIScreenOSDevice)Device).SNMPSettings.Community;
      return _communityStrings.Contains(comm);
    }
  }
}