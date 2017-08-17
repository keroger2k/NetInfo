using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.Cisco.IOS {

  [TestFixture]
  public class AliasExecTests {

    [Test]
    public void correctly_parses_out_all_alias_exec_settings() {
      var config = new AssetBlob {
        Body = @"!
!
alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v6_0_0
alias exec tb-ban-802-1x 802_1x-Global-v2_1_0
alias exec harden uNAVY-INSIDE-SSH-IOSRTR-v24_0_0
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(config);
      Assert.AreEqual(3, device.AliasExecSettings.Count);
    }

    [Test]
    public void correctly_parses_out_hardening_information() {
      var config = new AssetBlob {
        Body = @"!
!
alias exec nms uNRFK-INSIDE-SNMPV3-IOSRTR-v6_0_0
alias exec tb-ban-802-1x 802_1x-Global-v2_1_0
alias exec harden uNAVY-INSIDE-SSH-IOSRTR-v24_0_0
!
!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(config);
      Assert.IsNotNull(device.AliasExecSettings.HardeningSettings);
    }

    [Test]
    public void correctly_parses_out_hardening_information_enclave_zone_type_version() {
      var config = new AssetBlob {
        Body = @"alias exec harden uNAVY-INSIDE-SSH-IOSRTR-v24_0_0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(config);
      Assert.AreEqual("uNAVY", device.AliasExecSettings.HardeningSettings.Enclave);
      Assert.AreEqual("INSIDE", device.AliasExecSettings.HardeningSettings.Zone);
      Assert.AreEqual("IOSRTR", device.AliasExecSettings.HardeningSettings.Type);
      Assert.AreEqual("24.0.0", device.AliasExecSettings.HardeningSettings.Version);
      Assert.AreEqual(24, device.AliasExecSettings.HardeningSettings.MajorVersion);
    }
  }
}