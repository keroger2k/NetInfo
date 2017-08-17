using System.Collections.Generic;
using NetInfo.Audit.NMCI.Models;
using NetInfo.Devices;
using NetInfo.Devices.Infrastructure.Helpers;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate assigned IP Address falls within VLAN 33 for Ext TB/C2 COOP/B1/B2/B3/STB devices
  ///
  /// SN30-34 = Ext Transport;
  /// SN35-39 = C2 COOP;
  /// SN80-89 = B1;
  /// SN90-99 = B2/B3/STB
  /// </summary>
  public class IP004 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<Subnet> _allowedAddresses;

    public IP004(INMCIMcAfeeDevice device, IEnumerable<Subnet> allowedAddresses) {
      this.Device = device;
      this._allowedAddresses = allowedAddresses;
    }

    public bool Compliant() {
      var device = (INMCIMcAfeeDevice)Device;
      bool inside = false;
      foreach (var item in _allowedAddresses) {
        if (IPHelper.ip_inside_range(string.Format("{0}/{1}",
                                        item.NetworkAddress.ToString(),
                                        item.NetworkMask.ToString()),
                      device.SensorNetworkConfig.Address.ToString())) {
          inside = true;
        }
      }
      return inside;
    }
  }
}