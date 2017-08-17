using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the correct IOS version is being used
  /// </summary>
  public class IR004 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _approvedImages;

    public IR004(INMCIIOSDevice device, IEnumerable<string> approvedImages) {
      this.Device = device;
      this._approvedImages = approvedImages;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return _approvedImages.Any() && _approvedImages.Contains(device.ShowVersion.ImageFileName);
    }

    public override string ToString() {
      string message = string.Empty;
      if (this.Compliant()) {
        message = "Passing: Device is running approved image.";
      } else {
        var device = (INMCIIOSDevice)Device;
        message = string.Format("Failing: Device has {0}; Approved images are {1}.", device.ShowVersion.ImageFileName, string.Join(", ", this._approvedImages));
      }
      return message;
    }
  }
}