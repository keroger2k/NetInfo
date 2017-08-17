using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Audit.Models;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate the COINs, vBNS, and SIPR circuit ID's are included in the interface descriptions (see comment)
  ///
  /// Use NetCKT to confirm the circuit IDs are correct.
  /// </summary>
  public class IR080 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<CircuitInformation> _circuits;
    public static Regex DescriptionRegex = new Regex(@"<[=]+\s+.*\s+CircuitID=(.*)\s+[=]+>", RegexOptions.IgnoreCase);

    public IR080(INMCIIOSDevice device, IEnumerable<CircuitInformation> circuits) {
      this.Device = device;
      this._circuits = circuits;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      var activeCircuitsIds = device.Interfaces
        .Where(c => !c.Shutdown && DescriptionRegex.Match(c.Description).Success)
        .Select(c => DescriptionRegex.Match(c.Description).Groups[1].Value);
      var r = activeCircuitsIds.SelectMany(c => c.Split(','));
      var circuitList = _circuits.Select(c => c.CircuitId);
      foreach (var item in r) {
        if (!circuitList.Contains(item.Trim())) {
          return false;
        }
      }
      return true;
    }
  }
}