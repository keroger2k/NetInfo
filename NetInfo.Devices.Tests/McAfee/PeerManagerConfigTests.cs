using NetInfo.Devices.McAfee;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.McAfee {

  [TestFixture]
  public class PeerManagerConfigTests {

    [Test]
    public void should_correctly_return_default_values_when_no_settings_are_found() {
      var managerConfig = new PeerManagerConfig();
      managerConfig.Settings = new string[] { };

      Assert.AreEqual("0.0.0.0", managerConfig.Address.ToString());
      Assert.AreEqual(0, managerConfig.AlertTcpPort);
      Assert.AreEqual(0, managerConfig.InstallTcpPort);
      Assert.AreEqual(0, managerConfig.LoggingTcpPort);
    }

    [Test]
    public void should_correctly_parse_manager_ip_address() {
      var managerConfig = new PeerManagerConfig();

      managerConfig.Settings = new string[] {
        @"Manager IP addr : 1.1.1.1 (primary intf)"
      };

      Assert.AreEqual("1.1.1.1", managerConfig.Address.ToString());
    }

    [Test]
    public void should_correctly_parse_manager_install_tcp_port() {
      var managerConfig = new PeerManagerConfig();
      managerConfig.Settings = new string[] {
        @"Install TCP Port : 1111"
      };

      Assert.AreEqual("1111", managerConfig.InstallTcpPort.ToString());
    }

    [Test]
    public void should_correctly_parse_manager_alert_tcp_port() {
      var managerConfig = new PeerManagerConfig();
      managerConfig.Settings = new string[] {
        @"Alert TCP Port : 1111"
      };

      Assert.AreEqual("1111", managerConfig.AlertTcpPort.ToString());
    }

    [Test]
    public void should_correctly_parse_manager_loggin_tcp_port() {
      var managerConfig = new PeerManagerConfig();
      managerConfig.Settings = new string[] {
        @"Logging TCP Port : 1111"
      };

      Assert.AreEqual("1111", managerConfig.LoggingTcpPort.ToString());
    }

    [Test]
    public void should_correctly_return_all_values_when_they_are_found() {
      var managerConfig = new PeerManagerConfig();
      managerConfig.Settings = new string[] {
        @"Manager IP addr : 1.1.1.1 (primary intf)",
        @"Install TCP Port : 1111",
        @"Alert TCP Port : 1111",
        @"Logging TCP Port : 1111"
      };

      Assert.AreEqual("1.1.1.1", managerConfig.Address.ToString());
      Assert.AreEqual(1111, managerConfig.AlertTcpPort);
      Assert.AreEqual(1111, managerConfig.InstallTcpPort);
      Assert.AreEqual(1111, managerConfig.LoggingTcpPort);
    }
  }
}