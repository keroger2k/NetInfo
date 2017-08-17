using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure the correct RiOS version is being used
  /// </summary>
  public class RB003 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedImages;

    public RB003(INMCIRIOSDevice device, IEnumerable<string> approvedImages) {
      this.Device = device;
      this._approvedImages = approvedImages;
    }

    public bool Compliant() {
      var device = (INMCIRIOSDevice)Device;
      return _approvedImages.Any() &&
        _approvedImages.Any(c =>
          device.ShowBoot.Images
          .Select(d => d.RawImageCommand)
          .Contains(c));
    }
  }
}