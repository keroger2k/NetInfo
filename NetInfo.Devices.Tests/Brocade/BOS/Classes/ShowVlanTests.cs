using System.Linq;
using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class ShowVlanTests {

    [Test]
    public void show_vlan_can_determine_the_correct_number_of_vlans_being_shown() {
      var showVlan = new ShowVlan(
        @"Total PORT-VLAN entries: 35
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

PORT-VLAN 99, Name U_MANAGEMENT, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 233, Name U_USER_233, Priority level0, Spanning tree On
 Untagged Ports: (U1/M1)   4   5  13  14  15  16  19  20  21  22  23  24
 Untagged Ports: (U1/M1)  25  28  29  30  31  32  33  34  35
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled
".ToConfig()
      );

      Assert.AreEqual(3, showVlan.Vlans.Count());
    }

    [Test]
    public void show_vlan_can_determine_all_untagged_ports_per_vlan() {
      var showVlan = new ShowVlan(
        @"Total PORT-VLAN entries: 35
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

PORT-VLAN 99, Name U_MANAGEMENT, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 233, Name U_USER_233, Priority level0, Spanning tree On
 Untagged Ports: (U1/M1)   4   5  13  14  15  16  19  20  21  22  23  24
 Untagged Ports: (U1/M1)  25  28  29  30  31  32  33  34  35
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled
".ToConfig()
      );

      Assert.AreEqual(16, showVlan.Vlans.ElementAt(0).UnTaggedPorts.Count());
      Assert.AreEqual(0, showVlan.Vlans.ElementAt(1).UnTaggedPorts.Count());
      Assert.AreEqual(21, showVlan.Vlans.ElementAt(2).UnTaggedPorts.Count());
    }

    [Test]
    public void show_vlan_can_determine_all_ununtagged_ports_per_vlan() {
      var showVlan = new ShowVlan(
        @"Total PORT-VLAN entries: 35
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

PORT-VLAN 99, Name U_MANAGEMENT, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 233, Name U_USER_233, Priority level0, Spanning tree On
 Untagged Ports: (U1/M1)   4   5  13  14  15  16  19  20  21  22  23  24
 Untagged Ports: (U1/M1)  25  28  29  30  31  32  33  34  35
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled
".ToConfig()
      );

      Assert.AreEqual(0, showVlan.Vlans.ElementAt(0).TaggedPorts.Count());
      Assert.AreEqual(2, showVlan.Vlans.ElementAt(1).TaggedPorts.Count());
      Assert.AreEqual(2, showVlan.Vlans.ElementAt(2).TaggedPorts.Count());
    }

    [Test]
    public void show_vlan_can_determine_all_ununtagged_ports_per_vlan_with_gap() {
      var showVlan = new ShowVlan(
        @"Total PORT-VLAN entries: 19
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

PORT-VLAN 99, Name U_MANAGEMENT, Priority level0, Spanning tree On
 Untagged Ports: None
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled

PORT-VLAN 233, Name U_USER_233, Priority level0, Spanning tree On
 Untagged Ports: (U1/M1)   4   5  13  14  15  16  19  20  21  22  23  24
 Untagged Ports: (U1/M1)  25  28  29  30  31  32  33  34  35
   Tagged Ports: (U1/M1)   1   2
   Uplink Ports: None
 DualMode Ports: None
 Mac-Vlan Ports: None
     Monitoring: Disabled
".ToConfig()
      );

      Assert.AreEqual(38, showVlan.Vlans.ElementAt(0).UnTaggedPorts.Count());
    }
  }
}