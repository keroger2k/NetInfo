using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR191_Tests {

    [Test]
    public void IR191_should_return_true_when_ip_unreachables_is_not_enabled_on_external_interface() {
      var blob = new AssetBlob {
        Body = @"
interface Vlan40
 ip address 1.1.1.1 255.255.255.0
 no ip unreachables
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR191(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR191_should_return_false_when_ip_unreachables_is_enabled_on_external_interface() {
      var blob = new AssetBlob {
        Body = @"
interface Vlan40
 ip address 1.1.1.1 255.255.255.0
 ip unreachables
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR191(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR191_should_return_false_when_ip_unreachables_is_not_found_on_external_interface() {
      var blob = new AssetBlob {
        Body = @"
interface Vlan40
 ip address 1.1.1.1 255.255.255.0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR191(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}