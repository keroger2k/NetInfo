using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowVtpPassword {
    private readonly VtpSetting _vtpSetting;

    public ShowVtpPassword(IEnumerable<string> settings) {
      this._vtpSetting = new VtpSetting();
      this._vtpSetting.Settings = settings;
    }

    public string Password {
      get {
        return this._vtpSetting.Password;
      }
    }

    public bool IsPasswordCommandSupported {
      get {
        return this._vtpSetting.IsPasswordCommandSupported;
      }
    }

    private class VtpSetting : BaseSetting {
      private bool? _isPasswordCommandSupported;

      public bool IsPasswordCommandSupported {
        get {
          if (!_isPasswordCommandSupported.HasValue) {
            var r = GetSetting(new Regex(@"Invalid", RegexOptions.IgnoreCase));
            _isPasswordCommandSupported = (r == null);
          }
          return _isPasswordCommandSupported.Value;
        }
      }

      private string _password;

      public string Password {
        get {
          if (_password == null) {
            var r = GetSetting(new Regex(@"VTP (Encrypted )?Password:\s+(?<password>.*)", RegexOptions.IgnoreCase));
            _password = (r == null) ? null : r.Groups["password"].Value;
          }
          return _password;
        }
      }
    }
  }
}