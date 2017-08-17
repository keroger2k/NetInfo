using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB044_Tests {

    [Test]
    public void RB044_should_return_true_status_is_healthy() {
      var blob = new AssetBlob {
        Body = @"Status: Healthy"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB044(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB044_should_return_false_status_is_not_healthy() {
      var blob = new AssetBlob {
        Body = @"Status: Sick"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB044(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB044_should_be_able_find_status_for_this_format() {
      var blob = new AssetBlob {
        Body = @"QUAN-U00-CM-01 # #
QUAN-U00-CM-01 # show info
#
#
#
#
Current User:      admin

Status:            Healthy
Config:            initial
Appliance Up Time: 269d 1h 53m 22s

Serial:            D37HS000573BF
Model:             8000
Revision:          B
Version:           5.5.4c
QUAN-U00-CM-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB044(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB044_should_return_false_when_status_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB044(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}