using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR048_Tests {

    [Test]
    public void IR048_should_return_true_when_syn_wait_time_is_10() {
      var blob = new AssetBlob {
        Body = @"
!
ip tcp synwait-time 10
ip tcp intercept connection-timeout 60
ip tcp intercept watch-timeout 10
ip ssh time-out 30
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR048(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR048_should_return_false_when_syn_wait_time_is_not_10() {
      var blob = new AssetBlob {
        Body = @"
!
ip tcp synwait-time 99
ip tcp intercept connection-timeout 60
ip tcp intercept watch-timeout 10
ip ssh time-out 30
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR048(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}