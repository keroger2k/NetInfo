using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Brocade.BOS.Commands {

  public class ShowDot1x {
    private readonly IEnumerable<string> _output;

    public ShowDot1x(IEnumerable<string> output) {
      this._output = output;
    }

    public Dot1xResult Dot1x {
      get {
        return _output.Count() >= 11 ?
         new Dot1xResult {
           ReAuthPeriod = int.Parse(new Regex(@"re-authperiod\s+:\s+(\d+)\s+Seconds", RegexOptions.IgnoreCase).Match(_output.ElementAt(10)).Groups[1].Value)
         } :
        new Dot1xResult { ReAuthPeriod = int.MaxValue };
      }
    }

    public class Dot1xResult {

      public int ReAuthPeriod { get; set; }
    }
  }
}