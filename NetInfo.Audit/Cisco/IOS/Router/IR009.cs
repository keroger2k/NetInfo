using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.ExtensionMethods;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists and uses the correct source IP address: "ntp source" (see comment)
  ///
  /// If a loopback interface exists on the device, it MUST BE USED as the source-interface.
  /// If a loopback interface does not exist, use the Vlan99 interface
  /// If the Vlan99 interface does not exist or has an up/down status, use the Vlan30 interface.
  /// If a device is a DSL solution require BVI to be used.
  /// </summary>
  public class IR009 : ISTIGItem {

    public IDevice Device { get; private set; }
    private Regex sourceInterfaceRegex;

    public IR009(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      this.sourceInterfaceRegex = device.SourceInterfaceRegex();
      return this.sourceInterfaceRegex != null && this.sourceInterfaceRegex.Match(device.NetworkTimeProtocol.SourceVlan).Success;
    }

    public override string ToString() {
      string message = string.Empty;
      var device = (INMCIIOSDevice)Device;

      if (this.Compliant()) {
        message = "Passing";
      } else {
        if (this.sourceInterfaceRegex != null) {
          message = string.Format("Was expecting source interface to match '{0}' regex. Actual source interface '{1}'.",
            this.sourceInterfaceRegex.ToString(),
            device.IPSettings.TacacsSourceInterface);
        } else {
          message = "Unable to determine source interface regex for comparison.";
        }
      }
      return message;
    }
  }
}