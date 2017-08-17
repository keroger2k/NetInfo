using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class RCMDSettingsTests {
    private IPSettings rcmdSettings;

    private IEnumerable<string> genericSettings =
@"
ip rcmd rcp-enable
ip rcmd rsh-enable
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [SetUp]
    public void Init() {
      rcmdSettings = new IPSettings();
      rcmdSettings.Settings = genericSettings;
    }

    [Test]
    public void should_return_true_when_rcp_is_found() {
      Assert.True(rcmdSettings.RCMD.IsRCPEnabled);
    }

    [Test]
    public void should_return_true_when_rsh_is_found() {
      Assert.True(rcmdSettings.RCMD.IsRSHEnabled);
    }
  }
}