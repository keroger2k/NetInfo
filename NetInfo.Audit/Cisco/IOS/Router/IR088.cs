using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate the latest major version NMS Script is applied
  /// </summary>
  public class IR088 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int _nmsMajorVersion;

    public IR088(INMCIIOSDevice device, int nmsMajorVersion) {
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