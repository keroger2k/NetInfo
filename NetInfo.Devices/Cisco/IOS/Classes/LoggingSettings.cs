using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class LoggingSettings : BaseSetting, IConfigSetting {
    private bool? _isLoggingTrapEnabled;

    public bool isLoggingTrapEnabled {
      get {
        if (_isLoggingTrapEnabled == null) {
          var r = GetSetting(new Regex(@"^no logging\strap", RegexOptions.IgnoreCase));
          _isLoggingTrapEnabled = (r == null);
        }
        return _isLoggingTrapEnabled.Value;
      }
    }

    private string _TrapLevel;

    public string TrapLevel {
      get {
        if (string.IsNullOrEmpty(_TrapLevel)) {
          var r = GetSetting(new Regex(@"^logging\strap\s(\w+)$", RegexOptions.IgnoreCase));
          _TrapLevel = (r == null && this.isLoggingTrapEnabled) ? "informational" : r.Groups[1].Value;
        }
        return _TrapLevel;
      }
    }

    private string _SourceInterface;

    public string SourceInterface {
      get {
        if (string.IsNullOrEmpty(_SourceInterface)) {
          var r = GetSetting(new Regex(@"^logging\ssource-interface\s(?<interface>.*)$", RegexOptions.IgnoreCase));
          _SourceInterface = (r == null) ? null : r.Groups["interface"].Value;
        }
        return _SourceInterface;
      }
    }

    private IEnumerable<IPAddress> _Servers;

    public IEnumerable<IPAddress> Servers {
      get {
        if (_Servers == null) {
          var r = GetSettings(new Regex(@"^logging (host )?(?<ipaddress>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.IgnoreCase));
          _Servers = (r == null) ? new List<IPAddress>() : r.Select(c => IPAddress.Parse(c.Groups["ipaddress"].Value)).GroupBy(c => c).Select(c => c.First());
        }
        return _Servers;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^logging .*|^no logging .*", RegexOptions.IgnoreCase); }
    }
  }
}