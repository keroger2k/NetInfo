using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate SNMP server host IP's match the latest NMS Script, remove any extras
  /// </summary>
  public class RB022 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _servers;

    public RB022(INMCIRIOSDevice device, IEnumerable<IPAddress> servers) {
      this.Device = device;
      _servers = servers;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      var u = device.SNMP.Servers.Select(c => c.Address).Intersect(_servers);
      return device.SNMP.Servers
        .Select(c => c.Address)
        .OrderBy(c => c.ToString())
        .SequenceEqual(_servers.OrderBy(c => c.ToString()));
    }
  }
}