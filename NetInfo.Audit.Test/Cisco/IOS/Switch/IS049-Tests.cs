using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS049_Tests {

    [Test]
    public void IS049_should_return_true_when_only_informational_is_found_for_unmanaged_devices() {
      var blob = new AssetBlob {
        Body = @"!
logging trap informational
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input none
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS049_should_return_true_when_only_logging_trap_command_is_not_found_in_configuration_and_logging_trap_is_not_disabled_for_unmanaged_devices() {
      var blob = new AssetBlob {
        Body = @"!
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input none
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS049_should_return_false_when_logging_trap_is_disabled_for_unmanaged_devices() {
      var blob = new AssetBlob {
        Body = @"!
!
!
no logging trap
no cdp run
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input none
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS049_should_return_false_when_any_other_logging_settings_besides_informational_is_found_in_unmanaged_devices() {
      var blob = new AssetBlob {
        Body = @"!
logging trap notifications
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input none
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS049_should_return_true_when_logging_notifications_is_found_for_managed_devices() {
      var blob = new AssetBlob {
        Body = @"!
logging trap notifications
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input ssh
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS049_should_return_false_when_logging_notifications_is_not_found_for_managed_devices() {
      var blob = new AssetBlob {
        Body = @"!
logging trap informational
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input ssh
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS049_should_return_false_when_logging_trap_command_is_not_found_in_configurations_for_managed_devices() {
      var blob = new AssetBlob {
        Body = @"!
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input ssh
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS049_should_return_false_when_logging_trap_is_disabled_for_managed_devices() {
      var blob = new AssetBlob {
        Body = @"!
!
no logging trap
no cdp run
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input ssh
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS049_should_return_true_when_logging_informational_is_enabled_for_ODMN_devices() {
      var blob = new AssetBlob {
        Body = @"!
!
logging trap informational
no cdp run
!
ip access-list extended ODMN_ONLY_TRAFFIC_ACL_V2-0-0
 permit ip 172.16.0.0 0.15.255.255 172.16.0.0 0.15.255.255
 deny   ip any any log
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input ssh
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS049_should_return_false_when_logging_informational_is_not_enabled_for_ODMN_devices() {
      var blob = new AssetBlob {
        Body = @"!
!
logging trap notifications
no cdp run
!
ip access-list extended ODMN_ONLY_TRAFFIC_ACL_V2-0-0
 permit ip 172.16.0.0 0.15.255.255 172.16.0.0 0.15.255.255
 deny   ip any any log
!
!
line con 0
 exec-timeout 3 0
line vty 0 4
 transport input ssh
line vty 5 15
 transport input none
!
end

DANI-U00-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS049(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}