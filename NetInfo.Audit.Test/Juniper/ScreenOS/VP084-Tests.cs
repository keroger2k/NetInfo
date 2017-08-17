using NetInfo.Audit.Juniper.ScreenOS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class VP084_Tests {
    private AssetBlob blob;

    [SetUp]
    public void Init() {
    }

    [Test]
    public void VP084_should_return_true_when_banner_has_all_the_correct_words() {
      blob = new AssetBlob {
        Body = @"!#***************************************************************************************
!#*** Version: Netscreen Test Script Version 1.4
!#***************************************************************************************
!#*** Device: Use on any Netscreen
!#***************************************************************************************
!#*** Purpose: Use this script to gather data for further analysis
!#***************************************************************************************
!# NOTE: Ignore any errors due to syntax or missing hardware
!#***************************************************************************************
NAWEPRLHVP25-> set console page 0"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP084(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP084_should_return_false_when_banner_doesnt_has_all_the_correct_words() {
      blob = new AssetBlob {
        Body = @"!#***************************************************************************************
!#*** Version: Netscreen Test Script Version 1.4
!#***************************************************************************************
!#*** Device: Use fail any Netscreen
!#***************************************************************************************
!#*** Purpose: Use this script to gather data for further analysis
!#***************************************************************************************
!# NOTE: Ignore any errors due to syntax or missing hardware
!#***************************************************************************************
NAWEPRLHVP25-> set console page 0"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP084(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void VP084_should_parse_this_banner_correctly_banner1() {
      blob = new AssetBlob {
        Body = @"
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** Version: Netscreen Test Script Version 1.4
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** Device: Use on any Netscreen
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** Purpose: Use this script to gather data for further analysis
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** NOTE: Ignore any errors due to syntax or missing hardware
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)->
HA:MCUSQUANVP05(M)-> set console page 0"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP084(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void VP084_should_parse_this_banner_correctly_banner2() {
      blob = new AssetBlob {
        Body = @"
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** Version: Netscreen Test Script Version 1.4
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** Device: Fail on any Netscreen
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** Purpose: Use this script to gather data for further analysis
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)-> !#*** NOTE: Ignore any errors due to syntax or missing hardware
^---------unknown keyword !#***
HA:MCUSQUANVP05(M)-> !#*************************************************************************************************
^-----------------------------------------------------------------------------------------------------unknown keyword !#*************************************************************************************************
HA:MCUSQUANVP05(M)->
HA:MCUSQUANVP05(M)-> set console page 0"
      };

      INMCIScreenOSDevice device = new NMCIScreenOSDevice(blob);
      ISTIGItem item = new VP084(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}