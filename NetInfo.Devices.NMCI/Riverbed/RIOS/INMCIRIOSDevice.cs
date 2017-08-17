using System.Collections.Generic;
using NetInfo.Devices.Riverbed.RIOS;

namespace NetInfo.Devices.NMCI.Riverbed.RIOS {

  public interface INMCIRIOSDevice : IRIOSDevice {

    new Hostname Hostname { get; }

    new NMCISNMPSettings SNMP { get; }

    IEnumerable<string> TestScriptHeader { get; }
  }
}