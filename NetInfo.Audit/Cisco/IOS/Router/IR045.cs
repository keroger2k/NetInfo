using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure "ip cef" is enabled globally and on each ip interface (see the "show ip interface" output)
  ///
  /// Exceptions: 
  ///   1. uB2 & uST Outer router, interfaces configured for IP NAT can retain the ""no ip mroute-cache"" and ""no ip route-cache commands; 
  ///   2. NMCI to IT021 NOC uB1 tunnel interface include the ""no ip mroute-cache"" and ""no ip route-cache commands.
  /// </summary>
  public class IR045 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR045(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.ShowIpInterface.Interfaces
        .Where(c => c.InternetAddress != null)
        .Where(c => !c.NetworkAddressTranslationEnabled)
        .All(c => c.IpCefSwitchingEanbled);
    }
  }
}