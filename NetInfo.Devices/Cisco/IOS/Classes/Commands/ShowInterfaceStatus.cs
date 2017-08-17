using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS.Classes.Commands {

  public class ShowInterfaceStatus : BaseSetting {

    public enum InterfaceTypes {
      Fa,
      Gi,
      Te,
      Po
    }

    public enum InterfaceStatus {
      disabled,
      connected,
      notconnected,
      notconnect,
      monitoring,
      inactive,
      err_disabled,
      faulty
    }

    public enum InterfaceDuplex {
      auto,
      full,
      trunk,
      half
    }

    public static readonly Regex ShowInterfaceStatusRegx =
      new Regex(@"^(?<interfacetype>Te\d+\/\d+(\/\d+)?|Gi\d+\/\d+(\/\d+)?|Fa\d+\/?(\d+)?(\/\d+)?|Po\w+|Te\d+\/\d+)\s+(?<description>.*)\s+(?<status>faulty|err-disabled|connected|inactive|disabled|disconnected|notconnected|notconnect|monitoring)\s+(?<vlan>[0-9]+|routed|trunk|unassigne|unassigned)\s+(a-)?(?<duplex>\w+)\s+(a-)?(?<speed>\w+)\s?(?<type>.*)?$", RegexOptions.IgnoreCase);

    public ShowInterfaceStatus(IEnumerable<string> settings) {
      this.Settings = settings;
    }

    public IEnumerable<InterfaceStatusResult> Interfaces {
      get {
        return Settings.Where(c => ShowInterfaceStatusRegx.Match(c).Success).Select(c => {
          var result = ShowInterfaceStatusRegx.Match(c);
          if (result.Groups.Count < 2) { return null; }
          return new InterfaceStatusResult {
            InterfaceType = (InterfaceTypes)Enum.Parse(typeof(InterfaceTypes), result.Groups["interfacetype"].Value.Substring(0, 2)),
            Name = result.Groups["interfacetype"].Value,
            Description = result.Groups["description"].Value,
            Status = (InterfaceStatus)Enum.Parse(typeof(InterfaceStatus), result.Groups["status"].Value.Replace('-', '_')),
            Vlan = result.Groups["vlan"].Value,
            Duplex = (InterfaceDuplex)Enum.Parse(typeof(InterfaceDuplex), result.Groups["duplex"].Value),
            Speed = result.Groups["speed"].Value,
            Type = result.Groups["type"].Value
          };
        });
      }
    }

    public class InterfaceStatusResult {

      public InterfaceTypes InterfaceType { get; set; }

      public string Name { get; set; }

      public string Description { get; set; }

      public InterfaceStatus Status { get; set; }

      public string Vlan { get; set; }

      public InterfaceDuplex Duplex { get; set; }

      public string Speed { get; set; }

      public string Type { get; set; }
    }
  }
}