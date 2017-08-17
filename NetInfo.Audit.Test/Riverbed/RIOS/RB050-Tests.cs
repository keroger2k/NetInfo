using NetInfo.Audit.Riverbed.RIOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class RB050_Tests {

    [Test]
    public void RB050_should_return_true_for_updated_banner_v5_20() {
      var blob = new AssetBlob {
        Body = @"PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Version:     WANX Test Script Version 5.20 #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Device:  Use on any Riverbed WAN Interceptor #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Purpose: Use this script to gather data for further analysis #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** NOTE: Ignore any errors due to syntax or missing hardware #
PRLH-U00-WX-02 # # ## *** NOTE: Set columns to 400 to prevent output from being distorted #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # #
PRLH-U00-WX-02 # #
PRLH-U00-WX-02 # #"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB050(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB050_should_return_true_when_banner_has_all_the_correct_words() {
      var blob = new AssetBlob {
        Body = @"PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Version: WANX Test Script Version 5.19 #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Device: Use on any Riverbed WAN Accelerator #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Purpose: Use this script to gather data for further analysis #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** NOTE: Ignore any errors due to syntax or missing hardware #
PRLH-U00-WX-02 # # ## *** NOTE: Set columns to 400 to prevent output from being distorted #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # #
PRLH-U00-WX-02 # #
PRLH-U00-WX-02 # #"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB050(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void RB050_should_return_false_when_banner_doesnt_has_all_the_correct_words() {
      var blob = new AssetBlob {
        Body = @"PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Version: WANX Test Script Version 5.19 #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Device: Use on fail Riverbed WAN Accelerator #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** Purpose: Use this script to gather data for further analysis #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # # ## *** NOTE: Ignore any errors due to syntax or missing hardware #
PRLH-U00-WX-02 # # ## *** NOTE: Set columns to 400 to prevent output from being distorted #
PRLH-U00-WX-02 # # ## **************************************************************************************#
PRLH-U00-WX-02 # #
PRLH-U00-WX-02 # #
PRLH-U00-WX-02 # #"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB050(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void RB050_should_return_true_for_banner_type_1() {
      var blob = new AssetBlob {
        Body = @"## ************************************************************ **************************#

M012-U00-WX-01 # ## *** Version: WANX Test Script Version 5.16 #

M012-U00-WX-01 # ## ************************************************************ **************************#

M012-U00-WX-01 # ## *** Device: Use on any Riverbed WAN Accelerator #

M012-U00-WX-01 # ## ************************************************************ **************************#

M012-U00-WX-01 # ## *** Purpose: Use this script to gather data for further ana lysis #

M012-U00-WX-01 # ## ************************************************************ **************************#

M012-U00-WX-01 # ## *** NOTE: Ignore any errors due to syntax or missing hardw are #

M012-U00-WX-01 # ## *** NOTE: Set columns to 400 to prevent output from being distorted #

M012-U00-WX-01 # ## ************************************************************ **************************#

M012-U00-WX-01 # #

M012-U00-WX-01 # #

M012-U00-WX-01 # #

M012-U00-WX-01 # #

M012-U00-WX-01 # #

M012-U00-WX-01 # no cli session paging enable"
      };

      INMCIRIOSDevice device = new NMCIRIOSDevice(blob);
      ISTIGItem item = new RB050(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}