using System.Linq;
using NetInfo.Devices.Brocade.BOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class ShowInterfaceTests {

    [Test]
    public void show_interface_can_correctly_identify_a_port_in_dual_mode() {
      var status = new ShowInterface(@"GigabitEthernet1/1/1 is up, line protocol is up
  Member of 7 L2 VLANs, port is dual mode in Vlan 1, port state is FORWARDING

END-OF-TEST-SCRIPT
!".ToConfig());

      Assert.AreEqual(ShowInterface.Interface.PMode.dual, status.Interfaces.First().PortInformation.PortMode);
    }

    [Test]
    public void show_interface_can_correctly_identify_a_port_in_tagged() {
      var status = new ShowInterface(@"GigabitEthernet1/1/1 is up, line protocol is up
  Member of 9 L2 VLANs, port is tagged, port state is BLOCKING

END-OF-TEST-SCRIPT
!".ToConfig());

      Assert.AreEqual(ShowInterface.Interface.PMode.tagged, status.Interfaces.First().PortInformation.PortMode);
    }

    [Test]
    public void show_interface_can_correctly_identify_a_port_in_untagged() {
      var status = new ShowInterface(@"GigabitEthernet1/1/1 is up, line protocol is up
  Member of L2 VLAN ID 212, port is untagged, port state is BLOCKING

END-OF-TEST-SCRIPT
!".ToConfig());

      Assert.AreEqual(ShowInterface.Interface.PMode.untagged, status.Interfaces.First().PortInformation.PortMode);
    }

    [Test]
    public void show_interface_can_correctly_identify_a_trunk_port() {
      var status = new ShowInterface(@"GigabitEthernet1/1/1 is up, line protocol is up
  Member of 7 L2 VLANs, port is dual mode in Vlan 1, port state is FORWARDING

END-OF-TEST-SCRIPT
!".ToConfig());

      Assert.AreEqual(ShowInterface.Interface.PType.Trunk, status.Interfaces.First().PortInformation.PortType);
    }

    [Test]
    public void show_interface_can_correctly_identify_a_stack_port() {
      var status = new ShowInterface(@"GigabitEthernet1/1/1 is up, line protocol is up
  Stacking Port, port state is FORWARDING

END-OF-TEST-SCRIPT
!".ToConfig());

      Assert.AreEqual(ShowInterface.Interface.PType.Stack, status.Interfaces.First().PortInformation.PortType);
    }

    [Test]
    public void show_interface_can_correctly_identify_a_access_port() {
      var status = new ShowInterface(@"GigabitEthernet1/1/1 is up, line protocol is up
  Member of L2 VLAN ID 212, port is untagged, port state is BLOCKING

END-OF-TEST-SCRIPT
!".ToConfig());

      Assert.AreEqual(ShowInterface.Interface.PType.Access, status.Interfaces.First().PortInformation.PortType);
    }

    [Test]
    public void show_interface_can_determine_correct_number_of_interfaces() {
      var status = new ShowInterface(@"GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f898.c680 (bia 748e.f898.c680)
  Configured speed auto, actual 1Gbit, configured duplex fdx, actual fdx
  Configured mdi mode AUTO, actual MDI
  Member of 7 L2 VLANs, port is dual mode in Vlan 1, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
  Link Error Dampening is Disabled
  STP configured to ON, priority is level0, mac-learning is enabled
  Flow Control is config enabled, oper enabled, negotiation disabled
  Mirror disabled, Monitor disabled
  Member of active trunk ports 1/1/1,2/1/1, primary port
  Member of configured trunk ports 1/1/1,2/1/1, primary port
  Port name is U01_AS02_G1/3
  Inter-Packet Gap (IPG) is 96 bit times
  MTU 1500 bytes, encapsulation ethernet
  300 second input rate: 175144 bits/sec, 155 packets/sec, 0.01% utilization
  300 second output rate: 2608704 bits/sec, 275 packets/sec, 0.26% utilization
  2239778 packets input, 1659771419 bytes, 0 no buffer
  Received 5771 broadcasts, 2408 multicasts, 2231599 unicasts
  0 input errors, 0 CRC, 0 frame, 0 ignored
  0 runts, 0 giants
  2217701 packets output, 862511383 bytes, 0 underruns
  Transmitted 3628 broadcasts, 15683 multicasts, 2198390 unicasts
  0 output errors, 0 collisions
  Relay Agent Information option: Disabled

Egress queues:
Queue counters    Queued packets    Dropped Packets
    0             2206334                   0
    1                   0                   0
    2                   1                   0
    3                   0                   0
    4                   0                   0
    5                  36                   0
    6               11330                   0
    7                   0                   0
GigabitEthernet1/1/2 is down, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f898.c681 (bia 748e.f898.c681)
  Configured speed auto, actual unknown, configured duplex fdx, actual unknown
  Configured mdi mode AUTO, actual unknown
  Member of L2 VLAN ID 1, port is untagged, port state is BLOCKING
  BPDU guard is Enabled, ROOT protect is Disabled
  Link Error Dampening is Disabled
  STP configured to ON, priority is level0, mac-learning is enabled
  Flow Control is config enabled, oper disabled, negotiation disabled
  Mirror disabled, Monitor disabled
  Not member of any active trunks
  Not member of any configured trunks
  Port name is <=-Reserved-=>
  Inter-Packet Gap (IPG) is 96 bit times
  MTU 1500 bytes, encapsulation ethernet
  300 second input rate: 0 bits/sec, 0 packets/sec, 0.00% utilization
  300 second output rate: 0 bits/sec, 0 packets/sec, 0.00% utilization
  0 packets input, 0 bytes, 0 no buffer
  Received 0 broadcasts, 0 multicasts, 0 unicasts
  0 input errors, 0 CRC, 0 frame, 0 ignored
  0 runts, 0 giants
  0 packets output, 0 bytes, 0 underruns
  Transmitted 0 broadcasts, 0 multicasts, 0 unicasts
  0 output errors, 0 collisions
  Relay Agent Information option: Disabled

Egress queues:
Queue counters    Queued packets    Dropped Packets
    0                   0                   0
    1                   0                   0
    2                   0                   0
    3                   0                   0
    4                   0                   0
    5                   0                   0
    6                   0                   0
    7                   0                   0
GigabitEthernet1/1/3 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f898.c680 (bia 748e.f898.c682)
  Configured speed auto, actual 100Mbit, configured duplex fdx, actual fdx
  Configured mdi mode AUTO, actual MDIX
  Member of 7 L2 VLANs, port is dual mode in Vlan 1, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
  Link Error Dampening is Disabled
  STP configured to ON, priority is level0, mac-learning is enabled
  Flow Control is config enabled, oper enabled, negotiation disabled
  Mirror disabled, Monitor disabled
  Not member of any active trunks
  Not member of any configured trunks
  Port name is U00_IR01_F1/8
  Inter-Packet Gap (IPG) is 96 bit times
  MTU 1500 bytes, encapsulation ethernet
  300 second input rate: 5733608 bits/sec, 638 packets/sec, 5.83% utilization
  300 second output rate: 672328 bits/sec, 388 packets/sec, 0.72% utilization
  4371172 packets input, 1605284248 bytes, 0 no buffer
  Received 2727 broadcasts, 16404 multicasts, 4352041 unicasts
  0 input errors, 0 CRC, 0 frame, 0 ignored
  0 runts, 0 giants
  4584486 packets output, 3648099221 bytes, 0 underruns
  Transmitted 4518 broadcasts, 1868 multicasts, 4578100 unicasts
  0 output errors, 39154 collisions
  Relay Agent Information option: Disabled

Egress queues:
Queue counters    Queued packets    Dropped Packets
    0             4624524                   0
    1                   0                   0
    2                   1                   0
    3                   0                   0
    4                   0                   0
    5               14218                   0
    6                4864                   0
    7                   0                   0
16GigabitEthernet3/2/2 is up, line protocol is up
  Hardware is 16GigabitEthernet, address is 748e.f898.d172 (bia 748e.f898.d172)
  Interface type is 16Gig CX4
  Configured speed 16Gbit, actual 16Gbit, configured duplex fdx, actual fdx
  Stacking Port, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
  Link Error Dampening is Disabled
  STP configured to ON, priority is level0, mac-learning is enabled
  Flow Control is enabled
  Mirror disabled, Monitor disabled
  Not member of any active trunks
  Not member of any configured trunks
  No port name
  MTU 1500 bytes, encapsulation ethernet
  300 second input rate: 128512 bits/sec, 79 packets/sec, 0.00% utilization
  300 second output rate: 862120 bits/sec, 164 packets/sec, 0.00% utilization
  354840 packets input, 71518699 bytes, 0 no buffer
  Received 2397 broadcasts, 43606 multicasts, 308837 unicasts
  0 input errors, 0 CRC, 0 frame, 0 ignored
  0 runts, 0 giants
  802697 packets output, 609900480 bytes, 0 underruns
  Transmitted 706 broadcasts, 437390 multicasts, 364601 unicasts
  0 output errors, 0 collisions
  Relay Agent Information option: Disabled

Egress queues:
Queue counters    Queued packets    Dropped Packets
    0              365295                   0
    1                   0                   0
    2                   0                   0
    3                   0                   0
    4                 152                   0
    5                   0                   0
    6                 129                   0
    7              437040                   0
GigEthernetmgmt1 is disabled, line protocol is down
  Hardware is GigEthernet, address is 748e.f898.c680 (bia 748e.f898.c6b0)
  Configured speed auto, actual unknown, configured duplex fdx, actual unknown
  Configured mdi mode AUTO, actual unknown
  Not a Member of any VLAN , port is untagged, port state is NONE
  No port name
  MTU 1500 bytes, encapsulation ethernet
  300 second input rate: 0 bits/sec, 0 packets/sec, 0.00% utilization
  300 second output rate: 0 bits/sec, 0 packets/sec, 0.00% utilization
  0 packets input, 0 bytes, 0 no buffer
  Received 0 broadcasts, 0 multicasts, 0 unicasts
  0 input errors, 0 CRC, 0 frame, 0 ignored
  0 runts, 0 giants
  0 packets output, 0 bytes, 0 underruns
  Transmitted 0 broadcasts, 0 multicasts, 0 unicasts
  0 output errors, 0 collisions

END-OF-TEST-SCRIPT
!".ToConfig());

      Assert.AreEqual(5, status.Interfaces.Count());
    }
  }
}