using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Printer VLAN ACL is the correct major version
  /// </summary>
  public class IR101 : ISTIGItem {
    public static readonly Regex MajorInVersionRegex = new Regex(@"NMCI_Printer_VLAN_ACL_IN_(V\d+)", RegexOptions.IgnoreCase);
    public static readonly Regex MajorOutVersionRegex = new Regex(@"NMCI_Printer_VLAN_ACL_OUT_(V\d+)", RegexOptions.IgnoreCase);

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> _inbound;
    private readonly IEnumerable<string> _outbound;

    public IR101(INMCIIOSDevice device, IEnumerable<string> inbound, IEnumerable<string> outbound) {
      this.Device = device;
      _inbound = inbound;
      _outbound = outbound;
    }

    public bool Compliant() {
      var device = ((INMCIIOSDevice)Device);
      var inter = device.Interfaces.Where(c => !c.Physical && !c.Shutdown && c.Vlan >= 500 && c.Vlan <= 599);
      var r1 = inter
        .Any(c => c.AccessGroups
          .Where(d => d.Direction.Equals("in", StringComparison.InvariantCultureIgnoreCase))
            .All(e => _inbound.Contains(MajorInVersionRegex.Match(e.Name).Groups[1].Value)));
      var r2 = inter
        .Any(c => c.AccessGroups
          .Where(d => d.Direction.Equals("out", StringComparison.InvariantCultureIgnoreCase))
            .All(e => _outbound.Contains(MajorOutVersionRegex.Match(e.Name).Groups[1].Value)));
      return r1 && r2;
    }
  }
}