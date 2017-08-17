using NetInfo.Audit.Cisco.IOS.Router;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;
using NUnit.Framework;

namespace NetInfo.Audit.Tests.Cisco.IOS.Router {

  [TestFixture]
  public class IR020_Tests {

    [Test]
    public void ir020_should_return_false_when_a_banner_is_not_found() {
      var blob = new AssetBlob {
        Body = @"dial-peer cor custom
!
!
!
alias exec utb_WAN OC3-POS_DISA_v7_3"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR020(device);

      var result = item.Compliant();

      Assert.False(result);
    }

    [Test]
    public void ir020_should_return_true_for_a_device_that_matches_government_banner() {
      var blob = new AssetBlob {
        Body = @"dial-peer cor custom
!
!
!
banner login ^C

You are accessing a U.S. Government (USG) Information System (IS) that is
provided for USG-authorized use only. By using this IS (which includes any
device attached to this IS), you consent to the following conditions:

- The USG routinely intercepts and monitors communications on this IS for
  purposes including, but not limited to, penetration testing, COMSEC
  monitoring, network operations and defense, personnel misconduct (PM), law
  enforcement (LE), and counterintelligence (CI) investigations.
- At any time, the USG may inspect and seize data stored on this IS.
- Communications using, or data stored on, this IS are not private, are subject
  to routine monitoring, interception, and search, and may be disclosed or used
  for any USG-authorized purpose.
- This IS includes security measures (e.g., authentication and access controls)
  to protect USG interests--not for your personal benefit or privacy.
- Notwithstanding the above, using this IS does not constitute consent to PM,
  LE or CI investigative searching or monitoring of the content of privileged
  communications, or work product, related to personal representation or
  services by attorneys, psychotherapists, or clergy, and their assistants.
  Such communications and work product are private and confidential. See User
  Agreement for details.

^C
alias exec utb_WAN OC3-POS_DISA_v7_3"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR020(device);

      var result = item.Compliant();

      Assert.True(result);
    }

    [Test]
    public void ir020_should_return_false_for_a_device_that_doesnt_match_the_government_banner() {
      var blob = new AssetBlob {
        Body = @"dial-peer cor custom
!
!
!
banner login ^C

You are fail a U.S. Government (USG) Information System (IS) that is
provided for USG-authorized use only. By using this IS (which includes any
device attached to this IS), you consent to the following conditions:

- The USG routinely intercepts and monitors communications on this IS for
  purposes including, but not limited to, penetration testing, COMSEC
  monitoring, network operations and defense, personnel misconduct (PM), law
  enforcement (LE), and counterintelligence (CI) investigations.
- At any time, the USG may inspect and seize data stored on this IS.
- Communications using, or data stored on, this IS are not private, are subject
  to routine monitoring, interception, and search, and may be disclosed or used
  for any USG-authorized purpose.
- This IS includes security measures (e.g., authentication and access controls)
  to protect USG interests--not for your personal benefit or privacy.
- Notwithstanding the above, using this IS does not constitute consent to PM,
  LE, or CI investigative searching or monitoring of the content of privileged
  communications, or work product, related to personal representation or
  services by attorneys, psychotherapists, or clergy, and their assistants.
  Such communications and work product are private and confidential. See User
  Agreement for details.

^C
alias exec utb_WAN OC3-POS_DISA_v7_3"
      };
      INMCIIOSDevice device = new NMCIIOSDevice(blob);
      ISTIGItem item = new IR020(device);

      var result = item.Compliant();

      Assert.False(result);
    }
  }
}