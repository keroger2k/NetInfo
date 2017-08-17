using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class AAASettingsTests {
    private AAASettings aaaSettings;

    private IEnumerable<string> genericSettings = @"
aaa authentication login default group tacacs+ enable
aaa authentication enable default group tacacs+ enable
aaa authentication dot1x default group radius
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [SetUp]
    public void Init() {
      aaaSettings = new AAASettings();
      aaaSettings.Settings = genericSettings;
    }

    [Test]
    public void should_return_correct_default_dot1x_group_for_authentication() {
      Assert.AreEqual("radius", aaaSettings.Authentication.Dot1x.DefaultGroup);
    }

    [Test]
    public void should_return_true_when_correct_login_tacacs_group_is_enabled() {
      Assert.True(aaaSettings.Authentication.LoginGroupTacacsEnable);
    }

    [Test]
    public void should_return_true_when_correct_enable_tacacs_group_is_enabled() {
      Assert.True(aaaSettings.Authentication.EnableGroupTacacsEnable);
    }
  }
}