using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR000_Tests {

    [Test]
    public void IR000_should_return_true_when_a_test_script_is_correctly_applied_to_a_device() {
      var blob = new AssetBlob {
        Body = @"PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#terminal length 0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#write mem
Building configuration...
[OK]
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show running-config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show hardware
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform
% Incomplete command.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show clock
19:27:54.179 utc Thu Jan 16 2014
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ntp association

      address         ref clock     st  when  poll reach  delay  offset    disp
*~10.32.9.254      .GPS.             1   476  1024  377     1.1    0.26     0.5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show boot
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show bootvar
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#dir all-filesystems
Directory of system:/

    2  dr-x           0                    <no date>  memory
    1  -rw-      155801                    <no date>  running-config
   12  dr-x           0                    <no date>  vfiles

No space information available

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show idprom all
IDPROM for backplane #0
  (FRU is 'Catalyst 6500 9-slot backplane')
  OEM String = 'Cisco Systems'
  Product Number = 'WS-C6509'
  Serial Number = 'SMG0617A07E'
  Manufacturing Assembly Number = '73-3438-05'
  Manufacturing Assembly Revision = 'A0'
  Hardware Revision = 3.0
  Current supplied (+) or consumed (-) =  -

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diag

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diagbus

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform hardware capacity
System Resources
  PFC operating mode: PFC3B
  Supervisor redundancy mode: administratively sso, operationally sso

Multicast LTL Resources
  Usage:   30656 Total, 593 Used
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show fabric channel-counters

 slot channel   rxErrors   txErrors    txDrops  lbusDrops
    3       0          0          0          0          0
    4       0          0          0          0          0
    5       0          0          0          0          0

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vstack
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show module
Mod Ports Card Type                              Model              Serial No.
--- ----- -------------------------------------- ------------------ -----------
  3   16  SFM-capable 16 port 1000mb GBIC        WS-X6516-GBIC      SAD054700TU
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment status
backplane:
  operating clock count: 2
  operating VTT count: 3

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment power
                                ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show env all
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment all
                                  ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show power
system power redundancy mode = redundant
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers
Interface information:
	Interface EOBC0/0 (idb = 0x50D1AC48)
	Hardware is Mistral EOBC (revision 5)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers T1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show line aux 0
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface
Vlan1 is administratively down, line protocol is down
  Hardware is EtherSVI, address is 0009.44d3.4800 (bia 0009.44d3.4800)
  MTU 1500 bytes, BW 1000000 Kbit, DLY 10 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  Keepalive not supported
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/40 (size/max)
  5 minute input rate 0 bits/sec, 0 packets/sec
  5 minute output rate 0 bits/sec, 0 packets/sec
     0 packets input, 0 bytes, 0 no buffer
     Received 0 broadcasts (0 IP multicasts)
     0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored
     0 packets output, 0 bytes, 0 underruns
     0 output errors, 1 interface resets
     0 output buffer failures, 0 output buffers swapped out
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interfaces trunk

Port          Mode         Encapsulation  Status        Native vlan
Gi3/1         on           802.1q         trunking      1
Gi3/2         on           802.1q         trunking      1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface status

Port    Name               Status       Vlan       Duplex  Speed Type
Gi3/1   <== U00_CS03_G4/1  connected    trunk         full   1000 1000BaseLH
Gi3/2   <== U00_CS04_G4/1  connected    trunk         full   1000 1000BaseLH
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port-security
Secure Port  MaxSecureAddr  CurrentAddr  SecurityViolation  Security Action
                (Count)       (Count)          (Count)
---------------------------------------------------------------------------
---------------------------------------------------------------------------
Total Addresses in System (excluding one mac per port)     : 0
Max Addresses limit in System (excluding one mac per port) : 4096

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port security
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all summary
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all detail
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show authentication session
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm pvc
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm traffic
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ppp multilink
No active bundles
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show etherchannel summary
Flags:  D - down        P - bundled in port-channel
        I - stand-alone s - suspended
        H - Hot-standby (LACP only)
        R - Layer3      S - Layer2
        U - in use      f - failed to allocate aggregator

        M - not in use, minimum links not met
        u - unsuitable for bundling
        w - waiting to be aggregated
Number of channel-groups in use: 1
Number of aggregators:           1

Group  Port-channel  Protocol    Ports
------+-------------+-----------+-----------------------------------------------
112    Po112(SU)       PAgP      Gi5/1(P)   Gi5/2(P)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lacp neighbor

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac address-table
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac-address-table
Legend: * - primary entry
        age - seconds since last seen
        n/a - not available

  vlan   mac address     type    learn     age              ports
------+----------------+--------+-----+----------+--------------------------
*  607  3333.0000.0001    static  Yes          -   Switch,Stby-Switch
*  271  6c3b.e540.88c5   dynamic  Yes        255   Fa6/10

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show protocol
Global values:
  Internet Protocol routing is enabled
Vlan1 is administratively down, line protocol is down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface brief | exclude unassigned
Interface                  IP-Address      OK? Method Status                Protocol
Vlan107                    10.32.241.130   YES NVRAM  up                    up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip route
Codes: C - connected, S - static, R - RIP, M - mobile, B - BGP
       D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area
       N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
       E1 - OSPF external type 1, E2 - OSPF external type 2, E - EGP
       i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
       ia - IS-IS inter area, * - candidate default, U - per-user static route
       o - ODR, P - periodic downloaded static route

Gateway of last resort is 10.32.2.1 to network 0.0.0.0

     205.85.16.0/26 is subnetted, 1 subnets
O E2    205.85.16.64 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                     [110/20] via 10.32.2.65, 06:45:29, Vlan191
O E2 205.85.17.0/24 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                    [110/20] via 10.32.2.65, 06:45:29, Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp summary
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp neighbor
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf
 Routing Process ""ospf 1001"" with ID 10.32.10.209
 Start time: 00:01:11.092, Time elapsed: 4w4d
 Supports only single TOS(TOS0) routes

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf statistics

OSPF process ID 1001
------------------------------------------

  Area 0: SPF algorithm executed 29 times

  Area 7: SPF algorithm executed 4 times

  Summary OSPF SPF statistic

  SPF calculation time
Delta T   Intra	D-Intra	Summ	D-Summ	Ext	D-Ext	Total	Reason
2w3d   0	4	4	4	4	0	16	R,

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf neighbor

Neighbor ID     Pri   State           Dead Time   Address         Interface
10.32.10.197      1   2WAY/DROTHER    00:00:32    10.32.2.69      Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf interface
Loopback0 is up, line protocol is up
  Internet Address 10.32.10.209/32, Area 0
  Process ID 1001, Router ID 10.32.10.209, Network Type LOOPBACK, Cost: 1
  Loopback interface is treated as a stub Host
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf database

            OSPF Router with ID (10.32.10.209) (Process ID 1001)

		Router Link States (Area 0)

Link ID         ADV Router      Age         Seq#       Checksum Link count
10.32.10.97     10.32.10.97     1239        0x80004457 0x000AD7 13
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp neighbor
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp topology

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip cef
Prefix              Next Hop             Interface
0.0.0.0/0           10.32.2.1            Vlan190
                    10.32.2.65           Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat statistics
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat translations
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list
Standard IP access list 69
    10 permit 10.16.27.32, wildcard bits 0.0.0.31
    20 permit 10.0.18.240, wildcard bits 0.0.0.7
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list | include Standard|Extended
Standard IP access list 69
Standard IP access list 99
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-1
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-1
Extended IP access list NMCI_Printer_VLAN_ACL_IN_V17-3-2
Extended IP access list NMCI_Printer_VLAN_ACL_OUT_V17-3-2
Extended IP access list VTC_Endpoint_OUT_v2-0-0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show arp
Protocol  Address          Age (min)  Hardware Addr   Type   Interface
Internet  10.32.24.50             0   b499.bae3.5f3d  ARPA   Vlan271
Internet  10.32.23.61             6   6431.502c.e5eb  ARPA   Vlan270
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby brief
                     P indicates configured to preempt.
                     |
Interface   Grp Prio P State    Active addr     Standby addr    Group addr
Vl107       1   110  P Active   local           10.32.241.131   10.32.241.129
Vl270       1   100  P Standby  10.32.23.2      local           10.32.23.1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby
Vlan107 - Group 1
  Local state is Active, priority 110, may preempt
  Hellotime 1 sec, holdtime 3 sec
  Next hello sent in 0.000
  Virtual IP address is 10.32.241.129 configured
  Active router is local
  Standby router is 10.32.241.131 expires in 2.264
  Virtual mac address is 0000.0c07.ac01
  1 state changes, last state change 4w4d
  IP redundancy name is ""hsrp-Vl107-1"" (default)
  Priority tracking 2 interfaces or objects, 2 up:
    Interface or object        Decrement  State
    Vlan190                        7      Up
    Vlan191                        7      Up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto engine connection active

  ID Interface       IP-Address      State  Algorithm           Encrypt  Decrypt

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto key mypubkey rsa
% Key pair was generated at: 09:52:33 utc Sep 29 2011
Key name: PRLH-U07-DR-01.NMCI-ISF.com
 Usage: General Purpose Key
 Key is not exportable.
 Key Data:
  30819F30 0D06092A 864886F7 0D010101 05000381 8D003081 89028181 00CA819A
  1CC349C8 8A577B65 75EB1156 5C3DADB1 A04B4DA9 29258968 1B1B1757 4AEA49BC
  13342DD7 7A45D28E 6BE1EC77 32C59CB3 88CCA308 858CB44F 44BB95C8 8D11097D
  6CD31403 BE4A6E11 49139A48 5165FBC9 0D66A52B FA460412 D415FA4C 64D1AF31
  A5FBF347 85EAB2AF B80FDE9D ED5228B6 C632A95D 7EA5E5A3 0210E901 C5020301 0001
% Key pair was generated at: 18:36:26 utc Jan 16 2014
Key name: PRLH-U07-DR-01.NMCI-ISF.com.server
 Usage: Encryption Key
 Key is not exportable.
 Key Data:
  307C300D 06092A86 4886F70D 01010105 00036B00 30680261 00A8926C 1B8389CC
  9C2C4DDD 1B5704EF 2D162067 8DF19346 DD3946EB 8449BAAB FB08719E D00E32A3
  03CAC783 B3973EE0 40714FE3 2C17E1C0 FE6CFF30 95CCD69B D18801EA 2F3BA694
  60380F07 6F720F1F 483D2720 2489C434 70EEA624 8C1EC6E7 63020301 0001
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp
Chassis: SMG0617A07E
Location: Bldg_475_Floor_2_Room_Svr_Rack_4_
570714 SNMP packets input
    0 Bad SNMP version errors
    77 Unknown community name
    0 Illegal operation for community name supplied
    0 Encoding errors
    4065426 Number of requested variables
    0 Number of altered variables
    449552 Get-request PDUs
    105004 Get-next PDUs
    0 Set-request PDUs
574541 SNMP packets output
    33 Too big errors (Maximum packet size 1500)
    0 No such name errors
    0 Bad values errors
    0 General errors
    0 Response PDUs
    0 Trap PDUs
SNMP global trap: disabled

SNMP logging: disabled

SNMP Manager-role output packets
    0 Get-request PDUs
    0 Get-next PDUs
    0 Get-bulk PDUs
    0 Set-request PDUs
    3904 Inform-request PDUs
    1203 Timeouts
    65 Drops
SNMP Manager-role input packets
    0 Inform request PDUs
    0 Trap PDUs
    1335 Response PDUs
    0 Responses with errors

SNMP informs: enabled
    Informs in flight 4/25 (current/max)
    Logging to 10.0.16.136.162
        597 sent, 0 in-flight, 29 retries, 4 failed, 252 dropped
    Logging to 10.0.16.147.162
        597 sent, 4 in-flight, 805 retries, 238 failed, 355 dropped
    Logging to 10.32.9.224.162
        597 sent, 0 in-flight, 66 retries, 7 failed, 264 dropped
    Logging to 10.32.9.226.162
        597 sent, 0 in-flight, 36 retries, 8 failed, 259 dropped
    Logging to 10.32.9.244.162
        597 sent, 0 in-flight, 5 retries, 0 failed, 256 dropped
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp user
User name: nmsops
Engine ID: 00000063000100A20A001093
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 800000090300000944D34800
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: FBCC4B01A63B333E589E1B42
storage-type: nonvolatile	 active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp group
groupname: ILMI                         security model:v1
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: ILMI                         security model:v2c
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: nmcigroup                    security model:v3 priv
readview :v1default                	writeview: NMS
notifyview: *tv.FFFFFFFF.FFFFFFFF.FFF
row status: active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show radius server-group all
Sever group radius
    Sharecount = 1  sg_unconfigured = FALSE
    Type = standard  Memlocks = 1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
PRLH-U07-AS-03.NMCI-ISF.com
                 Gig 3/5            130          S I      WS-C4506-EGig 1/5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors detail
-------------------------
Device ID: PRLH-U07-AS-03.NMCI-ISF.com
Entry address(es):
  IP address: 10.32.241.140
Platform: cisco WS-C4506-E,  Capabilities: Switch IGMP
Interface: GigabitEthernet3/5,  Port ID (outgoing port): GigabitEthernet1/5
Holdtime : 130 sec

PRLH-U07-DR-01#show lldp interface
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors detail
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp status
VTP Version                     : 2
Configuration Revision          : 30
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 17
VTP Operating Mode              : Server
VTP Domain Name                 : PRLH_ZONE_7
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0x70 0xDA 0xFA 0xAD 0x7D 0x5D 0xCA 0xC1
Configuration last modified by 10.32.2.15 at 9-29-11 11:34:03
Local updater ID is 10.32.10.209 on interface Lo0 (first layer3 interface found)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp counters
VTP statistics:
Summary advertisements received    : 32699
Subset advertisements received     : 1
Request advertisements received    : 5
Summary advertisements transmitted : 59382
Subset advertisements transmitted  : 5
Request advertisements transmitted : 0
Number of config revision errors   : 0
Number of config digest errors     : 0
Number of V1 summary errors        : 0

VTP pruning statistics:

Trunk            Join Transmitted Join Received    Summary advts received from
                                                   non-pruning-capable device
---------------- ---------------- ---------------- ---------------------------
Fa6/1               0                0                0
Fa6/9               0                0                0
Fa6/10              0                0                0
Po112               0                0                0
Gi3/1               0                0                0
Gi3/2               0                0                0
Gi3/5               0                0                0
Gi3/6               0                0                0
Gi3/7               0                0                0
Gi3/13              0                0                0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp password
VTP Password: ZONE_7_r3iN_b0W
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree brief

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree active

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree summary
Switch is in pvst mode
Root bridge for: VLAN0107, VLAN0271, VLAN0571, VLAN0607, VLAN0927
EtherChannel misconfig guard is enabled
Extended system ID           is enabled

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan brief

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
2    U_UNUSED                         active    Gi3/3, Gi3/8, Gi3/9, Gi3/10
                                                Gi3/11, Gi3/14, Gi3/15, Gi3/16
                                                Gi4/1, Gi4/2, Gi4/3, Gi4/4
                                                Gi4/5, Gi4/6, Gi4/7, Gi4/8
                                                Gi4/9, Gi4/10, Gi4/11, Gi4/12
                                                Gi4/13, Gi4/14, Gi4/15, Gi4/16
                                                Fa6/2, Fa6/3, Fa6/4, Fa6/5
                                                Fa6/6, Fa6/7, Fa6/8, Fa6/11
                                                Fa6/12, Fa6/13, Fa6/14, Fa6/15
                                                Fa6/16, Fa6/17, Fa6/18, Fa6/19
                                                Fa6/20, Fa6/21, Fa6/22, Fa6/23
                                                Fa6/24
99   U_MANAGEMENT                     active
107  U_uDMN_Z07                       active
190  U_CORE_1                         active
191  U_CORE_2                         active
270  U_USER_270                       active
271  U_USER_271                       active
272  U_COI_NNPI_272                   active
570  U_PRINT_570                      active
571  U_PRINT_571                      active
607  U_VTC_ZONE_7                     active
927  U_COI_NAVFAC_927                 active
1002 fddi-default                     act/unsup
1003 token-ring-default               act/unsup
1004 fddinet-default                  act/unsup
1005 trnet-default                    act/unsup
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch brief
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/1
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/2
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/1
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/2
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 1
 No SPAN configuration is present in the system for session [1].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 2
 No SPAN configuration is present in the system for session [2].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show log
Syslog logging: enabled (0 messages dropped, 30 messages rate-limited, 0 flushes, 0 overruns)
    Console logging: disabled
    Monitor logging: level informational, 0 messages logged
    Buffer logging: level notifications, 138 messages logged
    Exception Logging: size (4096 bytes)
    Count and timestamp logging messages: disabled
    Trap logging: level notifications, 143 message lines logged
        Logging to 10.32.9.229, 143 message lines logged
        Logging to 10.32.9.238, 143 message lines logged

Log Buffer (20000 bytes):

*Dec 15 06:29:10: %SPANTREE-5-EXTENDED_SYSID: Extended SysId enabled for type vlan
*Dec 15 06:29:11: %LINK-3-UPDOWN: Interface GigabitEthernet3/1, changed state to down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu
CPU utilization for five seconds: 10%/2%; one minute: 7%; five minutes: 2%
 PID Runtime(ms)   Invoked      uSecs   5Sec   1Min   5Min TTY Process
   1           4        15        266  0.00%  0.00%  0.00%   0 Chunk Manager
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu history

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process mem
Processor Pool Total:  390998576 Used:   67034688 Free:  323963888
      I/O Pool Total:   67108864 Used:   10418768 Free:   56690096

 PID TTY  Allocated      Freed    Holding    Getbufs    Retbufs Process
   0   0   85791920   10850328   70473376          0          0 *Init*
   0   0      15000   27179368      15000          0          0 *Sched*
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show debugging

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show rtr config
	Complete Configuration Table (includes defaults)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show alias
Exec mode aliases:
  h                     help
  lo                    logout
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory raw
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#remote command switch show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2009 by cisco Systems, Inc.
Compiled Fri 25-Sep-09 10:14 by ccai
Image text-base: 0x40101040, data-base: 0x41492D20

ROM: System Bootstrap, Version 8.5(4)
BOOTLDR: s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)

PRLH-U07-DR-01 uptime is 4 weeks, 4 days, 12 hours, 58 minutes
Time since PRLH-U07-DR-01 switched to active is 4 weeks, 4 days, 13 hours, 0 minutes
System returned to ROM by power on
System restarted at 06:27:55 utc Sun Dec 15 2013
System image file is ""bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin""

cisco Catalyst 6000 (R7000) processor with 458720K/65536K bytes of memory.
Processor board ID SMG0617A07E
SR71000 CPU at 600Mhz, Implementation 0x504, Rev 1.2, 512KB L2 Cache
Last reset from power-on
X.25 software, Version 3.0.0.
24 FastEthernet/IEEE 802.3 interfaces
34 Gigabit Ethernet/IEEE 802.3 interfaces
1917K bytes of non-volatile configuration memory.
8192K bytes of packet buffer memory.

500472K bytes of Internal ATA PCMCIA card (Sector size 512 bytes).
Configuration register is 0x2102

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!END-OF-TEST-SCRIPT"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR000_should_return_false_when_a_test_script_has_a_typo_or_spelling_mistake() {
      var blob = new AssetBlob {
        Body = @"PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#terminal length 1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#write mem
Building configuration...
[OK]
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show running-config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show hardware
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform
% Incomplete command.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show clock
19:27:54.179 utc Thu Jan 16 2014
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ntp association

      address         ref clock     st  when  poll reach  delay  offset    disp
*~10.32.9.254      .GPS.             1   476  1024  377     1.1    0.26     0.5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show boot
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show bootvar
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#dir all-filesystems
Directory of system:/

    2  dr-x           0                    <no date>  memory
    1  -rw-      155801                    <no date>  running-config
   12  dr-x           0                    <no date>  vfiles

No space information available

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show idprom all
IDPROM for backplane #0
  (FRU is 'Catalyst 6500 9-slot backplane')
  OEM String = 'Cisco Systems'
  Product Number = 'WS-C6509'
  Serial Number = 'SMG0617A07E'
  Manufacturing Assembly Number = '73-3438-05'
  Manufacturing Assembly Revision = 'A0'
  Hardware Revision = 3.0
  Current supplied (+) or consumed (-) =  -

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diag

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diagbus

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform hardware capacity
System Resources
  PFC operating mode: PFC3B
  Supervisor redundancy mode: administratively sso, operationally sso

Multicast LTL Resources
  Usage:   30656 Total, 593 Used
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show fabric channel-counters

 slot channel   rxErrors   txErrors    txDrops  lbusDrops
    3       0          0          0          0          0
    4       0          0          0          0          0
    5       0          0          0          0          0

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vstack
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show module
Mod Ports Card Type                              Model              Serial No.
--- ----- -------------------------------------- ------------------ -----------
  3   16  SFM-capable 16 port 1000mb GBIC        WS-X6516-GBIC      SAD054700TU
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment status
backplane:
  operating clock count: 2
  operating VTT count: 3

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment power
                                ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show env all
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment all
                                  ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show power
system power redundancy mode = redundant
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers
Interface information:
	Interface EOBC0/0 (idb = 0x50D1AC48)
	Hardware is Mistral EOBC (revision 5)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers T1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show line aux 0
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface
Vlan1 is administratively down, line protocol is down
  Hardware is EtherSVI, address is 0009.44d3.4800 (bia 0009.44d3.4800)
  MTU 1500 bytes, BW 1000000 Kbit, DLY 10 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  Keepalive not supported
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/40 (size/max)
  5 minute input rate 0 bits/sec, 0 packets/sec
  5 minute output rate 0 bits/sec, 0 packets/sec
     0 packets input, 0 bytes, 0 no buffer
     Received 0 broadcasts (0 IP multicasts)
     0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored
     0 packets output, 0 bytes, 0 underruns
     0 output errors, 1 interface resets
     0 output buffer failures, 0 output buffers swapped out
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interfaces trunk

Port          Mode         Encapsulation  Status        Native vlan
Gi3/1         on           802.1q         trunking      1
Gi3/2         on           802.1q         trunking      1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface status

Port    Name               Status       Vlan       Duplex  Speed Type
Gi3/1   <== U00_CS03_G4/1  connected    trunk         full   1000 1000BaseLH
Gi3/2   <== U00_CS04_G4/1  connected    trunk         full   1000 1000BaseLH
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port-security
Secure Port  MaxSecureAddr  CurrentAddr  SecurityViolation  Security Action
                (Count)       (Count)          (Count)
---------------------------------------------------------------------------
---------------------------------------------------------------------------
Total Addresses in System (excluding one mac per port)     : 0
Max Addresses limit in System (excluding one mac per port) : 4096

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port security
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all summary
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all detail
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show authentication session
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm pvc
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm traffic
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ppp multilink
No active bundles
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show etherchannel summary
Flags:  D - down        P - bundled in port-channel
        I - stand-alone s - suspended
        H - Hot-standby (LACP only)
        R - Layer3      S - Layer2
        U - in use      f - failed to allocate aggregator

        M - not in use, minimum links not met
        u - unsuitable for bundling
        w - waiting to be aggregated
Number of channel-groups in use: 1
Number of aggregators:           1

Group  Port-channel  Protocol    Ports
------+-------------+-----------+-----------------------------------------------
112    Po112(SU)       PAgP      Gi5/1(P)   Gi5/2(P)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lacp neighbor

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac address-table
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac-address-table
Legend: * - primary entry
        age - seconds since last seen
        n/a - not available

  vlan   mac address     type    learn     age              ports
------+----------------+--------+-----+----------+--------------------------
*  607  3333.0000.0001    static  Yes          -   Switch,Stby-Switch
*  271  6c3b.e540.88c5   dynamic  Yes        255   Fa6/10

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show protocol
Global values:
  Internet Protocol routing is enabled
Vlan1 is administratively down, line protocol is down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface brief | exclude unassigned
Interface                  IP-Address      OK? Method Status                Protocol
Vlan107                    10.32.241.130   YES NVRAM  up                    up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip route
Codes: C - connected, S - static, R - RIP, M - mobile, B - BGP
       D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area
       N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
       E1 - OSPF external type 1, E2 - OSPF external type 2, E - EGP
       i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
       ia - IS-IS inter area, * - candidate default, U - per-user static route
       o - ODR, P - periodic downloaded static route

Gateway of last resort is 10.32.2.1 to network 0.0.0.0

     205.85.16.0/26 is subnetted, 1 subnets
O E2    205.85.16.64 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                     [110/20] via 10.32.2.65, 06:45:29, Vlan191
O E2 205.85.17.0/24 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                    [110/20] via 10.32.2.65, 06:45:29, Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp summary
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp neighbor
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf
 Routing Process ""ospf 1001"" with ID 10.32.10.209
 Start time: 00:01:11.092, Time elapsed: 4w4d
 Supports only single TOS(TOS0) routes

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf statistics

OSPF process ID 1001
------------------------------------------

  Area 0: SPF algorithm executed 29 times

  Area 7: SPF algorithm executed 4 times

  Summary OSPF SPF statistic

  SPF calculation time
Delta T   Intra	D-Intra	Summ	D-Summ	Ext	D-Ext	Total	Reason
2w3d   0	4	4	4	4	0	16	R,

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf neighbor

Neighbor ID     Pri   State           Dead Time   Address         Interface
10.32.10.197      1   2WAY/DROTHER    00:00:32    10.32.2.69      Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf interface
Loopback0 is up, line protocol is up
  Internet Address 10.32.10.209/32, Area 0
  Process ID 1001, Router ID 10.32.10.209, Network Type LOOPBACK, Cost: 1
  Loopback interface is treated as a stub Host
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf database

            OSPF Router with ID (10.32.10.209) (Process ID 1001)

		Router Link States (Area 0)

Link ID         ADV Router      Age         Seq#       Checksum Link count
10.32.10.97     10.32.10.97     1239        0x80004457 0x000AD7 13
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp neighbor
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp topology

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip cef
Prefix              Next Hop             Interface
0.0.0.0/0           10.32.2.1            Vlan190
                    10.32.2.65           Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat statistics
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat translations
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list
Standard IP access list 69
    10 permit 10.16.27.32, wildcard bits 0.0.0.31
    20 permit 10.0.18.240, wildcard bits 0.0.0.7
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list | include Standard|Extended
Standard IP access list 69
Standard IP access list 99
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-1
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-1
Extended IP access list NMCI_Printer_VLAN_ACL_IN_V17-3-2
Extended IP access list NMCI_Printer_VLAN_ACL_OUT_V17-3-2
Extended IP access list VTC_Endpoint_OUT_v2-0-0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show arp
Protocol  Address          Age (min)  Hardware Addr   Type   Interface
Internet  10.32.24.50             0   b499.bae3.5f3d  ARPA   Vlan271
Internet  10.32.23.61             6   6431.502c.e5eb  ARPA   Vlan270
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby brief
                     P indicates configured to preempt.
                     |
Interface   Grp Prio P State    Active addr     Standby addr    Group addr
Vl107       1   110  P Active   local           10.32.241.131   10.32.241.129
Vl270       1   100  P Standby  10.32.23.2      local           10.32.23.1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby
Vlan107 - Group 1
  Local state is Active, priority 110, may preempt
  Hellotime 1 sec, holdtime 3 sec
  Next hello sent in 0.000
  Virtual IP address is 10.32.241.129 configured
  Active router is local
  Standby router is 10.32.241.131 expires in 2.264
  Virtual mac address is 0000.0c07.ac01
  1 state changes, last state change 4w4d
  IP redundancy name is ""hsrp-Vl107-1"" (default)
  Priority tracking 2 interfaces or objects, 2 up:
    Interface or object        Decrement  State
    Vlan190                        7      Up
    Vlan191                        7      Up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto engine connection active

  ID Interface       IP-Address      State  Algorithm           Encrypt  Decrypt

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto key mypubkey rsa
% Key pair was generated at: 09:52:33 utc Sep 29 2011
Key name: PRLH-U07-DR-01.NMCI-ISF.com
 Usage: General Purpose Key
 Key is not exportable.
 Key Data:
  30819F30 0D06092A 864886F7 0D010101 05000381 8D003081 89028181 00CA819A
  1CC349C8 8A577B65 75EB1156 5C3DADB1 A04B4DA9 29258968 1B1B1757 4AEA49BC
  13342DD7 7A45D28E 6BE1EC77 32C59CB3 88CCA308 858CB44F 44BB95C8 8D11097D
  6CD31403 BE4A6E11 49139A48 5165FBC9 0D66A52B FA460412 D415FA4C 64D1AF31
  A5FBF347 85EAB2AF B80FDE9D ED5228B6 C632A95D 7EA5E5A3 0210E901 C5020301 0001
% Key pair was generated at: 18:36:26 utc Jan 16 2014
Key name: PRLH-U07-DR-01.NMCI-ISF.com.server
 Usage: Encryption Key
 Key is not exportable.
 Key Data:
  307C300D 06092A86 4886F70D 01010105 00036B00 30680261 00A8926C 1B8389CC
  9C2C4DDD 1B5704EF 2D162067 8DF19346 DD3946EB 8449BAAB FB08719E D00E32A3
  03CAC783 B3973EE0 40714FE3 2C17E1C0 FE6CFF30 95CCD69B D18801EA 2F3BA694
  60380F07 6F720F1F 483D2720 2489C434 70EEA624 8C1EC6E7 63020301 0001
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp
Chassis: SMG0617A07E
Location: Bldg_475_Floor_2_Room_Svr_Rack_4_
570714 SNMP packets input
    0 Bad SNMP version errors
    77 Unknown community name
    0 Illegal operation for community name supplied
    0 Encoding errors
    4065426 Number of requested variables
    0 Number of altered variables
    449552 Get-request PDUs
    105004 Get-next PDUs
    0 Set-request PDUs
574541 SNMP packets output
    33 Too big errors (Maximum packet size 1500)
    0 No such name errors
    0 Bad values errors
    0 General errors
    0 Response PDUs
    0 Trap PDUs
SNMP global trap: disabled

SNMP logging: disabled

SNMP Manager-role output packets
    0 Get-request PDUs
    0 Get-next PDUs
    0 Get-bulk PDUs
    0 Set-request PDUs
    3904 Inform-request PDUs
    1203 Timeouts
    65 Drops
SNMP Manager-role input packets
    0 Inform request PDUs
    0 Trap PDUs
    1335 Response PDUs
    0 Responses with errors

SNMP informs: enabled
    Informs in flight 4/25 (current/max)
    Logging to 10.0.16.136.162
        597 sent, 0 in-flight, 29 retries, 4 failed, 252 dropped
    Logging to 10.0.16.147.162
        597 sent, 4 in-flight, 805 retries, 238 failed, 355 dropped
    Logging to 10.32.9.224.162
        597 sent, 0 in-flight, 66 retries, 7 failed, 264 dropped
    Logging to 10.32.9.226.162
        597 sent, 0 in-flight, 36 retries, 8 failed, 259 dropped
    Logging to 10.32.9.244.162
        597 sent, 0 in-flight, 5 retries, 0 failed, 256 dropped
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp user
User name: nmsops
Engine ID: 00000063000100A20A001093
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 800000090300000944D34800
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: FBCC4B01A63B333E589E1B42
storage-type: nonvolatile	 active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp group
groupname: ILMI                         security model:v1
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: ILMI                         security model:v2c
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: nmcigroup                    security model:v3 priv
readview :v1default                	writeview: NMS
notifyview: *tv.FFFFFFFF.FFFFFFFF.FFF
row status: active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show radius server-group all
Sever group radius
    Sharecount = 1  sg_unconfigured = FALSE
    Type = standard  Memlocks = 1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
PRLH-U07-AS-03.NMCI-ISF.com
                 Gig 3/5            130          S I      WS-C4506-EGig 1/5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors detail
-------------------------
Device ID: PRLH-U07-AS-03.NMCI-ISF.com
Entry address(es):
  IP address: 10.32.241.140
Platform: cisco WS-C4506-E,  Capabilities: Switch IGMP
Interface: GigabitEthernet3/5,  Port ID (outgoing port): GigabitEthernet1/5
Holdtime : 130 sec

PRLH-U07-DR-01#show lldp interface
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors detail
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp status
VTP Version                     : 2
Configuration Revision          : 30
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 17
VTP Operating Mode              : Server
VTP Domain Name                 : PRLH_ZONE_7
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0x70 0xDA 0xFA 0xAD 0x7D 0x5D 0xCA 0xC1
Configuration last modified by 10.32.2.15 at 9-29-11 11:34:03
Local updater ID is 10.32.10.209 on interface Lo0 (first layer3 interface found)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp counters
VTP statistics:
Summary advertisements received    : 32699
Subset advertisements received     : 1
Request advertisements received    : 5
Summary advertisements transmitted : 59382
Subset advertisements transmitted  : 5
Request advertisements transmitted : 0
Number of config revision errors   : 0
Number of config digest errors     : 0
Number of V1 summary errors        : 0

VTP pruning statistics:

Trunk            Join Transmitted Join Received    Summary advts received from
                                                   non-pruning-capable device
---------------- ---------------- ---------------- ---------------------------
Fa6/1               0                0                0
Fa6/9               0                0                0
Fa6/10              0                0                0
Po112               0                0                0
Gi3/1               0                0                0
Gi3/2               0                0                0
Gi3/5               0                0                0
Gi3/6               0                0                0
Gi3/7               0                0                0
Gi3/13              0                0                0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp password
VTP Password: ZONE_7_r3iN_b0W
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree brief

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree active

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree summary
Switch is in pvst mode
Root bridge for: VLAN0107, VLAN0271, VLAN0571, VLAN0607, VLAN0927
EtherChannel misconfig guard is enabled
Extended system ID           is enabled

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan brief

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
2    U_UNUSED                         active    Gi3/3, Gi3/8, Gi3/9, Gi3/10
                                                Gi3/11, Gi3/14, Gi3/15, Gi3/16
                                                Gi4/1, Gi4/2, Gi4/3, Gi4/4
                                                Gi4/5, Gi4/6, Gi4/7, Gi4/8
                                                Gi4/9, Gi4/10, Gi4/11, Gi4/12
                                                Gi4/13, Gi4/14, Gi4/15, Gi4/16
                                                Fa6/2, Fa6/3, Fa6/4, Fa6/5
                                                Fa6/6, Fa6/7, Fa6/8, Fa6/11
                                                Fa6/12, Fa6/13, Fa6/14, Fa6/15
                                                Fa6/16, Fa6/17, Fa6/18, Fa6/19
                                                Fa6/20, Fa6/21, Fa6/22, Fa6/23
                                                Fa6/24
99   U_MANAGEMENT                     active
107  U_uDMN_Z07                       active
190  U_CORE_1                         active
191  U_CORE_2                         active
270  U_USER_270                       active
271  U_USER_271                       active
272  U_COI_NNPI_272                   active
570  U_PRINT_570                      active
571  U_PRINT_571                      active
607  U_VTC_ZONE_7                     active
927  U_COI_NAVFAC_927                 active
1002 fddi-default                     act/unsup
1003 token-ring-default               act/unsup
1004 fddinet-default                  act/unsup
1005 trnet-default                    act/unsup
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch brief
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/1
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/2
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/1
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/2
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 1
 No SPAN configuration is present in the system for session [1].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 2
 No SPAN configuration is present in the system for session [2].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show log
Syslog logging: enabled (0 messages dropped, 30 messages rate-limited, 0 flushes, 0 overruns)
    Console logging: disabled
    Monitor logging: level informational, 0 messages logged
    Buffer logging: level notifications, 138 messages logged
    Exception Logging: size (4096 bytes)
    Count and timestamp logging messages: disabled
    Trap logging: level notifications, 143 message lines logged
        Logging to 10.32.9.229, 143 message lines logged
        Logging to 10.32.9.238, 143 message lines logged

Log Buffer (20000 bytes):

*Dec 15 06:29:10: %SPANTREE-5-EXTENDED_SYSID: Extended SysId enabled for type vlan
*Dec 15 06:29:11: %LINK-3-UPDOWN: Interface GigabitEthernet3/1, changed state to down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu
CPU utilization for five seconds: 10%/2%; one minute: 7%; five minutes: 2%
 PID Runtime(ms)   Invoked      uSecs   5Sec   1Min   5Min TTY Process
   1           4        15        266  0.00%  0.00%  0.00%   0 Chunk Manager
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu history

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process mem
Processor Pool Total:  390998576 Used:   67034688 Free:  323963888
      I/O Pool Total:   67108864 Used:   10418768 Free:   56690096

 PID TTY  Allocated      Freed    Holding    Getbufs    Retbufs Process
   0   0   85791920   10850328   70473376          0          0 *Init*
   0   0      15000   27179368      15000          0          0 *Sched*
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show debugging

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show rtr config
	Complete Configuration Table (includes defaults)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show alias
Exec mode aliases:
  h                     help
  lo                    logout
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory raw
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#remote command switch show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2009 by cisco Systems, Inc.
Compiled Fri 25-Sep-09 10:14 by ccai
Image text-base: 0x40101040, data-base: 0x41492D20

ROM: System Bootstrap, Version 8.5(4)
BOOTLDR: s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)

PRLH-U07-DR-01 uptime is 4 weeks, 4 days, 12 hours, 58 minutes
Time since PRLH-U07-DR-01 switched to active is 4 weeks, 4 days, 13 hours, 0 minutes
System returned to ROM by power on
System restarted at 06:27:55 utc Sun Dec 15 2013
System image file is ""bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin""

cisco Catalyst 6000 (R7000) processor with 458720K/65536K bytes of memory.
Processor board ID SMG0617A07E
SR71000 CPU at 600Mhz, Implementation 0x504, Rev 1.2, 512KB L2 Cache
Last reset from power-on
X.25 software, Version 3.0.0.
24 FastEthernet/IEEE 802.3 interfaces
34 Gigabit Ethernet/IEEE 802.3 interfaces
1917K bytes of non-volatile configuration memory.
8192K bytes of packet buffer memory.

500472K bytes of Internal ATA PCMCIA card (Sector size 512 bytes).
Configuration register is 0x2102

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!END-OF-TEST-SCRIPT"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR000(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR000_should_return_false_when_a_test_script_is_incorrectly_applied_to_a_device() {
      var blob = new AssetBlob {
        Body = @"PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#terminal length 0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#write mem
Building configuration...
[OK]
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show running-config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show hardware
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform
% Incomplete command.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show clock
19:27:54.179 utc Thu Jan 16 2014
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ntp association

      address         ref clock     st  when  poll reach  delay  offset    disp
*~10.32.9.254      .GPS.             1   476  1024  377     1.1    0.26     0.5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show boot
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show bootvar
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#dir all-filesystems
Directory of system:/

    2  dr-x           0                    <no date>  memory
    1  -rw-      155801                    <no date>  running-config
   12  dr-x           0                    <no date>  vfiles

No space information available

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show idprom all
IDPROM for backplane #0
  (FRU is 'Catalyst 6500 9-slot backplane')
  OEM String = 'Cisco Systems'
  Product Number = 'WS-C6509'
  Serial Number = 'SMG0617A07E'
  Manufacturing Assembly Number = '73-3438-05'
  Manufacturing Assembly Revision = 'A0'
  Hardware Revision = 3.0
  Current supplied (+) or consumed (-) =  -

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diag

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diagbus

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform hardware capacity
System Resources
  PFC operating mode: PFC3B
  Supervisor redundancy mode: administratively sso, operationally sso

Multicast LTL Resources
  Usage:   30656 Total, 593 Used
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show fabric channel-counters

 slot channel   rxErrors   txErrors    txDrops  lbusDrops
    3       0          0          0          0          0
    4       0          0          0          0          0
    5       0          0          0          0          0

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vstack
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show module
Mod Ports Card Type                              Model              Serial No.
--- ----- -------------------------------------- ------------------ -----------
  3   16  SFM-capable 16 port 1000mb GBIC        WS-X6516-GBIC      SAD054700TU
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment status
backplane:
  operating clock count: 2
  operating VTT count: 3

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment power
                                ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show env all
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment all
                                  ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show power
system power redundancy mode = redundant
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers
Interface information:
	Interface EOBC0/0 (idb = 0x50D1AC48)
	Hardware is Mistral EOBC (revision 5)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers T1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show line aux 0
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface
Vlan1 is administratively down, line protocol is down
  Hardware is EtherSVI, address is 0009.44d3.4800 (bia 0009.44d3.4800)
  MTU 1500 bytes, BW 1000000 Kbit, DLY 10 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  Keepalive not supported
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/40 (size/max)
  5 minute input rate 0 bits/sec, 0 packets/sec
  5 minute output rate 0 bits/sec, 0 packets/sec
     0 packets input, 0 bytes, 0 no buffer
     Received 0 broadcasts (0 IP multicasts)
     0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored
     0 packets output, 0 bytes, 0 underruns
     0 output errors, 1 interface resets
     0 output buffer failures, 0 output buffers swapped out
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interfaces trunk

Port          Mode         Encapsulation  Status        Native vlan
Gi3/1         on           802.1q         trunking      1
Gi3/2         on           802.1q         trunking      1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface status

Port    Name               Status       Vlan       Duplex  Speed Type
Gi3/1   <== U00_CS03_G4/1  connected    trunk         full   1000 1000BaseLH
Gi3/2   <== U00_CS04_G4/1  connected    trunk         full   1000 1000BaseLH
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port-security
Secure Port  MaxSecureAddr  CurrentAddr  SecurityViolation  Security Action
                (Count)       (Count)          (Count)
---------------------------------------------------------------------------
---------------------------------------------------------------------------
Total Addresses in System (excluding one mac per port)     : 0
Max Addresses limit in System (excluding one mac per port) : 4096

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port security
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all summary
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all detail
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show authentication session
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm pvc
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm traffic
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ppp multilink
No active bundles
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show etherchannel summary
Flags:  D - down        P - bundled in port-channel
        I - stand-alone s - suspended
        H - Hot-standby (LACP only)
        R - Layer3      S - Layer2
        U - in use      f - failed to allocate aggregator

        M - not in use, minimum links not met
        u - unsuitable for bundling
        w - waiting to be aggregated
Number of channel-groups in use: 1
Number of aggregators:           1

Group  Port-channel  Protocol    Ports
------+-------------+-----------+-----------------------------------------------
112    Po112(SU)       PAgP      Gi5/1(P)   Gi5/2(P)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lacp neighbor

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac address-table
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac-address-table
Legend: * - primary entry
        age - seconds since last seen
        n/a - not available

  vlan   mac address     type    learn     age              ports
------+----------------+--------+-----+----------+--------------------------
*  607  3333.0000.0001    static  Yes          -   Switch,Stby-Switch
*  271  6c3b.e540.88c5   dynamic  Yes        255   Fa6/10

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show protocol
Global values:
  Internet Protocol routing is enabled
Vlan1 is administratively down, line protocol is down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface brief | exclude unassigned
Interface                  IP-Address      OK? Method Status                Protocol
Vlan107                    10.32.241.130   YES NVRAM  up                    up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip route
Codes: C - connected, S - static, R - RIP, M - mobile, B - BGP
       D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area
       N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
       E1 - OSPF external type 1, E2 - OSPF external type 2, E - EGP
       i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
       ia - IS-IS inter area, * - candidate default, U - per-user static route
       o - ODR, P - periodic downloaded static route

Gateway of last resort is 10.32.2.1 to network 0.0.0.0

     205.85.16.0/26 is subnetted, 1 subnets
O E2    205.85.16.64 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                     [110/20] via 10.32.2.65, 06:45:29, Vlan191
O E2 205.85.17.0/24 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                    [110/20] via 10.32.2.65, 06:45:29, Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp summary
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp neighbor
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf
 Routing Process ""ospf 1001"" with ID 10.32.10.209
 Start time: 00:01:11.092, Time elapsed: 4w4d
 Supports only single TOS(TOS0) routes

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf statistics

OSPF process ID 1001
------------------------------------------

  Area 0: SPF algorithm executed 29 times

  Area 7: SPF algorithm executed 4 times

  Summary OSPF SPF statistic

  SPF calculation time
Delta T   Intra	D-Intra	Summ	D-Summ	Ext	D-Ext	Total	Reason
2w3d   0	4	4	4	4	0	16	R,

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf neighbor

Neighbor ID     Pri   State           Dead Time   Address         Interface
10.32.10.197      1   2WAY/DROTHER    00:00:32    10.32.2.69      Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf interface
Loopback0 is up, line protocol is up
  Internet Address 10.32.10.209/32, Area 0
  Process ID 1001, Router ID 10.32.10.209, Network Type LOOPBACK, Cost: 1
  Loopback interface is treated as a stub Host
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf database

            OSPF Router with ID (10.32.10.209) (Process ID 1001)

		Router Link States (Area 0)

Link ID         ADV Router      Age         Seq#       Checksum Link count
10.32.10.97     10.32.10.97     1239        0x80004457 0x000AD7 13
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp neighbor
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp topology

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip cef
Prefix              Next Hop             Interface
0.0.0.0/0           10.32.2.1            Vlan190
                    10.32.2.65           Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat statistics
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat translations
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list
Standard IP access list 69
    10 permit 10.16.27.32, wildcard bits 0.0.0.31
    20 permit 10.0.18.240, wildcard bits 0.0.0.7
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list | include Standard|Extended
Standard IP access list 69
Standard IP access list 99
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-1
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-1
Extended IP access list NMCI_Printer_VLAN_ACL_IN_V17-3-2
Extended IP access list NMCI_Printer_VLAN_ACL_OUT_V17-3-2
Extended IP access list VTC_Endpoint_OUT_v2-0-0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show arp
Protocol  Address          Age (min)  Hardware Addr   Type   Interface
Internet  10.32.24.50             0   b499.bae3.5f3d  ARPA   Vlan271
Internet  10.32.23.61             6   6431.502c.e5eb  ARPA   Vlan270
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby brief
                     P indicates configured to preempt.
                     |
Interface   Grp Prio P State    Active addr     Standby addr    Group addr
Vl107       1   110  P Active   local           10.32.241.131   10.32.241.129
Vl270       1   100  P Standby  10.32.23.2      local           10.32.23.1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby
Vlan107 - Group 1
  Local state is Active, priority 110, may preempt
  Hellotime 1 sec, holdtime 3 sec
  Next hello sent in 0.000
  Virtual IP address is 10.32.241.129 configured
  Active router is local
  Standby router is 10.32.241.131 expires in 2.264
  Virtual mac address is 0000.0c07.ac01
  1 state changes, last state change 4w4d
  IP redundancy name is ""hsrp-Vl107-1"" (default)
  Priority tracking 2 interfaces or objects, 2 up:
    Interface or object        Decrement  State
    Vlan190                        7      Up
    Vlan191                        7      Up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto engine connection active

  ID Interface       IP-Address      State  Algorithm           Encrypt  Decrypt

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto key mypubkey rsa
% Key pair was generated at: 09:52:33 utc Sep 29 2011
Key name: PRLH-U07-DR-01.NMCI-ISF.com
 Usage: General Purpose Key
 Key is not exportable.
 Key Data:
  30819F30 0D06092A 864886F7 0D010101 05000381 8D003081 89028181 00CA819A
  1CC349C8 8A577B65 75EB1156 5C3DADB1 A04B4DA9 29258968 1B1B1757 4AEA49BC
  13342DD7 7A45D28E 6BE1EC77 32C59CB3 88CCA308 858CB44F 44BB95C8 8D11097D
  6CD31403 BE4A6E11 49139A48 5165FBC9 0D66A52B FA460412 D415FA4C 64D1AF31
  A5FBF347 85EAB2AF B80FDE9D ED5228B6 C632A95D 7EA5E5A3 0210E901 C5020301 0001
% Key pair was generated at: 18:36:26 utc Jan 16 2014
Key name: PRLH-U07-DR-01.NMCI-ISF.com.server
 Usage: Encryption Key
 Key is not exportable.
 Key Data:
  307C300D 06092A86 4886F70D 01010105 00036B00 30680261 00A8926C 1B8389CC
  9C2C4DDD 1B5704EF 2D162067 8DF19346 DD3946EB 8449BAAB FB08719E D00E32A3
  03CAC783 B3973EE0 40714FE3 2C17E1C0 FE6CFF30 95CCD69B D18801EA 2F3BA694
  60380F07 6F720F1F 483D2720 2489C434 70EEA624 8C1EC6E7 63020301 0001
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp
Chassis: SMG0617A07E
Location: Bldg_475_Floor_2_Room_Svr_Rack_4_
570714 SNMP packets input
    0 Bad SNMP version errors
    77 Unknown community name
    0 Illegal operation for community name supplied
    0 Encoding errors
    4065426 Number of requested variables
    0 Number of altered variables
    449552 Get-request PDUs
    105004 Get-next PDUs
    0 Set-request PDUs
574541 SNMP packets output
    33 Too big errors (Maximum packet size 1500)
    0 No such name errors
    0 Bad values errors
    0 General errors
    0 Response PDUs
    0 Trap PDUs
SNMP global trap: disabled

SNMP logging: disabled

SNMP Manager-role output packets
    0 Get-request PDUs
    0 Get-next PDUs
    0 Get-bulk PDUs
    0 Set-request PDUs
    3904 Inform-request PDUs
    1203 Timeouts
    65 Drops
SNMP Manager-role input packets
    0 Inform request PDUs
    0 Trap PDUs
    1335 Response PDUs
    0 Responses with errors

SNMP informs: enabled
    Informs in flight 4/25 (current/max)
    Logging to 10.0.16.136.162
        597 sent, 0 in-flight, 29 retries, 4 failed, 252 dropped
    Logging to 10.0.16.147.162
        597 sent, 4 in-flight, 805 retries, 238 failed, 355 dropped
    Logging to 10.32.9.224.162
        597 sent, 0 in-flight, 66 retries, 7 failed, 264 dropped
    Logging to 10.32.9.226.162
        597 sent, 0 in-flight, 36 retries, 8 failed, 259 dropped
    Logging to 10.32.9.244.162
        597 sent, 0 in-flight, 5 retries, 0 failed, 256 dropped
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp user
User name: nmsops
Engine ID: 00000063000100A20A001093
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 800000090300000944D34800
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: FBCC4B01A63B333E589E1B42
storage-type: nonvolatile	 active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp group
groupname: ILMI                         security model:v1
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: ILMI                         security model:v2c
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: nmcigroup                    security model:v3 priv
readview :v1default                	writeview: NMS
notifyview: *tv.FFFFFFFF.FFFFFFFF.FFF
row status: active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show radius server-group all
Sever group radius
    Sharecount = 1  sg_unconfigured = FALSE
    Type = standard  Memlocks = 1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
PRLH-U07-AS-03.NMCI-ISF.com
                 Gig 3/5            130          S I      WS-C4506-EGig 1/5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors detail
-------------------------
Device ID: PRLH-U07-AS-03.NMCI-ISF.com
Entry address(es):
  IP address: 10.32.241.140
Platform: cisco WS-C4506-E,  Capabilities: Switch IGMP
Interface: GigabitEthernet3/5,  Port ID (outgoing port): GigabitEthernet1/5
Holdtime : 130 sec

PRLH-U07-DR-01#show lldp interface
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors detail
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp status
VTP Version                     : 2
Configuration Revision          : 30
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 17
VTP Operating Mode              : Server
VTP Domain Name                 : PRLH_ZONE_7
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0x70 0xDA 0xFA 0xAD 0x7D 0x5D 0xCA 0xC1
Configuration last modified by 10.32.2.15 at 9-29-11 11:34:03
Local updater ID is 10.32.10.209 on interface Lo0 (first layer3 interface found)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp counters
VTP statistics:
Summary advertisements received    : 32699
Subset advertisements received     : 1
Request advertisements received    : 5
Summary advertisements transmitted : 59382
Subset advertisements transmitted  : 5
Request advertisements transmitted : 0
Number of config revision errors   : 0
Number of config digest errors     : 0
Number of V1 summary errors        : 0

VTP pruning statistics:

Trunk            Join Transmitted Join Received    Summary advts received from
                                                   non-pruning-capable device
---------------- ---------------- ---------------- ---------------------------
Fa6/1               0                0                0
Fa6/9               0                0                0
Fa6/10              0                0                0
Po112               0                0                0
Gi3/1               0                0                0
Gi3/2               0                0                0
Gi3/5               0                0                0
Gi3/6               0                0                0
Gi3/7               0                0                0
Gi3/13              0                0                0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp password
VTP Password: ZONE_7_r3iN_b0W
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree brief

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree active

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree summary
Switch is in pvst mode
Root bridge for: VLAN0107, VLAN0271, VLAN0571, VLAN0607, VLAN0927
EtherChannel misconfig guard is enabled
Extended system ID           is enabled

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch brief
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/1
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/2
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/1
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/2
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 1
 No SPAN configuration is present in the system for session [1].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 2
 No SPAN configuration is present in the system for session [2].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show log
Syslog logging: enabled (0 messages dropped, 30 messages rate-limited, 0 flushes, 0 overruns)
    Console logging: disabled
    Monitor logging: level informational, 0 messages logged
    Buffer logging: level notifications, 138 messages logged
    Exception Logging: size (4096 bytes)
    Count and timestamp logging messages: disabled
    Trap logging: level notifications, 143 message lines logged
        Logging to 10.32.9.229, 143 message lines logged
        Logging to 10.32.9.238, 143 message lines logged

Log Buffer (20000 bytes):

*Dec 15 06:29:10: %SPANTREE-5-EXTENDED_SYSID: Extended SysId enabled for type vlan
*Dec 15 06:29:11: %LINK-3-UPDOWN: Interface GigabitEthernet3/1, changed state to down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu
CPU utilization for five seconds: 10%/2%; one minute: 7%; five minutes: 2%
 PID Runtime(ms)   Invoked      uSecs   5Sec   1Min   5Min TTY Process
   1           4        15        266  0.00%  0.00%  0.00%   0 Chunk Manager
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu history

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process mem
Processor Pool Total:  390998576 Used:   67034688 Free:  323963888
      I/O Pool Total:   67108864 Used:   10418768 Free:   56690096

 PID TTY  Allocated      Freed    Holding    Getbufs    Retbufs Process
   0   0   85791920   10850328   70473376          0          0 *Init*
   0   0      15000   27179368      15000          0          0 *Sched*
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show debugging

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show rtr config
	Complete Configuration Table (includes defaults)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show alias
Exec mode aliases:
  h                     help
  lo                    logout
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory raw
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#remote command switch show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2009 by cisco Systems, Inc.
Compiled Fri 25-Sep-09 10:14 by ccai
Image text-base: 0x40101040, data-base: 0x41492D20

ROM: System Bootstrap, Version 8.5(4)
BOOTLDR: s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)

PRLH-U07-DR-01 uptime is 4 weeks, 4 days, 12 hours, 58 minutes
Time since PRLH-U07-DR-01 switched to active is 4 weeks, 4 days, 13 hours, 0 minutes
System returned to ROM by power on
System restarted at 06:27:55 utc Sun Dec 15 2013
System image file is ""bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin""

cisco Catalyst 6000 (R7000) processor with 458720K/65536K bytes of memory.
Processor board ID SMG0617A07E
SR71000 CPU at 600Mhz, Implementation 0x504, Rev 1.2, 512KB L2 Cache
Last reset from power-on
X.25 software, Version 3.0.0.
24 FastEthernet/IEEE 802.3 interfaces
34 Gigabit Ethernet/IEEE 802.3 interfaces
1917K bytes of non-volatile configuration memory.
8192K bytes of packet buffer memory.

500472K bytes of Internal ATA PCMCIA card (Sector size 512 bytes).
Configuration register is 0x2102

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!END-OF-TEST-SCRIPT"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR000(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR000_toString_should_give_a_description_of_the_results_when_commands_are_missing()
    {
        var blob = new AssetBlob
        {
            Body = @"PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#terminal length 0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#write mem
Building configuration...
[OK]
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show running-config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show hardware
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform
% Incomplete command.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show clock
19:27:54.179 utc Thu Jan 16 2014
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ntp association

      address         ref clock     st  when  poll reach  delay  offset    disp
*~10.32.9.254      .GPS.             1   476  1024  377     1.1    0.26     0.5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show boot
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show bootvar
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#dir all-filesystems
Directory of system:/

    2  dr-x           0                    <no date>  memory
    1  -rw-      155801                    <no date>  running-config
   12  dr-x           0                    <no date>  vfiles

No space information available

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show idprom all
IDPROM for backplane #0
  (FRU is 'Catalyst 6500 9-slot backplane')
  OEM String = 'Cisco Systems'
  Product Number = 'WS-C6509'
  Serial Number = 'SMG0617A07E'
  Manufacturing Assembly Number = '73-3438-05'
  Manufacturing Assembly Revision = 'A0'
  Hardware Revision = 3.0
  Current supplied (+) or consumed (-) =  -

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diag

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diagbus

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform hardware capacity
System Resources
  PFC operating mode: PFC3B
  Supervisor redundancy mode: administratively sso, operationally sso

Multicast LTL Resources
  Usage:   30656 Total, 593 Used
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show fabric channel-counters

 slot channel   rxErrors   txErrors    txDrops  lbusDrops
    3       0          0          0          0          0
    4       0          0          0          0          0
    5       0          0          0          0          0

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vstack
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show module
Mod Ports Card Type                              Model              Serial No.
--- ----- -------------------------------------- ------------------ -----------
  3   16  SFM-capable 16 port 1000mb GBIC        WS-X6516-GBIC      SAD054700TU
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment status
backplane:
  operating clock count: 2
  operating VTT count: 3

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment power
                                ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show env all
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment all
                                  ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show power
system power redundancy mode = redundant
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers
Interface information:
	Interface EOBC0/0 (idb = 0x50D1AC48)
	Hardware is Mistral EOBC (revision 5)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers T1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show line aux 0
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface
Vlan1 is administratively down, line protocol is down
  Hardware is EtherSVI, address is 0009.44d3.4800 (bia 0009.44d3.4800)
  MTU 1500 bytes, BW 1000000 Kbit, DLY 10 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  Keepalive not supported
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/40 (size/max)
  5 minute input rate 0 bits/sec, 0 packets/sec
  5 minute output rate 0 bits/sec, 0 packets/sec
     0 packets input, 0 bytes, 0 no buffer
     Received 0 broadcasts (0 IP multicasts)
     0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored
     0 packets output, 0 bytes, 0 underruns
     0 output errors, 1 interface resets
     0 output buffer failures, 0 output buffers swapped out
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interfaces trunk

Port          Mode         Encapsulation  Status        Native vlan
Gi3/1         on           802.1q         trunking      1
Gi3/2         on           802.1q         trunking      1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface status

Port    Name               Status       Vlan       Duplex  Speed Type
Gi3/1   <== U00_CS03_G4/1  connected    trunk         full   1000 1000BaseLH
Gi3/2   <== U00_CS04_G4/1  connected    trunk         full   1000 1000BaseLH
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port-security
Secure Port  MaxSecureAddr  CurrentAddr  SecurityViolation  Security Action
                (Count)       (Count)          (Count)
---------------------------------------------------------------------------
---------------------------------------------------------------------------
Total Addresses in System (excluding one mac per port)     : 0
Max Addresses limit in System (excluding one mac per port) : 4096

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port security
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all summary
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all detail
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show authentication session
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm pvc
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm traffic
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ppp multilink
No active bundles
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show etherchannel summary
Flags:  D - down        P - bundled in port-channel
        I - stand-alone s - suspended
        H - Hot-standby (LACP only)
        R - Layer3      S - Layer2
        U - in use      f - failed to allocate aggregator

        M - not in use, minimum links not met
        u - unsuitable for bundling
        w - waiting to be aggregated
Number of channel-groups in use: 1
Number of aggregators:           1

Group  Port-channel  Protocol    Ports
------+-------------+-----------+-----------------------------------------------
112    Po112(SU)       PAgP      Gi5/1(P)   Gi5/2(P)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lacp neighbor

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac address-table
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac-address-table
Legend: * - primary entry
        age - seconds since last seen
        n/a - not available

  vlan   mac address     type    learn     age              ports
------+----------------+--------+-----+----------+--------------------------
*  607  3333.0000.0001    static  Yes          -   Switch,Stby-Switch
*  271  6c3b.e540.88c5   dynamic  Yes        255   Fa6/10

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show protocol
Global values:
  Internet Protocol routing is enabled
Vlan1 is administratively down, line protocol is down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface brief | exclude unassigned
Interface                  IP-Address      OK? Method Status                Protocol
Vlan107                    10.32.241.130   YES NVRAM  up                    up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip route
Codes: C - connected, S - static, R - RIP, M - mobile, B - BGP
       D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area
       N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
       E1 - OSPF external type 1, E2 - OSPF external type 2, E - EGP
       i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
       ia - IS-IS inter area, * - candidate default, U - per-user static route
       o - ODR, P - periodic downloaded static route

Gateway of last resort is 10.32.2.1 to network 0.0.0.0

     205.85.16.0/26 is subnetted, 1 subnets
O E2    205.85.16.64 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                     [110/20] via 10.32.2.65, 06:45:29, Vlan191
O E2 205.85.17.0/24 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                    [110/20] via 10.32.2.65, 06:45:29, Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp summary
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp neighbor
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf
 Routing Process ""ospf 1001"" with ID 10.32.10.209
 Start time: 00:01:11.092, Time elapsed: 4w4d
 Supports only single TOS(TOS0) routes

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf statistics

OSPF process ID 1001
------------------------------------------

  Area 0: SPF algorithm executed 29 times

  Area 7: SPF algorithm executed 4 times

  Summary OSPF SPF statistic

  SPF calculation time
Delta T   Intra	D-Intra	Summ	D-Summ	Ext	D-Ext	Total	Reason
2w3d   0	4	4	4	4	0	16	R,

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf neighbor

Neighbor ID     Pri   State           Dead Time   Address         Interface
10.32.10.197      1   2WAY/DROTHER    00:00:32    10.32.2.69      Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf interface
Loopback0 is up, line protocol is up
  Internet Address 10.32.10.209/32, Area 0
  Process ID 1001, Router ID 10.32.10.209, Network Type LOOPBACK, Cost: 1
  Loopback interface is treated as a stub Host
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf database

            OSPF Router with ID (10.32.10.209) (Process ID 1001)

		Router Link States (Area 0)

Link ID         ADV Router      Age         Seq#       Checksum Link count
10.32.10.97     10.32.10.97     1239        0x80004457 0x000AD7 13
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp neighbor
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp topology

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip cef
Prefix              Next Hop             Interface
0.0.0.0/0           10.32.2.1            Vlan190
                    10.32.2.65           Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat statistics
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat translations
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list
Standard IP access list 69
    10 permit 10.16.27.32, wildcard bits 0.0.0.31
    20 permit 10.0.18.240, wildcard bits 0.0.0.7
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list | include Standard|Extended
Standard IP access list 69
Standard IP access list 99
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-1
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-1
Extended IP access list NMCI_Printer_VLAN_ACL_IN_V17-3-2
Extended IP access list NMCI_Printer_VLAN_ACL_OUT_V17-3-2
Extended IP access list VTC_Endpoint_OUT_v2-0-0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show arp
Protocol  Address          Age (min)  Hardware Addr   Type   Interface
Internet  10.32.24.50             0   b499.bae3.5f3d  ARPA   Vlan271
Internet  10.32.23.61             6   6431.502c.e5eb  ARPA   Vlan270
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby brief
                     P indicates configured to preempt.
                     |
Interface   Grp Prio P State    Active addr     Standby addr    Group addr
Vl107       1   110  P Active   local           10.32.241.131   10.32.241.129
Vl270       1   100  P Standby  10.32.23.2      local           10.32.23.1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby
Vlan107 - Group 1
  Local state is Active, priority 110, may preempt
  Hellotime 1 sec, holdtime 3 sec
  Next hello sent in 0.000
  Virtual IP address is 10.32.241.129 configured
  Active router is local
  Standby router is 10.32.241.131 expires in 2.264
  Virtual mac address is 0000.0c07.ac01
  1 state changes, last state change 4w4d
  IP redundancy name is ""hsrp-Vl107-1"" (default)
  Priority tracking 2 interfaces or objects, 2 up:
    Interface or object        Decrement  State
    Vlan190                        7      Up
    Vlan191                        7      Up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto engine connection active

  ID Interface       IP-Address      State  Algorithm           Encrypt  Decrypt

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto key mypubkey rsa
% Key pair was generated at: 09:52:33 utc Sep 29 2011
Key name: PRLH-U07-DR-01.NMCI-ISF.com
 Usage: General Purpose Key
 Key is not exportable.
 Key Data:
  30819F30 0D06092A 864886F7 0D010101 05000381 8D003081 89028181 00CA819A
  1CC349C8 8A577B65 75EB1156 5C3DADB1 A04B4DA9 29258968 1B1B1757 4AEA49BC
  13342DD7 7A45D28E 6BE1EC77 32C59CB3 88CCA308 858CB44F 44BB95C8 8D11097D
  6CD31403 BE4A6E11 49139A48 5165FBC9 0D66A52B FA460412 D415FA4C 64D1AF31
  A5FBF347 85EAB2AF B80FDE9D ED5228B6 C632A95D 7EA5E5A3 0210E901 C5020301 0001
% Key pair was generated at: 18:36:26 utc Jan 16 2014
Key name: PRLH-U07-DR-01.NMCI-ISF.com.server
 Usage: Encryption Key
 Key is not exportable.
 Key Data:
  307C300D 06092A86 4886F70D 01010105 00036B00 30680261 00A8926C 1B8389CC
  9C2C4DDD 1B5704EF 2D162067 8DF19346 DD3946EB 8449BAAB FB08719E D00E32A3
  03CAC783 B3973EE0 40714FE3 2C17E1C0 FE6CFF30 95CCD69B D18801EA 2F3BA694
  60380F07 6F720F1F 483D2720 2489C434 70EEA624 8C1EC6E7 63020301 0001
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp
Chassis: SMG0617A07E
Location: Bldg_475_Floor_2_Room_Svr_Rack_4_
570714 SNMP packets input
    0 Bad SNMP version errors
    77 Unknown community name
    0 Illegal operation for community name supplied
    0 Encoding errors
    4065426 Number of requested variables
    0 Number of altered variables
    449552 Get-request PDUs
    105004 Get-next PDUs
    0 Set-request PDUs
574541 SNMP packets output
    33 Too big errors (Maximum packet size 1500)
    0 No such name errors
    0 Bad values errors
    0 General errors
    0 Response PDUs
    0 Trap PDUs
SNMP global trap: disabled

SNMP logging: disabled

SNMP Manager-role output packets
    0 Get-request PDUs
    0 Get-next PDUs
    0 Get-bulk PDUs
    0 Set-request PDUs
    3904 Inform-request PDUs
    1203 Timeouts
    65 Drops
SNMP Manager-role input packets
    0 Inform request PDUs
    0 Trap PDUs
    1335 Response PDUs
    0 Responses with errors

SNMP informs: enabled
    Informs in flight 4/25 (current/max)
    Logging to 10.0.16.136.162
        597 sent, 0 in-flight, 29 retries, 4 failed, 252 dropped
    Logging to 10.0.16.147.162
        597 sent, 4 in-flight, 805 retries, 238 failed, 355 dropped
    Logging to 10.32.9.224.162
        597 sent, 0 in-flight, 66 retries, 7 failed, 264 dropped
    Logging to 10.32.9.226.162
        597 sent, 0 in-flight, 36 retries, 8 failed, 259 dropped
    Logging to 10.32.9.244.162
        597 sent, 0 in-flight, 5 retries, 0 failed, 256 dropped
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp user
User name: nmsops
Engine ID: 00000063000100A20A001093
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 800000090300000944D34800
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: FBCC4B01A63B333E589E1B42
storage-type: nonvolatile	 active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp group
groupname: ILMI                         security model:v1
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: ILMI                         security model:v2c
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: nmcigroup                    security model:v3 priv
readview :v1default                	writeview: NMS
notifyview: *tv.FFFFFFFF.FFFFFFFF.FFF
row status: active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show radius server-group all
Sever group radius
    Sharecount = 1  sg_unconfigured = FALSE
    Type = standard  Memlocks = 1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
PRLH-U07-AS-03.NMCI-ISF.com
                 Gig 3/5            130          S I      WS-C4506-EGig 1/5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors detail
-------------------------
Device ID: PRLH-U07-AS-03.NMCI-ISF.com
Entry address(es):
  IP address: 10.32.241.140
Platform: cisco WS-C4506-E,  Capabilities: Switch IGMP
Interface: GigabitEthernet3/5,  Port ID (outgoing port): GigabitEthernet1/5
Holdtime : 130 sec

PRLH-U07-DR-01#show lldp interface
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors detail
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp status
VTP Version                     : 2
Configuration Revision          : 30
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 17
VTP Operating Mode              : Server
VTP Domain Name                 : PRLH_ZONE_7
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0x70 0xDA 0xFA 0xAD 0x7D 0x5D 0xCA 0xC1
Configuration last modified by 10.32.2.15 at 9-29-11 11:34:03
Local updater ID is 10.32.10.209 on interface Lo0 (first layer3 interface found)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp counters
VTP statistics:
Summary advertisements received    : 32699
Subset advertisements received     : 1
Request advertisements received    : 5
Summary advertisements transmitted : 59382
Subset advertisements transmitted  : 5
Request advertisements transmitted : 0
Number of config revision errors   : 0
Number of config digest errors     : 0
Number of V1 summary errors        : 0

VTP pruning statistics:

Trunk            Join Transmitted Join Received    Summary advts received from
                                                   non-pruning-capable device
---------------- ---------------- ---------------- ---------------------------
Fa6/1               0                0                0
Fa6/9               0                0                0
Fa6/10              0                0                0
Po112               0                0                0
Gi3/1               0                0                0
Gi3/2               0                0                0
Gi3/5               0                0                0
Gi3/6               0                0                0
Gi3/7               0                0                0
Gi3/13              0                0                0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp password
VTP Password: ZONE_7_r3iN_b0W
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree brief

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree active

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree summary
Switch is in pvst mode
Root bridge for: VLAN0107, VLAN0271, VLAN0571, VLAN0607, VLAN0927
EtherChannel misconfig guard is enabled
Extended system ID           is enabled

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch brief
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/1
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/2
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/1
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/2
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 1
 No SPAN configuration is present in the system for session [1].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 2
 No SPAN configuration is present in the system for session [2].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show log
Syslog logging: enabled (0 messages dropped, 30 messages rate-limited, 0 flushes, 0 overruns)
    Console logging: disabled
    Monitor logging: level informational, 0 messages logged
    Buffer logging: level notifications, 138 messages logged
    Exception Logging: size (4096 bytes)
    Count and timestamp logging messages: disabled
    Trap logging: level notifications, 143 message lines logged
        Logging to 10.32.9.229, 143 message lines logged
        Logging to 10.32.9.238, 143 message lines logged

Log Buffer (20000 bytes):

*Dec 15 06:29:10: %SPANTREE-5-EXTENDED_SYSID: Extended SysId enabled for type vlan
*Dec 15 06:29:11: %LINK-3-UPDOWN: Interface GigabitEthernet3/1, changed state to down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu
CPU utilization for five seconds: 10%/2%; one minute: 7%; five minutes: 2%
 PID Runtime(ms)   Invoked      uSecs   5Sec   1Min   5Min TTY Process
   1           4        15        266  0.00%  0.00%  0.00%   0 Chunk Manager
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu history

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process mem
Processor Pool Total:  390998576 Used:   67034688 Free:  323963888
      I/O Pool Total:   67108864 Used:   10418768 Free:   56690096

 PID TTY  Allocated      Freed    Holding    Getbufs    Retbufs Process
   0   0   85791920   10850328   70473376          0          0 *Init*
   0   0      15000   27179368      15000          0          0 *Sched*
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show debugging

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show rtr config
	Complete Configuration Table (includes defaults)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show alias
Exec mode aliases:
  h                     help
  lo                    logout
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory raw
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#remote command switch show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2009 by cisco Systems, Inc.
Compiled Fri 25-Sep-09 10:14 by ccai
Image text-base: 0x40101040, data-base: 0x41492D20

ROM: System Bootstrap, Version 8.5(4)
BOOTLDR: s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)

PRLH-U07-DR-01 uptime is 4 weeks, 4 days, 12 hours, 58 minutes
Time since PRLH-U07-DR-01 switched to active is 4 weeks, 4 days, 13 hours, 0 minutes
System returned to ROM by power on
System restarted at 06:27:55 utc Sun Dec 15 2013
System image file is ""bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin""

cisco Catalyst 6000 (R7000) processor with 458720K/65536K bytes of memory.
Processor board ID SMG0617A07E
SR71000 CPU at 600Mhz, Implementation 0x504, Rev 1.2, 512KB L2 Cache
Last reset from power-on
X.25 software, Version 3.0.0.
24 FastEthernet/IEEE 802.3 interfaces
34 Gigabit Ethernet/IEEE 802.3 interfaces
1917K bytes of non-volatile configuration memory.
8192K bytes of packet buffer memory.

500472K bytes of Internal ATA PCMCIA card (Sector size 512 bytes).
Configuration register is 0x2102

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!END-OF-TEST-SCRIPT"
        };

        INMCIIOSDevice device = new NMCIIOSDevice(blob);
        ISTIGItem item = new IR000(device);

        Assert.AreEqual("Failing: Commands Missing: show config, show vlan brief", item.ToString());
    }

    [Test]
    public void IR000_toString_should_give_a_description_of_the_results_when_extra_commands_are_found()
    {
        var blob = new AssetBlob
        {
            Body = @"PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#terminal length 0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#write mem
Building configuration...
[OK]
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show running-config
!
hostname PRLH-U07-DR-01
!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show hardware
Cisco Internetwork Operating System Software
IOS (tm) s72033_rp Software (s72033_rp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform
% Incomplete command.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show clock
19:27:54.179 utc Thu Jan 16 2014
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ntp association

      address         ref clock     st  when  poll reach  delay  offset    disp
*~10.32.9.254      .GPS.             1   476  1024  377     1.1    0.26     0.5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show boot
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show bootvar
BOOT variable = sup-bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin,1;
CONFIG_FILE variable does not exist
BOOTLDR variable =
Configuration register is 0x2102

Standby is not present.
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#dir all-filesystems
Directory of system:/

    2  dr-x           0                    <no date>  memory
    1  -rw-      155801                    <no date>  running-config
   12  dr-x           0                    <no date>  vfiles

No space information available

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show idprom all
IDPROM for backplane #0
  (FRU is 'Catalyst 6500 9-slot backplane')
  OEM String = 'Cisco Systems'
  Product Number = 'WS-C6509'
  Serial Number = 'SMG0617A07E'
  Manufacturing Assembly Number = '73-3438-05'
  Manufacturing Assembly Revision = 'A0'
  Hardware Revision = 3.0
  Current supplied (+) or consumed (-) =  -

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diag

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show diagbus

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show platform hardware capacity
System Resources
  PFC operating mode: PFC3B
  Supervisor redundancy mode: administratively sso, operationally sso

Multicast LTL Resources
  Usage:   30656 Total, 593 Used
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show fabric channel-counters

 slot channel   rxErrors   txErrors    txDrops  lbusDrops
    3       0          0          0          0          0
    4       0          0          0          0          0
    5       0          0          0          0          0

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vstack
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show module
Mod Ports Card Type                              Model              Serial No.
--- ----- -------------------------------------- ------------------ -----------
  3   16  SFM-capable 16 port 1000mb GBIC        WS-X6516-GBIC      SAD054700TU
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment status
backplane:
  operating clock count: 2
  operating VTT count: 3

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment power
                                ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show env all
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show environment all
                                  ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show power
system power redundancy mode = redundant
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers
Interface information:
	Interface EOBC0/0 (idb = 0x50D1AC48)
	Hardware is Mistral EOBC (revision 5)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show controllers T1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show line aux 0
                          ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface
Vlan1 is administratively down, line protocol is down
  Hardware is EtherSVI, address is 0009.44d3.4800 (bia 0009.44d3.4800)
  MTU 1500 bytes, BW 1000000 Kbit, DLY 10 usec,
     reliability 255/255, txload 1/255, rxload 1/255
  Encapsulation ARPA, loopback not set
  Keepalive not supported
  ARP type: ARPA, ARP Timeout 04:00:00
  Last input never, output never, output hang never
  Last clearing of ""show interface"" counters never
  Input queue: 0/75/0/0 (size/max/drops/flushes); Total output drops: 0
  Queueing strategy: fifo
  Output queue: 0/40 (size/max)
  5 minute input rate 0 bits/sec, 0 packets/sec
  5 minute output rate 0 bits/sec, 0 packets/sec
     0 packets input, 0 bytes, 0 no buffer
     Received 0 broadcasts (0 IP multicasts)
     0 runts, 0 giants, 0 throttles
     0 input errors, 0 CRC, 0 frame, 0 overrun, 0 ignored
     0 packets output, 0 bytes, 0 underruns
     0 output errors, 1 interface resets
     0 output buffer failures, 0 output buffers swapped out
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interfaces trunk

Port          Mode         Encapsulation  Status        Native vlan
Gi3/1         on           802.1q         trunking      1
Gi3/2         on           802.1q         trunking      1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show interface status

Port    Name               Status       Vlan       Duplex  Speed Type
Gi3/1   <== U00_CS03_G4/1  connected    trunk         full   1000 1000BaseLH
Gi3/2   <== U00_CS04_G4/1  connected    trunk         full   1000 1000BaseLH
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port-security
Secure Port  MaxSecureAddr  CurrentAddr  SecurityViolation  Security Action
                (Count)       (Count)          (Count)
---------------------------------------------------------------------------
---------------------------------------------------------------------------
Total Addresses in System (excluding one mac per port)     : 0
Max Addresses limit in System (excluding one mac per port) : 4096

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show port security
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all summary
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show dot1x all detail
                              ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show authentication session
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm pvc
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show atm traffic
                     ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ppp multilink
No active bundles
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show etherchannel summary
Flags:  D - down        P - bundled in port-channel
        I - stand-alone s - suspended
        H - Hot-standby (LACP only)
        R - Layer3      S - Layer2
        U - in use      f - failed to allocate aggregator

        M - not in use, minimum links not met
        u - unsuitable for bundling
        w - waiting to be aggregated
Number of channel-groups in use: 1
Number of aggregators:           1

Group  Port-channel  Protocol    Ports
------+-------------+-----------+-----------------------------------------------
112    Po112(SU)       PAgP      Gi5/1(P)   Gi5/2(P)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lacp neighbor

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac address-table
                         ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show mac-address-table
Legend: * - primary entry
        age - seconds since last seen
        n/a - not available

  vlan   mac address     type    learn     age              ports
------+----------------+--------+-----+----------+--------------------------
*  607  3333.0000.0001    static  Yes          -   Switch,Stby-Switch
*  271  6c3b.e540.88c5   dynamic  Yes        255   Fa6/10

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show protocol
Global values:
  Internet Protocol routing is enabled
Vlan1 is administratively down, line protocol is down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface
Vlan1 is administratively down, line protocol is down
  Internet protocol processing disabled
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip interface brief | exclude unassigned
Interface                  IP-Address      OK? Method Status                Protocol
Vlan107                    10.32.241.130   YES NVRAM  up                    up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip route
Codes: C - connected, S - static, R - RIP, M - mobile, B - BGP
       D - EIGRP, EX - EIGRP external, O - OSPF, IA - OSPF inter area
       N1 - OSPF NSSA external type 1, N2 - OSPF NSSA external type 2
       E1 - OSPF external type 1, E2 - OSPF external type 2, E - EGP
       i - IS-IS, su - IS-IS summary, L1 - IS-IS level-1, L2 - IS-IS level-2
       ia - IS-IS inter area, * - candidate default, U - per-user static route
       o - ODR, P - periodic downloaded static route

Gateway of last resort is 10.32.2.1 to network 0.0.0.0

     205.85.16.0/26 is subnetted, 1 subnets
O E2    205.85.16.64 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                     [110/20] via 10.32.2.65, 06:45:29, Vlan191
O E2 205.85.17.0/24 [110/20] via 10.32.2.1, 06:45:29, Vlan190
                    [110/20] via 10.32.2.65, 06:45:29, Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp summary
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip bgp neighbor
% BGP not active

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf
 Routing Process ""ospf 1001"" with ID 10.32.10.209
 Start time: 00:01:11.092, Time elapsed: 4w4d
 Supports only single TOS(TOS0) routes

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf statistics

OSPF process ID 1001
------------------------------------------

  Area 0: SPF algorithm executed 29 times

  Area 7: SPF algorithm executed 4 times

  Summary OSPF SPF statistic

  SPF calculation time
Delta T   Intra	D-Intra	Summ	D-Summ	Ext	D-Ext	Total	Reason
2w3d   0	4	4	4	4	0	16	R,

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf neighbor

Neighbor ID     Pri   State           Dead Time   Address         Interface
10.32.10.197      1   2WAY/DROTHER    00:00:32    10.32.2.69      Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf interface
Loopback0 is up, line protocol is up
  Internet Address 10.32.10.209/32, Area 0
  Process ID 1001, Router ID 10.32.10.209, Network Type LOOPBACK, Cost: 1
  Loopback interface is treated as a stub Host
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip ospf database

            OSPF Router with ID (10.32.10.209) (Process ID 1001)

		Router Link States (Area 0)

Link ID         ADV Router      Age         Seq#       Checksum Link count
10.32.10.97     10.32.10.97     1239        0x80004457 0x000AD7 13
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp neighbor
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip eigrp topology

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip cef
Prefix              Next Hop             Interface
0.0.0.0/0           10.32.2.1            Vlan190
                    10.32.2.65           Vlan191
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat statistics
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show ip nat translations
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list
Standard IP access list 69
    10 permit 10.16.27.32, wildcard bits 0.0.0.31
    20 permit 10.0.18.240, wildcard bits 0.0.0.7
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show access-list | include Standard|Extended
Standard IP access list 69
Standard IP access list 99
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Inbound_V24-0-1
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V23-5-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-0
Extended IP access list COI_Generic_NAVFAC_User_Outbound_V24-0-1
Extended IP access list NMCI_Printer_VLAN_ACL_IN_V17-3-2
Extended IP access list NMCI_Printer_VLAN_ACL_OUT_V17-3-2
Extended IP access list VTC_Endpoint_OUT_v2-0-0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show arp
Protocol  Address          Age (min)  Hardware Addr   Type   Interface
Internet  10.32.24.50             0   b499.bae3.5f3d  ARPA   Vlan271
Internet  10.32.23.61             6   6431.502c.e5eb  ARPA   Vlan270
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby brief
                     P indicates configured to preempt.
                     |
Interface   Grp Prio P State    Active addr     Standby addr    Group addr
Vl107       1   110  P Active   local           10.32.241.131   10.32.241.129
Vl270       1   100  P Standby  10.32.23.2      local           10.32.23.1
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show standby
Vlan107 - Group 1
  Local state is Active, priority 110, may preempt
  Hellotime 1 sec, holdtime 3 sec
  Next hello sent in 0.000
  Virtual IP address is 10.32.241.129 configured
  Active router is local
  Standby router is 10.32.241.131 expires in 2.264
  Virtual mac address is 0000.0c07.ac01
  1 state changes, last state change 4w4d
  IP redundancy name is ""hsrp-Vl107-1"" (default)
  Priority tracking 2 interfaces or objects, 2 up:
    Interface or object        Decrement  State
    Vlan190                        7      Up
    Vlan191                        7      Up
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto engine connection active

  ID Interface       IP-Address      State  Algorithm           Encrypt  Decrypt

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show crypto key mypubkey rsa
% Key pair was generated at: 09:52:33 utc Sep 29 2011
Key name: PRLH-U07-DR-01.NMCI-ISF.com
 Usage: General Purpose Key
 Key is not exportable.
 Key Data:
  30819F30 0D06092A 864886F7 0D010101 05000381 8D003081 89028181 00CA819A
  1CC349C8 8A577B65 75EB1156 5C3DADB1 A04B4DA9 29258968 1B1B1757 4AEA49BC
  13342DD7 7A45D28E 6BE1EC77 32C59CB3 88CCA308 858CB44F 44BB95C8 8D11097D
  6CD31403 BE4A6E11 49139A48 5165FBC9 0D66A52B FA460412 D415FA4C 64D1AF31
  A5FBF347 85EAB2AF B80FDE9D ED5228B6 C632A95D 7EA5E5A3 0210E901 C5020301 0001
% Key pair was generated at: 18:36:26 utc Jan 16 2014
Key name: PRLH-U07-DR-01.NMCI-ISF.com.server
 Usage: Encryption Key
 Key is not exportable.
 Key Data:
  307C300D 06092A86 4886F70D 01010105 00036B00 30680261 00A8926C 1B8389CC
  9C2C4DDD 1B5704EF 2D162067 8DF19346 DD3946EB 8449BAAB FB08719E D00E32A3
  03CAC783 B3973EE0 40714FE3 2C17E1C0 FE6CFF30 95CCD69B D18801EA 2F3BA694
  60380F07 6F720F1F 483D2720 2489C434 70EEA624 8C1EC6E7 63020301 0001
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp
Chassis: SMG0617A07E
Location: Bldg_475_Floor_2_Room_Svr_Rack_4_
570714 SNMP packets input
    0 Bad SNMP version errors
    77 Unknown community name
    0 Illegal operation for community name supplied
    0 Encoding errors
    4065426 Number of requested variables
    0 Number of altered variables
    449552 Get-request PDUs
    105004 Get-next PDUs
    0 Set-request PDUs
574541 SNMP packets output
    33 Too big errors (Maximum packet size 1500)
    0 No such name errors
    0 Bad values errors
    0 General errors
    0 Response PDUs
    0 Trap PDUs
SNMP global trap: disabled

SNMP logging: disabled

SNMP Manager-role output packets
    0 Get-request PDUs
    0 Get-next PDUs
    0 Get-bulk PDUs
    0 Set-request PDUs
    3904 Inform-request PDUs
    1203 Timeouts
    65 Drops
SNMP Manager-role input packets
    0 Inform request PDUs
    0 Trap PDUs
    1335 Response PDUs
    0 Responses with errors

SNMP informs: enabled
    Informs in flight 4/25 (current/max)
    Logging to 10.0.16.136.162
        597 sent, 0 in-flight, 29 retries, 4 failed, 252 dropped
    Logging to 10.0.16.147.162
        597 sent, 4 in-flight, 805 retries, 238 failed, 355 dropped
    Logging to 10.32.9.224.162
        597 sent, 0 in-flight, 66 retries, 7 failed, 264 dropped
    Logging to 10.32.9.226.162
        597 sent, 0 in-flight, 36 retries, 8 failed, 259 dropped
    Logging to 10.32.9.244.162
        597 sent, 0 in-flight, 5 retries, 0 failed, 256 dropped
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp user
User name: nmsops
Engine ID: 00000063000100A20A001093
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: 800000090300000944D34800
storage-type: nonvolatile	 active	access-list: 69

User name: nmsops
Engine ID: FBCC4B01A63B333E589E1B42
storage-type: nonvolatile	 active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show snmp group
groupname: ILMI                         security model:v1
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: ILMI                         security model:v2c
readview :*ilmi                    	writeview: *ilmi
notifyview: <no notifyview specified>
row status: active

groupname: nmcigroup                    security model:v3 priv
readview :v1default                	writeview: NMS
notifyview: *tv.FFFFFFFF.FFFFFFFF.FFF
row status: active	access-list: 69

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show radius server-group all
Sever group radius
    Sharecount = 1  sg_unconfigured = FALSE
    Type = standard  Memlocks = 1

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp interface
GigabitEthernet3/1 is up, line protocol is up
  Encapsulation ARPA
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors
Capability Codes: R - Router, T - Trans Bridge, B - Source Route Bridge
                  S - Switch, H - Host, I - IGMP, r - Repeater, P - Phone

Device ID        Local Intrfce     Holdtme    Capability  Platform  Port ID
PRLH-U07-AS-03.NMCI-ISF.com
                 Gig 3/5            130          S I      WS-C4506-EGig 1/5
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show cdp neighbors detail
-------------------------
Device ID: PRLH-U07-AS-03.NMCI-ISF.com
Entry address(es):
  IP address: 10.32.241.140
Platform: cisco WS-C4506-E,  Capabilities: Switch IGMP
Interface: GigabitEthernet3/5,  Port ID (outgoing port): GigabitEthernet1/5
Holdtime : 130 sec

PRLH-U07-DR-01#show lldp interface
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show lldp neighbors detail
                      ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp status
VTP Version                     : 2
Configuration Revision          : 30
Maximum VLANs supported locally : 1005
Number of existing VLANs        : 17
VTP Operating Mode              : Server
VTP Domain Name                 : PRLH_ZONE_7
VTP Pruning Mode                : Disabled
VTP V2 Mode                     : Disabled
VTP Traps Generation            : Enabled
MD5 digest                      : 0x70 0xDA 0xFA 0xAD 0x7D 0x5D 0xCA 0xC1
Configuration last modified by 10.32.2.15 at 9-29-11 11:34:03
Local updater ID is 10.32.10.209 on interface Lo0 (first layer3 interface found)
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp counters
VTP statistics:
Summary advertisements received    : 32699
Subset advertisements received     : 1
Request advertisements received    : 5
Summary advertisements transmitted : 59382
Subset advertisements transmitted  : 5
Request advertisements transmitted : 0
Number of config revision errors   : 0
Number of config digest errors     : 0
Number of V1 summary errors        : 0

VTP pruning statistics:

Trunk            Join Transmitted Join Received    Summary advts received from
                                                   non-pruning-capable device
---------------- ---------------- ---------------- ---------------------------
Fa6/1               0                0                0
Fa6/9               0                0                0
Fa6/10              0                0                0
Po112               0                0                0
Gi3/1               0                0                0
Gi3/2               0                0                0
Gi3/5               0                0                0
Gi3/6               0                0                0
Gi3/7               0                0                0
Gi3/13              0                0                0
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vtp password
VTP Password: ZONE_7_r3iN_b0W
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree brief

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree active

VLAN0099
  Spanning tree enabled protocol ieee
  Root ID    Priority    32867
             Address     0008.e2d7.8f00
             Cost        3
             Port        1665 (Port-channel112)
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec

  Bridge ID  Priority    32867  (priority 32768 sys-id-ext 99)
             Address     0009.44d3.4800
             Hello Time   2 sec  Max Age 20 sec  Forward Delay 15 sec
             Aging Time 300

Interface        Role Sts Cost      Prio.Nbr Type
---------------- ---- --- --------- -------- --------------------------------
Gi3/5            Desg FWD 4         128.261  P2p
Gi3/6            Desg FWD 4         128.262  P2p
Gi3/7            Desg FWD 4         128.263  P2p
Gi3/13           Desg FWD 4         128.269  P2p
Fa6/1            Desg FWD 19        128.641  P2p
Fa6/9            Desg FWD 19        128.649  P2p
Fa6/10           Desg FWD 19        128.650  P2p
Po112            Root FWD 3         128.1665 P2p

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree summary
Switch is in pvst mode
Root bridge for: VLAN0107, VLAN0271, VLAN0571, VLAN0607, VLAN0927
EtherChannel misconfig guard is enabled
Extended system ID           is enabled

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan brief

VLAN Name                             Status    Ports
---- -------------------------------- --------- -------------------------------
1    default                          active    Gi3/4, Gi3/12
2    U_UNUSED                         active    Gi3/3, Gi3/8, Gi3/9, Gi3/10
                                                Gi3/11, Gi3/14, Gi3/15, Gi3/16
                                                Gi4/1, Gi4/2, Gi4/3, Gi4/4
                                                Gi4/5, Gi4/6, Gi4/7, Gi4/8
                                                Gi4/9, Gi4/10, Gi4/11, Gi4/12
                                                Gi4/13, Gi4/14, Gi4/15, Gi4/16
                                                Fa6/2, Fa6/3, Fa6/4, Fa6/5
                                                Fa6/6, Fa6/7, Fa6/8, Fa6/11
                                                Fa6/12, Fa6/13, Fa6/14, Fa6/15
                                                Fa6/16, Fa6/17, Fa6/18, Fa6/19
                                                Fa6/20, Fa6/21, Fa6/22, Fa6/23
                                                Fa6/24
99   U_MANAGEMENT                     active
107  U_uDMN_Z07                       active
190  U_CORE_1                         active
191  U_CORE_2                         active
270  U_USER_270                       active
271  U_USER_271                       active
272  U_COI_NNPI_272                   active
570  U_PRINT_570                      active
571  U_PRINT_571                      active
607  U_VTC_ZONE_7                     active
927  U_COI_NAVFAC_927                 active
1002 fddi-default                     act/unsup
1003 token-ring-default               act/unsup
1004 fddinet-default                  act/unsup
1005 trnet-default                    act/unsup
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show vlan-switch brief
                        ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/1
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g0/2
                                             ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/1
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show spanning-tree interface g1/2
                                            ^
% Invalid input detected at '^' marker.

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 1
 No SPAN configuration is present in the system for session [1].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show monitor session 2
 No SPAN configuration is present in the system for session [2].

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show log
Syslog logging: enabled (0 messages dropped, 30 messages rate-limited, 0 flushes, 0 overruns)
    Console logging: disabled
    Monitor logging: level informational, 0 messages logged
    Buffer logging: level notifications, 138 messages logged
    Exception Logging: size (4096 bytes)
    Count and timestamp logging messages: disabled
    Trap logging: level notifications, 143 message lines logged
        Logging to 10.32.9.229, 143 message lines logged
        Logging to 10.32.9.238, 143 message lines logged

Log Buffer (20000 bytes):

*Dec 15 06:29:10: %SPANTREE-5-EXTENDED_SYSID: Extended SysId enabled for type vlan
*Dec 15 06:29:11: %LINK-3-UPDOWN: Interface GigabitEthernet3/1, changed state to down
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu
CPU utilization for five seconds: 10%/2%; one minute: 7%; five minutes: 2%
 PID Runtime(ms)   Invoked      uSecs   5Sec   1Min   5Min TTY Process
   1           4        15        266  0.00%  0.00%  0.00%   0 Chunk Manager
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process cpu history

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show process mem
Processor Pool Total:  390998576 Used:   67034688 Free:  323963888
      I/O Pool Total:   67108864 Used:   10418768 Free:   56690096

 PID TTY  Allocated      Freed    Holding    Getbufs    Retbufs Process
   0   0   85791920   10850328   70473376          0          0 *Init*
   0   0      15000   27179368      15000          0          0 *Sched*
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show debugging

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show rtr config
	Complete Configuration Table (includes defaults)

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show alias
Exec mode aliases:
  h                     help
  lo                    logout
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show inventory raw
NAME: ""WS-C6509"", DESCR: ""Cisco Systems Catalyst 6500 9-slot Chassis System""
PID: WS-C6509          , VID:    , SN: SMG0617A07E

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#remote command switch show version
Cisco Internetwork Operating System Software
IOS (tm) s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2009 by cisco Systems, Inc.
Compiled Fri 25-Sep-09 10:14 by ccai
Image text-base: 0x40101040, data-base: 0x41492D20

ROM: System Bootstrap, Version 8.5(4)
BOOTLDR: s72033_sp Software (s72033_sp-ADVIPSERVICESK9_WAN-M), Version 12.2(18)SXF17, RELEASE SOFTWARE (fc1)

PRLH-U07-DR-01 uptime is 4 weeks, 4 days, 12 hours, 58 minutes
Time since PRLH-U07-DR-01 switched to active is 4 weeks, 4 days, 13 hours, 0 minutes
System returned to ROM by power on
System restarted at 06:27:55 utc Sun Dec 15 2013
System image file is ""bootdisk:s72033-advipservicesk9_wan-mz.122-18.SXF17.bin""

cisco Catalyst 6000 (R7000) processor with 458720K/65536K bytes of memory.
Processor board ID SMG0617A07E
SR71000 CPU at 600Mhz, Implementation 0x504, Rev 1.2, 512KB L2 Cache
Last reset from power-on
X.25 software, Version 3.0.0.
24 FastEthernet/IEEE 802.3 interfaces
34 Gigabit Ethernet/IEEE 802.3 interfaces
1917K bytes of non-volatile configuration memory.
8192K bytes of packet buffer memory.

500472K bytes of Internal ATA PCMCIA card (Sector size 512 bytes).
Configuration register is 0x2102

PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show this should fail my test
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#show fail
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!
PRLH-U07-DR-01#!END-OF-TEST-SCRIPT"
        };

        INMCIIOSDevice device = new NMCIIOSDevice(blob);
        ISTIGItem item = new IR000(device);

        var result = item.Compliant();

        Assert.AreEqual("Failing: Unrecognized Commands: show fail, show this should fail my test", item.ToString());
    }

  }
}