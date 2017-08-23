using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;
using NetInfo.Devices.IOS;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router
{

    [TestFixture]
    public class IR093_Tests
    {

        [Test]
        public void IR093_should_return_true_for_devices_with_two_or_more_correct_servers()
        {
            var blob = new AssetBlob
            {
                Body = @"tacacs-server host 10.16.27.44
tacacs-server host 10.0.16.152
tacacs-server host 10.32.9.233
tacacs-server directed-request
tacacs-server key 7 1446534A485432311519046D37"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0433(device);

            var result = item.Compliant();

            Assert.True(result);
        }

        [Test]
        public void IR093_should_return_true_for_devices_with_two_or_more_correct_servers_using_groups()
        {
            var blob = new AssetBlob
            {
                Body = @"!
!
aaa group server radius TAC_PLUS1
 server name acs-server-1
!
aaa group server tacacs+ TAC_PLUS2
 server name acs-server-1
 server name acs-server-2
!
tacacs server acs-server-1
 address ipv4 172.16.13.116
 key 7 test
 single-connection
tacacs server acs-server-2
 address ipv4 172.17.199.116
 key 7 test
 single-connection
!"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0433(device);

            var result = item.Compliant();

            Assert.True(result);
        }

        [Test]
        public void IR093_should_return_false_when_less_than_two_servers_are_configured()
        {
            var blob = new AssetBlob
            {
                Body = @"tacacs-server host 10.16.27.44
tacacs-server directed-request
tacacs-server key 7 1446534A485432311519046D37"
            };

            IIOSDevice device = new IOSDevice(blob);
            ISTIGItem item = new NET0433(device);

            var result = item.Compliant();

            Assert.False(result);
        }

    }
}