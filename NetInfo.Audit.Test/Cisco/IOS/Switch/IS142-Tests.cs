using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS142_Tests {

    [Test]
    public void IS142_should_return_true_when_no_access_ports_are_on_vlan1() {
      var blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/14
 switchport access vlan 2
 switchport mode access
!
interface FastEthernet0/15
 description <== DISABLED ==>
 switchport access vlan 2
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS142(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS142_should_return_false_when_an_access_port_is_on_vlan1_showing() {
      var blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/14
 switchport access vlan 1
 switchport mode access
!
interface FastEthernet0/15
 description <== DISABLED ==>
 switchport access vlan 2
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS142(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS142_should_return_true_when_port_channel_interfaces_is_not_assigned_to_vlan1() {
      var blob = new AssetBlob {
        Body = @"!
interface Port-channel1
 description <== Standby RAINFINITY Public Side ==>
 switchport
 switchport access vlan 12
 switchport mode access
 no ip address
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS142(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS142_should_return_false_when_an_access_port_is_on_vlan1_not_showing() {
      var blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/14
 switchport mode access
!
interface FastEthernet0/15
 description <== DISABLED ==>
 switchport access vlan 2
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS142(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}