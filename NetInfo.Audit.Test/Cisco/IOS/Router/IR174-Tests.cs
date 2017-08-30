using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR174_Tests {

    [Test]
    public void IR174_should_return_true_when_acls_97_98_99_have_log_following_all_permit_or_deny_statements() {
      var blob = new AssetBlob {
        Body = @"!
access-list 105 remark Norfolk NOC
access-list 105 permit 10.16.27.32 0.0.0.31 log
access-list 105 remark Secondary Server Farm EMS - SMTH
access-list 105 permit 10.33.24.0 0.0.0.255 log
access-list 105 deny   any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1640(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR174_should_return_false_when_acls_97_98_99_have_do_not_have_log_following_all_permit_or_deny_statements() {
      var blob = new AssetBlob {
        Body = @"!
access-list 105 remark Norfolk NOC
access-list 105 permit 10.16.27.32 0.0.0.31 log
access-list 105 remark Secondary Server Farm EMS - SMTH
access-list 105 permit 10.33.24.0 0.0.0.255
access-list 105 deny   any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new NET1640(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}