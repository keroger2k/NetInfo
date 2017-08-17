using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate TACACS key value matches NMS script
  /// </summary>
  public class IR129 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<string> _approvedKeys;

    public IR129(INMCIIOSDevice device, IEnumerable<string> approvedKeys) {
      this.Device = device;
      this._approvedKeys = approvedKeys;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return this._approvedKeys.Contains(device.TacacsServer.DeCryptedKey);
    }
  }
}