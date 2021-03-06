﻿using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure this command does NOT exist: "ip identd"
  /// </summary>
  public class IS030 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS030(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return !device.IPSettings.Identd;
    }
  }
}