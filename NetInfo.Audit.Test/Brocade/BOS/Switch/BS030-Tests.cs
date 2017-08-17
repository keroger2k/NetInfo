using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS030_Tests {

    [Test]
    public void BS030_should_return_true_when_des_is_used_as_the_priv_type() {
      var blob = new AssetBlob {
        Body = @"
SSH@PNB2-U01-AS-06>show snmp user
username = nmsops
acl id = 0
group = nmcigroup
security model = v3
group acl id = 69
authtype = md5
authkey = 997d1b158efe48fc99118422cb77aaee
privtype = des
privkey = 668ec00cc4ec3332cb06ed6a1049ccc2
engine ID= 800007c703748ef84f3740
SSH@PNB2-U01-AS-06>
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS030(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS030_should_returnfalse_when_des_is_not_used_as_the_priv_type() {
      var blob = new AssetBlob {
        Body = @"
SSH@PNB2-U01-AS-06>show snmp user
username = nmsops
acl id = 0
group = nmcigroup
security model = v3
group acl id = 69
authtype = md5
authkey = 997d1b158efe48fc99118422cb77aaee
privtype = none
engine ID= 800007c703748ef84f3740
SSH@PNB2-U01-AS-06>
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS030(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void prod1() {
      var blob = new AssetBlob {
        Body = @"
SSH@DLGR-U03-AS-23#show snmp user
username = nmsops
acl id = 0
group = nmcigroup
security model = v3
group acl id = 69
authtype = md5
authkey = 1d1a9b02339d631518f35e0a8b3a3060
privtype = none
engine ID= 800007c703748ef86fdfc0
SSH@DLGR-U03-AS-23#show snmp server
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS030(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}