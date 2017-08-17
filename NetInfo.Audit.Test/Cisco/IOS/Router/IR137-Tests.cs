using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR137_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR137_should_return_true_for_riverbed_complaint_device() {
      blob = new AssetBlob {
        Body = @"snmp-server location ""Bldg_3255_Floor_1_Room_IDF_Rack_4_"""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR137(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR137_should_return_false_for_incorrect_location_syntax() {
      blob = new AssetBlob {
        Body = @"snmp-server location ""Bldg_3255_Floor_1_Room_IDF_Rack_4"""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR137(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IR137_should_return_false_for_riverbed_non_complaint_device() {
      blob = new AssetBlob {
        Body = @""
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR137(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}