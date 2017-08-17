using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS.Commands {

  public class ShowVersion {
    private static readonly Regex UnitRegex = new Regex(@"UNIT (?<number>\d+): compiled on .* labeled as (?<image>\w+)", RegexOptions.IgnoreCase);
    private static readonly Regex SoftwareRegex = new Regex(@"SW: Version (?<version>.*)", RegexOptions.IgnoreCase);
    private readonly IEnumerable<string> _output;

    public ShowVersion(IEnumerable<string> output) {
      this._output = output;
    }

    public IEnumerable<Unit> Units {
      get {
        var list = new List<Unit>();
        for (int i = 0; i < _output.Count(); i++) {
          if (UnitRegex.Match(_output.ElementAt(i)).Success) {
            var m = UnitRegex.Match(_output.ElementAt(i));
            i += 2;
            var v = SoftwareRegex.Match(_output.ElementAt(i));
            var u = new Unit();
            u.Number = int.Parse(m.Groups["number"].Value);
            u.ImageName = m.Groups["image"].Value;
            u.SoftwareVersion = v.Groups["version"].Value;
            list.Add(u);
          }
        }
        return list;
      }
    }

    public class Unit {

      public int Number { get; set; }

      public string ImageName { get; set; }

      public string SoftwareVersion { get; set; }
    }
  }
}