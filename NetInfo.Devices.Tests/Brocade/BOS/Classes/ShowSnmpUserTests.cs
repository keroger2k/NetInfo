using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class ShowSnmpUserTests {

    [Test]
    public void show_snmp_user_correctly_parses_auth_type() {
      var user = new ShowSnmpUser(
        @"username = nmsops
acl id = 0
group = nmcigroup
security model = v3
group acl id = 69
authtype = md5
authkey = 43e866d68e1f95df6ea53b161bff705a
privtype = des
privkey = 0700639f2ba5e8904652553f87af6c14
engine ID= 800007c703748ef82e30c0".ToConfig()
      );

      Assert.AreEqual(ShowSnmpUser.AuthType.md5, user.SnmpUser.AuthType);
    }

    [Test]
    public void show_snmp_user_correctly_parses_priv_type() {
      var user = new ShowSnmpUser(
        @"username = nmsops
acl id = 0
group = nmcigroup
security model = v3
group acl id = 69
authtype = md5
authkey = 43e866d68e1f95df6ea53b161bff705a
privtype = des
privkey = 0700639f2ba5e8904652553f87af6c14
engine ID= 800007c703748ef82e30c0".ToConfig()
      );

      Assert.AreEqual(ShowSnmpUser.PrivType.des, user.SnmpUser.PrivType);
    }
  }
}