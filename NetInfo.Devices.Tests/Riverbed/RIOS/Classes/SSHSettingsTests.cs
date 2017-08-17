using NetInfo.Devices.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS.Classes {

  [TestFixture]
  public class SSHSettingsTests {

    [Test]
    public void smtp_object_with_no_settings_should_return_zero_servers() {
      var ssh = new SSHSettings();
      ssh.Settings = new string[] { };

      Assert.IsFalse(ssh.V2OnlyEnable);
      Assert.IsFalse(ssh.ServerListenEnable);
    }

    [Test]
    public void smtp_object_should_correcly_parse_server_addresses() {
      var ssh = new SSHSettings();
      ssh.Settings = new string[] {
        " ssh server v2-only enable"
      };

      Assert.IsTrue(ssh.V2OnlyEnable);
    }

    [Test]
    public void smtp_object_should_correcly_parse_server_listen_enable() {
      var ssh = new SSHSettings();
      ssh.Settings = new string[] {
        " ssh server listen enable"
      };

      Assert.IsTrue(ssh.ServerListenEnable);
    }
  }
}