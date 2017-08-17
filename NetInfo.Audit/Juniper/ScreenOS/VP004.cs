﻿using System;
using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate the user "PRLH-ADMIN" password is current and the privilege is set to "all"
  /// </summary>
  public class VP004 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> validNrfkPasswords;

    public VP004(INMCIScreenOSDevice device, IEnumerable<string> validNrfkPasswords) {
      this.Device = device;
      this.validNrfkPasswords = validNrfkPasswords;
    }

    public bool Compliant() {
      var device = (INMCIScreenOSDevice)Device;
      var pass = device.AdminSettings.Users
        .SingleOrDefault(c => c.Name.Equals("PRLH-ADMIN", StringComparison.OrdinalIgnoreCase));
      return validNrfkPasswords.Contains(pass.Password) && pass.Privilege.Equals("all", StringComparison.OrdinalIgnoreCase);
    }
  }
}