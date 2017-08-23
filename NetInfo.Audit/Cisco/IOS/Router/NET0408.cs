using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title:  The network element must authenticate all BGP peers within the same or between autonomous systems (AS).
    /// STIG ID:	NET0408     
    /// Rule ID:	SV-41555r2_rule
    /// Vuln ID:	V-31285       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0408 : ICiscoRouterSecurityItem
    {

        private IIOSDevice _device;
        private IEnumerable<BorderGatewayProtocol.Neighbor> neighborsNoPeerGroup;
        private ICollection<BorderGatewayProtocol.PeerGroup> listOfPeers;

        public NET0408(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            this.neighborsNoPeerGroup = _device.BGP.Neighbors
              .Where(c => string.IsNullOrEmpty(c.PeerGroup));

            var neighborsWithPeerGroups = _device.BGP.Neighbors
                .Where(c => !string.IsNullOrEmpty(c.PeerGroup))
                .Select(c => c.PeerGroup).Distinct();

            this.listOfPeers = new List<BorderGatewayProtocol.PeerGroup>();
            foreach (var peerGroup in neighborsWithPeerGroups)
            {
                this.listOfPeers.Add(_device.BGP.PeerGroups.FirstOrDefault(c => c.Name.Equals(peerGroup)));
            }

            return this.neighborsNoPeerGroup.All(c => !string.IsNullOrEmpty(c.Password)) && listOfPeers.All(c => !string.IsNullOrEmpty(c.Password));
        }

        public override string ToString()
        {
            string message = string.Empty;

            if (this.Compliant())
            {
                message = "Passing: All BGP neighbors are configured with an MD5 key.";
            }
            else
            {

                message = string.Format("Neighbors with no password: {0}\nPeer groups with no password: {1}",
                  string.Join(", ", this.neighborsNoPeerGroup.Where(c => string.IsNullOrEmpty(c.Password)).Select(c => c.Address.ToString())),
                  string.Join(", ", this.listOfPeers.Where(c => string.IsNullOrEmpty(c.Password)).Select(c => c.Name))
                  );
            }
            return message;
        }
    }
}