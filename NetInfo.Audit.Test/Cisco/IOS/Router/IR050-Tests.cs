using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR050_Tests {

    [Test]
    public void IR050_should_return_true_when_a_syslog_server_is_found() {
      var blob = new AssetBlob {
        Body = @"!
logging trap notifications
logging source-interface Vlan30
logging 10.16.27.43
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR050(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR050_should_return_false_when_a_syslog_server_is_not_found() {
      var blob = new AssetBlob {
        Body = @"!
logging trap notifications
logging source-interface Vlan30
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR050(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}