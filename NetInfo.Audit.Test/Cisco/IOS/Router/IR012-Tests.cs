using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router
{

    [TestFixture]
  public class IR012_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR012_should_return_true_when_vlan_1_is_not_being_trunked() {
      blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 91,99
 switchport mode trunk
!
!
"
      };

      IIOSDevice device = new IOSDevice(blob);
      ISTIGItem item = new NETVLAN005(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR012_should_return_false_when_vlan_1_is_being_trunked() {
      blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 1,91,99
 switchport mode trunk
!
!
"
      };

      IIOSDevice device = new IOSDevice(blob);
      ISTIGItem item = new NETVLAN005(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR012_should_return_return_true_when_vlan_is_not_being_trunked_and_vlan_ranages_are_being_used() {
      blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 91,99, 100-200
 switchport mode trunk
!
!
"
      };

      IIOSDevice device = new IOSDevice(blob);
      ISTIGItem item = new NETVLAN005(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR012_should_return_return_true_when_vlan_is_not_being_trunked_even_though_mode_trunk_is_not_explicitly_set() {
      blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 91,99, 100-200
!
!
"
      };

      IIOSDevice device = new IOSDevice(blob);
      ISTIGItem item = new NETVLAN005(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR012_should_return_return_false_when_vlan_is_being_trunked_even_though_mode_trunk_is_not_explicitly_set() {
      blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 1,91,99, 100-200
!
!
"
      };

      IIOSDevice device = new IOSDevice(blob);
      ISTIGItem item = new NETVLAN005(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR012_should_return_false_when_no_ports_are_in_trunking_mode() {
      blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport
 switchport mode access
!
!
"
      };

      IIOSDevice device = new IOSDevice(blob);
      ISTIGItem item = new NETVLAN005(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}