using System.Linq;
using NetInfo.Devices.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Riverbed.RIOS {

  [TestFixture]
  public class RIOSDeviceTest {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
      this.blob = new AssetBlob {
        Body = @"   clock timezone UTC"
      };
    }

    [Test]
    public void rios_device_should_correctly_parse_console_timeout_from_configuration() {
      IRIOSDevice device = new RIOSDevice(blob);

      string result = device.GetClock();

      Assert.AreEqual("   clock timezone UTC", result);
    }

    [Test]
    public void rios_device_should_correctly_parse_optimization_service_status_when_running() {
      var config = new AssetBlob {
        Body = @"Optimization Service: Running"
      };

      IRIOSDevice device = new RIOSDevice(config);

      Assert.True(device.OptimizationServiceEnabled);
    }

    [Test]
    public void rios_device_should_correctly_parse_optimization_service_status_when_not_running() {
      var config = new AssetBlob {
        Body = @"Optimization Service: Stopped"
      };

      IRIOSDevice device = new RIOSDevice(config);

      Assert.False(device.OptimizationServiceEnabled);
    }

    [Test]
    public void rios_device_should_correctly_parse_its_status() {
      var config = new AssetBlob {
        Body = @"   Status: Healthy"
      };

      IRIOSDevice device = new RIOSDevice(config);

      Assert.AreEqual("Healthy", device.Status);
    }

    [Test]
    public void rios_device_should_correctly_find_all_lines_in_banner() {
      var config = new AssetBlob {
        Body = @"##
   banner login ""

You are accessing a U.S. Government (USG) Information System (IS) that is
provided for USG-authorized use only. By using this IS (which includes any
device attached to this IS), you consent to the following conditions:

  Such communications and work product are private and confidential. See User
  Agreement for details.

""
   cli default auto-logout 3.0"
      };

      IRIOSDevice device = new RIOSDevice(config);
      var results = device.Banner;
      Assert.AreEqual(10, results.Count());
    }
  }
}