using System.Collections.Generic;
using System.Net;
using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR093_Tests {

    [Test]
    public void IR093_should_return_true_for_devices_with_two_or_more_correct_servers() {
      var blob = new AssetBlob {
        Body = @"tacacs-server host 10.16.27.44
tacacs-server host 10.0.16.152
tacacs-server host 10.32.9.233
tacacs-server directed-request
tacacs-server key 7 1446534A485432311519046D37"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR093(device, new List<IPAddress> {
        IPAddress.Parse("10.16.27.44"),
        IPAddress.Parse("10.0.16.152"),
        IPAddress.Parse("10.32.9.233")
      });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR093_should_return_false_when_less_than_two_servers_are_configured() {
      var blob = new AssetBlob {
        Body = @"tacacs-server host 10.16.27.44
tacacs-server directed-request
tacacs-server key 7 1446534A485432311519046D37"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR093(device, new List<IPAddress> {
        IPAddress.Parse("10.16.27.44"),
        IPAddress.Parse("10.0.16.152"),
        IPAddress.Parse("10.32.9.233")
      });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR093_should_return_false_when_tacacs_server_configured_is_extraneous() {
      var blob = new AssetBlob {
        Body = @"tacacs-server host 10.16.27.44
tacacs-server host 9.9.9.9
tacacs-server host 10.32.9.233
tacacs-server directed-request
tacacs-server key 7 1446534A485432311519046D37"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR093(device, new List<IPAddress> {
        IPAddress.Parse("10.16.27.44"),
        IPAddress.Parse("10.0.16.152"),
        IPAddress.Parse("10.32.9.233")
      });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}