using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Cisco.IOS;

namespace NetInfo.Audit.Cisco.IOS.Router {

  /// <summary>
  /// Validate the configuration file contains the entire Test Script Header.
  /// </summary>
  public class IR135 : ISTIGItem {
    private readonly Regex bannerText = new Regex(@"^.*#.*(Version:|Device:|Purpose:|NOTE:)(.*)$", RegexOptions.IgnoreCase);
    private readonly Regex versionRegex = new Regex(@"^.*#.*Version:\s+Cisco\s+IOS\s+Test\s+Script\s+Version\s+[\d\.]+", RegexOptions.IgnoreCase);
    private readonly Regex deviceRegex = new Regex(@"^.*#.*Device:\s+Use\s+on\s+any\s+Cisco\s+device\s+running\s+IOS", RegexOptions.IgnoreCase);
    private readonly Regex purposeRegex = new Regex(@"^.*#.*Purpose:\s+Use\s+this\s+script\s+to\s+gather\s+data\s+for\s+further\s+analysis", RegexOptions.IgnoreCase);
    private readonly Regex noteRegex = new Regex(@"^.*#.*NOTE:\s+Ignore\s+any\s+errors\s+due\s+to\s+syntax\s+or\s+missing\s+hardware", RegexOptions.IgnoreCase);

    public IDevice Device { get; private set; }

    public IR135(INMCIIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var text = ((INMCIIOSDevice)Device).TestScriptHeader.Where(c => bannerText.Match(c).Success);
      return text != null && text.Count() == 4 &&
        text.Any(c => versionRegex.Match(c).Success) &&
        text.Any(c => deviceRegex.Match(c).Success) &&
        text.Any(c => purposeRegex.Match(c).Success) &&
        text.Any(c => noteRegex.Match(c).Success);
    }
  }
}