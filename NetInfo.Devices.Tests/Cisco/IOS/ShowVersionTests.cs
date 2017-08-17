using System.Collections.Generic;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class ShowVersionTests {

    public IEnumerable<string> ShowVersion1() {
      return @"Cisco IOS Software, C3560 Software (C3560-IPSERVICESK9-M), Version 12.2(55)SE4, RELEASE SOFTWARE (fc1)
Technical Support: http://www.cisco.com/techsupport
Copyright (c) 1986-2011 by Cisco Systems, Inc.
Compiled Tue 06-Sep-11 02:44 by prod_rel_team
Image text-base: 0x01000000, data-base: 0x02F00000

ROM: Bootstrap program is C3560 boot loader
BOOTLDR: C3560 Boot Loader (C3560-HBOOT-M) Version 12.2(44)SE6, RELEASE SOFTWARE (fc1)

ABCD-U00-IR-01 uptime is 2 weeks, 2 days, 13 hours, 5 minutes
System returned to ROM by power-on
System restarted at 06:29:14 utc Wed Oct 10 2012
System image file is ""flash:c3560-ipservicesk9-mz.122-55.SE4.bin""

This product contains cryptographic features and is subject to United
States and local country laws governing import, export, transfer and
use. Delivery of Cisco cryptographic products does not imply
third-party authority to import, export, distribute or use encryption.
Importers, exporters, distributors and users are responsible for
compliance with U.S. and local country laws. By using this product you
agree to comply with applicable laws and regulations. If you are unable
to comply with U.S. and local laws, return this product immediately.

A summary of U.S. laws governing Cisco cryptographic products may be found at:
http://www.cisco.com/wwl/export/crypto/tool/stqrg.html

If you require further assistance please contact us by sending email to
export@cisco.com.

cisco WS-C3560-24TS (PowerPC405) processor (revision D0) with 131072K bytes of memory.
Processor board ID CAT1021N2RU
Last reset from power-on
4 Virtual Ethernet interfaces
24 FastEthernet interfaces
2 Gigabit Ethernet interfaces
The password-recovery mechanism is enabled.

512K bytes of flash-simulated non-volatile configuration memory.
Base ethernet MAC Address       : 00:18:19:43:AB:80
Motherboard assembly number     : 73-9897-06
Power supply part number        : 341-0097-02
Motherboard serial number       : CAT102122FZ
Power supply serial number      : DCA10161SK8
Model revision number           : D0
Motherboard revision number     : A0
Model number                    : WS-C3560-24TS-S
System serial number            : CAT1021N2RU
Top Assembly Part Number        : 800-26160-02
Top Assembly Revision Number    : C0
Version ID                      : V02
CLEI Code Number                : COMMG00ARB
Hardware Board Revision Number  : 0x01

Switch Ports Model              SW Version            SW Image
------ ----- -----              ----------            ----------
*    1 26    WS-C3560-24TS      12.2(55)SE4           C3560-IPSERVICESK9-M

Configuration register is 0xF

".ToConfig();
    }

    [Test]
    public void should_correctly_get_unable_to_process_when_show_version_is_not_correct_or_missing() {
      var sv = new ShowVersion(new string[] { });
      Assert.AreEqual("Unable to process this configuration", sv.ImageFileName);
    }

    [Test]
    public void should_parse_image_for_these_versions_of_show_version() {
      foreach (var item in new[] { ShowVersion1() }) {
        var sv = new ShowVersion(item);
        Assert.AreEqual("c3560-ipservicesk9-mz.122-55.SE4.bin", sv.ImageFileName);
      }
    }
  }
}