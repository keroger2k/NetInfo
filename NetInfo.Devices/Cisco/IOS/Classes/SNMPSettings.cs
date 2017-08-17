using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class SNMPSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^snmp-server .*", RegexOptions.IgnoreCase); }
    }

    public IEnumerable<Server> Servers {
      get {
        var results = GetSettings(new Regex(@"^snmp-server host (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) (informs |inform )?version (\d+) (\w+) (\w+)", RegexOptions.IgnoreCase));
        var list = new List<Server>();
        foreach (var line in results) {
          list.Add(new Server {
            Address = IPAddress.Parse(line.Groups[1].Value),
            Version = int.Parse(line.Groups[3].Value),
            VersionKeyword = line.Groups[4].Value,
            Username = line.Groups[5].Value
          });
        }
        return list.GroupBy(c => c.Address).Select(c => c.First());
      }
    }

    public IEnumerable<Community> Communities {
      get {
        var results = GetSettings(new Regex(@"^snmp-server community (?<communityString>.*) (?<access>\w+) (?<accessList>\d+)", RegexOptions.IgnoreCase));
        foreach (var line in results) {
          yield return new Community {
            CommunityString = line.Groups["communityString"].Value,
            Access = line.Groups["access"].Value,
            FilterAccessList = int.Parse(line.Groups["accessList"].Value),
          };
        }
      }
    }

    public IEnumerable<GroupSetting> Groups {
      get {
        var r1 = GetSettings(new Regex(@"^snmp-server group (\w+) (\w+) (\w+) (\w+) (\w+) (\w+)$", RegexOptions.IgnoreCase));
        var r2 = GetSettings(new Regex(@"^snmp-server group (\w+) (\w+) (\w+) (\w+) (\w+) .*access (\d+)$", RegexOptions.IgnoreCase));
        var t = r2.Select(c => new GroupSetting {
          Name = c.Groups[1].Value,
          Version = c.Groups[2].Value,
          VerionKeyword = c.Groups[3].Value,
          Access = c.Groups[4].Value,
          ViewName = c.Groups[5].Value,
          AccessGroup = int.Parse(c.Groups[6].Value),
        });
        var x = r1.Select(c => new GroupSetting {
          Name = c.Groups[1].Value,
          Version = c.Groups[2].Value,
          VerionKeyword = c.Groups[3].Value,
          Access = c.Groups[4].Value,
          ViewName = c.Groups[5].Value,
          Notify = c.Groups[6].Value.Equals("notify", StringComparison.OrdinalIgnoreCase),
        });
        return (r1.Any()) ? x : (r2.Any()) ? t : null;
      }
    }

    public string Location {
      get {
        var r = GetSetting(new Regex(@"^snmp-server location (.*)$", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Groups[1].Value;
      }
    }

    public string SourceInterface {
      get {
        var r = GetSetting(new Regex(@"^snmp-server\s+source-interface\s+informs\s+(\w+)$", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Groups[1].Value;
      }
    }

    public string TrapSource {
      get {
        var r = GetSetting(new Regex(@"^snmp-server\strap-source\s(?<interface>.*)$", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Groups["interface"].Value;
      }
    }

    public class Server {

      public IPAddress Address { get; set; }

      public int Version { get; set; }

      public string VersionKeyword { get; set; }

      public string Username { get; set; }
    }

    public class GroupSetting {

      public string Name { get; set; }

      public string Version { get; set; }

      public string VerionKeyword { get; set; }

      public string Access { get; set; }

      public string ViewName { get; set; }

      public bool Notify { get; set; }

      public int AccessGroup { get; set; }
    }

    public class Community {

      public string CommunityString { get; set; }

      public string Access { get; set; }

      public int FilterAccessList { get; set; }
    }
  }
}