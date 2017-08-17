using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS008_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void BS008_should_return_true_when_management_http_setting_is_set_to_no() {
      blob = new AssetBlob {
        Body = @"no web-management hp-top-tools
no web-management http"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS008_should_return_false_when_management_http_setting_not_found() {
      blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS008(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}