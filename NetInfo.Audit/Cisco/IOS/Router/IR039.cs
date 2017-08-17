﻿using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command does NOT exist on all layer 3 IP interfaces: "ip directed-broadcast"
  /// </summary>
  public class IR039 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR039(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.Interfaces
        .Where(c => !c.Shutdown && c.Address != null)
        .All(c => !c.IP.DirectedBroadcast);
    }
  }
}