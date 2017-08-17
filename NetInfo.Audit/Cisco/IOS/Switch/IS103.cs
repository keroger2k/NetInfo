using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Switch {

  /// <summary>
  /// Validate TACACS key value matches NMS script
  /// </summary>
  public class IS103 : ISTIGItem {

    public IDevice Device { get; private set; }

    private IEnumerable<string> _approvedKeys;

    public IS103(INMCIIOSDevice device, IEnumerable<string> approvedKeys) {
      this.Device = device;
      this._approvedKeys = approvedKeys;
    }

    public bool Compliant() {
      var device = (INMCIIOSDevice)Device;
      return this._approvedKeys.Contains(device.TacacsServer.DeCryptedKey);
    }
  }
}