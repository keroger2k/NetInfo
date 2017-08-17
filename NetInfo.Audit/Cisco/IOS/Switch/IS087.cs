using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate there are at least 2 TACACS server hosts configured, and the host IP's match the NMS Script: remove any extraneous hosts.
  /// </summary>
  public class IS087 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _servers;

    public IS087(INMCIIOSDevice device, IEnumerable<IPAddress> servers) {
      this.Device = device;
      this._servers = servers;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      return device.TacacsServer.Hosts.Count() > 1 && device.TacacsServer.Hosts.All(c => _servers.Contains(c));
    }
  }
}