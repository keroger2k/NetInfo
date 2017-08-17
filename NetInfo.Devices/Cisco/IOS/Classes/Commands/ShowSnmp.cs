using System.Collections.Generic;
using System.Linq;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowSnmp : BaseSetting {
    private readonly bool SNMP_ENABLED;

    public bool Enabled {
      get { return SNMP_ENABLED; }
    }

    public ShowSnmp(IEnumerable<string> settings) {
      Settings = settings;
      this.SNMP_ENABLED = !Settings.Contains("%SNMP agent not enabled");
    }
  }
}