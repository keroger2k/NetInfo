using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Juniper.ScreenOS {

  public class ScreenOSHostname : IHostname {
    protected readonly string _hostName;

    public ScreenOSHostname(string hostname) {
      this._hostName = hostname;
    }

    public string Name {
      get { return this._hostName; }
    }

    public string Site {
      get { return this.Name.Substring(4, 4); }
    }
  }
}