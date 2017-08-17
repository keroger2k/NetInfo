using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Ensure the AUX password matches the current hardening script.
  /// </summary>
  public class IR183 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<string> auxPasswords;

    public IR183(INMCIIOSDevice device, IEnumerable<string> consolePasswords) {
      this.Device = device;
      this.auxPasswords = consolePasswords;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var aux = device.Lines.SingleOrDefault(c => c.Type == LineType.AUX);
      return aux.Password != null && auxPasswords.Contains(aux.Password.Value);
    }
  }
}