using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS005_Tests {

    [Test]
    public void BS005_should_return_true_when_there_is_a_log_statement_at_the_end_of_each_permit_and_deny_for_acl_99() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31 log
access-list 99 deny any log
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS005(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS005_should_return_true_when_there_is_not_a_log_statement_at_the_end_of_each_permit_and_deny_for_acl_99() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31
access-list 99 deny any log
!"
      };

      NMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS005(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}