using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate all references to Riverbed are removed from the config
  /// </summary>
  public class RB031 : ISTIGItem {

    public IDevice Device { get; private set; }

    public RB031(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      return Device.AssetBlob.Configuration.Any(c => c.Contains("Riverbed"));
    }
  }
}