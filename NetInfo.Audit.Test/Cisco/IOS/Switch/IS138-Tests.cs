using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS138_Tests {

    [Test]
    public void IS138_should_return_true_when_there_is_a_log_statement_at_the_end_of_each_permit_and_deny_for_acl_98() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 98 remark Norfolk NOC
access-list 98 permit 10.16.27.32 0.0.0.31 log
access-list 98 deny any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS138(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS138_should_return_true_when_there_is_not_a_log_statement_at_the_end_of_each_permit_and_deny_for_acl_98() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 98 remark Norfolk NOC
access-list 98 permit 10.16.27.32 0.0.0.31
access-list 98 deny any log
!"
      };

      NMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS138(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}