using NetInfo.Devices.McAfee;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.McAfee {

  [TestFixture]
  public class McAfeeDeviceTests {

    [Test]
    public void should_correct_parse_manage_config_section_of_configuration() {
      var config = new AssetBlob {
        Body = @"[Sensor Network Config]
IP Address : 10.24.192.110
Netmask : 255.255.255.224
Default Gateway : 10.24.192.97
SSH Remote Logins : enabled

[Manager Config]
Manager IP addr : 1.1.1.1 (primary intf)
Install TCP Port : 1111
Alert TCP Port : 1111
Logging TCP Port : 1111

[Peer Manager Config]
Manager IP addr : 10.0.7.45 (primary intf)
Install TCP Port : 8501
Alert TCP Port : 8502
Logging TCP Port : 8503"
      };

      IMcafeeDevice device = new McAfeeDevice(config);
      Assert.AreEqual("1.1.1.1", device.ManagerConfig.Address.ToString());
      Assert.AreEqual(1111, device.ManagerConfig.AlertTcpPort);
      Assert.AreEqual(1111, device.ManagerConfig.InstallTcpPort);
      Assert.AreEqual(1111, device.ManagerConfig.LoggingTcpPort);
    }

    [Test]
    public void should_correct_parse_peer_manage_config_section_of_configuration() {
      var config = new AssetBlob {
        Body = @"
[Peer Manager Config]
Manager IP addr : 1.1.1.1 (primary intf)
Install TCP Port : 1111
Alert TCP Port : 1111
Logging TCP Port : 1111

"
      };

      IMcafeeDevice device = new McAfeeDevice(config);
      Assert.AreEqual("1.1.1.1", device.PeerManagerConfig.Address.ToString());
      Assert.AreEqual(1111, device.PeerManagerConfig.AlertTcpPort);
      Assert.AreEqual(1111, device.PeerManagerConfig.InstallTcpPort);
      Assert.AreEqual(1111, device.PeerManagerConfig.LoggingTcpPort);
    }
  }
}