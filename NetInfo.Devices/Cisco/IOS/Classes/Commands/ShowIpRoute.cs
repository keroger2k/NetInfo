using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands
{
    public class ShowIpRoute : BaseSetting
    {

        private Regex rgxGateway = new Regex(@"Gateway of last resort is (?<nextHop>\d+.\d+.\d+.\d+) to network (?<network>\d+.\d+.\d+.\d+)");
        private Regex rgxGatewayNotSet = new Regex(@"Gateway of last resort is not set");
        private Regex rgxRouteNetworks = new Regex(@"^(?<routeCode>\w+)\s+(?<routeSubCode>\w+)?\s+(?<network>\d+\.\d+\.\d+\.\d+)\/(?<networkMask>\d+)\s\[\d+\/\d+\]\svia\s(?<nextHop>\d+\.\d+\.\d+\.\d+),\s.*,\s(?<nextHopInterface>.*)");
        private Regex rgxRouteOnlyNetworkSingleLine = new Regex(@"^(?<routeCode>\w+)\s+(?<routeSubCode>\w+)?\s+(?<network>\d+\.\d+\.\d+\.\d+)\/(?<networkMask>\d+)\s+$");
        private Regex rgxRouteNetworkEntry = new Regex(@"^\s+\[\d+\/\d+\]\svia\s(?<nextHop>\d+\.\d+\.\d+\.\d+),\s.*,\s(?<nextHopInterface>.*)$");
        public ShowIpRoute(IEnumerable<string> settings)
        {
            this.Settings = settings;
        }

        public Gateway GatewayLastResort
        {
            get
            {
                Gateway gw = null;
                for (int i = 0; i < Settings.Count(); i++)
                {
                    var m = rgxGatewayNotSet.Match(Settings.ElementAt(i));
                    if (m.Success)
                    {
                        break;
                    }
                    var n = rgxGateway.Match(Settings.ElementAt(i));
                    if (n.Success)
                    {
                        gw = new Gateway
                        {
                            NextHop = IPAddress.Parse(n.Groups["nextHop"].ToString()),
                            Network = IPAddress.Parse(n.Groups["network"].ToString())
                        };
                    }
                }
                return gw;
            }
        }

        public IEnumerable<RouteTableNetwork> RouteTableNetorks
        {
            get
            {
                var rtNetworks = new List<RouteTableNetwork>();
                for (int i = 0; i < Settings.Count(); i++)
                {
                    var m = rgxRouteNetworks.Match(Settings.ElementAt(i));
                    if (m.Success)
                    {
                        var rtn = new RouteTableNetwork
                        {
                            RouteCode = m.Groups["routeCode"].ToString(),
                            RouteSubCode = m.Groups["routeSubCode"].ToString(),
                            CIDRMask = Int32.Parse(m.Groups["networkMask"].ToString()),
                            Network = IPAddress.Parse(m.Groups["network"].ToString()),
                        };

                        var nhrs = new List<NextHopRoute>();
                        var nhr = new NextHopRoute
                        {
                            NextHop = IPAddress.Parse(m.Groups["nextHop"].ToString()),
                            NextHopInterface = m.Groups["nextHopInterface"].ToString()
                        };
                        nhrs.Add(nhr);
                        rtn.NextHops = nhrs;
                        var ii = i + 1;
                        while (ii < Settings.Count() && rgxRouteNetworkEntry.Match(Settings.ElementAt(ii)).Success)
                        {
                            nhrs.Add(new NextHopRoute
                            {
                                NextHop = IPAddress.Parse(m.Groups["nextHop"].ToString()),
                                NextHopInterface = m.Groups["nextHopInterface"].ToString()
                            });
                            ii++;
                        }
                        rtNetworks.Add(rtn);
                    }
                    else if (rgxRouteOnlyNetworkSingleLine.Match(Settings.ElementAt(i)).Success)
                    {
                        var n = rgxRouteOnlyNetworkSingleLine.Match(Settings.ElementAt(i));
                        var rtn = new RouteTableNetwork
                        {
                            RouteCode = n.Groups["routeCode"].ToString(),
                            RouteSubCode = n.Groups["routeSubCode"].ToString(),
                            CIDRMask = Int32.Parse(n.Groups["networkMask"].ToString()),
                            Network = IPAddress.Parse(n.Groups["network"].ToString()),
                        };

                        var nhrs = new List<NextHopRoute>();
                        var ii = i + 1;
                        while (ii < Settings.Count() && rgxRouteNetworkEntry.Match(Settings.ElementAt(ii)).Success)
                        {
                            var o = rgxRouteNetworkEntry.Match(Settings.ElementAt(ii));
                            nhrs.Add(new NextHopRoute
                            {
                                NextHop = IPAddress.Parse(o.Groups["nextHop"].ToString()),
                                NextHopInterface = o.Groups["nextHopInterface"].ToString()
                            });
                            ii++;
                        }

                        rtn.NextHops = nhrs;
                        rtNetworks.Add(rtn);
                    }
                }
                return rtNetworks;
            }
        }

        public class Gateway
        {
            public IPAddress NextHop { get; set; }
            public IPAddress Network { get; set; }
        }

        public class RouteTableNetwork
        {
            public string RouteCode { get; set; }
            public string RouteSubCode { get; set; }
            public IPAddress Network { get; set; }
            public int CIDRMask { get; set; }
            public IEnumerable<NextHopRoute> NextHops { get; set; }
        }
        public class NextHopRoute
        {
            public IPAddress NextHop { get; set; }
            public string NextHopInterface { get; set; }
        }
    }
}
