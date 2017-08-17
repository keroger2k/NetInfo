using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS034_Tests {

    [Test]
    public void bs034_should_return_true_for_complaint_device() {
      var blob = new AssetBlob {
        Body = @"enable super-user-password 8 $1$L4/..wv/$HcFIXDaWqTvcuDnBibeKh"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS034(device, new[] { "$1$L4/..wv/$HcFIXDaWqTvcuDnBibeKh", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void bs034_should_return_false_for_noncomplaint_device() {
      var blob = new AssetBlob {
        Body = @"enable super-user-password 8 fail"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS034(device, new[] { "correct", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void bs034_should_return_false_device_with_this_format() {
      var blob = new AssetBlob {
        Body = @"enable super-user-password ....."
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS034(device, new[] { "correct", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}