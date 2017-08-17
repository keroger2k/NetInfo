using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS042_Tests {

    [Test]
    public void BS042_should_return_true_when_a_complete_test_script_header_is_found_example_1() {
      var blob = new AssetBlob {
        Body = @"SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#*** Version: Brocade Layer-2 Test Script Version 1.0.6
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#*** Device: Use on any Brocade device running as Layer-2 only
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#*** Purpose: Use this script to gather data for further analysis
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !# NOTE: Ignore any errors due to syntax or missing hardware
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !# NOTE: Due to buffer issues with some terminal software when connected via the
SSH@ALTN-U01-AS-12#! !# console port, it is STRONGLY recommended that this script be run via
SSH@ALTN-U01-AS-12#! !# a remote terminal (VTY, SSH, Telnet) session.
SSH@ALTN-U01-AS-12#! !#
SSH@ALTN-U01-AS-12#! !# If the console port must be used, increase the buffer size and 'line send
SSH@ALTN-U01-AS-12#! !# delay' (SecureCRT). 3000ms (max) for SecureCRT.
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !# Must be in the privileged level global EXEC (enable)
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#write memory"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS042(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS042_should_return_false_when_an_incomplete_test_script_header_is_found_example_1() {
      var blob = new AssetBlob {
        Body = @"
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#*** Version: Brocade Layer-2 Test Script Version 1.0.6
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#*** Device: Use FAIL any Brocade device running as Layer-2 only
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#*** Purpose: Use this script to gather data for further analysis
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !# NOTE: Ignore any errors due to syntax or missing hardware
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !# NOTE: Due to buffer issues with some terminal software when connected via the
SSH@ALTN-U01-AS-12#! !# console port, it is STRONGLY recommended that this script be run via
SSH@ALTN-U01-AS-12#! !# a remote terminal (VTY, SSH, Telnet) session.
SSH@ALTN-U01-AS-12#! !#
SSH@ALTN-U01-AS-12#! !# If the console port must be used, increase the buffer size and 'line send
SSH@ALTN-U01-AS-12#! !# delay' (SecureCRT). 3000ms (max) for SecureCRT.
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !#
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#! !# Must be in the privileged level global EXEC (enable)
SSH@ALTN-U01-AS-12#! !#************************************************************************************
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#
SSH@ALTN-U01-AS-12#write memory"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS042(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS042_should_return_true_when_hostname_is_not_include_in_test_script_header() {
      var blob = new AssetBlob {
        Body = @"!#****************************************************************************************#
!#***  Version: Brocade Layer-2 Test Script Version 1.0.6 				  #
!#****************************************************************************************#
!#***  Device:  Use on any Brocade device running as Layer-2 only		          #
!#****************************************************************************************#
!#***  Purpose: Use this script to gather data for further analysis 			  #
!#****************************************************************************************#
!#    NOTE: Ignore any errors due to syntax or missing hardware                           #
!#****************************************************************************************#
!#                                                                                        #
!#****************************************************************************************#
!#    NOTE: Due to buffer issues with some terminal software when connected via the       #
!#          console port, it is STRONGLY recommended that this script be run via          #
!#          a remote terminal (VTY, SSH, Telnet) session.                                 #
!#                                                                                        #
!#          If the console port must be used, increase the buffer size and 'line send     #
!#          delay' (SecureCRT).  3000ms (max) for SecureCRT.                              #
!#****************************************************************************************#
!#                                                                                        #
!#****************************************************************************************#
!# Must be in the privileged level global EXEC (enable)                                   #
!#****************************************************************************************#
!
!
!
!
SSH@SDNS-U01-AS-91#write memory"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS042(device);

      var result = item.Compliant();

      Assert.True(result);
    }
  }
}