using System.Linq;
using NetInfo.Devices.Cisco.IOS.Classes.Commands;
using NetInfo.Devices.Tests.Helpers;
using NUnit.Framework;

namespace NetInfo.Devices.Tests.Cisco.IOS {

  [TestFixture]
  public class DirAllFileSystemsTests {

    [Test]
    public void should_have_null_slot0_and_disk0_when_device_does_not_support() {
      var fs = new DirAllFileSystems(@"Directory of flash:/

    2  -rwx        3665  Oct 26 2012 19:35:40 +00:00  private-config.text
    8  -rwx         796   Mar 1 1993 00:00:41 +00:00  vlan.dat

32514048 bytes total (9109504 bytes free)
Directory of system:/

    2  -r--           0                    <no date>  default-running-config
    4  dr-x           0                    <no date>  memory
    1  -rw-       20288  Oct 25 2012 20:07:01 +00:00  running-config
    3  dr-x           0                    <no date>  vfiles

No space information available
Directory of tmpsys:/

    6  drw-           0                    <no date>  eem_lib_system
    9  drw-           0                    <no date>  macro_scripts

No space information available
Directory of nvram:/

  487  -rw-       20288                    <no date>  startup-config
    3  ----          25                    <no date>  persistent-data

524288 bytes total (497211 bytes free)".ToConfig());

      Assert.Null(fs.Slot0);
      Assert.Null(fs.Disk0);
    }

    [Test]
    public void should_parse_slot0_files_correctly() {
      var fs = new DirAllFileSystems(@"Directory of system:/

    2  dr-x           0                    <no date>  memory
    1  -rw-       43399   Mar 9 2011 19:35:58 +00:00  running-config
   12  dr-x           0                    <no date>  vfiles

No space information available
Directory of tmpsys:/

    6  drw-           0                    <no date>  eem_lib_system
    5  drw-           0                    <no date>  eem_lib_user
    4  drw-           0                    <no date>  eem_policy
    1  dr--           0                    <no date>  lib

No space information available
Directory of flexwan-fpd:/

    0  dr--   <no size>                    <no date>
    1  -r--     1225216                    <no date>  CWPA2_FPD_version_10.10

No space information available
Directory of disk0:/

    1  -rw-    80940580  Jun 27 2010 08:27:48 +00:00  s72033-advipservicesk9_wan-mz.122-18.SXF17.bin
    2  -rw-    80828964  Sep 13 2008 15:38:44 +00:00  s72033-advipservicesk9_wan-mz.122-18.SXF14.bin

512065536 bytes total (350289920 bytes free)
Directory of sup-bootflash:/

No files in directory

65536000 bytes total (65536000 bytes free)
Directory of sup-microcode:/

    0  dr--   <no size>                    <no date>
    1  -r--     4212078                    <no date>  CWPA_version_10.10
   24  -r--     1225250                    <no date>  CWPA2_FPD_version_10.10

80995848 bytes total (0 bytes free)
Directory of const_nvram:/

    1  -rw-        4836                    <no date>  vlan.dat

129004 bytes total (124168 bytes free)
Directory of nvram:/

 1918  -rw-       43399                    <no date>  startup-config
 1919  ----        3394                    <no date>  private-config
 1920  -rw-       43399                    <no date>  underlying-config
    1  ----           4                    <no date>  rf_cold_starts
    2  ----          48                    <no date>  persistent-data
    3  -rw-        5278                    <no date>  ifIndex-table

1964024 bytes total (1908987 bytes free)
Directory of bootflash:/

No files in directory

65536000 bytes total (65536000 bytes free)
Directory of sip6-disk0:/

No files in directory

64008192 bytes total (64008192 bytes free)".ToConfig());

      Assert.Null(fs.Slot0);
      Assert.AreEqual(2, fs.Disk0.Count());
    }
  }
}