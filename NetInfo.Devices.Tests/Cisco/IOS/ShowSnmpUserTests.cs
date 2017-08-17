using System;
using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowSnmpUserTests {

    private IEnumerable<string> config = @"

User name: nmsops
Engine ID: 00000063000100A20A2009E0
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmsops
Engine ID: 00000063000100A20A2009E2
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmsops
Engine ID: 800000090300001E1378D859
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmsops
Engine ID: 8000000B7F9C8B620C39B94139AE4FC31E513059D1
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup

User name: nmsops
Engine ID: 8000000B7FA7C50604FBCC4B01A63B333E589E1B42
storage-type: nonvolatile	 active	access-list: 69
Authentication Protocol: MD5
Privacy Protocol: DES
Group-name: nmcigroup
".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void show_command_should_correctly_return_number_of_snmp_user_settings() {
      var show = new ShowSnmpUser(config);

      Assert.AreEqual(5, show.UserSettings.Count());
    }

    [Test]
    public void show_command_should_correctly_parse_all_privacy_protocol_settings() {
      var show = new ShowSnmpUser(config);

      Assert.AreEqual(5, show.UserSettings.Count(c => c.PrivacyProtocol.Equals("des", StringComparison.OrdinalIgnoreCase)));
    }
  }
}