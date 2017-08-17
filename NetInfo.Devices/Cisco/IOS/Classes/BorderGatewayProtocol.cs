using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class BorderGatewayProtocol : BaseSetting {

    public int ASN {
      get {
        var r = GetSetting(new Regex(@"router bgp (\d+)", RegexOptions.IgnoreCase));
        return r == null ? -1 : int.Parse(r.Groups[1].Value);
      }
    }

    public IEnumerable<Neighbor> Neighbors {
      get {
        var neighbors = new List<Neighbor>();
        var matchingNeighbors = GetSettings(new Regex(@"neighbor\s+(?<ipAddress>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}).*"));
        var distinctMatchingNeighbors = matchingNeighbors.Select(c => IPAddress.Parse(c.Groups["ipAddress"].Value)).Distinct();
        foreach (var distinctMatchingNeighbor in distinctMatchingNeighbors) {
          var neighbor = new Neighbor();
          neighbor.Address = distinctMatchingNeighbor;
          var neighborSettings = GetSettings(new Regex(string.Format("(?<setting>neighbor\\s+{0}.*)", distinctMatchingNeighbor.ToString())));
          neighbor.Settings = neighborSettings.Select(c => c.Groups["setting"].Value);
          neighbors.Add(neighbor);
        }
        return neighbors;
      }
    }

    public IEnumerable<PeerGroup> PeerGroups {
      get {
        var peers = new List<PeerGroup>();
        var peerGroups = GetSettings(new Regex(@"neighbor\s+(?<peerGroup>.*) peer-group(\s+)?$"));
        foreach (var peerGroup in peerGroups) {
          var peer = new PeerGroup();
          peer.Name = peerGroup.Groups["peerGroup"].Value;
          var peerGroupSettings = GetSettings(new Regex(string.Format(@"(?<setting>neighbor\s+{0}\s+.*)", peer.Name)));
          peer.Settings = peerGroupSettings.Select(c => c.Groups["setting"].Value);
          peers.Add(peer);
        }
        return peers;
      }
    }

    public class PeerGroup : BaseSetting {

      public string Name { get; set; }

      public string Password {
        get {
          var r = GetSetting(new Regex(string.Format(@"neighbor\s+{0}\s+password 7 (?<hash>.*)", this.Name)));
          return r == null ? string.Empty : r.Groups["hash"].Value;
        }
      }

      public int RemoteAS {
        get {
          var r = GetSetting(new Regex(string.Format(@"neighbor\s+{0}\s+remote-as\s+(?<remoteAS>\d+)", this.Name)));
          return r == null ? -1 : int.Parse(r.Groups["remoteAS"].Value);
        }
      }
    }

    public class Neighbor : BaseSetting {

      public IPAddress Address { get; set; }

      public int RemoteAS {
        get {
          var r = GetSetting(new Regex(string.Format(@"neighbor\s+{0}\s+remote-as\s+(?<remoteAS>\d+)", this.Address)));
          return r == null ? -1 : int.Parse(r.Groups["remoteAS"].Value);
        }
      }

      public string Description { get; set; }

      public string Password {
        get {
          var r = GetSetting(new Regex(string.Format(@"neighbor\s+{0}\s+password 7 (?<hash>.*)", this.Address)));
          return r == null ? string.Empty : r.Groups["hash"].Value;
        }
      }

      public string PeerGroup {
        get {
          var r = GetSetting(new Regex(string.Format(@"neighbor\s+{0}\s+peer-group (?<peerGroup>.*)", this.Address)));
          return r == null ? string.Empty : r.Groups["peerGroup"].Value;
        }
      }

      public bool NextHopSelfEnabled { get; set; }

      public int AllowAsIn { get; set; }

      public PrefixListSetting PrefixList { get; set; }

      public RouteMapSetting RouteMap { get; set; }

      public FilterListSetting FilterList { get; set; }

      public class PrefixListSetting {

        public enum Direction {
          inbound,
          outbound
        }

        public string Name { get; set; }
      }

      public class RouteMapSetting {

        public enum Direction {
          inbound,
          outbound
        }

        public string Name { get; set; }
      }

      public class FilterListSetting {

        public enum Direction {
          inbound,
          outbound
        }

        public int Number { get; set; }
      }
    }
  }
}