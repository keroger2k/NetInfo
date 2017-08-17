using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.McAfee;

namespace NetInfo.Audit.McAfee {

  /// <summary>
  /// Validate the configuration file contains the entire Test Script Header.
  /// </summary>
  public class IP015 : ISTIGItem {
    private readonly Regex bannerText = new Regex(@"^.*(Version:|Devices:|Purpose:|Note:).*$", RegexOptions.IgnoreCase);
    private readonly Regex versionRegex = new Regex(@"^.*Version:\s+McAfee\s+IPS\s+ST&E\s+Test\s+Script\s+[\d\.]+", RegexOptions.IgnoreCase);
    private readonly Regex deviceRegex = new Regex(@"^.*Devices:\s+McAfee\s+IPS\s+sensor\s+devices", RegexOptions.IgnoreCase);
    private readonly Regex purposeRegex = new Regex(@"^.*Purpose:\s+Use\s+this\s+script\s+to\s+pull\s+ST&E\s+results", RegexOptions.IgnoreCase);
    private readonly Regex noteRegex = new Regex(@"^.*Note:\s+Ignore\s+any\s+Invalid\s+Syntax\s+Errors", RegexOptions.IgnoreCase);

    public IDevice Device { get; private set; }

    public IP015(INMCIMcAfeeDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var text = ((INMCIMcAfeeDevice)Device).TestScriptHeader.Where(c => bannerText.Match(c).Success);
      return text != null && text.Count() == 4 &&
        text.Any(c => versionRegex.Match(c).Success) &&
        text.Any(c => deviceRegex.Match(c).Success) &&
        text.Any(c => purposeRegex.Match(c).Success) &&
        text.Any(c => noteRegex.Match(c).Success);
    }
  }
}