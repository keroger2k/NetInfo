using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.Cisco.IOS.Enums;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class IOSDeviceTest {
    private AssetBlob lineItems;

    [SetUp]
    public void Init() {
      lineItems = new AssetBlob {
        Body = @"
alias exec nms uNRFK-ODMN2-IOS-v18_0_0
alias exec gfetap gfetap-v1_0_0
alias exec harden uNAVY-ODMN-IOSRTR-v25_0_0
!
line con 0
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
line aux 0
 password 7 04692E425A600D793A4D571F45012237
 no exec
 transport output none
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 password 7 011F47401F5E3E282B1C743F2B071D08
 transport input none
!"
      };
    }

    [Test]
    public void ios_device_should_correctly_parse_all_lines_for_an_ios_configuration() {
      IIOSDevice device = new IOSDevice(lineItems);
      Assert.AreEqual(4, device.Lines.Count());
    }

    [Test]
    public void ios_device_should_correctly_parse_line_console_configuration() {
      IIOSDevice device = new IOSDevice(lineItems);

      IEnumerable<IOSLineItem> result = device.Lines;

      Assert.AreEqual(1, result.ElementAt(0).Commands.Count());
      Assert.AreEqual(LineType.CONSOLE, result.ElementAt(0).Type);
      Assert.AreEqual(7, result.ElementAt(0).Password.Type);
      Assert.AreEqual("011F47401F5E3E282B1C743F2B071D08", result.ElementAt(0).Password.Value);
    }

    [Test]
    public void ios_device_should_correctly_parse_aux_port_configuration() {
      IOSDevice device = new IOSDevice(lineItems);

      IEnumerable<IOSLineItem> result = device.Lines;

      Assert.AreEqual(2, result.ElementAt(1).Commands.Count());
      Assert.AreEqual(LineType.AUX, result.ElementAt(1).Type);
      Assert.AreEqual(7, result.ElementAt(1).Password.Type);
      Assert.AreEqual("04692E425A600D793A4D571F45012237", result.ElementAt(1).Password.Value);
    }

    [Test]
    public void ios_device_should_correctly_parse_vty_0_4_ports_configuration() {
      IIOSDevice device = new IOSDevice(lineItems);

      IEnumerable<IOSLineItem> result = device.Lines;

      Assert.AreEqual(3, result.ElementAt(2).Commands.Count());
      Assert.AreEqual(LineType.VTY, result.ElementAt(2).Type);
      Assert.AreEqual(7, result.ElementAt(2).Password.Type);
      Assert.AreEqual("011F47401F5E3E282B1C743F2B071D08", result.ElementAt(2).Password.Value);
    }

    [Test]
    public void ios_device_should_correctly_parse_vty_5_15_ports_configuration() {
      IIOSDevice device = new IOSDevice(lineItems);

      IEnumerable<IOSLineItem> result = device.Lines;

      Assert.AreEqual(2, result.ElementAt(3).Commands.Count());
      Assert.AreEqual(LineType.VTY, result.ElementAt(3).Type);
      Assert.AreEqual(7, result.ElementAt(3).Password.Type);
      Assert.AreEqual("011F47401F5E3E282B1C743F2B071D08", result.ElementAt(3).Password.Value);
    }

    [Test]
    public void ios_device_should_correctly_find_clock_timezone_command_and_return_the_string() {
      var config = new AssetBlob {
        Body = @"aaa accounting commands 15 default start-stop group tacacs+
!
aaa session-id common
clock timezone utc 0
ip subnet-zero
no ip source-route
no ip gratuitous-arps
!
!"
      };

      IIOSDevice device = new IOSDevice(config);

      Assert.AreEqual("utc", device.Clock.Timezone);
      Assert.AreEqual(0, device.Clock.HourOffset);
    }

    [Test]
    public void ios_device_should_correctly_find_enable_secret_password_and_return_the_string() {
      var config = new AssetBlob {
        Body = @"!
boot system flash disk0:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin
logging buffered 20000 notifications
no logging console
logging monitor informational
enable secret 5 $1$V9.G$I.wDWcXyrzQ9CPVfF3Yx3
!
username localadmin privilege 0 password 7 011F47401F5E3E282B1C743F2B071D08
aaa new-model
"
      };

      IIOSDevice device = new IOSDevice(config);

      Assert.AreEqual("$1$V9.G$I.wDWcXyrzQ9CPVfF3Yx3", device.EnableSecret.Value);
    }

    [Test]
    public void ios_device_should_return_empty_list_when_no_raidus_server_configurations_are_present() {
      var config = new AssetBlob {
        Body = @"!
empty configuration
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(0, device.RadiusServers.Count());
    }

    [Test]
    public void ios_device_should_return_list_of_radius_servers_when_present() {
      var config = new AssetBlob {
        Body = @"!
tacacs-server host 10.0.16.152
tacacs-server host 10.16.27.44
tacacs-server host 10.32.9.233
tacacs-server directed-request
tacacs-server key 7 0257451A4F56173B7D7F3E4110
radius-server attribute 32 include-in-access-req
radius-server host 10.0.11.149 auth-port 1812 acct-port 1813 key 7 133F1D0723165379180838
radius-server host 10.0.197.237 auth-port 1812 acct-port 1813 key 7 111034124F463F0D533978
radius-server transaction max-tries 3
radius-server retransmit 1
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.RadiusServers.Count());
      Assert.AreEqual("10.0.11.149", device.RadiusServers.ElementAt(0).Host.ToString());
      Assert.AreEqual("10.0.197.237", device.RadiusServers.ElementAt(1).Host.ToString());
      Assert.AreEqual("133F1D0723165379180838", device.RadiusServers.ElementAt(0).Key.Value);
      Assert.AreEqual("111034124F463F0D533978", device.RadiusServers.ElementAt(1).Key.Value);
    }

    [Test]
    public void ios_device_should_correctly_find_hostname_and_return_hostname_object() {
      var config = new AssetBlob {
        Body = @"!
hostname ABCD-U00-IR-01
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.IsNotNull(device.Hostname);
      Assert.AreEqual("ABCD-U00-IR-01", device.Hostname);
    }

    [Test]
    public void ios_device_should_correctly_find_0_interfaces_configured_on_device_with_none_configured() {
      var config = new AssetBlob {
        Body = @""
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(0, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_vlan_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"!
interface Vlan71
 description <== B1 Ironport Segment ==>
!
interface Vlan95
 description <== GTSE Connection ==>
!
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_loopback_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"!
interface Loopback1
 description <== NMCI Loopback Interface ==>
!
interface Loopback521
 description <== Tunnel Source NRFK-NFLT IT21 ==>
!
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_tunnel_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"!
interface Tunnel521
 description <== IT21 B1 Connection in NFLT==>
!
interface Tunnel903
 description <== Tunnel to GLFP GTSE ==>
!
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_port_channel_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"!
interface Port-channel101
 description <== BUNDLED PORTS G2/3-4 TO OLB01 ==>
!
interface Port-channel201
 description <== BUNDLED PORTS G4/1-2 TO OLB02 ==>
!
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_fastethernet_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"!
interface FastEthernet1/8
 description <== BUNDLED PORTS G2/3-4 TO OLB01 ==>
!
interface FastEthernet1/9
 description <== BUNDLED PORTS G4/1-2 TO OLB02 ==>
!
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_gigabitethernet_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"!
interface GigabitEthernet2/8
 description <== BUNDLED PORTS G2/3-4 TO OLB01 ==>
!
interface GigabitEthernet2/9
 description <== BUNDLED PORTS G4/1-2 TO OLB02 ==>
!
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_ten_gigabitethernet_interfaces_configured_on_device() {
      var config = new AssetBlob {
        Body = @"!
interface TenGigabitEthernet3/8
 description <== BUNDLED PORTS G2/3-4 TO OLB01 ==>
!
interface TenGigabitEthernet3/9
 description <== BUNDLED PORTS G4/1-2 TO OLB02 ==>
!
router bgp 665
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_lines_in_banner_when_ending_control_character_is_only_character_on_last_line() {
      var config = new AssetBlob {
        Body = @"!
banner login ^C

You are accessing a U.S. Government (USG) Information System (IS) that is
provided for USG-authorized use only. By using this IS (which includes any
device attached to this IS), you consent to the following conditions:

^C
alias exec utb_WAN OC3-POS_DISA_v7_3
"
      };

      IIOSDevice device = new IOSDevice(config);
      var results = device.Banner;
      Assert.AreEqual(7, results.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_all_lines_in_banner_when_ending_control_character_is_on_last_line_with_text() {
      var config = new AssetBlob {
        Body = @"!
banner login ^C

You are accessing a U.S. Government (USG) Information System (IS) that is
provided for USG-authorized use only. By using this IS (which includes any
device attached to this IS), you consent to the following conditions:^C
alias exec utb_WAN OC3-POS_DISA_v7_3
"
      };

      IIOSDevice device = new IOSDevice(config);
      var results = device.Banner;
      Assert.AreEqual(5, results.Count());
    }

    [Test]
    public void ios_device_should_correctly_find_ip_domain_with_hyphen() {
      var config = new AssetBlob {
        Body = @"ip domain-name NMCI-ISF.com"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual("NMCI-ISF.com", device.Domain);
    }

    [Test]
    public void ios_device_should_correctly_find_ip_domain_without_hyphen() {
      var config = new AssetBlob {
        Body = @"ip domain name NMCI-ISF.com"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual("NMCI-ISF.com", device.Domain);
    }

    [Test]
    public void ios_device_should_correctly_find_crypto_key_name() {
      var config = new AssetBlob {
        Body = @"Key name: PRLH-UD0-AS-01.NMCI-ISF.com"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual("NMCI-ISF.com", device.CryptoKeyName);
    }

    [Test]
    public void ios_device_should_return_false_if_boot_network_is_not_configured() {
      var config = new AssetBlob {
        Body = @""
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.False(device.BootNetwork);
    }

    [Test]
    public void ios_device_should_return_true_if_boot_network_is_configured() {
      var config = new AssetBlob {
        Body = @"boot network"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.True(device.BootNetwork);
    }

    [Test]
    public void ios_device_should_return_false_if_service_config_is_not_configured() {
      var config = new AssetBlob {
        Body = @""
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.False(device.ServiceConfig);
    }

    [Test]
    public void ios_device_should_return_true_if_service_config_is_configured() {
      var config = new AssetBlob {
        Body = @"service config"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.True(device.ServiceConfig);
    }

    [Test]
    public void ios_device_should_correctly_return_ip_ssh_settings_with_single_white_spaces() {
      var config = new AssetBlob {
        Body = @"
ip ssh time-out 30
ip ssh authentication-retries 2
ip ssh version 2
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(30, device.IPSettings.SSH.Timeout);
      Assert.AreEqual(2, device.IPSettings.SSH.AuthenticationRetries);
      Assert.AreEqual(2, device.IPSettings.SSH.Version);
    }

    [Test]
    public void ios_device_should_return_zero_for_count_when_no_settings_are_found() {
      var config = new AssetBlob {
        Body = @"!
!
!
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(0, device.AliasExecSettings.Count);
    }

    [Test]
    public void ios_device_should_return_correct_count_for_alias_exec_settings() {
      var config = new AssetBlob {
        Body = @"!
!
alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v6_0_0
alias exec tb-ban-802-1x 802_1x-Global-v2_1_0
alias exec harden uNAVY-INSIDE-SSH-IOSRTR-v24_0_0
!
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(3, device.AliasExecSettings.Count);
    }

    [Test]
    public void ios_device_should_correctly_parse_show_interface_status_command() {
      var config = new AssetBlob {
        Body = @"AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#show interface status
Port Name Status Vlan Duplex Speed Type
Fa1/0 <== NS_VPN_1 (RED) connected 30 full 100 10/100BaseTX
Fa1/1 <== DISABLED ==> disabled 2 full 100 10/100BaseTX
AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#!
AKOZ-U00-IR-01#show port-security
^"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.ShowInterfaceStatus.Interfaces.Count());
    }

    [Test]
    public void ios_device_should_return_correct_settings_for_logging() {
      var config = new AssetBlob {
        Body = @"!
!
logging trap notifications
logging source-interface Vlan30
logging 1.1.1.1
!
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual("Vlan30", device.SyslogSettings.SourceInterface);
      Assert.AreEqual("notifications", device.SyslogSettings.TrapLevel);
      Assert.AreEqual("1.1.1.1", device.SyslogSettings.Servers.ElementAt(0).ToString());
    }

    [Test]
    public void ios_device_should_return_correct_settings_for_service() {
      var config = new AssetBlob {
        Body = @"!
version 12.2
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
!
hostname ABCD-U00-IR-01
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.False(device.ServiceSettings.Pad);
      Assert.True(device.ServiceSettings.PasswordEncryption);
      Assert.True(device.ServiceSettings.TcpKeepalivesIn);
      Assert.True(device.ServiceSettings.TcpKeepalivesOut);
      Assert.False(device.ServiceSettings.TcpSmallServers);
      Assert.False(device.ServiceSettings.UdpSmallServers);
    }

    [Test]
    public void ios_device_should_return_correct_settings_for_tacacs() {
      var config = new AssetBlob {
        Body = @"snmp ifmib ifindex persist
tacacs-server host 10.16.27.44
tacacs-server host 10.0.16.152
tacacs-server host 10.32.9.233
tacacs-server directed-request
tacacs-server key 7 1446534A485432311519046D37
radius-server attribute 32 include-in-access-req"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(3, device.TacacsServer.Hosts.Count());
      Assert.AreEqual("1446534A485432311519046D37", device.TacacsServer.Key);
    }

    [Test]
    public void ios_device_should_return_correct_settings_for_network_time() {
      var config = new AssetBlob {
        Body = @"exec-timeout 3 0
password 7 011F47401F5E3E282B1C743F2B071D08
transport input none
!
ntp clock-period 36029701
ntp source Vlan30
ntp server 10.32.9.254
ntp server 10.0.16.10
ntp server 10.16.27.68
end
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(3, device.NetworkTimeProtocol.Servers.Count());
      Assert.AreEqual("Vlan30", device.NetworkTimeProtocol.SourceVlan);
    }

    [Test]
    public void ios_device_should_correct_tacacs_server_source_interface() {
      var config = new AssetBlob {
        Body = @"ip tacacs source-interface Vlan30"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual("Vlan30", device.TacacsSourceInterface);
    }

    [Test]
    public void ios_device_should_correctly_detect_if_bgp_is_configured() {
      var config = new AssetBlob {
        Body = @"!
router bgp 616
 bgp log-neighbor-changes
!
ip forward-protocol nd"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.True(device.IsBGPConfigured);
    }

    [Test]
    public void ios_device_should_correctly_detect_if_eigrp_is_configured() {
      var config = new AssetBlob {
        Body = @"!
router eigrp 1
 network 138.168.64.172 0.0.0.3
 distance eigrp 90 105
 no auto-summary
!
ip forward-protocol nd"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.True(device.IsEIGRPConfigured);
    }

    [Test]
    public void ios_device_should_correctly_detect_if_ospf_is_configured() {
      var config = new AssetBlob {
        Body = @"!
router ospf 1001
 log-adjacency-changes
!
ip classless"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.True(device.IsOSPFConfigured);
    }

    [Test]
    public void ios_device_should_correctly_parse_all_extended_access_lists() {
      var config = new AssetBlob {
        Body = @"!
ip access-list extended NMCI_Printer_VLAN_ACL_IN_V17-2-0
 permit udp any gt 1023 host 239.255.255.250 eq 1900
 deny   ip any any log
ip access-list extended NMCI_Printer_VLAN_ACL_OUT_V17-2-0
 permit udp any eq ntp any gt 1023
 deny   ip any any log
!
logging trap notifications"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(2, device.ExtendedAccessLists.Count());
      Assert.AreEqual(2, device.ExtendedAccessLists.First().Rules.Count());
      Assert.AreEqual(2, device.ExtendedAccessLists.Last().Rules.Count());
    }

    [Test]
    public void ios_device_should_correctly_parse_all_route_maps() {
      var config = new AssetBlob {
        Body = @"!
route-map ospf-to-eigrp deny 10
 match ip address 101
 match route-type external type-2
!
route-map ospf-to-eigrp permit 20
 match ip address prefix-list pfx
 set metric 40000 1000 255 1 1500
!
route-map ospf-to-eigrp permit 30
 set tag 8
!
logging trap notifications"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(3, device.RouteMaps.Count());
      Assert.AreEqual(2, device.RouteMaps.First().Matches.Count());
      Assert.AreEqual(101, device.RouteMaps.First().GetMatchStandardAccessLists().ElementAt(0));
    }

    [Test]
    public void ios_device_should_correctly_parse_all_standard_access_lists() {
      var config = new AssetBlob {
        Body = @"logging 10.16.27.43
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 deny   any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.0.11.0 0.0.0.255
access-list 99 deny   any log
no cdp run"
      };

      IIOSDevice device = new IOSDevice(config);
      var acls = device.StandardAccessLists.ToList();
      Assert.AreEqual(2, acls.Count());
      Assert.AreEqual(3, acls.First().Rules.Count());
      Assert.AreEqual(3, acls.Last().Rules.Count());
    }

    [Test]
    public void ios_device_should_correctly_parse_dot1x_system_auth_control() {
      var config = new AssetBlob {
        Body = @"!
dot1x system-auth-control
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.True(device.Dot1xSettings.SystemAuthControl);
    }

    [Test]
    public void ios_device_should_correctly_parse_vlan_numbers_and_their_descriptions() {
      var config = new AssetBlob {
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
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(4, device.Vlans.Count());
    }

    [Test]
    public void ios_device_should_correctly_determine_if_is_managed() {
      var config = new AssetBlob {
        Body = @"!
line con 0
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
line vty 0 4
 exec-timeout 3 0
 transport input none
line vty 5 15
 exec-timeout 3 0
 transport input none
!
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.False(device.IsManaged);
    }

    [Test]
    public void ios_device_should_correctly_parse_ios_image_names_1() {
      var config = new AssetBlob {
        Body = @"PRLH-U00-BI-01#show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2009 by cisco Systems, Inc.
Compiled Fri 25-Sep-09 10:03 by ccai
Image text-base: 0x40101040, data-base: 0x42DDB970

ROM: System Bootstrap, Version 12.2(17r)SX7, RELEASE SOFTWARE (fc1)
BOOTLDR: s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)

PRLH-U00-BI-01 uptime is 1 year, 14 weeks, 4 days, 6 hours, 2 minutes
Time since PRLH-U00-BI-01 switched to active is 1 year, 14 weeks, 4 days, 6 hours, 10 minutes
System returned to ROM by reload at 20:06:16 UTC Sun Aug 29 2010 (SP by reload)
System restarted at 20:09:32 utc Sun Aug 29 2010
System image file is ""sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin""

This product contains cryptographic features and is subject to United
States and local country laws governing import, export, transfer and
use. Delivery of Cisco cryptographic products does not imply
third-party authority to import, export, distribute or use encryption.
Importers, exporters, distributors and users are responsible for
compliance with U.S. and local country laws. By using this product you
agree to comply with applicable laws and regulations. If you are unable
to comply with U.S. and local laws, return this product immediately.

A summary of U.S. laws governing Cisco cryptographic products may be found at:
http://www.cisco.com/wwl/export/crypto/tool/stqrg.html

If you require further assistance please contact us by sending email to
export@cisco.com.

cisco WS-C6509 (R7000) processor (revision 3.0) with 458720K/65536K bytes of memory.
Processor board ID TBM06031680
SR71000 CPU at 600Mhz, Implementation 0x504, Rev 1.2, 512KB L2 Cache
Last reset from s/w reset
SuperLAT software (copyright 1990 by Meridian Technology Corp).
X.25 software, Version 3.0.0.
Bridging software.
TN3270 Emulation software.
1 SIP-400 controller (1 POS).
6 Virtual Ethernet/IEEE 802.3 interfaces
34 Gigabit Ethernet/IEEE 802.3 interfaces
1 Packet over SONET network interface
1917K bytes of non-volatile configuration memory.
8192K bytes of packet buffer memory.

65536K bytes of Flash internal SIMM (Sector size 512K).
Configuration register is 0x2102

PRLH-U00-BI-01#!
"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(12, device.Image.Major);
      Assert.AreEqual(2, device.Image.Minor);
      Assert.AreEqual(18, device.Image.Release);
    }

    [Test]
    public void ios_device_should_correctly_device_model_when_there_is_no_show_inventory() {
      var config = new AssetBlob {
        Body = @"NSAA-U01-DH-01#show version
Cisco IOS Software, 3800 Software (C3845-ADVIPSERVICESK9-M), Version 12.4(25f), RELEASE SOFTWARE (fc2)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2011 by Cisco Systems, Inc.
Compiled Tue 16-Aug-11 09:31 by prod_rel_team

ROM: System Bootstrap, Version 12.4(13r)T11, RELEASE SOFTWARE (fc1)

NSAA-U01-DH-01 uptime is 26 weeks, 6 days, 13 hours, 39 minutes
System returned to ROM by reload at 02:33:34 utc Tue Jul 24 2012
System restarted at 02:34:44 utc Tue Jul 24 2012
System image file is ""flash:c3845-advipservicesk9-mz.124-25f.bin""

This product contains cryptographic features and is subject to United
States and local country laws governing import, export, transfer and
use. Delivery of Cisco cryptographic products does not imply
third-party authority to import, export, distribute or use encryption.
Importers, exporters, distributors and users are responsible for
compliance with U.S. and local country laws. By using this product you
agree to comply with applicable laws and regulations. If you are unable
to comply with U.S. and local laws, return this product immediately.

A summary of U.S. laws governing Cisco cryptographic products may be found at:
http://www.cisco.com/wwl/export/crypto/tool/stqrg.html

If you require further assistance please contact us by sending email to
export@cisco.com.

Cisco 3845 (revision 1.0) with 484352K/39936K bytes of memory.
Processor board ID FTX1409AK3G
7 DSL controllers
2 Gigabit Ethernet interfaces
4294967294 Serial interfaces
7 ATM interfaces
1 Virtual Private Network (VPN) Module
DRAM configuration is 64 bits wide with parity enabled.
479K bytes of NVRAM.
126000K bytes of ATA System CompactFlash (Read/Write)

Configuration register is 0x2102

NSAA-U01-DH-01#!
NSAA-U01-DH-01#!
NSAA-U01-DH-01#show inventory
NSAA-U01-DH-01#!
NSAA-U01-DH-01#!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual("3845", device.Model);
    }

    [Test]
    public void ios_device_should_correctly_handle_atm_interfaces_with_pvcs() {
      var config = new AssetBlob {
        Body = @"!
interface Loopback0
 no ip address
!
interface BRI0
 description <== Not Used ==>
 no ip address
 encapsulation hdlc
 shutdown
!
interface ATM0
 description <== MECH-U01-DH-01 ==>
 no ip address
 no atm ilmi-keepalive
!
interface ATM0.1 point-to-point
 description <== User PVC ==>
 snmp trap link-status
 pvc 0/21
  encapsulation aal5snap
 !
 bridge-group 21
!
interface ATM0.2 point-to-point
 description <== Printer PVC ==>
 snmp trap link-status
 pvc 0/51
  encapsulation aal5snap
 !
 bridge-group 51
!
interface ATM0.3 point-to-point
 description <== Management PVC ==>
 snmp trap link-status
 pvc 0/99
  encapsulation aal5snap
 !
 cdp enable
 bridge-group 99
!
interface FastEthernet0
 description <== End Device VLAN ==>
 switchport access vlan 210
 duplex full
 speed 100
 no snmp trap link-status
 no cdp enable
 spanning-tree portfast
!
interface FastEthernet1
 description <== End Device VLAN ==>
 switchport access vlan 210
 duplex full
 no snmp trap link-status
 no cdp enable
 spanning-tree portfast
!
interface BVI99
 description <== Virtual Mgmt Interface ==>
 ip address 10.22.2.166 255.255.254.0
 no ip redirects
 no ip proxy-arp
!
ip forward-protocol nd
ip route 0.0.0.0 0.0.0.0 10.22.2.1
!
!
!"
      };

      IIOSDevice device = new IOSDevice(config);
      Assert.AreEqual(9, device.Interfaces.Count());
    }
  }
}