using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB003_Tests {

    [Test]
    public void RB003_should_return_true_when_riverbed_device_is_running_correct_IOS_sh() {
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
      ISTIGItem item = new RB003(device, new[] { "rbt_sh 4.1.12c-nmci3" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB003_should_return_true_when_riverbed_device_is_running_correct_IOS_ib() {
      var blob = new AssetBlob {
        Body = @"AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # show boot
Installed images:
  Partition 1:
  rbt_ib 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

  Partition 2:
  rbt_ib 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

Last boot partition: 2
Next boot partition: 2
AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB003(device, new[] { "rbt_ib 4.1.12c-nmci3" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB003_should_return_true_when_riverbed_device_is_running_correct_IOS_gw() {
      var blob = new AssetBlob {
        Body = @"AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # show boot
Installed images:
  Partition 1:
  rbt_gw 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

  Partition 2:
  rbt_gw 4.1.12c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

Last boot partition: 2
Next boot partition: 2
AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB003(device, new[] { "rbt_gw 4.1.12c-nmci3" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB003_should_return_false_when_riverbed_device_is_not_running_correct_IOS() {
      var blob = new AssetBlob {
        Body = @"AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # show boot
Installed images:
  Partition 1:
  rbt_sh 4.1.13c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

  Partition 2:
  rbt_sh 4.1.13c-nmci3 #160_8 2012-07-26 17:20:22 i386 root@kaunas:svn://svn/mgmt/tags/tuvalu_160_fix_3_nmci_8

Last boot partition: 2
Next boot partition: 2
AMNZ-U00-WX-01 # #
AMNZ-U00-WX-01 # #"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB003(device, new[] { "rbt_sh 4.1.12c-nmci3" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}