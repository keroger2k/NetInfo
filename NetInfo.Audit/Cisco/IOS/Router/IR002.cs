using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all BGP neighbor relationships are configured with MD5 keys
  ///
  /// Exception:
  ///   The URB router templates do not include a BGP password for the UUNet link.
  /// </summary>
  public class IR002 : ISTIGItem {

    public IDevice Device { get; private set; }
    private IEnumerable<BorderGatewayProtocol.Neighbor> neighborsNoPeerGroup;
    private ICollection<BorderGatewayProtocol.PeerGroup> listOfPeers;

    public IR002(IIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (IIOSDevice)Device;
      this.neighborsNoPeerGroup = device.BGP.Neighbors
        .Where(c => c.RemoteAS != 701 && string.IsNullOrEmpty(c.PeerGroup));

      var neighborsWithPeerGroups = device.BGP.Neighbors
          .Where(c => c.RemoteAS != 701 && !string.IsNullOrEmpty(c.PeerGroup))
          .Select(c => c.PeerGroup).Distinct();

      this.listOfPeers = new List<BorderGatewayProtocol.PeerGroup>();
      foreach (var peerGroup in neighborsWithPeerGroups) {
        this.listOfPeers.Add(device.BGP.PeerGroups.FirstOrDefault(c => c.Name.Equals(peerGroup)));
      }

      return this.neighborsNoPeerGroup.All(c => !string.IsNullOrEmpty(c.Password)) && listOfPeers.All(c => !string.IsNullOrEmpty(c.Password));
    }

    public override string ToString() {
      string message = string.Empty;

      if (this.Compliant()) {
        message = "Passing: All BGP neighbors are configured with an MD5 key.";
      } else {
        
        message = string.Format("Neighbors with no password: {0}\nPeer groups with no password: {1}",
          string.Join(", ", this.neighborsNoPeerGroup.Where(c => string.IsNullOrEmpty(c.Password)).Select(c => c.Address.ToString())),
          string.Join(", ", this.listOfPeers.Where(c => string.IsNullOrEmpty(c.Password)).Select(c => c.Name))
          );
      }
      return message;
    }
  }
}