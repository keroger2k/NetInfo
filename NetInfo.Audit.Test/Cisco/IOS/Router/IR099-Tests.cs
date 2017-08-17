using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR099_Tests {
    private AssetBlob blob;

    [TestCase(@"clock timezone utc 0")]
    [TestCase(@"clock timezone utc 0 0")]
    public void ir099_should_return_true_for_complaint_device(string line) {
      blob = new AssetBlob {
        Body = line
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR099(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [TestCase(@"")]
    [TestCase(@"clock timezone utc -1")]
    [TestCase(@"clock timezone utc -1 -1")]
    [TestCase(@"clock timezone utc 0 -1")]
    [TestCase(@"clock timezone utc -1 0")]
    public void ir099_should_return_false_for_noncomplaint_device(string line) {
      blob = new AssetBlob {
        Body = line
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR099(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}