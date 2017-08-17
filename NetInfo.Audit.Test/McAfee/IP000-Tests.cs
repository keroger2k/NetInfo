using NetInfo.Audit.McAfee;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.McAfee {

  [TestFixture]
  public class IP000_Tests {

    [Test]
    public void IP000_should_return_true_when_device_has_test_script_correctly_applied() {
      var blob = new AssetBlob {
        Body = @"intruShell@AHDSEDSTSN41> show
intruShell@AHDSEDSTSN41> show acl stats
intruShell@AHDSEDSTSN41> downloadstatus
intruShell@AHDSEDSTSN41> guest-portal status
intruShell@AHDSEDSTSN41> show auditlog status
intruShell@AHDSEDSTSN41> show arp spoof status
intruShell@AHDSEDSTSN41> show auxport status
intruShell@AHDSEDSTSN41> show console timeout
intruShell@AHDSEDSTSN41> show mem-usage
intruShell@AHDSEDSTSN41> show mgmtport
intruShell@AHDSEDSTSN41> show netstat
intruShell@AHDSEDSTSN41> show sensordroppktevent status
intruShell@AHDSEDSTSN41> show sshaccesscontrol status
intruShell@AHDSEDSTSN41> show sshinactivetimeout
intruShell@AHDSEDSTSN41> show ssl config
intruShell@AHDSEDSTSN41> show ssl stats
intruShell@AHDSEDSTSN41> show tacacs
intruShell@AHDSEDSTSN41> show tcpipstats
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-syn inbound
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-syn-ack inbound
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-fin inbound
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-rst inbound
intruShell@AHDSEDSTSN41> status
intruShell@AHDSEDSTSN41> watchdog status
END-OF-TEST-SCRIPT
"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP000(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void IP000_should_return_false_when_device_has_test_script_incorrectly_applied() {
      var blob = new AssetBlob {
        Body = @"intruShell@AHDSEDSTSN41> show
intruShell@AHDSEDSTSN41> show acl statsFAIL
intruShell@AHDSEDSTSN41> show auditlog status
intruShell@AHDSEDSTSN41> show arp spoof status
intruShell@AHDSEDSTSN41> show auxport status
intruShell@AHDSEDSTSN41> show mem-usage
intruShell@AHDSEDSTSN41> show mgmtport
intruShell@AHDSEDSTSN41> show netstat
intruShell@AHDSEDSTSN41> show sensordroppktevent status
intruShell@AHDSEDSTSN41> show sshaccesscontrol status
intruShell@AHDSEDSTSN41> show sshinactivetimeout
intruShell@AHDSEDSTSN41> show ssl config
intruShell@AHDSEDSTSN41> show ssl stats
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-syn inbound
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-syn-ack inbound
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-fin inbound
intruShell@AHDSEDSTSN41> show userconfigvolumedosthreshold tcp-rst inbound
intruShell@AHDSEDSTSN41> status
intruShell@AHDSEDSTSN41> watchdog status
END-OF-TEST-SCRIPT"
      };
      INMCIMcAfeeDevice device = new NMCIMcAfeeDevice(blob);
      ISTIGItem item = new IP000(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}