using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure all eBGP neighbor relationships are configured with unique MD5 keys when BGP is configured
  ///
  /// Exception:
  ///   The URB router templates do not include a BGP password for the UUNet link.
  /// </summary>
  public class IR003 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR003(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.IsBGPConfigured &&
        device.BGP.Neighbors.Count() == device.BGP.Neighbors.Select(c => c.Password).Distinct().Count();
    }

    public override string ToString() {
      var message = string.Empty;
      if (this.Compliant()) {
        message = "Passing: All eBGP neighbor relationships are configued with unique MD5 keys.";
      } else {
        var device = (INMCIIOSDevice)Device;
        var z = device.BGP.Neighbors.Where(c => string.IsNullOrEmpty(c.Password));
        message = string.Format("Failing: Neighbors without passwords {0}",
          string.Join(", ", z.Select(c => c.Address.ToString())));
      }
      return message;
    }
  }
}