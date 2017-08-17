using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS024_Tests {

    [Test]
    public void IS024_should_return_true_when_privilege_mode_is_set_to_zero() {
      var blob = new AssetBlob {
        Body = @"!
username localadmin privilege 0 password 7 12355644545C1D3C6B0D767D62713552
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS024(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS024_should_return_false_when_privilege_mode_is_not_set_to_zero() {
      var blob = new AssetBlob {
        Body = @"!
username localadmin privilege 1 password 7 12355644545C1D3C6B0D767D62713552
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS024(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS024_should_return_false_when_no_users_are_configured() {
      var blob = new AssetBlob {
        Body = @"!
!"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS024(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}