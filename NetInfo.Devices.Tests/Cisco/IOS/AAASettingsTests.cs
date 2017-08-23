using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS;
using NUnit.Framework;
using static NetInfo.Devices.Cisco.IOS.AAASettings.GroupServerSettings;

namespace NetInfo.Devices.Tests.Cisco.IOS
{

    [TestFixture]
    public class AAASettingsTests
    {
        private AAASettings aaaSettings;

        private IEnumerable<string> genericSettings = @"!
aaa group server tacacs+ TAC_PLUS1
 server name acs-server-1
 server name acs-server-2
!
aaa group server radius TAC_PLUS2
 server name acs-server-1
 server name acs-server-2
!
aaa authentication login default group tacacs+ enable
aaa authentication enable default group tacacs+ enable
aaa authentication dot1x default group radius
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

        [SetUp]
        public void Init()
        {
            aaaSettings = new AAASettings();
            aaaSettings.Settings = genericSettings;
        }

        [Test]
        public void should_return_correct_default_dot1x_group_for_authentication()
        {
            Assert.AreEqual("radius", aaaSettings.Authentication.Dot1x.DefaultGroup);
        }

        [Test]
        public void should_be_able_to_parse_multiple_aaa_server_groups()
        {
            Assert.AreEqual(2, aaaSettings.Groups.Count());
        }

        [Test]
        public void group_server_setttings_should_be_able_to_parse_group_server_type()
        {
            Assert.AreEqual(AAAGroupServerTypes.tacacsPlus, aaaSettings.Groups.ElementAt(0).GroupServerType);
            Assert.AreEqual(AAAGroupServerTypes.radius, aaaSettings.Groups.ElementAt(1).GroupServerType);
        }

        [Test]
        public void group_server_setttings_should_be_able_to_parse_group_server_name()
        {
            Assert.AreEqual("TAC_PLUS1", aaaSettings.Groups.ElementAt(0).GroupServerName);
            Assert.AreEqual("TAC_PLUS2", aaaSettings.Groups.ElementAt(1).GroupServerName);
        }
        [Test]
        public void should_return_true_when_correct_login_tacacs_group_is_enabled()
        {
            Assert.True(aaaSettings.Authentication.LoginGroupTacacsEnable);
        }

        [Test]
        public void should_return_true_when_correct_enable_tacacs_group_is_enabled()
        {
            Assert.True(aaaSettings.Authentication.EnableGroupTacacsEnable);
        }
    }
}