using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB038_Tests {

    [Test]
    public void RB038_should_return_true_when_all_partitions_contain_the_correct_image() {
      var blob = new AssetBlob {
        Body = @"AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # show boot
Installed images:
  Partition 1:
  rbt_sh 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

  Partition 2:
  rbt_sh 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

Last boot partition: 2
Next boot partition: 2
AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB038(device, new[] { "4.1.12c-nmci3" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB038_should_return_false_when_not_all_partitions_contain_the_correct_image() {
      var blob = new AssetBlob {
        Body = @"AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # show boot
Installed images:
  Partition 1:
  rbt_sh 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

  Partition 2:
  rbt_sh 4.1.13c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

Last boot partition: 2
Next boot partition: 2
AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB038(device, new[] { "4.1.12c-nmci3" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}