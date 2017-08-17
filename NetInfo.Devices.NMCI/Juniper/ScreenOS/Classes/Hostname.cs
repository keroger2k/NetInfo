using NetInfo.Devices.NMCI.Infrastructure.Implementations.Juniper.ScreenOS;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Juniper.ScreenOS {

  public class Hostname : ScreenOSHostname {

    public Hostname(IHostname hostname)
      : base(hostname.Name) {
    }
  }
}