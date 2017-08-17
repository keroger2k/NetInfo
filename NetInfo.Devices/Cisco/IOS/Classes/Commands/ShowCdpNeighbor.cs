using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands
{

    public class ShowCdpNeighbor : BaseSetting
    {

        /// <summary>
        /// Regex to match
        ///
        ///   Source Interface: Orginating Device's Interface Type
        ///   Source Port: Orginating Device's Port Number
        ///   Destination Interface: Destination Device's Interface Type (ONLY Supports Gig or Fas until I see the rest of the formats)
        ///   Destination Port: Destination Device's Port Number
        /// </summary>
        private readonly Regex rgxInterface = new Regex(@"^\s+(?<sourceInt>Gig|Fas|Ser|Ten)(?<sourcePort>.*)\s+\d+\s+(\w|\-\s)+.+(?<destinationInt>Gig|Fas|Ser|Ten)\s+(?<destinationPort>.*)", RegexOptions.IgnoreCase);
        private readonly Regex rgxSingleLineEntryInterface = new Regex(@"^(?<destinationHostname>(\w|\-)+)\s+(?<sourceInt>Gig|Fas|Ser|Ten)(?<sourcePort>.*)\s+\d+\s+(\w|\-\s)+.+(?<destinationInt>Gig|Fas|Ser|Ten)\s+(?<destinationPort>.*)", RegexOptions.IgnoreCase);
        public ShowCdpNeighbor(IEnumerable<string> settings)
        {
            this.Settings = settings;
        }

        public IEnumerable<CDPNeighborInterface> Interfaces
        {
            get
            {
                var list = new List<CDPNeighborInterface>();
                for (int i = 0; i < Settings.Count(); i++)
                {
                    var m = rgxInterface.Match(Settings.ElementAt(i));
                    var n = rgxSingleLineEntryInterface.Match(Settings.ElementAt(i));
                    if (m.Success)
                    {
                        list.Add(new CDPNeighborInterface
                        {
                            DestinationHostname = Settings.ElementAt(i - 1),
                            SourceInterface = m.Groups["sourceInt"].Value,
                            SourcePort = m.Groups["sourcePort"].Value,
                            DestinationInterface = m.Groups["destinationInt"].Value,
                            DestinationPort = m.Groups["destinationPort"].Value
                        });
                    }
                    else if (n.Success)
                    {
                        list.Add(new CDPNeighborInterface
                        {
                            DestinationHostname = n.Groups["destinationHostname"].Value,
                            SourceInterface = n.Groups["sourceInt"].Value,
                            SourcePort = n.Groups["sourcePort"].Value,
                            DestinationInterface = n.Groups["destinationInt"].Value,
                            DestinationPort = n.Groups["destinationPort"].Value
                        });
                    }
                }
                return list;
            }
        }

        public class CDPNeighborInterface
        {

            public string DestinationHostname { get; set; }

            public string SourceInterface { get; set; }

            public string SourcePort { get; set; }

            public string DestinationInterface { get; set; }

            public string DestinationPort { get; set; }
        }
    }
}