using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate ISM Config Manager IP addr uses the managing NOC's Master IPS manager (see comment)
  ///
  /// Managing NOC is determined by hostname domain prefix. 
  /// For devices that start with NMCI, the PSI must be matched to the managing NOC:
  ///   NAEA or NMCI (NRFK Homed Sites) is "10.16.7.52" or "10.16.7.70;
  ///   AHDS, AQDS, NAWE, or NMCI (PRLH/SDNI Homed Sites) is "10.0.7.44" or "10.0.7.47";
  ///   MCUS or MCJP is "138.156.126.111";
  /// </summary>
  public class IP007 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<IPAddress> _managerAddresses;

    public IP007(INMCIMcAfeeDevice device, IEnumerable<IPAddress> managerAddresses) {
      this.Device = device;
      this._managerAddresses = managerAddresses;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      return _managerAddresses.Contains(device.ManagerConfig.Address);
    }
  }
}