using System.Linq;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Ensure the correct warning banner is being used
  /// </summary>
  public class RB010 : ISTIGItem {

    private const string _banner = @"   banner login ""
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
""
";

    public IDevice Device { get; private set; }

    public RB010(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      string strippedCorrectBanner = _banner.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
      var bannerToCheck = ((INMCIRIOSDevice)Device).Banner;
      if (!bannerToCheck.Any()) { return false; }
      string strippedBannerToCheck = string.Join("\r", bannerToCheck).Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
      return strippedBannerToCheck.Equals(strippedCorrectBanner); ;
    }
  }
}