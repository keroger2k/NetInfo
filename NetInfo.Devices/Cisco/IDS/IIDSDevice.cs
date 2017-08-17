using NetInfo.Devices.Cisco.IDS.Commands;

namespace NetInfo.Devices.IDS {

  public interface IIDSDevice : IDevice {

    string Timezone { get; }

    int TimezoneOffset { get; }

    bool VirtualSensorVS0 { get; }

    bool TelnetOptionDisabled { get; }

    ShowUsersAll ShowUsersAll { get; }
  }
}