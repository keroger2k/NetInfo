using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Juniper.ScreenOS {

  [TestFixture]
  public class VP064_Tests {
    private AssetBlob blob;

    [Test]
    public void VP064_should_return_true_for_juniper_complaint_device() {
      blob = new AssetBlob {
        Body = @"
set hostname MCUSQUANFWZ00
set snmp name ""MCUSQUANFWZ00"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP064(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP064_should_return_false_for_juniper_noncomplaint_device() {
      blob = new AssetBlob {
        Body = @"
set hostname MCUSQUANFWZ00
set snmp name ""MCUSQUANFWZ01"""
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP064(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}