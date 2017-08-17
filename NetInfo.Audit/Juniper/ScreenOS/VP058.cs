using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set pki x509 dn org-unit-name "USMC,PKI,DoD"
  /// </summary>
  public class VP058 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP058(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.PKISettings.x509.DN.OrgUnitName.Equals("USMC,PKI,DoD", StringComparison.OrdinalIgnoreCase);
    }
  }
}