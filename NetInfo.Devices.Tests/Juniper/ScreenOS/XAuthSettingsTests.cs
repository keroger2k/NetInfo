using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class XAuthSettingsTests {
    private XAuthSettings xauthSettings;

    [SetUp]
    public void Init() {
      xauthSettings = new XAuthSettings();
      xauthSettings.Settings = genericSettings;
    }

    private IEnumerable<string> genericSettings = @"
set xauth lifetime 480
set xauth default auth server Local
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void can_correctly_parse_lifetime() {
      Assert.AreEqual(480, xauthSettings.Lifetime);
    }

    [Test]
    public void can_correctly_parse_auth_server() {
      Assert.AreEqual("Local", xauthSettings.AuthServer);
    }
  }
}