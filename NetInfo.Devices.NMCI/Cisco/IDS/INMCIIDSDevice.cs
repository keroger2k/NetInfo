using System.Collections.Generic;
using NetInfo.Devices.IDS;

namespace NetInfo.Devices.NMCI.Cisco.IOS.IDS {

  public interface INMCIIDSDevice : IIDSDevice {

    IEnumerable<string> TestScriptHeader { get; }
  }
}