using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Audit.Tests.Helpers;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS003_Tests {

    [Test]
    public void BS003_should_return_true_when_acl_99_matches_the_approved_acl() {
      var blob = new AssetBlob {
        Body = @"!
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31 log
access-list 99 remark San Diego NOC
access-list 99 permit 10.0.18.240 0.0.0.7 log
access-list 99 permit 10.0.16.128 0.0.0.31 log
access-list 99 remark Pearl Harbor NOC
access-list 99 permit 10.32.9.224 0.0.0.31 log
access-list 99 remark Local Site Access
access-list 99 deny any log
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS003(device, @"access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31 log
access-list 99 remark San Diego NOC
access-list 99 permit 10.0.18.240 0.0.0.7 log
access-list 99 permit 10.0.16.128 0.0.0.31 log
access-list 99 remark Pearl Harbor NOC
access-list 99 permit 10.32.9.224 0.0.0.31 log
access-list 99 remark Local Site Access
access-list 99 deny any log".ToConfig());

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS003_should_return_false_when_acl_99_does_not_matches_the_approved_acl() {
      var blob = new AssetBlob {
        Body = @"!
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31 log
access-list 99 remark Local Site Access
access-list 99 deny any log
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS003(device, @"access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31 log
access-list 99 remark San Diego NOC
access-list 99 permit 10.0.18.240 0.0.0.7 log
access-list 99 permit 10.0.16.128 0.0.0.31 log
access-list 99 remark Pearl Harbor NOC
access-list 99 permit 10.32.9.224 0.0.0.31 log
access-list 99 remark Local Site Access
access-list 99 deny any log".ToConfig());

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}