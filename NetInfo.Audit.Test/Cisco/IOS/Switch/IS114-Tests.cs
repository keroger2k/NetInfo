using System.Collections.Generic;
using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Audit.Tests.Helpers;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS114_Tests {

    private IEnumerable<string> approvedAcl = @"access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 remark San Diego NOC
access-list 69 permit 10.0.18.240 0.0.0.7
access-list 69 permit 10.0.16.128 0.0.0.31
access-list 69 remark Pearl Harbor NOC
access-list 69 permit 10.32.9.224 0.0.0.31
access-list 69 remark EMS
access-list 69 permit 10.8.119.32 0.0.0.31
access-list 69 permit 10.4.120.0 0.0.0.255
access-list 69 permit 10.19.120.0 0.0.0.255
access-list 69 permit 10.18.57.0 0.0.0.255
access-list 69 permit 10.3.57.0 0.0.0.255
access-list 69 permit 10.0.195.0 0.0.0.255
access-list 69 permit 10.18.184.0 0.0.0.255
access-list 69 permit 10.2.248.0 0.0.0.255
access-list 69 permit 10.27.75.0 0.0.0.255
access-list 69 permit 10.22.120.0 0.0.0.255
access-list 69 permit 10.16.21.0 0.0.0.255
access-list 69 permit 10.6.25.0 0.0.0.255
access-list 69 permit 10.20.184.0 0.0.0.255
access-list 69 permit 10.31.226.0 0.0.1.255
access-list 69 permit 10.22.248.0 0.0.0.255
access-list 69 permit 10.32.119.0 0.0.0.255
access-list 69 permit 10.2.57.0 0.0.0.255
access-list 69 permit 10.0.11.0 0.0.0.255
access-list 69 permit 10.33.24.0 0.0.0.255
access-list 69 permit 10.1.121.0 0.0.0.255
access-list 69 permit 10.3.248.0 0.0.0.255
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log".ToConfig();

    [Test]
    public void IS114_should_return_true_when_acl_69_is_correct() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 remark San Diego NOC
access-list 69 permit 10.0.18.240 0.0.0.7
access-list 69 permit 10.0.16.128 0.0.0.31
access-list 69 remark Pearl Harbor NOC
access-list 69 permit 10.32.9.224 0.0.0.31
access-list 69 remark EMS
access-list 69 permit 10.8.119.32 0.0.0.31
access-list 69 permit 10.4.120.0 0.0.0.255
access-list 69 permit 10.19.120.0 0.0.0.255
access-list 69 permit 10.18.57.0 0.0.0.255
access-list 69 permit 10.3.57.0 0.0.0.255
access-list 69 permit 10.0.195.0 0.0.0.255
access-list 69 permit 10.18.184.0 0.0.0.255
access-list 69 permit 10.2.248.0 0.0.0.255
access-list 69 permit 10.27.75.0 0.0.0.255
access-list 69 permit 10.22.120.0 0.0.0.255
access-list 69 permit 10.16.21.0 0.0.0.255
access-list 69 permit 10.6.25.0 0.0.0.255
access-list 69 permit 10.20.184.0 0.0.0.255
access-list 69 permit 10.31.226.0 0.0.1.255
access-list 69 permit 10.22.248.0 0.0.0.255
access-list 69 permit 10.32.119.0 0.0.0.255
access-list 69 permit 10.2.57.0 0.0.0.255
access-list 69 permit 10.0.11.0 0.0.0.255
access-list 69 permit 10.33.24.0 0.0.0.255
access-list 69 permit 10.1.121.0 0.0.0.255
access-list 69 permit 10.3.248.0 0.0.0.255
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS114(device, approvedAcl);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS114_should_return_true_when_acl_69_is_correct_but_not_in_same_order() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 69 remark San Diego NOC
access-list 69 permit 10.0.18.240 0.0.0.7
access-list 69 permit 10.0.16.128 0.0.0.31
access-list 69 remark Pearl Harbor NOC
access-list 69 permit 10.32.9.224 0.0.0.31
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 remark EMS
access-list 69 permit 10.8.119.32 0.0.0.31
access-list 69 permit 10.4.120.0 0.0.0.255
access-list 69 permit 10.19.120.0 0.0.0.255
access-list 69 permit 10.18.57.0 0.0.0.255
access-list 69 permit 10.3.57.0 0.0.0.255
access-list 69 permit 10.0.195.0 0.0.0.255
access-list 69 permit 10.18.184.0 0.0.0.255
access-list 69 permit 10.2.248.0 0.0.0.255
access-list 69 permit 10.27.75.0 0.0.0.255
access-list 69 permit 10.22.120.0 0.0.0.255
access-list 69 permit 10.16.21.0 0.0.0.255
access-list 69 permit 10.6.25.0 0.0.0.255
access-list 69 permit 10.20.184.0 0.0.0.255
access-list 69 permit 10.31.226.0 0.0.1.255
access-list 69 permit 10.22.248.0 0.0.0.255
access-list 69 permit 10.32.119.0 0.0.0.255
access-list 69 permit 10.2.57.0 0.0.0.255
access-list 69 permit 10.0.11.0 0.0.0.255
access-list 69 permit 10.33.24.0 0.0.0.255
access-list 69 permit 10.1.121.0 0.0.0.255
access-list 69 permit 10.3.248.0 0.0.0.255
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS114(device, approvedAcl);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS114_should_return_false_when_acl_69_does_not_have_den_any_log_statement_as_last_entry() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 69 remark San Diego NOC
access-list 69 permit 10.0.18.240 0.0.0.7
access-list 69 permit 10.0.16.128 0.0.0.31
access-list 69 remark Pearl Harbor NOC
access-list 69 permit 10.32.9.224 0.0.0.31
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 remark EMS
access-list 69 permit 10.8.119.32 0.0.0.31
access-list 69 permit 10.4.120.0 0.0.0.255
access-list 69 permit 10.19.120.0 0.0.0.255
access-list 69 permit 10.18.57.0 0.0.0.255
access-list 69 permit 10.3.57.0 0.0.0.255
access-list 69 permit 10.0.195.0 0.0.0.255
access-list 69 permit 10.18.184.0 0.0.0.255
access-list 69 permit 10.2.248.0 0.0.0.255
access-list 69 permit 10.27.75.0 0.0.0.255
access-list 69 permit 10.22.120.0 0.0.0.255
access-list 69 permit 10.16.21.0 0.0.0.255
access-list 69 permit 10.6.25.0 0.0.0.255
access-list 69 permit 10.20.184.0 0.0.0.255
access-list 69 permit 10.31.226.0 0.0.1.255
access-list 69 permit 10.22.248.0 0.0.0.255
access-list 69 permit 10.32.119.0 0.0.0.255
access-list 69 permit 10.2.57.0 0.0.0.255
access-list 69 permit 10.0.11.0 0.0.0.255
access-list 69 permit 10.33.24.0 0.0.0.255
access-list 69 permit 10.1.121.0 0.0.0.255
access-list 69 permit 10.3.248.0 0.0.0.255
access-list 69 deny   any log
access-list 69 permit 10.28.249.0 0.0.0.255
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS114(device, approvedAcl);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS114_should_return_false_when_acl_69_is_not_complete_or_has_extra_lines() {
      var blob = new AssetBlob {
        Body = @"!
!
access-list 69 remark San Diego NOC
access-list 69 permit 10.0.18.240 0.0.0.7
access-list 69 permit 10.0.16.128 0.0.0.31
access-list 69 remark Pearl Harbor NOC
access-list 69 permit 10.32.9.224 0.0.0.31
access-list 69 remark Norfolk NOC
access-list 69 permit 10.16.27.32 0.0.0.31
access-list 69 remark EMS
access-list 69 permit 10.28.249.0 0.0.0.255
access-list 69 deny   any log
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS114(device, approvedAcl);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}