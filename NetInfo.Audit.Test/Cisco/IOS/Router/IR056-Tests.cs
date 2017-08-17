using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Audit.Tests.Helpers;
using NetInfo.Devices;
using NetInfo.Devices.Cisco.IOS;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR056_Tests {

    private StandardAccessList approvedAcl = new StandardAccessList {
      Rules = @"access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31 log
access-list 99 remark San Diego NOC
access-list 99 permit 10.0.18.240 0.0.0.7 log
access-list 99 permit 10.0.16.128 0.0.0.31 log
access-list 99 remark Pearl Harbor NOC
access-list 99 permit 10.32.9.224 0.0.0.31 log
access-list 99 remark Local Site Access
access-list 99 deny   any log".ToConfig()
    };

    [Test]
    public void IR056_should_return_true_when_acl_99_matches_the_approved_acl() {
      var blob = new AssetBlob {
        Body = @"!
access-list 98 remark Norfolk NOC
access-list 98 permit 10.32.9.224 0.0.0.31 log
access-list 98 remark Local Site Access
access-list 98 deny any log
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

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR056(device, approvedAcl);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR056_should_return_false_when_acl_99_does_not_matches_the_approved_acl() {
      var blob = new AssetBlob {
        Body = @"!
access-list 98 remark Norfolk NOC
access-list 98 permit 10.32.9.224 0.0.0.31 log
access-list 98 remark Local Site Access
access-list 98 deny any log
access-list 99 remark Norfolk NOC
access-list 99 permit 10.16.27.32 0.0.0.31 log
access-list 99 remark Local Site Access
access-list 99 deny any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR056(device, approvedAcl);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR056_should_return_true_when_the_last_line_has_spaces_after_the_deny_statement() {
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
access-list 99 deny   any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR056(device, approvedAcl);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR056_production1() {
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
access-list 99 deny   any log
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR056(device, approvedAcl);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}