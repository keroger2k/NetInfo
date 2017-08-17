using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR043_Tests {

    [Test]
    public void IR043_should_return_true_when_vlan30_is_return_when_no_loopback_or_vlan99_interface_exist() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ABCD-U00-IR-01
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
logging source-interface Vlan30
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR043(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR043_should_return_true_when_loopback_is_tacacs_source_and_device_has_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ABCD-U00-IR-01
!
!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 1.1.1.1 255.255.255.255
!
interface Vlan99
 description <== Management VLAN ==>
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
!
logging source-interface Loopback0
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR043(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR043_should_return_true_when_vlan99_is_tacacs_source_and_device_has_no_loopback_interface_configured() {
      var blob = new AssetBlob {
        Body = @"
!
!
hostname ABCD-U00-IR-01
!
!
interface Vlan99
 description <== Management VLAN ==>
 ip address 1.1.1.1 255.255.255.255
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
logging source-interface Vlan99
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR043(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR043_should_return_true_when_vlan30_is_tacacs_source_and_device_has_no_loopback_interface_and_vlan99_is_shutdown() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ABCD-U00-IR-01
!
!
!
interface Vlan99
 description <== Management VLAN ==>
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
logging source-interface Vlan30
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR043(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR043_should_return_false_when_logging_source_interface_is_not_found() {
      var blob = new AssetBlob {
        Body = @"
!
hostname ABCD-U00-IR-01
!
!
interface Loopback0
 description <== OSPF Router ID ==>
 ip address 1.1.1.1 255.255.255.255
!
interface Vlan99
 description <== Management VLAN ==>
 shutdown
!
interface Vlan30
 description <== Inside IA VPN Segment ==>
 ip address 1.1.1.1 255.255.255.255
!
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR043(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}