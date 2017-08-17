using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR070_Tests {

    [Test]
    public void IR070_should_return_true_when_transport_input_telnet_is_configured_on_vty_0_4() {
      var blob = new AssetBlob {
        Body = @"!
line con 0
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input telnet
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR070(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR070_should_return_false_when_transport_input_telnet_is_not_configured_on_vty_0_4() {
      var blob = new AssetBlob {
        Body = @"!
line con 0
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input none
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR070(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR070_should_return_false_when_an_empty_config_is_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR070(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}