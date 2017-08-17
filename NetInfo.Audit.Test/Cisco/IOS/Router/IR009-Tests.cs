using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR009_Tests {

    [Test]
    public void IR009_should_return_true_when_vlan30_is_return_when_no_loopback_or_vlan99_interface_exist() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-02
!
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.0 255.255.255.0
!
ntp source Vlan30
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR009(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR009_should_return_false_when_loopback0_is_assigned_and_no_ip_address_is_configured() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-02
!
!
interface Loopback0
 no ip address
!
ntp source Loopback0
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR009(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR009_should_return_true_when_loopback_is_ntp_source_and_device_has_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U00-IR-02
!
!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 10.0.0.0 255.255.255.0
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 10.0.0.0 255.255.255.0
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.0 255.255.255.0
!
ntp source Loopback0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR009(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR009_should_return_true_when_vlan99_is_ntp_source_and_device_has_no_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ALBY-U00-IR-02
!
!
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 10.0.0.0 255.255.255.0
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.0 255.255.255.0
!
ntp source Vlan99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR009(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR009_should_return_true_when_vlan30_is_ntp_source_and_device_has_no_loopback_interface_and_vlan99_is_shutdown() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ALBY-U00-IR-02
!
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 10.0.0.0 255.255.255.0
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 10.0.0.0 255.255.255.0
!
ntp source Vlan30
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR009(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR009_should_return_true_when_a_device_is_dsl_solution_and_is_using_the_BVI_for_the_source_interface() {
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
 ip address 10.0.0.0 255.255.255.0
!
ntp source BVI99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR009(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR009_should_return_false_when_a_device_is_a_dsl_solution_and_does_not_use_the_BVI_for_the_source_interface() {
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
 ip address 10.0.0.0 255.255.255.0
!
ntp source loopback0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR009(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}