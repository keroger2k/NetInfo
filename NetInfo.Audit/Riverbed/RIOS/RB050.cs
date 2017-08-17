using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Riverbed.RIOS;

namespace NetInfo.Audit.Riverbed.RIOS {

  /// <summary>
  /// Validate the configuration file contains the entire Test Script Header.
  /// </summary>
  public class RB050 : ISTIGItem {
    private readonly Regex bannerText = new Regex(@"[\w-]+.*(Version:|Device:|Purpose:|NOTE:)(.*)$", RegexOptions.IgnoreCase);
    private readonly Regex versionRegex = new Regex(@"[\w-]+.*Version:\s+WANX\s+Test\s+Script\s+Version\s+[\d\.]+", RegexOptions.IgnoreCase);
    private readonly Regex deviceRegex = new Regex(@"[\w-]+.*Device:\s+Use\s+on\s+any\s+Riverbed\s+WAN\s+(Accelerator|Interceptor)", RegexOptions.IgnoreCase);
    private readonly Regex purposeRegex = new Regex(@"[\w-]+.*Purpose:\s+Use\s+this\s+script\s+to\s+gather\s+data\s+for\s+further\s+analysis", RegexOptions.IgnoreCase);
    private readonly Regex note1Regex = new Regex(@"[\w-]+.*NOTE:\s+Ignore\s+any\s+errors\s+due\s+to\s+syntax\s+or\s+missing\s+hardware", RegexOptions.IgnoreCase);
    private readonly Regex note2Regex = new Regex(@"[\w-]+.*NOTE:\s+Set\s+columns\s+to\s+400\s+to\s+prevent\s+output\s+from\s+being\s+distorted", RegexOptions.IgnoreCase);

    public IDevice Device { get; private set; }

    public RB050(INMCIRIOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var text = ((INMCIRIOSDevice)Device).TestScriptHeader.Where(c => bannerText.Match(c).Success);
      return text != null && text.Count() == 5 &&
        text.Any(c => versionRegex.Match(c).Success) &&
        text.Any(c => deviceRegex.Match(c).Success) &&
        text.Any(c => purposeRegex.Match(c).Success) &&
        text.Any(c => note1Regex.Match(c).Success) &&
        text.Any(c => note2Regex.Match(c).Success);
    }
  }
}