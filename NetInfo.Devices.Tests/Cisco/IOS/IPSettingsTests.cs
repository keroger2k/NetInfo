using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class IPSettingsTests {

    [Test]
    public void ip_settings_returns_false_when_ip_http_server_is_not_found() {
      var settings = new IPSettings();

      Assert.False(settings.HttpServer);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_http_server_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip http-server" };

      Assert.False(ipSettings.HttpServer);
    }

    [Test]
    public void ip_settings_returns_true_when_ip_http_server_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip http-server" };

      Assert.True(ipSettings.HttpServer);
    }

    [Test]
    public void ip_settings_returns_false_when_ip_finger_is_not_found() {
      var ipSettings = new IPSettings();

      Assert.False(ipSettings.Finger);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_finger_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip finger" };

      Assert.False(ipSettings.Finger);
    }

    [Test]
    public void ip_settings_returns_true_when_ip_finger_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip finger" };

      Assert.True(ipSettings.Finger);
    }

    [Test]
    public void ip_settings_returns_false_when_ip_identd_is_not_found() {
      var ipSettings = new IPSettings();

      Assert.False(ipSettings.Identd);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_identd_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip identd" };

      Assert.False(ipSettings.Identd);
    }

    [Test]
    public void ip_settings_returns_true_when_ip_identd_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip identd" };

      Assert.True(ipSettings.Identd);
    }

    [Test]
    public void ip_settings_returns_false_when_ip_subnet_zero_is_not_found() {
      var ipSettings = new IPSettings();

      Assert.False(ipSettings.SubnetZero);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_subnet_zero_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip subnet-zero" };

      Assert.False(ipSettings.SubnetZero);
    }

    [Test]
    public void ip_settings_returns_true_when_ip_subnet_zero_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip subnet-zero" };

      Assert.True(ipSettings.SubnetZero);
    }

    [Test]
    public void ip_settings_returns_false_when_ip_source_route_is_not_found() {
      var ipSettings = new IPSettings();

      Assert.False(ipSettings.SourceRoute);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_source_route_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip source-route" };

      Assert.False(ipSettings.SourceRoute);
    }

    [Test]
    public void ip_settings_returns_true_when_ip_source_route_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip source-route" };

      Assert.True(ipSettings.SourceRoute);
    }

    [Test]
    public void ip_settings_returns_false_when_ip_routing_is_not_found() {
      var ipSettings = new IPSettings();

      Assert.False(ipSettings.Routing);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_routing_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip routing" };

      Assert.False(ipSettings.Routing);
    }

    [Test]
    public void ip_settings_returns_true_when_ip_routing_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip routing" };

      Assert.True(ipSettings.Routing);
    }

    [Test]
    public void ip_settings_returns_false_when_ip_gratuitous_arps_is_not_found() {
      var ipSettings = new IPSettings();

      Assert.False(ipSettings.GratuitousArps);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_gratuitous_arps_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip gratuitous-arps" };

      Assert.False(ipSettings.GratuitousArps);
    }

    [Test]
    public void ip_settings_returns_true_when_ip_gratuitous_arps_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip gratuitous-arps" };

      Assert.True(ipSettings.GratuitousArps);
    }

    [Test]
    public void ip_settings_returns_false_when_ip_domain_lookup_is_not_found() {
      var ipSettings = new IPSettings();

      Assert.False(ipSettings.DomainLookup);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_domain_lookup_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip domain-lookup" };

      Assert.False(ipSettings.DomainLookup);
    }

    [Test]
    public void ip_settings_returns_true_when_domain_lookup_is_found() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip domain-lookup" };

      Assert.True(ipSettings.DomainLookup);
    }

    [Test]
    public void ip_settings_returns_false_when_no_ip_domain_lookup_is_found_witout_hyphen() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "no ip domain lookup" };

      Assert.False(ipSettings.DomainLookup);
    }

    [Test]
    public void ip_settings_returns_true_when_domain_lookup_is_found_without_hyphen() {
      var ipSettings = new IPSettings();
      ipSettings.Settings = new string[] { "ip domain lookup" };

      Assert.True(ipSettings.DomainLookup);
    }
  }
}