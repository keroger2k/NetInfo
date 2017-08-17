using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS126_Tests {

    [Test]
    public void IS126_should_return_true_for_complaint_device() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v5_0_0
!
line con 0
 exec-timeout 3 0
 password 7 GOOD_NEW
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS126(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS126_should_return_false_for_noncomplaint_device() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v5_0_0
!
line con 0
 exec-timeout 3 0
 password 7 BAD_NEW
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS126(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS126_should_return_false_for_noncomplaint_device_where_console_password_does_not_exist() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v5_0_0
!
line con 0
 exec-timeout 3 0
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 0231211A095537394E0A19411C1F1412
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS126(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}