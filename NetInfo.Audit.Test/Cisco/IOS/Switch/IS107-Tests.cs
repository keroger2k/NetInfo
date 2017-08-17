using NetInfo.Audit.Cisco.IOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Switch {

  [TestFixture]
  public class IS107_Tests {

    [Test]
    public void IS107_should_return_true_when_banner_has_all_the_correct_words() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !#*** Version: Cisco IOS Test Script Version 5.21 #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !#*** Device: Use on any Cisco device running IOS #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !#*** Purpose: Use this script to gather data for further analysis #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !# NOTE: Ignore any errors due to syntax or missing hardware #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#terminal length 0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS107(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IS107_should_return_false_when_banner_doesnt_has_all_the_correct_words() {
      var blob = new AssetBlob {
        Body = @"ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !#*** Version: Cisco Missing Test Script Version 5.21 #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !#*** Device: Use on any Cisco device running IOS #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !#*** Purpose: Use this script to gather data for further analysis #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#! !# NOTE: Ignore any errors due to syntax or missing hardware #
ABCD-U00-IR-01#! !#**************************************************************************************#
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#!
ABCD-U00-IR-01#terminal length 0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS107(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void IS107_should_return_true_when_header_is_all_there_but_there_is_a_bang_after_hostname() {
      var blob = new AssetBlob {
        Body = @"CRAN-U00-OS-03!#***************************************************************************************
CRAN-U00-OS-03!#***  Version: Cisco IOS Test Script Version 5.23					#
CRAN-U00-OS-03!#***************************************************************************************
CRAN-U00-OS-03!#***  Device:  Use on any Cisco device running IOS 					#
CRAN-U00-OS-03!#***************************************************************************************
CRAN-U00-OS-03!#***  Purpose: Use this script to gather data for further analysis
CRAN-U00-OS-03!#***************************************************************************************
CRAN-U00-OS-03!#    NOTE: Ignore any errors due to syntax or missing hardware
CRAN-U00-OS-03!#***************************************************************************************
CRAN-U00-OS-03#terminal length 0"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IS107(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}