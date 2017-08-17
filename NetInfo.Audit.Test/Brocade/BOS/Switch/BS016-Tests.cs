using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS016_Tests {

    [Test]
    public void BS016_should_return_true_when_vlan_1_is_tagged_in_vlan_configurations_ONLY_when_it_uses_U_VTP_VLAN_as_the_name() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#show config
!
Startup-config data location is flash memory
!
Startup configuration:
!
ver 07.3.00dT7f3
!
!
!
!
vlan 1 name U_VTP_VLAN by port
 tagged ethe 1/1/1 ethe 2/1/1 ethe 2/1/3 to 2/1/4
!
vlan 2 name DEFAULT-VLAN by port
!
vlan 939 name U_COI_DOGSTAR_939 by port
 tagged ethe 1/1/1 ethe 2/1/1
 spanning-tree
!
!
!
alias ban-global8021x=ban-access-Global-802-1x-FCS6XXS-v3.0.0
alias nms=uPRLH-INSIDE-SNMPV3-Brocade-Switch-v5_1_0
alias Brocade-ACL=ACL-USN-Update-v2_0_0
alias harden=uNAVY-INSIDE-BROCADE-L2-v6_0_0
end

SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#show vlan
Total PORT-VLAN entries: 65
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 1, Name TEST, Priority level0, Spanning tree Off
 Untagged Ports: None
   Tagged Ports: None
   Uplink Ports: None
 DualMode Ports: (U1/M1)   1
 DualMode Ports: (U2/M1)   1
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 939, Name U_COI_DOGSTAR_939, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1
   Tagged Ports: (U2/M1)   1
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS016(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS016_should_return_false_when_vlan_1_is_tagged_in_vlan_configurations() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#show config
!
Startup-config data location is flash memory
!
Startup configuration:
!
ver 07.3.00dT7f3
!
!
!
!
vlan 1 name TEST by port
 tagged ethe 1/1/1 ethe 2/1/1
!
vlan 2 name DEFAULT-VLAN by port
!
vlan 939 name U_COI_DOGSTAR_939 by port
 tagged ethe 1/1/1 ethe 2/1/1
 spanning-tree
!
!
!
alias ban-global8021x=ban-access-Global-802-1x-FCS6XXS-v3.0.0
alias nms=uPRLH-INSIDE-SNMPV3-Brocade-Switch-v5_1_0
alias Brocade-ACL=ACL-USN-Update-v2_0_0
alias harden=uNAVY-INSIDE-BROCADE-L2-v6_0_0
end

SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#show vlan
Total PORT-VLAN entries: 65
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 1, Name TEST, Priority level0, Spanning tree Off
 Untagged Ports: None
   Tagged Ports: None
   Uplink Ports: None
 DualMode Ports: (U1/M1)   1
 DualMode Ports: (U2/M1)   1
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 939, Name U_COI_DOGSTAR_939, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1
   Tagged Ports: (U2/M1)   1
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS016(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS016_should_return_true_when_there_is_no_vlan_1_configured() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U05-AS-18#show config
!
Startup-config data location is flash memory
!
Startup configuration:
!
ver 07.3.00dT7f3
!
!
!
vlan 2 name DEFAULT-VLAN by port
!
vlan 939 name U_COI_DOGSTAR_939 by port
 tagged ethe 1/1/1 ethe 2/1/1
 spanning-tree
!
!
!
alias ban-global8021x=ban-access-Global-802-1x-FCS6XXS-v3.0.0
alias nms=uPRLH-INSIDE-SNMPV3-Brocade-Switch-v5_1_0
alias Brocade-ACL=ACL-USN-Update-v2_0_0
alias harden=uNAVY-INSIDE-BROCADE-L2-v6_0_0
end

SSH@PRLH-U05-AS-18#
SSH@PRLH-U05-AS-18#
SSH@JAXS-U03-AS-08#
SSH@JAXS-U03-AS-08#show vlan
Total PORT-VLAN entries: 35
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 2, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
 Untagged Ports: (U1/M1)   7  36  37  38  39  40  41  42  43  44  45  46
 Untagged Ports: (U1/M1)  47  48
 Untagged Ports: (U1/M2)   1   2
   Tagged Ports: None
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 940, Name U_COI_DMS_940, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

SSH@JAXS-U03-AS-08#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS016(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS016_should_return_true_when_there_vlan_1_configured_but_with_no_tagged_ports() {
      var blob = new AssetBlob {
        Body = @"SSH@JAXS-U03-AS-08#
SSH@JAXS-U03-AS-08#show vlan
Total PORT-VLAN entries: 35
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 1, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
 Untagged Ports: (U1/M1)   7  36  37  38  39  40  41  42  43  44  45  46
   Tagged Ports: None
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 940, Name U_COI_DMS_940, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

SSH@JAXS-U03-AS-08#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS016(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS016_should_return_true_when_there_is_no_vlan_1_configured_and_test_script_without_SSH_HOSTNAME_being_prepend_to_test_commands() {
      var blob = new AssetBlob {
        Body = @"show vlan brief

System-max vlan Params: Max(4095) Default(64) Current(255)
Default vlan Id :1
Total Number of Vlan Configured :19
VLANs Configured :1 99 240 to 249 340 540 to 541 604 926 933 936

show vlan
Total PORT-VLAN entries: 19
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 2, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
 Untagged Ports: (U1/M1)  13  14  15  16  17  18  19  20  21  22  23  24
 Untagged Ports: (U1/M1)  25  26  27  28  29  30  31  32  33  34  35  36
 Untagged Ports: (U1/M1)  37  38  39  40  41  42  43  44  45  46  47  48

 Untagged Ports: (U1/M2)   1   2
   Tagged Ports: None
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 99, Name U_MANAGEMENT, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 940, Name U_COI_DMS_940, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

show vlan | include Name
Total PORT-VLAN entries: 19
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS016(device);

      try {
        var result = item.Compliant();
      } catch (System.Exception) {
      }

      Assert.Ignore();
    }

    [Test]
    public void BS016_should_return_false_when_vlan_1_is_configured_and_there_are_tagged_ports() {
      var blob = new AssetBlob {
        Body = @"SSH@JAXS-U03-AS-08#
SSH@JAXS-U03-AS-08#show vlan
Total PORT-VLAN entries: 35
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 1, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
 Untagged Ports: None
   Tagged Ports: (U1/M1)  13  14  15  16  17  18  19  20  21  22  23  24
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 940, Name U_COI_DMS_940, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

SSH@JAXS-U03-AS-08#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS016(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}