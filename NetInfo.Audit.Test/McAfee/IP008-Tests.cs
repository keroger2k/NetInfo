using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP008_Tests {
    private AssetBlob blob;

    [Test]
    public void IP008_should_return_true_when_correct_install_port_is_found() {
      blob = new AssetBlob {
        Body = @"[Sensor Network Config]
IP Address : 10.24.192.110
Netmask : 255.255.255.224
Default Gateway : 10.24.192.97
SSH Remote Logins : enabled

[Manager Config]
Manager IP addr : 1.1.1.1 (primary intf)
Install TCP Port : 8501
Alert TCP Port : 8502
Logging TCP Port : 8503

[Peer Manager Config]
Manager IP addr : 10.0.7.45 (primary intf)
Install TCP Port : 8501
Alert TCP Port : 8502
Logging TCP Port : 8503"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP008(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP008_should_return_false_when_incorrect_install_port_is_found() {
      blob = new AssetBlob {
        Body = @"[Sensor Network Config]
IP Address : 10.24.192.110
Netmask : 255.255.255.224
Default Gateway : 10.24.192.97
SSH Remote Logins : enabled

[Manager Config]
Manager IP addr : 1.1.1.1 (primary intf)
Install TCP Port : 1111
Alert TCP Port : 1111
Logging TCP Port : 1111

[Peer Manager Config]
Manager IP addr : 10.0.7.45 (primary intf)
Install TCP Port : 8501
Alert TCP Port : 8502
Logging TCP Port : 8503"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP008(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}