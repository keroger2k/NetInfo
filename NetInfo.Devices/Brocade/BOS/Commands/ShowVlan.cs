using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS.Commands {

  public class ShowVlan : BaseSetting {
    private readonly Regex VLAN_REGEX = new Regex(@"PORT-VLAN (?<number>\d+), Name (?<name>[\w-]+)", RegexOptions.IgnoreCase);
    private readonly Regex UNTAGGED_PORT_REGEX = new Regex(@"Untagged Ports: (\(\w+\/\w+\)\s+(?<ports>.*)|(?<ports>None))", RegexOptions.IgnoreCase);
    private readonly Regex TAGGED_PORT_REGEX = new Regex(@"Tagged Ports: (\(\w+\/\w+\)\s+(?<ports>.*)|(?<ports>None))", RegexOptions.IgnoreCase);

    public ShowVlan(IEnumerable<string> settings) {
      this.Settings = settings;
    }

    private IEnumerable<Vlan> _Vlans;

    public IEnumerable<Vlan> Vlans {
      get {
        if (_Vlans == null) {
          var list = new List<Vlan>();
          for (int i = 0; i < Settings.Count(); i++) {
            var m0 = VLAN_REGEX.Match(Settings.ElementAt(i));
            if (m0.Success) {
              var v1 = new Vlan {
                Number = int.Parse(m0.Groups["number"].Value),
                Name = m0.Groups["name"].Value
              };
              list.Add(v1);
              List<int> untaggedVlans = new List<int>();
              List<int> taggedVlans = new List<int>();
              i++;
              while (string.IsNullOrEmpty(Settings.ElementAt(i)) || UNTAGGED_PORT_REGEX.Match(Settings.ElementAt(i)).Success) {
                if (string.IsNullOrEmpty(Settings.ElementAt(i))) { i++; continue; }
                var m1 = UNTAGGED_PORT_REGEX.Match(Settings.ElementAt(i));
                if (m1.Groups["ports"].Value.Equals("none", StringComparison.OrdinalIgnoreCase)) { i++; break; }
                untaggedVlans.AddRange(m1.Groups["ports"].Value.Trim().Split(default(string[]), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)));
                i++;
              }
              v1.UnTaggedPorts = untaggedVlans.ToArray();

              while (string.IsNullOrEmpty(Settings.ElementAt(i)) || TAGGED_PORT_REGEX.Match(Settings.ElementAt(i)).Success) {
                if (string.IsNullOrEmpty(Settings.ElementAt(i))) { i++; continue; }
                var m1 = TAGGED_PORT_REGEX.Match(Settings.ElementAt(i));
                if (m1.Groups["ports"].Value.Equals("none", StringComparison.OrdinalIgnoreCase)) { i++; break; }
                taggedVlans.AddRange(m1.Groups["ports"].Value.Trim().Split(default(string[]), StringSplitOptions.RemoveEmptyEntries).Select(c => int.Parse(c)));
                i++;
              }
              v1.TaggedPorts = taggedVlans.ToArray();
            }
          }
          _Vlans = list;
        }
        return _Vlans;
      }
    }

    public class Vlan {

      public int Number { get; set; }

      public string Name { get; set; }

      public int[] UnTaggedPorts { get; set; }

      public int[] TaggedPorts { get; set; }
    }
  }
}