using System.Linq;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.Cisco.IOS {

  [TestFixture]
  public class BannerTests {

    [Test]
    public void correctly_parses_out_valid_banner() {
      var config = new AssetBlob {
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
ABCD-U00-IR-01#terminal length 0
ABCD-U00-IR-01#!"
      };

      INMCIIOSDevice device = new NMCIIOSDevice(config);
      Assert.AreEqual(9, device.TestScriptHeader.Count());
    }
  }
}