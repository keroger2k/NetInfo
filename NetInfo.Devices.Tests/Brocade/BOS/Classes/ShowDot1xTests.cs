using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class ShowDot1xTests {

    [Test]
    public void show_dot1x_correctly_parses_re_auth_period() {
      var dot1x = new ShowDot1x(
        @"PAE Capability : Authenticator Only
system-auth-control : Enable
re-authentication : Enable
global-filter-strict-security : Enable
quiet-period : 30 Seconds
tx-period : 30 Seconds
supptimeout : 30 Seconds
servertimeout : 15 Seconds
maxreq : 2
reAuthMax : 2
re-authperiod : 3600 Seconds
Protocol Version : 1".ToConfig()
      );

      Assert.AreEqual(3600, dot1x.Dot1x.ReAuthPeriod);
    }

    [Test]
    public void show_dot1x_correctly_returns_0_when_command_is_not_found() {
      var dot1x = new ShowDot1x(
        @"".ToConfig()
      );

      Assert.AreEqual(int.MaxValue, dot1x.Dot1x.ReAuthPeriod);
    }
  }
}