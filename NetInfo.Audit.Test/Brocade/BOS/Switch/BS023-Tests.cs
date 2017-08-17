using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS023_Tests {

    [Test]
    public void BS023_should_return_true_when_there_are_two_or_more_tacacs_servers() {
      var blob = new AssetBlob {
        Body = @"
tacacs-server host 10.32.9.233
tacacs-server host 10.0.16.152
tacacs-server host 10.16.27.44
tacacs-server key 1 $w!oVW0!d8i>n
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS023(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS023_should_return_false_when_there_are_less_than_two_servers() {
      var blob = new AssetBlob {
        Body = @"
tacacs-server host 10.16.27.44
tacacs-server key 1 $w!oVW0!d8i>n
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS023(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}