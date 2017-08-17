using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS040_Tests {

    [Test]
    public void BS040_should_return_true_when_re_auth_period_is_less_than_or_equal_to_3600_seconds() {
      var blob = new AssetBlob {
        Body = @"SSH@NRFK-U01-AS-10#
SSH@NRFK-U01-AS-10#show dot1x
PAE Capability : Authenticator Only
system-auth-control : Enable
re-authentication : Enable
global-filter-strict-security : Enable
quiet-period : 30 Seconds
tx-period : 30 Seconds
supptimeout : 30 Seconds
servertimeout : 15 Seconds
maxreq : 2
reAuthMax : 2
re-authperiod : 3600 Seconds
Protocol Version : 1
SSH@NRFK-U01-AS-10#
SSH@NRFK-U01-AS-10#
SSH@NRFK-U01-AS-10#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS040(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS040_should_return_false_when_re_auth_period_is_not_less_than_or_equal_to_3600_seconds() {
      var blob = new AssetBlob {
        Body = @"SSH@NRFK-U01-AS-10#
SSH@NRFK-U01-AS-10#show dot1x
PAE Capability : Authenticator Only
system-auth-control : Enable
re-authentication : Enable
global-filter-strict-security : Enable
quiet-period : 30 Seconds
tx-period : 30 Seconds
supptimeout : 30 Seconds
servertimeout : 15 Seconds
maxreq : 2
reAuthMax : 2
re-authperiod : 3601 Seconds
Protocol Version : 1
SSH@NRFK-U01-AS-10#
SSH@NRFK-U01-AS-10#
SSH@NRFK-U01-AS-10#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS040(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS040_should_return_false_when_re_auth_period_is_not_found() {
      var blob = new AssetBlob {
        Body = @"SSH@NRFK-U01-AS-10#
SSH@NRFK-U01-AS-10#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS040(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS040_should_return_false_when_dot1x_is_not_enabled() {
      var blob = new AssetBlob {
        Body = @"SSH@JAXS-U03-AS-03#show dot1x
Error - 802.1X  is not enabled
SSH@JAXS-U03-AS-03#"
      };

      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS040(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}