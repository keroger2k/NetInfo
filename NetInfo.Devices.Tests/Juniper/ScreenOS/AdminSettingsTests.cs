using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class AdminSettingsTests {
    private AdminSettings adminSettings;

    [SetUp]
    public void Init() {
      adminSettings = new AdminSettings();
      adminSettings.Settings = genericSettings;
    }

    private IEnumerable<string> genericSettings = @"
set admin format dos
set admin name ""NS-ADMIN""
set admin password nPPUKJr1JNbOcxBASspDn1DtcHJSrn
set admin user ""NRFK-ADMIN"" password ""nHKaAJrmMUcMc6BH2s/B3TEtysGKln"" privilege ""all""
set admin user ""PRLH-ADMIN"" password ""nEyzOJrdA0XJcNYNTsJDdgHtcEB7vn"" privilege ""all""
set admin user ""RD-ADMIN"" password ""nKFeHMrqASOBcUgOQsIDreKtJYFWSn"" privilege ""read-only""
set admin user ""SDNI-ADMIN"" password ""nBR2D5rYJigNcHtIjsuFRcGt02IXen"" privilege ""all""
set admin manager-ip 10.0.18.0 255.255.255.0
set admin manager-ip 10.16.6.0 255.255.255.0
set admin manager-ip 10.16.27.32 255.255.255.224
set admin manager-ip 10.0.16.128 255.255.255.224
set admin manager-ip 10.32.9.224 255.255.255.224
set admin manager-ip 138.162.0.0 255.254.0.0
set admin sys-location ""Bldg_7400_Floor_1_Room_MOD3_Rack_AC37_""
set admin mail server-name ""10.32.118.40""
set admin auth timeout 10
set admin auth server ""Local""".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void can_correctly_parse_admin_users() {
      Assert.AreEqual(4, adminSettings.Users.Count());
    }

    [Test]
    public void can_correctly_parse_admin_managers_ip() {
      Assert.AreEqual(6, adminSettings.ManagerAddresses.Count());
    }

    [Test]
    public void can_correctly_parse_format() {
      Assert.AreEqual("dos", adminSettings.Format);
    }

    [Test]
    public void can_correctly_parse_admin_name() {
      Assert.AreEqual("NS-ADMIN", adminSettings.Name);
    }

    [Test]
    public void can_correctly_parse_admin_password() {
      Assert.AreEqual("nPPUKJr1JNbOcxBASspDn1DtcHJSrn", adminSettings.Password);
    }

    [Test]
    public void can_correctly_parse_admin_sys_location() {
      Assert.AreEqual("Bldg_7400_Floor_1_Room_MOD3_Rack_AC37_", adminSettings.SysLocation);
    }

    [Test]
    public void can_correctly_parse_admin_mail_server_name() {
      Assert.AreEqual("10.32.118.40", adminSettings.MailServerName.ToString());
    }

    [Test]
    public void can_correctly_parse_admin_auth_timeout() {
      Assert.AreEqual(10, adminSettings.AuthTimeout);
    }

    [Test]
    public void can_correctly_parse_admin_auth_web_timeout() {
      Assert.AreEqual(10, adminSettings.AuthTimeout);
    }

    [Test]
    public void can_correctly_parse_admin_auth_server() {
      Assert.AreEqual("Local", adminSettings.AuthServer);
    }
  }
}