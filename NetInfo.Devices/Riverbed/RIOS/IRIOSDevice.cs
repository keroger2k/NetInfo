using System.Collections.Generic;
using NetInfo.Devices.Riverbed.RIOS.Commands;

namespace NetInfo.Devices.Riverbed.RIOS {

  public interface IRIOSDevice : IDevice {

    string GetClock();

    string Model { get; }

    IEnumerable<string> Banner { get; }

    bool OptimizationServiceEnabled { get; }

    string Status { get; }

    NTPServerSettings NTP { get; }

    SNMPSettings SNMP { get; }

    SSHSettings SSH { get; }

    JobSettings JobSettings { get; }

    UserSettings UserSettings { get; }

    WebSettings Web { get; }

    TacacsSettings Tacacs { get; }

    AAASettings AAA { get; }

    ShowBoot ShowBoot { get; }

    ShowVersion ShowVersion { get; }

    ShowInfo ShowInfo { get; }

    ShowInterfaceBrief ShowInterfaceBrief { get; }
  }
}