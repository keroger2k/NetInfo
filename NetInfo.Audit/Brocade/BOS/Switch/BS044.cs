using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Validate the current major version of the hardening script is applied
  /// </summary>
  public class BS044 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int _hardeningMajorVersion;

    public BS044(INMCIBOSDevice device, int hardeningVersion) {
      this.Device = device;
      this._hardeningMajorVersion = hardeningVersion;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      if (device.AliasSettings != null) {
        var val = device.AliasSettings.GetValue("harden");
        var rgx = new Regex(@".*-v(\d+)_\d+_\d+", RegexOptions.IgnoreCase).Match(val);
        if (rgx.Success) {
          return int.Parse(rgx.Groups[1].Value) == _hardeningMajorVersion;
        }
      }
      return false;
    }
  }
}