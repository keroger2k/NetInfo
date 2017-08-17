using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS012_Tests {

    [Test]
    public void IS012_should_return_true_when_vlan_1_is_not_being_trunked() {
      var blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS012(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS012_should_return_false_when_vlan_1_is_being_trunked() {
      var blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS012(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS012_should_return_return_true_when_vlan_is_not_being_trunked_and_vlan_ranages_are_being_used() {
      var blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS012(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS012_should_return_return_true_when_vlan_is_not_being_trunked_even_though_mode_trunk_is_not_explicitly_set() {
      var blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS012(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS012_should_return_return_false_when_vlan_is_being_trunked_even_though_mode_trunk_is_not_explicitly_set() {
      var blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS012(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS012_should_return_false_when_no_ports_are_in_trunking_mode() {
      var blob = new AssetBlob {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS012(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS012_shold_return_true_when_trunk_does_not_define_allowed_vlans_on_trunk() {
      var blob = new AssetBlob {
        Body = @"
interface GigabitEthernet1/1
 description <== C7494-LR-02 ==>
 no ip address
 logging event link-status
 logging event bundle-status
 logging event trunk-status
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk pruning vlan none
 switchport mode trunk
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS012(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}