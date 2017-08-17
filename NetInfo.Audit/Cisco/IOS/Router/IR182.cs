using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the console password matches the current hardening script.
  /// </summary>
  public class IR182 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> consolePasswords;

    public IR182(INMCIIOSDevice device, IEnumerable<string> consolePasswords) {
      this.Device = device;
      this.consolePasswords = consolePasswords;
    }

    public bool Compliant() {
      var console = ((INMCIIOSDevice)Device).Lines.SingleOrDefault(c => c.Type == LineType.CONSOLE);
      return console.Password != null && consolePasswords.Contains(console.Password.Value);
    }
  }
}