using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS141_Tests {

    [Test]
    public void IS141_should_return_true_when_there_is_a_log_statement_at_the_end_of_each_permit_and_deny_for_acl_97() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 97 remark Norfolk NOC
access-list 97 permit 10.16.27.32 0.0.0.31 log
access-list 97 deny any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS141(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS141_should_return_true_when_there_is_not_a_log_statement_at_the_end_of_each_permit_and_deny_for_acl_97() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 97 remark Norfolk NOC
access-list 97 permit 10.16.27.32 0.0.0.31
access-list 97 deny any log
!"
      };

      NMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS141(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}