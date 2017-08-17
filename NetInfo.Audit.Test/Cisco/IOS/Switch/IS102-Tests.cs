using System.Collections.Generic;
using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS102_Tests {

    private IEnumerable<Vlan> vlans = new List<Vlan> {
      new Vlan { Number = 2, ShortDescription = "U_UNUSED" },
      new Vlan { Number = 30, ShortDescription = "U_IA_VPN_TB_I" },
      new Vlan { Number = 210, ShortDescription = "U_USER_210" },
      new Vlan { Number = 500, ShortDescription = "U_PRINT_500" },
    };

    [Test]
    public void IS102_should_return_true_when_all_vlan_names_match_their_standards() {
      var blob = new AssetBlob {
        Body = @"!
vlan internal allocation policy ascending
!
vlan 2
 name U_UNUSED
!
vlan 30
 name U_IA_VPN_TB_I
!
vlan 210
 name U_USER_210
!
vlan 500
 name U_PRINT_500
!
ip tcp synwait-time 10
!
!
!
!
interface Port-channel101
 description <== BUNDLED PORTS Gig3/1-4,4/1-4 TO U99-DR01 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 90,99
 switchport mode trunk
 no ip address
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS102(device, vlans);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS102_should_return_false_when_not_all_vlan_names_match_their_standards() {
      var blob = new AssetBlob {
        Body = @"!
vlan internal allocation policy ascending
!
vlan 2
 name U_UNUSED_FAIL
!
ip tcp synwait-time 10
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS102(device, vlans);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void production1() {
      var blob = new AssetBlob {
        Body = @"!
vlan internal allocation policy ascending
vlan access-log ratelimit 2000
!
vlan 2
 name U_UNUSED
!
vlan 90
 name U_CORE_1
!
vlan 99
 name U_MANAGEMENT
!
vlan 800
 name U_NETC_COI_VLAN_800
!
vlan 801
 name U_NETC_COI_VLAN_801
!
vlan 802
 name U_NETC_COI_VLAN_802
!
vlan 803
 name U_NETC_COI_VLAN_803
!
vlan 804
 name U_NETC_COI_VLAN_804
!
vlan 805
 name U_NETC_COI_VLAN_805
!
vlan 806
 name U_NETC_COI_VLAN_806
!
vlan 807
 name U_NETC_COI_VLAN_807
!
vlan 808
 name U_NETC_COI_VLAN_808
!
vlan 809
 name U_NETC_COI_VLAN_809
!
vlan 810
 name U_NETC_COI_VLAN_810
!
vlan 811
 name U_NETC_COI_VLAN_811
!
vlan 812
 name U_NETC_COI_VLAN_812
!
vlan 813
 name U_NETC_COI_VLAN_813
!
vlan 814
 name U_NETC_COI_VLAN_814
!
vlan 815
 name U_NETC_COI_VLAN_815
!
vlan 816
 name U_NETC_COI_VLAN_816
!
vlan 817
 name U_NETC_COI_VLAN_817
!
vlan 818
 name U_NETC_COI_VLAN_818
!
vlan 819
 name U_NETC_COI_VLAN_819
!
vlan 820
 name U_NETC_COI_VLAN_820
!
vlan 821
 name U_NETC_COI_VLAN_821
!
vlan 822
 name U_NETC_COI_VLAN_822
!
vlan 823
 name U_NETC_COI_VLAN_823
!
vlan 824
 name U_NETC_COI_VLAN_824
!
vlan 825
 name U_NETC_COI_VLAN_825
!
vlan 826
 name U_NETC_COI_VLAN_826
!
vlan 827
 name U_NETC_COI_VLAN_827
!
vlan 828
 name U_NETC_COI_VLAN_828
!
vlan 829
 name U_NETC_COI_VLAN_829
!
vlan 830
 name U_NETC_COI_VLAN_830
!
vlan 831
 name U_NETC_COI_VLAN_831
!
vlan 832
 name U_NETC_COI_VLAN_832
!
vlan 833
 name U_NETC_COI_VLAN_833
!
vlan 834
 name U_NETC_COI_VLAN_834
!
vlan 835
 name U_NETC_COI_VLAN_835
!
vlan 836
 name U_NETC_COI_VLAN_836
!
vlan 837
 name U_NETC_COI_VLAN_837
!
vlan 838
 name U_NETC_COI_VLAN_838
!
vlan 839
 name U_NETC_COI_VLAN_839
!
vlan 840
 name U_NETC_COI_VLAN_840
!
vlan 841
 name U_NETC_COI_VLAN_841
!
vlan 842
 name U_NETC_COI_VLAN_842
!
vlan 843
 name U_NETC_COI_VLAN_843
!
vlan 844
 name U_NETC_COI_VLAN_844
!
vlan 845
 name U_NETC_COI_VLAN_845
!
vlan 846
 name U_NETC_COI_VLAN_846
!
vlan 847
 name U_NETC_COI_VLAN_847
!
vlan 848
 name U_NETC_COI_VLAN_848
!
vlan 849
 name U_NETC_COI_VLAN_849
!
!
!
!
interface Port-channel101
 description <== BUNDLED PORTS Gig3/1-4,4/1-4 TO U99-DR01 ==>
 switchport
 switchport trunk encapsulation dot1q
 switchport trunk allowed vlan 90,99
 switchport mode trunk
 no ip address
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS102(device, vlans);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}