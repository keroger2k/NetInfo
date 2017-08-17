using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate the latest major version NMS Script is applied
  /// </summary>
  public class IS082 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int _nmsMajorVersion;

    public IS082(INMCIIOSDevice device, int nmsMajorVersion) {
      this.Device = device;
      this._nmsMajorVersion = nmsMajorVersion;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.AliasExecSettings != null &&
        device.AliasExecSettings.NMSSettings != null &&
        device.AliasExecSettings.NMSSettings.MajorVersion == _nmsMajorVersion;
    }
  }
}