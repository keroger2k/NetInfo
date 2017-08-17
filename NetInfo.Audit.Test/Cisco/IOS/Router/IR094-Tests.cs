using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR094_Tests {

    [Test]
    public void IR094_should_return_true_for_unmanaged_devices_that_have_cdp_disabled_globally() {
      var blob = new AssetBlob {
        Body = @"!
PRLH-URB-OR-01#show cdp interface
% CDP is not enabled
PRLH-URB-OR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR094(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR094_should_return_true_for_unmanaged_devices_that_have_have_cdp_enabled_but_no_interfaces_participating() {
      var blob = new AssetBlob {
        Body = @"PRLH-U00-BI-01#show cdp interface
Control Plane Interface is down, line protocol is down
  Encapsulation UNKNOWN
  Sending CDP packets every 60 seconds
  Holdtime is 180 seconds
PRLH-U00-BI-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR094(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}