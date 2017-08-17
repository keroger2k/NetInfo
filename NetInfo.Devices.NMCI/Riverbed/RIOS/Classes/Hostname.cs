using System.Collections.Generic;
using NetInfo.Devices.NMCI.Infrastructure.Implementations.Riverbed.RIOS;
using NetInfo.Devices.NMCI.Infrastructure.Interfaces;

namespace NetInfo.Devices.NMCI.Riverbed.RIOS {

  public class Hostname : RIOSHostname {
    private static readonly Dictionary<string, string> typeMap;

    static Hostname() {
      typeMap = new Dictionary<string, string>();
      typeMap["CM"] = "Inner";
      typeMap["WI"] = "Inner";
      typeMap["WX"] = "Inner";
    }

    public Hostname(IHostname hostname)
      : base(hostname.Name) {
    }

    public string Zone {
      get { return this.Name.Substring(6, 2); }
    }

    public string DeviceType {
      get { return this.Name.Substring(9, 2); }
    }
  }
}