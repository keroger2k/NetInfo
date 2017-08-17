using NetInfo.Devices.IPS;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class IPSDeviceTest {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
      blob = new AssetBlob {
        Body = @"standard-time-zone-name UTC"
      };
    }

    [Test]
    public void ips_device_should_correctly_find_timezone_command_and_return_the_string() {
      IDSDevice device = new IDSDevice(blob);

      Assert.AreEqual("UTC", device.Timezone);
    }
  }
}