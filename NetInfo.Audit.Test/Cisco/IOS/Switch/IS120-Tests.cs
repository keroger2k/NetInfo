using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS120_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void is120_should_return_true_for_complaint_device() {
      blob = new AssetBlob {
        Body = @"no logging console
logging monitor informational
enable secret 5 GOOD_MD5
!
alias exec harden uNAVY-ODMN-IOSSWT-v25_0_0
!
line con 0
 exec-timeout 3 0
 password 7 GOODTYPE_TYPE7
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 password 7 GOODTYPE_TYPE7
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 password 7 GOODTYPE_TYPE7
 transport input none
!
ntp clock-period 17179356"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS120(device, new[] { "GOOD_MD5", "old" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void is120_should_return_false_for_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"no logging console
logging monitor informational
enable secret 5 BAD_MD5
!
alias exec harden uNAVY-ODMN-IOSSWT-v25_0_0
!
line con 0
 exec-timeout 3 0
 password 7 BAD_TYPE7
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 password 7 BAD_TYPE7
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 password 7 BAD_TYPE7
 transport input none
!
ntp clock-period 17179356"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS120(device, new[] { "GOOD_MD5", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void is120_should_return_false_for_noncomplaint_device_where_password_is_not_found() {
      blob = new AssetBlob {
        Body = @"no logging console
logging monitor informational
enable secret 5 BAD_MD5
!
alias exec harden uNAVY-ODMN-IOSSWT-v25_0_0
!
line con 0
 exec-timeout 3 0
 password 7 BAD_TYPE7
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 password 7 BAD_TYPE7
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 transport input none
!
ntp clock-period 17179356"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS120(device, new[] { "GOOD_MD5", "old" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}