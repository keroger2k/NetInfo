using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Riverbed.RIOS {

  public class UserSettings : BaseSetting, IConfigSetting {

    public IEnumerable<User> Users {
      get {
        int tmp = 0;
        var r = GetSettings(new Regex(@"^\s+username \""?(?<name>\w+)\""? password ((?<encryption>\d+) )?(?<hash>.*)$", RegexOptions.IgnoreCase));
        return r == null ? new List<User>() : r.Select(c => new User {
          Name = c.Groups["name"].Value,
          Encryption = int.TryParse(c.Groups["encryption"].Value, out tmp) ? tmp : 0,
          Hash = c.Groups["hash"].Value
        });
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^\s+username .*$", RegexOptions.IgnoreCase); }
    }

    public class User {

      public string Name { get; set; }

      public int Encryption { get; set; }

      public string Hash { get; set; }
    }
  }
}