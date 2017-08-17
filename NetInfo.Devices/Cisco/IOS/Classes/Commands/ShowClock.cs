using System.Collections.Generic;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowClock : BaseSetting {

    public ShowClock(IEnumerable<string> settings) {
      Settings = settings;
    }
  }
}