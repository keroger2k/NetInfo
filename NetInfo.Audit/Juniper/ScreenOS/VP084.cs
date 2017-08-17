using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Juniper.ScreenOS;

namespace NetInfo.Audit.Juniper.ScreenOS {

  /// <summary>
  /// Validate the configuration file contains the entire Test Script Header and the entire test script was captured (see comment)
  /// </summary>
  public class VP084 : ISTIGItem {
    private readonly Regex bannerText = new Regex(@"!.*(Version:|Device:|Purpose:|NOTE:)(.*)$", RegexOptions.IgnoreCase);
    private readonly Regex versionRegex = new Regex(@"!.*Version:\s+Netscreen\s+Test\s+Script\s+Version\s+[\d\.]+", RegexOptions.IgnoreCase);
    private readonly Regex deviceRegex = new Regex(@"!.*Device:\s+Use\s+on\s+any\s+Netscreen", RegexOptions.IgnoreCase);
    private readonly Regex purposeRegex = new Regex(@"!.*Purpose:\s+Use\s+this\s+script\s+to\s+gather\s+data\s+for\s+further\s+analysis", RegexOptions.IgnoreCase);
    private readonly Regex noteRegex = new Regex(@"!.*NOTE:\s+Ignore\s+any\s+errors\s+due\s+to\s+syntax\s+or\s+missing\s+hardware", RegexOptions.IgnoreCase);

    public IDevice Device { get; private set; }

    public VP084(INMCIScreenOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var text = ((INMCIScreenOSDevice)Device).TestScriptHeader.Where(c => bannerText.Match(c).Success);
      return text != null && text.Count() == 4 &&
        text.Any(c => versionRegex.Match(c).Success) &&
        text.Any(c => deviceRegex.Match(c).Success) &&
        text.Any(c => purposeRegex.Match(c).Success) &&
        text.Any(c => noteRegex.Match(c).Success);
    }
  }
}