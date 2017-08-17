using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR175_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void ir175_should_return_true_for_complaint_device() {
      blob = new AssetBlob {
        Body = @"no logging console
logging monitor informational
enable secret 5 GOOD_MD5
!
alias exec harden uNAVY-ODMN-IOSSWT-v25_0_0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR175(device, new[] { "GOOD_MD5", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void ir175_should_return_false_for_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"no logging console
logging monitor informational
enable secret 5 BAD_MD5
!
alias exec harden uNAVY-ODMN-IOSSWT-v25_0_0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR175(device, new[] { "GOOD_MD5", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}