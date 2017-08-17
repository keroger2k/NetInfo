using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Juniper.ScreenOS.Commands {

  public class GetRoute {
    private static readonly Regex RouteRgx = new Regex(@"\*?\s+(?<id>\d+)\s+(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\/(?<prefix>\d+)\s+(?<interface>.*)\s+(?<gateway>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}|untrust-vr|trust-vr)\s+(?<type>\w+)\s+(?<pref>\d+)\s+(?<metric>\d+)\s+(?<vsys>\w+)", RegexOptions.IgnoreCase);

    //private static readonly Regex RouteStartRgx = new Regex(@"\* \d+")
    private readonly IEnumerable<string> _output;

    public GetRoute(IEnumerable<string> output) {
      this._output = output;
    }

    public IEnumerable<Route> Untrusted {
      get {
        return ParseRoutes(new Regex(@"^(untrust-vr|IPv4 Dest-Routes for <untrust-vr>) \((\d+) entries\)", RegexOptions.IgnoreCase));
      }
    }

    public IEnumerable<Route> Trusted {
      get { return ParseRoutes(new Regex(@"^(trust-vr|IPv4 Dest-Routes for <trust-vr>) \((\d+) entries\)", RegexOptions.IgnoreCase)); }
    }

    private List<Route> ParseRoutes(Regex rgx) {
      var routes = new List<Route>();
      int i = 0;
      while (true) {
        if (rgx.Match(_output.ElementAt(i)).Success) {
          var numberOfEntries = int.Parse(rgx.Match(_output.ElementAt(i)).Groups[2].Value);
          var startPosition = i + 4;
          for (var ii = startPosition; ii < startPosition + numberOfEntries; ii++) {
            var match = RouteRgx.Match(_output.ElementAt(ii));
            IPAddress tmpAddress = null;
            var route = new Route {
              Id = int.Parse(match.Groups["id"].Value),
              Address = IPAddress.Parse(match.Groups["ip"].Value),
              Prefix = byte.Parse(match.Groups["prefix"].Value),
              Interface = match.Groups["interface"].Value,
              Gateway = IPAddress.TryParse(match.Groups["gateway"].Value, out tmpAddress) ? tmpAddress : null, //not always an ip
              Preference = int.Parse(match.Groups["pref"].Value),
              Metric = int.Parse(match.Groups["metric"].Value),
              Vsys = match.Groups["vsys"].Value,
            };
            route.SetRouteType(match.Groups["type"].Value);
            routes.Add(route);
          }
          break;
        }
        i++;
      }
      return routes;
    }

    public class Route {
      private RouteType _type = RouteType.Unknown;

      public enum RouteType {
        Connected,
        Static,
        Host,
        AutoExported,
        Imported,
        RIP,
        IBGP,
        EBGP,
        OSPF,
        OSPFE1,
        OSPFE2,
        Unknown
      }

      public int Id { get; set; }

      public IPAddress Address { get; set; }

      public byte Prefix { get; set; }

      public string Interface { get; set; }

      public IPAddress Gateway { get; set; }

      public int Preference { get; set; }

      public int Metric { get; set; }

      public string Vsys { get; set; }

      public RouteType Type {
        get {
          return _type;
        }
      }

      public void SetRouteType(string route) {
        switch (route) {
          case "C":
            _type = RouteType.Connected;
            break;

          case "S":
            _type = RouteType.Connected;
            break;

          case "A":
            _type = RouteType.Connected;
            break;

          case "I":
            _type = RouteType.Connected;
            break;

          case "R":
            _type = RouteType.Connected;
            break;

          case "iB":
            _type = RouteType.Connected;
            break;

          case "eB":
            _type = RouteType.Connected;
            break;

          case "O":
            _type = RouteType.Connected;
            break;

          case "E1":
            _type = RouteType.Connected;
            break;

          case "E2":
            _type = RouteType.Connected;
            break;

          default:
            _type = RouteType.Unknown;
            break;
        }
      }
    }
  }
}