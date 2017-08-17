using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS.IDS;

namespace NetInfo.Audit.Cisco.IOS.IDS {

  /// <summary>
  /// Validate the configuration file contains the entire Test Script Header.
  /// </summary>
  public class ID014 : ISTIGItem {
    private readonly Regex bannerText = new Regex(@"^.*(Version:|Devices:|Purpose:).*$", RegexOptions.IgnoreCase);
    private readonly Regex versionRegex = new Regex(@"^.*Version:\s+Cisco\s+IDS\s+ST&E\s+Test\s+Script\s+[\d\.]+", RegexOptions.IgnoreCase);
    private readonly Regex deviceRegex = new Regex(@"^.*Devices:\s+Cisco\s+IDS\s+devices\s+without\s+access\s+to\s+the\s+operating\s+system", RegexOptions.IgnoreCase);
    private readonly Regex purposeRegex = new Regex(@"^.*Purpose:\s+Use\s+this\s+script\s+to\s+pull\s+ST&E\s+results", RegexOptions.IgnoreCase);

    public IDevice Device { get; private set; }

    public ID014(INMCIIDSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var text = ((INMCIIDSDevice)Device).TestScriptHeader.Where(c => bannerText.Match(c).Success);
      return text != null && text.Count() == 3 &&
        text.Any(c => versionRegex.Match(c).Success) &&
        text.Any(c => deviceRegex.Match(c).Success) &&
        text.Any(c => purposeRegex.Match(c).Success);
    }
  }
}