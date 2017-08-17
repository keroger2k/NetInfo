using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS001_Tests {

    [Test]
    public void BS001_should_return_true_when_there_is_either_a_deny_any_or_a_permit_any_standard() {
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
      ISTIGItem item = new BS001(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS001_should_return_false_when_there_is_no_deny_ip_any_any_log_at_the_end_standard() {
      var blob = new AssetBlob {
        Body = @"!
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 permit 10.28.249.0 0.0.0.255
!
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31
access-list 99 deny any log
!"
      };

      NMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS001(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}