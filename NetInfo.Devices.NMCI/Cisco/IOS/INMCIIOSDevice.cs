using System.Collections.Generic;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.IOS;

namespace NetInfo.Devices.NMCI.Cisco.IOS {

  public interface INMCIIOSDevice : IIOSDevice {

    new Hostname Hostname { get; }

    new NMCISNMPSettings SNMPSettings { get; }

    new NMCIAliasExecSettings AliasExecSettings { get; }

    IEnumerable<string> TestScriptHeader { get; }
  }
}