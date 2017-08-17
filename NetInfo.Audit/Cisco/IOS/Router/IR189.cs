using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.ExtensionMethods;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists and uses the correct source IP address: "ip radius source-interface"
  ///
  /// If a loopback interface exists on the device, it MUST BE USED as the source-interface.
  /// If a loopback interface does not exist, use the Vlan99 interface
  /// If the Vlan99 interface does not exist or has an up/down status, use the Vlan30 interface.
  /// If a device is a DSL solution require BVI to be used.
  /// </summary>
  public class IR189 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR189(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      Regex sourceInterfaceRegex = device.SourceInterfaceRegex();
      return sourceInterfaceRegex != null &&
        device.IPSettings.RadiusSourceInterface != null &&
        sourceInterfaceRegex.Match(device.IPSettings.RadiusSourceInterface).Success;
    }
  }
}