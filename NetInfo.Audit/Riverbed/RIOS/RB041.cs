using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate the major version of the applied hardening script is applied
  /// </summary>
  public class RB041 : ISTIGItem {

    public IDevice Device { get; private set; }

    private int _hardeningMajorVersion;

    public RB041(INMCIRIOSDevice device, int hardeningMajorVersion) {
      this._hardeningMajorVersion = hardeningMajorVersion;
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      var rgx = new Regex(@".*_v(?<number>\d+)_\d+_\d+", RegexOptions.IgnoreCase);
      var nmsJob = device.JobSettings.Jobs.FirstOrDefault(c => c.Name.Equals("Hardening", System.StringComparison.OrdinalIgnoreCase));
      return nmsJob != null && int.Parse(rgx.Match(nmsJob.Comment).Groups["number"].Value) == _hardeningMajorVersion;
    }
  }
}