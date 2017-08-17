using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS002_Tests {

    [Test]
    public void BS002_should_return_true_when_there_is_a_log_statement_at_the_end_of_each_deny() {
      var blob = new AssetBlob {
        Body = @"!
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny any log
!
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31
access-list 99 deny any log
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS002(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS002_should_return_true_when_there_is_not_a_log_statement_at_the_end_of_each_deny() {
      var blob = new AssetBlob {
        Body = @"!
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 permit 10.28.249.0 0.0.0.255
!
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31
access-list 99 deny any
!"
      };

      NMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS002(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}