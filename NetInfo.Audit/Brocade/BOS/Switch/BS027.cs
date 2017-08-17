using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the remote logging (syslog) servers are set according to the latest NMS script
  /// </summary>
  public class BS027 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _approvedServers;

    public BS027(INMCIBOSDevice device, IEnumerable<IPAddress> approvedServers) {
      this.Device = device;
      this._approvedServers = approvedServers;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var deviceUsers = device.LoggingSettings.Hosts.Select(c => c.ToString()).OrderBy(c => c);
      var approvedUsers = _approvedServers.Select(c => c.ToString()).OrderBy(c => c);
      return approvedUsers.SequenceEqual(deviceUsers);
    }
  }
}