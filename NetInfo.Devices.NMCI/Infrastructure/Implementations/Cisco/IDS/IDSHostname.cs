using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Cisco.IOS {

  public class IDSHostname : IHostname {
    protected readonly string _hostName;

    public IDSHostname(string hostname) {
      this._hostName = hostname;
    }

    public string Name {
      get { return this._hostName; }
    }

    public string Site {
      get { return string.IsNullOrEmpty(this._hostName) ? null : this._hostName.Substring(0, 4); }
    }
  }
}