using System.Collections.Generic;
using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS024_Tests {
    private IEnumerable<string> approvedUsers = new string[] { "localadmin" };

    [Test]
    public void BS024_should_return_true_the_usernames_match_the_hardening_scripts_usernames() {
      var blob = new AssetBlob {
        Body = @"username localadmin password 8 $1$no4..rQ5$Vi7UWWy/5GgDZlDZgjLDr/
username localadmin password 8 $1$no4..rQ5$Vi7UWWy/5GgDZlDZgjLDr/"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS024(device, approvedUsers);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}