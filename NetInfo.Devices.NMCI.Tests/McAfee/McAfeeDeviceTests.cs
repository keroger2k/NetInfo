using System.Linq;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.McAfee {

  [TestFixture]
  public class McAfeeDeviceTests {

    [Test]
    public void bos_device_should_be_able_correctly_extract_test_script_header() {
      var config = new AssetBlob {
        Body = @"!#****************************************************************************************#
!#***  Version: McAfee IPS ST&E Test Script 2.0 - NMCI          #
!#****************************************************************************************#
!#***  Devices:  McAfee IPS devices          #
!#****************************************************************************************#
!#***  Purpose: Use this script to pull ST&E results         #
!#****************************************************************************************#
!#***  Note: Ignore any Invalid Syntax Errors         #
!#****************************************************************************************#
!#     Result File to be posted:          #
!#                                 #
!#     Hostname Date Time.Txt                                                             #
!#****************************************************************************************#
intruShell@NAWEBANGSN90>   "
      };

      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(config);

      Assert.AreEqual(13, device.TestScriptHeader.Count());
    }
  }
}