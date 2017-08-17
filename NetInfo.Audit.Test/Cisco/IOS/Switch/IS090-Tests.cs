using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS090_Tests {

    [Test]
    public void IS090_should_return_true_when_all_vlans_spannting_tree_values_are_default() {
      var blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS090(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS090_should_return_false_when_not_all_vlans_spannting_tree_values_are_default() {
      var blob = new AssetBlob {
        Body = @"spanning-tree vlan 220,222,224,502,602 priority 4096
spanning-tree vlan 221,225,931 priority 8192
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS090(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}