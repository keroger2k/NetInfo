using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS041_Tests {

    [Test]
    public void BS041_should_return_true_when_no_vlan_1_is_found() {
      var blob = new AssetBlob {
        Body = @"
!
interface ethernet 1/1/5
 port-name DISABLED
 speed-duplex 100-full
 no snmp-server enable traps link-change
!
interface ve 99
 port-name <== Management VLAN ==>
 ip address 10.32.8.96 255.255.254.0
 ip proxy-arp disable
 no ip redirect
!
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS041(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS041_should_return_false_when_vlan_1_is_found() {
      var blob = new AssetBlob {
        Body = @"
!
interface ethernet 1/1/5
 port-name DISABLED
 speed-duplex 100-full
 no snmp-server enable traps link-change
!
interface ve 1
 port-name <== Management VLAN ==>
 ip address 10.32.8.96 255.255.254.0
 ip proxy-arp disable
 no ip redirect
!
interface ve 99
 port-name <== Management VLAN ==>
 ip address 10.32.8.96 255.255.254.0
 ip proxy-arp disable
 no ip redirect
!
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS041(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}