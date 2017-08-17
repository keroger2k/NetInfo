using NetInfo.Audit.Brocade.BOS.Switch;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Brocade.BOS.Switch {

  [TestFixture]
  public class BS006_Tests {

    [Test]
    public void BS006_should_return_false_when_a_banner_is_not_found() {
      AssetBlob blob = new AssetBlob {
        Body = @"dial-peer cor custom
!
!
!
alias exec utb_WAN OC3-POS_DISA_v7_3"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS006(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void BS006_should_return_true_for_a_device_that_matches_government_banner() {
      AssetBlob blob = new AssetBlob {
        Body = @"dial-peer cor custom
!
!
!
banner motd ^C
^C
You are accessing a U.S. Government (USG) Information System (IS) that is^C
provided for USG-authorized use only. By using this IS (which includes any^C
device attached to this IS), you consent to the following conditions:^C
^C
- The USG routinely intercepts and monitors communications on this IS for^C
purposes including, but not limited to, penetration testing, COMSEC ^C
monitoring, network operations and defense, personnel misconduct (PM), law^C
enforcement (LE), and counterintelligence (CI) investigations.^C
- At any time, the USG may inspect and seize data stored on this IS.^C
- Communications using, or data stored on, this IS are not private, are subject^C
to routine monitoring, interception, and search, and may be disclosed or used^C
for any USG-authorized purpose.^C
- This IS includes security measures (e.g., authentication and access controls)^C
to protect USG interests--not for your personal benefit or privacy.^C
- Notwithstanding the above, using this IS does not constitute consent to PM,^C
LE or CI investigative searching or monitoring of the content of privileged^C
communications, or work product, related to personal representation or ^C
services by attorneys, psychotherapists, or clergy, and their assistants. ^C
Such communications and work product are private and confidential. See User^C
Agreement for details.^C
^C
!
alias exec utb_WAN OC3-POS_DISA_v7_3"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS006(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void BS006_should_return_false_for_a_device_that_doesnt_match_the_government_banner() {
      AssetBlob blob = new AssetBlob {
        Body = @"dial-peer cor custom
!
!
!
banner motd ^C
^C
You are fail a U.S. Government (USG) Information System (IS) that is^C
provided for USG-authorized use only. By using this IS (which includes any^C
device attached to this IS), you consent to the following conditions:^C
^C
- The USG routinely intercepts and monitors communications on this IS for^C
purposes including, but not limited to, penetration testing, COMSEC ^C
monitoring, network operations and defense, personnel misconduct (PM), law^C
enforcement (LE), and counterintelligence (CI) investigations.^C
- At any time, the USG may inspect and seize data stored on this IS.^C
- Communications using, or data stored on, this IS are not private, are subject^C
to routine monitoring, interception, and search, and may be disclosed or used^C
for any USG-authorized purpose.^C
- This IS includes security measures (e.g., authentication and access controls)^C
to protect USG interests--not for your personal benefit or privacy.^C
- Notwithstanding the above, using this IS does not constitute consent to PM,^C
LE, or CI investigative searching or monitoring of the content of privileged^C
communications, or work product, related to personal representation or ^C
services by attorneys, psychotherapists, or clergy, and their assistants. ^C
Such communications and work product are private and confidential. See User^C
Agreement for details.^C
^C
!
alias exec utb_WAN OC3-POS_DISA_v7_3"
      };
      INMCIBOSDevice device = new NMCIBOSDevice(blob);
      ISTIGItem item = new BS006(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}