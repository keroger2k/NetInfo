using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate there are no manually configured spanning tree values
  /// </summary>
  public class IS090 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS090(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.SpanningTree.Count == 0;
    }
  }
}