using NetInfo.Devices.NMCI.Infrastructure.Implementations.Brocade.BOS;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Brocade.BOS {

  public class Hostname : BOSHostname {

    public Hostname(IHostname hostname)
      : base(hostname.Name) {
    }

    public string Zone {
      get { return _hostName.Substring(6, 2); }
    }

    public string DeviceType {
      get { return _hostName.Substring(9, 2); }
    }
  }
}