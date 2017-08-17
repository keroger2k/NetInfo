using System.Collections.Generic;

namespace NetInfo.Devices.McAfee {

  public interface IMcafeeDevice : IDevice {

    string MgmtLinkStatus { get; }

    int ConsoleTimeout { get; }

    bool AuxPortEnabled { get; }

    bool AuditLoggingEnabled { get; }

    int SSHInactiveTimeout { get; }

    bool SSHAccessControl { get; }

    IEnumerable<McAfeeDevice.Network> SSHAccessControlNetworkList { get; }

    ManagerConfig ManagerConfig { get; }

    PeerManagerConfig PeerManagerConfig { get; }

    SensorNetworkConfig SensorNetworkConfig { get; }

    SensorInfoConfig SensorInfoConfig { get; }
  }
}