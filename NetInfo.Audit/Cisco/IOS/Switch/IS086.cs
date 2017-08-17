using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate SNMP Server Host IP's match the NMS Script, remove any extras
  /// </summary>
  public class IS086 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _servers;

    public IS086(INMCIIOSDevice device, IEnumerable<IPAddress> servers) {
      this.Device = device;
      this._servers = servers;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      return device.SNMPSettings.Servers
        .Select(c => c.Address)
        .OrderBy(c => c.ToString())
        .SequenceEqual(_servers.OrderBy(c => c.ToString()));
    }
  }
}