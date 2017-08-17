using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP037_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void VP037_should_return_true_for_juniper_complaint_device() {
      blob = new AssetBlob {
        Body = @"set pki x509 dn org-unit-name ""USN,PKI,DoD"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP037(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP037_should_return_false_for_juniper_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"set pki x509 dn org-unit-name ""USMC,PKI,DoD"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP037(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}