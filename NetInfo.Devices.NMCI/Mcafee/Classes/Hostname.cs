using NetInfo.Devices.NMCI.Infrastructure.Implementations.McAfee;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.McAfee {

  public class Hostname : McAfeeHostname {

    public Hostname(IHostname hostname)
      : base(hostname.Name) {
    }
  }
}