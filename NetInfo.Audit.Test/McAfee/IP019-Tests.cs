using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP019_Tests {

    [Test]
    public void IP019_should_return_true_when_audit_logging_is_enabled() {
      var blob = new AssetBlob {
        Body = @"Audit Logging	 : Enabled"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP019(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP019_should_return_false_when_audit_logging_is_not_enabled() {
      var blob = new AssetBlob {
        Body = @"Audit Logging	 : Disabled"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP019(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}