﻿using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure TACACS is configured for VTY access authentication (see comment)
  ///
  /// required commands:
  ///
  ///  aaa authentication login default group tacacs+ enable
  ///  aaa authentication enable default group tacacs+ enable
  /// </summary>
  public class IS055 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS055(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return device.AAA.Authentication.LoginGroupTacacsEnable && device.AAA.Authentication.EnableGroupTacacsEnable;
    }
  }
}