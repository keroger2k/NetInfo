using System.Collections.Generic;
using NetInfo.Devices.Brocade.BOS.Classes;
using NetInfo.Devices.Brocade.BOS.Classes.Commands;
using NetInfo.Devices.Brocade.BOS.Commands;

namespace NetInfo.Devices.Brocade.BOS {

  public interface IBOSDevice : IDevice {

    int ConsoleTimeOut { get; }

    Password SuperPassword { get; }

    GlobalDot1x GlobalDot1xSettings { get; }

    IEnumerable<string> Banner { get; }

    IPSSHSettings SSH { get; }

    IEnumerable<RadiusServer> RadiusServers { get; }

    TacacsSettings TacacsServer { get; }

    SNMPSettings SNMP { get; }

    IEnumerable<Vlan> Vlans { get; }

    IEnumerable<BOSInterface> Interfaces { get; }

    WebManagement WebManagement { get; }

    bool PasswordEncryption { get; }

    bool TelnetServer { get; }

    SNTPSettings SNTP { get; }

    WriteMem WriteMem { get; }

    AAASettings AAA { get; }

    UserSettings UserSettings { get; }

    LoggingSettings LoggingSettings { get; }

    AliasSettings AliasSettings { get; }

    IEnumerable<ExtendedAccessList> ExtendedAccessLists { get; }

    IEnumerable<StandardAccessList> StandardAccessLists { get; }

    ShowInterfaceBrief ShowInterfaceBrief { get; }

    ShowInterface ShowInterface { get; }

    ShowSnmpUser ShowSnmpUser { get; }

    ShowDot1x ShowDot1xResults { get; }

    ShowVersion ShowVersion { get; }

    ShowVlan ShowVlan { get; }
  }
}