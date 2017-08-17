using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS130_Tests {

    [Test]
    public void should_return_true_for_device_where_all_access_ports_are_configured_with_port_control_auto() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/2
 switchport access vlan 210
 switchport mode access
 authentication port-control auto
!
interface FastEthernet0/3
 switchport access vlan 210
 switchport mode access
 authentication port-control auto
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS130(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void should_return_false_for_device_where_not_all_access_ports_are_configured_with_port_control_auto() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/2
 switchport access vlan 210
 switchport mode access
 authentication port-control auto
!
interface FastEthernet0/3
 switchport access vlan 210
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS130(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}