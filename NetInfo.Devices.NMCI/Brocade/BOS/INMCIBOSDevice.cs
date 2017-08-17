using System.Collections.Generic;
using NetInfo.Devices.Brocade.BOS;

namespace NetInfo.Devices.NMCI.Brocade.BOS {

  public interface INMCIBOSDevice : IBOSDevice {

    new Hostname Hostname { get; }

    IEnumerable<string> TestScriptHeader { get; }
  }
}