using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS022_Tests {

    [Test]
    public void bs022_should_return_true_for_brocade_switch_complaint_device() {
      var blob = new AssetBlob {
        Body = @"!
dot1x-enable
 re-authentication
 servertimeout 15
 timeout quiet-period 30
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS022(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void bs022_should_return_false_for_brocade_switch_with_noncomplaint_device() {
      var blob = new AssetBlob {
        Body = @"!
dot1x-enable
 servertimeout 15
 timeout quiet-period 30
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS022(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void bs022_should_return_false_for_brocade_switch_no_dot1x_configurations() {
      var blob = new AssetBlob {
        Body = @"!
system-max vlan 255
system-max spanning-tree 255
!
!
!
!"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS022(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}