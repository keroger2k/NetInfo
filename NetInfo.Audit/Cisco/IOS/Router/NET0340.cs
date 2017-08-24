using NetInfo.Devices.IOS;
using System.Linq;

namespace NetInfo.Audit.Cisco.IOS.Router
{

    /// <summary>
    /// Infrastructure Router Security Technical Implementation Guide Cisco :: Release: 23 Benchmark Date: 28 Jul 2017
    /// 
    /// Rule Title: Network devices must display the DoD-approved logon banner warning.
    /// STIG ID:	NET0340     
    /// Rule ID:	SV-3013r4_rule
    /// Vuln ID:	V-3013       
    /// Severity:	CAT II  Class:	Unclass
    /// </summary>
    public class NET0340 : ICiscoRouterSecurityItem
    {

        private const string _banner = @"banner login ^C

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

^C";

        private IIOSDevice _device;
        public NET0340(IIOSDevice device)
        {
            this._device = device;
        }

        public bool Compliant()
        {
            string strippedCorrectBanner = _banner.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
            var bannerToCheck = _device.Banner;
            if (!bannerToCheck.Any()) { return false; }
            string strippedBannerToCheck = string.Join("\r", bannerToCheck).Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
            return strippedBannerToCheck.Equals(strippedCorrectBanner); ;
        }
    }
}