using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS075_Tests {

    [Test]
    public void IS075_should_return_true_when_all_server_interfaces_have_no_null_or_default_descriptions() {
      var blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet5/2
 description <== DISABLED ==>
 switchport
 switchport access vlan 2
 switchport mode access
 shutdown
!
interface GigabitEthernet6/1
 description <== NV03_U8-103_0/1 ==>
 switchport
 switchport access vlan 99
 switchport mode access
!
interface GigabitEthernet6/2
 description <== CW09_U8-103_0/1 ==>
 switchport
 switchport access vlan 99
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS075(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS075_should_return_false_when_not_all_server_interfaces_have_no_null_or_default_descriptions() {
      var blob = new AssetBlob {
        Body = @"!
interface GigabitEthernet5/2
 description <== DISABLED ==>
 switchport
 switchport access vlan 2
 switchport mode access
 shutdown
!
interface GigabitEthernet6/1
 description <== DISABLED ==>
 switchport
 switchport access vlan 99
 switchport mode access
!
interface GigabitEthernet6/2
 description <== CW09_U8-103_0/1 ==>
 switchport
 switchport access vlan 99
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS075(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}