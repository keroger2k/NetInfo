using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR162_Tests {

    [Test]
    public void IR162_should_return_true_when_acl_is_exactly_like_the_acl_standards() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip access-list extended OR01_NMCI_UTB_OUT_V1-1-0
 remark block source illegitimate source addresses . NET0950 - RFC3330
 deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip 255.255.255.255 0.0.0.0 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 deny   udp any any eq 3544 log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR162(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR162_should_return_false_when_acl_is_not_correct() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip access-list extended OR01_NMCI_UTB_OUT_V1-1-0
 remark block source illegitimate source addresses . NET0950 - RFC3330
 deny   ip 255.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip 255.255.255.255 0.0.0.0 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR162(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR162_should_return_false_when_acl_contains_an_extra_line() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip access-list extended OR01_NMCI_UTB_OUT_V1-1-0
 remark block source illegitimate source addresses . NET0950 - RFC3330
 deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 1.1.1.1 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip 255.255.255.255 0.0.0.0 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR162(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR162_should_return_false_when_acl_is_short_one_line() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip access-list extended OR01_NMCI_UTB_OUT_V1-1-0
 remark block source illegitimate source addresses . NET0950 - RFC3330
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip 255.255.255.255 0.0.0.0 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR162(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR162_should_return_true_when_acl_gets_converted_to_deny_ip_host_command() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip access-list extended OR01_NMCI_UTB_OUT_V1-1-0
 remark block source illegitimate source addresses . NET0950 - RFC3330
 deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip host 255.255.255.255 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 deny   udp any any eq 3544 log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR162(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR162_should_return_false_when_uTB_Acl_is_applied_configured_multiple_times() {
      AssetBlob blob = new AssetBlob {
        Body = @"ip access-list extended OR01_NMCI_UTB_OUT_V2-0-0
 remark block source illegitimate source addresses . NET0950 - RFC3330
 deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip host 255.255.255.255 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!
ip access-list extended OR01_NMCI_UTB_OUT_V2-0-1
 remark block source illegitimate source addresses . NET0950 - RFC3330
 deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip host 255.255.255.255 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR162(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR162_should_only_get_distinct_acl_names_not_multiples_in_show_config_and_show_running() {
      AssetBlob blob = new AssetBlob {
        Body = @"ALAP-U00-OR-01#! !#**************************************************************************************#
ALAP-U00-OR-01#! !#***  Version: Cisco IOS Test Script Version 5.23
ALAP-U00-OR-01#! !#**************************************************************************************#
ALAP-U00-OR-01#! !#***  Device:  Use on any Cisco device running IOS
ALAP-U00-OR-01#! !#**************************************************************************************#
ALAP-U00-OR-01#! !#***  Purpose: Use this script to gather data for further analysis
ALAP-U00-OR-01#! !#**************************************************************************************#
ALAP-U00-OR-01#! !#    NOTE: Ignore any errors due to syntax or missing hardware
ALAP-U00-OR-01#! !#**************************************************************************************#
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#terminal length 0
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#write mem
Building configuration...

[OK]
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#show config
Using 17504 out of 245752 bytes
!
! Last configuration change at 00:41:53 utc Wed Nov 20 2013 by nasadmin
! NVRAM config last updated at 21:40:52 utc Wed Dec 11 2013 by cmang
!
version 12.4
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
no service dhcp
!
hostname ALAP-U00-OR-01
!
ip as-path access-list 10 permit ^$
!
no ip http server
no ip http secure-server
ip tacacs source-interface Vlan23
!
ip access-list extended ODMN_ONLY_TRAFFIC_ACL_V2-0-0
 permit ip 172.16.0.0 0.15.255.255 172.16.0.0 0.15.255.255
 deny   ip any any log
ip access-list extended OR01_NMCI_BGP_VPN_FILTER_IN_V8-3-0
 remark IPsec to uTB Netscreen - NET0162
 permit esp 138.162.0.0 0.1.255.255 138.163.15.0 0.0.0.63
 permit udp 138.162.0.0 0.1.255.255 138.163.15.0 0.0.0.63 eq isakmp
 remark Permit BGP for DISA/COINS
 permit tcp host 198.26.89.153 host 198.26.89.154 eq bgp
 permit tcp host 198.26.89.153 eq bgp host 198.26.89.154 gt 1024
 remark Permit ICMP ECHO/REPLY - STIG NET0911
 permit icmp any 138.163.15.0 0.0.0.63 echo
 permit icmp any 138.163.15.0 0.0.0.63 echo-reply
 permit icmp host 198.26.89.153 host 198.26.89.154 echo
 permit icmp host 198.26.89.153 host 198.26.89.154 echo-reply
 deny   icmp any any log
 remark SSH to NAVY Netscreen
 permit tcp 138.162.4.32 0.0.0.31 host 138.163.15.44 eq 22
 permit tcp 138.163.4.32 0.0.0.31 host 138.163.15.44 eq 22
 permit tcp 138.163.132.32 0.0.0.31 host 138.163.15.44 eq 22
 remark DEFAULT Deny/Log
 remark Addresses STIG Requirements NET0918 NET0923 NET0924 NET0926 NET0927 NET0940
 deny   ip any any log
ip access-list extended OR01_NMCI_UTB_OUT_V2-0-0
 remark block source illegitimate source addresses - NET0950 - RFC3330
 deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip host 255.255.255.255 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 remark BLOCK_TEREDO - STIG NET-TUNL-020
 deny   udp any any eq 3544 log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!
!
ip prefix-list NMCI seq 5 permit 138.162.0.0/16 ge 17
ip prefix-list NMCI seq 10 permit 138.163.0.0/16 ge 17
ntp server 172.18.1.21 key 1
!
end

ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#!
ALAP-U00-OR-01#show running-config
Building configuration...

Current configuration : 17504 bytes
!
! Last configuration change at 00:41:53 utc Wed Nov 20 2013 by nasadmin
! NVRAM config last updated at 21:40:52 utc Wed Dec 11 2013 by cmang
!
version 12.4
no service pad
service tcp-keepalives-in
service tcp-keepalives-out
service timestamps debug datetime
service timestamps log datetime
service password-encryption
service linenumber
no service dhcp
!
hostname ALAP-U00-OR-01
!
ip as-path access-list 10 permit ^$
!
no ip http server
no ip http secure-server
ip tacacs source-interface Vlan23
!
ip access-list extended ODMN_ONLY_TRAFFIC_ACL_V2-0-0
 permit ip 172.16.0.0 0.15.255.255 172.16.0.0 0.15.255.255
 deny   ip any any log
ip access-list extended OR01_NMCI_BGP_VPN_FILTER_IN_V8-3-0
 remark IPsec to uTB Netscreen - NET0162
 permit esp 138.162.0.0 0.1.255.255 138.163.15.0 0.0.0.63
 permit udp 138.162.0.0 0.1.255.255 138.163.15.0 0.0.0.63 eq isakmp
 remark Permit BGP for DISA/COINS
 permit tcp host 198.26.89.153 host 198.26.89.154 eq bgp
 permit tcp host 198.26.89.153 eq bgp host 198.26.89.154 gt 1024
 remark Permit ICMP ECHO/REPLY - STIG NET0911
 permit icmp any 138.163.15.0 0.0.0.63 echo
 permit icmp any 138.163.15.0 0.0.0.63 echo-reply
 permit icmp host 198.26.89.153 host 198.26.89.154 echo
 permit icmp host 198.26.89.153 host 198.26.89.154 echo-reply
 deny   icmp any any log
 remark SSH to NAVY Netscreen
 permit tcp 138.162.4.32 0.0.0.31 host 138.163.15.44 eq 22
 permit tcp 138.163.4.32 0.0.0.31 host 138.163.15.44 eq 22
 permit tcp 138.163.132.32 0.0.0.31 host 138.163.15.44 eq 22
 remark DEFAULT Deny/Log
 remark Addresses STIG Requirements NET0918 NET0923 NET0924 NET0926 NET0927 NET0940
 deny   ip any any log
ip access-list extended OR01_NMCI_UTB_OUT_V2-0-0
 remark block source illegitimate source addresses - NET0950 - RFC3330
 deny   ip 0.0.0.0 0.255.255.255 any log
 deny   ip 10.0.0.0 0.255.255.255 any log
 deny   ip 14.0.0.0 0.255.255.255 any log
 deny   ip 24.0.0.0 0.255.255.255 any log
 deny   ip 39.0.0.0 0.255.255.255 any log
 deny   ip 127.0.0.0 0.255.255.255 any log
 deny   ip 128.0.0.0 0.0.255.255 any log
 deny   ip 169.254.0.0 0.0.255.255 any log
 deny   ip 172.16.0.0 0.15.255.255 any log
 deny   ip 191.255.0.0 0.0.255.255 any log
 deny   ip 192.0.0.0 0.0.0.255 any log
 deny   ip 192.0.2.0 0.0.0.255 any log
 deny   ip 192.88.99.0 0.0.0.255 any log
 deny   ip 192.168.0.0 0.0.255.255 any log
 deny   ip 198.18.0.0 0.1.255.255 any log
 deny   ip 198.51.100.0 0.0.0.255 any log
 deny   ip 203.0.113.0 0.0.0.255 any log
 deny   ip 223.255.255.0 0.0.0.255 any log
 deny   ip 224.0.0.0 15.255.255.255 any log
 deny   ip 240.0.0.0 15.255.255.255 any log
 deny   ip host 255.255.255.255 any log
 remark - ALLOW ICMP ACCESS AND TYPE CODES OUTBOUND - STIG NET0912
 permit icmp any any echo
 permit icmp any any echo-reply
 permit icmp any any packet-too-big
 permit icmp any any source-quench
 permit icmp any any time-exceeded
 permit icmp any any parameter-problem
 deny   icmp any any log
 remark BLOCK_TEREDO - STIG NET-TUNL-020
 deny   udp any any eq 3544 log
 remark allow legitimate traffic
 permit ip any any
 remark DEFAULT Deny/Log
 deny   ip any any log
!
!
ip prefix-list NMCI seq 5 permit 138.162.0.0/16 ge 17
!
end

ALAP-U00-OR-01#!
ALAP-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR162(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}