using System.Text.RegularExpressions;
using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR109_Tests {

    [Test]
    public void IR109_should_return_true_when_correct_monitoring_commands_are_found() {
      var blob = new AssetBlob {
        Body = @"monitor session 1 source vlan 30 both
monitor session 1 destination remote vlan 901
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR109(device, new[] {
        new Regex(@"source vlan 30 both"),
        new Regex(@"destination remote vlan 901")
      });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR109_should_return_false_when_incorrect_monitoring_commands_are_found() {
      var blob = new AssetBlob {
        Body = @"monitor session 1 source vlan 31 both
monitor session 1 destination remote vlan 901
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR109(device, new[] {
        new Regex(@"source vlan 30 both"),
        new Regex(@"destination remote vlan 901")
      });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}