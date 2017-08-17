using NetInfo.Audit.Cisco.IOS.IDS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class ID011_Tests {

    [Test]
    public void ID011_should_return_false_when_an_account_is_a_set_as_service() {
      var blob = new AssetBlob {
        Body = @"mcusquansn20#
mcusquansn21# show users all
CLI ID User Privilege
* 27412 cids_admin administrator
(cisco) administrator
test service
mcusquansn21# END-OF-TEST-SCRIPT"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID011(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}