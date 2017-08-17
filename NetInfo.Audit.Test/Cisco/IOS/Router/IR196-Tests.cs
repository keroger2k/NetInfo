using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR196_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR196_should_return_true_when_no_access_ports_are_on_vlan1() {
      blob = new AssetBlob {
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
      ISTIGItem item = new IR196(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR196_should_return_false_when_an_access_port_is_on_vlan1_showing() {
      blob = new AssetBlob {
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
      ISTIGItem item = new IR196(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR196_should_return_false_when_an_access_port_is_on_vlan1_not_showing() {
      blob = new AssetBlob {
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
      ISTIGItem item = new IR196(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}