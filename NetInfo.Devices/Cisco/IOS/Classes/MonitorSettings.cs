using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class MonitorSettings : BaseSetting, IConfigSetting {

    public IEnumerable<SessionCommands> Commands {
      get {
        var rgx = new Regex(@"monitor session (?<number>\d+) (?<command>.*)", RegexOptions.IgnoreCase);
        var list = new List<SessionCommands>();
        foreach (var item in Settings) {
          var m = rgx.Match(item);
          if (m.Success) {
            list.Add(new SessionCommands {
              Number = int.Parse(m.Groups["number"].Value),
              Command = m.Groups["command"].Value
            });
          }
        }
        return list.ToList();
      }
    }

    public IEnumerable<Session> Sessions {
      get {
        var list = Commands
          .GroupBy(c => c.Number)
          .Select(g => new Session { Number = g.Key, Commands = g.ToList() });
        return list.ToList();
      }
    }

    public Regex GenericRegex {
      get { return new Regex(@"^monitor session .*", RegexOptions.IgnoreCase); }
    }

    public class SessionCommands {

      public int Number { get; set; }

      public string Command { get; set; }
    }

    public class Session {

      public int Number { get; set; }

      public IEnumerable<SessionCommands> Commands { get; set; }
    }
  }
}