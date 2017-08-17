using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS {

  public class SNMPSettings : BaseSetting, IConfigSetting {

    public Regex GenericRegex {
      get { return new Regex(@"^snmp-server .*", RegexOptions.IgnoreCase); }
    }

    public IEnumerable<SNMPServer> Servers {
      get {
        var results = GetSettings(new Regex(@"^snmp-server host (\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}) version v(\d+) (\w+) (\w+)", RegexOptions.IgnoreCase));
        foreach (var line in results) {
          yield return new SNMPServer {
            Address = IPAddress.Parse(line.Groups[1].Value),
            Version = int.Parse(line.Groups[2].Value),
          };
        }
      }
    }

    private IEnumerable<GroupSetting> _Groups;

    public IEnumerable<GroupSetting> Groups {
      get {
        if (_Groups == null) {
          var r1 = GetSettings(new Regex(@"^snmp-server group (\w+) v(\d+) priv access (\d+) \w+ (\w+) \w+ (\w+)( \w+ (\w+))?", RegexOptions.IgnoreCase));
          _Groups = r1.Select(c => new GroupSetting {
            Name = c.Groups[1].Value,
            Version = int.Parse(c.Groups[2].Value),
            AccessGroup = int.Parse(c.Groups[3].Value),
            Read = c.Groups[4].Value,
            Write = c.Groups[5].Value,
            Notify = c.Groups[6].Value,
          });
        }
        return _Groups;
      }
    }

    public IEnumerable<Community> CommunityStrings {
      get {
        var r1 = GetSettings(new Regex(@"^snmp-server community (\w+) (\w+)", RegexOptions.IgnoreCase));
        return r1.Select(c => new Community {
          Value = c.Groups[1].Value,
          CommunityMode = (Community.Mode)Enum.Parse(typeof(Community.Mode), c.Groups[2].Value)
        });
      }
    }

    public string SNMPLocation {
      get {
        var r = GetSetting(new Regex(@"^snmp-server location (.*)$", RegexOptions.IgnoreCase));
        return (r == null) ? null : r.Groups[1].Value;
      }
    }

    public class Community {

      public enum Mode {
        ro,
        rw
      }

      public string Value { get; set; }

      public Mode CommunityMode { get; set; }
    }

    public class SNMPServer {

      public IPAddress Address { get; set; }

      public int Version { get; set; }
    }

    public class GroupSetting {

      public string Name { get; set; }

      public int Version { get; set; }

      public int AccessGroup { get; set; }

      public string Read { get; set; }

      public string Write { get; set; }

      public string Notify { get; set; }
    }
  }
}