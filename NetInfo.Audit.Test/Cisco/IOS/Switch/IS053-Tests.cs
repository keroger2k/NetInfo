using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS053_Tests {
    /// <summary>
    ///
    /// Below are two circumstances under which the Tool is failing devices that should pass:
    ///
    /// - Monitoring ports assigned to vlan1 should be allowed, but the tool is indicating
    ///   these devices are failing this check.  Examples are: ALBY-U00-IS-03, ALBY-U00-OS-03, SMTZ-U00-IS-03
    ///
    /// - Disabled ports assigned to vlan1 should not be flagged as a failure by this line item.
    ///   Note: IS067 will capture these as failures since disabled ports must be assigned to access vlan2.
    ///
    /// </summary>

    [Test]
    public void IS053_should_return_true_when_all_users_ports_are_not_assigned_to_vlan_1() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#show interface status

Port      Name               Status       Vlan       Duplex  Speed Type
Fa0/1     <== NETSCREEN VPN  connected    30           full    100 10/100BaseTX
Fa0/2     <== DISABLED ==>   disabled     2            full    100 10/100BaseTX
Fa0/3     N-CP---00106-007D1 connected    210          full    100 10/100BaseTX
Fa0/5     N-CP---00101-003D1 notconnect   210          full    100 10/100BaseTX
Fa0/21    <== DISABLED ==>   disabled     2            full    100 10/100BaseTX
ABCD-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS053(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS053_should_return_false_when_any_user_port_is_assigned_to_vlan_1() {
      var blob = new AssetBlob {
        Body = @"PNDL-U08-DP-06#show interface status

Port    Name               Status       Vlan       Duplex Speed Type
Fa1/0   104-001-D1         connected    1            full   a-100 10/100BaseTX
Fa1/4   24085-102-002-D2   connected    1            full   a-100 10/100BaseTX
Fa1/5                      disabled     1            auto    auto 10/100BaseTX
Fa1/6   <== End Device VLA disabled     280          auto    auto 10/100BaseTX
Fa1/15  connected-to-fa0/0 connected    1          a-full   a-100 10/100BaseTX
PNDL-U08-DP-06#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS053(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS053_should_disregard_monitoring_ports_and_their_assignment_to_vlan_1() {
      var blob = new AssetBlob {
        Body = @"ALBY-U00-IS-03#show interface status

Port      Name               Status       Vlan       Duplex  Speed Type
Gi1/0/1   <== U00_IR01_G3/3  connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/2   <== U00_IR02_G3/3  connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/3   <== DISABLED ==>   disabled     2            auto   auto Not Present
Gi1/0/4   <== DISABLED ==>   disabled     2            auto   auto Not Present
Gi1/0/5   <== SNS 1 Gig0$ -  monitoring   1          a-full a-1000 1000BaseSX SFP
Gi1/0/6   <== SNS 1 Gig1$ -  monitoring   1          a-full a-1000 1000BaseSX SFP
Gi1/0/7   <== DISABLED ==>   disabled     2            auto   auto Not Present
Gi1/0/8   <== DISABLED ==>   disabled     2            auto   auto Not Present
Gi1/0/9   <== U01_DR01_G2/12 connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/10  <== U01_DR02_G2/12 connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/11  <== DISABLED ==>   disabled     2            auto   auto Not Present
Gi1/0/12  <== DISABLED ==>   disabled     2            auto   auto Not Present
ALBY-U00-IS-03#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS053(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS053_should_disregard_disabled_ports_and_their_assignment_to_vlan_1() {
      var blob = new AssetBlob {
        Body = @"ALBY-U00-IS-03#show interface status

Port      Name               Status       Vlan       Duplex  Speed Type
Gi1/0/1   <== U00_IR01_G3/3  connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/2   <== U00_IR02_G3/3  connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/3   <== DISABLED ==>   disabled     1            auto   auto Not Present
Gi1/0/4   <== DISABLED ==>   disabled     1            auto   auto Not Present
Gi1/0/5   <== SNS 1 Gig0$ -  monitoring   1          a-full a-1000 1000BaseSX SFP
Gi1/0/6   <== SNS 1 Gig1$ -  monitoring   1          a-full a-1000 1000BaseSX SFP
Gi1/0/7   <== DISABLED ==>   disabled     1            auto   auto Not Present
Gi1/0/8   <== DISABLED ==>   disabled     1            auto   auto Not Present
Gi1/0/9   <== U01_DR01_G2/12 connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/10  <== U01_DR02_G2/12 connected    trunk      a-full a-1000 1000BaseSX SFP
Gi1/0/11  <== DISABLED ==>   disabled     1            auto   auto Not Present
Gi1/0/12  <== DISABLED ==>   disabled     1            auto   auto Not Present
ALBY-U00-IS-03#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS053(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS053_should_fail_devices_with_ports_that_are_inactive_and_assigned_to_vlan_1() {
      var blob = new AssetBlob {
        Body = @"ANND-U03-AS-03#show interface status

Port      Name               Status       Vlan       Duplex  Speed Type
Fa1/1     3U14-6-A327-N      notconnect   233          full    100 100BaseFX
Fa2/43    3U11-10-A320-N     connected    504          full    100 100BaseFX
Te3/1     <== DISABLED ==>   disabled     2            full    10G No X2
Te3/2     <== DISABLED ==>   disabled     2            full    10G No X2
Gi3/3     <== U03_DR01_G2/3  connected    trunk        full   1000 1000BaseSX
Gi3/4     <== DISABLED ==>   disabled     2            full   1000 No Gbic
Gi3/5     <== DISABLED ==>   disabled     2            full   1000 No Gbic
Gi3/6     <== DISABLED ==>   disabled     2            full   1000 No Gbic
Te4/2                        inactive     1            full    10G No X2
Gi4/3                        inactive     1            full   1000 No Gbic
Gi4/4                        inactive     1            full   1000 No Gbic
Gi4/5                        inactive     1            full   1000 No Gbic
Gi4/6                        inactive     1            full   1000 No Gbic
ANND-U03-AS-03#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS053(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}