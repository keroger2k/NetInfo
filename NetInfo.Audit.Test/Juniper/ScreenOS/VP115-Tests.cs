using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP115_Tests {

    [Test]
    public void VP115_should_return_true_when_reverse_route_is_found_in_this_format1() {
      var blob = new AssetBlob {
        Body = @"set flow reverse-route tunnel prefer"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP115(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP115_should_return_true_when_reverse_route_is_found_in_this_format2() {
      var blob = new AssetBlob {
        Body = @"set flow route tunnel prefer-reverse-route"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP115(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP115_should_return_false_when_reverse_route_is_unset() {
      var blob = new AssetBlob {
        Body = @"unset flow route tunnel prefer-reverse-route"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP115(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP115_should_return_false_when_reverse_route_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP115(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}