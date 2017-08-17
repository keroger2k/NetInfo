using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class EnhancedInteriorGatewayRoutingProtocol : BaseSetting {

    public int ASN {
      get {
        var r = GetSetting(new Regex(@"router eigrp (\d+)", RegexOptions.IgnoreCase));
        return r == null ? -1 : int.Parse(r.Groups[1].Value);
      }
    }

    public ICollection<Network> Networks {
      get {
        var collection = new List<Network>();
        var r = GetSettings(new Regex(@"network (?<subnet>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\s+(?<inverse>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.IgnoreCase));
        if (r != null) {
          collection.AddRange(r.Select(c => new Network {
            Subnet = IPAddress.Parse(c.Groups["subnet"].Value),
            Inverse = IPAddress.Parse(c.Groups["inverse"].Value)
          }));
        }
        return collection;
      }
    }

    public class Network {

      public IPAddress Subnet { get; set; }

      public IPAddress Inverse { get; set; }
    }
  }
}