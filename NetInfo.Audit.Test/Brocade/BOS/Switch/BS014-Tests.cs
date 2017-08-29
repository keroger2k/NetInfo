using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS014_Tests {

    [Test]
    public void BS014_should_return_true_when_there_is_no_vlan_1_configured1() {
      var blob = new AssetBlob {
        Body = @"
SSH@PRLH-U08-AS-16#show config
!
Startup-config data location is flash memory
!
Startup configuration:
!
ver 07.3.00dT7f3
!
stack unit 1
  module 1 fcx-48-poe-port-management-module
  module 2 fcx-cx4-2-port-16g-module
!
global-stp
!
!
!
vlan 1 name DEFAULT-VLAN by port
!
SSH@PRLH-U08-AS-16#
SSH@PRLH-U08-AS-16#show vlan
Total PORT-VLAN entries: 11
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 1, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
 Untagged Ports: (U1/M2)   1   2
   Tagged Ports: None
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 2, Name [None], Priority level0, Spanning tree Off
 Untagged Ports: (U1/M1)  35  36  37  38  39  40  41  42  43  44  45
   Tagged Ports: None
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 108, Name U_uDMN_Z08, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 280, Name U_USER_280, Priority level0, Spanning tree On
 Untagged Ports: (U1/M1)   3   4   5   6   7   8   9  10  11  12  13  14
 Untagged Ports: (U1/M1)  15  16  17  18  19  20  21  24  25  26  27  28
 Untagged Ports: (U1/M1)  29  30  31  32  33  34
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 281, Name U_USER_281, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 282, Name U_USER_282, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 283, Name U_USER_283, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 580, Name U_PRINT_580, Priority level0, Spanning tree On
 Untagged Ports: (U1/M1)  22  23  46  47  48
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 581, Name U_PRINT_581, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 608, Name U_VTC_ZONE_8, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 928, Name U_COI_NAVFAC_928, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

SSH@PRLH-U08-AS-16#
SSH@PRLH-U08-AS-16#
SSH@PRLH-U08-AS-16#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS014(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS014_should_return_true_when_there_is_no_vlan_1_configured() {
      var blob = new AssetBlob {
        Body = @"SSH@JAXS-U03-AS-08#
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
      ISTIGItem item = new BS014(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS014_should_return_true_when_there_vlan_1_configured_but_with_no_untagged_ports() {
      var blob = new AssetBlob {
        Body = @"SSH@JAXS-U03-AS-08#
SSH@JAXS-U03-AS-08#show vlan
Total PORT-VLAN entries: 35
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 1, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
 Untagged Ports: None
   Tagged Ports: (U1/M1)   7  36  37  38  39  40  41  42  43  44  45  46
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
      ISTIGItem item = new BS014(device);

      var result = item.Compliant();

      Assert.True(result);
    }

//    [Test]
//    public void BS014_should_return_true_when_there_is_no_vlan_1_configured_and_test_script_without_SSH_HOSTNAME_being_prepend_to_test_commands() {
//      var blob = new AssetBlob {
//        Body = @"show vlan brief

//System-max vlan Params: Max(4095) Default(64) Current(255)
//Default vlan Id :1
//Total Number of Vlan Configured :19
//VLANs Configured :1 99 240 to 249 340 540 to 541 604 926 933 936

//show vlan
//Total PORT-VLAN entries: 19
//Maximum PORT-VLAN entries: 255

//Legend: [Stk=Stack-Id, S=Slot]
//PORT-VLAN 2, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
// Untagged Ports: (U1/M1)  13  14  15  16  17  18  19  20  21  22  23  24
// Untagged Ports: (U1/M1)  25  26  27  28  29  30  31  32  33  34  35  36
// Untagged Ports: (U1/M1)  37  38  39  40  41  42  43  44  45  46  47  48

// Untagged Ports: (U1/M2)   1   2
//   Tagged Ports: None
//   Uplink Ports: None
// DualMode Ports: None
// Mac-Vlan Ports: None
//     Monitoring: Disabled

//PORT-VLAN 99, Name U_MANAGEMENT, Priority level0, Spanning tree On
// Untagged Ports: None
//   Tagged Ports: (U1/M1)   1   2
//   Uplink Ports: None
// DualMode Ports: None
// Mac-Vlan Ports: None
//     Monitoring: Disabled

//PORT-VLAN 940, Name U_COI_DMS_940, Priority level0, Spanning tree On
// Untagged Ports: None
//   Tagged Ports: (U1/M1)   1   2
//   Uplink Ports: None
// DualMode Ports: None
// Mac-Vlan Ports: None
//     Monitoring: Disabled

//show vlan | include Name
//Total PORT-VLAN entries: 19
//Maximum PORT-VLAN entries: 255

//Legend: [Stk=Stack-Id, S=Slot]
//"
//      };
//      INMCIBOSDevice device = new NMCIBOSDevice(blob);
//      ISTIGItem item = new BS014(device);

//      try {
//        var result = item.Compliant();
//      } catch (System.Exception) {
//      }

//      Assert.Ignore();
//    }

    [Test]
    public void BS014_should_return_false_when_there_vlan_1_configured_and_untagged_ports() {
      var blob = new AssetBlob {
        Body = @"SSH@JAXS-U03-AS-08#
SSH@JAXS-U03-AS-08#show vlan
Total PORT-VLAN entries: 35
Maximum PORT-VLAN entries: 255

Legend: [Stk=Stack-Id, S=Slot]
PORT-VLAN 1, Name DEFAULT-VLAN, Priority level0, Spanning tree Off
 Untagged Ports: (U1/M1)  13  14  15  16  17  18  19  20  21  22  23  24
 Untagged Ports: (U1/M1)  25  26  27  28  29  30  31  32  33  34  35  36
 Untagged Ports: (U1/M1)  37  38  39  40  41  42  43  44  45  46  47  48
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
      ISTIGItem item = new BS014(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}