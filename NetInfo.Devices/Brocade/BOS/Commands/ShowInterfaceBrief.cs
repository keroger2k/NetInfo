using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS.Commands {

  public class ShowInterfaceBrief {

    public enum LinkStatus {
      Up,
      Down,
      Disable,
      ERRDIS
    }

    public enum StateStatus {
      Forward,
      None,
      NA
    }

    public enum DuplexStatus {
      Full,
      Half,
      None,
      NA
    }

    public enum SpeedStatus {
      SixteenGigabit,
      Gigabit,
      HundredMegabit,
      None,
      NA
    }

    public static readonly Regex ShowInterfaceBriefRegx =
      new Regex(@"^(?<Port>\d+\/\d+\/\d+|mgmt\d+|ve\d+)\s+(?<Link>Up|Down|Disable|ERR-DIS)\s+(?<State>Forward|None|N\/A)\s+(?<Duplex>Full|Half|None|N\/A)\s+(?<Speed>16G|1G|10M|100M|None|N\/A)\s+(?<Trunk>None|Yes|No|\d+)\s+(?<Tag>Yes|No|N\/A)\s+(?<Pvid>N\/A|\d+|None)\s+(?<Pri>\d+|N\/A)\s+(?<MAC>[0-9a-f]{4}\.[0-9a-f]{4}\.[0-9a-f]{4})(?<Name>.*)?$", RegexOptions.IgnoreCase);

    private readonly IEnumerable<string> _output;

    public ShowInterfaceBrief(IEnumerable<string> output) {
      var regex = new Regex(@"^Port\s+Link.*$", RegexOptions.IgnoreCase);
      this._output = output.Where(c => !regex.Match(c).Success);
    }

    public IEnumerable<InterfaceBriefResult> Interfaces {
      get {
        return _output.Select(c => {
          var result = ShowInterfaceBriefRegx.Match(c);
          if (result.Groups.Count == 1) { return null; }
          return new InterfaceBriefResult(result);
        });
      }
    }

    public class InterfaceBriefResult {
      private readonly Match _result;

      public InterfaceBriefResult(Match result) {
        this._result = result;
        Port = result.Groups["Port"].Value;
        Link = (LinkStatus)Enum.Parse(typeof(LinkStatus), result.Groups["Link"].Value.Equals("ERR-DIS") ? "ERRDIS" : result.Groups["Link"].Value);
        State = (StateStatus)Enum.Parse(typeof(StateStatus), result.Groups["State"].Value.Replace("/", string.Empty));
        Duplex = (DuplexStatus)Enum.Parse(typeof(DuplexStatus), result.Groups["Duplex"].Value.Replace("/", string.Empty));
        Speed = ParseSpeed(result.Groups["Speed"].Value);
        Trunk = !new[] { "None", "No" }.Contains(result.Groups["Trunk"].Value);
        Tag = result.Groups["Tag"].Value.Equals("N/A", StringComparison.OrdinalIgnoreCase) ? new System.Nullable<bool>() : result.Groups["Tag"].Value.Equals("yes", StringComparison.OrdinalIgnoreCase);
        Pvid = result.Groups["Pvid"].Value;
        Pri = result.Groups["Pri"].Value;
        MAC = result.Groups["MAC"].Value;
        Name = result.Groups["Name"].Value.Trim();
      }

      public string Port { get; set; }

      public LinkStatus Link { get; set; }

      public StateStatus State { get; set; }

      public DuplexStatus Duplex { get; set; }

      public SpeedStatus Speed { get; set; }

      public bool Trunk { get; set; }

      public bool? Tag { get; set; }

      public string Pvid { get; set; }

      public string Pri { get; set; }

      public string MAC { get; set; }

      public string Name { get; set; }

      private SpeedStatus ParseSpeed(string value) {
        switch (value) {
          case "16G":
            return SpeedStatus.SixteenGigabit;

          case "1G":
            return SpeedStatus.Gigabit;

          case "100M":
            return SpeedStatus.HundredMegabit;

          case "None":
            return SpeedStatus.None;

          default:
            return SpeedStatus.NA;
        }
      }
    }
  }
}