using System;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all of the RSA keys have NMCI-ISF.com in the key name on unclassified devices
  ///
  /// See the ""show crypto key mypubkey rsa"" output.
  /// Contact the NOC to correct the configuration.
  ///
  /// Exceptions:
  ///   Wireless Devices
  /// </summary>
  public class IR156 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR156(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var key = ((INMCIIOSDevice)Device).CryptoKeyName;
      return (string.IsNullOrEmpty(key) ? false : key.Equals("NMCI-ISF.com", StringComparison.InvariantCultureIgnoreCase));
    }
  }
}