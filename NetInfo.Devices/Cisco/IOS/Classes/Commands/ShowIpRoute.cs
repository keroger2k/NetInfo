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
                    if(n.Success)
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

        public class Gateway
        {
            public IPAddress NextHop { get; set; }
            public IPAddress Network { get; set; }
        }
    }
}
