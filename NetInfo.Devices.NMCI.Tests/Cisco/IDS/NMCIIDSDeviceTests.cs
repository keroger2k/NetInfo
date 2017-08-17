using System.Linq;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class NMCIIDSDeviceTests {

    [Test]
    public void cisco_ids_device_should_be_able_correctly_extract_test_script_header() {
      var config = new AssetBlob {
        Body = @"#************************************************************************#
#*** Version: Cisco IDS ST&E Test Script 1.5 - NMCI #
#************************************************************************#
#*** Devices: Cisco IDS devices without access to the operating system #
#************************************************************************#
#*** Purpose: Use this script to pull ST&E results #
#************************************************************************#
#*** Result File to be posted: #
# #
# Hostname Date Time.txt #
# #
#************************************************************************#
terminal length 0"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(config);

      Assert.AreEqual(12, device.TestScriptHeader.Count());
    }
  }
}