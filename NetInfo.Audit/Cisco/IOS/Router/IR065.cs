using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure SNMP read and read/write communities are set per the NMS script
  ///
  /// This only pertains to managed devices that do not use SNMPv3.
  /// </summary>
  public class IR065 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedCommunities;

    public IR065(INMCIIOSDevice device, IEnumerable<string> approvedCommunities) {
      this.Device = device;
      this._approvedCommunities = approvedCommunities;
    }

    /// <summary>
    /// Should only be checking devices that are not SNMPv3.
    /// </summary>
    /// <returns></returns>
    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var configuredStrings = device.SNMPSettings.Communities.Select(c => c.CommunityString).OrderBy(c => c);
      return _approvedCommunities.OrderBy(c => c).SequenceEqual(configuredStrings);
    }
  }
}