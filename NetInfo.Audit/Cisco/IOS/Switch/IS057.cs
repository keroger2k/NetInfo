﻿using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Ensure ACL99 is properly applied to all virtual terminal (VTY) lines per the hardening script
  /// </summary>
  public class IS057 : ISTIGItem {

    public IDevice Device { get; private set; }

    public IS057(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var lines = ((INMCIIOSDevice)Device).Lines;
      var vtys = lines.Where(c => c.Type == LineType.VTY);
      var accessClassRegex = new Regex(@"\s*access-class (\d+) in$", RegexOptions.IgnoreCase);

      foreach (var line in vtys) {
        if (!line.Commands.Any(c => accessClassRegex.Match(c).Success)) {
          return false;
        }
        var l = line.Commands.SingleOrDefault(c => accessClassRegex.Match(c).Success);
        return new string[] { "97", "98", "99" }.Contains(accessClassRegex.Match(l).Groups[1].Value);
      }

      return true;
    }
  }
}