using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Infrastructure.Implementations.Brocade.BOS {

  public class BOSHostname : IHostname {
    private static Regex NAMING_FORMAT = new Regex(@"[\w]{4}-[\w]{3}-[\w]{2}-[\d]{2}");
    protected readonly string _hostName;

    public BOSHostname(string hostname) {
      if (NAMING_FORMAT.Match(hostname).Success) {
        this._hostName = hostname;
      }
    }

    public string Name {
      get { return this._hostName; }
    }

    public string Site {
      get { return string.IsNullOrEmpty(this._hostName) ? null : this._hostName.Substring(0, 4); }
    }
  }
}