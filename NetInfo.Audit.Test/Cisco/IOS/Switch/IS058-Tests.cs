using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS058_Tests {

    [Test]
    public void IS058_should_return_true_when_vty_0_4_is_configured_with_transport_input_ssh() {
      var blob = new AssetBlob {
        Body = @"!
line vty 0 4
  transport input ssh
line vty 5 15
  access-class 99 in
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS058(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS058_should_return_false_when_vty_0_4_is_NOT_configured_with_transport_input_ssh() {
      var blob = new AssetBlob {
        Body = @"!
line vty 0 4
  transport input none
line vty 5 15
  access-class 99 in
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS058(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}