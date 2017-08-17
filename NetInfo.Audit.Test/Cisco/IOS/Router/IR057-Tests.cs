using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR057_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR057_should_return_true_when_access_class_is_applied_correctly_to_vty_lines() {
      blob = new AssetBlob {
        Body = @"!
line vty 0 4
  access-class 99 in
line vty 5 15
  access-class 99 in
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR057(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR057_should_return_false_when_access_class_is_not_present_on_vty() {
      blob = new AssetBlob {
        Body = @"!
line vty 0 4
line vty 5 15
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR057(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR057_should_return_false_when_access_class_is_not_applied_to_access_list_99() {
      blob = new AssetBlob {
        Body = @"!
line vty 0 4
  access-class 1 in
line vty 5 15
  access-class 1 in
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR057(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}