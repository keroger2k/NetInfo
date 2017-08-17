using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate TACACS server host IP's match the latest NMS Script, remove any extras
  /// </summary>
  public class RB023 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _valid;

    public RB023(INMCIRIOSDevice device, IEnumerable<IPAddress> valid) {
      this.Device = device;
      _valid = valid;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      var u = device.Tacacs.Hosts.Select(c => c.Host).Intersect(_valid);
      return device.Tacacs.Hosts.Count() == _valid.Count() && u.Count() == _valid.Count() && _valid.SequenceEqual(device.Tacacs.Hosts.Select(c => c.Host));
    }
  }
}