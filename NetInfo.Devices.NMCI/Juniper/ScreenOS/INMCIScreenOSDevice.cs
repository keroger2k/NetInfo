using System.Collections.Generic;
using NetInfo.Devices.Juniper.ScreenOS;

namespace NetInfo.Devices.NMCI.Juniper.ScreenOS {

  public interface INMCIScreenOSDevice : IScreenOSDevice {

    new Hostname Hostname { get; }

    IEnumerable<string> TestScriptHeader { get; }
  }
}