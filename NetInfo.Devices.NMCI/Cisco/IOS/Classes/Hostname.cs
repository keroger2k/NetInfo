using NetInfo.Devices.NMCI.Infrastructure.Implementations.Cisco.IOS;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Cisco.IOS {

  public class Hostname : IOSHostname {

    public Hostname(IHostname hostname)
      : base(hostname.Name) {
    }

    public string Zone {
      get { return string.IsNullOrEmpty(_hostName) ? null : _hostName.Substring(6, 2); }
    }

    public string DeviceType {
      get { return string.IsNullOrEmpty(_hostName) ? null : _hostName.Substring(9, 2); }
    }
  }
}