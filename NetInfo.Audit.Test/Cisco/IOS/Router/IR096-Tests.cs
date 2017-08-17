using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR096_Tests {

    [Test]
    public void IR096_should_return_false_when_all_cisco_devices_connected_have_cdp_enabled_on_the_interface() {
      var blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet4/14
 description <== MLPA_U03_TA03_G0/26 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 189,196
!
interface GigabitEthernet4/16
 description <== NETC_TRUNK_INTERFACE ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 800-849
 switchport mode trunk
 no ip address
 logging event link-status
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR096(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR096_should_return_false_when_a_cisco_device_is_connected_and_cdp_is_not_enabled() {
      var blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet4/14
 description <== MLPA_U03_TA03_G0/26 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 189,196
!
interface GigabitEthernet4/16
 description <== NETC_TRUNK_INTERFACE ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 800-849
 switchport mode trunk
 no ip address
 logging event link-status
 no cdp enable
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR096(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}