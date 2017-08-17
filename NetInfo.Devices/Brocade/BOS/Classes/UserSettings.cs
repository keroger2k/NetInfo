using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class UserSettings : BaseSetting, IConfigSetting {

    public IEnumerable<User> Users {
      get {
        var r = GetSettings(new Regex(@"^username (?<name>\w+) password (?<key>\d+) (?<password>.*)$", RegexOptions.IgnoreCase));
        return (r == null) ? new List<User>() : r.Select(c => new User {
          Name = c.Groups["name"].Value,
          Password = c.Groups["password"].Value,
          Key = int.Parse(c.Groups["key"].Value)
        }).GroupBy(c => c.Name).Select(c => c.First()); ;
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^username\s+.*", RegexOptions.IgnoreCase); }
    }

    public class User {

      public string Name { get; set; }

      public string Password { get; set; }

      public int Key { get; set; }
    }
  }
}