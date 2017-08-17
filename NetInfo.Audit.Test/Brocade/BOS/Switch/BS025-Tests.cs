using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS025_Tests {

    [Test]
    public void BS025_should_return_true_when_two_sntp_servers_are_configured() {
      var blob = new AssetBlob {
        Body = @"sntp server 10.26.97.193 4
sntp server 10.26.97.194 4
sntp poll-interval 3600"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS025(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS025_should_return_false_when_less_than_two_sntp_servers_are_configured() {
      var blob = new AssetBlob {
        Body = @"sntp server 10.26.97.193 4
sntp poll-interval 3600"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS025(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS025_should_return_true_when_authentication_key_is_used_and_there_are_two_sntp_servers() {
      var blob = new AssetBlob {
        Body = @"sntp server 10.16.253.3 1 authentication-key 2 2 $XHB6Tj1fZGkmUFxvLE8s
sntp server 10.16.253.2 1 authentication-key 2 2 $XHB6Tj1fZGkmUFxvLE8s"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS025(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS025_should_return_false_when_authentication_key_is_used_and_there_is_one_sntp_servers() {
      var blob = new AssetBlob {
        Body = @"sntp server 10.16.253.3 1 authentication-key 2 2 $XHB6Tj1fZGkmUFxvLE8s"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS025(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}