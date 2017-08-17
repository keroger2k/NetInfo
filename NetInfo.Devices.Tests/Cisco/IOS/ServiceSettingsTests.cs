using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS.Classes {

  [TestFixture]
  public class ServiceSettingsTests {

    [Test]
    public void service_settings_object_with_no_settings_should_default_results() {
      var serviceSettings = new ServiceSettings();

      Assert.False(serviceSettings.Pad);
      Assert.False(serviceSettings.TcpKeepalivesIn);
      Assert.False(serviceSettings.TcpKeepalivesOut);
      Assert.False(serviceSettings.TcpSmallServers);
      Assert.False(serviceSettings.UdpSmallServers);
      Assert.False(serviceSettings.PasswordEncryption);
    }

    [Test]
    public void service_settings_object_with_all_settings_enabled_returns_true_for_all() {
      var serviceSettings = new ServiceSettings();
      serviceSettings.Settings = new string[] {
        @"service pad",
        @"service tcp-keepalives-in",
@"service tcp-keepalives-out",
@"service udp-small-servers",
@"service tcp-small-servers",
@"service password-encryption",};

      Assert.True(serviceSettings.Pad);
      Assert.True(serviceSettings.TcpKeepalivesIn);
      Assert.True(serviceSettings.TcpKeepalivesOut);
      Assert.True(serviceSettings.TcpSmallServers);
      Assert.True(serviceSettings.UdpSmallServers);
      Assert.True(serviceSettings.PasswordEncryption);
    }
  }
}