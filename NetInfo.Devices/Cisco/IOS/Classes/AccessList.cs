using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class ExtendedAccessList {

    public string Name { get; set; }

    public IEnumerable<string> Rules { get; set; }

    public IEnumerable<string> RulesNoComments {
      get {
        return this.Rules.Where(c => !new Regex(@"^\s+remark.*", RegexOptions.IgnoreCase).Match(c).Success);
      }
    }
  }

  public class StandardAccessList {

    public int Number {
      get {
        return int.Parse(new Regex(@"access-list\s+(\d+).*", RegexOptions.IgnoreCase).Match(this.Rules.First()).Groups[1].Value);
      }
    }

    public IEnumerable<string> Rules { get; set; }

    public IEnumerable<string> RulesNoComments {
      get {
        return this.Rules.Where(c => !new Regex(@"access-list\s+\d+\s+remark", RegexOptions.IgnoreCase).Match(c).Success);
      }
    }
  }
}