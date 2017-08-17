using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR135_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void IR135_should_return_true_when_banner_has_all_the_correct_words() {
      blob = new AssetBlob {
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
      ISTIGItem item = new IR135(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IR135_should_return_false_when_banner_doesnt_has_all_the_correct_words() {
      blob = new AssetBlob {
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
      ISTIGItem item = new IR135(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}