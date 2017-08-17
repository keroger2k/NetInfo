using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowVtpStatus {
    private readonly VtpSetting _vtpSetting;

    public ShowVtpStatus(IEnumerable<string> settings) {
      this._vtpSetting = new VtpSetting();
      this._vtpSetting.Settings = settings;
    }

    public string DomainName {
      get {
        return this._vtpSetting.DomainName;
      }
    }

    private class VtpSetting : BaseSetting {

      public string Version {
        get {
          var r = GetSetting(new Regex(@"VTP Version\s+:\s+(.*)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }

      public int Revision {
        get {
          var r = GetSetting(new Regex(@"Configuration Revision\s+:\s+(\d+)", RegexOptions.IgnoreCase));
          return (r == null) ? -1 : int.Parse(r.Groups[1].Value);
        }
      }

      public int MaxVlansSupportedLocally {
        get {
          var r = GetSetting(new Regex(@"Maximum VLANs supported locally\s+:\s+(\d+)", RegexOptions.IgnoreCase));
          return (r == null) ? -1 : int.Parse(r.Groups[1].Value);
        }
      }

      public int NumberOfExistingVlans {
        get {
          var r = GetSetting(new Regex(@"Number of existing VLANs\s+:\s+(\d+)", RegexOptions.IgnoreCase));
          return (r == null) ? -1 : int.Parse(r.Groups[1].Value);
        }
      }

      public string OperatingMode {
        get {
          var r = GetSetting(new Regex(@"VTP Operating Mode\s+:\s+(\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }

      public string DomainName {
        get {
          var r = GetSetting(new Regex(@"VTP Domain Name\s+:\s+(\w+)", RegexOptions.IgnoreCase));
          return (r == null) ? null : r.Groups[1].Value;
        }
      }
    }
  }
}