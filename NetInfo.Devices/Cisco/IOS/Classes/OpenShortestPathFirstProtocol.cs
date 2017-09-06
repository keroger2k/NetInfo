using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS
{

    public class OpenShortestPathFirstProtocol : BaseSetting
    {

        public int ProcessId
        {
            get
            {
                var r = GetSetting(new Regex(@"router ospf (\d+)", RegexOptions.IgnoreCase));
                return r == null ? -1 : int.Parse(r.Groups[1].Value);
            }
        }

        public IEnumerable<int> AuthenticaionDigestAreas
        {
            get
            {
                var r = GetSettings(new Regex(@"^\s+?area (?<areaNumber>\d+) authentication .*$", RegexOptions.IgnoreCase));
                return r == null ? new List<int>() : r.Select(c => int.Parse(c.Groups["areaNumber"].Value));
            }
        }

        public IEnumerable<string> NonPassiveInterfaces
        {
            get
            {
                var r = GetSettings(new Regex(@"no passive-interface (?<interface>.*)", RegexOptions.IgnoreCase));
                return r == null ? new List<string>() : r.Select(c => c.Groups["interface"].Value);
            }
        }

        public IEnumerable<string> PassiveInterfaces
        {
            get
            {
                var r = GetSettings(new Regex(@"^\s+passive-interface (?<interface>.*)", RegexOptions.IgnoreCase));
                return r == null ? new List<string>() : r.Select(c => c.Groups["interface"].Value);
            }
        }

        public IEnumerable<OSPFNetworkCommand> Networks
        {
            get
            {
                var r = GetSettings(new Regex(@"\s+network (?<network>.*) (?<mask>.*) area (?<area>.*)", RegexOptions.IgnoreCase));
                return r == null ? new List<OSPFNetworkCommand>() : r.Select(c => new OSPFNetworkCommand
                {
                    Network = IPAddress.Parse(c.Groups["network"].Value),
                    InverseMask = IPAddress.Parse(c.Groups["mask"].Value),
                    Area = c.Groups["area"].Value
                });
            }
        }

        public class OSPFNetworkCommand
        {
            public IPAddress Network { get; set; }
            public IPAddress InverseMask { get; set; }
            public string Area { get; set; }
        }

    }
}