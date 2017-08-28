using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router
{

    [TestFixture]
    public class IR008_Tests
    {
        [Test]
        public void IR008_should_return_true_if_there_is_a_loopback_with_an_ip_address_on_the_device_and_configured_as_source_tacacs()
        {
            var blob = new AssetBlob
            {
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

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0897(device);

            var result = item.Compliant();

            Assert.True(result);
        }

        [Test]
        public void IR008_should_return_false_if_there_is_a_loopback_on_device_and_any_other_interface_is_used_as_tacacs_source()
        {
            var blob = new AssetBlob
            {
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

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0897(device);

            var result = item.Compliant();

            Assert.False(result);
        }

        [Test]
        public void IR008_should_return_false_if_there_is_a_loopback_on_device_without_an_ip_address()
        {
            var blob = new AssetBlob
            {
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

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0897(device);

            var result = item.Compliant();

            Assert.False(result);
        }

        [Test]
        public void IR008_should_return_true_when_loopback_is_tacacs_source_and_device_has_an_enabled_loopback_interface_configured()
        {
            var blob = new AssetBlob
            {
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

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0897(device);

            var result = item.Compliant();

            Assert.True(result);
        }

        [Test]
        public void IR008_should_return_false_when_no_interfaces_have_ip_addresses_configured()
        {
            var blob = new AssetBlob
            {
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

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0897(device);

            var result = item.Compliant();

            Assert.False(result);
        }

    }
}