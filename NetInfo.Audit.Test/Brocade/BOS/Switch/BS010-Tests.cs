using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS010_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void BS010_should_return_false_no_telnet_server_is_not_found() {
      blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS010(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS010_should_return_false_when_no_telnet_server_is_found() {
      blob = new AssetBlob {
        Body = @"no telnet server"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS010(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}