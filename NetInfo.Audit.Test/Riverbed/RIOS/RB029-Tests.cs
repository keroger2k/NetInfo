using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class RB029_Tests {

    [Test]
    public void RB029_should_return_true_when_all_ntp_servers_are_in_nmci_address_space() {
      var blob = new AssetBlob {
        Body = @" ntp server 10.0.16.10 enable
   ntp server 10.0.16.10 version ""4""
   ntp server 10.16.27.68 enable
   ntp server 10.16.27.68 version ""4""
   ntp server 10.32.9.254 enable
   ntp server 10.32.9.254 version ""4""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB029(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB029_should_return_false_when_not_all_ntp_servers_are_in_nmci_address_space() {
      var blob = new AssetBlob {
        Body = @" ntp server 10.0.16.10 enable
   ntp server 10.0.16.10 version ""4""
   ntp server 10.16.27.68 enable
   ntp server 10.16.27.68 version ""4""
   ntp server 12.12.12.12 enable
   ntp server 12.12.12.12 version ""4""
"
      };
      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB029(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}