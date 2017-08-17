using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set pki x509 dn org-name "U.S. Government"
  /// </summary>
  public class VP016 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP016(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.PKISettings.x509.DN.OrgName.Equals("U.S. Government", StringComparison.OrdinalIgnoreCase);
    }
  }
}