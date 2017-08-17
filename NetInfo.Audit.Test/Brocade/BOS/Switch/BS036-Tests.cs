using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS036_Tests {

    [Test]
    public void BS036_should_return_false_when_no_authentication_key_is_found() {
      var blob = new AssetBlob {
        Body = @"
sntp server 10.32.10.193 4
sntp server 10.32.10.194 4
"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS036(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS036_should_return_true_when_authentication_key_is_found() {
      var blob = new AssetBlob {
        Body = @"
sntp server 10.32.10.193 4 authentication-key 2 2 dklfjadkfj
sntp server 10.32.10.194 4 authentication-key 2 2 dklfjadkfj
"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS036(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS036_should_return_false_when_not_all_sntp_servers_have_authentication_keys() {
      var blob = new AssetBlob {
        Body = @"
sntp server 10.32.10.193 4
sntp server 10.32.10.194 4 authentication-key 2 2 dklfjadkfj
"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS036(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}