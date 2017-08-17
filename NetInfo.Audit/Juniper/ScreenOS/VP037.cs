using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set pki x509 dn org-unit-name "USN,PKI,DoD"
  /// </summary>
  public class VP037 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP037(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.PKISettings.x509.DN.OrgUnitName.Equals("USN,PKI,DoD", StringComparison.OrdinalIgnoreCase);
    }
  }
}