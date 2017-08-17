using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class UserSettings : BaseSetting, IConfigSetting {

    public IEnumerable<User> Users {
      get {
        var r = GetSettings(new Regex(@"^username (\w+) privilege (\d+) password (\d+) (\w+)$", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Select(c => new User {
          Username = c.Groups[1].Value,
          PrivilegeLevel = int.Parse(c.Groups[2].Value),
          PasswordType = int.Parse(c.Groups[3].Value),
          Password = c.Groups[4].Value,
        }).GroupBy(c => c.Username).Select(c => c.First());
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^username .*", RegexOptions.IgnoreCase); }
    }

    public class User : IEqualityComparer<User> {

      public string Username { get; set; }

      public int PrivilegeLevel { get; set; }

      public int PasswordType { get; set; }

      public string Password { get; set; }

      public bool Equals(User x, User y) {
        return x.Username.Equals(y.Username, StringComparison.OrdinalIgnoreCase);
      }

      public int GetHashCode(User obj) {
        return obj.GetHashCode();
      }
    }
  }
}