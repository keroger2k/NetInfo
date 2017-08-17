using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS039_Tests {

    [Test]
    public void BS039_should_return_true_when_all_access_ports_have_dot1x_port_control_auto() {
      var blob = new AssetBlob {
        Body = @"!
!
interface management 1
 disable
!
interface ethernet 1/1/1
 port-name U04_DR01_G2/11
!
interface ethernet 1/1/2
 port-name U04_DR02_G2/11
!
interface ethernet 1/1/3
 dot1x port-control auto
 port-name 001D1-00CCU-2020
 stp-bpdu-guard
 no snmp-server enable traps link-change
!
SSH@ALTN-U01-AS-12#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c0)
  Member of 13 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is down, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c1)
  Member of 13 L2 VLANs, port is tagged, port state is BLOCKING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/3 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c2 (bia 748e.f82e.30c2)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS039(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS039_should_return_false_when_not_all_access_ports_have_dot1x_port_control_auto() {
      var blob = new AssetBlob {
        Body = @"!
!
interface management 1
 disable
!
interface ethernet 1/1/1
 port-name U04_DR01_G2/11
!
interface ethernet 1/1/2
 port-name U04_DR02_G2/11
!
interface ethernet 1/1/3
 port-name 001D1-00CCU-2020
 stp-bpdu-guard
 no snmp-server enable traps link-change
!
SSH@ALTN-U01-AS-12#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c0)
  Member of 13 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is down, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c1)
  Member of 13 L2 VLANs, port is tagged, port state is BLOCKING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/3 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c2 (bia 748e.f82e.30c2)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS039(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS039_should_not_consider_trunk_ports_stack_ports_or_disabled_ports() {
      var blob = new AssetBlob {
        Body = @"!
!
interface management 1
 disable
!
interface ethernet 1/1/1
 port-name U04_DR01_G2/11
!
interface ethernet 1/1/2
 port-name U04_DR02_G2/11
!
interface ethernet 1/1/3
 port-name 001D1-00CCU-2020
 stp-bpdu-guard
 no snmp-server enable traps link-change
!
interface ethernet 1/1/4
 dot1x port-control auto
 port-name 001D1-00CCU-2020
 stp-bpdu-guard
 no snmp-server enable traps link-change
!
SSH@ALTN-U01-AS-12#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c0)
  Member of 13 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is down, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c1)
  Member of 13 L2 VLANs, port is tagged, port state is BLOCKING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/3 is disabled, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.30c2 (bia 748e.f82e.30c2)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/4 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c2 (bia 748e.f82e.30c2)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS039(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS039_should_return_true_if_access_port_is_configured_with_mac_authentication_enable() {
      var blob = new AssetBlob {
        Body = @"!
interface ethernet 1/1/1
 port-name U00_IR01_G3/7
!
interface ethernet 1/1/2
 port-name U00_IR02_G3/7
!
interface ethernet 1/1/3
 dot1x port-control auto
 port-name DISABLED
 disable
 speed-duplex 100-full
 stp-bpdu-guard
 no snmp-server enable traps link-change
!
interface ethernet 1/1/4
 dot1x port-control auto
 port-name NC-XXXXX-004D1
 stp-bpdu-guard
 no snmp-server enable traps link-change
!
interface ethernet 1/1/5
 port-name NP-XXXXX-005D1
 speed-duplex 100-full
 mac-authentication enable
 stp-bpdu-guard
 no snmp-server enable traps link-change
!
SSH@ALTN-U01-AS-12#show interfaces
GigabitEthernet1/1/1 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c0)
  Member of 13 L2 VLANs, port is tagged, port state is FORWARDING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/2 is down, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.30c0 (bia 748e.f82e.30c1)
  Member of 13 L2 VLANs, port is tagged, port state is BLOCKING
  BPDU guard is Disabled, ROOT protect is Disabled
GigabitEthernet1/1/3 is disabled, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.30c2 (bia 748e.f82e.30c2)
  Member of L2 VLAN ID 2, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/4 is down, line protocol is down
  Hardware is GigabitEthernet, address is 748e.f82e.30c3 (bia 748e.f82e.30c3)
  Member of L2 VLAN ID 217, port is untagged, port state is DISABLED
  BPDU guard is Enabled, ROOT protect is Disabled
GigabitEthernet1/1/5 is up, line protocol is up
  Hardware is GigabitEthernet, address is 748e.f82e.30c4 (bia 748e.f82e.30c4)
  Member of L2 VLAN ID 500, port is untagged, port state is FORWARDING
  BPDU guard is Enabled, ROOT protect is Disabled
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS039(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}