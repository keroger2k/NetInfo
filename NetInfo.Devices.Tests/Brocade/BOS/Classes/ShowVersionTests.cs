using System.Collections.Generic;
using System.Linq;
using NetInfo.Devices.Brocade.BOS.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Brocade.BOS {

  [TestFixture]
  public class ShowVersionTests {

    private IEnumerable<string> config = @"Copyright (c) 1996-2011 Brocade Communications Systems, Inc.
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
UNIT 5: compiled on Jun 28 2011 at 18:39:17 labeled as FCXR07203a
(6672961 bytes) from Primary FCXR07203a.bin
SW: Version 07.2.03aT7f3
Boot-Monitor Image size = 369292, Version:07.1.00T7f5 (grz07100)
HW: Stackable FCX648S-HPOE-PREM (PROM-TYPE FCX-ADV-U)
==========================================================================
UNIT 1: SL 1: FCX-48GS POE 48-port Management Module
Serial #: BCY2219G0VY
License: FCX_ADV_ROUTER_SOFT_PACKAGE (LID: deaHHGOiFxa)
P-ENGINE 0: type DB90, rev 01
P-ENGINE 1: type DB90, rev 01
PROM-TYPE: FCX-ADV-U
==========================================================================
UNIT 1: SL 2: FCX-2XGC 2-port 16G Module (2-CX4)
==========================================================================
UNIT 2: SL 1: FCX-48GS POE 48-port Management Module
Serial #: BCY2231G0ZL
License: FCX_ADV_ROUTER_SOFT_PACKAGE (LID: deaHHIGiFbn)
P-ENGINE 0: type DB90, rev 01
P-ENGINE 1: type DB90, rev 01
PROM-TYPE: FCX-ADV-U
==========================================================================
UNIT 2: SL 2: FCX-2XGC 2-port 16G Module (2-CX4)
==========================================================================
UNIT 3: SL 1: FCX-48GS POE 48-port Management Module
Serial #: BCY2228G08Y
License: FCX_ADV_ROUTER_SOFT_PACKAGE (LID: deaHHHNiFNa)
P-ENGINE 0: type DB90, rev 01
P-ENGINE 1: type DB90, rev 01
PROM-TYPE: FCX-ADV-U
==========================================================================
UNIT 3: SL 2: FCX-2XGC 2-port 16G Module (2-CX4)
==========================================================================
UNIT 4: SL 1: FCX-48GS POE 48-port Management Module
Serial #: BCY2205G040
License: FCX_ADV_ROUTER_SOFT_PACKAGE (LID: deaHHFKiFJF)
P-ENGINE 0: type DB90, rev 01
P-ENGINE 1: type DB90, rev 01
PROM-TYPE: FCX-ADV-U
==========================================================================
UNIT 4: SL 2: FCX-2XGC 2-port 16G Module (2-CX4)
==========================================================================
UNIT 5: SL 1: FCX-48GS POE 48-port Management Module
Serial #: BCY2229G0LF
License: FCX_ADV_ROUTER_SOFT_PACKAGE (LID: deaHHHOiFnh)
P-ENGINE 0: type DB90, rev 01
P-ENGINE 1: type DB90, rev 01
PROM-TYPE: FCX-ADV-U
==========================================================================
UNIT 5: SL 2: FCX-2XGC 2-port 16G Module (2-CX4)
==========================================================================
800 MHz Power PC processor 8544E (version 0021/0022) 400 MHz bus
65536 KB flash memory
256 MB DRAM
STACKID 1 system uptime is 11 hours 17 minutes 31 seconds
STACKID 2 system uptime is 11 hours 13 minutes 13 seconds
STACKID 3 system uptime is 11 hours 13 minutes 55 seconds
STACKID 4 system uptime is 11 hours 13 minutes 30 seconds
STACKID 5 system uptime is 11 hours 12 minutes 53 seconds
The system started at 23:53:23 GMT+00 Fri Sep 23 2011
The system : started=cold start
My stack unit ID = 1, bootup role = active".ToConfig();

    [Test]
    public void show_version_correctly_identifies_the_number_of_units() {
      var sv = new ShowVersion(config);

      Assert.AreEqual(5, sv.Units.Count());
    }

    [Test]
    public void show_version_correctly_identifies_all_the_software_versions() {
      var sv = new ShowVersion(config);

      Assert.AreEqual("07.2.03aT7f3", sv.Units.ElementAt(0).SoftwareVersion);
      Assert.AreEqual("07.2.03aT7f3", sv.Units.ElementAt(1).SoftwareVersion);
      Assert.AreEqual("07.2.03aT7f3", sv.Units.ElementAt(2).SoftwareVersion);
      Assert.AreEqual("07.2.03aT7f3", sv.Units.ElementAt(3).SoftwareVersion);
      Assert.AreEqual("07.2.03aT7f3", sv.Units.ElementAt(4).SoftwareVersion);
    }

    [Test]
    public void show_version_correctly_identifies_all_the_unit_numbers() {
      var sv = new ShowVersion(config);

      Assert.AreEqual(1, sv.Units.ElementAt(0).Number);
      Assert.AreEqual(2, sv.Units.ElementAt(1).Number);
      Assert.AreEqual(3, sv.Units.ElementAt(2).Number);
      Assert.AreEqual(4, sv.Units.ElementAt(3).Number);
      Assert.AreEqual(5, sv.Units.ElementAt(4).Number);
    }
  }
}