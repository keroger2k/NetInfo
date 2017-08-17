using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR059_Tests {

    [Test]
    public void IR059_should_return_true_when_vty_5_15_is_configured_with_transport_input_none() {
      var blob = new AssetBlob {
        Body = @"!
line vty 0 4
  transport input ssh
line vty 5 15
  access-class 99 in
  transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR059(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR059_should_return_false_when_vty_5_15_is_NOT_configured_with_transport_input_none() {
      var blob = new AssetBlob {
        Body = @"!
line vty 0 4
  transport input none
line vty 5 15
  access-class 99 in
  transport input ssh
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR059(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}