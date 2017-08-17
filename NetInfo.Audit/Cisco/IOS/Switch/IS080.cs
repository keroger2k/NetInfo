using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Remove any config files saved to slot0: or disk0:
  /// </summary>
  public class IS080 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly Regex rgxFile = new Regex(@".*\.bin", RegexOptions.IgnoreCase);

    public IS080(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      List<string> files = new List<string>();
      if (device.DirAllFileSystems.Disk0 != null) {
        files.AddRange(device.DirAllFileSystems.Disk0.Select(c => c.Name));
      }
      if (device.DirAllFileSystems.Slot0 != null) {
        files.AddRange(device.DirAllFileSystems.Slot0.Select(c => c.Name));
      }
      return files.All(c => rgxFile.Match(c).Success);
    }
  }
}