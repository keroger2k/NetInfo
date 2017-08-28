using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router
{

    [TestFixture]
    public class IR043_Tests
    {


        [Test]
        public void IR043_should_return_true_when_loopback_is_tacacs_source_and_device_has_loopback_interface_configured()
        {
            var blob = new AssetBlob
            {
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

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0898(device);

            var result = item.Compliant();

            Assert.True(result);
        }

        [Test]
        public void IR043_should_return_false_when_loopback_is_tacacs_source_and_device_has_loopback_interface_configured_but_no_loopback_ip_address()
        {
            var blob = new AssetBlob
            {
                Body = @"
!
!
hostname ABCD-U00-IR-01
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
logging source-interface Loopback0
!
"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0898(device);

            var result = item.Compliant();

            Assert.False(result);
        }

    }
}