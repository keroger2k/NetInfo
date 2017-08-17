using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS140_Tests {

    [Test]
    public void IS140_should_return_true_when_access_class_97_is_applied_to_vty_lines() {
      var blob = new AssetBlob {
        Body = @"!
line con 0
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
line vty 0 4
 access-class 97 in
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 login
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS140(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS140_should_return_false_when_access_class_97_is_not_applied_to_vty_lines() {
      var blob = new AssetBlob {
        Body = @"!
line con 0
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
line vty 0 4
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
 transport input ssh
line vty 5 15
 exec-timeout 3 0
 login
 transport input none
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS140(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS140_should_return_true_when_only_vty_0_4_and_access_class_97_is_configured() {
      var blob = new AssetBlob {
        Body = @"!
line con 0
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
line vty 0 4
 access-class 97 in
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
 transport input ssh
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS140(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS140_should_return_false_when_only_vty_0_4_and_access_class_97_is_not_configured() {
      var blob = new AssetBlob {
        Body = @"!
line con 0
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
line vty 0 4
 exec-timeout 3 0
 password 7 14422A4A48550D2A2E2A633663293124
 login
 transport input ssh
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS140(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}