using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS038_Tests {

    [Test]
    public void BS038_should_return_true_for_when_two_radius_servers_are_found() {
      var blob = new AssetBlob {
        Body = @"no telnet server
username localadmin password 8 $1$no4..rQ5$Vi7UWWy/5GgDZlDZgjLDr/
radius-server host 10.2.62.161 auth-port 1812 acct-port 1813 default key 1 $g@q@\W8{-| dot1x
radius-server host 10.0.197.237 auth-port 1812 acct-port 1813 default key 1 $\k+W3l!Y-8 dot1x
radius-server timeout 5
tacacs-server host 10.16.27.44
tacacs-server host 10.0.16.152"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS038(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS038_should_return_false_when_less_than_two_radius_servers_are_found() {
      var blob = new AssetBlob {
        Body = @"no telnet server
username localadmin password 8 $1$no4..rQ5$Vi7UWWy/5GgDZlDZgjLDr/
radius-server host 10.2.62.161 auth-port 1812 acct-port 1813 default key 1 $g@q@\W8{-| dot1x
radius-server timeout 5
tacacs-server host 10.16.27.44
tacacs-server host 10.0.16.152"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS038(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}