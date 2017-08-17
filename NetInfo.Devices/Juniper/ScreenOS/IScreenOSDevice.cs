using System.Collections.Generic;
using NetInfo.Devices.Juniper.ScreenOS.Commands;

namespace NetInfo.Devices.Juniper.ScreenOS {

  public interface IScreenOSDevice : IDevice {

    IEnumerable<ScreenOSInterface> Interfaces { get; }

    string GetClockTimezone();

    string AutherServer { get; }

    bool SCSEnabled { get; }

    bool SSHEnabled { get; }

    bool AutherServerLocalId { get; }

    bool AutherServerName { get; }

    bool UseInterfaceConfigPort80 { get; }

    bool NetworkTimeEnabled { get; }

    AdminSettings AdminSettings { get; }

    InterfaceSettings InterfaceSettings { get; }

    IKESettings IKESettings { get; }

    PKISettings PKISettings { get; }

    XAuthSettings XAuthSettings { get; }

    ClockSettings ClockSettings { get; }

    SNMPSettings SNMPSettings { get; }

    FlowSettings FlowSettings { get; }

    SSLSettings SSLSettings { get; }

    ConsoleSettings ConsoleSettings { get; }

    AlgSettings AlgSettings { get; }

    NTPSettings NTP { get; }

    GetRoute GetRoute { get; }

    GetLicense GetLicense { get; }
  }
}