using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class NetworkTimeProtocolTests {

    [Test]
    public void should_return_default_values_when_no_settings_are_found() {
      var ntp = new NTPSettings();

      Assert.AreEqual(0, ntp.Servers.Count());
      Assert.AreEqual("", ntp.SourceVlan);
    }

    [Test]
    public void should_return_correct_source_vlan() {
      var ntp = new NTPSettings();
      ntp.Settings = @"ntp source Vlan30
ntp server 1.1.1.1".ToConfig();

      Assert.AreEqual(1, ntp.Servers.Count());
      Assert.AreEqual("Vlan30", ntp.SourceVlan);
    }

    [Test]
    public void should_return_correct_source_vlan_when_sub_interfaces_are_utilized() {
      var ntp = new NTPSettings();
      ntp.Settings = @"ntp trusted-key 1
ntp source GigabitEthernet0/0.929".ToConfig();

      Assert.AreEqual("GigabitEthernet0/0.929", ntp.SourceVlan);
    }

    [Test]
    public void should_return_ntp_servers_with_keys_configured() {
      var ntp = new NTPSettings();
      ntp.Settings = @"ntp server 10.32.9.254 key 1
ntp server 10.0.16.10 key 1
ntp server 10.16.27.68 key 1
".ToConfig();
      Assert.AreEqual(3, ntp.Servers.Count());
    }

    [Test]
    public void should_return_ntp_servers_without_keys_configured() {
      var ntp = new NTPSettings();
      ntp.Settings = @"ntp server 10.32.9.254
ntp server 10.0.16.10
ntp server 10.16.27.68
".ToConfig();
      Assert.AreEqual(3, ntp.Servers.Count());
    }

    [Test]
    public void should_return_null_when_key_value_is_not_set_on_ntp_server_setting() {
      var ntp = new NTPSettings();
      ntp.Settings = @"ntp server 10.32.9.254".ToConfig();
      Assert.Null(ntp.Servers.ElementAt(0).Key);
    }

    [Test]
    public void should_return_ntp_servers_with_keys_configured_and_their_key_value() {
      var ntp = new NTPSettings();
      ntp.Settings = @"ntp server 10.32.9.254 key 1".ToConfig();
      Assert.AreEqual(1, ntp.Servers.ElementAt(0).Key.Value);
    }

    [Test]
    public void should_correctly_parse_authentication_keys() {
      var ntp = new NTPSettings();
      ntp.Settings = @"ntp authentication-key 1 md5 150B434543231A273D7D073B0728432C 7
ntp authentication-key 2 md5 150B434543231A273D7D073B0728432C 7".ToConfig();
      Assert.AreEqual(2, ntp.Keys.Count());
      Assert.AreEqual(1, ntp.Keys.ElementAt(0).Number);
      Assert.AreEqual(2, ntp.Keys.ElementAt(1).Number);
    }
  }
}