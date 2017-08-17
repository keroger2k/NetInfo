using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS000_Tests {

    [Test]
    public void BS000_should_return_true_when_test_script_is_correctly_applied() {
      var blob = new AssetBlob {
        Body = @"SSH@ALTN-U01-AS-12#write memory
SSH@ALTN-U01-AS-12#skip-page-display
SSH@ALTN-U01-AS-12#show config
SSH@ALTN-U01-AS-12#show running-config
SSH@ALTN-U01-AS-12#show version
SSH@ALTN-U01-AS-12#show dot1x
SSH@ALTN-U01-AS-12#show default value
SSH@ALTN-U01-AS-12#show clock
SSH@ALTN-U01-AS-12#show sntp association
SSH@ALTN-U01-AS-12#show sntp status
SSH@ALTN-U01-AS-12#show boot-preference
SSH@ALTN-U01-AS-12#show flash
SSH@ALTN-U01-AS-12#show files
SSH@ALTN-U01-AS-12#show module
SSH@ALTN-U01-AS-12#show chassis
SSH@ALTN-U01-AS-12#show stack detail
SSH@ALTN-U01-AS-12#show dot1x mac-sessions
SSH@ALTN-U01-AS-12#show auth-mac-addresses
SSH@ALTN-U01-AS-12#show auth-mac-address unauthorized-mac
SSH@ALTN-U01-AS-12#show interfaces brief
SSH@ALTN-U01-AS-12#show interfaces brief | inc Yes
SSH@ALTN-U01-AS-12#show interfaces brief | exclude unassigned
SSH@ALTN-U01-AS-12#show interface | in line | name
SSH@ALTN-U01-AS-12#show interfaces stack-ports
SSH@ALTN-U01-AS-12#show trunk
SSH@ALTN-U01-AS-12#show link-aggregate
SSH@ALTN-U01-AS-12#show port security
SSH@ALTN-U01-AS-12#show port security statistics
SSH@ALTN-U01-AS-12#show port security mac
SSH@ALTN-U01-AS-12#show mac-address all
SSH@ALTN-U01-AS-12#show ip interface
SSH@ALTN-U01-AS-12#show ip interface ve 99
SSH@ALTN-U01-AS-12#show ip route
SSH@ALTN-U01-AS-12#show ip access-lists
SSH@ALTN-U01-AS-12#show ip access-list | include Standard|Extended
SSH@ALTN-U01-AS-12#show arp
SSH@ALTN-U01-AS-12#show users
SSH@ALTN-U01-AS-12#show aaa
SSH@ALTN-U01-AS-12#show snmp
SSH@ALTN-U01-AS-12#show snmp user
SSH@ALTN-U01-AS-12#show snmp server
SSH@ALTN-U01-AS-12#show snmp group
SSH@ALTN-U01-AS-12#show lldp local-info
SSH@ALTN-U01-AS-12#show lldp neighbors
SSH@ALTN-U01-AS-12#show lldp neighbors detail
SSH@ALTN-U01-AS-12#show vlan brief
SSH@ALTN-U01-AS-12#show vlan
SSH@ALTN-U01-AS-12#show vlan | include Name
SSH@ALTN-U01-AS-12#show vlan | include Tagged
SSH@ALTN-U01-AS-12#show span detail
SSH@ALTN-U01-AS-12#show span detail | include ACTIVE
SSH@ALTN-U01-AS-12#show span detail | include not configured
SSH@ALTN-U01-AS-12#show monitor
SSH@ALTN-U01-AS-12#show cpu
SSH@ALTN-U01-AS-12#show process cpu
SSH@ALTN-U01-AS-12#show memory
SSH@ALTN-U01-AS-12#show memory tcp
SSH@ALTN-U01-AS-12#show logging
SSH@ALTN-U01-AS-12#show debug
SSH@ALTN-U01-AS-12#show configuration | include alias
SSH@ALTN-U01-AS-12#show interfaces
SSH@ALTN-U01-AS-12#!END-OF-TEST-SCRIPT"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS000_should_return_false_when_test_script_is_incorrectly_applied() {
      var blob = new AssetBlob {
        Body = @"SSH@ALTN-U01-AS-12#write memory1
SSH@ALTN-U01-AS-12#skip-page-display
SSH@ALTN-U01-AS-12#show config
SSH@ALTN-U01-AS-12#show version
SSH@ALTN-U01-AS-12#show dot1x
SSH@ALTN-U01-AS-12#show default value
SSH@ALTN-U01-AS-12#show clock
SSH@ALTN-U01-AS-12#show sntp association
SSH@ALTN-U01-AS-12#show sntp status
SSH@ALTN-U01-AS-12#show boot-preference
SSH@ALTN-U01-AS-12#show flash
SSH@ALTN-U01-AS-12#show files
SSH@ALTN-U01-AS-12#show module
SSH@ALTN-U01-AS-12#show chassis
SSH@ALTN-U01-AS-12#show stack detail
SSH@ALTN-U01-AS-12#show dot1x mac-sessions
SSH@ALTN-U01-AS-12#show auth-mac-addresses
SSH@ALTN-U01-AS-12#show auth-mac-address unauthorized-mac
SSH@ALTN-U01-AS-12#show interfaces brief
SSH@ALTN-U01-AS-12#show interfaces brief | inc Yes
SSH@ALTN-U01-AS-12#show interfaces brief | exclude unassigned
SSH@ALTN-U01-AS-12#show interface | in line | name
SSH@ALTN-U01-AS-12#show interfaces stack-ports
SSH@ALTN-U01-AS-12#show trunk
SSH@ALTN-U01-AS-12#show link-aggregate
SSH@ALTN-U01-AS-12#show port security
SSH@ALTN-U01-AS-12#show port security statistics
SSH@ALTN-U01-AS-12#show port security mac
SSH@ALTN-U01-AS-12#show mac-address all
SSH@ALTN-U01-AS-12#show ip interface
SSH@ALTN-U01-AS-12#show ip interface ve 99
SSH@ALTN-U01-AS-12#show ip route
SSH@ALTN-U01-AS-12#show ip access-lists
SSH@ALTN-U01-AS-12#show ip access-list | include Standard|Extended
SSH@ALTN-U01-AS-12#show arp
SSH@ALTN-U01-AS-12#show users
SSH@ALTN-U01-AS-12#show aaa
SSH@ALTN-U01-AS-12#show snmp user
SSH@ALTN-U01-AS-12#show snmp server
SSH@ALTN-U01-AS-12#show snmp group
SSH@ALTN-U01-AS-12#show lldp local-info
SSH@ALTN-U01-AS-12#show lldp neighbors
SSH@ALTN-U01-AS-12#show lldp neighbors detail
SSH@ALTN-U01-AS-12#show vlan brief
SSH@ALTN-U01-AS-12#show vlan
SSH@ALTN-U01-AS-12#show vlan | include Name
SSH@ALTN-U01-AS-12#show vlan | include Tagged
SSH@ALTN-U01-AS-12#show span detail
SSH@ALTN-U01-AS-12#show span detail | include ACTIVE
SSH@ALTN-U01-AS-12#show span detail | include not configured
SSH@ALTN-U01-AS-12#show monitor
SSH@ALTN-U01-AS-12#show cpu
SSH@ALTN-U01-AS-12#show process cpu
SSH@ALTN-U01-AS-12#show memory
SSH@ALTN-U01-AS-12#show memory tcp
SSH@ALTN-U01-AS-12#show logging
SSH@ALTN-U01-AS-12#show debug
SSH@ALTN-U01-AS-12#show configuration | include alias
SSH@ALTN-U01-AS-12#show interfaces
SSH@ALTN-U01-AS-12#!END-OF-TEST-SCRIPT"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS000(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}