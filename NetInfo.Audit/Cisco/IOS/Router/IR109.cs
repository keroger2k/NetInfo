using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate "monitor session" commands are correct (remove any incorrect commands)
  /// </summary>
  public class IR109 : ISTIGItem {

    public IDevice Device { get; private set; }

    private readonly IEnumerable<Regex> _rgxCommands;

    public IR109(INMCIIOSDevice device, IEnumerable<Regex> rgxCommands) {
      this.Device = device;
      this._rgxCommands = rgxCommands;
    }

    public bool Compliant() {
      if (_rgxCommands == null) { return false; }
      var device = (INMCIIOSDevice)Device;
      for (int i = 0; i < device.MonitorSettings.Commands.Count(); i++) {
        var rgx = _rgxCommands.ElementAt(i);
        var line = device.MonitorSettings.Commands.ElementAt(i).Command;
        if (!rgx.Match(line).Success) {
          return false;
        }
      }
      return true;
    }
  }
}