﻿using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Shutdown "notconnected" ports unless they are assigned to a SF, user, COI or printer vlan
  /// </summary>
  public class IS096 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS096(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var result = device.ShowInterfaceStatus.Interfaces.Where(c => c.Status == ShowInterfaceStatus.InterfaceStatus.notconnect ||
        c.Status == ShowInterfaceStatus.InterfaceStatus.notconnected);
      return result.All(c => !string.IsNullOrEmpty(c.Description));
    }
  }
}