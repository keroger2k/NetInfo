using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR130_Tests {

    [Test]
    public void IR130_should_return_true_when_vlan23_is_return_for_odmn_device() {
      var blob = new AssetBlob {
        Body = @"
!
snmp-server trap-source Vlan23
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR130(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR130_should_return_true_when_vlan51_is_return_for_odmn_device() {
      var blob = new AssetBlob {
        Body = @"
!
snmp-server trap-source Vlan51
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR130(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR130_should_return_true_when_vlan56_is_return_for_odmn_device() {
      var blob = new AssetBlob {
        Body = @"
!
snmp-server trap-source Vlan56
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR130(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR130_should_return_false_when_vlan_other_than_23_51_56_is_return_for_odmn_device() {
      var blob = new AssetBlob {
        Body = @"
!
snmp-server trap-source Vlan99
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR130(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR130_should_return_false_when_source_interface_can_not_be_found() {
      var blob = new AssetBlob {
        Body = @"
!
"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR130(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}