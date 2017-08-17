using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetInfo.Devices.Cisco.IOS {

  public class RouteMap {

    public string Name { get; set; }

    public IEnumerable<string> Matches { get; set; }

    public IEnumerable<int> GetMatchStandardAccessLists() {
      var rgx = new Regex(@"match ip address (?<aclnumber>\d+)", RegexOptions.IgnoreCase);
      return Matches.Where(c => rgx.Match(c).Success).Select(c => int.Parse(rgx.Match(c).Groups["aclnumber"].Value));
    }
  }
}