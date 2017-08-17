using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate Peer Manager IP address matches IP address of Peer IPS manager at alternate NOC (see comment)
  ///
  /// Managing NOC is determined by hostname domain prefix. 
  /// For devices that start with NMCI, the PSI must be matched to the managing NOC:
  ///   NAEA or NMCI (NRFK Homed Sites) is "10.0.7.45" or "10.0.7.46";
  ///   AHDS, AQDS, NAWE, or NMCI (PRLH/SDNI Homed sites) is "10.16.7.50" or "10.16.7.51";
  ///   MCUS or MCJP is "138.156.126.112";
  /// </summary>
  public class IP011 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _managerAddresses;

    public IP011(INMCIMcAfeeDevice device, IEnumerable<IPAddress> managerAddresses) {
      this.Device = device;
      this._managerAddresses = managerAddresses;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return _managerAddresses.Contains(device.PeerManagerConfig.Address);
    }
  }
}