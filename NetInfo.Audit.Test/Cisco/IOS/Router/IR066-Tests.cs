using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR066_Tests {

    [Test]
    public void IR066_should_return_true_when_there_is_a_log_statement_at_the_end_of_each_deny() {
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1800(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR066_should_return_true_when_there_is_not_a_log_statement_at_the_end_of_each_deny() {
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

      NMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1800(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}