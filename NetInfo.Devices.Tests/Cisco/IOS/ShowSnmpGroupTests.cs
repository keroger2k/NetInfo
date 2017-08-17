using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowSnmpGroupTests {

    private IEnumerable<string> config = @"groupname: ILMI                         security model:v1
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

".Split('\n').Select(c => c.Trim(new char[] { '\r' }));

    [Test]
    public void show_command_should_correctly_return_number_of_snmp_user_settings() {
      var show = new ShowSnmpGroup(config);

      Assert.AreEqual(3, show.GroupSettings.Count());
      Assert.AreEqual("ILMI", show.GroupSettings.ElementAt(0).GroupName);
      Assert.AreEqual("ILMI", show.GroupSettings.ElementAt(1).GroupName);
      Assert.AreEqual("nmcigroup", show.GroupSettings.ElementAt(2).GroupName);
    }
  }
}