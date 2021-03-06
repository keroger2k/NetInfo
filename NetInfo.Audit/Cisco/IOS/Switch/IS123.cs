﻿using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure the following commands exist: "ntp authentication-key", "ntp authenticate" and "ntp trusted-key"
  /// </summary>
  public class IS123 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS123(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.NetworkTimeProtocol.Keys.Any() && device.NetworkTimeProtocol.TrustedKeys.Any() && device.NetworkTimeProtocol.Authenticate;
    }
  }
}