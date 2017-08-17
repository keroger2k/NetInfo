using System.Collections.Generic;
using NetInfo.Devices.McAfee;

namespace NetInfo.Devices.NMCI.McAfee {

  public interface INMCIMcAfeeDevice : IMcafeeDevice {

    IEnumerable<string> TestScriptHeader { get; }

    new Hostname Hostname { get; }
  }
}