using System.Linq;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Devices.NMCI.Tests.Brocade.BOS {

  [TestFixture]
  public class NMCIBOSDeviceTests {

    [Test]
    public void bos_device_should_be_able_correctly_extract_test_script_header() {
      var config = new AssetBlob {
        Body = @"
SSH@ALTN-U01-AS-12#! !#************************************************************************************
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

      INMCIBOSDevice device = new NMCIBOSDevice(config);

      Assert.AreEqual(22, device.TestScriptHeader.Count());
    }
  }
}