using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS
{

    [TestFixture]
    public class TacacsServerTests
    {

        [Test]
        public void should_return_correct_default_values_when_no_settings_are_found()
        {
            var tacacsServer = new TacacsSettings();

            Assert.AreEqual(0, tacacsServer.Hosts.Count());
            Assert.AreEqual(null, tacacsServer.Key);
        }

        [Test]
        public void should_correctly_return_the_tacacs_servers()
        {
            var tacacsServer = new TacacsSettings();
            tacacsServer.Settings = new string[] {
        @"tacacs-server host 1.1.1.1",
        @"tacacs-server host 2.2.2.2",
      };

            Assert.AreEqual(2, tacacsServer.Hosts.Count());
        }

        [Test]
        public void should_correctly_return_the_tacacs_key_hash()
        {
            var tacacsServer = new TacacsSettings();
            tacacsServer.Settings = new string[] {
        @"tacacs-server key 7 1446534A485432311519046D37",
      };

            Assert.AreEqual("1446534A485432311519046D37", tacacsServer.Key);
        }
    }
}