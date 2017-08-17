using System.Linq;
using System.Text.RegularExpressions;
using NetInfo.Devices;
using NetInfo.Devices.NMCI.Brocade.BOS;

namespace NetInfo.Audit.Brocade.BOS.Switch {

  /// <summary>
  /// Validate the configuration file contains the entire Test Script Header.
  /// </summary>
  public class BS042 : ISTIGItem {
    private readonly Regex bannerText = new Regex(@"^.*(Version:|Device:|Purpose:|NOTE:).*$", RegexOptions.IgnoreCase);
    private readonly Regex versionRegex = new Regex(@"^.*Version:\s+Brocade\s+Layer-2\s+Test\s+Script\s+Version\s+[\d\.]+", RegexOptions.IgnoreCase);
    private readonly Regex deviceRegex = new Regex(@"^.*Device:\s+Use\s+on\s+any\s+Brocade\s+device\s+running\s+as\s+Layer-2\s+only", RegexOptions.IgnoreCase);
    private readonly Regex purposeRegex = new Regex(@"^.*Purpose:\s+Use\s+this\s+script\s+to\s+gather\s+data\s+for\s+further\s+analysis", RegexOptions.IgnoreCase);
    private readonly Regex note1Regex = new Regex(@"^.*NOTE:\s+Ignore\s+any\s+errors\s+due\s+to\s+syntax\s+or\s+missing\s+hardware", RegexOptions.IgnoreCase);
    private readonly Regex note2Regex = new Regex(@"^.*NOTE:\s+Due\s+to\s+buffer\s+issues\s+with\s+some\s+terminal\s+software\s+when\s+connected\s+via\s+the", RegexOptions.IgnoreCase);

    public IDevice Device { get; private set; }

    public BS042(INMCIBOSDevice device) {
      this.Device = device;
    }

    public bool Compliant() {
      var text = ((INMCIBOSDevice)Device).TestScriptHeader.Where(c => bannerText.Match(c).Success);
      return text != null && text.Count() == 5 &&
        text.Any(c => versionRegex.Match(c).Success) &&
        text.Any(c => deviceRegex.Match(c).Success) &&
        text.Any(c => purposeRegex.Match(c).Success) &&
        text.Any(c => note1Regex.Match(c).Success) &&
      text.Any(c => note2Regex.Match(c).Success);
    }
  }
}