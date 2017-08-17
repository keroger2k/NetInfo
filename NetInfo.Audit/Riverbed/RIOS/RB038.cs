using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate the correct RIOS is installed on all partitions
  /// </summary>
  public class RB038 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedImages;

    public RB038(INMCIRIOSDevice device, IEnumerable<string> approvedImages) {
      this.Device = device;
      this._approvedImages = approvedImages;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return _approvedImages.Any() &&
        device.ShowBoot.Images.Select(c => c.Name).All(c => _approvedImages.Contains(c));
    }
  }
}