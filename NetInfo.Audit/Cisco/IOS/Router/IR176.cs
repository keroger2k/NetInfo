using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure these commands exist: "no vstack enable" or "no vstack"
  ///
  /// The command seen in the configuration is platform specific and available
  /// in version 12.2(52)SE and higher Catalyst switch images.
  /// By default vstack is enabled; only the "no" command will appear in
  /// the "show configution" output.
  /// </summary>
  public class IR176 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR176(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.VStackEnabled;
    }
  }
}