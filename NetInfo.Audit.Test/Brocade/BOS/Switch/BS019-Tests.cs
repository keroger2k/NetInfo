using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS019_Tests {

    [Test]
    public void BS019_should_return_true_when_approved_version_matches_existing_software() {
      var blob = new AssetBlob {
        Body = @"
SSH@NRFK-U01-AS-10#show version
Copyright (c) 1996-2011 Brocade Communications Systems, Inc.
UNIT 1: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
UNIT 2: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
UNIT 3: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
UNIT 4: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
Boot-Monitor Image size = 369286, Version:07.0.01T7f5 (grz07001)
HW: Stackable FCX648S-HPOE-PREM (PROM-TYPE FCX-ADV-U)
==========================================================================
SSH@NRFK-U01-AS-10#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS019(device, new string[] { "FCXR07203a.bin" });

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS019_should_return_false_when_one_or_more_units_software_does_not_match_approved_version() {
      var blob = new AssetBlob {
        Body = @"
SSH@NRFK-U01-AS-10#show version
Copyright (c) 1996-2011 Brocade Communications Systems, Inc.
UNIT 1: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
UNIT 2: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.3.03aT7f3
UNIT 3: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
UNIT 4: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
Boot-Monitor Image size = 369286, Version:07.0.01T7f5 (grz07001)
HW: Stackable FCX648S-HPOE-PREM (PROM-TYPE FCX-ADV-U)
==========================================================================
SSH@NRFK-U01-AS-10#
"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS019(device, new string[] { "FAIL.bin" });

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS019_should_return_false_when_show_version_is_not_found() {
      var blob = new AssetBlob {
        Body = @""
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS019(device, new string[] { "FCXR07203a.bin" });

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}