using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IDS.Commands {

  public class ShowUsersAll {
    private readonly static Regex UserRegex = new Regex(@"(\*\s+\d+\s+)?(?<user>[\w\(\)]+)\s+(?<priv>\w+)", RegexOptions.IgnoreCase);
    private readonly IEnumerable<string> _output;

    public ShowUsersAll(IEnumerable<string> output) {
      this._output = output;
    }

    public IEnumerable<User> Users {
      get {
        var list = new List<User>();
        for (int i = 0; i < _output.Count(); i++) {
          if (UserRegex.Match(_output.ElementAt(i)).Success) {
            var m = UserRegex.Match(_output.ElementAt(i));
            var u = new User();
            u.Name = m.Groups["user"].Value;
            u.UserPrivilege = (User.Privilege)Enum.Parse(typeof(User.Privilege), m.Groups["priv"].Value);
            list.Add(u);
          }
        }
        return list;
      }
    }

    public class User {

      public enum Privilege {
        administrator,
        viewer,
        service,
        configuration
      }

      public string Name { get; set; }

      public Privilege UserPrivilege { get; set; }
    }
  }
}