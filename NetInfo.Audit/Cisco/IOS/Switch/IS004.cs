using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the correct IOS version is being used
  /// </summary>
  public class IS004 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedImages;

    public IS004(INMCIIOSDevice device, IEnumerable<string> approvedImages) {
      this.Device = device;
      this._approvedImages = approvedImages;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return _approvedImages.Any() && _approvedImages.Contains(device.ShowVersion.ImageFileName);
    }
  }
}