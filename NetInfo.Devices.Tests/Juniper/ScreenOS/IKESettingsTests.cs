using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class IKESettingsTests {
    private IKESettings ikeSettings;

    [SetUp]
    public void Init() {
      ikeSettings = new IKESettings();
      ikeSettings.Settings = genericSettings;
    }

    private IEnumerable<string> genericSettings = @"
set ike policy-checking
set ike respond-bad-spi 1
set ike id-mode subnet
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void can_correctly_parse_policy_checking() {
      Assert.True(ikeSettings.PolicyCheckingEnabled);
    }

    [Test]
    public void can_correctly_parse_respond_bad_spi_number() {
      Assert.AreEqual(1, ikeSettings.RepondBadSpi);
    }

    [Test]
    public void can_correctly_parse_id_mode() {
      Assert.AreEqual("subnet", ikeSettings.IdMode);
    }
  }
}