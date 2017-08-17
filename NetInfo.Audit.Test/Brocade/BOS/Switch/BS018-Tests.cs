using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS018_Tests {

    [Test]
    public void BS018_should_return_true_when_dual_mode_interface_is_trunking_and_in_vlan_1_or_vlan_3() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c0 (bia 748e.f82e.e2c0)
  Member of 9 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.d381 (bia 748e.f82e.d381)
  Member of 4 L2 VLANs, port is dual mode in Vlan 1, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#!END-OF-TEST-SCRIPT"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS018_should_return_false_when_dual_mode_interface_is_trunking_in_one_vlan_other_than_vlan_1_or_vlan_3() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#show interfaces
GigabitEthernet1/1/2 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.d381 (bia 748e.f82e.d381)
  Member of 1 L2 VLANs, port is dual mode in Vlan 210, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#!END-OF-TEST-SCRIPT"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS018(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS018_should_return_true_when_all_access_ports_are_not_tagged() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c0 (bia 748e.f82e.e2c0)
  Member of 9 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is disabled, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.e2c1 (bia 748e.f82e.e2c1)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/3 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c2 (bia 748e.f82e.e2c2)
  Member of L2 VLAN ID 282, port is untagged, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/4 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c3 (bia 748e.f82e.e2c3)
  Member of L2 VLAN ID 282, port is untagged, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#!END-OF-TEST-SCRIPT"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS018_should_return_false_when_any_access_ports_are_tagged() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c0 (bia 748e.f82e.e2c0)
  Member of 9 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is disabled, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.e2c1 (bia 748e.f82e.e2c1)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/3 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c2 (bia 748e.f82e.e2c2)
  Member of L2 VLAN ID 282, port is untagged, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/4 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c3 (bia 748e.f82e.e2c3)
  Member of L2 VLAN ID 282, port is tagged, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#!END-OF-TEST-SCRIPT"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS018(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS018_should_disregard_stacking_ports() {
      var blob = new AssetBlob {
        Body = @"SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c0 (bia 748e.f82e.e2c0)
  Member of 9 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is disabled, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.e2c1 (bia 748e.f82e.e2c1)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/3 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c2 (bia 748e.f82e.e2c2)
  Member of L2 VLAN ID 282, port is untagged, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/4 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.e2c3 (bia 748e.f82e.e2c3)
  Member of L2 VLAN ID 282, port is untagged, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
16GigabitEthernet1/2/1 is up, line protocol is up
  Hardware is 16GigabitEthernet, address is 748e.f82e.e2f1 (bia 748e.f82e.e2f1)
  Stacking Port, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#
SSH@PRLH-U08-AS-18#!END-OF-TEST-SCRIPT"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS018(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS018_needs_to_account_for_this_type_of_script_ending() {
      var blob = new AssetBlob {
        Body = @"
SSH@DLGR-U03-AS-23#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f86f.dfc0 (bia 748e.f86f.dfc0)
  Configured speed auto, actual 1Gbit, configured duplex fdx, actual fdx
  Configured mdi mode AUTO, actual MDIX
  Member of 12 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
  Link Error Dampening is Disabled
  STP configured to ON, priority is level0, mac-learning is enabled
  Flow Control is config enabled, oper enabled, negotiation disabled
  Mirror disabled, Monitor disabled
  Not member of any active trunks
  Not member of any configured trunks
  Port name is U03_DR01_G4/10
  Inter-Packet Gap (IPG) is 96 bit times
  MTU 1500 bytes, encapsulation ethernet
  300 second input rate: 189784 bits/sec, 86 packets/sec, 0.01% utilization
  300 second output rate: 70232 bits/sec, 33 packets/sec, 0.00% utilization
  172900317 packets input, 137585551813 bytes, 0 no buffer
  Received 3121569 broadcasts, 28978283 multicasts, 140800465 unicasts
  0 input errors, 0 CRC, 0 frame, 0 ignored
  0 runts, 0 giants
  84755701 packets output, 39358021133 bytes, 0 underruns
  Transmitted 495760 broadcasts, 222638 multicasts, 84037303 unicasts
  0 output errors, 0 collisions
  Relay Agent Information option: Disabled

Egress queues:
Queue counters    Queued packets    Dropped Packets
    0            76195575                   0
    1                   0                   0
    2                 494                   0
    3                   0                   0
    4                   0                   0
    5              264722                   0
    6                   6                   0
    7                   0                   0
GigEthernetmgmt1 is disabled, line protocol is down
  Hardware is GigEthernet, address is 748e.f86f.dfc0 (bia 748e.f86f.dff0)
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
!
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS018(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}