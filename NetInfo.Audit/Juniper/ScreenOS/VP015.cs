using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate set pki x509 dn country-name "US"
  /// </summary>
  public class VP015 : ISTIGItem {

    public IDevice Device { get; private set; }

    public VP015(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      return device.PKISettings.x509.DN.CountryName.Equals("US", StringComparison.OrdinalIgnoreCase);
    }
  }
}