using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate NTP server IP's match the latest NMS Script, remove any extras
  /// </summary>
  public class RB021 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _valid;

    public RB021(INMCIRIOSDevice device, IEnumerable<IPAddress> valid) {
      this.Device = device;
      _valid = valid;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      var u = device.NTP.Servers.Select(c => c.Address).Intersect(_valid);
      return device.NTP.Servers.Count() == _valid.Count() && u.Count() == _valid.Count() && _valid.SequenceEqual(device.NTP.Servers.Select(c => c.Address));
    }
  }
}