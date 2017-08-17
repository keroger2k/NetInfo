using NetInfo.Audit.Cisco.IOS.IDS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class ID014_Tests {
    private AssetBlob blob;

    [Test]
    public void ID014_should_return_true_when_a_complete_test_script_header_is_found_example_1() {
      blob = new AssetBlob {
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
      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID014(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void ID014_should_return_true_when_a_complete_test_script_header_is_found_example_2() {
      blob = new AssetBlob {
        Body = @"!#****************************************************************************************#
!#*** Version: Cisco IDS ST&E Test Script 2.0 - NMCI #
!#****************************************************************************************#
!#*** Devices: Cisco IDS devices without access to the operating system	 #
!#****************************************************************************************#
!#*** Purpose: Use this script to pull ST&E results #
!#****************************************************************************************#
!# Result File to be posted:	 #
!# #
!# Hostname Date Time.Txt #
!# Result File to be posted: #
!#***************************************************************************************#
naweprlhsn20# terminal length 0"
      };
      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID014(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void ID014_should_return_false_when_an_incomplete_test_script_header_is_found_example_1() {
      blob = new AssetBlob {
        Body = @"#************************************************************************#
#*** Version: Cisco IDS ST&E Test Script 1.5 - NMCI #
#************************************************************************#
#*** Devices: Cisco IDS FAIL without access to the operating system #
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
      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID014(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}