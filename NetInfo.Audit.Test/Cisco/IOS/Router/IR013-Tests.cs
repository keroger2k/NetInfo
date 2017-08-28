using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR013_Tests {

    [Test]
    public void IR013_should_return_true_when_a_port_is_in_access_mode_and_no_trunking_commands_are_found() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport access vlan 210
 switchport mode access
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NETVLAN007(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR013_should_return_false_when_a_port_is_in_access_mode_and_trunking_encapsulation_command_is_found() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport access vlan 210
 switchport mode access
 switchport trunk encapsulation dot1q
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NETVLAN007(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR013_should_return_false_when_a_port_is_in_access_mode_and_trunking_vlans_allowed_command_is_found() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport access vlan 210
 switchport mode access
 switchport trunk allowed vlan 91,99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NETVLAN007(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR013_should_return_false_when_a_port_is_in_access_mode_and_both_encapusulations_and_allowed_vlans_commands_are_found() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport access vlan 210
 switchport mode access
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 91,99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NETVLAN007(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR013_should_not_check_any_ports_that_are_shutdown() {
      AssetBlob blob = new AssetBlob {
        Body = @"
!
interface GigabitEthernet3/2
 description <== U01_DR01_G2/16 ==>
 switchport access vlan 210
 switchport mode access
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 91,99
 shutdown
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NETVLAN007(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}