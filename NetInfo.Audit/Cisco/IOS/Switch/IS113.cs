using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure crypto pki trustpoints & crypto pki certification chains do not exist
  /// </summary>
  public class IS113 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS113(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return
        string.IsNullOrEmpty(device.Crypto.PKI.CertificateChain) &&
        string.IsNullOrEmpty(device.Crypto.PKI.TrustPoint);
    }
  }
}