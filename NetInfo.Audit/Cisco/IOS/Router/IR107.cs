using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate the major version of the applied hardening script is applied
  /// </summary>
  public class IR107 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int _hardeningMajorVersion;

    public IR107(INMCIIOSDevice device, int hardeningMajorVersion) {
      this.Device = device;
      this._hardeningMajorVersion = hardeningMajorVersion;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.AliasExecSettings != null &&
        device.AliasExecSettings.HardeningSettings != null &&
        device.AliasExecSettings.HardeningSettings.MajorVersion == _hardeningMajorVersion;
    }
  }
}