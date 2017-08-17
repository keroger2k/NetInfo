using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  ///
  /// DEPRECATED!
  ///
  /// Checking these is a function of the Asset Team.
  /// The Asset Team takes the action to get the NOC to update the location strings.
  /// The Audit Team no longer need to check this.
  /// </summary>
  public class IR137 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR137(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.SNMPSettings.Location != null;
    }
  }
}