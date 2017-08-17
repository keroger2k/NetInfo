using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Riverbed.RIOS {

  public class RIOSHostname : IHostname {
    protected readonly string _hostName;

    public RIOSHostname(string hostname) {
      this._hostName = hostname;
    }

    public string Name {
      get { return this._hostName; }
    }

    public string Site {
      get { return this.Name.Substring(0, 4); }
    }
  }
}