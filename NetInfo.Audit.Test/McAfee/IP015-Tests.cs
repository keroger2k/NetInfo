using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP015_Tests {

    [Test]
    public void IP015_should_return_true_when_updated_test_script_is_found() {
      var blob = new AssetBlob {
        Body = @"!#****************************************************************************************#
!#***  Version: McAfee IPS ST&E Test Script 2.0 - NMCI          #
!#****************************************************************************************#
!#***  Devices:  McAfee IPS sensor devices          #
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
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP015(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP015_should_return_false_when_old_test_script_header_is_found() {
      var blob = new AssetBlob {
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
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP015(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IP015_should_return_false_when_an_incomplete_test_script_header_is_found_example_1() {
      var blob = new AssetBlob {
        Body = @"!#****************************************************************************************#
!#***  Version: McAfee IPS ST&E Test Script 2.0 - NMCI          #
!#****************************************************************************************#
!#***  Devices:  McAfee FAIL devices          #
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
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP015(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}