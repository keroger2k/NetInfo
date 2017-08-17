using NetInfo.Audit.Cisco.IOS.IDS;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.IDS {

  [TestFixture]
  public class ID009_Tests {

    [Test]
    public void ID009_should_return_true_when_cids_admin_and_cisco_is_found_and_disabled() {
      var blob = new AssetBlob {
        Body = @"mcusquansn20# show configuration
Generating current config: |Generating current config: /Generating current config: -Generating current config: \Generating current config: |Generating current config: /Generating current config: -Generating current config: |Generating current config: /Generating current config: -Generating current config: \Generating current config: |Generating current config: /Generating current config: -Generating current config: |Generating current config: /Generating current config: -Generating current config: \Generating current config: |Generating current config: /Generating current config: -Generating current config: |Generating current config: /Generating current config: -Generating current config: \Generating current config: |Generating current config: /Generating current config: -Generating current config: |Generating current config: /Generating current config: -Generating current config: \Generating current config: |Generating current config: /Generating current config: -Generating current config: |show users all
mcusquansn20# show clock
*11:52:48 UTC Wed Aug 31 2011
mcusquansn20# show version
Application Partition:
Cisco Intrusion Prevention System, Version 5.1(8)E3
Host:
Realm Keys key1.0
Signature Definition:
Signature Update S439.0 2009-09-30
Virus Update V1.4 2007-03-02
OS Version: 2.4.26-IDS-smp-bigphys
Platform: IDS-4250-SX
Serial Number: 5SNN331
License expired: 30-Sep-2010 UTC
Sensor up-time is 173 days.
Using 460492800 out of 1984651264 bytes of available memory (23% usage)
system is using 17.4M out of 29.0M bytes of available disk space (60% usage)
application-data is using 42.2M out of 174.7M bytes of available disk space (25% usage)
boot is using 35.4M out of 75.9M bytes of available disk space (49% usage)
application-log is using 532.4M out of 2.8G bytes of available disk space (20% usage)
MainApp Z-2008_JUN_06_00_48 (Release) 2008-06-06T01:07:01-0500 Running
AnalysisEngine ZE-2008_OCT_15_22_02 (Release) 2008-10-15T22:18:16-0500 Running
CLI Z-2008_JUN_06_00_48 (Release) 2008-06-06T01:07:01-0500
Upgrade History:
* IPS-sig-S389-req-E3 20:53:32 UTC Wed Apr 01 2009
IPS-sig-S439-req-E3.pkg 18:16:10 UTC Mon Oct 05 2009
Recovery Partition Version 1.1 - 5.1(8)E2
mcusquansn20#
mcusquansn21# show users all
CLI ID User Privilege
* 27412 cids_admin administrator
(cisco) administrator
mcusquansn21# END-OF-TEST-SCRIPT"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID009(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void ID009_should_return_false_when_cids_admin_is_not_found() {
      var blob = new AssetBlob {
        Body = @"mcusquansn21# show users all
CLI ID User Privilege
(cisco) administrator
mcusquansn21# END-OF-TEST-SCRIPT"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID009(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void ID009_should_return_false_when_cisco_user_is_enabled() {
      var blob = new AssetBlob {
        Body = @"mcusquansn21# show users all
CLI ID User Privilege
cisco administrator
mcusquansn21# END-OF-TEST-SCRIPT"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID009(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void ID009_should_return_false_when_cisco_is_not_found() {
      var blob = new AssetBlob {
        Body = @"mcusquansn21# show users all
CLI ID User Privilege
* 27412 cids_admin administrator
mcusquansn21# END-OF-TEST-SCRIPT"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID009(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void ID009_should_return_false_when_neither_cisco_or_cids_admin_are_not_found() {
      var blob = new AssetBlob {
        Body = @"mcusquansn21# show users all
CLI ID User Privilege
mcusquansn21# END-OF-TEST-SCRIPT"
      };

      INMCIIDSDevice device = new NMCIIDSDevice(blob);
      ISTIGItem item = new ID009(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}