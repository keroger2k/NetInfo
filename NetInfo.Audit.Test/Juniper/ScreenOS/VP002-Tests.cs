using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP002_Tests {

    [Test]
    public void vp002_should_return_true_for_4x_complaint_device() {
      var blob = new AssetBlob {
        Body = @"set admin password ""nGt+FdrrLvIFcOWDxskDUAPtpUBG5n"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP002(device, new[] { "nGt+FdrrLvIFcOWDxskDUAPtpUBG5n", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void vp002_should_return_false_for_4x_noncomplaint_device() {
      var blob = new AssetBlob {
        Body = @"set admin password ""fail"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP002(device, new[] { "correct", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void vp002_should_return_true_for_5x_complaint_device() {
      var blob = new AssetBlob {
        Body = @"set admin password nPPUKJr1JNbOcxBASspDn1DtcHJSrn"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP002(device, new[] { "nPPUKJr1JNbOcxBASspDn1DtcHJSrn", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void vp002_should_return_false_for_5x_noncomplaint_device() {
      var blob = new AssetBlob {
        Body = @"set admin password fail"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP002(device, new[] { "correct", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}