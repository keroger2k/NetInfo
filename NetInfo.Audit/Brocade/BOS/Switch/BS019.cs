using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Ensure the correct SW version is being used
  /// </summary>
  public class BS019 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<string> _approvedVersions;

    public BS019(INMCIBOSDevice device, IEnumerable<string> approvedVersions) {
      this.Device = device;
      this._approvedVersions = approvedVersions;
    }

    public bool Compliant() {
      var device = (INMCIBOSDevice)Device;
      var imagesNames = device.ShowVersion.Units.Select(c => c.ImageName.Trim()).Distinct();
      return imagesNames.Count() == 1 && _approvedVersions.Contains(string.Format("{0}.bin", imagesNames.FirstOrDefault()));
    }
  }
}