using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Validate the latest major version NMS Script is applied
  /// </summary>
  public class BS043 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int _nmsMajorVersion;

    public BS043(INMCIBOSDevice device, int nmsMajorVersion) {
      this.Device = device;
      this._nmsMajorVersion = nmsMajorVersion;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      if (device.AliasSettings != null) {
        var val = device.AliasSettings.GetValue("nms");
        var rgx = new Regex(@".*-v(\d+)_\d+_\d+", RegexOptions.IgnoreCase).Match(val);
        if (rgx.Success) {
          return int.Parse(rgx.Groups[1].Value) == _nmsMajorVersion;
        }
      }
      return false;
    }
  }
}