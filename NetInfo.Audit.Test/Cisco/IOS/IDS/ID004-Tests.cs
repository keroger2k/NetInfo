using NetInfo.Audit.Cisco.IOS.IDS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class ID004_Tests {

    [Test]
    public void ID004_should_return_true_when_telnet_option_is_disabled() {
      var blob = new AssetBlob {
        Body = @"telnet-option disabled"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID004(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}