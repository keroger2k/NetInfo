using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate the latest major version NMS Script is applied
  /// </summary>
  public class RB020 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int _nmsMajorVersion;

    public RB020(INMCIRIOSDevice device, int nmsMajorVersion) {
      this._nmsMajorVersion = nmsMajorVersion;
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      var rgx = new Regex(@".*-v(?<number>\d+)_\d+_\d+", RegexOptions.IgnoreCase);
      var nmsJob = device.JobSettings.Jobs.FirstOrDefault(c => c.Name.Equals("NMS", System.StringComparison.OrdinalIgnoreCase));
      return nmsJob != null && int.Parse(rgx.Match(nmsJob.Comment).Groups["number"].Value) == _nmsMajorVersion;
    }
  }
}