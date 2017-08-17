using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS131_Tests {

    [Test]
    public void should_return_true_for_device_where_all_access_ports_are_configured_with_dot1x_reauth_less_than_3600() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/2
 switchport access vlan 210
 switchport mode access
 authentication periodic
 dot1x timeout re-authperiod 30
!
interface FastEthernet0/3
 switchport access vlan 210
 switchport mode access
 authentication periodic
 dot1x timeout re-authperiod 30
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS131(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void should_return_false_for_device_where_not_all_access_ports_are_configured_with_dot1x_reauth_less_than_3600() {
      AssetBlob blob = new AssetBlob {
        Body = @"!
interface FastEthernet0/2
 switchport access vlan 210
 switchport mode access
 authentication periodic
 dot1x timeout re-authperiod 3700
!
interface FastEthernet0/3
 switchport access vlan 210
 switchport mode access
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS131(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void default_configuration_should_return_true_meaning_it_will_not_show_in_configuration() {
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
      ISTIGItem item = new IS131(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}