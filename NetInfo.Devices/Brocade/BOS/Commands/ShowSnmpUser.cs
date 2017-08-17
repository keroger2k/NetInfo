using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS.Commands {

  public class ShowSnmpUser {
    private const string USERNAME = @"username\s+=\s+(?<username>\w+)";
    private const string PRIVTYPE = @"privtype\s+=\s+(?<privType>\w+)";
    private const string AUTHTYPE = @"authtype\s+=\s+(?<authType>\w+)";

    private Regex _usernameRegex = new Regex(USERNAME, RegexOptions.IgnoreCase);
    private Regex _privTypeRegex = new Regex(PRIVTYPE, RegexOptions.IgnoreCase);
    private Regex _authTypeRegex = new Regex(AUTHTYPE, RegexOptions.IgnoreCase);

    public enum AuthType {
      none,
      sha1,
      md5
    }

    public enum PrivType {
      none,
      des,
      aes
    }

    private readonly IEnumerable<string> _output;

    public ShowSnmpUser(IEnumerable<string> output) {
      this._output = output;
    }

    public SnmpUserResult SnmpUser {
      get {
        var username = _output.Where(c => _usernameRegex.Match(c).Success).FirstOrDefault();
        var privType = _output.Where(c => _privTypeRegex.Match(c).Success).FirstOrDefault();
        var authType = _output.Where(c => _authTypeRegex.Match(c).Success).FirstOrDefault();

        return new SnmpUserResult {
          Username = string.IsNullOrEmpty(username) ? string.Empty : _usernameRegex.Match(username).Groups["username"].Value,
          PrivType = string.IsNullOrEmpty(privType) ? PrivType.none : (PrivType)Enum.Parse(typeof(PrivType), _privTypeRegex.Match(privType).Groups["privType"].Value),
          AuthType = string.IsNullOrEmpty(privType) ? AuthType.none : (AuthType)Enum.Parse(typeof(AuthType), _authTypeRegex.Match(authType).Groups["authType"].Value),
        };
      }
    }

    public class SnmpUserResult {

      public string Username { get; set; }

      public int AccessListId { get; set; }

      public string Group { get; set; }

      public string SecurityModel { get; set; }

      public string GroupAccessListId { get; set; }

      public AuthType AuthType { get; set; }

      public string AuthKey { get; set; }

      public PrivType PrivType { get; set; }

      public string PrivKey { get; set; }

      public string EngineId { get; set; }
    }
  }
}