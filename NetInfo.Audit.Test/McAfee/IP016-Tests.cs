using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP016_Tests {

    [Test]
    public void IP016_should_return_true_when_management_link_is_ok() {
      var blob = new AssetBlob {
        Body = @"MGMT port Link Status	: negotiated 100baseTx-FD, link ok"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP016(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP016_should_return_true_when_management_link_is_up() {
      var blob = new AssetBlob {
        Body = @"MGMT port Link Status	: negotiated 100baseTx-FD, link up"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP016(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP016_should_return_false_when_management_link_is_not_up_or_ok() {
      var blob = new AssetBlob {
        Body = @"MGMT port Link Status	: negotiated 100baseTx-FD, link down"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP016(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IP016_should_return_true_when_management_status_is_reported_with_software_version_6_1_15_33_and_hardware_version_1_20() {
      var blob = new AssetBlob {
        Body = @"MGMT port Link Status   : link up "
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP016(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}