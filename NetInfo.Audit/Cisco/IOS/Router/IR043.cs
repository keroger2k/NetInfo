﻿using System.Text.RegularExpressions;
using NetInfo.Devices.NMCI.Infrastructure.ExtensionMethods;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists and uses the correct source IP address: "logging source-interface" (see comment)
  ///
  /// If a loopback interface exists on the device, it MUST BE USED as the source-interface.
  /// If a loopback interface does not exist, use the Vlan99 interface
  /// If the Vlan99 interface does not exist or has an up/down status, use the Vlan30 interface.
  /// </summary>
  public class IR043 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IR043(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      Regex sourceInterfaceRegex = device.SourceInterfaceRegex();
      return sourceInterfaceRegex != null &&
        device.SyslogSettings.SourceInterface != null &&
        sourceInterfaceRegex.Match(device.SyslogSettings.SourceInterface).Success;
    }
  }
}