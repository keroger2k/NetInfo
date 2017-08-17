using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate status of device shows Healthy
  /// </summary>
  public class RB044 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB044(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return device.Status.Equals("Healthy", StringComparison.InvariantCultureIgnoreCase);
    }
  }
}