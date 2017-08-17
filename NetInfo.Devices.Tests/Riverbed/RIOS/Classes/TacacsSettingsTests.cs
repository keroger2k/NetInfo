using System.Linq;
using NetInfo.Devices.Riverbed.RIOS;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS.Classes {

  [TestFixture]
  public class TacacsSettingsTests {

    [Test]
    public void tacacs_object_with_no_settings_will_return_no_host_servers() {
      var t = new TacacsSettings();
      t.Settings = @"".ToConfig();

      Assert.AreEqual(0, t.Hosts.Count());
    }

    [Test]
    public void tacacs_object_will_correctly_parse_host_servers() {
      var t = new TacacsSettings();
      t.Settings = @"
 tacacs-server host 10.32.9.233 timeout 20 retransmit 1
 tacacs-server host 10.32.9.233 key 7 ""k2ZwBUYw/xQ7CYB7vNozoVWpZsxtddKD""
 tacacs-server host 10.16.27.44 timeout 20 retransmit 1
 tacacs-server host 10.16.27.44 key 7 ""QgAxGn/EF/Ra+hlRvL7B1g/gZBJwr06m""
".ToConfig();

      Assert.AreEqual(2, t.Hosts.Count());
    }

    [Test]
    public void tacacs_object_will_correctly_parse_host_servers_with_this_version_syntax() {
      var t = new TacacsSettings();
      t.Settings = @"
 tacacs-server host 10.32.9.233
 tacacs-server host 10.0.16.152
 tacacs-server host 10.16.27.44
".ToConfig();

      Assert.AreEqual(3, t.Hosts.Count());
    }
  }
}