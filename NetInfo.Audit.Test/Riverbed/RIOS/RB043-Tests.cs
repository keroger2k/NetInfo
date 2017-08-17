using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB043_Tests {

    [Test]
    public void RB043_should_return_true_when_all_ntp_servers_use_version_3() {
      var blob = new AssetBlob {
        Body = @"ntp server 1.1.1.1 version ""3""
ntp server 2.2.2.2 version ""3""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB043(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB043_should_return_false_when_not_all_ntp_servers_use_version_3() {
      var blob = new AssetBlob {
        Body = @"ntp server 1.1.1.1 version ""4""
ntp server 2.2.2.2 version ""4""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB043(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}