using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS127_Tests {

    [Test]
    public void IS127_should_return_true_for_a_device_with_the_correct_password_on_all_vtys() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v5_0_0
!
line con 0
 exec-timeout 3 0
 password 7 GOOD_NEW
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 GOOD_NEW
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 GOOD_NEW
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS127(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS127_should_return_false_for_a_device_with_incorrect_passwords_on_all_vtys() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v5_0_0
!
line con 0
 exec-timeout 3 0
 password 7 BAD_NEW
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 BAD_NEW
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 BAD_NEW
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS127(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS127_should_return_false_for_a_device_with_password_incorrect_on_vty_5_15() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v5_0_0
!
line con 0
 exec-timeout 3 0
 password 7 GOOD_NEW
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 GOOD_NEW
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 BAD_NEW
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS127(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS127_should_return_false_for_a_device_with_password_incorrect_on_vty_0_4() {
      var blob = new AssetBlob {
        Body = @"alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v5_0_0
!
line con 0
 exec-timeout 3 0
 password 7 GOOD_NEW
line vty 0 4
 access-class 99 in
 exec-timeout 3 0
 password 7 BAD_NEW
 transport input ssh
line vty 5 15
 access-class 99 in
 exec-timeout 3 0
 password 7 GOOD_NEW
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS127(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS127_should_return_false_device_without_password_defined_on_vty_5_15() {
      var blob = new AssetBlob {
        Body = @"
!
line con 0
 exec-timeout 3 0
 password 7 GOOD_NEW
 transport output all
line aux 0
 access-class 98 in
 password 7 GOOD_NEW
 no exec
 transport output none
line vty 0 4
 access-class 98 in
 exec-timeout 3 0
 password 7 GOOD_NEW
 transport input ssh
 transport output all
line vty 5 15
 exec-timeout 3 0
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS127(device, new[] { "GOOD_NEW", "GOOD_PREVIOUS" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}