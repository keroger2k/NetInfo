﻿using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure this command exists and uses the correct source IP address: "snmp-server trap-source vlanXX" (see comment)
  ///
  /// Use the management vlan interface IP address as the source address.
  /// The permitted Vlans are Vlan23 (uTB), Vlan51 (uB2) and Vlan56 (uB1).
  ///
  /// Note:
  ///   The use of the ODMN management vlan IP address is permitted per an approved waiver.
  /// </summary>
  public class IR130 : ISTIGItem {

    public IDevice Device { get; private set; }

    private string[] approvedSources = new string[] { "Vlan23", "Vlan51", "Vlan56" };

    public IR130(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.SNMPSettings.TrapSource != null && approvedSources.Contains(device.SNMPSettings.TrapSource);
    }
  }
}