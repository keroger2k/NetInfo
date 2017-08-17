using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR173_Tests {

    [Test]
    public void should_return_true_for_device_where_all_access_ports_are_configured_with_authentication_periodic() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/2
 switchport access vlan 210
 switchport mode access
 authentication periodic
!
interface FastEthernet0/3
 switchport access vlan 210
 switchport mode access
 authentication periodic
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR173(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void should_return_false_for_device_where_not_all_access_ports_are_configured_with_authentication_periodic() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/2
 switchport access vlan 210
 switchport mode access
 authentication periodic
!
interface FastEthernet0/3
 switchport access vlan 210
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR173(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}