using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate the correct snmp hosts are configured per the hardening script (USMC)
  /// </summary>
  public class VP067 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<IPAddress> _addresses;

    public VP067(INMCIScreenOSDevice device, IEnumerable<IPAddress> addresses) {
      this.Device = device;
      _addresses = addresses;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      var r = _addresses.Intersect(device.SNMPSettings.Hosts.Select(c => c.Host));
      return r.Count() == _addresses.Count() && _addresses.SequenceEqual(device.SNMPSettings.Hosts.Select(c => c.Host));
    }
  }
}