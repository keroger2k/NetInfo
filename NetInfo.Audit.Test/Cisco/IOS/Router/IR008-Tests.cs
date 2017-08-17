using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR008_Tests {

    [Test]
    public void IR008_should_return_true_when_management_vlans_exists_without_ip_address_but_with_bridge_numbers() {
      var blob = new AssetBlob {
        Body = @"
!
hostname BSSZ-U01-DH-01
!
interface Vlan99
 description <== Management VLAN ==>
 no ip address
 bridge-group 99
!
interface BVI99
 description <== Virtual Mgmt Interface ==>
 ip address 138.157.132.19 255.255.255.240
 no ip redirects
 no ip proxy-arp
!
ip forward-protocol nd
ip route 0.0.0.0 0.0.0.0 138.157.132.17
no ip http server
no ip http secure-server
!
!
ip tacacs source-interface BVI99
!
logging trap notifications
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_true_if_there_is_a_loopback_with_an_ip_address_on_the_device_and_configured_as_source_tacacs() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-02
!
!
interface loopback0
 ip address 10.0.0.1 255.255.255.255
!
ip tacacs source-interface loopback0
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_false_if_there_is_a_loopback_on_device_and_any_other_interface_is_used_as_tacacs_source() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-02
!
!
interface loopback0
 ip address 10.0.0.1 255.255.255.255
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.1 255.255.255.255
!
ip tacacs source-interface Vlan30
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR008_should_return_false_if_there_is_a_loopback_on_device_without_an_ip_address() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-02
!
!
interface loopback0
 description <== Version 1.4 ==>
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 10.0.0.1 255.255.255.255
!
ip tacacs source-interface Vlan99
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR008_should_return_true_when_vlan30_is_tacacs_source_and_device_has_no_loopback_or_vlan99_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-02
!
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.1 255.255.255.255
!
ip tacacs source-interface Vlan30
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_true_when_loopback_is_tacacs_source_and_device_has_an_enabled_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U00-IR-02
!
!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 10.0.0.1 255.255.255.255
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 10.0.0.2 255.255.255.255
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.3 255.255.255.255
!
ip tacacs source-interface Loopback0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_false_when_no_interfaces_have_ip_addresses_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U00-IR-02
!
!
interface Loopback0
 description <== OSPF Router ID ==>
!
interface Vlan99
 description <== Management VLAN ==>
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
!
ip tacacs source-interface Loopback0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR008_should_return_true_when_there_is_no_loopback_or_management_interface() {
      var blob = new AssetBlob {
        Body = @"!
hostname SNCI-U00-BO-01
!
!
interface GigabitEthernet0/1
 description <== NAVY Netscreen VPN 1 ==>
 ip address 138.163.34.161 255.255.255.240
!
interface GigabitEthernet0/2
 description <== U_ODMN_TB ==>
 ip address 172.16.68.130 255.255.255.248
!
interface GigabitEthernet0/3
 description <== DISABLED ==>
 no ip address
!
interface ATM1/0
 description <== <== Connection to MUGU-U00-BE-01 ==>
 ip address 192.168.0.10 255.255.255.252
!
interface FastEthernet2/0
 no ip address
 shutdown
!
interface FastEthernet2/1
 no ip address
 shutdown
!
!
ip tacacs source-interface GigabitEthernet0/2
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_true_when_vlan99_is_tacacs_source_and_device_has_no_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U00-IR-02
!
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 10.0.0.1 255.255.255.255
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.2 255.255.255.255
!
ip tacacs source-interface Vlan99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_false_when_vlan99_is_tacacs_source_and_device_has_shutdown_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U00-IR-02
!
!
interface Loopback0
 description <== OSPF Router ID ==>
 shutdown
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 10.0.0.1 255.255.255.255
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.2 255.255.255.255
!
ip tacacs source-interface Vlan99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR008_should_return_true_when_vlan30_is_tacacs_source_and_device_has_no_loopback_interface_and_vlan99_is_shutdown() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U00-IR-02
!
!
interface Vlan99
 description <== Management VLAN ==>
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.1 255.255.255.255
!
ip tacacs source-interface Vlan30
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_true_when_a_device_is_dsl_solution_and_is_using_the_BVI_for_the_source_interface() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U04-DP-02
!
!
interface BVI99
 ip address 158.236.225.148 255.255.255.0
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
!
ip tacacs source-interface BVI99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_true_when_a_device_is_dsl_solution_and_is_using_any_BVI_for_the_source_interface() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U04-DP-02
!
!
interface BVI100
 ip address 158.236.225.148 255.255.255.0
!
interface BVI101
 ip address 158.237.225.148 255.255.255.0
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
!
ip tacacs source-interface BVI100
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR008_should_return_false_when_a_dsl_solution_has_two_bvis_configured_where_one_is_disabled_and_set_as_source_interface() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U04-DP-02
!
!
interface BVI100
 ip address 158.236.225.148 255.255.255.0
 shutdown
!
interface BVI101
 ip address 158.237.225.148 255.255.255.0
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
!
ip tacacs source-interface BVI100
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR008_should_return_false_when_a_device_is_a_dsl_solution_and_does_not_use_the_BVI_for_the_source_interface() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U04-DP-02
!
!
interface BVI99
 ip address 158.236.225.148 255.255.255.0
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
!
ip tacacs source-interface loopback0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR008(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}