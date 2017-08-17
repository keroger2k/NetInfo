using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS029_Tests {

    [Test]
    public void IS029_should_return_true_when_service_tcp_keepalives_in_out_are_found() {
      var blob = new AssetBlob {
        Body = @"service tcp-keepalives-in
service tcp-keepalives-out"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS029(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS029_should_return_false_when_service_tcp_keepalives_out_is_not_found() {
      var blob = new AssetBlob {
        Body = @"service tcp-keepalives-in"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS029(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS029_should_return_false_when_service_tcp_keepalives_in_is_not_found() {
      var blob = new AssetBlob {
        Body = @"service tcp-keepalives-out"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS029(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS029_should_return_false_when_neither_service_tcp_keepalives_in_out_are_found() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS029(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}